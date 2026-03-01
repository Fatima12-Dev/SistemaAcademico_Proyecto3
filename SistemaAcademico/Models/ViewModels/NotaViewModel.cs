using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaAcademico.DAL.Models;

namespace SistemaAcademico.Models.ViewModels
{
    public class NotaViewModel
    {
        public Nota Nota { get; set; } = new Nota();
        public SelectList? Matriculas { get; set; }
        public bool PuedeEditar { get; set; } = true;
    }
}
