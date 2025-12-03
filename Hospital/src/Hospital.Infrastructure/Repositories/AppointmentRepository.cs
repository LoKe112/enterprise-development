using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class AppointmentRepository : IRepository<Appointment>
{
    private readonly HospitalDbContext _dbContext;

    public AppointmentRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Appointment> CreateAsync(Appointment entity)
    {
        _dbContext.Appointments.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _dbContext.Appointments.ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Appointments.FindAsync(id);
    }

    public async Task<Appointment?> UpdateAsync(Appointment entity)
    {
        Appointment? storedEntity = await _dbContext.Appointments.FindAsync(entity.Id);

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
