using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing doctors.
/// </summary>
public class PatientRepository : IRepository<Patient>
{
    private readonly HospitalDbContext _dbContext;

    public PatientRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Patient> CreateAsync(Patient entity)
    {
        _dbContext.Patients.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _dbContext.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Patients.FindAsync(id);
    }

    public async Task<Patient?> UpdateAsync(Patient entity)
    {
        Patient? storedEntity = await _dbContext.Patients.FindAsync(entity.Id);

        if (storedEntity is null)
            return null;

        _dbContext.Patients.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var count = await _dbContext.Patients.Where(x => x.Id == id).ExecuteDeleteAsync();

        return count > 0;
    }
}
