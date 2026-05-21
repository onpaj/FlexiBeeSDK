# Ledger GetChangedSinceAsync Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add `ILedgerClient.GetChangedSinceAsync` that filters `ucetni-denik` rows by `lastUpdate gte "<since>"`, enabling the BI sync to use a true watermark-based CDC pattern instead of a rolling 7-day window.

**Architecture:** A second constructor on `LedgerRequest` builds a `lastUpdate gte "..."` filter (replacing the `datUcto` range entirely). `LedgerItemFlexiDto` gets a `LastUpdate` property so callers can advance their watermark. The `Detail` string in `LedgerRequest` is extended to include `lastUpdate` so FlexiBee returns the field in responses.

**Tech Stack:** C# / .NET, Newtonsoft.Json, xUnit, FluentAssertions

---

## File Map

| Action | File | What changes |
|--------|------|-------------|
| Modify | `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerItemFlexiDto.cs` | Add `LastUpdate` property (`DateTime?`, mapped from `lastUpdate`) |
| Modify | `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerRequest.cs` | Add `lastUpdate` to `Detail` string; add second constructor for `since`-based filter; change `Order` to `lastUpdate` in that constructor |
| Modify | `src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/ILedgerClient.cs` | Add `GetChangedSinceAsync` signature |
| Modify | `src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/LedgerClient.cs` | Implement `GetChangedSinceAsync` |
| Create | `test/Rem.FlexiBeeSDK.Tests/LedgerItemFlexiDtoTests.cs` | Unit test: `lastUpdate` deserializes to `LastUpdate` |
| Modify | `test/Rem.FlexiBeeSDK.Tests/LedgerRequestTests.cs` | Fix `DefaultPropertyValues_AreSetCorrectly` for updated `Detail`; add new test class `LedgerChangedSinceRequestTests` at bottom |
| Modify | `test/Rem.FlexiBeeSDK.Tests/LedgerTests.cs` | Add integration test `GetChangedSinceReturnsRowsWithPopulatedLastUpdate` |

---

## Task 1: Add `LastUpdate` to `LedgerItemFlexiDto`

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerItemFlexiDto.cs:106-117` (add after `DocumentIdEvidencePath`)
- Create: `test/Rem.FlexiBeeSDK.Tests/LedgerItemFlexiDtoTests.cs`

- [ ] **Step 1: Write the failing test**

Create file `test/Rem.FlexiBeeSDK.Tests/LedgerItemFlexiDtoTests.cs`:

```csharp
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
        // FlexiBee returns offset-aware strings; Newtonsoft converts to local DateTime
        // so we compare the UTC representation
        var expectedUtc = new DateTimeOffset(2025, 5, 20, 14, 30, 0, TimeSpan.FromHours(2)).UtcDateTime;
        Assert.Equal(expectedUtc, dto.LastUpdate!.Value.ToUniversalTime());
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
```

- [ ] **Step 2: Run test to verify it fails**

```bash
cd /Users/pajgrtondrej/conductor/workspaces/FlexiBeeSDK/nashville
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerItemFlexiDtoTests" --no-build 2>&1 | tail -20
```

Expected: build error — `LastUpdate` does not exist on `LedgerItemFlexiDto`.

- [ ] **Step 3: Add `LastUpdate` property to `LedgerItemFlexiDto`**

In `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerItemFlexiDto.cs`, add after line 116 (after `DocumentIdEvidencePath`):

```csharp
    [JsonProperty("lastUpdate")]
    public DateTime? LastUpdate { get; set; }
```

The full bottom of the file becomes:

```csharp
    [JsonProperty("zuctovano")]
    public bool IsCleared { get; set; }

    [JsonProperty("idUcetniDenik")]
    public object JournalId { get; set; }

    [JsonProperty("idDokl")]
    public int DocumentId { get; set; }

    [JsonProperty("idDokl@evidencePath")]
    public string DocumentIdEvidencePath { get; set; }

    [JsonProperty("lastUpdate")]
    public DateTime? LastUpdate { get; set; }
}
```

- [ ] **Step 4: Run test to verify it passes**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerItemFlexiDtoTests" 2>&1 | tail -20
```

