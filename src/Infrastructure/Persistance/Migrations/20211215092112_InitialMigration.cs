using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnvVariable",
                columns: table => new
                {
                    IdEnvVariable = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvVariable", x => x.IdEnvVariable);
                });

            migrationBuilder.CreateTable(
                name: "LicenseType",
                columns: table => new
                {
                    IdLicenseType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    LicenseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfWebsites = table.Column<int>(type: "int", nullable: false),
                    NumberOfUsers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseType", x => x.IdLicenseType);
                });

            migrationBuilder.CreateTable(
                name: "MachineResetSettings",
                columns: table => new
                {
                    IdMachineResetSettings = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResetTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineResetSettings", x => x.IdMachineResetSettings);
                });

            migrationBuilder.CreateTable(
                name: "MachineResponseLog",
                columns: table => new
                {
                    IdMachineResponseLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineResponseLog", x => x.IdMachineResponseLog);
                });

            migrationBuilder.CreateTable(
                name: "MachineResponseSettings",
                columns: table => new
                {
                    IdMachineResponseSettings = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WaitingTime = table.Column<int>(type: "int", nullable: false),
                    NumberOfSelectedWebsite = table.Column<int>(type: "int", nullable: false),
                    TestEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastTaskTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineResponseSettings", x => x.IdMachineResponseSettings);
                });

            migrationBuilder.CreateTable(
                name: "MachineUptimeLog",
                columns: table => new
                {
                    IdMachineUptimeLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineUptimeLog", x => x.IdMachineUptimeLog);
                });

            migrationBuilder.CreateTable(
                name: "MachineUptimeSettings",
                columns: table => new
                {
                    IdMachineUptimeSettings = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WaitingTime = table.Column<int>(type: "int", nullable: false),
                    NumberOfSelectedWebsite = table.Column<int>(type: "int", nullable: false),
                    LastTaskTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineUptimeSettings", x => x.IdMachineUptimeSettings);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteUptimeMachineLibrary",
                columns: table => new
                {
                    IdWebsiteUptimeMachineLibrary = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineLocation = table.Column<int>(type: "int", nullable: false),
                    MachinEnviroment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteUptimeMachineLibrary", x => x.IdWebsiteUptimeMachineLibrary);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    IdLicense = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLicenseType = table.Column<int>(type: "int", nullable: false),
                    LicenseExpired = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.IdLicense);
                    table.ForeignKey(
                        name: "FK_License_LicenseType_IdLicenseType",
                        column: x => x.IdLicenseType,
                        principalTable: "LicenseType",
                        principalColumn: "IdLicenseType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLicense = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_User_License_IdLicense",
                        column: x => x.IdLicense,
                        principalTable: "License",
                        principalColumn: "IdLicense",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Website",
                columns: table => new
                {
                    IdWebsite = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLicense = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestLocation = table.Column<int>(type: "int", nullable: false),
                    TestEnviroment = table.Column<int>(type: "int", nullable: false),
                    IsTestedByBenchmark = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    BenchmarkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatestUptimeResult = table.Column<bool>(type: "bit", nullable: false),
                    LastTestedByBenchmark = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Website", x => x.IdWebsite);
                    table.ForeignKey(
                        name: "FK_Website_License_IdLicense",
                        column: x => x.IdLicense,
                        principalTable: "License",
                        principalColumn: "IdLicense",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteResponseScore",
                columns: table => new
                {
                    IdWebsiteResponseScore = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWebsite = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    structure_score = table.Column<int>(type: "int", nullable: false),
                    speed_index = table.Column<int>(type: "int", nullable: false),
                    onload_time = table.Column<int>(type: "int", nullable: false),
                    redirect_duration = table.Column<int>(type: "int", nullable: false),
                    first_paint_time = table.Column<int>(type: "int", nullable: false),
                    dom_content_loaded_duration = table.Column<int>(type: "int", nullable: false),
                    dom_content_loaded_time = table.Column<int>(type: "int", nullable: false),
                    dom_interactive_time = table.Column<int>(type: "int", nullable: false),
                    page_requests = table.Column<int>(type: "int", nullable: false),
                    page_bytes = table.Column<int>(type: "int", nullable: false),
                    gtmetrix_grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    html_bytes = table.Column<int>(type: "int", nullable: false),
                    first_contentful_paint = table.Column<int>(type: "int", nullable: false),
                    performance_score = table.Column<int>(type: "int", nullable: false),
                    fully_loaded_time = table.Column<int>(type: "int", nullable: false),
                    total_blocking_time = table.Column<int>(type: "int", nullable: false),
                    largest_contentful_paint = table.Column<int>(type: "int", nullable: false),
                    time_to_interactive = table.Column<int>(type: "int", nullable: false),
                    time_to_first_byte = table.Column<int>(type: "int", nullable: false),
                    rum_speed_index = table.Column<int>(type: "int", nullable: false),
                    backend_duration = table.Column<int>(type: "int", nullable: false),
                    onload_duration = table.Column<int>(type: "int", nullable: false),
                    connect_duration = table.Column<int>(type: "int", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteResponseScore", x => x.IdWebsiteResponseScore);
                    table.ForeignKey(
                        name: "FK_WebsiteResponseScore_Website_IdWebsite",
                        column: x => x.IdWebsite,
                        principalTable: "Website",
                        principalColumn: "IdWebsite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteResponseSettings",
                columns: table => new
                {
                    IdWebsiteResponseSettings = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWebsite = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adblock = table.Column<int>(type: "int", nullable: false),
                    SimulateDevice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteResponseSettings", x => x.IdWebsiteResponseSettings);
                    table.ForeignKey(
                        name: "FK_WebsiteResponseSettings_Website_IdWebsite",
                        column: x => x.IdWebsite,
                        principalTable: "Website",
                        principalColumn: "IdWebsite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteUptimeScore",
                columns: table => new
                {
                    IdWebsiteUptimeScore = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWebsite = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElapsedTime = table.Column<int>(type: "int", nullable: false),
                    TestTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteUptimeScore", x => x.IdWebsiteUptimeScore);
                    table.ForeignKey(
                        name: "FK_WebsiteUptimeScore_Website_IdWebsite",
                        column: x => x.IdWebsite,
                        principalTable: "Website",
                        principalColumn: "IdWebsite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_License_IdLicenseType",
                table: "License",
                column: "IdLicenseType");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdLicense",
                table: "User",
                column: "IdLicense");

            migrationBuilder.CreateIndex(
                name: "IX_Website_IdLicense",
                table: "Website",
                column: "IdLicense");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteResponseScore_IdWebsite",
                table: "WebsiteResponseScore",
                column: "IdWebsite");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteResponseSettings_IdWebsite",
                table: "WebsiteResponseSettings",
                column: "IdWebsite",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteUptimeScore_IdWebsite",
                table: "WebsiteUptimeScore",
                column: "IdWebsite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvVariable");

            migrationBuilder.DropTable(
                name: "MachineResetSettings");

            migrationBuilder.DropTable(
                name: "MachineResponseLog");

            migrationBuilder.DropTable(
                name: "MachineResponseSettings");

            migrationBuilder.DropTable(
                name: "MachineUptimeLog");

            migrationBuilder.DropTable(
                name: "MachineUptimeSettings");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WebsiteResponseScore");

            migrationBuilder.DropTable(
                name: "WebsiteResponseSettings");

            migrationBuilder.DropTable(
                name: "WebsiteUptimeMachineLibrary");

            migrationBuilder.DropTable(
                name: "WebsiteUptimeScore");

            migrationBuilder.DropTable(
                name: "Website");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "LicenseType");
        }
    }
}
