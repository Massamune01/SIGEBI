using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Web.Controllers
{
    public class LibroesController : Controller
    {
        private readonly ILibroService _libroService;

        public LibroesController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        // GET: Libroes
        public async Task<IActionResult> Index()
        {
            ServiceResult result = await _libroService.GetAllLibrosAsync();

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: Libroes/Details/5
        public async Task<IActionResult> Details(long id)
        {
            ServiceResult result = await _libroService.GetLibroByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: Libroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibroCreateDto libroCreateDto)
        {
            try
            {
                ServiceResult result = await _libroService.CreateLibroAsync(libroCreateDto);

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

        // GET: Libroes/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            ServiceResult result = await _libroService.GetLibroByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: Libroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LibroUpdateDto libroUpdateDto)
        {
            try
            {
                ServiceResult result = await _libroService.UpdateLibroAsync(libroUpdateDto);

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
