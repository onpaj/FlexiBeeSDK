using System.Linq;
using Rem.FlexiBeeSDK.Model.Response;

public static class OperationResultExtensions
{
    public static string? GetErrorMessage(this OperationResult<OperationResultDetail> result)
    {
        if (result.IsSuccess)
            return null;
        
        return result.ErrorMessage ?? result.Result?.Results?.FirstOrDefault()?.Errors?.FirstOrDefault()?.Message;
    }
}