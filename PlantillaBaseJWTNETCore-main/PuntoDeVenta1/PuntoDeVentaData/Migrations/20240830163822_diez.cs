using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class diez : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTag_TagMultiSelects_IdTagMultiSelect",
                table: "UsuarioTag");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTag_UsuarioMultiSelects_IdUsuarioMultiSelect",
                table: "UsuarioTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioTag",
                table: "UsuarioTag");

            migrationBuilder.RenameTable(
                name: "UsuarioTag",
                newName: "UsuarioTags");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTag_IdUsuarioMultiSelect",
                table: "UsuarioTags",
                newName: "IX_UsuarioTags_IdUsuarioMultiSelect");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioTags",
                table: "UsuarioTags",
                columns: new[] { "IdTagMultiSelect", "IdUsuarioMultiSelect" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTags_TagMultiSelects_IdTagMultiSelect",
                table: "UsuarioTags",
                column: "IdTagMultiSelect",
                principalTable: "TagMultiSelects",
                principalColumn: "IdTag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTags_UsuarioMultiSelects_IdUsuarioMultiSelect",
                table: "UsuarioTags",
                column: "IdUsuarioMultiSelect",
                principalTable: "UsuarioMultiSelects",
                principalColumn: "IdUsuarioTag",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTags_TagMultiSelects_IdTagMultiSelect",
                table: "UsuarioTags");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTags_UsuarioMultiSelects_IdUsuarioMultiSelect",
                table: "UsuarioTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioTags",
                table: "UsuarioTags");

            migrationBuilder.RenameTable(
                name: "UsuarioTags",
                newName: "UsuarioTag");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTags_IdUsuarioMultiSelect",
                table: "UsuarioTag",
                newName: "IX_UsuarioTag_IdUsuarioMultiSelect");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioTag",
                table: "UsuarioTag",
                columns: new[] { "IdTagMultiSelect", "IdUsuarioMultiSelect" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTag_TagMultiSelects_IdTagMultiSelect",
                table: "UsuarioTag",
                column: "IdTagMultiSelect",
                principalTable: "TagMultiSelects",
                principalColumn: "IdTag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTag_UsuarioMultiSelects_IdUsuarioMultiSelect",
                table: "UsuarioTag",
                column: "IdUsuarioMultiSelect",
                principalTable: "UsuarioMultiSelects",
                principalColumn: "IdUsuarioTag",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
