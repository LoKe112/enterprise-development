using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Models;

namespace Hospital.Tests; 
public class HospitalDataSeed {
public List<Specialization> Specializations { get; }
public List<Patient> Patients { get; }
public List<Doctor> Doctors { get; }
public List<Appointment> Appointments { get; }

public HospitalDataSeed() {
  Specializations = InitSpecializations();
  Patients = InitPatients();
  Doctors = InitDoctors(Specializations);
  Appointments = InitAppointments(Patients, Doctors);
}

private List<Specialization> InitSpecializations() =>
[new Specialization { Id = 1, Name = "Терапевт" },
 new Specialization { Id = 2, Name = "Хирург" },
 new Specialization { Id = 3, Name = "Кардиолог" },
 new Specialization { Id = 4, Name = "Невролог" },
 new Specialization { Id = 5, Name = "Офтальмолог" },
 new Specialization { Id = 6, Name = "Стоматолог" },
 new Specialization { Id = 7, Name = "Педиатр" },
 new Specialization { Id = 8, Name = "Дерматолог" }];

private List<Patient> InitPatients() =>
[new Patient { Id = 1, PassportNumber = "143649",
               FullName = "Ряхов Вячеслав Вячеславович", Gender = Gender.Male,
               DateOfBirth = new DateTime(1980, 5, 15),
               Address = "ул. Карбышева, д. 81, кв. 46",
               BloodGroup = BloodGroup.A, RhFactor = RhFactor.Positive,
               PhoneNumber = "+79372165498" },

 new Patient { Id = 2, PassportNumber = "234567",
               FullName = "Петрова Мария Сергеевна", Gender = Gender.Female,
               DateOfBirth = new DateTime(1990, 8, 22),
               Address = "ул. Ленина, д. 25, кв. 12",
               BloodGroup = BloodGroup.O, RhFactor = RhFactor.Negative,
               PhoneNumber = "+79279037779" },

 new Patient { Id = 3, PassportNumber = "345678",
               FullName = "Сидоров Петр Алексеевич", Gender = Gender.Male,
               DateOfBirth = new DateTime(1985, 3, 10),
               Address = "пр. Победы, д. 15, кв. 8",
               BloodGroup = BloodGroup.B, RhFactor = RhFactor.Positive,
               PhoneNumber = "+79279037780" },

 new Patient { Id = 4, PassportNumber = "456789",
               FullName = "Козлова Анна Викторовна", Gender = Gender.Female,
               DateOfBirth = new DateTime(1992, 12, 5),
               Address = "ул. Советская, д. 7, кв. 3",
               BloodGroup = BloodGroup.AB, RhFactor = RhFactor.Negative,
               PhoneNumber = "+79279037781" },

 new Patient { Id = 5, PassportNumber = "567890",
               FullName = "Николаев Дмитрий Олегович", Gender = Gender.Male,
               DateOfBirth = new DateTime(1978, 7, 18),
               Address = "ул. Гагарина, д. 33, кв. 15",
               BloodGroup = BloodGroup.A, RhFactor = RhFactor.Positive,
               PhoneNumber = "+79279037782" },

 new Patient { Id = 6, PassportNumber = "678901",
               FullName = "Орлова Екатерина Игоревна",
               Gender = Gender.Female,
               DateOfBirth = new DateTime(1988, 2, 28),
               Address = "ул. Мира, д. 12, кв. 9",
               BloodGroup = BloodGroup.O, RhFactor = RhFactor.Positive,
               PhoneNumber = "+79279037783" },

 new Patient { Id = 7, PassportNumber = "789012",
               FullName = "Павлов Сергей Николаевич", Gender = Gender.Male,
               DateOfBirth = new DateTime(1995, 9, 14),
               Address = "пр. Независимости, д. 45, кв. 22",
               BloodGroup = BloodGroup.B, RhFactor = RhFactor.Negative,
               PhoneNumber = "+79279037784" },

 new Patient { Id = 8, PassportNumber = "890123",
               FullName = "Романова Ольга Дмитриевна",
               Gender = Gender.Female,
               DateOfBirth = new DateTime(1983, 11, 30),
               Address = "ул. Фрунзе, д. 18, кв. 6",
               BloodGroup = BloodGroup.A, RhFactor = RhFactor.Positive,
               PhoneNumber = "+79279037785" },

 new Patient { Id = 9, PassportNumber = "901234",
               FullName = "Семенов Александр Петрович",
               Gender = Gender.Male,
               DateOfBirth = new DateTime(1975, 4, 25),
               Address = "ул. Кирова, д. 27, кв. 11",
               BloodGroup = BloodGroup.AB, RhFactor = RhFactor.Positive,
               PhoneNumber = "+79279037786" },

 new Patient { Id = 10, PassportNumber = "012345",
               FullName = "Тихонова Ирина Васильевna",
               Gender = Gender.Female,
               DateOfBirth = new DateTime(1998, 6, 8),
               Address = "ул. Пушкинская, д. 9, кв. 4",
               BloodGroup = BloodGroup.O, RhFactor = RhFactor.Negative,
               PhoneNumber = "+79279037787" }];

private List<Doctor> InitDoctors(List<Specialization> specializations) =>
[new Doctor { Id = 1, PassportNumber = "100001",
              FullName = "Сидоров Алексей Петрович", YearOfBirth = 1975,
              Specialization = specializations[0], ExperienceYears = 20 },

 new Doctor { Id = 2, PassportNumber = "100002",
              FullName = "Кузнецова Елена Владимировна", YearOfBirth = 1980,
              Specialization = specializations[2], ExperienceYears = 15 },

 new Doctor { Id = 3, PassportNumber = "100003",
              FullName = "Петров Дмитрий Иванович", YearOfBirth = 1982,
              Specialization = specializations[1], ExperienceYears = 12 },

 new Doctor { Id = 4, PassportNumber = "100004",
              FullName = "Павлова Ольга Сергеевна", YearOfBirth = 1978,
              Specialization = specializations[3], ExperienceYears = 18 },

 new Doctor { Id = 5, PassportNumber = "100005",
              FullName = "Попов Михаил Александрович", YearOfBirth = 1985,
              Specialization = specializations[4], ExperienceYears = 10 },

 new Doctor { Id = 6, PassportNumber = "100006",
              FullName = "Смирнова Татьяна Николаевна", YearOfBirth = 1990,
              Specialization = specializations[5], ExperienceYears = 7 },

 new Doctor { Id = 7, PassportNumber = "100007",
              FullName = "Васильев Андрей Викторович", YearOfBirth = 1970,
              Specialization = specializations[6], ExperienceYears = 25 },

 new Doctor { Id = 8, PassportNumber = "100008",
              FullName = "Федорова Наталья Игоревна", YearOfBirth = 1988,
              Specialization = specializations[7], ExperienceYears = 9 },

 new Doctor { Id = 9, PassportNumber = "100009",
              FullName = "Алексеев Геннадий Степанович", YearOfBirth = 1965,
              Specialization = specializations[0], ExperienceYears = 30 },

 new Doctor { Id = 10, PassportNumber = "100010",
              FullName = "Дмитриева Светлана Олеговна", YearOfBirth = 1983,
              Specialization = specializations[2], ExperienceYears = 14 }];

private List<Appointment> InitAppointments(List<Patient> patients,
                                           List<Doctor> doctors) =>
[new Appointment { Id = 1, PatientId = 1, DoctorId = 1,
                   AppointmentDateTime = new DateTime(2024, 3, 15, 10, 0,
                                                      0),
                   RoomNumber = "101a", IsFollowUp = false },

 new Appointment { Id = 2, PatientId = 2, DoctorId = 2,
                   AppointmentDateTime = new DateTime(2024, 3, 16, 14, 30,
                                                      0),
                   RoomNumber = "205", IsFollowUp = true },

 new Appointment { Id = 3, PatientId = 3, DoctorId = 3,
                   AppointmentDateTime = new DateTime(2024, 3, 17, 9, 0, 0),
                   RoomNumber = "101", IsFollowUp = false },

 new Appointment { Id = 4, PatientId = 1, DoctorId = 4,
                   AppointmentDateTime = new DateTime(2024, 3, 18, 11, 30,
                                                      0),
                   RoomNumber = "302", IsFollowUp = true },
 new Appointment { Id = 5, PatientId = 5, DoctorId = 6,
                   AppointmentDateTime = new DateTime(2024, 3, 16, 14, 30,
                                                      0),
                   RoomNumber = "205b", IsFollowUp = true },

 new Appointment { Id = 6, PatientId = 6, DoctorId = 5,
                   AppointmentDateTime = new DateTime(2024, 4, 17, 9, 0, 0),
                   RoomNumber = "111", IsFollowUp = false },

 new Appointment { Id = 7, PatientId = 9, DoctorId = 7,
                   AppointmentDateTime = new DateTime(2024, 5, 18, 11, 30,
                                                      0),
                   RoomNumber = "402", IsFollowUp = true },

 new Appointment { Id = 8, PatientId = 8, DoctorId = 8,
                   AppointmentDateTime = new DateTime(2024, 2, 16, 14, 30,
                                                      0),
                   RoomNumber = "217a", IsFollowUp = true },

 new Appointment { Id = 9, PatientId = 7, DoctorId = 9,
                   AppointmentDateTime = new DateTime(2024, 8, 17, 9, 0, 0),
                   RoomNumber = "102", IsFollowUp = false },

 new Appointment { Id = 10, PatientId = 10, DoctorId = 10,
                   AppointmentDateTime = new DateTime(2025, 9, 18, 11, 30,
                                                      0),
                   RoomNumber = "302", IsFollowUp = true },
];
}
