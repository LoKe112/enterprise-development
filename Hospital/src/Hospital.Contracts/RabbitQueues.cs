namespace Hospital.Contracts;

/// <summary>
/// Contains RabbitMQ queue names used by the consumer.
/// </summary>
public static class RabbitQueues
{
    public const string Specializations = "specializations.create";
    public const string Doctors = "doctors.create";
    public const string Patients = "patients.create";
    public const string Appointments = "appointments.create";
}