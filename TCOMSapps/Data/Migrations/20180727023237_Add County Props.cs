using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TCOMSapps.Data.Migrations
{
    public partial class AddCountyProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OosTitleLetterMsPhoneNumber",
                table: "Counties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OosTitleLetterMsPhoneNumber",
                table: "Counties");
        }
    }
}
