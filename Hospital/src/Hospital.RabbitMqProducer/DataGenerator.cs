using Bogus;
using Bogus.Bson;
using Hospital.Contracts;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;


namespace Hospital.RabbitMqProducer;
internal class DataGenerator(IHttpClientFactory factory)
{
    
    private List<string> _specializatons = new()
    {
        "Terapevt",
        "Lor",
        "Pediatr",
        "Oculist"
    };

    public async Task<List<AppointmentRequest>> GenerateAppointments(int count)
    {
        var http = factory.CreateClient("HospitalApi");

        var doctors = await http.GetFromJsonAsync<List<DoctorResponse>>(
            "/api/doctors", System.Text.Json.JsonSerializerOptions.Web);

        var patients = await http.GetFromJsonAsync<List<PatientResponse>>(
            "/api/patients", System.Text.Json.JsonSerializerOptions.Web);

        Faker<AppointmentRequest> faker = new Faker<AppointmentRequest>()
            .RuleFor(x => x.RoomNumber, f => f.Random.AlphaNumeric(4))
            .RuleFor(x => x.IsFollowUp, f => f.Random.Bool())
            .RuleFor(x => x.AppointmentDateTime, f => f.Date.Recent())
            .RuleFor(x => x.DoctorId, f => f.PickRandom(doctors!.Select(x => x.Id)))
            .RuleFor(x => x.PatientId, f => f.PickRandom(patients!.Select(x => x.Id)));
        return faker.Generate(count);
    }
    public async Task<List<DoctorRequest>> GenerateDoctors(int count)
    {
        var http = factory.CreateClient("HospitalApi");

        var specializations = await http.GetFromJsonAsync<List<SpecializationResponse>>(
            "/api/specializations", System.Text.Json.JsonSerializerOptions.Web);

        Faker<DoctorRequest> faker = new Faker<DoctorRequest>()
            .RuleFor(x => x.YearOfBirth, f => f.Random.Int(1950, 2001))
            .RuleFor(x => x.PassportNumber, f => f.Random.AlphaNumeric(10))
            .RuleFor(x => x.ExperienceYears, f => f.Random.Int(1, 50))
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.SpecializationId, f => f.PickRandom(specializations!.Select(x => x.Id)));
        return faker.Generate(count);
    }
    public Task<List<PatientRequest>> GeneratePatients(int count)
    {
        Faker<PatientRequest> faker = new Faker<PatientRequest>()
            .RuleFor(x => x.DateOfBirth, f => f.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-60)), DateOnly.FromDateTime(DateTime.Now.AddYears(-18))))
            .RuleFor(x => x.RhFactor, f => f.Random.Enum<RhFactorDto>())
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.BloodGroup, f => f.Random.Enum<BloodGroupDto>())
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.Gender, f => f.Random.Enum<GenderDto>())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.PassportNumber, f => f.Random.AlphaNumeric(10));
        return Task.FromResult(faker.Generate(count));
    }
    public Task<List<SpecializationRequest>> GenerateSpecoalizations(int count)
    {
        Faker<SpecializationRequest> faker = new Faker<SpecializationRequest>()
            .RuleFor(x => x.Name, f => f.PickRandom(_specializatons));
        return Task.FromResult(faker.Generate(count));
    }
        


}
