using Microsoft.EntityFrameworkCore.Migrations;

namespace IgnProtoView.Migrations
{
    public partial class clickedLinkField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LinkClicked",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkClicked",
                table: "AspNetUsers");
        }
    }
}
