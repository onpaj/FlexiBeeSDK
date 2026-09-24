using FluentAssertions;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Products.StockToDate;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public sealed class StockToDateItemTests
{
    private const double Tolerance = 1e-6;

    // Live sample row from stav-skladu-k-datu (sklad=5), prumCena rounded to 2 decimals by FlexiBee
    private const string SampleRowJson = """
    {
      "id": "15351",
      "stavMJ": "71238.961591",
      "stavMJPozad": "71238.961591",
      "pozadavkyMJ": "0.0",
      "tuz": "22219.47",
      "nazev": "Ethanol 96% denaturovaný líh",
      "prumCena": "0.31",
      "mj1": "code:G"
    }
    """;

    [Fact]
    public void Deserializes_StockValue_From_Tuz()
    {
        // Act
        var item = JsonConvert.DeserializeObject<StockToDateItem>(SampleRowJson);

        // Assert
        item.Should().NotBeNull();
        item!.StockValue.Should().Be(22219.47);
        item.AveragePrice.Should().Be(0.31);
        item.Amount.Should().Be(71238.961591);
    }

    [Fact]
    public void ExactAveragePrice_Is_StockValue_Divided_By_Amount()
    {
        // Arrange
        var item = JsonConvert.DeserializeObject<StockToDateItem>(SampleRowJson)!;

        // Act
        var price = item.ExactAveragePrice;

        // Assert
        price.Should().BeApproximately(0.311901, Tolerance);
    }

    [Fact]
    public void ExactAveragePrice_Falls_Back_To_AveragePrice_When_Amount_Is_Zero()
    {
        // Arrange
        var item = new StockToDateItem { Amount = 0, StockValue = 100, AveragePrice = 0.31 };

        // Act
        var price = item.ExactAveragePrice;

        // Assert
        price.Should().Be(0.31);
    }

    [Fact]
    public void ExactAveragePrice_Falls_Back_To_AveragePrice_When_StockValue_Is_Zero()
    {
        // Arrange
        var item = new StockToDateItem { Amount = 50, StockValue = 0, AveragePrice = 0.31 };

        // Act
        var price = item.ExactAveragePrice;

        // Assert
        price.Should().Be(0.31);
    }

    [Fact]
    public void ExactAveragePrice_Falls_Back_To_AveragePrice_When_Amount_Is_Negative()
    {
        // Arrange
        var item = new StockToDateItem { Amount = -5, StockValue = -10, AveragePrice = 2.5 };

        // Act
        var price = item.ExactAveragePrice;

        // Assert
        price.Should().Be(2.5);
    }
}
