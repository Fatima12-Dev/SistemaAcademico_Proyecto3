using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademico.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    id_curso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCurso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.id_curso);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    id_estudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreEstudiante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    carnet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.id_estudiante);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    id_matricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_estudiante = table.Column<int>(type: "int", nullable: false),
                    id_curso = table.Column<int>(type: "int", nullable: false),
                    id_periodo = table.Column<int>(type: "int", nullable: false),
                    fechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.id_matricula);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    id_nota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_matricula = table.Column<int>(type: "int", nullable: false),
                    nota1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nota2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nota3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    promedio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.id_nota);
                });

            migrationBuilder.CreateTable(
                name: "Periodos",
                columns: table => new
                {
                    id_periodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombrePeriodo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodos", x => x.id_periodo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Periodos");
        }
    }
}
