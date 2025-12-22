using Bogus;
using Hospital.Contracts;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hospital.RabbitMqProducer;

internal class DataGenerator(IHttpClientFactory factory)
{
    private static JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = 
        {
            new JsonStringEnumConverter()
        }

    };

    private readonly List<string> _specializatons =
        [
            "Terapevt",
            "Lor",
            "Pediatr",
            "Oculist"
        ];

    public static List<Guid> SpecializationIds { get; } =
[
        Guid.Parse("153922c2-d6b3-45b4-a3d3-a4e8f2b6c36b"),
        Guid.Parse("84300ed6-61d2-4dca-bd2c-cb5231c3580c"),
        Guid.Parse("27bcf4af-b769-4593-98c1-c27a716ee15c"),
        Guid.Parse("a018227a-57bf-4e3e-bea9-24ab89683fa3"),
        Guid.Parse("85c11f07-8f4f-4a32-b9ac-a0a9372feb4f"),
        Guid.Parse("c71552c5-4b28-4f0c-a62c-ac131a519059"),
        Guid.Parse("acef342c-afab-46f1-8405-64a47d52ae94"),
        Guid.Parse("c21f32c0-7e92-4f30-b563-ead686408d99")
];

    public static List<Guid> PatientIds { get; } =
    [
        Guid.Parse("7e6d96bc-ac36-47a2-a09b-f671ed094570"),
        Guid.Parse("7b84c310-4e6f-4369-a41d-f4590e40c596"),
        Guid.Parse("38db15f1-f410-4fdd-8ae4-009aaabcd7d8"),
        Guid.Parse("df83ef59-220f-4650-a068-180fa0d3b081"),
        Guid.Parse("eb7dc1f2-1131-4fef-b36a-183996c4f224"),
        Guid.Parse("2e812a2a-8ccf-451f-bec9-d22abc0ce387"),
        Guid.Parse("47805386-754c-4c1c-b248-000ee803b496"),
        Guid.Parse("81d8168d-01e1-4efe-8177-418648f47c8a"),
        Guid.Parse("65ad0c88-bfc1-454f-9cb0-f70f2fa76edd"),
        Guid.Parse("1773f4a2-6ac9-4b15-9833-8732419eec4b")
    ];

    public static List<Guid> DoctorIds { get; } =
    [
        Guid.Parse("8efc51d1-ad5d-4f9a-a9fa-1e1d67e665e8"),
        Guid.Parse("00e3797d-409e-43ce-8524-3acdaa6ea730"),
        Guid.Parse("0bd859e7-41c5-4872-966a-83d28466d220"),
        Guid.Parse("be04fe70-5e98-49a1-b9e9-73fba7419da0"),
        Guid.Parse("371c0f3f-1bbb-40fa-8eb1-7e755a4ca230"),
        Guid.Parse("5ee38cbd-121b-49c3-8de9-50da2cf8ef89"),
        Guid.Parse("12d89e4f-2cb4-47df-b7dd-e31977805ce6"),
        Guid.Parse("7d9c6f1a-39e4-4834-9d9c-84eb0ee6b51c"),
        Guid.Parse("48800168-8cb8-482b-9b21-13ccc9e9e5b4"),
        Guid.Parse("646de129-7522-4b68-9a49-61ae5eb74b11")
    ];

    public static List<Guid> AppointmentIds { get; } =
    [
        Guid.Parse("b90a5a17-5364-4c3c-8dd5-9b1e0ec4be09"),
        Guid.Parse("9dc011e9-d231-4035-ba24-5a781f1e9849"),
        Guid.Parse("efeee871-68d6-49fe-bf0a-abe54c67b322"),
        Guid.Parse("2e54c00a-6784-4cae-a618-a24141cc997d"),
        Guid.Parse("acdcc23e-a457-4141-b478-f0f04bbcf313"),
        Guid.Parse("75df2b96-c3d7-4279-9325-509fce49bcc1"),
        Guid.Parse("c2dfdda5-3d00-4f70-b732-f2d96bbda44c"),
        Guid.Parse("adc63525-2d03-4825-9e92-c03ccfdc45fc"),
        Guid.Parse("79513998-9403-4899-95c6-8c9822f59e88"),
        Guid.Parse("dda29f9f-2d45-4340-83d5-139e9f03f210")
    ];

    public async Task<List<AppointmentRequest>> GenerateAppointments(int count)
    {
        var http = factory.CreateClient("hospital-api");

        List<Guid> doctorIds;
        try
        {
            var doctors = await http.GetFromJsonAsync<List<DoctorResponse>>(
                "/api/doctors", _jsonSerializerOptions);

            doctorIds = doctors?.Select(x => x.Id).ToList() ?? DoctorIds;
        }
        catch (Exception ex) 
        {
            doctorIds = DoctorIds;
        }

        List<Guid> patientIds;
        try
        {
            var patients = await http.GetFromJsonAsync<List<PatientResponse>>(
                "/api/patients", _jsonSerializerOptions);

            patientIds = patients?.Select(x => x.Id).ToList() ?? PatientIds;
        }
        catch (Exception ex)
        {
            patientIds = PatientIds;
        }

        Faker<AppointmentRequest> faker = new Faker<AppointmentRequest>()
            .RuleFor(x => x.RoomNumber, f => f.Random.AlphaNumeric(4))
            .RuleFor(x => x.IsFollowUp, f => f.Random.Bool())
            .RuleFor(x => x.AppointmentDateTime, f => f.Date.Recent())
            .RuleFor(x => x.DoctorId, f => f.PickRandom(doctorIds))
            .RuleFor(x => x.PatientId, f => f.PickRandom(patientIds));

        return faker.Generate(count);
    }

    public async Task<List<DoctorRequest>> GenerateDoctors(int count)
    {
        var http = factory.CreateClient("hospital-api");

        List<Guid> specializationIds;
        try
        {
            var specializations = await http.GetFromJsonAsync<List<SpecializationResponse>>(
                "/api/specializations", _jsonSerializerOptions);

            specializationIds = specializations?.Select(x => x.Id).ToList() ?? DoctorIds;
        }
        catch
        {
            specializationIds = SpecializationIds;
        }

        

        Faker<DoctorRequest> faker = new Faker<DoctorRequest>()
            .RuleFor(x => x.YearOfBirth, f => f.Random.Int(1950, 2001))
            .RuleFor(x => x.PassportNumber, f => f.Random.AlphaNumeric(10))
            .RuleFor(x => x.ExperienceYears, (f, doctor) =>
            {
                var currentYear = DateTime.UtcNow.Year;
                var age = currentYear - doctor.YearOfBirth;

                const int minStartAge = 24;

                var maxExperience = Math.Max(1, age - minStartAge);

                return f.Random.Int(1, maxExperience);
            })
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.SpecializationId, f => f.PickRandom(specializationIds));
        return faker.Generate(count);
    }
    public Task<List<PatientRequest>> GeneratePatients(int count)
    {
        Faker<PatientRequest> faker = new Faker<PatientRequest>()
            .RuleFor(x => x.DateOfBirth, f => f.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-60)), DateOnly.FromDateTime(DateTime.Now.AddYears(-18))))
            .RuleFor(x => x.RhFactor, f => f.PickRandom<RhFactorDto>())
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.BloodGroup, f => f.PickRandom<BloodGroupDto>())
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.Gender, f => f.PickRandom<GenderDto>())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.PassportNumber, f => f.Random.AlphaNumeric(10));
        return Task.FromResult(faker.Generate(count));
    }
    public Task<List<SpecializationRequest>> GenerateSpecializations(int count)
    {
        Faker<SpecializationRequest> faker = new Faker<SpecializationRequest>()
            .RuleFor(x => x.Name, f => f.PickRandom(_specializatons));
        return Task.FromResult(faker.Generate(count));
    }
}
