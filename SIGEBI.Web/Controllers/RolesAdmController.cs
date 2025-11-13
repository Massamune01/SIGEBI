using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    public class RolesAdmController : Controller
    {
        private readonly IRolService _rolService;
        public RolesAdmController(IRolService rolService)
        {
            _rolService = rolService;
        }

        // GET: RolesAdmController
        public async Task<ActionResult> Index()
        {
            ServiceResult result = await _rolService.GetRolAll();
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }
            return View(result.Data);
        }

        // GET: RolesAdmController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ServiceResult result = await _rolService.GetEntityBy(id);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }
            return View(result.Data);
        }

        // GET: RolesAdmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesAdmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RolCreateDto rolCreateDto)
        {
            try
            {
                ServiceResult result = await _rolService.CreateRol(rolCreateDto);
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

        // GET: RolesAdmController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ServiceResult result = await _rolService.GetEntityBy(id);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }
            return View(result.Data);
        }

        // POST: RolesAdmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RolUpdateDto rolUpdateDto)
        {
            try
            {
                ServiceResult result = await _rolService.UpdateRol(rolUpdateDto);
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
