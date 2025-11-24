using System.Net.Http.Json;
using SIGEBI.Infraestructure.Interfaces;

namespace SIGEBI.Infraestructure.ClassHttpClient
{
    public class LogOpHttpClient : IHttpClientBase
    {
        public readonly HttpClient client;
        private readonly string _baseUrl = "https://localhost:7135/api/";

        public LogOpHttpClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<HttpResponseMessage> Create(object model)
        {
            return await client.PostAsJsonAsync("LogOperations/create-LogOp", model);
        }

        public async Task<HttpResponseMessage> Details(int id)
        {
            return await client.GetAsync($"LogOperations/GetLogOpById?id={id}");
        }

        public async Task<HttpResponseMessage> Edit(object model)
        {
            return await client.PutAsJsonAsync("LogOperations/update-LogOp", model);
        }

        public async Task<HttpResponseMessage> Index()
        {
            return await client.GetAsync("LogOperations/GetAllLogOp");
        }
    }
}
