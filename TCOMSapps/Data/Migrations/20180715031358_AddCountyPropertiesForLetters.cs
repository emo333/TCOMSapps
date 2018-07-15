using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TCOMSapps.Data.Migrations
{
    public partial class AddCountyPropertiesForLetters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterAddressLine1",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterAddressLine2",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterCity",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterPhoneNumbers",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterState",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterTaxCollectorName",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterWebsite",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterZip",
                table: "Counties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OosTitleLetterAddressLine1",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "OosTitleLetterAddressLine2",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "OosTitleLetterCity",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "OosTitleLetterPhoneNumbers",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "OosTitleLetterState",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "OosTitleLetterTaxCollectorName",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "OosTitleLetterWebsite",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "OosTitleLetterZip",
                table: "Counties");
        }
    }
}
