using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAnsower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferralDate",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "Respond",
                table: "TblFeedback");

            migrationBuilder.CreateTable(
                name: "TblAnsower",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdExpert = table.Column<int>(type: "int", nullable: false),
                    FkIdFeedback = table.Column<int>(type: "int", nullable: false),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Respond = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAnsower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblAnsower_TblFeedback_FkIdFeedback",
                        column: x => x.FkIdFeedback,
                        principalTable: "TblFeedback",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblAnsower_FkIdFeedback",
                table: "TblAnsower",
                column: "FkIdFeedback");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblAnsower");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReferralDate",
                table: "TblFeedback",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Respond",
                table: "TblFeedback",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
