using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.IdentityServer.Inftrastructure.Migrations.AppPersistedGrant
{
    public partial class InitialDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceFlowCodes",
                columns: table => new
                {
                    DeviceCode = table.Column<string>(nullable: true),
                    UserCode = table.Column<string>(nullable: true),
                    SubjectId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrant",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    SubjectId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "PersistedGrant");
        }
    }
}
