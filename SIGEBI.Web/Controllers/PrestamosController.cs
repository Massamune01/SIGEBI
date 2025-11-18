using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Persistence.Context;
using SIGEBI.Web.Models;

namespace SIGEBI.Web.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly IPrestamosService _prestamoService;
        private readonly IMapper _mapper;

        public PrestamosController(IPrestamosService prestamosService, IMapper mapper)
        {
            _prestamoService = prestamosService;
            _mapper = mapper;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            ServiceResult result = await _prestamoService.GetAllPrestamosAsync();

            if(!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            List<PrestamoDto> prestamos = _mapper.Map<List<PrestamoDto>>(result.Data);

            return View(prestamos);
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ServiceResult result = await _prestamoService.GetPrestamoByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            PrestamoDto prestamoDto = _mapper.Map<PrestamoDto>(result.Data);

            return View(prestamoDto);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {

            var libros = _prestamoService.GetLibroWithTituloAndISBN().Result.Data;

            ViewBag.Libros = libros;

            return View();
        }

        // POST: Prestamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrestamoCreateDto prestamoCreateDto)
        {
            try
            {
                ServiceResult result = await _prestamoService.CreatePrestamoAsync(prestamoCreateDto);

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

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ServiceResult result = await _prestamoService.GetPrestamoByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            PrestamoDto prestamoDto = _mapper.Map<PrestamoDto>(result.Data);

            return View(prestamoDto);
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PrestamoUpdateDto prestamoUpdateDto)
        {
            try
            {
                ServiceResult result = await _prestamoService.UpdatePrestamoAsync(prestamoUpdateDto);

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
