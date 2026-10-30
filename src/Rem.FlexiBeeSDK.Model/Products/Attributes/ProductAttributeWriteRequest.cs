using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Attributes;

/// <summary>
/// Payload for creating, updating or deleting one <c>atribut</c> row. Only populated fields are serialized.
/// </summary>
public class ProductAttributeWriteRequest
{
    public const string DeleteAction = "delete";

    private ProductAttributeWriteRequest()
    {
    }

    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public int? Id { get; private set; }

    [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
    public string? Product { get; private set; }

    [JsonProperty("typAtributu", NullValueHandling = NullValueHandling.Ignore)]
    public int? AttributeTypeId { get; private set; }

    [JsonProperty("valBoolean", NullValueHandling = NullValueHandling.Ignore)]
    public bool? BooleanValue { get; private set; }

    [JsonProperty("valString", NullValueHandling = NullValueHandling.Ignore)]
    public string? StringValue { get; private set; }

    [JsonProperty("valInteger", NullValueHandling = NullValueHandling.Ignore)]
    public int? IntegerValue { get; private set; }

    [JsonProperty("valNumeric", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NumericValue { get; private set; }

    [JsonProperty("@action", NullValueHandling = NullValueHandling.Ignore)]
    public string? Action { get; private set; }

    public static ProductAttributeWriteRequest Create(ProductReference product, int attributeTypeId, ProductAttributeValue value) =>
        WithValue(new ProductAttributeWriteRequest
        {
            Product = product.ToRequestValue(),
            AttributeTypeId = attributeTypeId,
        }, value);

    public static ProductAttributeWriteRequest Update(int id, ProductAttributeValue value) =>
        WithValue(new ProductAttributeWriteRequest { Id = id }, value);

    public static ProductAttributeWriteRequest Delete(int id) =>
        new() { Id = id, Action = DeleteAction };

    private static ProductAttributeWriteRequest WithValue(ProductAttributeWriteRequest request, ProductAttributeValue value)
    {
        request.BooleanValue = value.AsBoolean();
        request.StringValue = value.AsString();
        request.IntegerValue = value.AsInteger();
        request.NumericValue = value.AsNumeric();
        return request;
    }
}
