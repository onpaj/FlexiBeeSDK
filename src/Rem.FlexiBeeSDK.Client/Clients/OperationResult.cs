using System.Net;

namespace Rem.FlexiBeeSDK.Client.Clients
{
    public class OperationResult
    {
        public OperationResult(HttpStatusCode statusCode, string data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public HttpStatusCode StatusCode { get; }
        public string Data { get; }

        public bool IsSuccess => (int) StatusCode < 300 && (int) StatusCode >= 200;
    }
}