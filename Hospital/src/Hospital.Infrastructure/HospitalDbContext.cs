using Hospital.Domain;
using Hospital.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure;

public class HospitalDbContext(DbContextOptions options, DataSeeder dataSeeder) : DbContext(options)
{
    private readonly DataSeeder _dataSeeder = dataSeeder;

    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Specialization>(options =>
        {
            options.HasKey(x => x.Id);
            
            options.Property(x => x.Name)
                .IsRequired();

            options.HasData(_dataSeeder.Specializations);
        });

        modelBuilder.Entity<Doctor>(options =>
        {
            options.HasKey(x => x.Id);

            options.Property(x => x.PassportNumber)
                .IsRequired();

            options.Property(x => x.FullName)
                .IsRequired();

            options.Property(x => x.YearOfBirth)
                .IsRequired();

            options.Property(x => x.ExperienceYears)
                .IsRequired();

            options.HasOne(x => x.Specialization)
                .WithMany()
                .HasForeignKey(x => x.SpecializationId)
                .OnDelete(DeleteBehavior.Cascade);

            options.HasData(_dataSeeder.Doctors);
        });

        modelBuilder.Entity<Patient>(options =>
        {
            options.HasKey(x => x.Id);

            options.Property(x => x.PassportNumber)
                .IsRequired();

            options.Property(x => x.FullName)
                .IsRequired();

            options.Property(x => x.BloodGroup)
                .IsRequired();

            options.Property(x => x.Address)
                .IsRequired();

            options.Property(x => x.RhFactor)
                .IsRequired();

            options.Property(x => x.Gender)
                .IsRequired();

            options.Property(x => x.PhoneNumber)
                .IsRequired();

            options.HasData(_dataSeeder.Patients);
        });

        modelBuilder.Entity<Appointment>(options =>
        {
            options.HasKey(x => x.Id);

            options.Property(x => x.RoomNumber)
                .IsRequired();

            options.Property(x => x.IsFollowUp)
                .IsRequired();

            options.Property(x => x.AppointmentDateTime)
                .IsRequired();

            options.HasOne(x => x.Doctor)
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            options.HasOne(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            options.HasData(_dataSeeder.Appointments);
        });
        
    }
}
