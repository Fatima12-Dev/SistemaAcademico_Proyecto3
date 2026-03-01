using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.DAL;
using SistemaAcademico.Models;

namespace SistemaAcademico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalEstudiantes = await _context.Estudiantes.CountAsync();
            ViewBag.TotalCursos = await _context.Cursos.CountAsync();
            ViewBag.TotalPeriodos = await _context.Periodos.CountAsync();
            ViewBag.TotalMatriculas = await _context.Matriculas.CountAsync();
            ViewBag.TotalNotas = await _context.Notas.CountAsync();

            ViewBag.Aprobados = await _context.Notas.CountAsync(n => n.estado == "Aprobado");
            ViewBag.Reprobados = await _context.Notas.CountAsync(n => n.estado == "Reprobado");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
