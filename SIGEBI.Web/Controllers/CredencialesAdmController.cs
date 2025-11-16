using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    public class CredencialesAdmController : Controller
    {
        public readonly ICredencialesService _credencialesService;

        public CredencialesAdmController(ICredencialesService credencialesService)
        {
            _credencialesService = credencialesService;
        }

        // GET: CredencialesAdmController
        public async Task<ActionResult> Index()
        {
            ServiceResult result = await _credencialesService.GetCredencialesAll();

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CredencialesAdmController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ServiceResult result = await _credencialesService.GetCredencialesById(id);

            CredencialesGetModel credencial = result.Data;

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(credencial);
        }

        // GET: CredencialesAdmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CredencialesAdmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CredencialesCreateDto credencialesCreateDto)
        {
            try
            {
                ServiceResult result = await _credencialesService.CreateCredenciales(credencialesCreateDto);

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

        // GET: CredencialesAdmController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ServiceResult result = await _credencialesService.GetCredencialesById(id);

            CredencialesGetModel credencial = result.Data;

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return View(credencial);
        }

        // POST: CredencialesAdmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CredencialesUpdateDto credencialesUpdateDto)
        {
            try
            {
                ServiceResult result = await _credencialesService.UpdateCredenciales(credencialesUpdateDto);

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
