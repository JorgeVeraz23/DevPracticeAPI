using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class nueve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTag_TagMultiSelect_IdTagMultiSelect",
                table: "UsuarioTag");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTag_UsuarioMultiSelect_IdUsuarioMultiSelect",
                table: "UsuarioTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioMultiSelect",
                table: "UsuarioMultiSelect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagMultiSelect",
                table: "TagMultiSelect");

            migrationBuilder.RenameTable(
                name: "UsuarioMultiSelect",
                newName: "UsuarioMultiSelects");

            migrationBuilder.RenameTable(
                name: "TagMultiSelect",
                newName: "TagMultiSelects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioMultiSelects",
                table: "UsuarioMultiSelects",
                column: "IdUsuarioTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagMultiSelects",
                table: "TagMultiSelects",
                column: "IdTag");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTag_TagMultiSelects_IdTagMultiSelect",
                table: "UsuarioTag");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTag_UsuarioMultiSelects_IdUsuarioMultiSelect",
                table: "UsuarioTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioMultiSelects",
                table: "UsuarioMultiSelects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagMultiSelects",
                table: "TagMultiSelects");

            migrationBuilder.RenameTable(
                name: "UsuarioMultiSelects",
                newName: "UsuarioMultiSelect");

            migrationBuilder.RenameTable(
                name: "TagMultiSelects",
                newName: "TagMultiSelect");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioMultiSelect",
                table: "UsuarioMultiSelect",
                column: "IdUsuarioTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagMultiSelect",
                table: "TagMultiSelect",
                column: "IdTag");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTag_TagMultiSelect_IdTagMultiSelect",
                table: "UsuarioTag",
                column: "IdTagMultiSelect",
                principalTable: "TagMultiSelect",
                principalColumn: "IdTag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTag_UsuarioMultiSelect_IdUsuarioMultiSelect",
                table: "UsuarioTag",
                column: "IdUsuarioMultiSelect",
                principalTable: "UsuarioMultiSelect",
                principalColumn: "IdUsuarioTag",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
