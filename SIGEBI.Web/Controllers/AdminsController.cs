using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Web.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;



        public AdminsController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            ServiceResult result = await _adminService.GetAllAdminAsync();

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            List<AdminDto> adminDtos = _mapper.Map<List<AdminDto>>(result.Data);

            return View(adminDtos);
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ServiceResult result = await _adminService.GetAdminByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            AdminDto adminDto = _mapper.Map<AdminDto>(result.Data);

            return View(adminDto);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateDto adminCreateDto)
        {
            try
            {
                ServiceResult result = await _adminService.CreateAdminAsync(adminCreateDto);

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

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ServiceResult result = await _adminService.GetAdminByIdAsync(id);

            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            AdminDto adminDto = _mapper.Map<AdminDto>(result.Data);

            return View(adminDto);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUpdateDto adminUpdateDto)
        {
            try
            {
                ServiceResult result = await _adminService.UpdateAdminAsync(adminUpdateDto);

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
