using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietProyect_IV.Migrations
{
    /// <inheritdoc />
    public partial class DatosFaltantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculoCalorias_NivelActividad_NivelActividadId",
                table: "CalculoCalorias");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculoCalorias_Usuario_UsuarioId",
                table: "CalculoCalorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NivelActividad",
                table: "NivelActividad");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "NivelActividad",
                newName: "NivelActividades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NivelActividades",
                table: "NivelActividades",
                column: "NivelActividadId");

            migrationBuilder.CreateTable(
                name: "ObjetivosCaloricos",
                columns: table => new
                {
                    ObjetivoCaloricoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoObjetivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AjusteCalorias = table.Column<int>(type: "int", nullable: false),
                    CalculoCaloriasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjetivosCaloricos", x => x.ObjetivoCaloricoId);
                    table.ForeignKey(
                        name: "FK_ObjetivosCaloricos_CalculoCalorias_CalculoCaloriasId",
                        column: x => x.CalculoCaloriasId,
                        principalTable: "CalculoCalorias",
                        principalColumn: "CalculoCaloriasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjetivosCaloricos_CalculoCaloriasId",
                table: "ObjetivosCaloricos",
                column: "CalculoCaloriasId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculoCalorias_NivelActividades_NivelActividadId",
                table: "CalculoCalorias",
                column: "NivelActividadId",
                principalTable: "NivelActividades",
                principalColumn: "NivelActividadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculoCalorias_Usuarios_UsuarioId",
                table: "CalculoCalorias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculoCalorias_NivelActividades_NivelActividadId",
                table: "CalculoCalorias");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculoCalorias_Usuarios_UsuarioId",
                table: "CalculoCalorias");

            migrationBuilder.DropTable(
                name: "ObjetivosCaloricos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NivelActividades",
                table: "NivelActividades");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "NivelActividades",
                newName: "NivelActividad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NivelActividad",
                table: "NivelActividad",
                column: "NivelActividadId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculoCalorias_NivelActividad_NivelActividadId",
                table: "CalculoCalorias",
                column: "NivelActividadId",
                principalTable: "NivelActividad",
                principalColumn: "NivelActividadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculoCalorias_Usuario_UsuarioId",
                table: "CalculoCalorias",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
