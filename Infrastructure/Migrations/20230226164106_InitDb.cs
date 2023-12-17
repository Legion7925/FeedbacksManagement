using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAndFamily = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblKnowledgeTree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblKnowledgeTree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblKnowledgeTree_TblKnowledgeTree_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TblKnowledgeTree",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TblPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblSpecialtie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSpecialtie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUserPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdUser = table.Column<int>(type: "int", nullable: false),
                    FkIdPage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordReset = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblCase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkIdCustomer = table.Column<int>(type: "int", nullable: false),
                    FkIdProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblCase_TblCustomer_FkIdCustomer",
                        column: x => x.FkIdCustomer,
                        principalTable: "TblCustomer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TblCase_TblProduct_FkIdProduct",
                        column: x => x.FkIdProduct,
                        principalTable: "TblProduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TblFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<byte>(type: "tinyint", nullable: false),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkIdCustomer = table.Column<int>(type: "int", nullable: false),
                    FkIdProduct = table.Column<int>(type: "int", nullable: false),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priorty = table.Column<byte>(type: "tinyint", nullable: false),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    Respond = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Similarity = table.Column<byte>(type: "tinyint", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblFeedback_TblCustomer_FkIdCustomer",
                        column: x => x.FkIdCustomer,
                        principalTable: "TblCustomer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TblFeedback_TblProduct_FkIdProduct",
                        column: x => x.FkIdProduct,
                        principalTable: "TblProduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TblExpert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<byte>(type: "tinyint", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FkIdSpecialty = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblExpert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblExpert_TblSpecialtie_FkIdSpecialty",
                        column: x => x.FkIdSpecialty,
                        principalTable: "TblSpecialtie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TblRelationshipTagFeedbak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdFeedback = table.Column<int>(type: "int", nullable: false),
                    FkIdTag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRelationshipTagFeedbak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblRelationshipTagFeedbak_TblFeedback_FkIdFeedback",
                        column: x => x.FkIdFeedback,
                        principalTable: "TblFeedback",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TblRelationshipTagFeedbak_TblTag_FkIdTag",
                        column: x => x.FkIdTag,
                        principalTable: "TblTag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TblRelationshipExpertFeedbak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdExpert = table.Column<int>(type: "int", nullable: false),
                    FkIdFeedback = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRelationshipExpertFeedbak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblRelationshipExpertFeedbak_TblExpert_FkIdExpert",
                        column: x => x.FkIdExpert,
                        principalTable: "TblExpert",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TblRelationshipExpertFeedbak_TblFeedback_FkIdExpert",
                        column: x => x.FkIdExpert,
                        principalTable: "TblFeedback",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblCase_FkIdCustomer",
                table: "TblCase",
                column: "FkIdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_TblCase_FkIdProduct",
                table: "TblCase",
                column: "FkIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_TblExpert_FkIdSpecialty",
                table: "TblExpert",
                column: "FkIdSpecialty");

            migrationBuilder.CreateIndex(
                name: "IX_TblFeedback_FkIdCustomer",
                table: "TblFeedback",
                column: "FkIdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_TblFeedback_FkIdProduct",
                table: "TblFeedback",
                column: "FkIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_TblKnowledgeTree_ParentId",
                table: "TblKnowledgeTree",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipExpertFeedbak_FkIdExpert",
                table: "TblRelationshipExpertFeedbak",
                column: "FkIdExpert");

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipTagFeedbak_FkIdFeedback",
                table: "TblRelationshipTagFeedbak",
                column: "FkIdFeedback");

            migrationBuilder.CreateIndex(
                name: "IX_TblRelationshipTagFeedbak_FkIdTag",
                table: "TblRelationshipTagFeedbak",
                column: "FkIdTag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblCase");

            migrationBuilder.DropTable(
                name: "TblKnowledgeTree");

            migrationBuilder.DropTable(
                name: "TblPages");

            migrationBuilder.DropTable(
                name: "TblRelationshipExpertFeedbak");

            migrationBuilder.DropTable(
                name: "TblRelationshipTagFeedbak");

            migrationBuilder.DropTable(
                name: "TblUserPages");

            migrationBuilder.DropTable(
                name: "TblUsers");

            migrationBuilder.DropTable(
                name: "TblExpert");

            migrationBuilder.DropTable(
                name: "TblFeedback");

            migrationBuilder.DropTable(
                name: "TblTag");

            migrationBuilder.DropTable(
                name: "TblSpecialtie");

            migrationBuilder.DropTable(
                name: "TblCustomer");

            migrationBuilder.DropTable(
                name: "TblProduct");
        }
    }
}
