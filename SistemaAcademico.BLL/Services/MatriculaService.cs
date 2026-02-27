using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademico.DAL.Models;

namespace SistemaAcademico.BLL.Services
{
    public class MatriculaService
    {
        // REGLA 1: No duplicar matrícula (Estudiante + Curso + Periodo)
        public bool EsMatriculaValida(int idEstudiante, int idCurso, int idPeriodo, List<Matricula> matriculasExistentes)
        {
 
            bool yaExiste = matriculasExistentes.Any(m =>
                m.id_estudiante == idEstudiante &&
                m.id_curso == idCurso &&
                m.id_periodo == idPeriodo);


            return !yaExiste;
        }
    }
}
