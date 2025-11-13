using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Interfaces;
using SIGEBI.Persistence.Models.Configuration.Credenciales;

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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CredencialesAdmController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CredencialesAdmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CredencialesAdmController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CredencialesAdmController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
