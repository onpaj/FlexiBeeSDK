using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockTaking;

public class FilterRequest
{
    public FilterRequest(string propertyName, IEnumerable<string> values)
    {
        Filter = $"({propertyName} in ({string.Join(",", values.Select(s => $"\"{s}\""))}))";
    }

    [JsonProperty("filter")]
    public string Filter { get; set; }
}