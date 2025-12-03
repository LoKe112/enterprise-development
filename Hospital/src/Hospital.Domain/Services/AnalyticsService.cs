using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Domain.Services.Abstractions;

namespace Hospital.Domain.Services;

public class AnalyticsService(
    IRepository<Doctor> doctorRepository,
    IRepository<Patient> patientRepository,
    IRepository<Appointment> appointmentRepository) : IAnalyticsService
{
    /// <summary>
    /// Returns doctors with 10 or more years of experience.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of doctor IDs with 10 or more years of experience.</returns>
    public async Task<List<Guid>> GetDoctorsWithExperienceAtLeast10Async(CancellationToken cancellationToken = default)
    {
        var doctors = await doctorRepository.GetAllAsync();

        var result = doctors
            .Where(d => d.ExperienceYears >= 10)
            .OrderBy(d => d.Id)
            .Select(d => d.Id)
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns patients of a specific doctor, ordered by name.
    /// </summary>
    /// <param name="doctorId">The ID of the doctor.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of patients assigned to the specified doctor, ordered by surname, name, and patronymic.</returns>
    public async Task<List<Patient>> GetPatientsByDoctorOrderedByFullNameAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        var patients = await patientRepository.GetAllAsync();

        var result = appointments
            .Where(a => a.DoctorId == doctorId)
            .Select(a => patients.First(p => p.Id == a.PatientId))
            .OrderBy(p => p.FullName) 
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns repeated appointments per patient in the last month.
    /// </summary>
    /// <param name="today">The reference date for calculating the last month.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of tuples with patient ID and the count of repeated appointments in the last month.</returns>
    public async Task<List<(Guid PatientId, int Count)>> GetFollowUpAppointmentsCountLastMonthAsync(DateTime today, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync();

        var monthAgo = today.AddMonths(-1);

        var result = appointments
            .Where(a => a.IsFollowUp && a.AppointmentDateTime >= monthAgo && a.AppointmentDateTime <= today)
            .GroupBy(a => a.PatientId)
            .Select(g => (PatientId: g.Key, Count: g.Count()))
            .OrderBy(x => x.PatientId)
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns patients over 30 years old who have appointments with multiple doctors.
    /// </summary>
    /// <param name="today">The reference date for calculating age.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of patient IDs over 30 years old with appointments with multiple doctors.</returns>
    public async Task<List<Guid>> GetPatientsOver30WithMultipleDoctorsOrderedByBirthDateAsync(DateOnly today, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        var patients = await patientRepository.GetAllAsync();

        var ageLimit = today.AddYears(-30);

        var result = appointments
            .Join(patients,
                a => a.PatientId,
                p => p.Id,
                (a, p) => new { Appointment = a, Patient = p })
            .Where(x => x.Patient.DateOfBirth <= ageLimit)
            .GroupBy(x => x.Patient.Id)
            .Where(g => g.Select(x => x.Appointment.DoctorId).Distinct().Count() > 1)
            .Select(g => g.Key)
            .OrderBy(id => patients.First(p => p.Id == id).DateOfBirth)
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns appointments in the current month for a specific cabinet.
    /// </summary>
    /// <param name="officeNumber">The office or cabinet number.</param>
    /// <param name="today">The reference date for the current month.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of appointments in the specified cabinet for the current month.</returns>
    public async Task<List<Appointment>> GetAppointmentsInSelectedRoomThisMonthAsync(string officeNumber, DateTime today, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync();

        var result = appointments
            .Where(a => a.RoomNumber == officeNumber
                        && a.AppointmentDateTime.Year == today.Year
                        && a.AppointmentDateTime.Month == today.Month)
            .OrderBy(a => a.AppointmentDateTime)
            .ToList();

        return result;
    }
}