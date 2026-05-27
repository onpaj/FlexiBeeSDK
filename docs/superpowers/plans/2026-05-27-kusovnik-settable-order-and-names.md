# Kusovnik: Settable Order + All Four Name Fields — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Extend the FlexiBeeSDK Kusovnik (Bill of Materials) client so consumers can set the display order (`poradi`) of BoM rows and edit all four name/translation fields (`nazev`, `nazevA`, `nazevB`, `nazevC`) — one row at a time, or in a bulk reorder.

**Architecture:** Two new methods on `IBoMClient`: a generic `UpdateBoMItemAsync(id, order?, name?, nameA?, nameB?, nameC?)` for partial single-row patches and a `SetItemsOrderAsync(items)` for bulk reorder. Wire format uses a new partial-update DTO `UpdateBoMItemRequest` with Newtonsoft's `NullValueHandling.Ignore` so only supplied fields land on the wire. The `BoMItemFlexiDto` gains three sibling string properties (`NameA`/`NameB`/`NameC`) next to existing `Name`; `BomRequest.Detail` is extended so GET responses include them. PUT goes through the existing `Dictionary<string, object>` envelope already used by `RecalculatePurchasePrice` and `UpdateIngredientAmountAsync`.

**Tech Stack:** .NET 8, C# (nullable refs on), Newtonsoft.Json, xUnit, FluentAssertions, AutoFixture/AutoMoq. Integration tests hit the configured live FlexiBee demo server via `FlexiFixture`.

---

## Background Context (read before starting)

- The FlexiBee `kusovnik` evidence has NO native note/description field. Verified properties (from `https://demo.flexibee.eu/c/demo/kusovnik/properties.json`): `id, lastUpdate, nazev, nazevA, nazevB, nazevC, mnoz, hladina, poradi, cesta, otec, cenik, otecCenik`. The `nazevA`/`B`/`C` slots are translation labels (EN/DE/FR) that this SDK currently does not expose. Consuming apps can repurpose any of them as a "note" field — the SDK stays neutral.
- The existing read path lives in `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs` and uses a POST-based query (`BomRequest`) that returns rows via `BomResult.BoMItems`. Mutations use PUT.
- The existing mutation pattern is at `BoMClient.UpdateIngredientAmountAsync` (lines 86-111): builds a `Dictionary<string, object>` keyed by `ResourceIdentifier` ("kusovnik") with a small request DTO as the value, then `await PutAsync(document, cancellationToken)`. FlexiBee/Winstrom also accepts a JSON array under the resource key — that's what bulk reorder will use.
- All existing BoM tests in `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs` are integration tests against the live demo server (configured via user secrets in `FlexiFixture`). Two new pure-unit tests in this plan use `Newtonsoft.Json.JsonConvert` directly — no fixture needed.
- The test product `KRE003030` is already used by `UpdateIngredientAmount_ChangesAndRestores` and is known to have multiple non-header BoM rows — reuse it.

## File Structure

**Created:**
- `src/Rem.FlexiBeeSDK.Model/Products/UpdateBoMItemRequest.cs` — partial-update DTO; nullable fields with `NullValueHandling.Ignore` so unset fields are omitted from JSON.
- `test/Rem.FlexiBeeSDK.Tests/UpdateBoMItemRequestSerializationTests.cs` — pure unit tests for the serialization shape.

**Modified:**
- `src/Rem.FlexiBeeSDK.Model/BoMItemFlexiDto.cs` — add `NameA`, `NameB`, `NameC` properties next to existing `Name`.
- `src/Rem.FlexiBeeSDK.Model/Products/BoM/BomRequest.cs` — extend `Detail` field selection to include `nazevA,nazevB,nazevC`.
- `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs` — add `UpdateBoMItemAsync` and `SetItemsOrderAsync` signatures.
- `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs` — implement the two new methods.
- `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs` — add integration tests (change-and-restore pattern, mirroring `UpdateIngredientAmount_ChangesAndRestores`).

---

## Task 1: Add `UpdateBoMItemRequest` partial-update DTO

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/UpdateBoMItemRequest.cs`
- Test: `test/Rem.FlexiBeeSDK.Tests/UpdateBoMItemRequestSerializationTests.cs`

- [ ] **Step 1: Write the failing serialization test**

Create file `test/Rem.FlexiBeeSDK.Tests/UpdateBoMItemRequestSerializationTests.cs`:

```csharp
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rem.FlexiBeeSDK.Model.Products;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class UpdateBoMItemRequestSerializationTests
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
        parsed["id"]!.Value<int>().Should().Be(7);
        parsed["poradi"]!.Value<int>().Should().Be(3);
        parsed["nazev"]!.Value<string>().Should().Be("main");
        parsed["nazevA"]!.Value<string>().Should().Be("english");
        parsed["nazevB"]!.Value<string>().Should().Be("deutsch");
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
```

- [ ] **Step 2: Run the test to verify it fails (class does not exist yet)**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~UpdateBoMItemRequestSerializationTests"
```
Expected: build failure with "The type or namespace name 'UpdateBoMItemRequest' could not be found".

