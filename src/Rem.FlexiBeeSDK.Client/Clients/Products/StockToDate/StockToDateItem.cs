using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;

public class StockToDateItem
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("cenik")]
    public List<Product> Product { get; set; }

    [JsonProperty("eanKod")]
    public string EanKod { get; set; }

    [JsonProperty("prumCena")]
    public double AveragePrice { get; set; }

    public int ProductTypeId => ProductItemGroup.First().Id;
    
    [JsonProperty("skupZboz")]
    public List<Product> ProductItemGroup { get; set; }

    [JsonProperty("stavMJ")]
    public double Amount { get; set; }

    [JsonProperty("stavMJPozad")]
    public double AmountRequired { get; set; }
}