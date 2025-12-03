using Hospital.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure;

public class HospitalDbContext : DbContext
{
    private readonly DataSeeder _dataSeeder;

    public HospitalDbContext(DbContextOptions options, DataSeeder dataSeeder) : base(options)
    {
        _dataSeeder = dataSeeder;
    }

    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Specialization>().ToTable("specializations");

        modelBuilder.Entity<Specialization>(options =>
        {
            options.HasKey(x => x.Id);

            options.Property(x => x.Name)
                .IsRequired();

            options.HasData(_dataSeeder.Specializations);
        });

        // todo
    }
}
