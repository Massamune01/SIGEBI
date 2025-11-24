using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Infraestructure.ClassHttpClient;
using SIGEBI.Web.ViewModels.Cliente;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class ClienteControllerConsumeAPI : Controller
    {
        ClienteHttpClient _clienteClient = new ClienteHttpClient();

        // GET: ClienteControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllClienteResponse getAllClienteResponse = null;
            try
            {
                using(_clienteClient.client)
                {
                    var response = await _clienteClient.Index();
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllClienteResponse = JsonSerializer.Deserialize<GetAllClienteResponse>(responseString, options);
                    }
                    else
                    {
                        getAllClienteResponse = new GetAllClienteResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                getAllClienteResponse = new GetAllClienteResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getAllClienteResponse.Data);
        }

        // GET: ClienteControllerConsumeAPI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetClienteResponse getClienteResponse = null;
            try
            {
                using (_clienteClient.client)
                {
                    var response = await _clienteClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getClienteResponse = JsonSerializer.Deserialize<GetClienteResponse>(responseString, options);
                    }
                    else
                    {
                        getClienteResponse = new GetClienteResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                getClienteResponse = new GetClienteResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getClienteResponse.Data);
        }

        // GET: ClienteControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteControllerConsumeAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClienteCreateDto model)
        {
            ClienteCreateDto createResponse = null;
            try
            {
                using (_clienteClient.client)
                {
                    var response = await _clienteClient.Create(model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = JsonSerializer.Deserialize<ClienteCreateDto>(responseString, options);
                        if (createResponse is null)
                        {
                            TempData["ErrorMessage"] = "Cliente cannot be created";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Cliente successfully updated";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Error al consumir la API";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteControllerConsumeAPI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetClienteResponse getClienteResponse = null;
            try
            {
                using (_clienteClient.client)
                {
                    var response = await _clienteClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getClienteResponse = JsonSerializer.Deserialize<GetClienteResponse>(responseString, options);
                    }
                    else
                    {
                        getClienteResponse = new GetClienteResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                getClienteResponse = new GetClienteResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getClienteResponse.Data);
        }

        // POST: ClienteControllerConsumeAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClienteUpdateDto model)
        {
            ClienteUpdateDto updateResponse = null;
            try
            {
                using (_clienteClient.client)
                {
                    var response = await _clienteClient.Edit(model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        updateResponse = JsonSerializer.Deserialize<ClienteUpdateDto>(responseString, options);
                        if (updateResponse is null)
                        {
                            TempData["ErrorMessage"] = "Cliente cannot be update";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Cliente successfully updated";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Error al consumir la API";
                    }
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
