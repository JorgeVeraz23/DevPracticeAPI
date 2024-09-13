using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class prueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LIB");

            migrationBuilder.CreateTable(
                name: "BOOK",
                schema: "LIB",
                columns: table => new
                {
                    IdBook = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOK", x => x.IdBook);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BOOK_Codigo",
                schema: "LIB",
                table: "BOOK",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOOK",
                schema: "LIB");
        }
    }
}
