namespace Hospital.Contracts;

/// <summary>
/// Response
/// </summary>
public class PatientResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the patient.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Passport number of the patient. Consists of 4 then 6 digits.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the patient in the format "Last Name First Name Middle Name".
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gender of the patient.
    /// </summary>
    public required GenderDto Gender { get; set; }

    /// <summary>
    /// Date of birth of the patient.
    /// </summary>
    public required DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Residential address of the patient.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// The blood group of the patient.
    /// </summary>
    public required BloodGroupDto BloodGroup { get; set; }

    /// <summary>
    ///The Rh factor of the patient's blood.
    /// </summary>
    public required RhFactorDto RhFactor { get; set; }

    /// <summary>
    /// The contact phone number of the patient.
    /// </summary>
    public required string PhoneNumber { get; set; }
}
