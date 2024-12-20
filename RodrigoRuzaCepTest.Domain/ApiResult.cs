namespace RodrigoRuzaCepTest.Domain
{
    public class ApiResult
    {
        public ApiResult(string message, object data, int statusCode)
        {
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public string Message { get; }
        public object Data { get; }
        public int StatusCode { get; }
    }
}