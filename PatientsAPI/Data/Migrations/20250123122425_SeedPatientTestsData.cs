using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientsAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedPatientTestsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("6ba6c168-d041-440f-b304-3e7b7cf2d544"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("81043f86-28ca-4914-9fc2-a850c58b3fa2"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("95ff5c1b-7972-40b4-87f4-a045f79348a9"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("caff4464-8882-494a-910c-4b2b8388b253"));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Adresse", "DateDeNaissance", "GenreId", "Nom", "NumeroTelephone", "Prenom" },
                values: new object[,]
                {
                    { new Guid("157d6514-89de-431b-924b-08dd2e6bc8f4"), "2 High St", new DateTime(1945, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "TestBorderline", "200-333-4444", "Test" },
                    { new Guid("415b6bbb-bd43-4d04-924c-08dd2e6bc8f4"), "3 Club Road", new DateTime(2004, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "TestInDanger", "300-444-5555", "Test" },
                    { new Guid("94a687bd-5ad7-4596-924d-08dd2e6bc8f4"), "4 Valley Dr", new DateTime(2002, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TestEarlyOnset", "400-555-6666", "Test" },
                    { new Guid("ccc9e063-c800-43d5-924a-08dd2e6bc8f4"), "1 Brookside St", new DateTime(1966, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TestNone", "100-222-3333", "Test" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("157d6514-89de-431b-924b-08dd2e6bc8f4"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("415b6bbb-bd43-4d04-924c-08dd2e6bc8f4"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("94a687bd-5ad7-4596-924d-08dd2e6bc8f4"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: new Guid("ccc9e063-c800-43d5-924a-08dd2e6bc8f4"));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Adresse", "DateDeNaissance", "GenreId", "Nom", "NumeroTelephone", "Prenom" },
                values: new object[,]
                {
                    { new Guid("6ba6c168-d041-440f-b304-3e7b7cf2d544"), "4 Valley Dr", new DateTime(2002, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TestEarlyOnset", "400-555-6666", "Test" },
                    { new Guid("81043f86-28ca-4914-9fc2-a850c58b3fa2"), "2 High St", new DateTime(1945, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "TestBorderline", "200-333-4444", "Test" },
                    { new Guid("95ff5c1b-7972-40b4-87f4-a045f79348a9"), "1 Brookside St", new DateTime(1966, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "TestNone", "100-222-3333", "Test" },
                    { new Guid("caff4464-8882-494a-910c-4b2b8388b253"), "3 Club Road", new DateTime(2004, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "TestInDanger", "300-444-5555", "Test" }
                });
        }
    }
}
