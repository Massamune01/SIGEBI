using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Web.ViewModels.Prestamo;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class PrestamoControllerConsumeAPI : Controller
    {
        // GET: PrestamoControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllPrestamoResponse getAllPrestamoResponse = null;
            try
            {
                using (var client =  new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response =  await client.GetAsync("Prestamos/GetAllPrest");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllPrestamoResponse = JsonSerializer.Deserialize<GetAllPrestamoResponse>(responseString, options);
                    }
                    else
                    {
                        getAllPrestamoResponse = new GetAllPrestamoResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                getAllPrestamoResponse = new GetAllPrestamoResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getAllPrestamoResponse.Data);
        }

        // GET: PrestamoControllerConsumeAPI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetPrestamoResponse getPrestamoResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Prestamos/GetPrestById?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getPrestamoResponse = JsonSerializer.Deserialize<GetPrestamoResponse>(responseString, options);
                    }
                    else
                    {
                        getPrestamoResponse = new GetPrestamoResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getPrestamoResponse = new GetPrestamoResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getPrestamoResponse.Data);
        }

        // GET: PrestamoControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrestamoControllerConsumeAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrestamoCreateDto model)
        {
            PrestamoCreateDto createResponse = null;
            try
            {
                using (var client= new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response =  await client.PostAsJsonAsync("Prestamos/create-prestamos", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };  
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = JsonSerializer.Deserialize<PrestamoCreateDto>(responseString, options);
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
                        ViewBag.ErrorMessage = "Error al consumir la API";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrestamoControllerConsumeAPI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetPrestamoResponse getPrestamoResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Prestamos/GetPrestById?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getPrestamoResponse = JsonSerializer.Deserialize<GetPrestamoResponse>(responseString, options);
                    }
                    else
                    {
                        getPrestamoResponse = new GetPrestamoResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getPrestamoResponse = new GetPrestamoResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getPrestamoResponse.Data);
        }

        // POST: PrestamoControllerConsumeAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PrestamoUpdateDto model)
        {
            PrestamoUpdateDto updateResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.PutAsJsonAsync("Prestamos/update-prestamo", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        updateResponse = JsonSerializer.Deserialize<PrestamoUpdateDto>(responseString, options);
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
                        ViewBag.ErrorMessage = "Error al consumir la API";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrestamoControllerConsumeAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrestamoControllerConsumeAPI/Delete/5
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
