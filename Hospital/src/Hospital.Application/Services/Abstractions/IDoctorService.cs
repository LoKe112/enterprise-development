using Hospital.Contracts;

namespace Hospital.Application.Services.Abstractions;

/// <summary>
/// Interface for the doctor service.
/// </summary>
public interface IDoctorService
{
    /// <summary>
    /// Creates a new doctor.
    /// </summary>
    /// <param name="request">The doctor data to create.</param>
    /// <returns>The created doctor.</returns>
    public Task<DoctorResponse> CreateDoctorAsync(DoctorRequest request);

    /// <summary>
    /// Deletes a doctor by Id.
    /// </summary>
    /// <param name="id">The Id of the doctor to delete.</param>
    /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeleteDoctorAsync(Guid id);

    /// <summary>
    /// Returns a doctor by Id.
    /// </summary>
    /// <param name="id">The Id of the doctor.</param>
    /// <returns>The doctor with the specified Id, or <c>null</c> if not found.</returns>
    public Task<DoctorResponse?> GetDoctorAsync(Guid id);

    /// <summary>
    /// Returns all doctors.
    /// </summary>
    /// <returns>A list of all doctors.</returns>
    public Task<List<DoctorResponse>> GetAllDoctorsAsync();

    /// <summary>
    /// Updates a doctor.
    /// </summary>
    /// <param name="id">The Id of the doctor to update.</param>
    /// <param name="request">The updated doctor data.</param>
    /// <returns>The updated doctor, or <c>null</c> if not found.</returns>
    public Task<DoctorResponse?> UpdateDoctorAsync(Guid id, DoctorRequest request);
}