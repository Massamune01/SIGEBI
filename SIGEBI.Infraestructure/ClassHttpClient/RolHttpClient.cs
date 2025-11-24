using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Infraestructure.Interfaces;

namespace SIGEBI.Infraestructure.ClassHttpClient
{
    public class RolHttpClient : IHttpClientBase
    {
        public readonly HttpClient client;
        private readonly string _baseUrl = "https://localhost:7135/api/";

        public RolHttpClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<HttpResponseMessage> Create(object model)
        {
            return await client.PostAsJsonAsync("Rol/create-rol", model);
        }

        public async Task<HttpResponseMessage> Details(int id)
        {
            return await client.GetAsync($"Rol/GetEntityByID?id={id}");
        }

        public async Task<HttpResponseMessage> Edit(object model)
        {
            return await client.PutAsJsonAsync("Rol/update-rol", model);
        }

        public async Task<HttpResponseMessage> Index()
        {
            return await client.GetAsync("Rol/GetRoles");
        }
    }
}
