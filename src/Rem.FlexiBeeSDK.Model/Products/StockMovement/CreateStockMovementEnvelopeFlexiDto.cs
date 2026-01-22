using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class CreateStockMovementEnvelopeFlexiDto
{
    public CreateStockMovementEnvelopeFlexiDto(CreateStockMovementFlexiDto stockMovement)
    {
        StockMovements = new List<CreateStockMovementFlexiDto> { stockMovement };
    }

    [JsonProperty("winstrom")]
    public WinstromEnvelope Winstrom { get; set; } = new();

    public class WinstromEnvelope
    {
        [JsonProperty("skladovy-pohyb")]
        public List<CreateStockMovementFlexiDto> StockMovements { get; set; }

        [JsonProperty("@version")]
        public string Version { get; set; } = "1.0";
    }

    [JsonIgnore]
    private List<CreateStockMovementFlexiDto> StockMovements
    {
        set => Winstrom.StockMovements = value;
    }
}
