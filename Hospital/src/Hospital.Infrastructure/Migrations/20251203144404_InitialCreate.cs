using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PassportNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodGroup = table.Column<int>(type: "int", nullable: false),
                    RhFactor = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PassportNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearOfBirth = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppointmentDateTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    RoomNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsFollowUp = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DoctorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "BloodGroup", "DateOfBirth", "FullName", "Gender", "PassportNumber", "PhoneNumber", "RhFactor" },
                values: new object[,]
                {
                    { new Guid("2006ceae-7aa4-4efb-b34c-155b711537f8"), "ул. Советская, д. 7, кв. 3", 3, new DateOnly(1992, 12, 5), "Козлова Анна Викторовна", 1, "2893 456789", "+79279037781", 1 },
                    { new Guid("4ec3b071-eb38-4e0c-a337-afeb0629a51b"), "ул. Мира, д. 12, кв. 9", 0, new DateOnly(1988, 2, 28), "Орлова Екатерина Игоревна", 1, "2230 678901", "+79279037783", 0 },
                    { new Guid("84886119-b39b-4606-bfe3-0fa6985dcaa5"), "ул. Ленина, д. 25, кв. 12", 0, new DateOnly(1980, 5, 17), "Петрова Мария Сергеевна", 1, "1234 234567", "+79279037779", 1 },
                    { new Guid("8b5bed9a-c816-4cb2-934b-d96fd3c8066a"), "ул. Фрунзе, д. 18, кв. 6", 1, new DateOnly(1983, 11, 30), "Романова Ольга Дмитриевна", 1, "6748 890123", "+79279037785", 0 },
                    { new Guid("b25a1128-c1dc-4f14-a3b7-f527868cd100"), "пр. Независимости, д. 45, кв. 22", 2, new DateOnly(1995, 9, 14), "Павлов Сергей Николаевич", 0, "1245 789012", "+79279037784", 1 },
                    { new Guid("b734dc6b-662c-4e70-9e53-9aba02b32e4d"), "ул. Пушкинская, д. 9, кв. 4", 0, new DateOnly(1998, 6, 8), "Тихонова Ирина Васильевna", 1, "4232 012345", "+79279037787", 1 },
                    { new Guid("bdfdd5e0-31e9-4b2d-824e-f92db05e8c72"), "ул. Гагарина, д. 33, кв. 15", 1, new DateOnly(1978, 7, 18), "Николаев Дмитрий Олегович", 0, " 6745 567890", "+79279037782", 0 },
                    { new Guid("e9cbc97c-badf-40f5-963c-d5d814c3534d"), "ул. Кирова, д. 27, кв. 11", 3, new DateOnly(1975, 4, 25), "Семенов Александр Петрович", 0, "1100 901234", "+79279037786", 0 },
                    { new Guid("f27a334d-3821-439e-8784-f37b36268e33"), "пр. Победы, д. 15, кв. 8", 2, new DateOnly(1985, 3, 10), "Сидоров Петр Алексеевич", 0, "4567 345678", "+79279037780", 0 },
                    { new Guid("ffcbcb1d-127d-429f-bc5c-a35ee026c33d"), "ул. Карбышева, д. 81, кв. 46", 1, new DateOnly(2001, 9, 13), "Ряхов Вячеслав Вячеславович", 0, "5321 143649", "+79372165498", 0 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("354be342-95fe-41b0-a378-8bff19227199"), "Хирург" },
                    { new Guid("5ad2ad3a-f8a0-4f15-9444-5c9220f86947"), "Педиатр" },
                    { new Guid("81607380-1e87-40d7-8bb1-bc26b876e11d"), "Стоматолог" },
                    { new Guid("86b74fd3-0836-44cb-8c20-ac42e0b3738c"), "Офтальмолог" },
                    { new Guid("90eb8e02-009f-465e-bbce-5c6d5369a9f9"), "Дерматолог" },
                    { new Guid("b92db95e-74f4-48d7-8952-70926642c9c3"), "Кардиолог" },
                    { new Guid("e9f77cd0-40a8-464f-8e18-350d23b115fd"), "Невролог" },
                    { new Guid("f2a372c5-9d78-4c28-8ec8-b2b03dfefbfa"), "Терапевт" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "ExperienceYears", "FullName", "PassportNumber", "SpecializationId", "YearOfBirth" },
                values: new object[,]
                {
                    { new Guid("00274149-82c2-486d-8d64-80df5e49e091"), 14, "Дмитриева Светлана Олеговна", "6723 100010", new Guid("b92db95e-74f4-48d7-8952-70926642c9c3"), 1983 },
                    { new Guid("377ec49b-732c-47c8-8de6-4727dd65f535"), 7, "Смирнова Татьяна Николаевна", "6711 100006", new Guid("81607380-1e87-40d7-8bb1-bc26b876e11d"), 1990 },
                    { new Guid("4fbe7fd4-f2db-4413-a69f-a50a55dff21e"), 30, "Алексеев Геннадий Степанович", "6011 100009", new Guid("f2a372c5-9d78-4c28-8ec8-b2b03dfefbfa"), 1965 },
                    { new Guid("a300295d-c48b-44f6-872e-b6cf5a4edc7a"), 25, "Васильев Андрей Викторович", "6811 100007", new Guid("5ad2ad3a-f8a0-4f15-9444-5c9220f86947"), 1970 },
                    { new Guid("a7bce7d0-f4a4-4ef4-bb65-377884ac1015"), 18, "Павлова Ольга Сергеевна", "6511 100004", new Guid("e9f77cd0-40a8-464f-8e18-350d23b115fd"), 1978 },
                    { new Guid("bdf704b9-5148-478b-bacd-521426544ff4"), 20, "Сидоров Алексей Петрович", "6211 100001", new Guid("f2a372c5-9d78-4c28-8ec8-b2b03dfefbfa"), 1975 },
                    { new Guid("c52621b2-aa10-4eca-8fbd-7fd7bf51eef5"), 9, "Федорова Наталья Игоревна", "6911 100008", new Guid("90eb8e02-009f-465e-bbce-5c6d5369a9f9"), 1988 },
                    { new Guid("c9dd12bb-e6d5-48f5-9541-56576583eefa"), 15, "Кузнецова Елена Владимировна", "6311 100002", new Guid("b92db95e-74f4-48d7-8952-70926642c9c3"), 1980 },
                    { new Guid("e2dfec7a-776d-431a-867b-979404f052f1"), 12, "Петров Дмитрий Иванович", "6411 100003", new Guid("354be342-95fe-41b0-a378-8bff19227199"), 1982 },
                    { new Guid("f3ca1450-b9ec-4d25-b8ee-d9e2a61e73e6"), 10, "Попов Михаил Александрович", "6611 100005", new Guid("86b74fd3-0836-44cb-8c20-ac42e0b3738c"), 1985 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "IsFollowUp", "PatientId", "RoomNumber" },
                values: new object[,]
                {
                    { new Guid("0b1d9298-0c6f-40c6-8c9e-f42b3147f210"), new DateTimeOffset(new DateTime(2024, 8, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("4fbe7fd4-f2db-4413-a69f-a50a55dff21e"), false, new Guid("b25a1128-c1dc-4f14-a3b7-f527868cd100"), "102" },
                    { new Guid("3520f47d-1dd0-40c7-a4e3-5d63e8dba7f7"), new DateTimeOffset(new DateTime(2025, 8, 27, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("a7bce7d0-f4a4-4ef4-bb65-377884ac1015"), true, new Guid("84886119-b39b-4606-bfe3-0fa6985dcaa5"), "302" },
                    { new Guid("52e16433-6552-428c-a66a-60a147276ac8"), new DateTimeOffset(new DateTime(2024, 3, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("e2dfec7a-776d-431a-867b-979404f052f1"), false, new Guid("f27a334d-3821-439e-8784-f37b36268e33"), "101" },
                    { new Guid("5531f4e3-4086-41ad-a1b3-b7918466b142"), new DateTimeOffset(new DateTime(2025, 9, 18, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00274149-82c2-486d-8d64-80df5e49e091"), true, new Guid("b734dc6b-662c-4e70-9e53-9aba02b32e4d"), "101a" },
                    { new Guid("68f8d574-93ed-4d32-a9d0-3679de9d1b38"), new DateTimeOffset(new DateTime(2024, 3, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bdf704b9-5148-478b-bacd-521426544ff4"), true, new Guid("bdfdd5e0-31e9-4b2d-824e-f92db05e8c72"), "205b" },
                    { new Guid("6f83f992-ed65-4f26-8b2b-891d73e5f5b4"), new DateTimeOffset(new DateTime(2025, 9, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f3ca1450-b9ec-4d25-b8ee-d9e2a61e73e6"), false, new Guid("4ec3b071-eb38-4e0c-a337-afeb0629a51b"), "111" },
                    { new Guid("9bf0a789-7c46-4050-9a13-9445fec63464"), new DateTimeOffset(new DateTime(2024, 2, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c52621b2-aa10-4eca-8fbd-7fd7bf51eef5"), true, new Guid("8b5bed9a-c816-4cb2-934b-d96fd3c8066a"), "217a" },
                    { new Guid("a6fef7d6-d3ba-4ca1-a3d9-55edf6bc754d"), new DateTimeOffset(new DateTime(2024, 3, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bdf704b9-5148-478b-bacd-521426544ff4"), false, new Guid("ffcbcb1d-127d-429f-bc5c-a35ee026c33d"), "101a" },
                    { new Guid("ad221b3e-ec15-4438-bfe3-34639f93436f"), new DateTimeOffset(new DateTime(2025, 9, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("a300295d-c48b-44f6-872e-b6cf5a4edc7a"), true, new Guid("e9cbc97c-badf-40f5-963c-d5d814c3534d"), "402" },
                    { new Guid("c6bef0fe-3894-47a2-9c6c-cdfaddf71174"), new DateTimeOffset(new DateTime(2024, 3, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c9dd12bb-e6d5-48f5-9541-56576583eefa"), true, new Guid("84886119-b39b-4606-bfe3-0fa6985dcaa5"), "205" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
