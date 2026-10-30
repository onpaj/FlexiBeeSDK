using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Attributes;

/// <summary>
/// Row of evidence <c>typ-atributu</c> (attribute definition). Field names validated against
/// <c>/c/{company}/typ-atributu/properties.json</c>.
/// </summary>
public class ProductAttributeTypeFlexiDto
{
    public const string DetailFields = "id,kod,typAtributK";

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string? Code { get; set; }

    /// <summary>Value type key, e.g. <c>typAtribut.boolean</c> or <c>typAtribut.retezec</c>.</summary>
    [JsonProperty("typAtributK")]
    public string? ValueTypeKey { get; set; }
}
