namespace Hospital.Domain.Models;

public class Doctor
{
    /// <summary>
    /// Unique identifier for the doctor.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Passport number of the doctor. Consists of 4 then 6 digits.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the doctor in the format "Last Name First Name Middle Name".
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Birth year of the doctor.
    /// </summary>
    public required int YearOfBirth { get; set; }

    /// <summary>
    /// Id of the specialization of the doctor.
    /// </summary>
    public required Guid SpecializationId { get; set; }

    /// <summary>
    /// Medical specialization of the doctor.
    /// </summary>
    public Specialization? Specialization { get; set; }

    /// <summary>
    /// Number of years of professional experience of the doctor.
    /// </summary>
    public required int ExperienceYears { get; set; }
}
