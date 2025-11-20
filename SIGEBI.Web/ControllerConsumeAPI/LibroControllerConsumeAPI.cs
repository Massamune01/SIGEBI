using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Web.ViewModels.Libro;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class LibroControllerConsumeAPI : Controller
    {
        // GET: LibroControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllLibroResponse getAllLibroResponse = null;
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync("Libroes/GetAllLibros");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllLibroResponse = JsonSerializer.Deserialize<GetAllLibroResponse>(responseString, options);
                    }
                    else
                    {
                        getAllLibroResponse = new GetAllLibroResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllLibroResponse = new GetAllLibroResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getAllLibroResponse.Data);
        }

        // GET: LibroControllerConsumeAPI/Details/5
        public async Task<IActionResult> Details(Int64 id)
        {
            GetLibroResponse getLibroResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Libroes/GetLibroById?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getLibroResponse = JsonSerializer.Deserialize<GetLibroResponse>(responseString, options);
                    }
                    else
                    {
                        getLibroResponse = new GetLibroResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getLibroResponse = new GetLibroResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getLibroResponse.Data);
        }

        // GET: LibroControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibroControllerConsumeAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibroCreateDto model)
        {
            LibroCreateDto createResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.PostAsJsonAsync("Libroes/create-libro", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = JsonSerializer.Deserialize<LibroCreateDto>(responseString, options);
                        if (createResponse is null)
                        {
                            TempData["ErrorMessage"] = "Libro cannot be created";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Libro successfully created";
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

        // GET: LibroControllerConsumeAPI/Edit/5
        public async Task<IActionResult> Edit(Int64 id)
        {
            GetLibroResponse getLibroResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Libroes/GetLibroById?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getLibroResponse = JsonSerializer.Deserialize<GetLibroResponse>(responseString, options);
                    }
                    else
                    {
                        getLibroResponse = new GetLibroResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getLibroResponse = new GetLibroResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }
            return View(getLibroResponse.Data);
        }

        // POST: LibroControllerConsumeAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LibroUpdateDto model)
        {
            LibroUpdateDto updateResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.PutAsJsonAsync("Libroes/update-libro", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        updateResponse = JsonSerializer.Deserialize<LibroUpdateDto>(responseString, options);
                        if (updateResponse is null)
                        {
                            TempData["ErrorMessage"] = "Libro cannot be update";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Libro successfully updated";
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

        // GET: LibroControllerConsumeAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LibroControllerConsumeAPI/Delete/5
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
