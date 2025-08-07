namespace MediAgenda.Responses
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }

        public ApiResponse() { }
        public ApiResponse(string message, bool success, T? data)
        {
            Message = message;
            Success = success;
            Data = data;
        }
    }
}
