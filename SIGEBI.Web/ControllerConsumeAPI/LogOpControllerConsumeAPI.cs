using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Infraestructure.ClassHttpClient;
using SIGEBI.Web.ViewModels.LogOp;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class LogOpControllerConsumeAPI : Controller
    {
        private readonly LogOpHttpClient _logOpClient = new LogOpHttpClient();

        // GET: LogOpControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllLogOpResponse getAllLogOpResponse = null;
            try
            {
                using (_logOpClient.client)
                {
                    var response = await _logOpClient.Index();
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllLogOpResponse = JsonSerializer.Deserialize<GetAllLogOpResponse>(responseString, options);
                    }
                    else
                    {
                        getAllLogOpResponse = new GetAllLogOpResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch( Exception ex)
            {
                getAllLogOpResponse = new GetAllLogOpResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
                ViewBag.ErrorMessage = getAllLogOpResponse.Message;
                return View();
            }
            return View(getAllLogOpResponse.Data);
        }

        // GET: LogOpControllerConsumeAPI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetLogOpResponse getLogOpResponse = null;
            try
            {
                using (_logOpClient.client)
                {
                    var response = await _logOpClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getLogOpResponse = JsonSerializer.Deserialize<GetLogOpResponse>(responseString, options);
                    }
                    else
                    {
                        getLogOpResponse = new GetLogOpResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getLogOpResponse = new GetLogOpResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
                ViewBag.ErrorMessage = getLogOpResponse.Message;
                return View();
            }
            return View(getLogOpResponse.Data);
        }

        // GET: LogOpControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogOpControllerConsumeAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLogOperationDto model)
        {
            CreateLogOperationDto createResponse = null;
            try
            {
                using (_logOpClient.client)
                {
                    var response = await _logOpClient.Create(model);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = JsonSerializer.Deserialize<CreateLogOperationDto>(responseString, options);

                        if (createResponse is null)
                        {
                            TempData["ErrorMessage"] = "Admin cannot be update";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Admin successfully updated";
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

        // GET: LogOpControllerConsumeAPI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetLogOpResponse getLogOpResponse = null;
            try
            {
                using (_logOpClient.client)
                {
                    var response = await _logOpClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getLogOpResponse = JsonSerializer.Deserialize<GetLogOpResponse>(responseString, options);
                    }
                    else
                    {
                        getLogOpResponse = new GetLogOpResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getLogOpResponse = new GetLogOpResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
                ViewBag.ErrorMessage = getLogOpResponse.Message;
                return View();
            }
            return View(getLogOpResponse.Data);
        }

        // POST: LogOpControllerConsumeAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateLogOperationDto model)
        {
            UpdateLogOperationDto updateResponse = null;
            try
            {
                using (_logOpClient.client)
                {
                    var response = await _logOpClient.Edit(model);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        updateResponse = JsonSerializer.Deserialize<UpdateLogOperationDto>(responseString, options);

                        if (updateResponse is null)
                        {
                            TempData["ErrorMessage"] = "Admin cannot be update";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Admin successfully updated";
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
