using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Hospital.Application.Mappers;

namespace Hospital.Application.Services;

/// <summary>Service for managing specializations.</summary>
public class SpecializationService(IRepository<Specialization> repository) : ISpecializationService
{
    /// <summary>
    /// Creates a new specialization.
    /// </summary>
    /// <param name="request">The specialization data to create.</param>
    /// <returns>The created specialization.</returns>
    public async Task<SpecializationResponse> CreateSpecializationAsync(SpecializationRequest request)
    {
        var specialization = request.ToDomain();
        var createdSpecialization = await repository.CreateAsync(specialization);
        return createdSpecialization.ToResponse();
    }

    /// <summary>
    /// Returns all specializations.
    /// </summary>
    /// <returns>List of all specializations.</returns>
    public async Task<List<SpecializationResponse>> GetAllSpecializationsAsync()
    {
        var specializations = await repository.GetAllAsync();
        return specializations.Select(s => s.ToResponse()).ToList();
    }

    /// <summary>
    /// Returns a specialization by ID.
    /// </summary>
    /// <param name="id">The ID of the specialization.</param>
    /// <returns>The specialization with the specified ID, or <c>null</c> if not found.</returns>
    public async Task<SpecializationResponse?> GetSpecializationAsync(Guid id)
    {
        var specialization = await repository.GetByIdAsync(id);
        return specialization?.ToResponse();
    }

    /// <summary>
    /// Updates an existing specialization.
    /// </summary>
    /// <param name="id">The ID of the specialization to update.</param>
    /// <param name="request">The updated specialization data.</param>
    /// <returns>The updated specialization, or <c>null</c> if not found.</returns>
    public async Task<SpecializationResponse?> UpdateSpecializationAsync(Guid id, SpecializationRequest request)
    {
        var specialization = await repository.GetByIdAsync(id);

        if (specialization is null)
            return null;

        request.MapTo(specialization);
        var updatedAppointment = await repository.UpdateAsync(specialization);
        return updatedAppointment?.ToResponse();
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