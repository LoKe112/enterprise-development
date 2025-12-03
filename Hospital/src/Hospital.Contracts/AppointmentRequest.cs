namespace Hospital.Contracts;

public class AppointmentRequest
{
    /// <summary>
    /// Date and time when the appointment is scheduled.
    /// </summary>
    public required DateTime AppointmentDateTime { get; set; }

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
    /// Identifier of the doctor associated with this appointment.
    /// </summary>
    public required Guid DoctorId { get; set; }
}
