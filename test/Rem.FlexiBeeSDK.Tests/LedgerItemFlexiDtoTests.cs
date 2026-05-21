using System;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class LedgerItemFlexiDtoTests
{
    [Fact]
    public void Deserialize_WithLastUpdate_PopulatesLastUpdateProperty()
    {
        var json = """
            {
                "id": 42,
                "lastUpdate": "2025-05-20T14:30:00.000+02:00"
            }
            """;

        var dto = JsonConvert.DeserializeObject<LedgerItemFlexiDto>(json);

        Assert.NotNull(dto);
        Assert.NotNull(dto.LastUpdate);
        Assert.Equal(42, dto.Id);
        var expected = new DateTimeOffset(2025, 5, 20, 14, 30, 0, TimeSpan.FromHours(2));
        Assert.Equal(expected, dto.LastUpdate!.Value);
    }

    [Fact]
    public void Deserialize_WithoutLastUpdate_LastUpdateIsNull()
    {
        var json = """{ "id": 99 }""";

        var dto = JsonConvert.DeserializeObject<LedgerItemFlexiDto>(json);

        Assert.NotNull(dto);
        Assert.Null(dto.LastUpdate);
    }
}
