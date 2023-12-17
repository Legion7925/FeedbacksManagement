using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class manytomanytreeexperttag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblRelationshipTreeExpert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdTree = table.Column<int>(type: "int", nullable: false),
                    FkIdExpert = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRelationshipTreeExpert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblRelationshipTreeExpert_TblExpert_FkIdExpert",
                        column: x => x.FkIdExpert,
                        principalTable: "TblExpert",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TblRelationshipTreeExpert_TblKnowledgeTree_FkIdTree",
                        column: x => x.FkIdTree,
                        principalTable: "TblKnowledgeTree",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TblRelationshipTreeTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdTree = table.Column<int>(type: "int", nullable: false),
                    FkIdTag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRelationshipTreeTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblRelationshipTreeTag_TblKnowledgeTree_FkIdTree",
                        column: x => x.FkIdTree,
                        principalTable: "TblKnowledgeTree",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TblRelationshipTreeTag_TblTag_FkIdTag",
                        column: x => x.FkIdTag,
                        principalTable: "TblTag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipTreeExpert_FkIdExpert",
                table: "TblRelationshipTreeExpert",
                column: "FkIdExpert");

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipTreeExpert_FkIdTree",
                table: "TblRelationshipTreeExpert",
                column: "FkIdTree");

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipTreeTag_FkIdTag",
                table: "TblRelationshipTreeTag",
                column: "FkIdTag");

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipTreeTag_FkIdTree",
                table: "TblRelationshipTreeTag",
                column: "FkIdTree");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblRelationshipTreeExpert");

            migrationBuilder.DropTable(
                name: "TblRelationshipTreeTag");
        }
    }
}
