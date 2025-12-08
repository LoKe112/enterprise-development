using Hospital.Application.Services;
using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Hospital.Domain;
using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataSeeder>();

builder.AddMySqlDbContext<HospitalDbContext>("HospitalDatabase",
    settings =>
    {
        settings.ServerVersion = "9.5.0";
    },
    optionsBuilder =>
    {
        optionsBuilder.UseSnakeCaseNamingConvention();

    });

builder.Services.AddScoped<IRepository<Specialization>, SpecializationRepository>();
builder.Services.AddScoped<IRepository<Doctor>, DoctorRepository>();
builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();

builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);

    var contractsAssembly = typeof(AppointmentResponse).Assembly;
    var contractsXml = $"{contractsAssembly.GetName().Name}.xml";
    var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, contractsXml);

    c.IncludeXmlComments(contractsXmlPath);
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();