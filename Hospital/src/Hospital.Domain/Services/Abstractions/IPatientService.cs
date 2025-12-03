using Hospital.Domain.Models;

namespace Hospital.Domain.Services.Abstractions;

/// <summary>
/// Interface for the patient service.
/// </summary>
public interface IPatientService
{
    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patient">The patient to create.</param>
    /// <returns>The Id of the created patient.</returns>
    public Task<Patient> CreatePatientAsync(Patient patient);

    /// <summary>
    /// Deletes a patient by Id.
    /// </summary>
    /// <param name="guid">The Id of the patient to delete.</param>
    /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeletePatientAsync(Guid guid);

    /// <summary>
    /// Returns a patient by Id.
    /// </summary>
    /// <param name="guid">The Id of the patient.</param>
    /// <returns>The patient with the specified Id, or <c>null</c> if not found.</returns>
    public Task<Patient?> GetPatientAsync(Guid guid);

    /// <summary>
    /// Returns all patients.
    /// </summary>
    /// <returns>A list of all patients.</returns>
    public Task<List<Patient>> GetAllPatientsAsync();

    /// <summary>
    /// Updates a patient.
    /// </summary>
    /// <param name="guid">The Id of the patient to update.</param>
    /// <param name="patient">The patient data.</param>
    /// <returns>The updated patient, or <c>null</c> if not found.</returns>
    public Task<Patient?> UpdatePatientAsync(Guid guid, Patient patient);
}