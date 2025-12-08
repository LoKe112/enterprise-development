using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing appointments.
/// </summary>
public class AppointmentRepository(HospitalDbContext dbContext) : IRepository<Appointment>
{
    private readonly HospitalDbContext _dbContext = dbContext;

    public async Task<Appointment> CreateAsync(Appointment entity)
    {
        _dbContext.Appointments.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _dbContext.Appointments
            .Include(x => x.Doctor)
                .ThenInclude(x => x.Specialization)
            .Include(x => x.Patient)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Appointments
            .Include(x => x.Doctor)
                .ThenInclude(x => x.Specialization)
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Appointment?> UpdateAsync(Appointment entity)
    {
        var storedEntity = await _dbContext.Appointments.FindAsync(entity.Id);

        if (storedEntity is null)
            return null;

        _dbContext.Appointments.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var count = await _dbContext.Appointments.Where(x => x.Id == id).ExecuteDeleteAsync();

        return count > 0;
    }
}
