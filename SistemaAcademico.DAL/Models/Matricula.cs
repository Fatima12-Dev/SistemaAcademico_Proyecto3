using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Matricula
    {
        public int id_matricula { get; set; } 
        public int id_estudiante { get; set; } 
        public int id_curso { get; set; } 
        public int id_periodo { get; set; } 
        public DateTime fechaInscripcion { get; set; }
    }
}
