using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMedia.Migrations
{
    public partial class AddGroupSubscriptionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "groupId",
                table: "Post",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InscricoesGrupos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(nullable: true),
                    grupoId = table.Column<int>(nullable: true),
                    userAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscricoesGrupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InscricoesGrupos_Grupos_grupoId",
                        column: x => x.grupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InscricoesGrupos_User_userAccountId",
                        column: x => x.userAccountId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_groupId",
                table: "Post",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_InscricoesGrupos_grupoId",
                table: "InscricoesGrupos",
                column: "grupoId");

            migrationBuilder.CreateIndex(
                name: "IX_InscricoesGrupos_userAccountId",
                table: "InscricoesGrupos",
                column: "userAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Grupos_groupId",
                table: "Post",
                column: "groupId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Grupos_groupId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "InscricoesGrupos");

            migrationBuilder.DropIndex(
                name: "IX_Post_groupId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "groupId",
                table: "Post");
        }
    }
}
