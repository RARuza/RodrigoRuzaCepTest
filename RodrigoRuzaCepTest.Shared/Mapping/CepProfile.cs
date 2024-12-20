using AutoMapper;
using RodrigoRuzaCepTest.Domain.Entities;
using RodrigoRuzaCepTest.Shared.Models;

namespace RodrigoRuzaCepTest.Shared.Mapping
{
    public class CepProfile : Profile
    {
        public CepProfile()
        {
            CreateMap<CepResponse, Cep>()
                .ForMember(dest => dest.CepCode, opt => opt.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Bairro))
                .ForMember(dest => dest.Localidade, opt => opt.MapFrom(src => src.Localidade))
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
                .ForMember(dest => dest.Unidade, opt => opt.MapFrom(src => src.Unidade))
                .ForMember(dest => dest.Ibge, opt => opt.MapFrom(src => src.Ibge))
                .ForMember(dest => dest.Gia, opt => opt.MapFrom(src => src.Gia));
        }
    }
}