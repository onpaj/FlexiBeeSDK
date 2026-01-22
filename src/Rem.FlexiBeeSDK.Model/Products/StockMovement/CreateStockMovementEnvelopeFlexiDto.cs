using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class CreateStockMovementEnvelopeFlexiDto
{
    public CreateStockMovementEnvelopeFlexiDto(CreateStockMovementFlexiDto stockMovement)
    {
        StockMovements = new List<CreateStockMovementFlexiDto> { stockMovement };
    }

    [JsonProperty("skladovy-pohyb", NullValueHandling = NullValueHandling.Ignore)]
    public List<CreateStockMovementFlexiDto> StockMovements { get; set; }

    [JsonProperty("@version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; } = "1.0";
}
