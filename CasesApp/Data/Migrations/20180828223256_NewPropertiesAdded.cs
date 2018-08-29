using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesApp.Data.Migrations
{
    public partial class NewPropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApproveDate",
                table: "Case",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Case",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RevieweDate",
                table: "Case",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveDate",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "RevieweDate",
                table: "Case");
        }
    }
}
