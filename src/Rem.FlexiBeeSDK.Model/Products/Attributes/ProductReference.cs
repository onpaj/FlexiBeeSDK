using System;

namespace Rem.FlexiBeeSDK.Model.Products.Attributes;

/// <summary>
/// Identifies a product (evidence <c>cenik</c>) either by its FlexiBee id or by its code.
/// </summary>
public sealed class ProductReference
{
    private ProductReference(int? id, string? code)
    {
        Id = id;
        Code = code;
    }

    public int? Id { get; }
    public string? Code { get; }

    /// <summary>Value for a write payload: the numeric id or <c>code:XYZ</c>.</summary>
    public string ToRequestValue() => Code != null ? $"code:{Code}" : Id!.Value.ToString();

    public static ProductReference FromId(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), id, "Product id must be positive.");

        return new ProductReference(id, null);
    }

    public static ProductReference FromCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Product code must not be empty.", nameof(code));

        // The code is embedded into a quoted FlexiBee filter expression; both characters break the WQL parser.
        if (code.Contains("'") || code.Contains("\\"))
            throw new ArgumentException("Product code must not contain a single quote or a backslash.", nameof(code));

        return new ProductReference(null, code);
    }

    public override string ToString() => ToRequestValue();
}