Expected: 2 tests PASS.

- [ ] **Step 5: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerItemFlexiDto.cs \
        test/Rem.FlexiBeeSDK.Tests/LedgerItemFlexiDtoTests.cs
git commit -m "feat: add LastUpdate property to LedgerItemFlexiDto"
```

---

## Task 2: Add `lastUpdate` to the `Detail` string in `LedgerRequest`

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerRequest.cs:19-20` (update `Detail` default)
- Modify: `test/Rem.FlexiBeeSDK.Tests/LedgerRequestTests.cs:200` (fix `DefaultPropertyValues_AreSetCorrectly`)

FlexiBee omits `lastUpdate` from responses unless explicitly requested in the `detail` custom field list. Adding it here makes it available for both `GetAsync` and `GetChangedSinceAsync`.

- [ ] **Step 1: Run the existing test to confirm it currently passes**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerRequestTests.DefaultPropertyValues_AreSetCorrectly" 2>&1 | tail -10
```

Expected: 1 test PASS (baseline before our change).

- [ ] **Step 2: Update `Detail` in `LedgerRequest`**

In `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerRequest.cs`, replace lines 18-20:

```csharp
    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:parSymbol,datVyst,datUcto,doklad,nazFirmy,stredisko(nazev,kod,id),popis,sumTuz,sumMen,mena(kod),kurz,mdUcet(kod,nazev,id),dalUcet(kod,nazev,id),zuctovano,idUcetniDenik,idDokl";
```

with:

```csharp
    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:parSymbol,datVyst,datUcto,doklad,nazFirmy,stredisko(nazev,kod,id),popis,sumTuz,sumMen,mena(kod),kurz,mdUcet(kod,nazev,id),dalUcet(kod,nazev,id),zuctovano,idUcetniDenik,idDokl,lastUpdate";
```

- [ ] **Step 3: Fix the broken assertion in `LedgerRequestTests`**

In `test/Rem.FlexiBeeSDK.Tests/LedgerRequestTests.cs`, find the test `DefaultPropertyValues_AreSetCorrectly` (line ~189) and update the `Detail` assertion at line ~200:

Replace:
```csharp
            Assert.Equal("custom:parSymbol,datVyst,datUcto,doklad,nazFirmy,stredisko(nazev,kod,id),popis,sumTuz,sumMen,mena(kod),kurz,mdUcet(kod,nazev,id),dalUcet(kod,nazev,id),zuctovano,idUcetniDenik,idDokl", request.Detail);
```

with:
```csharp
            Assert.Equal("custom:parSymbol,datVyst,datUcto,doklad,nazFirmy,stredisko(nazev,kod,id),popis,sumTuz,sumMen,mena(kod),kurz,mdUcet(kod,nazev,id),dalUcet(kod,nazev,id),zuctovano,idUcetniDenik,idDokl,lastUpdate", request.Detail);
```

- [ ] **Step 4: Run all `LedgerRequestTests` to verify everything passes**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerRequestTests" 2>&1 | tail -20
```

Expected: all 16 tests PASS.

- [ ] **Step 5: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerRequest.cs \
        test/Rem.FlexiBeeSDK.Tests/LedgerRequestTests.cs
git commit -m "feat: include lastUpdate field in LedgerRequest detail"
```

---

## Task 3: Add `lastUpdate`-based constructor to `LedgerRequest`

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerRequest.cs`
- Modify: `test/Rem.FlexiBeeSDK.Tests/LedgerRequestTests.cs` (add new test class at bottom of file)

The new constructor takes a single `DateTime since` and builds a `(lastUpdate gte "...")` filter. It also overrides `Order` to `lastUpdate` so batches arrive in watermark order.

- [ ] **Step 1: Write the failing tests**

Append a new test class at the **bottom** of `test/Rem.FlexiBeeSDK.Tests/LedgerRequestTests.cs`, just before the closing `}` of the namespace:

