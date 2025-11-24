namespace SIGEBI.Infraestructure.Interfaces
{
    public interface IHttpClientBase
    {
        Task<HttpResponseMessage> Index();
        Task<HttpResponseMessage> Details(int id);
        Task<HttpResponseMessage> Create(object model);
        Task<HttpResponseMessage> Edit(object model);

    }
}
