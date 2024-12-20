using Microsoft.EntityFrameworkCore;
using RodrigoRuzaCepTest.Domain.Entities;
using RodrigoRuzaCepTest.Infraestructure.Configuration;

namespace RodrigoRuzaCepTest.Infraestructure.Context
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        public virtual DbSet<Cep> Ceps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CepConfiguration());
        }
    }
}