namespace Rem.FlexiBeeSDK.Model.Products;

public class ProductLot
{
    public string ProductCode { get; set; }
    public decimal Amount { get; set; }
    public string? Expiration { get; set; }
    public string? Lot { get; set; }
}