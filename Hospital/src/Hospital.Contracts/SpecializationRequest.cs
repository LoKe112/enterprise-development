namespace Hospital.Contracts;

/// <summary>
/// Request
/// </summary>
public class SpecializationRequest
{
    /// <summary>
    /// Name of the medical specialization.
    /// </summary>
    public required string Name { get; set; }
}
