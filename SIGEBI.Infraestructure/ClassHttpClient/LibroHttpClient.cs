using System.Net.Http.Json;
using SIGEBI.Infraestructure.Interfaces;

namespace SIGEBI.Infraestructure.ClassHttpClient
{
    public class LibroHttpClient : IHttpClientBase
    {
        public readonly HttpClient client;
        private readonly string _baseUrl = "https://localhost:7135/api/";

        public LibroHttpClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<HttpResponseMessage> Create(object model)
        {
            return await client.PostAsJsonAsync("Libroes/create-libro", model);
        }

        public async Task<HttpResponseMessage> Details(int id)
        {
            return await client.GetAsync($"Libroes/GetLibroById?id={id}");
        }

        public async Task<HttpResponseMessage> Details(Int64 id)
        {
            return await client.GetAsync($"Libroes/GetLibroById?id={id}");
        }

        public async Task<HttpResponseMessage> Edit(object model)
        {
            return await client.PutAsJsonAsync("Libroes/update-libro", model);
        }

        public async Task<HttpResponseMessage> Index()
        {
            return await client.GetAsync("Libroes/GetAllLibros");
        }
    }
}
