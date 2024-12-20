using System.Net;
using AutoMapper;
using Bogus;
using FakeItEasy;
using FluentAssertions;
using Newtonsoft.Json;
using RodrigoRuzaCepTest.Domain.Entities;
using RodrigoRuzaCepTest.Domain.Repositories;
using RodrigoRuzaCepTest.Domain.Service.Interface;
using RodrigoRuzaCepTest.Shared.HttpHandler.Interface;
using RodrigoRuzaCepTest.Shared.Service;
using System.Text;

namespace RodrigoRuzaCepTest.UnitTests.Service
{
    [TestFixture]
    public class CepServiceTests
    {
        private ICepRepository _cepRepository;
        private IHttpHandler _httpHandler;
        private IMapper _mapper;
        private ICepService _cepService;

        [SetUp]
        public void SetUp()
        {
            _cepRepository = A.Fake<ICepRepository>();
            _httpHandler = A.Fake<IHttpHandler>();
            _mapper = A.Fake<IMapper>();
            _cepService = new CepService(_cepRepository, _httpHandler, _mapper);
        }

        [Test]
        public async Task GetByCepCode_Should_Return_CepData_If_Exists_In_Repository()
        {
            // Arrange
            var faker = new Faker();
            var fakeCep = faker.Random.Replace("########");
            var fakeCepEntity = new Cep { CepCode = fakeCep };

            A.CallTo(() => _cepRepository.GetByCepCode(fakeCep)).Returns(Task.FromResult(fakeCepEntity));

            // Act
            var result = await _cepService.GetByCepCode(fakeCep);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().Be(fakeCepEntity);
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task GetByCepCode_Should_Return_Error_If_Cep_Invalid()
        {
            // Arrange
            var invalidCep = "123"; // Less than 8 characters

            // Act
            var result = await _cepService.GetByCepCode(invalidCep);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("CEP Inválido, digite novamente.");
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetByCepCode_Should_Call_HttpHandler_If_Cep_Not_In_Repository()
        {
            // Arrange
            var faker = new Faker();
            var fakeCep = faker.Random.Replace("########");
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { cep = fakeCep }), Encoding.UTF8, "application/json")
            };

            A.CallTo(() => _cepRepository.GetByCepCode(fakeCep)).Returns(Task.FromResult((Cep)null));
            A.CallTo(() => _httpHandler.GetByCepCode(fakeCep)).Returns(Task.FromResult(fakeResponse));

            // Act
            var result = await _cepService.GetByCepCode(fakeCep);

            // Assert
            A.CallTo(() => _httpHandler.GetByCepCode(fakeCep)).MustHaveHappened();
            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task GetByLogradouro_Should_Return_Data_If_Found()
        {
            // Arrange
            var faker = new Faker();
            var fakeLogradouro = faker.Address.StreetName();
            var fakeCepEntity = new Cep { Logradouro = fakeLogradouro };

            A.CallTo(() => _cepRepository.GetByLogradouro(fakeLogradouro)).Returns(Task.FromResult(fakeCepEntity));

            // Act
            var result = await _cepService.GetByLogradouro(fakeLogradouro);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().Be(fakeCepEntity);
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task GetByUf_Should_Return_Error_If_Uf_Invalid()
        {
            // Arrange
            var invalidUf = "ZZ"; // Invalid UF

            // Act
            var result = await _cepService.GetByUf(invalidUf);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("ZZ não é uma UF válida.");
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetByUf_Should_Return_Data_If_Found()
        {
            // Arrange
            var fakeUf = "SP";
            var fakeCepList = new List<Cep> { new Cep { Uf = fakeUf } };

            A.CallTo(() => _cepRepository.GetByUf(fakeUf)).Returns(Task.FromResult(fakeCepList));

            // Act
            var result = await _cepService.GetByUf(fakeUf);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(fakeCepList);
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }

}