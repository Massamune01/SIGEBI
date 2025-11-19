using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Web.ViewModels.Biblio;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class BibliotecarioControllerConsumeAPI : Controller
    {
        // GET: BibliotecarioControllerConsumeAPI
        public async Task<IActionResult> Index()
        {
            GetAllBiblioResponse getAllBiblioResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync("Bibliotecarios/GetAllBiblio");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllBiblioResponse = JsonSerializer.Deserialize<GetAllBiblioResponse>(responseString, options);
                    }
                    else
                    {
                        getAllBiblioResponse = new GetAllBiblioResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllBiblioResponse = new GetAllBiblioResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }

            return View(getAllBiblioResponse.Data);
        }

        // GET: BibliotecarioControllerConsumeAPI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetBiblioResponse getBiblioResponse = null;
            try
            {
                using(var client = new HttpClient()) 
                {

                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Bibliotecarios/GetBiblioById?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        getBiblioResponse = JsonSerializer.Deserialize<GetBiblioResponse>(responseString, options);
                    }
                    else
                    {
                        getBiblioResponse = new GetBiblioResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                getBiblioResponse = new GetBiblioResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
            }

            return View(getBiblioResponse.Data);
        }

        // GET: BibliotecarioControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BibliotecarioControllerConsumeAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BibliotecarioCreateDto model)
        {
            BibliotecarioCreateDto createResponse = null;
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response =  await client.PostAsJsonAsync("Bibliotecarios/create-biblio", model);
                    if(response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        createResponse = JsonSerializer.Deserialize<BibliotecarioCreateDto>(responseString, options);
                        if (createResponse is null)
                        {
                            TempData["ErrorMessage"] = "Bibliotecario cannot be update";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Bibliotecario successfully updated";
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

        // GET: BibliotecarioControllerConsumeAPI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetBiblioResponse getBiblioResponse = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.GetAsync($"Bibliotecarios/GetBiblioById?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        getBiblioResponse = JsonSerializer.Deserialize<GetBiblioResponse>(responseString, options);
                    }
                    else
                    {
                        getBiblioResponse = new GetBiblioResponse
                        {
                            Success = false,
                            Message = "Error al consumir la API"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getBiblioResponse = new GetBiblioResponse
                {
                    Success = false,
                    Message = $"Error al consumir la API {ex.Message}"
                };
                ViewBag.ErrorMessage = getBiblioResponse.Message;
                return View();
            }

            return View(getBiblioResponse.Data);
        }

        // POST: BibliotecarioControllerConsumeAPI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BibliotecarioUpdateDto model)
        {
            BibliotecarioUpdateDto updateResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7135/api/");
                    var response = await client.PutAsJsonAsync("Bibliotecarios/update-Biblio", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        updateResponse = JsonSerializer.Deserialize<BibliotecarioUpdateDto>(responseString, options);
                        if (updateResponse is null)
                        {
                            TempData["ErrorMessage"] = "Bibliotecario cannot be update";
                        }
                        else
                        {
                            TempData["SuccessMessage"] = "Bibliotecario successfully updated";
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

        // GET: BibliotecarioControllerConsumeAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BibliotecarioControllerConsumeAPI/Delete/5
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
