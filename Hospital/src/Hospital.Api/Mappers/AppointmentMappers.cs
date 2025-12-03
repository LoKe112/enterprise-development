using Hospital.Contracts;
using Hospital.Domain.Models;

namespace Hospital.Api.Mappers;

/// <summary>
/// Provides mapping methods for Appointments.
/// </summary>
public static class AppointmentMapper
{
    /// <summary>
    /// Converts an AppointmentDto to an Appointment domain model.
    /// </summary>
    public static Appointment ToDomain(this AppointmentRequest request) =>
    new Appointment
    {
        AppointmentDateTime = request.AppointmentDateTime,
        RoomNumber = request.RoomNumber,
        IsFollowUp = request.IsFollowUp,
        PatientId = request.PatientId,
        DoctorId = request.DoctorId,
        Id = Guid.Empty
    };

    /// <summary>
    /// Converts an Appointment to anAppointmentResponseDto.
    /// </summary>
    public static AppointmentResponse ToResponse(this Appointment entity) =>
    new AppointmentResponse
    {
        AppointmentDateTime = entity.AppointmentDateTime,
        RoomNumber = entity.RoomNumber,
        IsFollowUp = entity.IsFollowUp,
        PatientId = entity.PatientId,
        DoctorId = entity.DoctorId,
        Id = entity.Id
    };
}