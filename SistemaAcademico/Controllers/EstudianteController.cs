using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.DAL;
using SistemaAcademico.DAL.Models;

namespace SistemaAcademico.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudianteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Estudiante
        public async Task<IActionResult> Index()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            return View(estudiantes);
        }

        // GET: Estudiante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.id_estudiante == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // GET: Estudiante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Estudiante creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // POST: Estudiante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Estudiante estudiante)
        {
            if (id != estudiante.id_estudiante) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Estudiante actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Estudiantes.AnyAsync(e => e.id_estudiante == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.id_estudiante == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Estudiante eliminado exitosamente.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
