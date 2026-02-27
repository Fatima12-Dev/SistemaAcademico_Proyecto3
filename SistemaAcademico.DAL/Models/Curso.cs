using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Curso
    {
        public int id_curso { get; set; } 
        public string? nombreCurso { get; set; }
        public string? seccion { get; set; }
    }
}
