using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesApp.Data.Migrations
{
    public partial class emails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproverEmail",
                table: "Case",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerEmail",
                table: "Case",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerEmail",
                table: "Case",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverEmail",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "ReviewerEmail",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "WorkerEmail",
                table: "Case");
        }
    }
}
