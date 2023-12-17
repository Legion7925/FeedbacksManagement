using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditDbRe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FkIdExpert",
                table: "TblRelationshipExpertFeedbak",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FkIdSpecialty",
                table: "TblRelationshipExpertFeedbak",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipExpertFeedbak_FkIdSpecialty",
                table: "TblRelationshipExpertFeedbak",
                column: "FkIdSpecialty");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRelationshipExpertFeedbak_TblSpecialtie_FkIdSpecialty",
                table: "TblRelationshipExpertFeedbak",
                column: "FkIdSpecialty",
                principalTable: "TblSpecialtie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblRelationshipExpertFeedbak_TblSpecialtie_FkIdSpecialty",
                table: "TblRelationshipExpertFeedbak");

            migrationBuilder.DropIndex(
                name: "IX_TblRelationshipExpertFeedbak_FkIdSpecialty",
                table: "TblRelationshipExpertFeedbak");

            migrationBuilder.DropColumn(
                name: "FkIdSpecialty",
                table: "TblRelationshipExpertFeedbak");

            migrationBuilder.AlterColumn<int>(
                name: "FkIdExpert",
                table: "TblRelationshipExpertFeedbak",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
