using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rem.FlexiBeeSDK.Model.Payments;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class BankUnpairRequestTests
{
    [Fact]
    public void Serialization_ProducesCorrectJson()
    {
        var request = new BankUnpairRequest { Id = "code:90313" };

        var json = JsonConvert.SerializeObject(request);
        var obj = JObject.Parse(json);

        Assert.Equal("code:90313", obj["id"]!.Value<string>());
        Assert.Equal(JTokenType.Array, obj["odparovani"]!.Type);
        Assert.Empty(obj["odparovani"]!);
    }

    [Fact]
    public void Serialization_AsListProducesArray()
    {
        var requests = new List<BankUnpairRequest> { new() { Id = "code:90313" } };

        var json = JsonConvert.SerializeObject(requests);
        var array = JArray.Parse(json);

        Assert.Single(array);
        Assert.Equal("code:90313", array[0]["id"]!.Value<string>());
        Assert.Equal(JTokenType.Array, array[0]["odparovani"]!.Type);
    }
}
