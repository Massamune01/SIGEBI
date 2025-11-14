using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Web.Controllers
{
    public class LogOperationsController : Controller
    {
        private readonly SIGEBIContext _context;

        public LogOperationsController(SIGEBIContext context)
        {
            _context = context;
        }

        // GET: LogOperations
        public async Task<IActionResult> Index()
        {
            return View(await _context.LogOperations.ToListAsync());
        }

        // GET: LogOperations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logOperations = await _context.LogOperations
                .FirstOrDefaultAsync(m => m.IdOp == id);
            if (logOperations == null)
            {
                return NotFound();
            }

            return View(logOperations);
        }

        // GET: LogOperations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogOperations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOp,Entity,Fecha,TypeOperation,Descripcion,LastUpdateBy,UpdateBy,StatusOp")] LogOperations logOperations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logOperations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logOperations);
        }

        // GET: LogOperations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logOperations = await _context.LogOperations.FindAsync(id);
            if (logOperations == null)
            {
                return NotFound();
            }
            return View(logOperations);
        }

        // POST: LogOperations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOp,Entity,Fecha,TypeOperation,Descripcion,LastUpdateBy,UpdateBy,StatusOp")] LogOperations logOperations)
        {
            if (id != logOperations.IdOp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logOperations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogOperationsExists(logOperations.IdOp))
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
            return View(logOperations);
        }

        // GET: LogOperations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logOperations = await _context.LogOperations
                .FirstOrDefaultAsync(m => m.IdOp == id);
            if (logOperations == null)
            {
                return NotFound();
            }

            return View(logOperations);
        }

        // POST: LogOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logOperations = await _context.LogOperations.FindAsync(id);
            if (logOperations != null)
            {
                _context.LogOperations.Remove(logOperations);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogOperationsExists(int id)
        {
            return _context.LogOperations.Any(e => e.IdOp == id);
        }
    }
}
