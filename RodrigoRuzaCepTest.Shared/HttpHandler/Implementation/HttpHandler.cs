using RodrigoRuzaCepTest.Shared.HttpHandler.Interface;

namespace RodrigoRuzaCepTest.Shared.HttpHandler.Implementation
{
    public class HttpHandler : IHttpHandler
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> GetByCepCode(string cep)
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao acessar o serviço: {ex.Message}");
            }
        }
    }
}