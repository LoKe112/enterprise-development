using Hospital.Domain.Models;
namespace Hospital.Infrastructure;

/// <summary>Seeds test data to hospital models.</summary>
public class DataSeeder
{
    public DataSeeder()
    {
        Specializations =
        [
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Терапевт"
            },
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Хирург"
            },
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Кардиолог"
            },
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Невролог"
            },
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Офтальмолог"
            },
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Стоматолог"
            },
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Педиатр"
            },
            new Specialization
            {
                Id = Guid.NewGuid(),
                Name = "Дерматолог"
            }
        ];

        Patients =
        [
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "5321 143649",
                FullName = "Ряхов Вячеслав Вячеславович",
                Gender = Gender.Male,
                DateOfBirth = new DateOnly(2001, 9, 13),
                Address = "ул. Карбышева, д. 81, кв. 46",
                BloodGroup = BloodGroup.A,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+79372165498"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "1234 234567",
                FullName = "Петрова Мария Сергеевна",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(1980, 5, 17),
                Address = "ул. Ленина, д. 25, кв. 12",
                BloodGroup = BloodGroup.O,
                RhFactor = RhFactor.Negative,
                PhoneNumber = "+79279037779"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "4567 345678",
                FullName = "Сидоров Петр Алексеевич",
                Gender = Gender.Male,
                DateOfBirth = new DateOnly(1985, 3, 10),
                Address = "пр. Победы, д. 15, кв. 8",
                BloodGroup = BloodGroup.B,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+79279037780"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "2893 456789",
                FullName = "Козлова Анна Викторовна",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(1992, 12, 5),
                Address = "ул. Советская, д. 7, кв. 3",
                BloodGroup = BloodGroup.AB,
                RhFactor = RhFactor.Negative,
                PhoneNumber = "+79279037781"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = " 6745 567890",
                FullName = "Николаев Дмитрий Олегович",
                Gender = Gender.Male,
                DateOfBirth = new DateOnly(1978, 7, 18),
                Address = "ул. Гагарина, д. 33, кв. 15",
                BloodGroup = BloodGroup.A,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+79279037782"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "2230 678901",
                FullName = "Орлова Екатерина Игоревна",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(1988, 2, 28),
                Address = "ул. Мира, д. 12, кв. 9",
                BloodGroup = BloodGroup.O,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+79279037783"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "1245 789012",
                FullName = "Павлов Сергей Николаевич",
                Gender = Gender.Male,
                DateOfBirth = new DateOnly(1995, 9, 14),
                Address = "пр. Независимости, д. 45, кв. 22",
                BloodGroup = BloodGroup.B,
                RhFactor = RhFactor.Negative,
                PhoneNumber = "+79279037784"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6748 890123",
                FullName = "Романова Ольга Дмитриевна",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(1983, 11, 30),
                Address = "ул. Фрунзе, д. 18, кв. 6",
                BloodGroup = BloodGroup.A,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+79279037785"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "1100 901234",
                FullName = "Семенов Александр Петрович",
                Gender = Gender.Male,
                DateOfBirth = new DateOnly(1975, 4, 25),
                Address = "ул. Кирова, д. 27, кв. 11",
                BloodGroup = BloodGroup.AB,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+79279037786"
            },
            new Patient
            {
                Id = Guid.NewGuid(),
                PassportNumber = "4232 012345",
                FullName = "Тихонова Ирина Васильевna",
                Gender = Gender.Female,
                DateOfBirth = new DateOnly(1998, 6, 8),
                Address = "ул. Пушкинская, д. 9, кв. 4",
                BloodGroup = BloodGroup.O,
                RhFactor = RhFactor.Negative,
                PhoneNumber = "+79279037787"
            }
        ];

        Doctors =
        [
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6211 100001",
                FullName = "Сидоров Алексей Петрович",
                YearOfBirth = 1975,
                SpecializationId = Specializations[0].Id,
                ExperienceYears = 20
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6311 100002",
                FullName = "Кузнецова Елена Владимировна",
                YearOfBirth = 1980,
                SpecializationId = Specializations[2].Id,
                ExperienceYears = 15
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6411 100003",
                FullName = "Петров Дмитрий Иванович",
                YearOfBirth = 1982,
                SpecializationId = Specializations[1].Id,
                ExperienceYears = 12
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6511 100004",
                FullName = "Павлова Ольга Сергеевна",
                YearOfBirth = 1978,
                SpecializationId = Specializations[3].Id,
                ExperienceYears = 18
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6611 100005",
                FullName = "Попов Михаил Александрович",
                YearOfBirth = 1985,
                SpecializationId = Specializations[4].Id,
                ExperienceYears = 10
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6711 100006",
                FullName = "Смирнова Татьяна Николаевна",
                YearOfBirth = 1990,
                SpecializationId = Specializations[5].Id,
                ExperienceYears = 7
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6811 100007",
                FullName = "Васильев Андрей Викторович",
                YearOfBirth = 1970,
                SpecializationId = Specializations[6].Id,
                ExperienceYears = 25
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6911 100008",
                FullName = "Федорова Наталья Игоревна",
                YearOfBirth = 1988,
                SpecializationId = Specializations[7].Id,
                ExperienceYears = 9
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6011 100009",
                FullName = "Алексеев Геннадий Степанович",
                YearOfBirth = 1965,
                SpecializationId = Specializations[0].Id,
                ExperienceYears = 30
            },
            new Doctor
            {
                Id = Guid.NewGuid(),
                PassportNumber = "6723 100010",
                FullName = "Дмитриева Светлана Олеговна",
                YearOfBirth = 1983,
                SpecializationId = Specializations[2].Id,
                ExperienceYears = 14
            }
        ];

        Appointments =
        [
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[0].Id,
                DoctorId = Doctors[0].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 15, 10, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "101a",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[1].Id,
                DoctorId = Doctors[1].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 16, 14, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "205",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[2].Id,
                DoctorId = Doctors[2].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 17, 9, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "101",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[1].Id,
                DoctorId = Doctors[3].Id,
                AppointmentDateTime = new DateTimeOffset(2025, 8, 27, 11, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "302",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[4].Id,
                DoctorId = Doctors[0].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 16, 14, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "205b",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[5].Id,
                DoctorId = Doctors[4].Id,
                AppointmentDateTime = new DateTimeOffset(2025, 9, 12, 9, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "111",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[8].Id,
                DoctorId = Doctors[6].Id,
                AppointmentDateTime = new DateTimeOffset(2025, 9, 3, 11, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "402",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[7].Id,
                DoctorId = Doctors[7].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 2, 16, 14, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "217a",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[6].Id,
                DoctorId = Doctors[8].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 8, 17, 9, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "102",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Patients[9].Id,
                DoctorId = Doctors[9].Id,
                AppointmentDateTime = new DateTimeOffset(2025, 9, 18, 11, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "101a",
                IsFollowUp = true
            }
        ];
    }

    /// <summary>List of seeded doctors specializations.</summary>
    public List<Specialization> Specializations { get; }

    /// <summary>List of seeded hospital patients.</summary>
    public List<Patient> Patients { get; }

    /// <summary>List of seeded hospital doctors.</summary>
    public List<Doctor> Doctors { get; }

    /// <summary>List of seeded hospital appointments.</summary>
    public List<Appointment> Appointments { get; }
}
