using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.BLL.Services;
using SistemaAcademico.DAL;
using SistemaAcademico.DAL.Models;
using SistemaAcademico.Models.ViewModels;

namespace SistemaAcademico.Controllers
{
    public class MatriculaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MatriculaService _matriculaService;

        public MatriculaController(ApplicationDbContext context, MatriculaService matriculaService)
        {
            _context = context;
            _matriculaService = matriculaService;
        }

        public async Task<IActionResult> Index()
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .Include(m => m.Periodo)
                .ToListAsync();

            return View(matriculas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .Include(m => m.Periodo)
                .FirstOrDefaultAsync(m => m.id_matricula == id);

            if (matricula == null) return NotFound();

            return View(matricula);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new MatriculaViewModel
            {
                Estudiantes = new SelectList(await _context.Estudiantes.ToListAsync(), "id_estudiante", "nombreEstudiante"),
                Cursos = new SelectList(await _context.Cursos.ToListAsync(), "id_curso", "nombreCurso"),
                Periodos = new SelectList(await _context.Periodos.ToListAsync(), "id_periodo", "nombrePeriodo")
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatriculaViewModel vm)
        {
            var matriculasExistentes = await _context.Matriculas.ToListAsync();
            bool esValida = _matriculaService.EsMatriculaValida(
                vm.Matricula.id_estudiante,
                vm.Matricula.id_curso,
                vm.Matricula.id_periodo,
                matriculasExistentes);

            if (!esValida)
            {
                TempData["Error"] = "Error: Ya existe una matricula con la misma combinacion de Estudiante, Curso y Periodo.";
                vm.Estudiantes = new SelectList(await _context.Estudiantes.ToListAsync(), "id_estudiante", "nombreEstudiante", vm.Matricula.id_estudiante);
                vm.Cursos = new SelectList(await _context.Cursos.ToListAsync(), "id_curso", "nombreCurso", vm.Matricula.id_curso);
                vm.Periodos = new SelectList(await _context.Periodos.ToListAsync(), "id_periodo", "nombrePeriodo", vm.Matricula.id_periodo);
                return View(vm);
            }

            vm.Matricula.fechaInscripcion = DateTime.Now;
            _context.Add(vm.Matricula);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Matricula creada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null) return NotFound();

            var vm = new MatriculaViewModel
            {
                Matricula = matricula,
                Estudiantes = new SelectList(await _context.Estudiantes.ToListAsync(), "id_estudiante", "nombreEstudiante", matricula.id_estudiante),
                Cursos = new SelectList(await _context.Cursos.ToListAsync(), "id_curso", "nombreCurso", matricula.id_curso),
                Periodos = new SelectList(await _context.Periodos.ToListAsync(), "id_periodo", "nombrePeriodo", matricula.id_periodo)
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MatriculaViewModel vm)
        {
            if (id != vm.Matricula.id_matricula) return NotFound();

            var matriculasExistentes = await _context.Matriculas
                .Where(m => m.id_matricula != id)
                .ToListAsync();

            bool esValida = _matriculaService.EsMatriculaValida(
                vm.Matricula.id_estudiante,
                vm.Matricula.id_curso,
                vm.Matricula.id_periodo,
                matriculasExistentes);

            if (!esValida)
            {
                TempData["Error"] = "Error: Ya existe una matricula con la misma combinacion de Estudiante, Curso y Periodo.";
                vm.Estudiantes = new SelectList(await _context.Estudiantes.ToListAsync(), "id_estudiante", "nombreEstudiante", vm.Matricula.id_estudiante);
                vm.Cursos = new SelectList(await _context.Cursos.ToListAsync(), "id_curso", "nombreCurso", vm.Matricula.id_curso);
                vm.Periodos = new SelectList(await _context.Periodos.ToListAsync(), "id_periodo", "nombrePeriodo", vm.Matricula.id_periodo);
                return View(vm);
            }

            try
            {
                _context.Update(vm.Matricula);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Matricula actualizada exitosamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Matriculas.AnyAsync(m => m.id_matricula == id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .Include(m => m.Periodo)
                .FirstOrDefaultAsync(m => m.id_matricula == id);

            if (matricula == null) return NotFound();

            return View(matricula);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula != null)
            {
                try
                {
                    _context.Matriculas.Remove(matricula);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Matricula eliminada exitosamente.";
                }
                catch (DbUpdateException)
                {
                    TempData["Error"] = "No se puede eliminar esta matrícula porque tiene notas asociadas.";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