- [ ] **Step 3: Create the DTO class**

Create file `src/Rem.FlexiBeeSDK.Model/Products/UpdateBoMItemRequest.cs`:

```csharp
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products;

public class UpdateBoMItemRequest
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("poradi", NullValueHandling = NullValueHandling.Ignore)]
    public int? Order { get; set; }

    [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    [JsonProperty("nazevA", NullValueHandling = NullValueHandling.Ignore)]
    public string? NameA { get; set; }

    [JsonProperty("nazevB", NullValueHandling = NullValueHandling.Ignore)]
    public string? NameB { get; set; }

    [JsonProperty("nazevC", NullValueHandling = NullValueHandling.Ignore)]
    public string? NameC { get; set; }
}
```

- [ ] **Step 4: Run the test to verify it passes**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~UpdateBoMItemRequestSerializationTests"
```
Expected: 3 tests pass.

- [ ] **Step 5: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/UpdateBoMItemRequest.cs \
        test/Rem.FlexiBeeSDK.Tests/UpdateBoMItemRequestSerializationTests.cs
git commit -m "feat: add UpdateBoMItemRequest partial-update DTO"
```

---

## Task 2: Expose `nazevA/nazevB/nazevC` on `BoMItemFlexiDto` and extend `BomRequest.Detail`

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Model/BoMItemFlexiDto.cs:16-17`
- Modify: `src/Rem.FlexiBeeSDK.Model/Products/BoM/BomRequest.cs:17`
- Test: `test/Rem.FlexiBeeSDK.Tests/BoMItemFlexiDtoDeserializationTests.cs` (new)

- [ ] **Step 1: Write the failing deserialization test**

Create file `test/Rem.FlexiBeeSDK.Tests/BoMItemFlexiDtoDeserializationTests.cs`:

```csharp
using FluentAssertions;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class BoMItemFlexiDtoDeserializationTests
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
```

- [ ] **Step 2: Run the test to verify it fails (properties do not exist)**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~BoMItemFlexiDtoDeserializationTests"
```
Expected: build failure — `'BoMItemFlexiDto' does not contain a definition for 'NameA'`.

- [ ] **Step 3: Add the three properties to `BoMItemFlexiDto`**

In `src/Rem.FlexiBeeSDK.Model/BoMItemFlexiDto.cs`, find the existing `Name` property (around line 16-17):

```csharp
    [JsonProperty("nazev")]
    public string Name { get; set; }
```

Replace with:

```csharp
    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("nazevA")]
    public string? NameA { get; set; }

    [JsonProperty("nazevB")]
    public string? NameB { get; set; }

    [JsonProperty("nazevC")]
    public string? NameC { get; set; }
```

- [ ] **Step 4: Run the test to verify it passes**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~BoMItemFlexiDtoDeserializationTests"
```
Expected: 2 tests pass.

- [ ] **Step 5: Extend `BomRequest.Detail` so GET responses include the new fields**

In `src/Rem.FlexiBeeSDK.Model/Products/BoM/BomRequest.cs` line 17, find:

```csharp
    public string Detail => "custom:id,mnoz,hladina,poradi,cesta,cenik(id,kod,nazev,mj1(kod),nakupCena,typZasobyK,skupZboz,cenaZaklBezDph,cenaZaklVcDph,cenaZakl,popis,evidSarze,evidExpir),nazev,otec(id,nazev,mnoz),otecCenik(id,kod,nazev)";
```

Replace with:

```csharp
    public string Detail => "custom:id,mnoz,hladina,poradi,cesta,cenik(id,kod,nazev,mj1(kod),nakupCena,typZasobyK,skupZboz,cenaZaklBezDph,cenaZaklVcDph,cenaZakl,popis,evidSarze,evidExpir),nazev,nazevA,nazevB,nazevC,otec(id,nazev,mnoz),otecCenik(id,kod,nazev)";
