using System;
using System.Globalization;

namespace Rem.FlexiBeeSDK.Model.Products.Attributes;

/// <summary>
/// Value kinds an attribute definition (<c>typ-atributu.typAtributK</c>) can have. Date is not supported yet.
/// </summary>
public enum ProductAttributeValueKind
{
    Boolean,
    String,
    Integer,
    Numeric,
}

/// <summary>
/// A typed, immutable value to store in <c>atribut</c>. Each kind maps to exactly one writable field
/// (<c>valBoolean</c>, <c>valString</c>, <c>valInteger</c>, <c>valNumeric</c>).
/// </summary>
public sealed class ProductAttributeValue
{
    private readonly object _value;

    private ProductAttributeValue(ProductAttributeValueKind kind, object value)
    {
        Kind = kind;
        _value = value;
    }

    public ProductAttributeValueKind Kind { get; }

    public static ProductAttributeValue FromBoolean(bool value) => new(ProductAttributeValueKind.Boolean, value);

    public static ProductAttributeValue FromString(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        return new ProductAttributeValue(ProductAttributeValueKind.String, value);
    }

    public static ProductAttributeValue FromInteger(int value) => new(ProductAttributeValueKind.Integer, value);

    public static ProductAttributeValue FromNumeric(decimal value) => new(ProductAttributeValueKind.Numeric, value);

    /// <summary>The <c>typAtributK</c> enum key an attribute definition must have to accept this value.</summary>
    public string RequiredTypeKey => Kind switch
    {
        ProductAttributeValueKind.Boolean => "typAtribut.boolean",
        ProductAttributeValueKind.String => "typAtribut.retezec",
        ProductAttributeValueKind.Integer => "typAtribut.integer",
        ProductAttributeValueKind.Numeric => "typAtribut.numeric",
        _ => throw new InvalidOperationException($"Unsupported attribute value kind {Kind}."),
    };

    internal bool? AsBoolean() => Kind == ProductAttributeValueKind.Boolean ? (bool)_value : (bool?)null;
    internal string? AsString() => Kind == ProductAttributeValueKind.String ? (string)_value : null;
    internal int? AsInteger() => Kind == ProductAttributeValueKind.Integer ? (int)_value : (int?)null;
    internal decimal? AsNumeric() => Kind == ProductAttributeValueKind.Numeric ? (decimal)_value : (decimal?)null;

    public override string ToString() => Convert.ToString(_value, CultureInfo.InvariantCulture) ?? string.Empty;
}
