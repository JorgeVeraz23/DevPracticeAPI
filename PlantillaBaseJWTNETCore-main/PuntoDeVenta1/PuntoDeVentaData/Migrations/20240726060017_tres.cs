using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class tres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autors_Libros_LibroIdLibro",
                table: "Autors");

            migrationBuilder.DropIndex(
                name: "IX_Autors_LibroIdLibro",
                table: "Autors");

            migrationBuilder.DropColumn(
                name: "LibroIdLibro",
                table: "Autors");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "LibroIdLibro",
                table: "Autors",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Autors_LibroIdLibro",
                table: "Autors",
                column: "LibroIdLibro");

            migrationBuilder.AddForeignKey(
                name: "FK_Autors_Libros_LibroIdLibro",
                table: "Autors",
                column: "LibroIdLibro",
                principalTable: "Libros",
                principalColumn: "IdLibro");
        }
    }
}
