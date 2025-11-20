using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Web.ViewModels.LogOp;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class LogOpControllerConsumeAPI : Controller
    {
        // GET: LogOpControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllLogOpResponse getAllLogOpResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync("LogOperations/GetAllLogOp");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"LogOperations/GetLogOpById?id={id}");
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
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response =  await client.PostAsJsonAsync("LogOperations/create-LogOp", model);

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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"LogOperations/GetLogOpById?id={id}");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.PutAsJsonAsync("LogOperations/update-LogOp", model);

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

        // GET: LogOpControllerConsumeAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogOpControllerConsumeAPI/Delete/5
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
