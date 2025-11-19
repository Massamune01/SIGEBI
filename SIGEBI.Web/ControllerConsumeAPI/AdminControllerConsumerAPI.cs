using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Web.ViewModels.Admin;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class AdminControllerConsumerAPI : Controller
    {
        // GET: AdminControllerConsumerAPI
        public async Task<IActionResult> Index()
        {
            GetAllAdminsResponse getAllAdminsResponse = null;
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync("Admins/GetAllAdmin");
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
                using(var cliente = new HttpClient()) 
                {                     
                    
                    cliente.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await cliente.GetAsync($"Admins/GetAdminById?id={id}");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");

                    var response = await client.PostAsJsonAsync("Admins/create-admin", model);

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
                using (var cliente = new HttpClient())
                {

                    cliente.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await cliente.GetAsync($"Admins/GetAdminById?id={id}");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");

                    var response = await client.PutAsJsonAsync<AdminUpdateDto>("Admins/UpdateAdmin", model);

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

        // GET: AdminControllerConsumerAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminControllerConsumerAPI/Delete/5
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
