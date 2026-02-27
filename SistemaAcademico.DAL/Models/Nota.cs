using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.DAL.Models
{
    public class Nota
    {
        public int id_nota { get; set; } 
        public int id_matricula { get; set; } 
        public decimal nota1 { get; set; }
        public decimal nota2 { get; set; }
        public decimal nota3 { get; set; }
        public decimal promedio { get; set; }
        public string? estado { get; set; } 
        public DateTime fechaRegistro { get; set; } 
    }
}
