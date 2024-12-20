using RodrigoRuzaCepTest.Domain.Entities;

namespace RodrigoRuzaCepTest.Domain.Repositories
{
    public interface ICepRepository
    {
        Task<Cep> GetByCepCode(string cepCode);
        Task<List<Cep>> GetByUf(string uf);
        Task<bool> Save(Cep cep);
        Task<Cep> GetByLogradouro(string logradouro);
    }
}