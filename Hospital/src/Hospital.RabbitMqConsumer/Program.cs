using Hospital.Application.Services;
using Hospital.Application.Services.Abstractions;
using Hospital.Domain;
using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Repositories;
using Hospital.RabbitMqConsumer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddLogging();

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

builder.AddRabbitMQClient("RabbitMQ");

builder.Services.AddHostedService<RabbitMqConsumer>();

var app = builder.Build();

app.Run();