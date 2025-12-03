using Hospital.Contracts;
using Hospital.Domain.Models;
namespace Hospital.Api.Mappers;

/// <summary>
/// Provides mapping methods for Specalizations.
/// </summary>
public static class SpecalizationMappers
{
    public static Specialization ToDomain(this SpecializationRequest request) =>
        new Specialization
        {
            Id = Guid.Empty,
            Name = request.Name
        };

    public static SpecializationResponse ToResponse(this Specialization entity) =>
        new SpecializationResponse
        {
            Id = entity.Id,
            Name = entity.Name
        };
}
