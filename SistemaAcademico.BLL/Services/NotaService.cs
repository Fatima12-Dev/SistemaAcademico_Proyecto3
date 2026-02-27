using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademico.DAL.Models;

namespace SistemaAcademico.BLL.Services
{
    public class NotaService
    {

        public string ValidarYCalcularNota(Nota nota)
        {
            // REGLA 4: No permitir notas fuera de rango 0–10
            if (nota.nota1 < 0 || nota.nota1 > 10 ||
                nota.nota2 < 0 || nota.nota2 > 10 ||
                nota.nota3 < 0 || nota.nota3 > 10)
            {
                return "Error: Las notas deben estar en el rango de 0 a 10.";
            }

            // REGLA 2: Ponderación de notas (30/30/40)
            nota.promedio = (nota.nota1 * 0.30m) + (nota.nota2 * 0.30m) + (nota.nota3 * 0.40m);

            // REGLA 3: Estado Aprobado/Reprobado (>= 7)
            if (nota.promedio >= 7)
            {
                nota.estado = "Aprobado";
            }
            else
            {
                nota.estado = "Reprobado";
            }

            return "Exito";
        }

        // REGLA 5: Bloqueo de edición después de 7 días
        public bool PermitirEdicion(DateTime fechaRegistro)
        {
            TimeSpan diferencia = DateTime.Now - fechaRegistro;


            return diferencia.TotalDays <= 7;
        }
    }
}
