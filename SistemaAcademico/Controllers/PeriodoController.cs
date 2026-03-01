using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.DAL;
using SistemaAcademico.DAL.Models;

namespace SistemaAcademico.Controllers
{
    public class PeriodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeriodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var periodos = await _context.Periodos.ToListAsync();
            return View(periodos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var periodo = await _context.Periodos
                .FirstOrDefaultAsync(p => p.id_periodo == id);

            if (periodo == null) return NotFound();

            return View(periodo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(periodo);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Periodo creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(periodo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null) return NotFound();

            return View(periodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Periodo periodo)
        {
            if (id != periodo.id_periodo) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(periodo);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Periodo actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Periodos.AnyAsync(p => p.id_periodo == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(periodo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var periodo = await _context.Periodos
                .FirstOrDefaultAsync(p => p.id_periodo == id);

            if (periodo == null) return NotFound();

            return View(periodo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo != null)
            {
                try
                {
                    _context.Periodos.Remove(periodo);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Periodo eliminado exitosamente.";
                }
                catch (DbUpdateException)
                {
                    TempData["Error"] = "No se puede eliminar este periodo porque tiene matrículas asociadas.";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
