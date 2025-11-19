using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Web.ViewModels.Cliente;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class ClienteControllerConsumeAPI : Controller
    {
        // GET: ClienteControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllClienteResponse getAllClienteResponse = null;
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync("Clientes/GetAllClientes");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Clientes/GetClienteById?id={id}");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response  = await client.PostAsJsonAsync("Clientes/create-cliente", model);
                    if(response.IsSuccessStatusCode)
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Clientes/GetClienteById?id={id}");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.PutAsJsonAsync("Clientes/update-cliente", model);
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

        // GET: ClienteControllerConsumeAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteControllerConsumeAPI/Delete/5
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
