using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARMApocalypseSASAPI.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Survivors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    LastLocationLongitude = table.Column<double>(type: "REAL", nullable: false),
                    LastLocationLatitude = table.Column<double>(type: "REAL", nullable: false),
                    IsInfected = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survivors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurvivorInfectionReports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ReportingSurvivorID = table.Column<string>(type: "TEXT", nullable: false),
                    ReportedSurvivorID = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfReport = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurvivorInfectionReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurvivorInfectionReports_Survivors_ReportedSurvivorID",
                        column: x => x.ReportedSurvivorID,
                        principalTable: "Survivors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurvivorInfectionReports_Survivors_ReportingSurvivorID",
                        column: x => x.ReportingSurvivorID,
                        principalTable: "Survivors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    SurvivorId = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPoints = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeItems_Survivors_SurvivorId",
                        column: x => x.SurvivorId,
                        principalTable: "Survivors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[] { "1084e310-6a07-4d9c-92a6-8225ab696e25", new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(4004), true, false, "Food", 3m, new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(4004) });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[] { "1c06a133-72e2-462a-8cad-f5d1865ef417", new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(4008), true, false, "Medication", 2m, new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(4008) });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[] { "38604219-8499-464b-8a1b-c6fbd226773f", new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(3964), true, false, "Water", 4m, new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(3966) });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[] { "5d7acdd0-3a9b-47ca-89f1-a8228ac136f2", new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(4010), true, false, "Ammunition", 1m, new DateTime(2022, 12, 30, 8, 45, 28, 912, DateTimeKind.Utc).AddTicks(4011) });

            migrationBuilder.CreateIndex(
                name: "IX_SurvivorInfectionReports_ReportedSurvivorID",
                table: "SurvivorInfectionReports",
                column: "ReportedSurvivorID");

            migrationBuilder.CreateIndex(
                name: "IX_SurvivorInfectionReports_ReportingSurvivorID",
                table: "SurvivorInfectionReports",
                column: "ReportingSurvivorID");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_ItemId",
                table: "TradeItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_SurvivorId",
                table: "TradeItems",
                column: "SurvivorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurvivorInfectionReports");

            migrationBuilder.DropTable(
                name: "TradeItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Survivors");
        }
    }
}
