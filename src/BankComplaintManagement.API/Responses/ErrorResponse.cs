namespace BankComplaintManagement.API.Responses
{
    public class ErrorResponse
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; } = null!;

        public object? Errors { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
