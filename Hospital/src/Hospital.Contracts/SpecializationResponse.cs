namespace Hospital.Contracts;

public class SpecializationResponse
{
    /// <summary>
    /// Unique identifier for the medical specialization.
    /// </summary>
    public required Guid Id { get; set; }
    /// <summary>
    /// Name of the medical specialization.
    /// </summary>
    public required string Name { get; set; }
}
