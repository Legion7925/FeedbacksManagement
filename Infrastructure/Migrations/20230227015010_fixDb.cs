using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblRelationshipExpertFeedbak_TblFeedback_FkIdExpert",
                table: "TblRelationshipExpertFeedbak");

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipExpertFeedbak_FkIdFeedback",
                table: "TblRelationshipExpertFeedbak",
                column: "FkIdFeedback");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRelationshipExpertFeedbak_TblFeedback_FkIdFeedback",
                table: "TblRelationshipExpertFeedbak",
                column: "FkIdFeedback",
                principalTable: "TblFeedback",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblRelationshipExpertFeedbak_TblFeedback_FkIdFeedback",
                table: "TblRelationshipExpertFeedbak");

            migrationBuilder.DropIndex(
                name: "IX_TblRelationshipExpertFeedbak_FkIdFeedback",
                table: "TblRelationshipExpertFeedbak");

            migrationBuilder.AddForeignKey(
                name: "FK_TblRelationshipExpertFeedbak_TblFeedback_FkIdExpert",
                table: "TblRelationshipExpertFeedbak",
                column: "FkIdExpert",
                principalTable: "TblFeedback",
                principalColumn: "Id");
        }
    }
}
