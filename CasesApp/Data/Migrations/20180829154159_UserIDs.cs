using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesApp.Data.Migrations
{
    public partial class UserIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Case",
                newName: "WorkerID");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "Case",
                newName: "ReviewerID");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "Case",
                newName: "ApproverID");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerID",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewerID",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApproverID",
                table: "Case",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkerID",
                table: "Case",
                newName: "WorkerId");

            migrationBuilder.RenameColumn(
                name: "ReviewerID",
                table: "Case",
                newName: "ReviewerId");

            migrationBuilder.RenameColumn(
                name: "ApproverID",
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
    }
}
