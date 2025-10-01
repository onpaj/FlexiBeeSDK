using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public class StockItemsMovementUpsertRequestEnvelopeFlexiDto
{
    public StockItemsMovementUpsertRequestEnvelopeFlexiDto(StockItemsMovementUpsertRequestFlexiDto stockMovementRequest)
    {
        StockMovement = [stockMovementRequest];
    }
    
    [JsonProperty("skladovy-pohyb", NullValueHandling = NullValueHandling.Ignore)]
    public List<StockItemsMovementUpsertRequestFlexiDto> StockMovement { get; set; }

    [JsonProperty("@version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; }
}