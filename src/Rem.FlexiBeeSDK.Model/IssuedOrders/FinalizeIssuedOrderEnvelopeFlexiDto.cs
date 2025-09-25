using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class FinalizeIssuedOrderEnvelopeFlexiDto
{
    [JsonProperty("objednavka-vydana")]
    public List<FinalizeIssuedOrderFlexiDto> Orders { get; set; }
    public FinalizeIssuedOrderEnvelopeFlexiDto(FinalizeIssuedOrderFlexiDto issuedOrder)
    {
        Orders = new List<FinalizeIssuedOrderFlexiDto>()
        {
            issuedOrder
        };
    }
}