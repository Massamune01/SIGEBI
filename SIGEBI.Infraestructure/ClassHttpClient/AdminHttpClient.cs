using System.Net.Http;
using System.Net.Http.Json;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Infraestructure.Interfaces;

namespace SIGEBI.Infraestructure.ClassHttpClient
{
    public class AdminHttpClient : IHttpClientBase
    {
        public readonly HttpClient client;
        private readonly string _baseUrl = "https://localhost:7135/api/";

        public AdminHttpClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        public HttpClient httpClient => client;

        public async Task<HttpResponseMessage> Create(object model)
        {
            return await client.PostAsJsonAsync("Admins/create-admin", (AdminCreateDto)model);
        }

        public async Task<HttpResponseMessage> Details(int id)
        {
            return await client.GetAsync($"Admins/GetAdminById?id={id}");
        }

        public async Task<HttpResponseMessage> Edit( object model)
        {
            return await client.PutAsJsonAsync<AdminUpdateDto>("Admins/UpdateAdmin", (AdminUpdateDto)model);
        }

        public async Task<HttpResponseMessage> Index()
        {
            return await client.GetAsync("Admins/GetAllAdmin");
        }
    }
}
