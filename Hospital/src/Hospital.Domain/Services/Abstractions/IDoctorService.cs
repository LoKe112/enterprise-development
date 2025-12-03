using Hospital.Domain.Models;

namespace Hospital.Domain.Services.Abstractions;

/// <summary>
/// Interface for the doctor service.
/// </summary>
public interface IDoctorService
{
    /// <summary>
    /// Creates a new doctor.
    /// </summary>
    /// <param name="doctor">The doctor to create.</param>
    /// <returns>The Id of the created doctor.</returns>
    public Task<Doctor> CreateDoctorAsync(Doctor doctor);

    /// <summary>
    /// Deletes a doctor by Id.
    /// </summary>
    /// <param name="guid">The Id of the doctor to delete.</param>
    /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeleteDoctorAsync(Guid guid);

    /// <summary>
    /// Returns a doctor by Id.
    /// </summary>
    /// <param name="guid">The Id of the doctor.</param>
    /// <returns>The doctor with the specified Id, or <c>null</c> if not found.</returns>
    public Task<Doctor?> GetDoctorAsync(Guid guid);

    /// <summary>
    /// Returns all doctors.
    /// </summary>
    /// <returns>A list of all doctors.</returns>
    public Task<List<Doctor>> GetAllDoctorsAsync();

    /// <summary>
    /// Updates a doctor.
    /// </summary>
    /// <param name="guid">The Id of the doctor to update.</param>
    /// <param name="doctor">The doctor data.</param>
    /// <returns>The updated doctor, or <c>null</c> if not found.</returns>
    public Task<Doctor?> UpdateDoctorAsync(Guid guid, Doctor doctor);
}