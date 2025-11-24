using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Infraestructure.ClassHttpClient;
using SIGEBI.Web.ViewModels.Admin;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class AdminControllerConsumerAPI : Controller
    {
        private readonly AdminHttpClient _adminClient = new AdminHttpClient();

        // GET: AdminControllerConsumerAPI
        public async Task<IActionResult> Index()
        {

            GetAllAdminsResponse getAllAdminsResponse = null;
            try
            {
                using(_adminClient.client)
                {
                    var response = await _adminClient.Index();
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllAdminsResponse = JsonSerializer.Deserialize<GetAllAdminsResponse>(responseString, options);
                    }
                    else
                    {
                        getAllAdminsResponse = new GetAllAdminsResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllAdminsResponse = new GetAllAdminsResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getAllAdminsResponse.Data);
        }

        // GET: AdminControllerConsumerAPI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetAdminsResponse getAdminsResponse = null;
            try
            {
                using (_adminClient.client)
                {
                    var response = await _adminClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        getAdminsResponse = JsonSerializer.Deserialize<GetAdminsResponse>(responseString, options);
                    }
                    else
                    {
                        getAdminsResponse = new GetAdminsResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                getAdminsResponse = new GetAdminsResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
                ViewBag.ErrorMessage = getAdminsResponse.Message;
                return View();
            }
            return View(getAdminsResponse.Data);
        }

        // GET: AdminControllerConsumerAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminControllerConsumerAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateDto model)
        {
            AdminCreateDto createResponse = null;
            try
            {
                using (_adminClient.client)
                {
                    var response = await _adminClient.Create(model);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = JsonSerializer.Deserialize<AdminCreateDto>(responseString, options);

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

        // GET: AdminControllerConsumerAPI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetAdminsResponse getAdminsResponse = null;
            try
            {
                using (_adminClient.client)
                {
                    var response = await _adminClient.Details(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAdminsResponse = JsonSerializer.Deserialize<GetAdminsResponse>(responseString, options);
                    }
                    else
                    {
                        getAdminsResponse = new GetAdminsResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAdminsResponse = new GetAdminsResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getAdminsResponse.Data);
        }

        // POST: AdminControllerConsumerAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUpdateDto model)
        {
            AdminUpdateDto editResponse = null;
            try
            {
                using (_adminClient.client)
                {
                    var response = await _adminClient.Edit(model);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        editResponse = JsonSerializer.Deserialize<AdminUpdateDto>(responseString, options);

                        if (editResponse is null)
                        {
                            TempData["ErrorMessage"] = "Admin cannot be update";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Admin successfully updated";
                        }
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
