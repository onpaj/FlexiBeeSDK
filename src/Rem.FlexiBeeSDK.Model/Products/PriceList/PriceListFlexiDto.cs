using Newtonsoft.Json;

public class PriceListFlexiDto
{
    [JsonProperty("cenaZakl", NullValueHandling = NullValueHandling.Ignore)]
    public double? BasePrice { get; set; }

    [JsonProperty("cenaZaklBezDph", NullValueHandling = NullValueHandling.Ignore)]
    public double? BasePriceWithoutVat { get; set; }

    [JsonProperty("cenaZaklVcDph", NullValueHandling = NullValueHandling.Ignore)]
    public double? BasePriceWithVat { get; set; }

    [JsonProperty("cenJednotka", NullValueHandling = NullValueHandling.Ignore)]
    public int? PriceUnit { get; set; }

    [JsonProperty("hmotMj", NullValueHandling = NullValueHandling.Ignore)]
    public double? Weight { get; set; }

    [JsonProperty("hmotObal", NullValueHandling = NullValueHandling.Ignore)]
    public double? PackagingWeight { get; set; }

    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id => $"code:{ProductCode}";

    [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
    public string ProductCode { get; set; }

    [JsonProperty("kratkyPopis", NullValueHandling = NullValueHandling.Ignore)]
    public string? ShortDescription { get; set; }

    [JsonProperty("nakupCena", NullValueHandling = NullValueHandling.Ignore)]
    public double? PurchasePrice { get; set; }

    [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    [JsonProperty("objem", NullValueHandling = NullValueHandling.Ignore)]
    public double? Volume { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string? Note { get; set; }

    [JsonProperty("typCenyDphK", NullValueHandling = NullValueHandling.Ignore)]
    public string? VatPriceType { get; set; }

    [JsonProperty("typSzbDphK", NullValueHandling = NullValueHandling.Ignore)]
    public string? VatRateType { get; set; }
}