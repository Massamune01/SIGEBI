using System.Net.Http.Json;
using SIGEBI.Infraestructure.Interfaces;

namespace SIGEBI.Infraestructure.ClassHttpClient
{
    public class ClienteHttpClient : IHttpClientBase
    {
        public readonly HttpClient client;
        private readonly string _baseUrl = "https://localhost:7135/api/";

        public ClienteHttpClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<HttpResponseMessage> Create(object model)
        {
            return await client.PostAsJsonAsync("Clientes/create-cliente", model);
        }

        public async Task<HttpResponseMessage> Details(int id)
        {
            return await client.GetAsync($"Clientes/GetClienteById?id={id}");
        }

        public async Task<HttpResponseMessage> Edit(object model)
        {
            return await client.PutAsJsonAsync("Clientes/update-cliente", model);
        }

        public async Task<HttpResponseMessage> Index()
        {
            return await client.GetAsync("Clientes/GetAllClientes");
        }
    }
}
