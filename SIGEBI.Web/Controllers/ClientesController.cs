using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            ServiceResult result = await _clienteService.GetAllClientesAsync();
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            List<ClienteDto> clienteDtos = _mapper.Map<List<ClienteDto>>(result.Data);

            return View(clienteDtos);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ServiceResult result = await _clienteService.GetClienteByIdAsync(id);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            ClienteDto clienteDto = _mapper.Map<ClienteDto>(result.Data);

            return View(clienteDto);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteCreateDto clienteCreateDto)
        {
            try
            {
                ServiceResult result = await _clienteService.CreateClienteAsync(clienteCreateDto);
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

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ServiceResult result = await _clienteService.GetClienteByIdAsync(id);
            if (!result.Success)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            ClienteDto clienteDto = _mapper.Map<ClienteDto>(result.Data);

            return View(clienteDto);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClienteUpdateDto clienteUpdateDto)
        {
            try
            {
                ServiceResult result = await _clienteService.UpdateClienteAsync(clienteUpdateDto);
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
