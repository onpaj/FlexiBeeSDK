using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Sets;

public class ProductSetsRequest
{
    public ProductSetsRequest(string productCode)
    {
        Filter = $"cenikSada eq \"code:{productCode}\"";
    }
    
    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")] public string Detail { get; set; } = "custom:mnozMj,cenik(nazev,kod,id),cenikSada(nazev,kod,id),poznam,id";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("includes")] public string Includes { get; set; } = "/sady-a-komplety/cenik";

    [JsonProperty("order")] public string Order { get; set; } = "cenik";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";
    
    [JsonProperty("filter")] public string Filter { get; private set; }
}