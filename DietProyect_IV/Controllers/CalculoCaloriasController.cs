using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DietProyect_IV.Data;
using DietProyect_IV.Models;

namespace DietProyect_IV.Controllers
{
    public class CalculoCaloriasController : Controller
    {
        private readonly DietProyect_IVContext _context;

        public CalculoCaloriasController(DietProyect_IVContext context)
        {
            _context = context;
        }

        // GET: CalculoCalorias
        public async Task<IActionResult> Index()
        {
            var dietProyect_IVContext = _context.CalculoCalorias.Include(c => c.NivelActividad).Include(c => c.Usuario);
            return View(await dietProyect_IVContext.ToListAsync());
        }

        // GET: CalculoCalorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calculoCalorias = await _context.CalculoCalorias
                .Include(c => c.NivelActividad)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CalculoCaloriasId == id);
            if (calculoCalorias == null)
            {
                return NotFound();
            }

            return View(calculoCalorias);
        }

        // GET: CalculoCalorias/Create
        public IActionResult Create()
        {
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "NivelActividadId");
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Sexo");
            return View();
        }

        // POST: CalculoCalorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalculoCaloriasId,TasaMetabolicaBasal,CaloriasDiarias,UsuarioId,NivelActividadId")] CalculoCalorias calculoCalorias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calculoCalorias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "NivelActividadId", calculoCalorias.NivelActividadId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Sexo", calculoCalorias.UsuarioId);
            return View(calculoCalorias);
        }

        // GET: CalculoCalorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calculoCalorias = await _context.CalculoCalorias.FindAsync(id);
            if (calculoCalorias == null)
            {
                return NotFound();
            }
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "NivelActividadId", calculoCalorias.NivelActividadId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Sexo", calculoCalorias.UsuarioId);
            return View(calculoCalorias);
        }

        // POST: CalculoCalorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalculoCaloriasId,TasaMetabolicaBasal,CaloriasDiarias,UsuarioId,NivelActividadId")] CalculoCalorias calculoCalorias)
        {
            if (id != calculoCalorias.CalculoCaloriasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calculoCalorias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalculoCaloriasExists(calculoCalorias.CalculoCaloriasId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "NivelActividadId", calculoCalorias.NivelActividadId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Sexo", calculoCalorias.UsuarioId);
            return View(calculoCalorias);
        }

        // GET: CalculoCalorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calculoCalorias = await _context.CalculoCalorias
                .Include(c => c.NivelActividad)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CalculoCaloriasId == id);
            if (calculoCalorias == null)
            {
                return NotFound();
            }

            return View(calculoCalorias);
        }

        // POST: CalculoCalorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calculoCalorias = await _context.CalculoCalorias.FindAsync(id);
            if (calculoCalorias != null)
            {
                _context.CalculoCalorias.Remove(calculoCalorias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalculoCaloriasExists(int id)
        {
            return _context.CalculoCalorias.Any(e => e.CalculoCaloriasId == id);
        }
    }
}
