using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockItemDocumentFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("typDokl")]
    public string DocumentTypeCode { get; set; }

    [JsonProperty("typDokl@internalId")]
    public int DocumentTypeId { get; set; }

    [JsonProperty("typDokl@showAs")]
    public string DocumentTypeName { get; set; }

    [JsonProperty("kod")]
    public string DocumentCode { get; set; }
    
    [JsonProperty("typPohybuK")]
    public string MovementDirectionCode { get; set; }

    [JsonProperty("typPohybuK@showAs")]
    public string MovementDirectionName { get; set; }
}