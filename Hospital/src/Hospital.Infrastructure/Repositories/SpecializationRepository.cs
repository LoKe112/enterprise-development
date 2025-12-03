using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories;

public class SpecializationRepository : IRepository<Specialization>
{
    private readonly HospitalDbContext _dbContext;

    public SpecializationRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Specialization> CreateAsync(Specialization entity)
    {
        _dbContext.Specializations.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Specialization>> GetAllAsync()
    {
        return await _dbContext.Specializations.ToListAsync();
    }

    public async Task<Specialization?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Specializations.FindAsync(id);
    }

    public async Task<Specialization?> UpdateAsync(Specialization entity)
    {
        Specialization? storedEntity = await _dbContext.Specializations.FindAsync(entity.Id);

        if (storedEntity is null)
            return null;

        _dbContext.Specializations.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var count = await _dbContext.Specializations.Where(x => x.Id == id).ExecuteDeleteAsync();

        return count > 0;
    }
}
