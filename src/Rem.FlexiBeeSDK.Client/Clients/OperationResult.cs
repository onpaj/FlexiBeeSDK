using System.Linq;
using System.Net;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class OperationResult
    {
        public OperationResult(HttpStatusCode statusCode, FlexiResult data)
        {
            StatusCode = statusCode;
            Data = data;

            ErrorMessage = data?.Results?.FirstOrDefault()?.Errors?.FirstOrDefault()?.Message;
        }
        
        public OperationResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        
        public OperationResult(HttpStatusCode statusCode, string error)
        {
            StatusCode = statusCode;
            Data = null;

            ErrorMessage = error;
        }

        public HttpStatusCode StatusCode { get; }
        public FlexiResult? Data { get; }
        public string? ErrorMessage { get; set; }

        public bool IsSuccess => (int) StatusCode < 300 && (int) StatusCode >= 200;

        public ErrorType? ErrorType => Data?.Results?.FirstOrDefault()?.Errors?.FirstOrDefault()?.ErrorType;
    }
}