```

(Only difference: `nazev` → `nazev,nazevA,nazevB,nazevC`.)

- [ ] **Step 6: Verify the existing integration tests still pass**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~BoMTests"
```
Expected: all 8 existing BoM tests pass. (Requires user secrets configured; if not configured, skip this step and rely on Task 3's integration test to cover the round-trip.)

- [ ] **Step 7: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/BoMItemFlexiDto.cs \
        src/Rem.FlexiBeeSDK.Model/Products/BoM/BomRequest.cs \
        test/Rem.FlexiBeeSDK.Tests/BoMItemFlexiDtoDeserializationTests.cs
git commit -m "feat: expose nazevA/B/C on BoMItemFlexiDto and include them in BoM GET detail"
```

---

## Task 3: Implement `UpdateBoMItemAsync` (single-row patch)

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs`
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs`
- Modify: `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs`

- [ ] **Step 1: Write the failing integration test for setting `NameC`**

Append to `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs` (inside the `BoMTests` class, after `UpdateIngredientAmount_IngredientNotFound_ShouldThrow`):

```csharp
        [Fact]
        public async Task UpdateBoMItem_SetsNameC_AndRestores()
        {
            var client = _fixture.Create<BoMClient>();
            const string productCode = "KRE003030";

            // Fetch current BoM and pick a non-header row
            var bom = await client.GetAsync(productCode);
            var row = bom.FirstOrDefault(r => r.Level != 1);
            row.Should().NotBeNull($"product {productCode} must have at least one non-header BoM row");

            var originalNameC = row!.NameC;
            var newNameC = $"sdk-test-{Guid.NewGuid():N}";

            // Set NameC
            await client.UpdateBoMItemAsync(row.Id, nameC: newNameC);

            // Verify persistence
            var updated = (await client.GetAsync(productCode)).Single(r => r.Id == row.Id);
            updated.NameC.Should().Be(newNameC);

            // Restore
            await client.UpdateBoMItemAsync(row.Id, nameC: originalNameC ?? string.Empty);
        }

        [Fact]
        public async Task UpdateBoMItem_SetsOrder_AndRestores()
        {
            var client = _fixture.Create<BoMClient>();
            const string productCode = "KRE003030";

            var bom = await client.GetAsync(productCode);
            var row = bom.FirstOrDefault(r => r.Level != 1);
            row.Should().NotBeNull();

            var originalOrder = row!.Order;
            var newOrder = originalOrder + 100;

            await client.UpdateBoMItemAsync(row.Id, order: newOrder);

            var updated = (await client.GetAsync(productCode)).Single(r => r.Id == row.Id);
            updated.Order.Should().Be(newOrder);

            await client.UpdateBoMItemAsync(row.Id, order: originalOrder);
        }

        [Fact]
        public async Task UpdateBoMItem_ThrowsWhenNothingProvided()
        {
            var client = _fixture.Create<BoMClient>();

            var act = async () => await client.UpdateBoMItemAsync(id: 1);

            await act.Should().ThrowAsync<ArgumentException>();
        }
```

- [ ] **Step 2: Run the tests to verify they fail (method does not exist)**

Run:
```bash
dotnet build
```
Expected: build failure — `'IBoMClient' does not contain a definition for 'UpdateBoMItemAsync'`.

- [ ] **Step 3: Add the signature to `IBoMClient`**

In `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs`, replace the file contents with:

```csharp
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products.BoM;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.BoM
{
    public interface IBoMClient
    {
        Task<IList<BoMItemFlexiDto>> GetAsync(string code, CancellationToken cancellationToken = default);

        Task<IList<BoMItemFlexiDto>> GetByIngredientAsync(string code, CancellationToken cancellationToken = default);

        Task<bool> RecalculatePurchasePrice(int bomId, CancellationToken cancellationToken = default);

        Task<ProductWeightFlexiDto?> GetBomWeight(string productCode, CancellationToken cancellationToken = default);

        Task UpdateIngredientAmountAsync(string productCode, string ingredientCode, double newAmount, CancellationToken cancellationToken = default);

        Task UpdateBoMItemAsync(
            int id,
            int? order = null,
            string? name = null,
            string? nameA = null,
            string? nameB = null,
            string? nameC = null,
            CancellationToken cancellationToken = default);
    }
}
```

- [ ] **Step 4: Implement `UpdateBoMItemAsync` in `BoMClient`**

In `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs`, just before the closing brace of the class (after `StripCodePrefix`, before line 119's `}`), add:

```csharp
        public async Task UpdateBoMItemAsync(
            int id,
            int? order = null,
            string? name = null,
            string? nameA = null,
            string? nameB = null,
            string? nameC = null,
            CancellationToken cancellationToken = default)
        {
            if (order is null && name is null && nameA is null && nameB is null && nameC is null)
                throw new ArgumentException("At least one field must be provided to update.");

            var document = new Dictionary<string, object>
            {
                {
                    ResourceIdentifier,
                    new UpdateBoMItemRequest
                    {
                        Id = id,
                        Order = order,
                        Name = name,
                        NameA = nameA,
                        NameB = nameB,
                        NameC = nameC,
                    }
                }
            };

            await PutAsync(document, cancellationToken: cancellationToken);
        }
```

- [ ] **Step 5: Run the three new tests to verify they pass**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~UpdateBoMItem_"
```
Expected: 3 tests pass (`UpdateBoMItem_SetsNameC_AndRestores`, `UpdateBoMItem_SetsOrder_AndRestores`, `UpdateBoMItem_ThrowsWhenNothingProvided`).

