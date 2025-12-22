using Hospital.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Hospital.RabbitMqProducer;

/// <summary>
/// Background hosted service responsible for periodically generating test data
/// and publishing messages to RabbitMQ queues.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RabbitMqProducer"/> class.
/// </remarks>
/// <param name="connectionFactory">RabbitMQ connection factory.</param>
/// <param name="dataGenerator">Service responsible for generating test data.</param>
/// <param name="logger">Logger instance.</param>
internal sealed class RabbitMqProducer(
    IConnectionFactory connectionFactory,
    DataGenerator dataGenerator,
    ILogger<RabbitMqProducer> logger) : BackgroundService
{
    private readonly TimeSpan _publishInterval = TimeSpan.FromSeconds(20);

    /// <summary>
    /// Starts the producer by creating a RabbitMQ connection and channel,
    /// declaring required queues, and periodically publishing generated data.
    /// </summary>
    /// <param name="stoppingToken">
    /// Cancellation token that is triggered when the hosted service is stopping.
    /// </param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var connection =
            await connectionFactory.CreateConnectionAsync(stoppingToken);

        await using var channel =
            await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await DeclareQueuesAsync(channel, stoppingToken);

        logger.LogInformation("RabbitMQ Producer started. Publishing every {Interval} seconds",
            _publishInterval.TotalSeconds);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PublishBatchAsync(channel, stoppingToken);
                logger.LogInformation("Batch of messages published successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while publishing messages");
            }

            await Task.Delay(_publishInterval, stoppingToken);
        }

        logger.LogInformation("RabbitMQ Producer stopped");
    }

    /// <summary>
    /// Publishes a batch of generated messages to all queues.
    /// </summary>
    private async Task PublishBatchAsync(IChannel channel, CancellationToken ct)
    {
        var specializations = await dataGenerator.GenerateSpecializations(5);
        await PublishAsync(channel, RabbitQueues.Specializations, specializations, ct);

        var doctors = await dataGenerator.GenerateDoctors(10);
        await PublishAsync(channel, RabbitQueues.Doctors, doctors, ct);

        var patients = await dataGenerator.GeneratePatients(20);
        await PublishAsync(channel, RabbitQueues.Patients, patients, ct);

        var appointments = await dataGenerator.GenerateAppointments(30);
        await PublishAsync(channel, RabbitQueues.Appointments, appointments, ct);
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

        logger.LogInformation(
            "Published {Count} messages to {Queue}",
            messages.Count(),
            queue);
    }
}

/// <summary>
/// Contains RabbitMQ queue names used by the producer.
/// </summary>
