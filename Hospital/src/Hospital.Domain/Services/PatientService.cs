using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Domain.Services.Abstractions;

namespace Hospital.Domain.Services;

/// <summary>Service for managing patients.</summary>
public class PatientService(IRepository<Patient> repository) : IPatientService
{
    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patient">Patient.</param>
    /// <returns>The ID of the created patient.</returns>
    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        return await repository.CreateAsync(patient);
    }

    /// <summary>
    /// Returns all patients.
    /// </summary>
    /// <returns>List of all patients.</returns>
    public async Task<List<Patient>> GetAllPatientsAsync()
    {
        return await repository.GetAllAsync();
    }

    /// <summary>
    /// Returns a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient.</param>
    /// <returns>The patient with the specified ID, or <c>null</c> if not found.</returns>
    public async Task<Patient?> GetPatientAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="id">The ID of the patient to update.</param>
    /// <param name="entity">The updated patient data.</param>
    /// <returns>The updated patient, or <c>null</c> if not found.</returns>
    public async Task<Patient?> UpdatePatientAsync(Guid id, Patient entity)
    {
        entity.Id = id;
        return await repository.UpdateAsync(entity);
    }

    /// <summary>
    /// Deletes a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient to delete.</param>
    /// <returns><c>true</c> if the patient was deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeletePatientAsync(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}
