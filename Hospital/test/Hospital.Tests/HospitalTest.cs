namespace Hospital.Tests;

/// <summary>
/// Unit tests for Hospital.Models.
/// </summary>
public class HospitalTests(HospitalDataSeed seed) : IClassFixture<HospitalDataSeed>
{
    /// <summary>
    /// Test that count doctors with at least 10 years of expirience.
    /// </summary>
    [Fact]
    public void GetDoctorsWithExperienceAtLeast10Years()
    {
        var expectedCount = 8;

        var experiencedDoctors = seed.Doctors
            .Where(d => d.ExperienceYears >= 10)
            .ToList();

        Assert.NotNull(experiencedDoctors);
        Assert.All(experiencedDoctors, d => Assert.True(d.ExperienceYears >= 10));
        Assert.Equal(expectedCount, experiencedDoctors.Count);
    }

    /// <summary>
    /// Tests retrieving patients for specific doctor ordered by name.
    /// </summary>
    [Fact]
    public void GetPatientsByDoctorOrderedByFullName()
    {
        var targetDoctor = seed.Doctors[0];
        var expectedPatientCount = 1;

        var doctorPatients = (from a in seed.Appointments
                              join p in seed.Patients on a.PatientId equals p.Id
                              where a.DoctorId == targetDoctor.Id
                              orderby p.FullName
                              select p)
                            .Distinct()
                            .ToList();

        Assert.NotNull(doctorPatients);
        Assert.True(doctorPatients.Count >= expectedPatientCount);
        Assert.True(doctorPatients.SequenceEqual(doctorPatients.OrderBy(p => p.FullName)));
    }

    /// <summary>
    /// Tests counting follow-up appointments in last month.
    /// </summary>
    [Fact]
    public void GetFollowUpAppointmentsCountLastMonth()
    {        
        var currentDate = new DateTime(2025, 9, 22); 
        var lastMonthStart = currentDate.AddMonths(-1);
        var lastMonthEnd = currentDate;
        var expectedCount = 3; 

        var followUpCount = seed.Appointments
            .Count(a => a.IsFollowUp &&
                       a.AppointmentDateTime >= lastMonthStart &&
                       a.AppointmentDateTime <= lastMonthEnd);

        Assert.Equal(expectedCount, followUpCount);
    }

    /// <summary>
    /// Tests finding patients over 30 with multiple doctors ordered by birth date.
    /// </summary>
    [Fact]
    public void GetPatientsOver30WithMultipleDoctorsOrderedByBirthDate()
    {
        var today = new DateOnly(2025, 9, 22);
        var expectedCount = 1;

        var patientsWithMultipleDoctors = (from a in seed.Appointments
                                           join p in seed.Patients on a.PatientId equals p.Id
                                           where p.DateOfBirth <= today.AddYears(-31) 
                                           group a by p into patientGroup
                                           where patientGroup.Select(a => a.DoctorId).Distinct().Count() > 1
                                           orderby patientGroup.Key.DateOfBirth
                                           select patientGroup.Key)
                                         .ToList();

        Assert.NotNull(patientsWithMultipleDoctors);
        Assert.Equal(expectedCount, patientsWithMultipleDoctors.Count);

        Assert.True(patientsWithMultipleDoctors.SequenceEqual(
            patientsWithMultipleDoctors.OrderBy(p => p.DateOfBirth)));

    }

    /// <summary>
    /// Tests retrieving appointments in specific room for current month.
    /// </summary>
    [Fact]
    public void GetAppointmentsInSelectedRoomThisMonth()
    {
        const string targetRoom = "101a";
        var currentDate = new DateTime(2025, 9, 15);
        var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1);
        var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);
        var expectedCount = 1;

        var roomAppointments = seed.Appointments
            .Where(a => a.RoomNumber == targetRoom &&
                       a.AppointmentDateTime >= currentMonthStart &&
                       a.AppointmentDateTime <= currentMonthEnd)
            .ToList();

        Assert.NotNull(roomAppointments);
        Assert.Equal(expectedCount, roomAppointments.Count);
    }
}