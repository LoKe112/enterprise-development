using Hospital.Contracts;
using Hospital.Domain.Models;
namespace Hospital.Application.Mappers;

/// <summary>
/// Provides mapping methods for Patient.
/// </summary>
public static class DoctorsMapper
{
    /// <summary>
    /// Converts an DoctortDto to an Doctor domain model.
    /// </summary>
    public static Doctor ToDomain(this DoctorRequest request) =>
    new()
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
    new()
    {
        SpecializationId= entity.SpecializationId,
        PassportNumber = entity.PassportNumber,
        FullName = entity.FullName,
        YearOfBirth = entity.YearOfBirth,
        Specialization = entity.Specialization!.ToResponse(),
        ExperienceYears = entity.ExperienceYears,
        Id = entity.Id
    };

    /// <summary>
    /// Updates an existing Doctor entity with data from DoctorRequest DTO.
    /// </summary>
    /// <param name="request">Source DTO with new values.</param>
    /// <param name="doctor">Target entity to update.</param>
    /// <returns>Updated doctor entity.</returns>
    public static Doctor MapTo(this DoctorRequest request, Doctor doctor)
    {
        doctor.FullName = request.FullName;
        doctor.PassportNumber = request.PassportNumber;
        doctor.YearOfBirth = request.YearOfBirth;
        doctor.ExperienceYears = request.ExperienceYears;
        doctor.SpecializationId = request.SpecializationId;

        return doctor;
    }
}