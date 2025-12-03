using Hospital.Contracts;
using Hospital.Domain.Models;
namespace Hospital.Api.Mappers;

/// <summary>
/// Provides mapping methods for doctors.
/// </summary>
public static class PatientsMapper
{
    public static BloodGroupDto ToDto(this BloodGroup bloodGroup) =>
        bloodGroup switch
        {
            BloodGroup.A => BloodGroupDto.A,
            BloodGroup.O => BloodGroupDto.O,
            BloodGroup.B => BloodGroupDto.B,
            BloodGroup.AB => BloodGroupDto.AB
        };

    public static BloodGroupDto ToResponse(this BloodGroup bloodGroup) =>
        bloodGroup switch
        {
            BloodGroup.A => BloodGroupDto.A,
            BloodGroup.O => BloodGroupDto.O,
            BloodGroup.B => BloodGroupDto.B,
            BloodGroup.AB => BloodGroupDto.AB
        };



    /// <summary>
    /// Converts an DoctortDto to an Doctor domain model.
    /// </summary>
    public static Patient ToDomain(this PatientRequest request) =>
        new Patient
        {
            PassportNumber = request.PassportNumber,
            FullName = request.FullName,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            BloodGroup = request.BloodGroup,
            RhFactor = request.RhFactor,
            PhoneNumber = request.PhoneNumber,
            Id = Guid.Empty
        };

    /// <summary>
    /// Converts an Doctor to an DoctorResponseDto.
    /// </summary>
    public static DoctorResponse ToResponse(this Patient entity) =>
        new PatientResponse
        {
            PassportNumber = entity.PassportNumber,
            FullName = entity.FullName,
            Gender = entity.Gender,
            DateOfBirth = entity.DateOfBirth,
            Address = entity.Address,
            BloodGroup = entity.BloodGroup,
            RhFactor = entity.RhFactor,
            PhoneNumber = entity.PhoneNumber,
            Id = entity.Id
        };
}