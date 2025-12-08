using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing doctors.
/// </summary>
public class DoctorRepository(HospitalDbContext dbContext) : IRepository<Doctor>
{
    private readonly HospitalDbContext _dbContext = dbContext;

    public async Task<Doctor> CreateAsync(Doctor entity)
    {
        _dbContext.Doctors.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Doctor>> GetAllAsync()
    {
        return await _dbContext.Doctors.Include(d => d.Specialization).ToListAsync();
    }

    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Doctors.Include(d => d.Specialization).FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Doctor?> UpdateAsync(Doctor entity)
    {
        var storedEntity = await _dbContext.Doctors.FindAsync(entity.Id);

        if (storedEntity is null)
            return null;

        _dbContext.Doctors.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var count = await _dbContext.Doctors.Where(x => x.Id == id).ExecuteDeleteAsync();

        return count > 0;
    }
}