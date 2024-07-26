using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class dos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Autors_AutorIdAutor",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_AutorIdAutor",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "AutorIdAutor",
                table: "Libros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AutorIdAutor",
                table: "Libros",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_AutorIdAutor",
                table: "Libros",
                column: "AutorIdAutor");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Autors_AutorIdAutor",
                table: "Libros",
                column: "AutorIdAutor",
                principalTable: "Autors",
                principalColumn: "IdAutor");
        }
    }
}
