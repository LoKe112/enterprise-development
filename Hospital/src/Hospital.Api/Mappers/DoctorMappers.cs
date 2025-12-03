using Hospital.Contracts;
using Hospital.Domain.Models;
namespace Hospital.Api.Mappers;

/// <summary>
/// Provides mapping methods for doctors.
/// </summary>
public static class DoctorsMapper
{
    /// <summary>
    /// Converts an DoctortDto to an Doctor domain model.
    /// </summary>
    public static Doctor ToDomain(this DoctorRequest request) =>
    new Doctor
    {
        PassportNumber = request.PassportNumber,
        FullName = request.FullName,
        YearOfBirth = request.YearOfBirth,       
        SpecializationId = request.SpecializationId,
        ExperienceYears = request.ExperienceYears,
        Id = Guid.Empty
    };

    /// <summary>
    /// Converts an Doctor to an DoctorResponseDto.
    /// </summary>
    public static DoctorResponse ToResponse(this Doctor entity) => 
    new DoctorResponse
    {
        PassportNumber = entity.PassportNumber,
        FullName = entity.FullName,
        YearOfBirth = entity.YearOfBirth,
        Specialization = entity.Specialization.ToResponse(),
        ExperienceYears = entity.ExperienceYears,
        Id = entity.Id
    };
}