using System.Net;

namespace Rem.FlexiBeeSDK.Model.Response
{
    public class OperationResult<TData>
    {
        public OperationResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        
        public OperationResult(HttpStatusCode statusCode, string error)
        {
            StatusCode = statusCode;
            ErrorMessage = error;
        }

        public OperationResult(HttpStatusCode statusCode, TData result) : this(statusCode)
        {
            Result = result;
        }

        public TData? Result { get; }

        public HttpStatusCode StatusCode { get; }
        public string? ErrorMessage { get; set; }

        public bool IsSuccess => (int) StatusCode < 300 && (int) StatusCode >= 200;
    }
}