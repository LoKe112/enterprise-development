namespace Hospital.Models;

public class Specialization
{
    /// <summary>
    /// Unique identifier for the medical specialization.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Name of the medical specialization.
    /// </summary>
    public required string Name { get; set; }
}
