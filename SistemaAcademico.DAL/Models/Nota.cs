using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Nota
    {
        public int id_nota { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una matricula.")]
        public int id_matricula { get; set; }

        [Required(ErrorMessage = "La nota 1 es obligatoria.")]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        public decimal nota1 { get; set; }

        [Required(ErrorMessage = "La nota 2 es obligatoria.")]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        public decimal nota2 { get; set; }

        [Required(ErrorMessage = "La nota 3 es obligatoria.")]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        public decimal nota3 { get; set; }

        public decimal promedio { get; set; }
        public string? estado { get; set; }
        public DateTime fechaRegistro { get; set; }

        public Matricula? Matricula { get; set; }
    }
}
