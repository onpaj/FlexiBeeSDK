using FluentAssertions;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Products.StockToDate;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public sealed class ProductFlexiDtoTests
{
    [Fact]
    public void Deserializes_DescriptionC_From_PopisC()
    {
        // Arrange
        const string json = """
        {
          "id": 1569,
          "kod": "AKL124",
          "nazev": "Acmella In-Tense extrakt",
          "popisC": "Gatuline Expression AF"
        }
        """;

        // Act
        var dto = JsonConvert.DeserializeObject<ProductFlexiDto>(json);

        // Assert
        dto.Should().NotBeNull();
        dto!.Name.Should().Be("Acmella In-Tense extrakt");
        dto.DescriptionC.Should().Be("Gatuline Expression AF");
    }

    [Fact]
    public void StockToDateRequest_Detail_Requests_PopisC_On_Cenik()
    {
        // Arrange
        var request = new StockToDateRequest();

        // Act
        var cenikProjection = request.Detail.Substring(request.Detail.IndexOf("cenik(", System.StringComparison.Ordinal));

        // Assert
        cenikProjection.Should().StartWith("cenik(");
        cenikProjection[..cenikProjection.IndexOf(')')].Should().Contain("popisC");
    }
}
