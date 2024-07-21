using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;

public class StockToDateItem
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("cenik@internalId")]
    public int InternalId { get; set; }

    [JsonProperty("cenik")]
    public List<Product> Product { get; set; }

    [JsonProperty("eanKod")]
    public string EanKod { get; set; }

    [JsonProperty("prumCena")]
    public double AveragePrice { get; set; }

    [JsonProperty("skupZboz@internalId")]
    public int ProductTypeId { get; set; }

    [JsonProperty("stavMJ")]
    public double Amount { get; set; }

    [JsonProperty("stavMJPozad")]
    public double AmountRequired { get; set; }
}