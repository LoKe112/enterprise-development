using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models;

public class Appointment
{
    /// <summary>
    /// Unique identifier for the appointment.
    /// </summary>
    public required int Id { get; set; }

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
    /// Identifier of the patient associated with this appointmen
    /// </summary>
    public required int PatientId { get; set; }

    /// <summary>
    /// Identifier of the doctor associated with this appointment.
    /// </summary>
    public required int DoctorId { get; set; }
}
