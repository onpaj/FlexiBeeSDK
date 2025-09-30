using System.Collections.Generic;
using Newtonsoft.Json;

public class PriceListCollectionFlexiDto
{
    public PriceListCollectionFlexiDto(PriceListFlexiDto priceListData)
    {
        PriceList = [priceListData];
    }
        
    [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
    public List<PriceListFlexiDto> PriceList { get; set; }

    [JsonProperty("@version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; }
}