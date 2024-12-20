using AutoMapper;
using Newtonsoft.Json;
using RodrigoRuzaCepTest.Domain;
using RodrigoRuzaCepTest.Domain.Entities;
using RodrigoRuzaCepTest.Domain.Repositories;
using RodrigoRuzaCepTest.Domain.Service.Interface;
using RodrigoRuzaCepTest.Shared.HttpHandler.Interface;
using RodrigoRuzaCepTest.Shared.Models;
using RodrigoRuzaCepTest.Shared.Models.Extensions;
using System.Net;

namespace RodrigoRuzaCepTest.Shared.Service
{
    public class CepService : ICepService
    {
        private readonly ICepRepository _cepRepository;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        public CepService(ICepRepository cepRepository, IHttpHandler httpHandler, IMapper mapper)
        {
            _cepRepository = cepRepository;
            _httpHandler = httpHandler;
            _mapper = mapper;
        }

        public async Task<ApiResult> GetByCepCode(string cepCode)
        {
            cepCode = cepCode.Replace("-", "").Replace(" ", "");
            var cep = await _cepRepository.GetByCepCode(cepCode);

            if (cep != null)
                return new ApiResult("", cep, (int)HttpStatusCode.OK);

            HttpResponseMessage response = await _httpHandler.GetByCepCode(cepCode);

            if (response.IsSuccessStatusCode is false)
                return new ApiResult("CEP Inválido, digite novamente.", null, (int)HttpStatusCode.BadRequest);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (jsonResponse.Contains("erro"))
                return new ApiResult("CEP Inválido, digite novamente.", null, (int)HttpStatusCode.BadRequest);

            var cepResponse = JsonConvert.DeserializeObject<CepResponse>(jsonResponse);

            cepResponse.Cep = cepResponse.Cep.Replace("-", "");

            var result = _mapper.Map<Cep>(cepResponse);
            await _cepRepository.Save(result);

            return new ApiResult("", result, (int)HttpStatusCode.OK);
        }

        public async Task<ApiResult> GetByLogradouro(string logradouro)
        {
            var result = await _cepRepository.GetByLogradouro(logradouro);

            if (result == null)
                return new ApiResult("logradouro não encontrado", null, (int)HttpStatusCode.NoContent);

            return new ApiResult("", result, (int)HttpStatusCode.OK);
        }

        public async Task<ApiResult> GetByUf(string uf)
        {
            if (uf.IsValidUF() is false)
                return new ApiResult($"{uf.ToUpper()} não é uma UF válida.", null, (int)HttpStatusCode.BadRequest);

            var result = await _cepRepository.GetByUf(uf.ToUpperInvariant());

            if (result.Any() is false)
                return new ApiResult("Cep não encontrado", null, (int)HttpStatusCode.NoContent);

            return new ApiResult("", result, (int)HttpStatusCode.OK);
        }
    }
}