using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Hospital.Application.Mappers;

namespace Hospital.Application.Services;

/// <summary>Service for managing patients.</summary>
public class PatientService(IRepository<Patient> repository) : IPatientService
{
    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="request">The patient data to create.</param>
    /// <returns>The created patient.</returns>
    public async Task<PatientResponse> CreatePatientAsync(PatientRequest request)
    {
        var patient = request.ToDomain();
        var createdPatient = await repository.CreateAsync(patient);
        return createdPatient.ToResponse();
    }

    /// <summary>
    /// Returns all patients.
    /// </summary>
    /// <returns>List of all patients.</returns>
    public async Task<List<PatientResponse>> GetAllPatientsAsync()
    {
        var patients = await repository.GetAllAsync();
        return patients.Select(p => p.ToResponse()).ToList();
    }

    /// <summary>
    /// Returns a patient by ID.
    /// </summary>
    /// <param name="id">The ID of the patient.</param>
    /// <returns>The patient with the specified ID, or <c>null</c> if not found.</returns>
    public async Task<PatientResponse?> GetPatientAsync(Guid id)
    {
        var patient = await repository.GetByIdAsync(id);
        return patient?.ToResponse();
    }

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="id">The ID of the patient to update.</param>
    /// <param name="request">The updated patient data.</param>
    /// <returns>The updated patient, or <c>null</c> if not found.</returns>
    public async Task<PatientResponse?> UpdatePatientAsync(Guid id, PatientRequest request)
    {
        var patient = await repository.GetByIdAsync(id);

        if (patient is null)
            return null;

        request.MapTo(patient);
        var updatedAppointment = await repository.UpdateAsync(patient);
        return updatedAppointment?.ToResponse();
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