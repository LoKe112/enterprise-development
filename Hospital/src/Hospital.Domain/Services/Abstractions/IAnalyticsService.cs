using Hospital.Domain.Models;

namespace Hospital.Domain.Services.Abstractions;

/// <summary>
/// Interface for the analytics service.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Test that count doctors with at least 10 years of expirience.
    /// </summary>
    public Task<List<Guid>> GetDoctorsWithExperienceAtLeast10Async(CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests retrieving patients for specific doctor ordered by name.
    /// </summary>
    public Task<List<Patient>> GetPatientsByDoctorOrderedByFullNameAsync(Guid doctorId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests counting follow-up appointments in last month.
    /// </summary>
    public Task<List<(Guid PatientId, int Count)>> GetFollowUpAppointmentsCountLastMonthAsync(DateTime today, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests finding patients over 30 with multiple doctors ordered by birth date.
    /// </summary>
    public Task<List<Guid>> GetPatientsOver30WithMultipleDoctorsOrderedByBirthDateAsync(DateOnly today, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tests retrieving appointments in specific room for current month.
    /// </summary>
    public Task<List<Appointment>> GetAppointmentsInSelectedRoomThisMonthAsync(string officeNumber, DateTime today, CancellationToken cancellationToken = default);
}