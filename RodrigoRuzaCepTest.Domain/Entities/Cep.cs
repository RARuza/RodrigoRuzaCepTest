namespace RodrigoRuzaCepTest.Domain.Entities
{
    public class Cep
    {
        public int Id { get; set; }
        public string CepCode { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public long? Unidade { get; set; }
        public int? Ibge { get; set; }
        public string Gia { get; set; }
    }
}