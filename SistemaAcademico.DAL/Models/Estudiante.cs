using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Estudiante
    {
        public int id_estudiante { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres.")]
        public string? nombreEstudiante { get; set; }

        [Required(ErrorMessage = "El carnet es obligatorio.")]
        [StringLength(20, ErrorMessage = "El carnet no puede exceder 20 caracteres.")]
        public string? carnet { get; set; }

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
