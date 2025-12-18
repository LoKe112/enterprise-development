using Hospital.RabbitMqProducer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogging();

builder.AddRabbitMQClient("RabbitMQ");

builder.Services.AddHttpClient("HospitalApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5260");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSingleton<DataGenerator>();

builder.Services.AddHostedService<RabbitMqProducer>();

var app = builder.Build();

app.Run();