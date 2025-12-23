using Hospital.RabbitMqProducer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogging();

builder.AddRabbitMQClient("RabbitMQ");

var configuration = builder.Configuration;
var httpEndpoint = configuration["HOSPITAL_API_HTTP"];
var httpsEndpoint = configuration["HOSPITAL_API_HTTPS"];

var hospitalApiUrl = !string.IsNullOrEmpty(httpsEndpoint)
    ? httpsEndpoint
    : httpEndpoint;

if (string.IsNullOrEmpty(hospitalApiUrl))
{
    throw new InvalidOperationException(
        "Neither HOSPITAL_API_HTTP nor HOSPITAL_API_HTTPS environment variables are set.");
}

builder.Services.AddHttpClient("hospital-api", client =>
{
    client.BaseAddress = new Uri(hospitalApiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddTransient<DataGenerator>();
builder.Services.AddHostedService<RabbitMqProducer>();

var app = builder.Build();
app.Run();