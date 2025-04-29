using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DietProyect_IV.Data;
using DietProyect_IV.Models;
using DietProyect_IV.ViewModels;

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

        // GET: CalculoCalorias/Calculadora
        public IActionResult Calculadora()
        {
            // Cargar los niveles de actividad para el dropdown
            ViewBag.NivelesActividad = _context.Set<NivelActividad>().ToList();

            return View();
        }

        // POST: CalculoCalorias/Calcular
        [HttpPost]
        public IActionResult Calcular(CalculadoraViewModel model)
        {
            if (ModelState.IsValid)
            {
                double tmb = 0;

                if (model.Sexo == "Femenino")
                {
                    tmb = 655 + (9.6 * model.PesoKg) + (1.8 * model.AlturaCm) - (4.7 * model.Edad);
                }
                else // Masculino
                {
                    tmb = 66 + (13.7 * model.PesoKg) + (5 * model.AlturaCm) - (6.8 * model.Edad);
                }

                var nivelActividad = _context.Set<NivelActividad>().Find(model.NivelActividadId);
                double factorActividad = nivelActividad?.FactorActividad ?? 1.2; // Valor por defecto si no se encuentra

                double caloriasDiarias = tmb * factorActividad;

                int ajusteCalorias = 0;
                switch (model.Objetivo)
                {
                    case "Perder peso":
                        ajusteCalorias = -500; // Déficit calórico para perder peso
                        break;
                    case "Ganar músculo":
                        ajusteCalorias = 300; // Superávit calórico para ganar músculo
                        break;
                    case "Mantener peso":
                        ajusteCalorias = 0; // Sin ajuste para mantener peso
                        break;
                }

                double caloriasObjetivo = caloriasDiarias + ajusteCalorias;

                var resultadoViewModel = new ResultadoCaloriasViewModel
                {
                    Nombre = model.Nombre,
                    PesoKg = model.PesoKg,
                    AlturaCm = model.AlturaCm,
                    Edad = model.Edad,
                    Sexo = model.Sexo,
                    NivelActividad = nivelActividad?.Descripcion,
                    Objetivo = model.Objetivo,
                    TMB = Math.Round(tmb, 0),
                    CaloriasDiarias = Math.Round(caloriasDiarias, 0),
                    CaloriasObjetivo = Math.Round(caloriasObjetivo, 0),
                    AjusteCalorias = ajusteCalorias,
                    CalculadoraModel = model
                };

                TempData["ResultadoCaloriasViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(resultadoViewModel);

                return View("Resultado", resultadoViewModel);
            }

            ViewBag.NivelesActividad = _context.Set<NivelActividad>().ToList();
            return View("Calculadora", model);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar()
        {
            var resultadoJson = TempData["ResultadoCaloriasViewModel"] as string;
            if (resultadoJson == null)
            {
                return RedirectToAction("Calculadora");
            }

            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultadoCaloriasViewModel>(resultadoJson);
            var model = resultado.CalculadoraModel;

            Usuario usuario;
            if (!string.IsNullOrEmpty(model.Nombre))
            {
                usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Nombre == model.Nombre);
            }
            else
            {
                model.Nombre = "Usuario " + DateTime.Now.ToString("yyyyMMddHHmmss");
                usuario = null;
            }

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    Nombre = model.Nombre,
                    Edad = model.Edad,
                    PesoKg = model.PesoKg,
                    AlturaCm = model.AlturaCm,
                    Sexo = model.Sexo
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();
            }
            else
            {
                usuario.Edad = model.Edad;
                usuario.PesoKg = model.PesoKg;
                usuario.AlturaCm = model.AlturaCm;
                usuario.Sexo = model.Sexo;

                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }

            var calculoCalorias = new CalculoCalorias
            {
                TasaMetabolicaBasal = resultado.TMB,
                CaloriasDiarias = resultado.CaloriasObjetivo,
                UsuarioId = usuario.UsuarioId,
                NivelActividadId = model.NivelActividadId
            };

            _context.Add(calculoCalorias);
            await _context.SaveChangesAsync();

            var objetivoCalorico = new ObjetivoCalorico
            {
                TipoObjetivo = model.Objetivo,
                AjusteCalorias = resultado.AjusteCalorias,
                CalculoCaloriasId = calculoCalorias.CalculoCaloriasId
            };

            _context.Add(objetivoCalorico);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Cálculo guardado correctamente";
            return RedirectToAction("Details", new { id = calculoCalorias.CalculoCaloriasId });
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
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "Descripcion");
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre");
            return View();
        }

        // POST: CalculoCalorias/Create
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
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "Descripcion", calculoCalorias.NivelActividadId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre", calculoCalorias.UsuarioId);
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
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "Descripcion", calculoCalorias.NivelActividadId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre", calculoCalorias.UsuarioId);
            return View(calculoCalorias);
        }

        // POST: CalculoCalorias/Edit/5
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
            ViewData["NivelActividadId"] = new SelectList(_context.Set<NivelActividad>(), "NivelActividadId", "Descripcion", calculoCalorias.NivelActividadId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "Nombre", calculoCalorias.UsuarioId);
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