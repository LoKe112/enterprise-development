using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Hospital.Application.Mappers;

namespace Hospital.Application.Services;

/// <summary>
/// Service for managing doctors.
/// </summary>
public class DoctorService(IRepository<Doctor> repository) : IDoctorService
{
    /// <summary>
    /// Creates a new doctor.
    /// </summary>
    /// <param name="request">The doctor data to create.</param>
    /// <returns>The created doctor.</returns>
    public async Task<DoctorResponse> CreateDoctorAsync(DoctorRequest request)
    {
        var doctor = request.ToDomain();
        var createdDoctor = await repository.CreateAsync(doctor);
        return createdDoctor.ToResponse();
    }

    /// <summary>
    /// Returns all doctors.
    /// </summary>
    /// <returns>List of all doctors.</returns>
    public async Task<List<DoctorResponse>> GetAllDoctorsAsync()
    {
        var doctors = await repository.GetAllAsync();
        return [.. doctors.Select(a => a.ToResponse())];
    }

    /// <summary>
    /// Returns a doctor by ID.
    /// </summary>
    /// <param name="id">The ID of the doctor.</param>
    /// <returns>The doctor with the specified ID, or <c>null</c> if not found.</returns>
    public async Task<DoctorResponse?> GetDoctorAsync(Guid id)
    {
        var doctor = await repository.GetByIdAsync(id);
        return doctor?.ToResponse();
    }

    /// <summary>
    /// Updates an existing doctor.
    /// </summary>
    /// <param name="id">The ID of the doctor to update.</param>
    /// <param name="request">The updated doctor data.</param>
    /// <returns>The updated doctor, or <c>null</c> if not found.</returns>
    public async Task<DoctorResponse?> UpdateDoctorAsync(Guid id, DoctorRequest request)
    {
        var doctor = await repository.GetByIdAsync(id);

        if (doctor is null)
            return null;

        request.MapTo(doctor);
        var updatedAppointment = await repository.UpdateAsync(doctor);
        return updatedAppointment?.ToResponse();
    }

    /// <summary>
    /// Deletes a doctor by ID.
    /// </summary>
    /// <param name="id">The ID of the doctor to delete.</param>
    /// <returns><c>true</c> if the doctor was deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteDoctorAsync(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}