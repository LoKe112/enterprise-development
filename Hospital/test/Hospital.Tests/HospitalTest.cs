using Hospital.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Hospital.Models;
namespace Hospital.Tests;

public class HospitalTests
{
    private readonly HospitalDataSeed _data;

    public HospitalTests()
    {
        _data = new HospitalDataSeed();
    }


    [Fact]
    public void GetDoctorsWithExperienceAtLeast10Years()
    {

        var experiencedDoctors = _data.Doctors
            .Where(d => d.ExperienceYears >= 10)
            .ToList();


        Assert.NotNull(experiencedDoctors);
        Assert.All(experiencedDoctors, d => Assert.True(d.ExperienceYears >= 10));
        Assert.True(experiencedDoctors.Count >= 5);
    }

    [Fact]
    public void GetPatientsByDoctor_OrderedByFullName()
    {

        var targetDoctor = _data.Doctors[0];


        var doctorPatients = (from a in _data.Appointments
                              join p in _data.Patients on a.PatientId equals p.Id
                              where a.DoctorId == targetDoctor.Id
                              orderby p.FullName
                              select p)
                            .Distinct()
                            .ToList();


        Assert.NotNull(doctorPatients);
        Assert.True(doctorPatients.Count >= 1);
        Assert.True(doctorPatients.SequenceEqual(doctorPatients.OrderBy(p => p.FullName)));
    }

    [Fact]
    public void GetFollowUpAppointmentsCountLastMonth()
    {

        var lastMonthStart = DateTime.Now.AddMonths(-1);
        lastMonthStart = new DateTime(lastMonthStart.Year, lastMonthStart.Month, 1);
        var lastMonthEnd = lastMonthStart.AddMonths(1).AddDays(-1);


        var followUpCount = _data.Appointments
            .Count(a => a.IsFollowUp &&
                       a.AppointmentDateTime >= lastMonthStart &&
                       a.AppointmentDateTime <= lastMonthEnd);


        Assert.True(followUpCount >= 0);
    }

    [Fact]
    public void GetPatientsOver30WithMultipleDoctors_OrderedByBirthDate()
    {

        var patientsWithMultipleDoctors = (from a in _data.Appointments
                                           join p in _data.Patients on a.PatientId equals p.Id
                                           where (DateTime.Today.Year - p.DateOfBirth.Year -
                                                 (DateTime.Today.DayOfYear < p.DateOfBirth.DayOfYear ? 1 : 0)) > 30
                                           group a by p into patientGroup
                                           where patientGroup.Select(a => a.DoctorId).Distinct().Count() > 1
                                           orderby patientGroup.Key.DateOfBirth
                                           select patientGroup.Key)
                                         .ToList();

        Assert.NotNull(patientsWithMultipleDoctors);

        Assert.All(patientsWithMultipleDoctors, p =>
        {
            var age = DateTime.Today.Year - p.DateOfBirth.Year -
                     (DateTime.Today.DayOfYear < p.DateOfBirth.DayOfYear ? 1 : 0);
            Assert.True(age > 30);
        });

        foreach (var patient in patientsWithMultipleDoctors)
        {
            var doctorCount = _data.Appointments
                .Where(a => a.PatientId == patient.Id)
                .Select(a => a.DoctorId)
                .Distinct()
                .Count();
            Assert.True(doctorCount > 1);
        }

        Assert.True(patientsWithMultipleDoctors.SequenceEqual(
            patientsWithMultipleDoctors.OrderBy(p => p.DateOfBirth)));
    }

    [Fact]
    public void GetAppointmentsInSelectedRoomThisMonth()
    {            
        const string targetRoom = "101à";
        var currentMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);
       
        var roomAppointments = _data.Appointments
            .Where(a => a.RoomNumber == targetRoom &&
                       a.AppointmentDateTime >= currentMonthStart &&
                       a.AppointmentDateTime <= currentMonthEnd)
            .ToList();
        
        Assert.NotNull(roomAppointments);
        Assert.All(roomAppointments, a =>
        {
            Assert.Equal(targetRoom, a.RoomNumber);
            Assert.True(a.AppointmentDateTime >= currentMonthStart &&
                       a.AppointmentDateTime <= currentMonthEnd);
        });
    }
}