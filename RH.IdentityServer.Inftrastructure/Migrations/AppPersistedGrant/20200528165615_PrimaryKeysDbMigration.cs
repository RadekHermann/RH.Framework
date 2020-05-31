using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.IdentityServer.Inftrastructure.Migrations.AppPersistedGrant
{
    public partial class PrimaryKeysDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "PersistedGrant",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "PersistedGrant",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "PersistedGrant",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "PersistedGrant",
                maxLength: 50000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "PersistedGrant",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCode",
                table: "DeviceFlowCodes",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "DeviceFlowCodes",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiration",
                table: "DeviceFlowCodes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceCode",
                table: "DeviceFlowCodes",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "DeviceFlowCodes",
                maxLength: 50000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "DeviceFlowCodes",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersistedGrant",
                table: "PersistedGrant",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceFlowCodes",
                table: "DeviceFlowCodes",
                column: "UserCode");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrant_Expiration",
                table: "PersistedGrant",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrant_SubjectId_ClientId_Type",
                table: "PersistedGrant",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFlowCodes_DeviceCode",
                table: "DeviceFlowCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFlowCodes_Expiration",
                table: "DeviceFlowCodes",
                column: "Expiration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersistedGrant",
                table: "PersistedGrant");

            migrationBuilder.DropIndex(
                name: "IX_PersistedGrant_Expiration",
                table: "PersistedGrant");

            migrationBuilder.DropIndex(
                name: "IX_PersistedGrant_SubjectId_ClientId_Type",
                table: "PersistedGrant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceFlowCodes",
                table: "DeviceFlowCodes");

            migrationBuilder.DropIndex(
                name: "IX_DeviceFlowCodes_DeviceCode",
                table: "DeviceFlowCodes");

            migrationBuilder.DropIndex(
                name: "IX_DeviceFlowCodes_Expiration",
                table: "DeviceFlowCodes");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "PersistedGrant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "PersistedGrant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "PersistedGrant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50000);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "PersistedGrant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "PersistedGrant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "DeviceFlowCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiration",
                table: "DeviceFlowCodes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "DeviceCode",
                table: "DeviceFlowCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "DeviceFlowCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50000);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "DeviceFlowCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "UserCode",
                table: "DeviceFlowCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
