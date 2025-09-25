using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class CreateIssuedOrderItemEnvelopeFlexiDto
{
    [JsonProperty("objednavka-vydana")]
    public List<CreateIssuedOrderFlexiDto> Orders { get; set; }
    public CreateIssuedOrderItemEnvelopeFlexiDto(CreateIssuedOrderFlexiDto issuedOrder)
    {
        Orders = new List<CreateIssuedOrderFlexiDto>()
        {
            issuedOrder
        };
    }
}