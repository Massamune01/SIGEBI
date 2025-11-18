using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    public class BibliotecariosController : Controller
    {
        private readonly IBibliotecarioService _bibliotecarioService;
        private readonly IMapper _mapper;

        public BibliotecariosController(IBibliotecarioService bibliotecarioService, IMapper mapper)
        {
            _bibliotecarioService = bibliotecarioService;
            _mapper = mapper;
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

            List<BibliotecarioDto> bibliotecarioDtos = _mapper.Map<List<BibliotecarioDto>>(result.Data);

            return View(bibliotecarioDtos);
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

            BibliotecarioDto bibliotecarioDto = _mapper.Map<BibliotecarioDto>(result.Data);

            return View(bibliotecarioDto);
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

            BibliotecarioDto bibliotecarioDto = _mapper.Map<BibliotecarioDto>(result.Data);

            return View(bibliotecarioDto);
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
