namespace Hospital.Domain.Models;

public class Appointment
{
    /// <summary>
    /// Unique identifier for the appointment.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Date and time when the appointment is scheduled.
    /// </summary>
    public required DateTimeOffset AppointmentDateTime { get; set; }

    /// <summary>
    /// Room number where the appointment will take place.
    /// </summary>
    public required string RoomNumber { get; set; }

    /// <summary>
    /// Value indicating whether this is a follow-up appointment.
    /// </summary>
    public required bool IsFollowUp { get; set; }

    /// <summary>
    /// Identifier of the patient associated with this appointment.
    /// </summary>
    public required Guid PatientId { get; set; }

    /// <summary>
    /// Patient associated with this appointment.
    /// </summary>
    public Patient Patient { get; set; } = null!;

    /// <summary>
    /// Identifier of the doctor associated with this appointment.
    /// </summary>
    public required Guid DoctorId { get; set; }

    /// <summary>
    /// Doctor associated with this appointment.
    /// </summary>
    public Doctor Doctor { get; set; } = null!;
}
