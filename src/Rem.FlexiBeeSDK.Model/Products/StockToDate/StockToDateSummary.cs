namespace Rem.FlexiBeeSDK.Model.Products;

public class StockToDateSummary
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public double OnStock { get; set; }
    public double Reserved { get; set; }
    /// <summary>
    /// Average price (<c>prumCena</c>) as returned by stav-skladu-k-datu — rounded to 2 decimals.
    /// Prefer <see cref="ExactAveragePrice"/> for per-unit pricing of low-value units (e.g. grams).
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Stock value in CZK (<c>tuz</c>), 2 decimals.
    /// </summary>
    public double StockValue { get; set; }

    /// <summary>
    /// Exact average price computed as <c>tuz / stavMJ</c> (<see cref="StockValue"/> / <see cref="OnStock"/>),
    /// because <c>prumCena</c> from this endpoint is rounded to 2 decimals. Falls back to <see cref="Price"/>
    /// when quantity or value is not positive.
    /// </summary>
    public double ExactAveragePrice { get; set; }
    public int? ProductTypeId { get; set; }
    public string MoqName { get; set; }
    public string MoqAmount { get; set; }
    public int ProductId { get; set; }
    public bool HasLots { get; set; }
    public bool HasExpiration { get; set; }
    public double Volume { get; set; }
    public double Weight { get; set; }
    public string? SupplierCode { get; set; }
    public int? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? Note { get; set; }
}