using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Domain.Services.Abstractions;

namespace Hospital.Domain.Services;

/// <summary>Service for managing specializations.</summary>
public class SpecializationService(IRepository<Specialization> repository) : ISpecializationService
{
    /// <summary>
    /// Creates a new specialization.
    /// </summary>
    /// <param name="specialization">Specialization.</param>
    /// <returns>The ID of the created specialization.</returns>
    public async Task<Specialization> CreateSpecializationAsync(Specialization specialization)
    {
        return await repository.CreateAsync(specialization);
    }

    /// <summary>
    /// Returns all specializations.
    /// </summary>
    /// <returns>List of all specializations.</returns>
    public async Task<List<Specialization>> GetAllSpecializationsAsync()
    {
        return await repository.GetAllAsync();
    }

    /// <summary>
    /// Returns a specialization by ID.
    /// </summary>
    /// <param name="id">The ID of the specialization.</param>
    /// <returns>The specialization with the specified ID, or <c>null</c> if not found.</returns>
    public async Task<Specialization?> GetSpecializationAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing specialization.
    /// </summary>
    /// <param name="id">The ID of the specialization to update.</param>
    /// <param name="entity">The updated specialization data.</param>
    /// <returns>The updated specialization, or <c>null</c> if not found.</returns>
    public async Task<Specialization?> UpdateSpecializationAsync(Guid id, Specialization entity)
    {
        entity.Id = id;
        return await repository.UpdateAsync(entity);
    }

    /// <summary>
    /// Deletes a specialization by ID.
    /// </summary>
    /// <param name="id">The ID of the specialization to delete.</param>
    /// <returns><c>true</c> if the specialization was deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteSpecializationAsync(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}
