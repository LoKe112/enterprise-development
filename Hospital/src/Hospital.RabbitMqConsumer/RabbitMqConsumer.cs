using System.Text;
using System.Text.Json;
using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Hospital.RabbitMqConsumer;

internal sealed class RabbitMqConsumer : BackgroundService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RabbitMqConsumer> _logger;

    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMqConsumer(
        IConnectionFactory connectionFactory,
        IServiceScopeFactory scopeFactory,
        ILogger<RabbitMqConsumer> logger)
    {
        _connectionFactory = connectionFactory;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _connectionFactory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await DeclareQueuesAsync(_channel, stoppingToken);

        await StartConsumeAsync<SpecializationRequest>(_channel, RabbitQueues.Specializations, HandleSpecializationAsync, stoppingToken);
        await StartConsumeAsync<DoctorRequest>(_channel, RabbitQueues.Doctors, HandleDoctorAsync, stoppingToken);
        await StartConsumeAsync<PatientRequest>(_channel, RabbitQueues.Patients, HandlePatientAsync, stoppingToken);
        await StartConsumeAsync<AppointmentRequest>(_channel, RabbitQueues.Appointments, HandleAppointmentAsync, stoppingToken);

        _logger.LogInformation("RabbitMQ Consumer started");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private static async Task DeclareQueuesAsync(IChannel channel, CancellationToken ct)
    {
        foreach (var queue in new[]
        {
            RabbitQueues.Specializations,
            RabbitQueues.Doctors,
            RabbitQueues.Patients,
            RabbitQueues.Appointments
        })
        {
            await channel.QueueDeclareAsync(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: ct);
        }
    }

    private async Task StartConsumeAsync<T>(
        IChannel channel,
        string queue,
        Func<T, IServiceScope, Task> handler,
        CancellationToken ct)
    {
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            try
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<T>(body)!;

                await handler(message, scope);

                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message from {Queue}", queue);

                await channel.BasicNackAsync(
                    ea.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }
        };

        await channel.BasicConsumeAsync(
            queue: queue,
            autoAck: false,
            consumer: consumer,
            cancellationToken: ct);
    }

    // ===================== HANDLERS =====================

    private static async Task HandleSpecializationAsync(
        SpecializationRequest request,
        IServiceScope scope)
    {
        var service = scope.ServiceProvider
            .GetRequiredService<ISpecializationService>();

        await service.CreateSpecializationAsync(request);
    }

    private static async Task HandleDoctorAsync(
        DoctorRequest request,
        IServiceScope scope)
    {
        var service = scope.ServiceProvider
            .GetRequiredService<IDoctorService>();

        await service.CreateDoctorAsync(request);
    }

    private static async Task HandlePatientAsync(
        PatientRequest request,
        IServiceScope scope)
    {
        var service = scope.ServiceProvider
            .GetRequiredService<IPatientService>();

        await service.CreatePatientAsync(request);
    }

    private static async Task HandleAppointmentAsync(
        AppointmentRequest request,
        IServiceScope scope)
    {
        var service = scope.ServiceProvider
            .GetRequiredService<IAppointmentService>();

        await service.CreateAppointmentAsync(request);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel is not null)
            await _channel.DisposeAsync();

        if (_connection is not null)
            await _connection.DisposeAsync();

        await base.StopAsync(cancellationToken);
    }
}

internal static class RabbitQueues
{
    public const string Specializations = "specializations.create";
    public const string Doctors = "doctors.create";
    public const string Patients = "patients.create";
    public const string Appointments = "appointments.create";
}