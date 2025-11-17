using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Web.Controllers
{
    public class BibliotecariosController : Controller
    {
        private readonly IBibliotecarioService _bibliotecarioService;

        public BibliotecariosController(IBibliotecarioService bibliotecarioService)
        {
            _bibliotecarioService = bibliotecarioService;
        }

        // GET: Bibliotecarios
        public async Task<IActionResult> Index()
        {
            ServiceResult result = await _bibliotecarioService.GetAllBibliotecariosAsync();

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: Bibliotecarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ServiceResult result = await _bibliotecarioService.GetBibliotecarioByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
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
        public async Task<IActionResult> Create(BibliotecarioCreateDto bibliotecarioCreateDto)
        {
            try
            {
                ServiceResult result = await _bibliotecarioService.CreateBibliotecarioAsync(bibliotecarioCreateDto);

                if (!result.Success)
                {
                    ViewBag.ErrorMessage = result.Message;
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Bibliotecarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ServiceResult result = await _bibliotecarioService.GetBibliotecarioByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: Bibliotecarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BibliotecarioUpdateDto bibliotecarioUpdateDto)
        {
            try
            {
                ServiceResult result = await _bibliotecarioService.UpdateBibliotecarioAsync(bibliotecarioUpdateDto);

                if (!result.Success)
                {
                    ViewBag.ErrorMessage = result.Message;
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
