using Hospital.Domain.Models;
using Hospital.Domain.Repositories.Abstractions;
using Hospital.Contracts;
using Hospital.Application.Services.Abstractions;
using Hospital.Application.Mappers;

namespace Hospital.Application.Services;

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
    public async Task<List<DoctorResponse>> GetDoctorsWithExperienceAtLeast10Async(CancellationToken cancellationToken = default)
    {
        var doctors = await doctorRepository.GetAllAsync();

        var result = doctors
            .Where(d => d.ExperienceYears >= 10)
            .OrderBy(d => d.Id)
            .Select(d => d.ToResponse())
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns patients of a specific doctor, ordered by name.
    /// </summary>
    /// <param name="doctorId">The ID of the doctor.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of patients assigned to the specified doctor, ordered by surname, name, and patronymic.</returns>
    public async Task<List<PatientResponse>> GetPatientsByDoctorOrderedByFullNameAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        var patients = await patientRepository.GetAllAsync();

        var result = appointments
            .Where(a => a.DoctorId == doctorId)
            .Select(a => patients.First(p => p.Id == a.PatientId))
            .OrderBy(p => p.FullName)
            .Select(p => p.ToResponse()) 
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns repeated appointments per patient in the last month.
    /// </summary>
    /// <param name="today">The reference date for calculating the last month.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of tuples with patient ID and the count of repeated appointments in the last month.</returns>
    public async Task<List<(PatientResponse Patient, int Count)>> GetFollowUpAppointmentsCountLastMonthAsync(CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        var patients = await patientRepository.GetAllAsync();

        var today = DateTimeOffset.Now;
        var monthAgo = today.AddMonths(-1);

        var patientAppointments = appointments
            .Where(a => a.IsFollowUp && a.AppointmentDateTime >= monthAgo && a.AppointmentDateTime <= today)
            .GroupBy(a => a.PatientId)
            .Select(g => new { PatientId = g.Key, Count = g.Count() })
            .ToDictionary(x => x.PatientId, x => x.Count);

        var result = patients
            .Where(p => patientAppointments.ContainsKey(p.Id))
            .OrderBy(p => p.Id)
            .Select(p => (Patient: p.ToResponse(), Count: patientAppointments[p.Id]))
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns patients over 30 years old who have appointments with multiple doctors.
    /// </summary>
    /// <param name="today">The reference date for calculating age.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>List of patient IDs over 30 years old with appointments with multiple doctors.</returns>
    public async Task<List<PatientResponse>> GetPatientsOver30WithMultipleDoctorsOrderedByBirthDateAsync(CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync();
        var patients = await patientRepository.GetAllAsync();

        var today = DateOnly.FromDateTime(DateTime.Now);
        var ageLimit = today.AddYears(-30);

        // Получаем ID пациентов, которые подходят под условия
        var patientIds = appointments
            .Join(patients,
                a => a.PatientId,
                p => p.Id,
                (a, p) => new { Appointment = a, Patient = p })
            .Where(x => x.Patient.DateOfBirth <= ageLimit)
            .GroupBy(x => x.Patient.Id)
            .Where(g => g.Select(x => x.Appointment.DoctorId).Distinct().Count() > 1)
            .Select(g => g.Key)
            .ToList();

        // Получаем пациентов по ID и маппим в DTO
        var result = patients
            .Where(p => patientIds.Contains(p.Id))
            .OrderBy(p => p.DateOfBirth)
            .Select(p => p.ToResponse()) // Используем существующий маппер
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
    public async Task<List<AppointmentResponse>> GetAppointmentsInSelectedRoomThisMonthAsync(string officeNumber, CancellationToken cancellationToken = default)
    {
        var today = DateTimeOffset.Now;
        var appointments = await appointmentRepository.GetAllAsync();

        var result = appointments
            .Where(a => a.RoomNumber == officeNumber
                        && a.AppointmentDateTime.Year == today.Year
                        && a.AppointmentDateTime.Month == today.Month)
            .OrderBy(a => a.AppointmentDateTime)
            .Select(a => a.ToResponse()) // Используем существующий маппер
            .ToList();

        return result;
    }
}