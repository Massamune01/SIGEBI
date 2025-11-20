using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Web.ViewModels.Roles;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class RolControllerConsumeAPI : Controller
    {
        // GET: RolControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllRolesResponse getAllRolesResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync("Rol/GetRoles");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllRolesResponse = JsonSerializer.Deserialize<GetAllRolesResponse>(responseString, options);
                    }
                    else
                    {
                        getAllRolesResponse = new GetAllRolesResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllRolesResponse = new GetAllRolesResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getAllRolesResponse.Data);
        }

        // GET: RolControllerConsumeAPI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetRolesResponse getRolesResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Rol/GetEntityByID?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var responseString = await response.Content.ReadAsStringAsync();
                        getRolesResponse = JsonSerializer.Deserialize<GetRolesResponse>(responseString, options);
                    }
                    else
                    {
                        getRolesResponse = new GetRolesResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getRolesResponse = new GetRolesResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getRolesResponse.Data);
        }

        // GET: RolControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolControllerConsumeAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolCreateDto model)
        {
            RolCreateDto createResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");

                    var response = await client.PostAsJsonAsync("Rol/create-rol", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = JsonSerializer.Deserialize<RolCreateDto>(responseString, options);

                        if (createResponse is null)
                        {
                            TempData["ErrorMessage"] = "Admin cannot be created";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Admin successfully created";
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

        // GET: RolControllerConsumeAPI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetRolesResponse getRolesResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Rol/GetEntityByID?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var responseString = await response.Content.ReadAsStringAsync();
                        getRolesResponse = JsonSerializer.Deserialize<GetRolesResponse>(responseString, options);
                    }
                    else
                    {
                        getRolesResponse = new GetRolesResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getRolesResponse = new GetRolesResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getRolesResponse.Data);
        }

        // POST: RolControllerConsumeAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RolUpdateDto model)
        {
            RolUpdateDto updateResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");

                    var response = await client.PutAsJsonAsync("Rol/update-rol", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        updateResponse = JsonSerializer.Deserialize<RolUpdateDto>(responseString, options);

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

        // GET: RolControllerConsumeAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolControllerConsumeAPI/Delete/5
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
