namespace RodrigoRuzaCepTest.Domain.Service.Interface
{
    public interface ICepService
    {
        Task<ApiResult> GetByCepCode(string cepCode);
        Task<ApiResult> GetByUf(string uf);
        Task<ApiResult> GetByLogradouro(string logradouro);
    }
}