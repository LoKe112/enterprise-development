using Hospital.Contracts;
using Hospital.Domain.Models;
namespace Hospital.Application.Mappers;

/// <summary>
/// Provides mapping methods for Specalizations.
/// </summary>
public static class SpecalizationMappers
{
    /// <summary>
    /// Converts an SpecializationDto to an Specialization domain model.
    /// </summary>
    public static Specialization ToDomain(this SpecializationRequest request) =>
        new()
        {
            Id = Guid.Empty,
            Name = request.Name
        };
    /// <summary>
    /// Converts an Specialization to an SpecializationResponseDto.
    /// </summary>
    public static SpecializationResponse ToResponse(this Specialization entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    public static Specialization MapTo(this SpecializationRequest request, Specialization specialization)
    {
        specialization.Name = request.Name;

        return specialization;
    }
}
