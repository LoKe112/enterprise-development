using Hospital.RabbitMqProducer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogging();

builder.AddRabbitMQClient("RabbitMQ");

builder.Services.AddServiceDiscovery();

builder.Services.AddHttpClient("hospital-api", client =>
{
    client.BaseAddress = new Uri("https+http://hospital-api");   
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddServiceDiscovery();

builder.Services.AddTransient<DataGenerator>();

builder.Services.AddHostedService<RabbitMqProducer>();

var app = builder.Build();

app.Run();