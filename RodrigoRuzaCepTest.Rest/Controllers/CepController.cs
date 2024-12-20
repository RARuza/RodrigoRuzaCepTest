using Microsoft.AspNetCore.Mvc;
using RodrigoRuzaCepTest.Domain.Service.Interface;

namespace RodrigoRuzaCepTest.Rest.Controllers
{
    [ApiController, Route("cep")]
    public class CepController : ControllerBase
    {
        private readonly ICepService _cepService;

        public CepController(ICepService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet("{cepCode}")]
        public async Task<IActionResult> GetByCepCode(string cepCode)
        {
            var result = await _cepService.GetByCepCode(cepCode);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("uf/{uf}")]
        public async Task<IActionResult> GetByUf(string uf)
        {
            var result = await _cepService.GetByUf(uf);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("logradouro/{logradouro}")]
        public async Task<IActionResult> GetByLogradouro(string logradouro)
        {
            var result = await _cepService.GetByLogradouro(logradouro);

            return StatusCode(result.StatusCode, result);
        }
    }
}