using Hospital.Contracts;
using Hospital.Domain.Models;

namespace Hospital.Application.Mappers;

/// <summary>
/// Provides mapping methods for Appointments.
/// </summary>
public static class AppointmentMapper
{
    /// <summary>
    /// Converts an AppointmentDto to an Appointment domain model.
    /// </summary>
    public static Appointment ToDomain(this AppointmentRequest request) =>
    new()
    {
        AppointmentDateTime = request.AppointmentDateTime,
        RoomNumber = request.RoomNumber,
        IsFollowUp = request.IsFollowUp,
        PatientId = request.PatientId,
        DoctorId = request.DoctorId,
        Id = Guid.Empty,        
    };

    /// <summary>
    /// Converts an Appointment to anAppointmentResponseDto.
    /// </summary>
    public static AppointmentResponse ToResponse(this Appointment entity) =>
    new()
    {
        AppointmentDateTime = entity.AppointmentDateTime,
        RoomNumber = entity.RoomNumber,
        IsFollowUp = entity.IsFollowUp,
        PatientId = entity.PatientId,
        DoctorId = entity.DoctorId,
        Id = entity.Id,
        Patient = entity.Patient?.ToResponse(),
        Doctor = entity.Doctor?.ToResponse()
    };

    /// <summary>
    /// Updates an existing Doctor entity with data from DoctorRequest DTO.
    /// </summary>
    /// <param name="request">Source DTO with new values.</param>
    /// <param name="appointment">Target entity to update.</param>
    /// <returns>Updated doctor entity.</returns>
    public static Appointment MapTo(this AppointmentRequest request, Appointment appointment)
    {
        appointment.AppointmentDateTime = request.AppointmentDateTime;
        appointment.RoomNumber = request.RoomNumber;
        appointment.IsFollowUp = request.IsFollowUp;
        appointment.PatientId = request.PatientId;
        appointment.DoctorId = request.DoctorId;

        return appointment;
    }
}