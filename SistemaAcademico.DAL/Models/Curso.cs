using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Curso
    {
        public int id_curso { get; set; }

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres.")]
        public string? nombreCurso { get; set; }

        [Required(ErrorMessage = "La seccion es obligatoria.")]
        [StringLength(10, ErrorMessage = "La seccion no puede exceder 10 caracteres.")]
        public string? seccion { get; set; }

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
