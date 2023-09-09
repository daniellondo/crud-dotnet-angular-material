namespace Domain.Dtos
{
    public class BaseResponse<TResult>
    {
        public BaseResponse(string message, TResult result, object? error = null)
        {
            Message = message;
            Result = result;
            Error = error;
        }
        public string Message { get; set; }
        public TResult Result { get; set; }
        public object? Error { get; set; }
    }
}
