using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesApp.Data.Migrations
{
    public partial class ApplicationUsersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Worker",
                table: "Case",
                newName: "WorkerId");

            migrationBuilder.RenameColumn(
                name: "Reviewer",
                table: "Case",
                newName: "ReviewerId");

            migrationBuilder.RenameColumn(
                name: "RevieweDate",
                table: "Case",
                newName: "ReviewDate");

            migrationBuilder.RenameColumn(
                name: "Approver",
                table: "Case",
                newName: "ApproverId");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerId",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewerId",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApproverId",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Case_ApproverId",
                table: "Case",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_ReviewerId",
                table: "Case",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_WorkerId",
                table: "Case",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_AspNetUsers_ApproverId",
                table: "Case",
                column: "ApproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Case_AspNetUsers_ReviewerId",
                table: "Case",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Case_AspNetUsers_WorkerId",
                table: "Case",
                column: "WorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_AspNetUsers_ApproverId",
                table: "Case");

            migrationBuilder.DropForeignKey(
                name: "FK_Case_AspNetUsers_ReviewerId",
                table: "Case");

            migrationBuilder.DropForeignKey(
                name: "FK_Case_AspNetUsers_WorkerId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_ApproverId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_ReviewerId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_WorkerId",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Case",
                newName: "Worker");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "Case",
                newName: "Reviewer");

            migrationBuilder.RenameColumn(
                name: "ReviewDate",
                table: "Case",
                newName: "RevieweDate");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "Case",
                newName: "Approver");

            migrationBuilder.AlterColumn<string>(
                name: "Worker",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reviewer",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Approver",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
