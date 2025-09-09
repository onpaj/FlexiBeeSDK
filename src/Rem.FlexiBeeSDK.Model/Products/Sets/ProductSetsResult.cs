using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.Sets;

public class ProductSetsResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("sady-a-komplety")]
    public List<ProductSetFlexiDto> Sets { get; set; }
}