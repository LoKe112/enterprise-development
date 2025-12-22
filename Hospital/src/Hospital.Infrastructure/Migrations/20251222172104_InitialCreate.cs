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
                name: "patients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    passport_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    full_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<int>(type: "int", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    blood_group = table.Column<int>(type: "int", nullable: false),
                    rh_factor = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_patients", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "specializations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specializations", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    passport_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    full_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    year_of_birth = table.Column<int>(type: "int", nullable: false),
                    specialization_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    experience_years = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_doctors", x => x.id);
                    table.ForeignKey(
                        name: "fk_doctors_specializations_specialization_id",
                        column: x => x.specialization_id,
                        principalTable: "specializations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    appointment_date_time = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    room_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_follow_up = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    patient_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    doctor_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appointments", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointments_doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_appointments_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "patients",
                columns: new[] { "id", "address", "blood_group", "date_of_birth", "full_name", "gender", "passport_number", "phone_number", "rh_factor" },
                values: new object[,]
                {
                    { new Guid("1773f4a2-6ac9-4b15-9833-8732419eec4b"), "ул. Пушкинская, д. 9, кв. 4", 0, new DateOnly(1998, 6, 8), "Тихонова Ирина Васильевna", 1, "4232 012345", "+79279037787", 1 },
                    { new Guid("2e812a2a-8ccf-451f-bec9-d22abc0ce387"), "ул. Мира, д. 12, кв. 9", 0, new DateOnly(1988, 2, 28), "Орлова Екатерина Игоревна", 1, "2230 678901", "+79279037783", 0 },
                    { new Guid("38db15f1-f410-4fdd-8ae4-009aaabcd7d8"), "пр. Победы, д. 15, кв. 8", 2, new DateOnly(1985, 3, 10), "Сидоров Петр Алексеевич", 0, "4567 345678", "+79279037780", 0 },
                    { new Guid("47805386-754c-4c1c-b248-000ee803b496"), "пр. Независимости, д. 45, кв. 22", 2, new DateOnly(1995, 9, 14), "Павлов Сергей Николаевич", 0, "1245 789012", "+79279037784", 1 },
                    { new Guid("65ad0c88-bfc1-454f-9cb0-f70f2fa76edd"), "ул. Кирова, д. 27, кв. 11", 3, new DateOnly(1975, 4, 25), "Семенов Александр Петрович", 0, "1100 901234", "+79279037786", 0 },
                    { new Guid("7b84c310-4e6f-4369-a41d-f4590e40c596"), "ул. Ленина, д. 25, кв. 12", 0, new DateOnly(1980, 5, 17), "Петрова Мария Сергеевна", 1, "1234 234567", "+79279037779", 1 },
                    { new Guid("7e6d96bc-ac36-47a2-a09b-f671ed094570"), "ул. Карбышева, д. 81, кв. 46", 1, new DateOnly(2001, 9, 13), "Ряхов Вячеслав Вячеславович", 0, "5321 143649", "+79372165498", 0 },
                    { new Guid("81d8168d-01e1-4efe-8177-418648f47c8a"), "ул. Фрунзе, д. 18, кв. 6", 1, new DateOnly(1983, 11, 30), "Романова Ольга Дмитриевна", 1, "6748 890123", "+79279037785", 0 },
                    { new Guid("df83ef59-220f-4650-a068-180fa0d3b081"), "ул. Советская, д. 7, кв. 3", 3, new DateOnly(1992, 12, 5), "Козлова Анна Викторовна", 1, "2893 456789", "+79279037781", 1 },
                    { new Guid("eb7dc1f2-1131-4fef-b36a-183996c4f224"), "ул. Гагарина, д. 33, кв. 15", 1, new DateOnly(1978, 7, 18), "Николаев Дмитрий Олегович", 0, " 6745 567890", "+79279037782", 0 }
                });

            migrationBuilder.InsertData(
                table: "specializations",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("153922c2-d6b3-45b4-a3d3-a4e8f2b6c36b"), "Терапевт" },
                    { new Guid("27bcf4af-b769-4593-98c1-c27a716ee15c"), "Кардиолог" },
                    { new Guid("84300ed6-61d2-4dca-bd2c-cb5231c3580c"), "Хирург" },
                    { new Guid("85c11f07-8f4f-4a32-b9ac-a0a9372feb4f"), "Офтальмолог" },
                    { new Guid("a018227a-57bf-4e3e-bea9-24ab89683fa3"), "Невролог" },
                    { new Guid("acef342c-afab-46f1-8405-64a47d52ae94"), "Педиатр" },
                    { new Guid("c21f32c0-7e92-4f30-b563-ead686408d99"), "Дерматолог" },
                    { new Guid("c71552c5-4b28-4f0c-a62c-ac131a519059"), "Стоматолог" }
                });

            migrationBuilder.InsertData(
                table: "doctors",
                columns: new[] { "id", "experience_years", "full_name", "passport_number", "specialization_id", "year_of_birth" },
                values: new object[,]
                {
                    { new Guid("00e3797d-409e-43ce-8524-3acdaa6ea730"), 15, "Кузнецова Елена Владимировна", "6311 100002", new Guid("27bcf4af-b769-4593-98c1-c27a716ee15c"), 1980 },
                    { new Guid("0bd859e7-41c5-4872-966a-83d28466d220"), 12, "Петров Дмитрий Иванович", "6411 100003", new Guid("84300ed6-61d2-4dca-bd2c-cb5231c3580c"), 1982 },
                    { new Guid("12d89e4f-2cb4-47df-b7dd-e31977805ce6"), 25, "Васильев Андрей Викторович", "6811 100007", new Guid("acef342c-afab-46f1-8405-64a47d52ae94"), 1970 },
                    { new Guid("371c0f3f-1bbb-40fa-8eb1-7e755a4ca230"), 10, "Попов Михаил Александрович", "6611 100005", new Guid("85c11f07-8f4f-4a32-b9ac-a0a9372feb4f"), 1985 },
                    { new Guid("48800168-8cb8-482b-9b21-13ccc9e9e5b4"), 30, "Алексеев Геннадий Степанович", "6011 100009", new Guid("153922c2-d6b3-45b4-a3d3-a4e8f2b6c36b"), 1965 },
                    { new Guid("5ee38cbd-121b-49c3-8de9-50da2cf8ef89"), 7, "Смирнова Татьяна Николаевна", "6711 100006", new Guid("c71552c5-4b28-4f0c-a62c-ac131a519059"), 1990 },
                    { new Guid("646de129-7522-4b68-9a49-61ae5eb74b11"), 14, "Дмитриева Светлана Олеговна", "6723 100010", new Guid("27bcf4af-b769-4593-98c1-c27a716ee15c"), 1983 },
                    { new Guid("7d9c6f1a-39e4-4834-9d9c-84eb0ee6b51c"), 9, "Федорова Наталья Игоревна", "6911 100008", new Guid("c21f32c0-7e92-4f30-b563-ead686408d99"), 1988 },
                    { new Guid("8efc51d1-ad5d-4f9a-a9fa-1e1d67e665e8"), 20, "Сидоров Алексей Петрович", "6211 100001", new Guid("153922c2-d6b3-45b4-a3d3-a4e8f2b6c36b"), 1975 },
                    { new Guid("be04fe70-5e98-49a1-b9e9-73fba7419da0"), 18, "Павлова Ольга Сергеевна", "6511 100004", new Guid("a018227a-57bf-4e3e-bea9-24ab89683fa3"), 1978 }
                });

            migrationBuilder.InsertData(
                table: "appointments",
                columns: new[] { "id", "appointment_date_time", "doctor_id", "is_follow_up", "patient_id", "room_number" },
                values: new object[,]
                {
                    { new Guid("2e54c00a-6784-4cae-a618-a24141cc997d"), new DateTimeOffset(new DateTime(2025, 8, 27, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("be04fe70-5e98-49a1-b9e9-73fba7419da0"), true, new Guid("7b84c310-4e6f-4369-a41d-f4590e40c596"), "302" },
                    { new Guid("75df2b96-c3d7-4279-9325-509fce49bcc1"), new DateTimeOffset(new DateTime(2025, 9, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("371c0f3f-1bbb-40fa-8eb1-7e755a4ca230"), false, new Guid("2e812a2a-8ccf-451f-bec9-d22abc0ce387"), "111" },
                    { new Guid("79513998-9403-4899-95c6-8c9822f59e88"), new DateTimeOffset(new DateTime(2024, 8, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("48800168-8cb8-482b-9b21-13ccc9e9e5b4"), false, new Guid("47805386-754c-4c1c-b248-000ee803b496"), "102" },
                    { new Guid("9dc011e9-d231-4035-ba24-5a781f1e9849"), new DateTimeOffset(new DateTime(2024, 3, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00e3797d-409e-43ce-8524-3acdaa6ea730"), true, new Guid("7b84c310-4e6f-4369-a41d-f4590e40c596"), "205" },
                    { new Guid("acdcc23e-a457-4141-b478-f0f04bbcf313"), new DateTimeOffset(new DateTime(2024, 3, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8efc51d1-ad5d-4f9a-a9fa-1e1d67e665e8"), true, new Guid("eb7dc1f2-1131-4fef-b36a-183996c4f224"), "205b" },
                    { new Guid("adc63525-2d03-4825-9e92-c03ccfdc45fc"), new DateTimeOffset(new DateTime(2024, 2, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("7d9c6f1a-39e4-4834-9d9c-84eb0ee6b51c"), true, new Guid("81d8168d-01e1-4efe-8177-418648f47c8a"), "217a" },
                    { new Guid("b90a5a17-5364-4c3c-8dd5-9b1e0ec4be09"), new DateTimeOffset(new DateTime(2024, 3, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8efc51d1-ad5d-4f9a-a9fa-1e1d67e665e8"), false, new Guid("7e6d96bc-ac36-47a2-a09b-f671ed094570"), "101a" },
                    { new Guid("c2dfdda5-3d00-4f70-b732-f2d96bbda44c"), new DateTimeOffset(new DateTime(2025, 9, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("12d89e4f-2cb4-47df-b7dd-e31977805ce6"), true, new Guid("65ad0c88-bfc1-454f-9cb0-f70f2fa76edd"), "402" },
                    { new Guid("dda29f9f-2d45-4340-83d5-139e9f03f210"), new DateTimeOffset(new DateTime(2025, 9, 18, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("646de129-7522-4b68-9a49-61ae5eb74b11"), true, new Guid("1773f4a2-6ac9-4b15-9833-8732419eec4b"), "101a" },
                    { new Guid("efeee871-68d6-49fe-bf0a-abe54c67b322"), new DateTimeOffset(new DateTime(2024, 3, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("0bd859e7-41c5-4872-966a-83d28466d220"), false, new Guid("38db15f1-f410-4fdd-8ae4-009aaabcd7d8"), "101" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_appointments_doctor_id",
                table: "appointments",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_patient_id",
                table: "appointments",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_doctors_specialization_id",
                table: "doctors",
                column: "specialization_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "specializations");
        }
    }
}
