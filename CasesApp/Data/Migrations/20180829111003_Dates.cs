using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesApp.Data.Migrations
{
    public partial class Dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveDate",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "Case",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Case",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReviewed",
                table: "Case",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "DateReviewed",
                table: "Case");

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
                name: "ReviewDate",
                table: "Case",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");
        }
    }
}
