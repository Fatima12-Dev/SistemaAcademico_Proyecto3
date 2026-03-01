using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Matricula
    {
        public int id_matricula { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un estudiante.")]
        public int id_estudiante { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un curso.")]
        public int id_curso { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un periodo.")]
        public int id_periodo { get; set; }

        public DateTime fechaInscripcion { get; set; }

        public Estudiante? Estudiante { get; set; }
        public Curso? Curso { get; set; }
        public Periodo? Periodo { get; set; }
        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
