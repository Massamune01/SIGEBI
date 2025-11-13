using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Web.Controllers
{
    public class BibliotecariosController : Controller
    {
        private readonly SIGEBIContext _context;

        public BibliotecariosController(SIGEBIContext context)
        {
            _context = context;
        }

        // GET: Bibliotecarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bibliotecarios.ToListAsync());
        }

        // GET: Bibliotecarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliotecarios = await _context.Bibliotecarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bibliotecarios == null)
            {
                return NotFound();
            }

            return View(bibliotecarios);
        }

        // GET: Bibliotecarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bibliotecarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TotalDevoluciones,TotalHorasTrabajadas,TotalClientesAtendidos,TotalPrestamos,BiblioEstatus,IdLgOpBiblio,Id,Nombre,Apellido,Cedula,Edad,Genero,Email,Nacimiento,RolId")] Bibliotecarios bibliotecarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bibliotecarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bibliotecarios);
        }

        // GET: Bibliotecarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliotecarios = await _context.Bibliotecarios.FindAsync(id);
            if (bibliotecarios == null)
            {
                return NotFound();
            }
            return View(bibliotecarios);
        }

        // POST: Bibliotecarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TotalDevoluciones,TotalHorasTrabajadas,TotalClientesAtendidos,TotalPrestamos,BiblioEstatus,IdLgOpBiblio,Id,Nombre,Apellido,Cedula,Edad,Genero,Email,Nacimiento,RolId")] Bibliotecarios bibliotecarios)
        {
            if (id != bibliotecarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bibliotecarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BibliotecariosExists(bibliotecarios.Id))
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
            return View(bibliotecarios);
        }

        // GET: Bibliotecarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bibliotecarios = await _context.Bibliotecarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bibliotecarios == null)
            {
                return NotFound();
            }

            return View(bibliotecarios);
        }

        // POST: Bibliotecarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bibliotecarios = await _context.Bibliotecarios.FindAsync(id);
            if (bibliotecarios != null)
            {
                _context.Bibliotecarios.Remove(bibliotecarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BibliotecariosExists(int id)
        {
            return _context.Bibliotecarios.Any(e => e.Id == id);
        }
    }
}
