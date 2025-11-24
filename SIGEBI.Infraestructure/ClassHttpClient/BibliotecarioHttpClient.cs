using System.Net.Http.Json;
using SIGEBI.Infraestructure.Interfaces;

namespace SIGEBI.Infraestructure.ClassHttpClient
{
    public class BibliotecarioHttpClient : HttpClientHandler, IHttpClientBase
    {
        public readonly HttpClient client;
        private readonly string _baseUrl = "https://localhost:7135/api/";

        public BibliotecarioHttpClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<HttpResponseMessage> Create(object model)
        {
            return await client.PostAsJsonAsync("Bibliotecarios/create-biblio", model);
        }

        public async Task<HttpResponseMessage> Details(int id)
        {
            return await client.GetAsync($"Bibliotecarios/GetBiblioById?id={id}"); 
        }

        public async Task<HttpResponseMessage> Edit(object model)
        {
            return await client.PutAsJsonAsync("Bibliotecarios/update-Biblio", model);
        }

        public async Task<HttpResponseMessage> Index()
        {
           return await client.GetAsync("Bibliotecarios/GetAllBiblio");
        }
    }
}
