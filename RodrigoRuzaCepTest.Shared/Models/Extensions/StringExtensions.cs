namespace RodrigoRuzaCepTest.Shared.Models.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidUF(this string uf)
        {
            if (string.IsNullOrWhiteSpace(uf))
                return false;

            var validUFs = new HashSet<string>
            {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
                "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
                "RS", "RO", "RR", "SC", "SP", "SE", "TO"
            };

            return validUFs.Contains(uf.ToUpper());
        }
    }
}