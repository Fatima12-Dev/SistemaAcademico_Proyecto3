using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Estudiante
    {
        public int id_estudiante { get; set; } 
        public string? nombreEstudiante { get; set; }
        public string? carnet { get; set; }
    }
}
