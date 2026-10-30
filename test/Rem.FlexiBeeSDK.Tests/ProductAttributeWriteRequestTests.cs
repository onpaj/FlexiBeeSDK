using FluentAssertions;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Products.Attributes;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class ProductAttributeWriteRequestTests
{
    [Fact]
    public void Create_SerializesProductTypeAndBooleanOnly()
    {
        var request = ProductAttributeWriteRequest.Create(ProductReference.FromCode("ABC"), 90, ProductAttributeValue.FromBoolean(true));

        JsonConvert.SerializeObject(request).Should().Be("{\"cenik\":\"code:ABC\",\"typAtributu\":90,\"valBoolean\":true}");
    }

    [Fact]
    public void Update_SerializesIdAndValueOnly()
    {
        var request = ProductAttributeWriteRequest.Update(77, ProductAttributeValue.FromBoolean(false));

        JsonConvert.SerializeObject(request).Should().Be("{\"id\":77,\"valBoolean\":false}");
    }

    [Fact]
    public void Delete_SerializesIdAndAction()
    {
        JsonConvert.SerializeObject(ProductAttributeWriteRequest.Delete(77)).Should().Be("{\"id\":77,\"@action\":\"delete\"}");
    }

    [Theory]
    [InlineData(ProductAttributeValueKind.String, "typAtribut.retezec", "\"valString\":\"x\"")]
    [InlineData(ProductAttributeValueKind.Integer, "typAtribut.integer", "\"valInteger\":5")]
    [InlineData(ProductAttributeValueKind.Numeric, "typAtribut.numeric", "\"valNumeric\":1.5")]
    [InlineData(ProductAttributeValueKind.Boolean, "typAtribut.boolean", "\"valBoolean\":true")]
    public void Update_MapsEachKindToItsOwnField(ProductAttributeValueKind kind, string typeKey, string expectedJsonFragment)
    {
        var value = kind switch
        {
            ProductAttributeValueKind.String => ProductAttributeValue.FromString("x"),
            ProductAttributeValueKind.Integer => ProductAttributeValue.FromInteger(5),
            ProductAttributeValueKind.Numeric => ProductAttributeValue.FromNumeric(1.5m),
            _ => ProductAttributeValue.FromBoolean(true),
        };

        value.RequiredTypeKey.Should().Be(typeKey);
        JsonConvert.SerializeObject(ProductAttributeWriteRequest.Update(1, value)).Should().Contain(expectedJsonFragment);
    }

    [Fact]
    public void Deserialize_ParsesFlexiRowWithEmptyNumbersAsNull()
    {
        const string json = "{\"id\":\"2367\",\"valBoolean\":\"true\",\"valString\":\"\",\"valInteger\":\"\",\"valNumeric\":\"\",\"hodnota\":\"true\",\"cenik\":\"code:X\",\"typAtributu\":\"code:T\"}";

        var row = JsonConvert.DeserializeObject<ProductAttributeFlexiDto>(json)!;

        row.Id.Should().Be(2367);
        row.BooleanValue.Should().BeTrue();
        row.StringValue.Should().BeEmpty();
        row.IntegerValue.Should().BeNull();
        row.NumericValue.Should().BeNull();
        row.DisplayValue.Should().Be("true");
    }
}
