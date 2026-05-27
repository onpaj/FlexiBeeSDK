using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rem.FlexiBeeSDK.Model.Products;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public sealed class UpdateBoMItemRequestSerializationTests
{
    [Fact]
    public void Serializes_Only_Id_When_All_Other_Fields_Null()
    {
        // Arrange
        var request = new UpdateBoMItemRequest { Id = 42 };

        // Act
        var json = JsonConvert.SerializeObject(request);
        var parsed = JObject.Parse(json);

        // Assert
        parsed.Properties().Select(p => p.Name).Should().BeEquivalentTo(new[] { "id" });
        parsed["id"]!.Value<int>().Should().Be(42);
    }

    [Fact]
    public void Serializes_All_Supplied_Fields_With_Correct_Json_Names()
    {
        // Arrange
        var request = new UpdateBoMItemRequest
        {
            Id = 7,
            Order = 3,
            Name = "main",
            NameA = "english",
            NameB = "deutsch",
            NameC = "francais",
        };

        // Act
        var json = JsonConvert.SerializeObject(request);
        var parsed = JObject.Parse(json);

        // Assert
        parsed["id"].Should().NotBeNull();
        parsed["id"]!.Value<int>().Should().Be(7);
        parsed["poradi"].Should().NotBeNull();
        parsed["poradi"]!.Value<int>().Should().Be(3);
        parsed["nazev"].Should().NotBeNull();
        parsed["nazev"]!.Value<string>().Should().Be("main");
        parsed["nazevA"].Should().NotBeNull();
        parsed["nazevA"]!.Value<string>().Should().Be("english");
        parsed["nazevB"].Should().NotBeNull();
        parsed["nazevB"]!.Value<string>().Should().Be("deutsch");
        parsed["nazevC"].Should().NotBeNull();
        parsed["nazevC"]!.Value<string>().Should().Be("francais");
    }

    [Fact]
    public void Omits_Null_Order_And_Name_Fields()
    {
        // Arrange
        var request = new UpdateBoMItemRequest { Id = 9, NameC = "only-c" };

        // Act
        var json = JsonConvert.SerializeObject(request);
        var parsed = JObject.Parse(json);

        // Assert
        parsed.Properties().Select(p => p.Name).Should().BeEquivalentTo(new[] { "id", "nazevC" });
    }
}
