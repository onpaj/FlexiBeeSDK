using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockToDate;

public class StockToDateResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("stav-skladu-k-datu")]
    public List<StockToDateItem> StockData { get; set; }
}