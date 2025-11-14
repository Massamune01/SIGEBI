using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Web.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly SIGEBIContext _context;

        public PrestamosController(SIGEBIContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prestamos.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamos = await _context.Prestamos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamos == null)
            {
                return NotFound();
            }

            return View(prestamos);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prestamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatePrest,DateDevol,DateWasDevol,Status,IdLibros,IdCliente,IdLgOpPrest")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamos = await _context.Prestamos.FindAsync(id);
            if (prestamos == null)
            {
                return NotFound();
            }
            return View(prestamos);
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatePrest,DateDevol,DateWasDevol,Status,IdLibros,IdCliente,IdLgOpPrest")] Prestamos prestamos)
        {
            if (id != prestamos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamosExists(prestamos.Id))
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
            return View(prestamos);
        }
        private bool PrestamosExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
