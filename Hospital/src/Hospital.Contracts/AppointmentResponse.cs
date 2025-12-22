namespace Hospital.Contracts;

/// <summary>
/// Response
/// </summary>
public class AppointmentResponse
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

    public required PatientResponse? Patient { get; set; }

    public required DoctorResponse? Doctor { get; set; }
    /// <summary>
    /// Identifier of the doctor associated with this appointment.
    /// </summary>
    public required Guid DoctorId { get; set; }
}
