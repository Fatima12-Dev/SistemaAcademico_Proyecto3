using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaAcademico.DAL.Models;

namespace SistemaAcademico.Models.ViewModels
{
    public class MatriculaViewModel
    {
        public Matricula Matricula { get; set; } = new Matricula();
        public SelectList? Estudiantes { get; set; }
        public SelectList? Cursos { get; set; }
        public SelectList? Periodos { get; set; }
    }
}
