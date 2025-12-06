using Hospital.Contracts;
using Hospital.Domain.Models;
namespace Hospital.Application.Mappers;

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
            BloodGroup.AB => BloodGroupDto.AB,
            _ => throw new InvalidOperationException()
        };

    public static GenderDto ToDto(this Gender bloodGroup) =>
        bloodGroup switch
        {
            Gender.Male => GenderDto.Male,
            Gender.Female => GenderDto.Female,
            _ => throw new InvalidOperationException()
        };

    public static RhFactorDto ToDto(this RhFactor bloodGroup) =>
        bloodGroup switch
        {
            RhFactor.Positive => RhFactorDto.Positive,
            RhFactor.Negative => RhFactorDto.Negative,
            _ => throw new InvalidOperationException()
        };

    public static BloodGroup ToDomain(this BloodGroupDto bloodGroup) =>
        bloodGroup switch
        {
            BloodGroupDto.A => BloodGroup.A,
            BloodGroupDto.O => BloodGroup.O,
            BloodGroupDto.B => BloodGroup.B,
            BloodGroupDto.AB => BloodGroup.AB,
            _ => throw new InvalidOperationException()
        };

    public static Gender ToDomain(this GenderDto bloodGroup) =>
        bloodGroup switch
        {
            GenderDto.Male => Gender.Male,
            GenderDto.Female => Gender.Female,
            _ => throw new InvalidOperationException()
        };

    public static RhFactor ToDomain(this RhFactorDto bloodGroup) =>
        bloodGroup switch
        {
            RhFactorDto.Positive => RhFactor.Positive,
            RhFactorDto.Negative => RhFactor.Negative,
            _ => throw new InvalidOperationException()
        };

    /// <summary>
    /// Converts an PatientDto to an Patient domain model.
    /// </summary>
    public static Patient ToDomain(this PatientRequest request) =>
        new()
        {
            PassportNumber = request.PassportNumber,
            FullName = request.FullName,
            Gender = request.Gender.ToDomain(),
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            BloodGroup = request.BloodGroup.ToDomain(),
            RhFactor = request.RhFactor.ToDomain(),
            PhoneNumber = request.PhoneNumber,
            Id = Guid.Empty
        };

    /// <summary>
    /// Converts an Patient to an PatientResponseDto.
    /// </summary>
    public static PatientResponse ToResponse(this Patient entity) =>
        new()
        {
            PassportNumber = entity.PassportNumber,
            FullName = entity.FullName,
            Gender = entity.Gender.ToDto(),
            DateOfBirth = entity.DateOfBirth,
            Address = entity.Address,
            BloodGroup = entity.BloodGroup.ToDto(),
            RhFactor = entity.RhFactor.ToDto(),
            PhoneNumber = entity.PhoneNumber,
            Id = entity.Id
        };

    /// <summary>
    /// Updates an existing Doctor entity with data from DoctorRequest DTO.
    /// </summary>
    /// <param name="request">Source DTO with new values.</param>
    /// <param name="patient">Target entity to update.</param>
    /// <returns>Updated doctor entity.</returns>
    public static Patient MapTo(this PatientRequest request, Patient patient)
    {
        patient.PassportNumber = request.PassportNumber;
        patient.FullName = request.FullName;
        patient.Gender = request.Gender.ToDomain();
        patient.DateOfBirth = request.DateOfBirth;
        patient.Address = request.Address;
        patient.BloodGroup = request.BloodGroup.ToDomain();
        patient.RhFactor = request.RhFactor.ToDomain();
        patient.PhoneNumber = request.PhoneNumber;

        return patient;
    }
}