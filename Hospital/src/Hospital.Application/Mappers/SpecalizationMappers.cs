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

    /// <summary>
    /// Updates an existing Doctor entity with data from DoctorRequest DTO.
    /// </summary>
    /// <param name="request">Source DTO with new values.</param>
    /// <param name="specialization">Target entity to update.</param>
    /// <returns>Updated doctor entity.</returns>
    public static Specialization MapTo(this SpecializationRequest request, Specialization specialization)
    {
        specialization.Name = request.Name;

        return specialization;
    }
}
