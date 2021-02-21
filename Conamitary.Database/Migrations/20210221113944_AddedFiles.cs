using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conamitary.Database.Migrations
{
    public partial class AddedFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Md5Checksum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Receips_ReceipeId",
                        column: x => x.ReceipeId,
                        principalTable: "Receips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ReceipeId",
                table: "Images",
                column: "ReceipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
