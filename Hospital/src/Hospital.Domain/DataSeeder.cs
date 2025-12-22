using Hospital.Domain.Models;

namespace Hospital.Domain;

/// <summary>Seeds test data to hospital models.</summary>
public class DataSeeder
{
    public DataSeeder()
    {
        Specializations =
        [
            new Specialization
            {
                Id = Guid.Parse("153922c2-d6b3-45b4-a3d3-a4e8f2b6c36b"),
                Name = "Терапевт"
            },
            new Specialization
            {
                Id = Guid.Parse("84300ed6-61d2-4dca-bd2c-cb5231c3580c"),
                Name = "Хирург"
            },
            new Specialization
            {
                Id = Guid.Parse("27bcf4af-b769-4593-98c1-c27a716ee15c"),
                Name = "Кардиолог"
            },
            new Specialization
            {
                Id = Guid.Parse("a018227a-57bf-4e3e-bea9-24ab89683fa3"),
                Name = "Невролог"
            },
            new Specialization
            {
                Id = Guid.Parse("85c11f07-8f4f-4a32-b9ac-a0a9372feb4f"),
                Name = "Офтальмолог"
            },
            new Specialization
            {
                Id = Guid.Parse("c71552c5-4b28-4f0c-a62c-ac131a519059"),
                Name = "Стоматолог"
            },
            new Specialization
            {
                Id = Guid.Parse("acef342c-afab-46f1-8405-64a47d52ae94"),
                Name = "Педиатр"
            },
            new Specialization
            {
                Id = Guid.Parse("c21f32c0-7e92-4f30-b563-ead686408d99"),
                Name = "Дерматолог"
            }
        ];

        Patients =
        [
            new Patient
            {
                Id = Guid.Parse("7e6d96bc-ac36-47a2-a09b-f671ed094570"),
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
                Id = Guid.Parse("7b84c310-4e6f-4369-a41d-f4590e40c596"),
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
                Id = Guid.Parse("38db15f1-f410-4fdd-8ae4-009aaabcd7d8"),
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
                Id = Guid.Parse("df83ef59-220f-4650-a068-180fa0d3b081"),
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
                Id = Guid.Parse("eb7dc1f2-1131-4fef-b36a-183996c4f224"),
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
                Id = Guid.Parse("2e812a2a-8ccf-451f-bec9-d22abc0ce387"),
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
                Id = Guid.Parse("47805386-754c-4c1c-b248-000ee803b496"),
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
                Id = Guid.Parse("81d8168d-01e1-4efe-8177-418648f47c8a"),
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
                Id = Guid.Parse("65ad0c88-bfc1-454f-9cb0-f70f2fa76edd"),
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
                Id = Guid.Parse("1773f4a2-6ac9-4b15-9833-8732419eec4b"),
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
                Id = Guid.Parse("8efc51d1-ad5d-4f9a-a9fa-1e1d67e665e8"),
                PassportNumber = "6211 100001",
                FullName = "Сидоров Алексей Петрович",
                YearOfBirth = 1975,
                SpecializationId = Specializations[0].Id,
                ExperienceYears = 20
            },
            new Doctor
            {
                Id = Guid.Parse("00e3797d-409e-43ce-8524-3acdaa6ea730"),
                PassportNumber = "6311 100002",
                FullName = "Кузнецова Елена Владимировна",
                YearOfBirth = 1980,
                SpecializationId = Specializations[2].Id,
                ExperienceYears = 15
            },
            new Doctor
            {
                Id = Guid.Parse("0bd859e7-41c5-4872-966a-83d28466d220"),
                PassportNumber = "6411 100003",
                FullName = "Петров Дмитрий Иванович",
                YearOfBirth = 1982,
                SpecializationId = Specializations[1].Id,
                ExperienceYears = 12
            },
            new Doctor
            {
                Id = Guid.Parse("be04fe70-5e98-49a1-b9e9-73fba7419da0"),
                PassportNumber = "6511 100004",
                FullName = "Павлова Ольга Сергеевна",
                YearOfBirth = 1978,
                SpecializationId = Specializations[3].Id,
                ExperienceYears = 18
            },
            new Doctor
            {
                Id = Guid.Parse("371c0f3f-1bbb-40fa-8eb1-7e755a4ca230"),
                PassportNumber = "6611 100005",
                FullName = "Попов Михаил Александрович",
                YearOfBirth = 1985,
                SpecializationId = Specializations[4].Id,
                ExperienceYears = 10
            },
            new Doctor
            {
                Id = Guid.Parse("5ee38cbd-121b-49c3-8de9-50da2cf8ef89"),
                PassportNumber = "6711 100006",
                FullName = "Смирнова Татьяна Николаевна",
                YearOfBirth = 1990,
                SpecializationId = Specializations[5].Id,
                ExperienceYears = 7
            },
            new Doctor
            {
                Id = Guid.Parse("12d89e4f-2cb4-47df-b7dd-e31977805ce6"),
                PassportNumber = "6811 100007",
                FullName = "Васильев Андрей Викторович",
                YearOfBirth = 1970,
                SpecializationId = Specializations[6].Id,
                ExperienceYears = 25
            },
            new Doctor
            {
                Id = Guid.Parse("7d9c6f1a-39e4-4834-9d9c-84eb0ee6b51c"),
                PassportNumber = "6911 100008",
                FullName = "Федорова Наталья Игоревна",
                YearOfBirth = 1988,
                SpecializationId = Specializations[7].Id,
                ExperienceYears = 9
            },
            new Doctor
            {
                Id = Guid.Parse("48800168-8cb8-482b-9b21-13ccc9e9e5b4"),
                PassportNumber = "6011 100009",
                FullName = "Алексеев Геннадий Степанович",
                YearOfBirth = 1965,
                SpecializationId = Specializations[0].Id,
                ExperienceYears = 30
            },
            new Doctor
            {
                Id = Guid.Parse("646de129-7522-4b68-9a49-61ae5eb74b11"),
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
                Id = Guid.Parse("b90a5a17-5364-4c3c-8dd5-9b1e0ec4be09"),
                PatientId = Patients[0].Id,
                DoctorId = Doctors[0].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 15, 10, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "101a",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.Parse("9dc011e9-d231-4035-ba24-5a781f1e9849"),
                PatientId = Patients[1].Id,
                DoctorId = Doctors[1].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 16, 14, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "205",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.Parse("efeee871-68d6-49fe-bf0a-abe54c67b322"),
                PatientId = Patients[2].Id,
                DoctorId = Doctors[2].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 17, 9, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "101",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.Parse("2e54c00a-6784-4cae-a618-a24141cc997d"),
                PatientId = Patients[1].Id,
                DoctorId = Doctors[3].Id,
                AppointmentDateTime = new DateTimeOffset(2025, 8, 27, 11, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "302",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.Parse("acdcc23e-a457-4141-b478-f0f04bbcf313"),
                PatientId = Patients[4].Id,
                DoctorId = Doctors[0].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 3, 16, 14, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "205b",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.Parse("75df2b96-c3d7-4279-9325-509fce49bcc1"),
                PatientId = Patients[5].Id,
                DoctorId = Doctors[4].Id,
                AppointmentDateTime = new DateTimeOffset(2025, 9, 12, 9, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "111",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.Parse("c2dfdda5-3d00-4f70-b732-f2d96bbda44c"),
                PatientId = Patients[8].Id,
                DoctorId = Doctors[6].Id,
                AppointmentDateTime = new DateTimeOffset(2025, 9, 3, 11, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "402",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.Parse("adc63525-2d03-4825-9e92-c03ccfdc45fc"),
                PatientId = Patients[7].Id,
                DoctorId = Doctors[7].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 2, 16, 14, 30,
                    0, TimeSpan.Zero),
                RoomNumber = "217a",
                IsFollowUp = true
            },
            new Appointment
            {
                Id = Guid.Parse("79513998-9403-4899-95c6-8c9822f59e88"),
                PatientId = Patients[6].Id,
                DoctorId = Doctors[8].Id,
                AppointmentDateTime = new DateTimeOffset(2024, 8, 17, 9, 0,
                    0, TimeSpan.Zero),
                RoomNumber = "102",
                IsFollowUp = false
            },
            new Appointment
            {
                Id = Guid.Parse("dda29f9f-2d45-4340-83d5-139e9f03f210"),
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
