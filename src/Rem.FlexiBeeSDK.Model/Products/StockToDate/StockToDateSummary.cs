namespace Rem.FlexiBeeSDK.Model.Products;

public class StockToDateSummary
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public double OnStock { get; set; }
    public double Reserved { get; set; }
    public double Price { get; set; }
    public int? ProductTypeId { get; set; }
    public string MoqName { get; set; }
    public string MoqAmount { get; set; }
    public int ProductId { get; set; }
    public bool HasLots { get; set; }
    public bool HasExpiration { get; set; }
    public double Volume { get; set; }
    public double Weight { get; set; }
}