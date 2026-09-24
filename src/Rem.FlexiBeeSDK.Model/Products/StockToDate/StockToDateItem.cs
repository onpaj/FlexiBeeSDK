using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockToDate;

public class StockToDateItem
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("cenik")]
    public List<ProductFlexiDto> Product { get; set; }

    [JsonProperty("eanKod")]
    public string EanKod { get; set; }

    [JsonProperty("prumCena")]
    public double AveragePrice { get; set; }

    public int? ProductTypeId => ProductItemGroup?.FirstOrDefault()?.Id;

    [JsonProperty("skupZboz")]
    public List<ProductTypeGroup> ProductItemGroup { get; set; } = new();

    [JsonProperty("stavMJ")]
    public double Amount { get; set; }

    [JsonProperty("stavMJPozad")]
    public double AmountRequired { get; set; }

    /// <summary>
    /// Stock value in CZK (<c>tuz</c>), rounded to 2 decimals by FlexiBee.
    /// </summary>
    [JsonProperty("tuz")]
    public double StockValue { get; set; }

    /// <summary>
    /// Exact average price computed as <c>tuz / stavMJ</c>. <c>prumCena</c> from this endpoint is rounded
    /// to 2 decimals, which is materially wrong for per-gram materials. Falls back to <see cref="AveragePrice"/>
    /// when quantity or value is not positive.
    /// </summary>
    [JsonIgnore]
    public double ExactAveragePrice => Amount > 0 && StockValue > 0 ? StockValue / Amount : AveragePrice;
}