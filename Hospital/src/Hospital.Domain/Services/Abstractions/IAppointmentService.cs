using Hospital.Domain.Models;

namespace Hospital.Domain.Services.Abstractions;

/// <summary>
/// Interface for the appointment service.
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    /// <param name="appointment">The appointment to create.</param>
    /// <returns>The Id of the created appointment.</returns>
    public Task<Appointment> CreateAppointmentAsync(Appointment appointment);

    /// <summary>
    /// Deletes a appointment by Id.
    /// </summary>
    /// <param name="guid">The Id of the appointment to delete.</param>
    /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeleteAppointmentAsync(Guid guid);

    /// <summary>
    /// Returns a appointment by Id.
    /// </summary>
    /// <param name="guid">The Id of the appointment.</param>
    /// <returns>The appointment with the specified Id, or <c>null</c> if not found.</returns>
    public Task<Appointment?> GetAppointmentAsync(Guid guid);

    /// <summary>
    /// Returns all appointments.
    /// </summary>
    /// <returns>A list of all appointments.</returns>
    public Task<List<Appointment>> GetAllAppointmentsAsync();

    /// <summary>
    /// Updates a appointment.
    /// </summary>
    /// <param name="guid">The Id of the appointment to update.</param>
    /// <param name="appointment">The appointment data.</param>
    /// <returns>The updated appointment, or <c>null</c> if not found.</returns>
    public Task<Appointment?> UpdateAppointmentAsync(Guid guid, Appointment appointment);
}