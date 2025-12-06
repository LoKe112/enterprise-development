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
                    { new Guid("09d0cf7d-84bd-4c9b-8a40-9d7da02a75a8"), "ул. Пушкинская, д. 9, кв. 4", 0, new DateOnly(1998, 6, 8), "Тихонова Ирина Васильевna", 1, "4232 012345", "+79279037787", 1 },
                    { new Guid("2db0cc68-8de3-4340-b824-6149e9845e89"), "ул. Мира, д. 12, кв. 9", 0, new DateOnly(1988, 2, 28), "Орлова Екатерина Игоревна", 1, "2230 678901", "+79279037783", 0 },
                    { new Guid("701c8638-70ff-475a-97d8-c7014916e158"), "ул. Ленина, д. 25, кв. 12", 0, new DateOnly(1980, 5, 17), "Петрова Мария Сергеевна", 1, "1234 234567", "+79279037779", 1 },
                    { new Guid("9838ccd2-7a54-4bd2-9eba-2891913accd9"), "ул. Карбышева, д. 81, кв. 46", 1, new DateOnly(2001, 9, 13), "Ряхов Вячеслав Вячеславович", 0, "5321 143649", "+79372165498", 0 },
                    { new Guid("99b30229-b463-464f-88ea-0107fc41b0a0"), "ул. Кирова, д. 27, кв. 11", 3, new DateOnly(1975, 4, 25), "Семенов Александр Петрович", 0, "1100 901234", "+79279037786", 0 },
                    { new Guid("b10a50d2-4cb6-42c1-83da-a6c7117aeeac"), "ул. Гагарина, д. 33, кв. 15", 1, new DateOnly(1978, 7, 18), "Николаев Дмитрий Олегович", 0, " 6745 567890", "+79279037782", 0 },
                    { new Guid("b524fb6a-0b69-4b3b-b9eb-6a0e6c4a56ad"), "ул. Фрунзе, д. 18, кв. 6", 1, new DateOnly(1983, 11, 30), "Романова Ольга Дмитриевна", 1, "6748 890123", "+79279037785", 0 },
                    { new Guid("d165e993-705a-44ef-b340-df1dcc4bae09"), "пр. Независимости, д. 45, кв. 22", 2, new DateOnly(1995, 9, 14), "Павлов Сергей Николаевич", 0, "1245 789012", "+79279037784", 1 },
                    { new Guid("efe1f4d8-4c12-4b50-b82e-9b3a4878657d"), "пр. Победы, д. 15, кв. 8", 2, new DateOnly(1985, 3, 10), "Сидоров Петр Алексеевич", 0, "4567 345678", "+79279037780", 0 },
                    { new Guid("f3c54e7b-e4c3-48b0-ae13-8d74b6c89f2e"), "ул. Советская, д. 7, кв. 3", 3, new DateOnly(1992, 12, 5), "Козлова Анна Викторовна", 1, "2893 456789", "+79279037781", 1 }
                });

            migrationBuilder.InsertData(
                table: "specializations",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("2ddda41b-811b-4207-8d62-7bd6f380302e"), "Терапевт" },
                    { new Guid("8f100466-4664-4124-8ef9-0268f6d20718"), "Педиатр" },
                    { new Guid("9526c9b9-9bc2-4ba2-8c5d-d5f37f4a9ddc"), "Хирург" },
                    { new Guid("a3d48bd8-dc4a-4b05-b3dc-57bde5ba751c"), "Дерматолог" },
                    { new Guid("b2e11556-ddd6-4f73-a282-83e04cae99b6"), "Офтальмолог" },
                    { new Guid("d893ad2c-ae54-4eaa-8092-36bf3c0aa882"), "Кардиолог" },
                    { new Guid("d93d55bd-6914-4b1f-94fd-774f974dd3f3"), "Стоматолог" },
                    { new Guid("f5bd5316-537e-48d2-86e2-ccb75e907d4c"), "Невролог" }
                });

            migrationBuilder.InsertData(
                table: "doctors",
                columns: new[] { "id", "experience_years", "full_name", "passport_number", "specialization_id", "year_of_birth" },
                values: new object[,]
                {
                    { new Guid("1e598b02-c3be-4572-aaaf-2afa1853619e"), 9, "Федорова Наталья Игоревна", "6911 100008", new Guid("a3d48bd8-dc4a-4b05-b3dc-57bde5ba751c"), 1988 },
                    { new Guid("4809a06b-78a1-4bac-b4a3-4b69a814c4c1"), 14, "Дмитриева Светлана Олеговна", "6723 100010", new Guid("d893ad2c-ae54-4eaa-8092-36bf3c0aa882"), 1983 },
                    { new Guid("510c69f9-31a1-430d-85ef-a4967de4ce49"), 15, "Кузнецова Елена Владимировна", "6311 100002", new Guid("d893ad2c-ae54-4eaa-8092-36bf3c0aa882"), 1980 },
                    { new Guid("625e23fc-240a-4db3-99c5-32e4e2e17676"), 10, "Попов Михаил Александрович", "6611 100005", new Guid("b2e11556-ddd6-4f73-a282-83e04cae99b6"), 1985 },
                    { new Guid("63477dbe-81bf-4eef-ada4-1c698440548d"), 12, "Петров Дмитрий Иванович", "6411 100003", new Guid("9526c9b9-9bc2-4ba2-8c5d-d5f37f4a9ddc"), 1982 },
                    { new Guid("63d138c0-74c7-4fee-85c1-447e592b5ed0"), 18, "Павлова Ольга Сергеевна", "6511 100004", new Guid("f5bd5316-537e-48d2-86e2-ccb75e907d4c"), 1978 },
                    { new Guid("83a896b2-a6c4-4468-84fc-26399a3db1ff"), 7, "Смирнова Татьяна Николаевна", "6711 100006", new Guid("d93d55bd-6914-4b1f-94fd-774f974dd3f3"), 1990 },
                    { new Guid("bd99c5f1-3697-4e0d-bd22-af813a063a5e"), 30, "Алексеев Геннадий Степанович", "6011 100009", new Guid("2ddda41b-811b-4207-8d62-7bd6f380302e"), 1965 },
                    { new Guid("d0e1cb98-d7dd-49c5-8605-ff68176c3160"), 20, "Сидоров Алексей Петрович", "6211 100001", new Guid("2ddda41b-811b-4207-8d62-7bd6f380302e"), 1975 },
                    { new Guid("d2ad7cab-939e-41c1-9b98-b1a9d1c1e67a"), 25, "Васильев Андрей Викторович", "6811 100007", new Guid("8f100466-4664-4124-8ef9-0268f6d20718"), 1970 }
                });

            migrationBuilder.InsertData(
                table: "appointments",
                columns: new[] { "id", "appointment_date_time", "doctor_id", "is_follow_up", "patient_id", "room_number" },
                values: new object[,]
                {
                    { new Guid("09a636f2-fff7-459d-8cf4-dd3430d5f342"), new DateTimeOffset(new DateTime(2024, 3, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d0e1cb98-d7dd-49c5-8605-ff68176c3160"), true, new Guid("b10a50d2-4cb6-42c1-83da-a6c7117aeeac"), "205b" },
                    { new Guid("4f2c1a01-f573-40a7-a06b-3e394be9ca0f"), new DateTimeOffset(new DateTime(2024, 3, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("63477dbe-81bf-4eef-ada4-1c698440548d"), false, new Guid("efe1f4d8-4c12-4b50-b82e-9b3a4878657d"), "101" },
                    { new Guid("633f8f1d-cdc0-425e-8780-4807970946f5"), new DateTimeOffset(new DateTime(2024, 3, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("510c69f9-31a1-430d-85ef-a4967de4ce49"), true, new Guid("701c8638-70ff-475a-97d8-c7014916e158"), "205" },
                    { new Guid("77a922cc-4719-4e4e-91af-7dc2d8c8694f"), new DateTimeOffset(new DateTime(2025, 9, 18, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("4809a06b-78a1-4bac-b4a3-4b69a814c4c1"), true, new Guid("09d0cf7d-84bd-4c9b-8a40-9d7da02a75a8"), "101a" },
                    { new Guid("82324979-ff64-45af-bf79-8f628ada5fc9"), new DateTimeOffset(new DateTime(2025, 8, 27, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("63d138c0-74c7-4fee-85c1-447e592b5ed0"), true, new Guid("701c8638-70ff-475a-97d8-c7014916e158"), "302" },
                    { new Guid("a50dd755-4af8-4a1d-a5b6-b8ead24a892e"), new DateTimeOffset(new DateTime(2025, 9, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("625e23fc-240a-4db3-99c5-32e4e2e17676"), false, new Guid("2db0cc68-8de3-4340-b824-6149e9845e89"), "111" },
                    { new Guid("a8495792-da0a-459a-9c0e-7369522e53ad"), new DateTimeOffset(new DateTime(2025, 9, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d2ad7cab-939e-41c1-9b98-b1a9d1c1e67a"), true, new Guid("99b30229-b463-464f-88ea-0107fc41b0a0"), "402" },
                    { new Guid("e750e7f5-620c-46bd-bf9a-a450c1f9856e"), new DateTimeOffset(new DateTime(2024, 3, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d0e1cb98-d7dd-49c5-8605-ff68176c3160"), false, new Guid("9838ccd2-7a54-4bd2-9eba-2891913accd9"), "101a" },
                    { new Guid("f288fb39-6460-48ac-a4d6-f27cd687e862"), new DateTimeOffset(new DateTime(2024, 2, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("1e598b02-c3be-4572-aaaf-2afa1853619e"), true, new Guid("b524fb6a-0b69-4b3b-b9eb-6a0e6c4a56ad"), "217a" },
                    { new Guid("f6d62c69-2f26-486d-8189-f38b4552e683"), new DateTimeOffset(new DateTime(2024, 8, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bd99c5f1-3697-4e0d-bd22-af813a063a5e"), false, new Guid("d165e993-705a-44ef-b340-df1dcc4bae09"), "102" }
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
