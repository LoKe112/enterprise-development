using Hospital.Application.Mappers;
using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;

namespace Hospital.Application.Services;

/// <summary>
/// Service for managing appointments.
/// </summary>
public class AppointmentService(IRepository<Appointment> repository) : IAppointmentService
{
    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    /// <param name="request">The appointment to create.</param>
    /// <returns>The ID of the created appointment.</returns>
    public async Task<AppointmentResponse> CreateAppointmentAsync(AppointmentRequest request)
    {
        var appointment = request.ToDomain();
        var createdAppointment = await repository.CreateAsync(appointment);
        return createdAppointment.ToResponse();
    }

    /// <summary>
    /// Returns all appointments.
    /// </summary>
    /// <returns>List of all appointments.</returns>
    public async Task<List<AppointmentResponse>> GetAllAppointmentsAsync()
    {
        var appointments = await repository.GetAllAsync();
        return appointments.Select(a => a.ToResponse()).ToList();
    }

    /// <summary>
    /// Returns a appointment by ID.
    /// </summary>
    /// <param name="id">The ID of the appointment.</param>
    /// <returns>The appointment with the specified ID, or <c>null</c> if not found.</returns>
    public async Task<AppointmentResponse?> GetAppointmentAsync(Guid id)
    {
        var appointment = await repository.GetByIdAsync(id);
        return appointment?.ToResponse();
    }

    /// <summary>
    /// Updates an existing appointment.
    /// </summary>
    /// <param name="id">The ID of the appointment to update.</param>
    /// <param name="request">The updated appointment data.</param>
    /// <returns>The updated appointment, or <c>null</c> if not found.</returns>
    public async Task<AppointmentResponse?> UpdateAppointmentAsync(Guid id, AppointmentRequest request)
    {
        var appointment = await repository.GetByIdAsync(id);

        if (appointment is null)
            return null;

        request.MapTo(appointment);
        var updatedAppointment = await repository.UpdateAsync(appointment);
        return updatedAppointment?.ToResponse();
    }

    /// <summary>
    /// Deletes a appointment by ID.
    /// </summary>
    /// <param name="id">The ID of the appointment to delete.</param>
    /// <returns><c>true</c> if the appointment was deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteAppointmentAsync(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}