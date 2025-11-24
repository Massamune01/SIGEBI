using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Infraestructure.ClassHttpClient;
using SIGEBI.Web.ViewModels.Crede;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class CredeControllerConsumeAPI : Controller
    {
        private readonly CredeHttpClient _credeClient = new CredeHttpClient();

        // GET: CredeControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllCredeResponse getAllCredeResponse = null;
            try
            {
                using (_credeClient.client)
                {
                    var response = await _credeClient.Index();
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllCredeResponse = JsonSerializer.Deserialize<GetAllCredeResponse>(responseString, options);
                    }
                    else
                    {
                        getAllCredeResponse = new GetAllCredeResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                getAllCredeResponse = new GetAllCredeResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getAllCredeResponse.Data);
        }

        // GET: CredeControllerConsumeAPI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetCredeResponse getCredeResponse = null;
            try
            {
                using (_credeClient.client)
                {
                    var response = await _credeClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getCredeResponse = JsonSerializer.Deserialize<GetCredeResponse>(responseString, options);
                    }
                    else
                    {
                        getCredeResponse = new GetCredeResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                getCredeResponse = new GetCredeResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getCredeResponse.Data);
        }

        // GET: CredeControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CredeControllerConsumeAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CredencialesCreateDto model)
        {
            CredencialesCreateDto createResponse = null;
            try
            {
                using (_credeClient.client)
                {
                    var response = await _credeClient.Create(model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = JsonSerializer.Deserialize<CredencialesCreateDto>(responseString, options);
                        if(createResponse != null)
                        {
                            TempData["SuccessMessage"] = "Credenciales created successfully";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Credenciales cannot be created";
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

        // GET: CredeControllerConsumeAPI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetCredeResponse getCredeResponse = null;
            try
            {
                using (_credeClient.client)
                {
                    var response = await _credeClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getCredeResponse = JsonSerializer.Deserialize<GetCredeResponse>(responseString, options);
                    }
                    else
                    {
                        getCredeResponse = new GetCredeResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                getCredeResponse = new GetCredeResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getCredeResponse.Data);
        }

        // POST: CredeControllerConsumeAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CredencialesUpdateDto model)
        {
            CredencialesUpdateDto updateResponse = null;
            try
            {
                using (_credeClient.client)
                {
                    var response = await _credeClient.Edit(model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        updateResponse = JsonSerializer.Deserialize<CredencialesUpdateDto>(responseString, options);
                        if (updateResponse != null)
                        {
                            TempData["SuccessMessage"] = "Credenciales updated successfully";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Credenciales cannot be updated";
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
