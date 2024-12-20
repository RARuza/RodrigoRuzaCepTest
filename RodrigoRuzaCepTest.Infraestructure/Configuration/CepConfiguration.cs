using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RodrigoRuzaCepTest.Domain.Entities;

namespace RodrigoRuzaCepTest.Infraestructure.Configuration
{
    public class CepConfiguration : IEntityTypeConfiguration<Cep>
    {
        public void Configure(EntityTypeBuilder<Cep> builder)
        {
            builder.ToTable("CEP");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CepCode)
                   .HasColumnName("cep")
                   .HasMaxLength(9);

            builder.Property(e => e.Logradouro)
                   .HasColumnName("logradouro")
                   .HasMaxLength(500);

            builder.Property(e => e.Complemento)
                   .HasColumnName("complemento")
                   .HasMaxLength(500);

            builder.Property(e => e.Bairro)
                   .HasColumnName("bairro")
                   .HasMaxLength(500);

            builder.Property(e => e.Localidade)
                   .HasColumnName("localidade")
                   .HasMaxLength(500);

            builder.Property(e => e.Uf)
                   .HasColumnName("uf")
                   .HasMaxLength(2);

            builder.Property(e => e.Unidade)
                   .HasColumnName("unidade");

            builder.Property(e => e.Ibge)
                   .HasColumnName("ibge");

            builder.Property(e => e.Gia)
                   .HasColumnName("gia")
                   .HasMaxLength(500);
        }
    }
}