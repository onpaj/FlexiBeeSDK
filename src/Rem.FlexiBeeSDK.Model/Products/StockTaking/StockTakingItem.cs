using System;

namespace Rem.FlexiBeeSDK.Model.Products.StockTaking;

public class StockTakingItem
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public DateTime? Expiration { get; set; }
    public string? LotCode { get; set; }
    public decimal Amount { get; set; }
}