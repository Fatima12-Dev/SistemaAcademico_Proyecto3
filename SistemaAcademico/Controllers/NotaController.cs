using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.BLL.Services;
using SistemaAcademico.DAL;
using SistemaAcademico.DAL.Models;
using SistemaAcademico.Models.ViewModels;

namespace SistemaAcademico.Controllers
{
    public class NotaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly NotaService _notaService;

        public NotaController(ApplicationDbContext context, NotaService notaService)
        {
            _context = context;
            _notaService = notaService;
        }

        public async Task<IActionResult> Index()
        {
            var notas = await _context.Notas
                .Include(n => n.Matricula)
                    .ThenInclude(m => m!.Estudiante)
                .Include(n => n.Matricula)
                    .ThenInclude(m => m!.Curso)
                .ToListAsync();

            return View(notas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var nota = await _context.Notas
                .Include(n => n.Matricula)
                    .ThenInclude(m => m!.Estudiante)
                .Include(n => n.Matricula)
                    .ThenInclude(m => m!.Curso)
                .FirstOrDefaultAsync(n => n.id_nota == id);

            if (nota == null) return NotFound();

            return View(nota);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new NotaViewModel
            {
                Matriculas = await ObtenerListaMatriculas()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotaViewModel vm)
        {
            string resultado = _notaService.ValidarYCalcularNota(vm.Nota);

            if (resultado != "Exito")
            {
                TempData["Error"] = resultado;
                vm.Matriculas = await ObtenerListaMatriculas(vm.Nota.id_matricula);
                return View(vm);
            }

            vm.Nota.fechaRegistro = DateTime.Now;
            _context.Add(vm.Nota);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = $"Nota registrada exitosamente. Promedio: {vm.Nota.promedio:F2} - Estado: {vm.Nota.estado}";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null) return NotFound();

            if (!_notaService.PermitirEdicion(nota.fechaRegistro))
            {
                TempData["Error"] = "Error: No se puede editar esta nota. Han pasado mas de 7 dias desde su registro.";
                return RedirectToAction(nameof(Index));
            }

            var vm = new NotaViewModel
            {
                Nota = nota,
                Matriculas = await ObtenerListaMatriculas(nota.id_matricula),
                PuedeEditar = true
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NotaViewModel vm)
        {
            if (id != vm.Nota.id_nota) return NotFound();

            var notaOriginal = await _context.Notas.AsNoTracking()
                .FirstOrDefaultAsync(n => n.id_nota == id);

            if (notaOriginal == null) return NotFound();

            if (!_notaService.PermitirEdicion(notaOriginal.fechaRegistro))
            {
                TempData["Error"] = "Error: No se puede editar esta nota. Han pasado mas de 7 dias desde su registro.";
                return RedirectToAction(nameof(Index));
            }

            string resultado = _notaService.ValidarYCalcularNota(vm.Nota);

            if (resultado != "Exito")
            {
                TempData["Error"] = resultado;
                vm.Matriculas = await ObtenerListaMatriculas(vm.Nota.id_matricula);
                return View(vm);
            }

            try
            {
                vm.Nota.fechaRegistro = notaOriginal.fechaRegistro;
                _context.Update(vm.Nota);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = $"Nota actualizada exitosamente. Promedio: {vm.Nota.promedio:F2} - Estado: {vm.Nota.estado}";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Notas.AnyAsync(n => n.id_nota == id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var nota = await _context.Notas
                .Include(n => n.Matricula)
                    .ThenInclude(m => m!.Estudiante)
                .Include(n => n.Matricula)
                    .ThenInclude(m => m!.Curso)
                .FirstOrDefaultAsync(n => n.id_nota == id);

            if (nota == null) return NotFound();

            return View(nota);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Nota eliminada exitosamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> ObtenerListaMatriculas(int? selectedId = null)
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .ToListAsync();

            var items = matriculas.Select(m => new
            {
                m.id_matricula,
                Descripcion = $"{m.Estudiante?.nombreEstudiante} - {m.Curso?.nombreCurso} ({m.Curso?.seccion})"
            });

            return new SelectList(items, "id_matricula", "Descripcion", selectedId);
        }
    }
}
