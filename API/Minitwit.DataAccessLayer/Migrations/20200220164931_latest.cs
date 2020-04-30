using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Minitwit.DataAccessLayer.Migrations
{
    public partial class latest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Latest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    latest = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Latest", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Latest");
        }
    }
}
