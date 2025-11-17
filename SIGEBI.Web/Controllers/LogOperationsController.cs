using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Web.Controllers
{
    public class LogOperationsController : Controller
    {
        private readonly ILogOperationsService _logOperationsService;

        public LogOperationsController(ILogOperationsService logOperationsService)
        {
            _logOperationsService = logOperationsService;
        }

        // GET: LogOperations
        public async Task<IActionResult> Index()
        {
            ServiceResult result = await _logOperationsService.GetAllLogOperationsAsync();

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: LogOperations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ServiceResult result = await _logOperationsService.GetLogOperationsByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
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
        public async Task<IActionResult> Create(CreateLogOperationDto createLogOperationDto)
        {
            try
            {
                ServiceResult result = await _logOperationsService.CreateLogOperationsAsync(createLogOperationDto);

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

        // GET: LogOperations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ServiceResult result = await _logOperationsService.GetLogOperationsByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: LogOperations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateLogOperationDto updateLogOperationDto)
        {
            try
            {
                ServiceResult result = await _logOperationsService.UpdateLogOperationsAsync(updateLogOperationDto);

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
