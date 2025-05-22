using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockToDate;

public class LotsResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("sarze-expirace")]
    public List<LotsItem> Lots { get; set; }
}