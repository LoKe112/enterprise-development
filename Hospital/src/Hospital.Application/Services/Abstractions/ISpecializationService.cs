using Hospital.Contracts;

namespace Hospital.Application.Services.Abstractions;

/// <summary>
/// Interface for the specialization service.
/// </summary>
public interface ISpecializationService
{
    /// <summary>
    /// Creates a new specialization.
    /// </summary>
    /// <param name="request">The specialization data to create.</param>
    /// <returns>The created specialization.</returns>
    public Task<SpecializationResponse> CreateSpecializationAsync(SpecializationRequest request);

    /// <summary>
    /// Returns all specializations.
    /// </summary>
    /// <returns>A list of all specializations.</returns>
    public Task<List<SpecializationResponse>> GetAllSpecializationsAsync();

    /// <summary>
    /// Returns a specialization by Id.
    /// </summary>
    /// <param name="id">The Id of the specialization.</param>
    /// <returns>The specialization with the specified Id, or <c>null</c> if not found.</returns>
    public Task<SpecializationResponse?> GetSpecializationAsync(Guid id);

    /// <summary>
    /// Updates a specialization.
    /// </summary>
    /// <param name="id">The Id of the specialization to update.</param>
    /// <param name="request">The updated specialization data.</param>
    /// <returns>The updated specialization, or <c>null</c> if not found.</returns>
    public Task<SpecializationResponse?> UpdateSpecializationAsync(Guid id, SpecializationRequest request);

    /// <summary>
    /// Deletes a specialization by Id.
    /// </summary>
    /// <param name="id">The Id of the specialization to delete.</param>
    /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeleteSpecializationAsync(Guid id);
}