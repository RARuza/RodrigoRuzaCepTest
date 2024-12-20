using Microsoft.EntityFrameworkCore;
using RodrigoRuzaCepTest.Domain.Entities;
using RodrigoRuzaCepTest.Domain.Repositories;
using RodrigoRuzaCepTest.Infraestructure.Context;

namespace RodrigoRuzaCepTest.Infraestructure.Repositories
{
    public class CepRepository : ICepRepository
    {
        private readonly LocalDbContext _context;
        public CepRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<Cep> GetByCepCode(string cepCode)
        {
            return await _context.Ceps.FirstOrDefaultAsync(c => c.CepCode == cepCode);
        }

        public async Task<Cep> GetByLogradouro(string logradouro)
        {
            return await _context.Ceps.FirstOrDefaultAsync(c => c.Logradouro == logradouro);
        }

        public async Task<List<Cep>> GetByUf(string uf)
        {
            return await _context.Ceps
                .Where(c => c.Uf == uf)
                .ToListAsync();
        }

        public async Task<bool> Save(Cep cep)
        {
            try
            {
                await _context.AddAsync(cep);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}