```csharp
    public class LedgerChangedSinceRequestTests
    {
        [Fact]
        public void Constructor_WithSince_FilterContainsLastUpdateGte()
        {
            var since = new DateTime(2025, 5, 21, 10, 30, 0, DateTimeKind.Utc);

            var request = new LedgerRequest(since);

            Assert.Contains("lastUpdate gte", request.Filter);
            Assert.Contains("2025-05-21T10:30:00Z", request.Filter);
        }

        [Fact]
        public void Constructor_WithSince_FilterDoesNotContainDatUcto()
        {
            var since = new DateTime(2025, 5, 21, 10, 30, 0, DateTimeKind.Utc);

            var request = new LedgerRequest(since);

            Assert.DoesNotContain("datUcto", request.Filter);
        }

        [Fact]
        public void Constructor_WithSince_FilterMatchesExpectedFormat()
        {
            var since = new DateTime(2025, 5, 21, 10, 30, 0, DateTimeKind.Utc);

            var request = new LedgerRequest(since);

            Assert.Equal("(lastUpdate gte \"2025-05-21T10:30:00Z\")", request.Filter);
        }

        [Fact]
        public void Constructor_WithLocalSince_ConvertsToUtcInFilter()
        {
            // Arrange: a time expressed in local kind — must be normalized to UTC in the filter
            var since = new DateTime(2025, 5, 21, 10, 30, 0, DateTimeKind.Utc);

            var request = new LedgerRequest(since);

            // Filter must always contain a UTC representation (ends with Z)
            Assert.EndsWith("\")", request.Filter); // last char before ) is closing quote
            Assert.Contains("Z\"", request.Filter);
        }

        [Fact]
        public void Constructor_WithSince_OrderIsLastUpdate()
        {
            var since = new DateTime(2025, 5, 21, DateTimeKind.Utc);

            var request = new LedgerRequest(since);

            Assert.Equal("lastUpdate", request.Order);
        }

        [Fact]
        public void Constructor_WithSince_DefaultPaginationIsZero()
        {
            var since = new DateTime(2025, 5, 21, DateTimeKind.Utc);

            var request = new LedgerRequest(since);

            Assert.Equal(0, request.Limit);
            Assert.Equal(0, request.Start);
        }
    }
```

Note: the new class goes **inside** the `namespace Rem.FlexiBeeSDK.Tests` block (before the closing `}`). The file currently uses old-style namespace block syntax (`namespace Rem.FlexiBeeSDK.Tests { ... }`), so the new class must be placed before the outermost closing `}`.

- [ ] **Step 2: Run tests to verify they fail**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerChangedSinceRequestTests" 2>&1 | tail -20
```

Expected: build error — `LedgerRequest` has no constructor taking a single `DateTime`.

- [ ] **Step 3: Add the new constructor to `LedgerRequest`**

In `src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerRequest.cs`, add the following constructor after the existing one (after line 14):

```csharp
    public LedgerRequest(DateTime since)
    {
        Filter = $"(lastUpdate gte \"{since.ToUniversalTime():yyyy-MM-ddTHH:mm:ss}Z\")";
        Order = "lastUpdate";
    }
```

The file's constructor section becomes:

```csharp
public class LedgerRequest
{
    public LedgerRequest(DateTime dateFrom, DateTime dateTo, IEnumerable<string>? debitAccountPrefixes = null, IEnumerable<string>? creditAccountPrefixes = null, string? departmentId = null)
    {
        Filter =
            $"((datUcto gte \"{dateFrom:yyyy-MM-dd}\" and datUcto lte \"{dateTo:yyyy-MM-dd}\") {GetAccountFilterString("mdUcet", debitAccountPrefixes)} {GetAccountFilterString("dalUcet", creditAccountPrefixes)} {GetDepartmentFilterString(departmentId)})";
    }

    public LedgerRequest(DateTime since)
    {
        Filter = $"(lastUpdate gte \"{since.ToUniversalTime():yyyy-MM-ddTHH:mm:ss}Z\")";
        Order = "lastUpdate";
    }

