using System.Net.Http.Json;
using SIGEBI.Infraestructure.Interfaces;

namespace SIGEBI.Infraestructure.ClassHttpClient
{
    public class CredeHttpClient : IHttpClientBase
    {
        public readonly HttpClient client;
        private readonly string _baseUrl = "https://localhost:7135/api/";

        public CredeHttpClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<HttpResponseMessage> Create(object model)
        {
            return await client.PostAsJsonAsync("Credenciales/create-credenciales", model);
        }

        public async Task<HttpResponseMessage> Details(int id)
        {
            return await client.GetAsync($"Credenciales/GetEntityByID?id={id}");
        }

        public async Task<HttpResponseMessage> Edit(object model)
        {
            return  await client.PutAsJsonAsync("Credenciales/update-credenciales", model);
        }

        public async Task<HttpResponseMessage> Index()
        {
            return await client.GetAsync("Credenciales/GetCredenciales");
        }
    }
}
