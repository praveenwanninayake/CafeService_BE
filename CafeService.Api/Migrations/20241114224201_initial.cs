using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeService.Api.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "M_Cafe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Cafe", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "M_Employee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FK_CafeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_M_Employee_M_Cafe_FK_CafeId",
                        column: x => x.FK_CafeId,
                        principalTable: "M_Cafe",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "M_Cafe",
                columns: new[] { "Id", "CreatedDateTime", "Description", "Location", "Logo", "ModifiedDateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("17464bda-0763-468c-9574-4acb5e652c06"), new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(7668), "A cozy place to enjoy your coffee.", "New York, NY", null, new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(7669), "Central Perk" },
                    { new Guid("606b33b9-0db6-4ed8-9b68-39ccf3d059f9"), new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(7685), "European style coffee house.", "London, UK", null, new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(7686), "Cafe Nero" },
                    { new Guid("ff742216-126b-48e7-a054-d31955b88f0a"), new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(7678), "Specialty coffee roaster and retailer.", "San Francisco, CA", null, new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(7679), "Blue Bottle Coffee" }
                });

            migrationBuilder.InsertData(
                table: "M_Employee",
                columns: new[] { "Id", "CreatedDateTime", "EmailAddress", "FK_CafeId", "Gender", "ModifiedDateTime", "Name", "PhoneNumber", "StartDate" },
                values: new object[,]
                {
                    { "UIA123456", new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8046), "alice.johnson@example.com", null, 2, new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8047), "Alice Johnson", "91234567", null },
                    { "UIB987654", new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8053), "bob.smith@example.com", null, 1, new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8054), "Bob Smith", "81234567", null },
                    { "UIC765432", new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8059), "charlie.brown@example.com", null, 1, new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8060), "Charlie Brown", "92345678", null },
                    { "UID456789", new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8064), "diana.prince@example.com", null, 2, new DateTime(2024, 11, 14, 22, 42, 0, 113, DateTimeKind.Utc).AddTicks(8065), "Diana Prince", "83456789", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_Employee_FK_CafeId",
                table: "M_Employee",
                column: "FK_CafeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_Employee");

            migrationBuilder.DropTable(
                name: "M_Cafe");
        }
    }
}
