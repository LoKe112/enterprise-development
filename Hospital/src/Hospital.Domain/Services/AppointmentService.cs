using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Domain.Services.Abstractions;

namespace Hospital.Domain.Services;

/// <summary>
/// Service for managing appointments.
/// </summary>
public class AppointmentService(IRepository<Appointment> repository) : IAppointmentService
{
    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    /// <param name="appointment">The appointment to create.</param>
    /// <returns>The ID of the created appointment.</returns>
    public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
    {
        return await repository.CreateAsync(appointment);
    }

    /// <summary>
    /// Returns all appointments.
    /// </summary>
    /// <returns>List of all appointments.</returns>
    public async Task<List<Appointment>> GetAllAppointmentsAsync() =>
        await repository.GetAllAsync();

    /// <summary>
    /// Returns a appointment by ID.
    /// </summary>
    /// <param name="id">The ID of the appointment.</param>
    /// <returns>The appointment with the specified ID, or <c>null</c> if not found.</returns>
    public async Task<Appointment?> GetAppointmentAsync(Guid id) =>
        await repository.GetByIdAsync(id);

    /// <summary>
    /// Updates an existing appointment.
    /// </summary>
    /// <param name="id">The ID of the appointment to update.</param>
    /// <param name="appointment">The updated appointment data.</param>
    /// <returns>The updated appointment, or <c>null</c> if not found.</returns>
    public async Task<Appointment?> UpdateAppointmentAsync(Guid id, Appointment appointment)
    {
        appointment.Id = id;
        return await repository.UpdateAsync(appointment);
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