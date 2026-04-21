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
        var request = new BankUnpairRequest { Id = 90313 };

        var json = JsonConvert.SerializeObject(request);
        var obj = JObject.Parse(json);

        Assert.Equal(90313, obj["id"]!.Value<int>());
        Assert.Equal(JTokenType.Array, obj["odparovani"]!.Type);
        Assert.Empty(obj["odparovani"]!);
    }

    [Fact]
    public void Serialization_AsListProducesArray()
    {
        var requests = new List<BankUnpairRequest> { new() { Id = 90313 } };

        var json = JsonConvert.SerializeObject(requests);
        var array = JArray.Parse(json);

        Assert.Single(array);
        Assert.Equal(90313, array[0]["id"]!.Value<int>());
        Assert.Equal(JTokenType.Array, array[0]["odparovani"]!.Type);
    }
}
