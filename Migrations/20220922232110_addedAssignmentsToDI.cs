using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework_Log.Migrations
{
    public partial class addedAssignmentsToDI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Courses_CourseID",
                table: "Assignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_CourseID",
                table: "Assignments",
                newName: "IX_Assignments_CourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseID",
                table: "Assignments",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseID",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_CourseID",
                table: "Assignment",
                newName: "IX_Assignment_CourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Courses_CourseID",
                table: "Assignment",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
