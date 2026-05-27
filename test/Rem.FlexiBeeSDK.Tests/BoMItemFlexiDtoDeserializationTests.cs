using FluentAssertions;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public sealed class BoMItemFlexiDtoDeserializationTests
{
    [Fact]
    public void Deserializes_All_Four_Name_Fields()
    {
        // Arrange
        const string json = """
        {
          "id": 11,
          "nazev": "primary",
          "nazevA": "english label",
          "nazevB": "deutsch label",
          "nazevC": "francais label",
          "mnoz": 1.0,
          "hladina": 2,
          "poradi": 5,
          "cesta": "1/2/",
          "otecCenik": [],
          "cenik": [],
          "otec": []
        }
        """;

        // Act
        var dto = JsonConvert.DeserializeObject<BoMItemFlexiDto>(json);

        // Assert
        dto.Should().NotBeNull();
        dto!.Name.Should().Be("primary");
        dto.NameA.Should().Be("english label");
        dto.NameB.Should().Be("deutsch label");
        dto.NameC.Should().Be("francais label");
    }

    [Fact]
    public void Tolerates_Missing_Name_Translations()
    {
        // Arrange — older payloads / rows where translations are blank
        const string json = """
        {
          "id": 1,
          "nazev": "only-primary",
          "mnoz": 1.0,
          "hladina": 2,
          "poradi": 1,
          "cesta": "1/",
          "otecCenik": [],
          "cenik": [],
          "otec": []
        }
        """;

        // Act
        var dto = JsonConvert.DeserializeObject<BoMItemFlexiDto>(json);

        // Assert
        dto!.Name.Should().Be("only-primary");
        dto.NameA.Should().BeNull();
        dto.NameB.Should().BeNull();
        dto.NameC.Should().BeNull();
    }
}
