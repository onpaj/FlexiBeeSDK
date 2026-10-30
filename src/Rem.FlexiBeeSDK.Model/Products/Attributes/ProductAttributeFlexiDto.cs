using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Attributes;

/// <summary>
/// Row of evidence <c>atribut</c> attached to a product. Field names validated against
/// <c>/c/{company}/atribut/properties.json</c>.
/// </summary>
public class ProductAttributeFlexiDto
{
    public const string DetailFields = "id,valBoolean,valString,valInteger,valNumeric,hodnota,cenik,typAtributu";

    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>Reference to the product, e.g. <c>code:ABC001</c>.</summary>
    [JsonProperty("cenik")]
    public string? Product { get; set; }

    /// <summary>Reference to the attribute definition, e.g. <c>code:TOPPRODUKT</c>.</summary>
    [JsonProperty("typAtributu")]
    public string? AttributeType { get; set; }

    [JsonProperty("valBoolean")]
    public bool? BooleanValue { get; set; }

    [JsonProperty("valString")]
    public string? StringValue { get; set; }

    [JsonProperty("valInteger")]
    public int? IntegerValue { get; set; }

    [JsonProperty("valNumeric")]
    public decimal? NumericValue { get; set; }

    /// <summary>FlexiBee's computed textual form of the value (<c>hodnota</c>), e.g. <c>true</c>.</summary>
    [JsonProperty("hodnota")]
    public string? DisplayValue { get; set; }
}
