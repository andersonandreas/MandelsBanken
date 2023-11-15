using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MandelsBankenConsole.Migrations
{
    public partial class UpdatedIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Accounts",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "ID");
        }
    }
}
