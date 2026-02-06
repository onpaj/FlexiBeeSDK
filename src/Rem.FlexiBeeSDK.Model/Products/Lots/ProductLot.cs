using System;

namespace Rem.FlexiBeeSDK.Model.Products.Lots;

public class ProductLot
{
    public string ProductCode { get; set; }
    public decimal Amount { get; set; }
    public DateTime? Expiration { get; set; }
    public string? Lot { get; set; }
    public int Id { get; set; }
}