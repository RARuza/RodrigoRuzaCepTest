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
using System.Text.RegularExpressions;

namespace RodrigoRuzaCepTest.Shared.Service
{
    public class CepService : ICepService
    {
        private readonly ICepRepository _cepRepository;
        private readonly IHttpHandler _httpHandler;
        private readonly IMapper _mapper;

        private const string InvalidCepMessage = "CEP Inválido, digite novamente.";

        public CepService(ICepRepository cepRepository, IHttpHandler httpHandler, IMapper mapper)
        {
            _cepRepository = cepRepository;
            _httpHandler = httpHandler;
            _mapper = mapper;
        }

        public async Task<ApiResult> GetByCepCode(string cepCode)
        {
            cepCode = FormatCep(cepCode);

            if (cepCode == null)
                return new ApiResult(InvalidCepMessage, null, (int)HttpStatusCode.BadRequest);

            Cep cepSavedData = await _cepRepository.GetByCepCode(cepCode);

            if (cepSavedData != null)
                return new ApiResult("", cepSavedData, (int)HttpStatusCode.OK);

            HttpResponseMessage response = await _httpHandler.GetByCepCode(cepCode);

            if (response.IsSuccessStatusCode is false)
                return new ApiResult(InvalidCepMessage, null, (int)HttpStatusCode.BadRequest);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (jsonResponse.Contains("erro"))
                return new ApiResult(InvalidCepMessage, null, (int)HttpStatusCode.BadRequest);

            CepResponse cepResponse = JsonConvert.DeserializeObject<CepResponse>(jsonResponse);
            cepResponse.Cep = FormatCep(cepResponse.Cep);

            Cep result = _mapper.Map<Cep>(cepResponse);

            await _cepRepository.Save(result);

            return new ApiResult("", result, (int)HttpStatusCode.OK);
        }

        public async Task<ApiResult> GetByLogradouro(string logradouro)
        {
            Cep result = await _cepRepository.GetByLogradouro(logradouro);

            if (result == null)
                return new ApiResult("Logradouro não encontrado", null, (int)HttpStatusCode.NoContent);

            return new ApiResult("", result, (int)HttpStatusCode.OK);
        }

        public async Task<ApiResult> GetByUf(string uf)
        {
            if (uf.IsValidUF() is false)
                return new ApiResult($"{uf.ToUpper()} não é uma UF válida.", null, (int)HttpStatusCode.BadRequest);

            List<Cep> result = await _cepRepository.GetByUf(uf.ToUpperInvariant());

            if (result.Any() is false)
                return new ApiResult("Cep não encontrado", null, (int)HttpStatusCode.NoContent);

            return new ApiResult("", result, (int)HttpStatusCode.OK);
        }

        private static string FormatCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return null;

            string cepApenasNumeros = Regex.Replace(cep, @"\D", string.Empty);

            if (cepApenasNumeros.Length != 8)
                return null;

            return cepApenasNumeros;
        }
    }
}