- [ ] **Step 6: Run the full BoMTests suite to verify no regressions**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~BoMTests"
```
Expected: all 11 BoM tests pass (8 original + 3 new).

- [ ] **Step 7: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs \
        src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs \
        test/Rem.FlexiBeeSDK.Tests/BoMTests.cs
git commit -m "feat: add UpdateBoMItemAsync for partial updates of order and name fields"
```

---

## Task 4: Implement `SetItemsOrderAsync` (bulk reorder)

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs`
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs`
- Modify: `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs`

- [ ] **Step 1: Write the failing integration test**

Append to `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs` inside `BoMTests`:

```csharp
        [Fact]
        public async Task SetItemsOrderAsync_BulkReorder_AndRestores()
        {
            var client = _fixture.Create<BoMClient>();
            const string productCode = "KRE003030";

            var bom = await client.GetAsync(productCode);
            var rows = bom.Where(r => r.Level != 1).ToList();
            rows.Should().HaveCountGreaterThan(1,
                $"product {productCode} must have at least two non-header rows for a bulk reorder test");

            var originalPairs = rows.Select(r => (r.Id, r.Order)).ToList();

            // Swap order between the first two rows
            var swapped = new List<(int Id, int Order)>
            {
                (rows[0].Id, rows[1].Order),
                (rows[1].Id, rows[0].Order),
            };

            await client.SetItemsOrderAsync(swapped);

            // Verify
            var afterSwap = await client.GetAsync(productCode);
            afterSwap.Single(r => r.Id == rows[0].Id).Order.Should().Be(rows[1].Order);
            afterSwap.Single(r => r.Id == rows[1].Id).Order.Should().Be(rows[0].Order);

            // Restore
            await client.SetItemsOrderAsync(originalPairs);
        }

        [Fact]
        public async Task SetItemsOrderAsync_EmptyInput_IsNoOp()
        {
            var client = _fixture.Create<BoMClient>();

            var act = async () => await client.SetItemsOrderAsync(Array.Empty<(int, int)>());

            await act.Should().NotThrowAsync();
        }
```

- [ ] **Step 2: Run the tests to verify they fail**

Run:
```bash
dotnet build
```
Expected: build failure — `'IBoMClient' does not contain a definition for 'SetItemsOrderAsync'`.

- [ ] **Step 3: Add the signature to `IBoMClient`**

In `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs`, inside the interface body, after the `UpdateBoMItemAsync` declaration from Task 3, add:

```csharp
        Task SetItemsOrderAsync(
            IEnumerable<(int Id, int Order)> items,
            CancellationToken cancellationToken = default);
```

- [ ] **Step 4: Implement `SetItemsOrderAsync` in `BoMClient`**

In `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs`, just below `UpdateBoMItemAsync` (added in Task 3), add:

```csharp
        public async Task SetItemsOrderAsync(
            IEnumerable<(int Id, int Order)> items,
            CancellationToken cancellationToken = default)
        {
            var payload = items
                .Select(i => new UpdateBoMItemRequest { Id = i.Id, Order = i.Order })
                .ToList();

            if (payload.Count == 0) return;

            var document = new Dictionary<string, object>
            {
                { ResourceIdentifier, payload }
            };

            await PutAsync(document, cancellationToken: cancellationToken);
        }
```

- [ ] **Step 5: Run the two new tests to verify they pass**

Run:
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~SetItemsOrderAsync"
```
Expected: 2 tests pass.

- [ ] **Step 6: Run the full test suite to verify no regressions**

Run:
```bash
dotnet test
```
Expected: all tests pass.

- [ ] **Step 7: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs \
        src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs \
        test/Rem.FlexiBeeSDK.Tests/BoMTests.cs
git commit -m "feat: add SetItemsOrderAsync for bulk BoM reorder"
```

---

## Final Verification

- [ ] **Step 1: Full build**

Run:
```bash
dotnet build --configuration Release
```
Expected: build succeeds with no warnings introduced by the new code.

- [ ] **Step 2: Full test run**

Run:
```bash
dotnet test
```
Expected: all tests pass. New tests added by this plan: 5 unit (`UpdateBoMItemRequestSerializationTests` × 3, `BoMItemFlexiDtoDeserializationTests` × 2) + 5 integration (`UpdateBoMItem_SetsNameC_AndRestores`, `UpdateBoMItem_SetsOrder_AndRestores`, `UpdateBoMItem_ThrowsWhenNothingProvided`, `SetItemsOrderAsync_BulkReorder_AndRestores`, `SetItemsOrderAsync_EmptyInput_IsNoOp`).

- [ ] **Step 3: Manual sanity check (optional)**

After running the integration tests once, open the FlexiBee web UI for the demo company and inspect product `KRE003030`'s BoM. The data should look identical to before the run — every test restores its changes.
