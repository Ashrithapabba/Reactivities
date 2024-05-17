namespace Application.Core
{
    public class AppException
    {
        public AppException(int statucCode, string message, string details = null)
        {
            StatucCode = statucCode;
            Message = message;
            Details = details;
        }

        public int StatucCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}