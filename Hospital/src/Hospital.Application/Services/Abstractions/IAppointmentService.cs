using Hospital.Contracts;

namespace Hospital.Application.Services.Abstractions;

public interface IAppointmentService
{
    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    /// <param name="request">The appointment data to create.</param>
    /// <returns>The created appointment.</returns>
    public Task<AppointmentResponse> CreateAppointmentAsync(AppointmentRequest request);

    /// <summary>
    /// Deletes an appointment by Id.
    /// </summary>
    /// <param name="id">The Id of the appointment to delete.</param>
    /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeleteAppointmentAsync(Guid id);

    /// <summary>
    /// Returns an appointment by Id.
    /// </summary>
    /// <param name="id">The Id of the appointment.</param>
    /// <returns>The appointment with the specified Id, or <c>null</c> if not found.</returns>
    public Task<AppointmentResponse?> GetAppointmentAsync(Guid id);

    /// <summary>
    /// Returns all appointments.
    /// </summary>
    /// <returns>A list of all appointments.</returns>
    public Task<List<AppointmentResponse>> GetAllAppointmentsAsync();

    /// <summary>
    /// Updates an appointment.
    /// </summary>
    /// <param name="id">The Id of the appointment to update.</param>
    /// <param name="request">The updated appointment data.</param>
    /// <returns>The updated appointment, or <c>null</c> if not found.</returns>
    public Task<AppointmentResponse?> UpdateAppointmentAsync(Guid id, AppointmentRequest request);

}