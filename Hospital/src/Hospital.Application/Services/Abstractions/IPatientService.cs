using Hospital.Contracts;

namespace Hospital.Application.Services.Abstractions;

/// <summary>
/// Interface for the patient service.
/// </summary>
public interface IPatientService
{
    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="request">The patient data to create.</param>
    /// <returns>The created patient.</returns>
    public Task<PatientResponse> CreatePatientAsync(PatientRequest request);

    /// <summary>
    /// Deletes a patient by Id.
    /// </summary>
    /// <param name="id">The Id of the patient to delete.</param>
    /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeletePatientAsync(Guid id);

    /// <summary>
    /// Returns a patient by Id.
    /// </summary>
    /// <param name="id">The Id of the patient.</param>
    /// <returns>The patient with the specified Id, or <c>null</c> if not found.</returns>
    public Task<PatientResponse?> GetPatientAsync(Guid id);

    /// <summary>
    /// Returns all patients.
    /// </summary>
    /// <returns>A list of all patients.</returns>
    public Task<List<PatientResponse>> GetAllPatientsAsync();

    /// <summary>
    /// Updates a patient.
    /// </summary>
    /// <param name="id">The Id of the patient to update.</param>
    /// <param name="request">The updated patient data.</param>
    /// <returns>The updated patient, or <c>null</c> if not found.</returns>
    public Task<PatientResponse?> UpdatePatientAsync(Guid id, PatientRequest request);
}