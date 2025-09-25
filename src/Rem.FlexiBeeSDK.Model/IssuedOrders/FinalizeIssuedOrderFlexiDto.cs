using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class FinalizeIssuedOrderFlexiDto
{
    public FinalizeIssuedOrderFlexiDto(int orderId)
    {
        Id = orderId.ToString();
    }

    public FinalizeIssuedOrderFlexiDto(string orderNumber)
    {
         Id = $"code:{orderNumber}";   
    }
    
    [JsonProperty("id")] public string Id { get; private set; }

    [JsonProperty("realizaceObj@type")] public string FinalizeStockMovementType => "skladovy-pohyb";

    [JsonProperty("realizaceObj")]
    public IssuedOrderStockMovementFlexiDto FinalizeStockMovement { get; set; } =  new IssuedOrderStockMovementFlexiDto();
}