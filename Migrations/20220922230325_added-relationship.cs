using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework_Log.Migrations
{
    public partial class addedrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assignment",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Assignment",
                newName: "Id");
        }
    }
}
