using System.Text;
using System.Text.Json;
using Hospital.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Hospital.RabbitMqProducer;

/// <summary>
/// Background hosted service responsible for generating test data
/// and publishing messages to RabbitMQ queues.
/// </summary>
internal sealed class RabbitMqProducer : BackgroundService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly DataGenerator _dataGenerator;
    private readonly ILogger<RabbitMqProducer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqProducer"/> class.
    /// </summary>
    /// <param name="connectionFactory">RabbitMQ connection factory.</param>
    /// <param name="dataGenerator">Service responsible for generating test data.</param>
    /// <param name="logger">Logger instance.</param>
    public RabbitMqProducer(
        IConnectionFactory connectionFactory,
        DataGenerator dataGenerator,
        ILogger<RabbitMqProducer> logger)
    {
        _connectionFactory = connectionFactory;
        _dataGenerator = dataGenerator;
        _logger = logger;
    }

    /// <summary>
    /// Starts the producer by creating a RabbitMQ connection and channel,
    /// declaring required queues, generating data, and publishing messages.
    /// </summary>
    /// <param name="stoppingToken">
    /// Cancellation token that is triggered when the hosted service is stopping.
    /// </param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var connection =
            await _connectionFactory.CreateConnectionAsync(stoppingToken);

        await using var channel =
            await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await DeclareQueuesAsync(channel, stoppingToken);

        _logger.LogInformation("RabbitMQ Producer started");

        var specializations = await _dataGenerator.GenerateSpecoalizations(5);
        await PublishAsync(channel, RabbitQueues.Specializations, specializations, stoppingToken);

        var doctors = await _dataGenerator.GenerateDoctors(10);
        await PublishAsync(channel, RabbitQueues.Doctors, doctors, stoppingToken);

        var patients = await _dataGenerator.GeneratePatients(20);
        await PublishAsync(channel, RabbitQueues.Patients, patients, stoppingToken);

        var appointments = await _dataGenerator.GenerateAppointments(30);
        await PublishAsync(channel, RabbitQueues.Appointments, appointments, stoppingToken);

        _logger.LogInformation("All messages published");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    /// <summary>
    /// Declares all RabbitMQ queues required by the producer.
    /// </summary>
    /// <param name="channel">The RabbitMQ channel.</param>
    /// <param name="ct">Cancellation token.</param>
    private static async Task DeclareQueuesAsync(
        IChannel channel,
        CancellationToken ct)
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

    /// <summary>
    /// Publishes a collection of messages to the specified RabbitMQ queue.
    /// </summary>
    /// <typeparam name="T">The type of the message payload.</typeparam>
    /// <param name="channel">The RabbitMQ channel.</param>
    /// <param name="queue">The target queue name.</param>
    /// <param name="messages">The messages to publish.</param>
    /// <param name="ct">Cancellation token.</param>
    private async Task PublishAsync<T>(
        IChannel channel,
        string queue,
        IEnumerable<T> messages,
        CancellationToken ct)
    {
        foreach (var message in messages)
        {
            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(message));

            var props = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queue,
                mandatory: false,
                basicProperties: props,
                body: body,
                cancellationToken: ct);
        }

        _logger.LogInformation(
            "Published {Count} messages to {Queue}",
            messages.Count(),
            queue);
    }
}

/// <summary>
/// Contains RabbitMQ queue names used by the producer.
/// </summary>
internal static class RabbitQueues
{
    public const string Specializations = "specializations.create";
    public const string Doctors = "doctors.create";
    public const string Patients = "patients.create";
    public const string Appointments = "appointments.create";
}