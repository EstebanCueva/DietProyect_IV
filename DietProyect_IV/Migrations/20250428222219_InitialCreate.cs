using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietProyect_IV.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NivelActividad",
                columns: table => new
                {
                    NivelActividadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FactorActividad = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelActividad", x => x.NivelActividadId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    PesoKg = table.Column<double>(type: "float", nullable: false),
                    AlturaCm = table.Column<double>(type: "float", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "CalculoCalorias",
                columns: table => new
                {
                    CalculoCaloriasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TasaMetabolicaBasal = table.Column<double>(type: "float", nullable: false),
                    CaloriasDiarias = table.Column<double>(type: "float", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NivelActividadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoCalorias", x => x.CalculoCaloriasId);
                    table.ForeignKey(
                        name: "FK_CalculoCalorias_NivelActividad_NivelActividadId",
                        column: x => x.NivelActividadId,
                        principalTable: "NivelActividad",
                        principalColumn: "NivelActividadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculoCalorias_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculoCalorias_NivelActividadId",
                table: "CalculoCalorias",
                column: "NivelActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculoCalorias_UsuarioId",
                table: "CalculoCalorias",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculoCalorias");

            migrationBuilder.DropTable(
                name: "NivelActividad");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
