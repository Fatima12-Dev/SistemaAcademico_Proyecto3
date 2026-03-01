using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademico.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionesFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notas_id_matricula",
                table: "Notas",
                column: "id_matricula");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_id_curso",
                table: "Matriculas",
                column: "id_curso");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_id_estudiante",
                table: "Matriculas",
                column: "id_estudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_id_periodo",
                table: "Matriculas",
                column: "id_periodo");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Cursos_id_curso",
                table: "Matriculas",
                column: "id_curso",
                principalTable: "Cursos",
                principalColumn: "id_curso",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Estudiantes_id_estudiante",
                table: "Matriculas",
                column: "id_estudiante",
                principalTable: "Estudiantes",
                principalColumn: "id_estudiante",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Periodos_id_periodo",
                table: "Matriculas",
                column: "id_periodo",
                principalTable: "Periodos",
                principalColumn: "id_periodo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Matriculas_id_matricula",
                table: "Notas",
                column: "id_matricula",
                principalTable: "Matriculas",
                principalColumn: "id_matricula",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Cursos_id_curso",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Estudiantes_id_estudiante",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Periodos_id_periodo",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Matriculas_id_matricula",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Notas_id_matricula",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_id_curso",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_id_estudiante",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_id_periodo",
                table: "Matriculas");
        }
    }
}