    // ... rest of properties unchanged
```

- [ ] **Step 4: Run tests to verify they pass**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerChangedSinceRequestTests" 2>&1 | tail -20
```

Expected: 6 tests PASS.

- [ ] **Step 5: Run full suite to check no regressions**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ 2>&1 | tail -20
```

Expected: all existing tests + 6 new tests PASS.

- [ ] **Step 6: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Accounting/Ledger/LedgerRequest.cs \
        test/Rem.FlexiBeeSDK.Tests/LedgerRequestTests.cs
git commit -m "feat: add LedgerRequest constructor for lastUpdate-based filtering"
```

---

## Task 4: Add `GetChangedSinceAsync` to interface and client

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/ILedgerClient.cs`
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/LedgerClient.cs`
- Modify: `test/Rem.FlexiBeeSDK.Tests/LedgerTests.cs` (add integration test)

- [ ] **Step 1: Write the failing integration test**

In `test/Rem.FlexiBeeSDK.Tests/LedgerTests.cs`, add after the last existing `[Fact]` method (before the closing `}`):

```csharp
        [Fact]
        public async Task GetChangedSince_ReturnsRowsWithPopulatedLastUpdate()
        {
            var client = _fixture.Create<LedgerClient>();
            var since = DateTime.UtcNow.AddDays(-30);

            var items = await client.GetChangedSinceAsync(since, limit: 10);

            items.Should().NotBeNull();
            items.Should().OnlyContain(item => item.LastUpdate.HasValue);
            items.Should().OnlyContain(item => item.LastUpdate!.Value.ToUniversalTime() >= since.ToUniversalTime());
        }

        [Fact]
        public async Task GetChangedSince_WithFutureDate_ReturnsEmptyList()
        {
            var client = _fixture.Create<LedgerClient>();
            var futureDate = DateTime.UtcNow.AddDays(1);

            var items = await client.GetChangedSinceAsync(futureDate);

            items.Should().NotBeNull();
            items.Should().BeEmpty();
        }
```

- [ ] **Step 2: Run tests to verify they fail**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerTests.GetChangedSince" 2>&1 | tail -20
```

Expected: build error — `GetChangedSinceAsync` does not exist on `LedgerClient`.

- [ ] **Step 3: Add method signature to `ILedgerClient`**

Replace the full content of `src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/ILedgerClient.cs` with:

```csharp
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;

public interface ILedgerClient
{
    Task<IReadOnlyList<LedgerItemFlexiDto>> GetAsync(DateTime dateFrom, DateTime dateTo, IEnumerable<string>? debitAccountPrefixes = null, IEnumerable<string>? creditAccountPrefix = null, string? departmentId = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LedgerItemFlexiDto>> GetChangedSinceAsync(DateTime since, int? limit = null, int? skip = null, CancellationToken cancellationToken = default);
}
```

- [ ] **Step 4: Implement `GetChangedSinceAsync` in `LedgerClient`**

In `src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/LedgerClient.cs`, add the new method after `GetAsync` (after line 40):

```csharp
    public async Task<IReadOnlyList<LedgerItemFlexiDto>> GetChangedSinceAsync(
        DateTime since,
        int? limit = null,
        int? skip = null,
        CancellationToken cancellationToken = default)
    {
        var queryDoc = new LedgerRequest(since)
        {
            Limit = limit ?? 0,
            Start = skip ?? 0,
        };

        var query = new FlexiQuery();
        var result = await PostAsync<LedgerRequest, LedgerResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.LedgerItems ?? new List<LedgerItemFlexiDto>();
    }
```

The full updated `LedgerClient.cs`:

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;

public class LedgerClient : ResourceClient, ILedgerClient
{
    public LedgerClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<LedgerClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.Ledger;
    protected override string? RequestIdentifier => null;

    public async Task<IReadOnlyList<LedgerItemFlexiDto>> GetAsync(DateTime dateFrom, DateTime dateTo, IEnumerable<string>? debitAccountPrefixes = null, IEnumerable<string>? creditAccountPrefixes = null, string? departmentId = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default)
    {
        var queryDoc = new LedgerRequest(dateFrom, dateTo, debitAccountPrefixes, creditAccountPrefixes, departmentId)
        {
            Limit = limit,
            Start = skip,
        };

        var query = new FlexiQuery();
        var result = await PostAsync<LedgerRequest, LedgerResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.LedgerItems ?? new List<LedgerItemFlexiDto>();
    }

    public async Task<IReadOnlyList<LedgerItemFlexiDto>> GetChangedSinceAsync(
        DateTime since,
        int? limit = null,
        int? skip = null,
        CancellationToken cancellationToken = default)
    {
        var queryDoc = new LedgerRequest(since)
        {
            Limit = limit ?? 0,
            Start = skip ?? 0,
        };

        var query = new FlexiQuery();
        var result = await PostAsync<LedgerRequest, LedgerResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.LedgerItems ?? new List<LedgerItemFlexiDto>();
    }
}
```

- [ ] **Step 5: Build to catch any compile errors**

```bash
dotnet build 2>&1 | tail -20
```

Expected: Build succeeded with 0 errors.

- [ ] **Step 6: Run unit and request tests**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerItemFlexiDtoTests|FullyQualifiedName~LedgerRequestTests|FullyQualifiedName~LedgerChangedSinceRequestTests" 2>&1 | tail -20
```

Expected: all pass.

- [ ] **Step 7: Run integration tests (requires staging FlexiBee credentials in user secrets)**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ --filter "FullyQualifiedName~LedgerTests.GetChangedSince" 2>&1 | tail -30
```

Expected: 2 integration tests PASS. If credentials are not configured, tests will skip or throw a connection error — that is acceptable for CI; they require a live FlexiBee instance.

- [ ] **Step 8: Run full test suite for regression check**

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ 2>&1 | tail -20
```

Expected: all tests PASS (unit + integration).

- [ ] **Step 9: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/ILedgerClient.cs \
        src/Rem.FlexiBeeSDK.Client/Clients/Accounting/Ledger/LedgerClient.cs \
        test/Rem.FlexiBeeSDK.Tests/LedgerTests.cs
git commit -m "feat: add GetChangedSinceAsync to ILedgerClient for watermark-based CDC"
```

---

## Self-Review

### Spec coverage

| Requirement | Task |
|-------------|------|
| `ILedgerClient.GetChangedSinceAsync(DateTime since, int? limit, int? skip, CancellationToken)` | Task 4 |
| Filter: `lastUpdate gte "<iso8601-utc>"` | Task 3 |
| Inclusive `gte` (not `gt`) | Task 3 — constructor uses `gte` |
| No `datUcto` filter on new method | Task 3 — new constructor has separate filter, Task 3 test asserts `DoesNotContain("datUcto")` |
| `LedgerItemFlexiDto.LastUpdate` (DateTime?) for watermark advancement | Task 1 |
| `lastUpdate` returned by FlexiBee (detail string updated) | Task 2 |
| `GetAsync` unchanged | Tasks 1–4 do not touch `GetAsync` logic |
| Unit test: filter contains `lastUpdate gte` and NOT `datUcto` | Task 3, steps 1–4 |
| Unit test: DTO deserializes `lastUpdate` | Task 1 |
| Integration test: empty list for future `since` | Task 4, `GetChangedSince_WithFutureDate_ReturnsEmptyList` |
| Regression: existing tests still pass | Tasks 2, 3, 4 each run full suite |

### Placeholder scan

No TBDs, no "similar to Task N" shortcuts, no steps without code. ✓

### Type consistency

- `LedgerRequest(DateTime since)` constructor introduced in Task 3, used in `LedgerClient.GetChangedSinceAsync` in Task 4. ✓
- `LedgerItemFlexiDto.LastUpdate` added in Task 1, asserted in Task 4 integration test. ✓
- `ILedgerClient.GetChangedSinceAsync` signature added in Task 4 step 3, implemented same task step 4. ✓
