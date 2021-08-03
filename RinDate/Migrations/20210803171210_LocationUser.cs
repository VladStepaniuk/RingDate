using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace RinDate.Migrations
{
    public partial class LocationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RolesIn");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesIn",
                table: "RolesIn",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesIn",
                table: "RolesIn");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "RolesIn",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");
        }
    }
}
