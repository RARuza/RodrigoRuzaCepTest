using RodrigoRuzaCepTest.Shared.Models;

namespace RodrigoRuzaCepTest.Shared.HttpHandler.Interface
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> GetByCepCode(string cep);
    }
}