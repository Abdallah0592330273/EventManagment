using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class includedescriptionInEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Events",
                newName: "EventStartDate");

            migrationBuilder.AddColumn<string>(
                name: "EventDescription",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEndDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDescription",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventEndDate",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventStartDate",
                table: "Events",
                newName: "EventDate");
        }
    }
}
