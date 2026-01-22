# StockMovementClient Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Implement complete CRUD operations for FlexiBee `skladovy-pohyb` (Stock Movement) agenda with full model support and client integration.

**Architecture:** Following existing SDK patterns, create separate client for stock movement headers (`skladovy-pohyb`) to complement existing `StockItemsMovementClient` (`skladovy-pohyb-polozka`). Use POST-based queries for reads, standard POST for creates/updates. No DELETE operation (API limitation: `@deletable: false`).

**Tech Stack:** .NET 6+, Newtonsoft.Json, FlexiBee API v1.0, xUnit (for tests)

---

## Task 1: Add Agenda Constant

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Model/Agenda.cs:19`

**Step 1: Add StockMovement constant**

Add the constant after line 19 (after `StockMovements = "skladovy-pohyb-polozka"`):

```csharp
public const string StockMovement = "skladovy-pohyb";
```

**Step 2: Verify the change**

Run: `cat src/Rem.FlexiBeeSDK.Model/Agenda.cs | grep -A 2 "StockMovements"`

Expected output:
```
public const string StockMovements = "skladovy-pohyb-polozka";
public const string StockMovement = "skladovy-pohyb";
```

**Step 3: Build to verify no syntax errors**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded

**Step 4: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Agenda.cs
git commit -m "feat: add StockMovement agenda constant"
```

---

## Task 2: Create StockMovementFlexiDto Model (Read Response)

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementFlexiDto.cs`

**Step 1: Create the DTO class**

Create file with this content:

```csharp
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("lastUpdate")]
    public DateTime? LastUpdate { get; set; }

    [JsonProperty("datVyst")]
    public DateTime IssueDate { get; set; }

    [JsonProperty("datUcto")]
    public DateTime? AccountingDate { get; set; }

    [JsonProperty("typPohybuK")]
    public string MovementTypeRaw { get; set; }

    [JsonProperty("typPohybuSkladK")]
    public string StockMovementTypeRaw { get; set; }

    [JsonProperty("popis")]
    public string Description { get; set; }

    [JsonProperty("poznam")]
    public string Note { get; set; }

    [JsonProperty("sumCelkem")]
    public decimal TotalAmount { get; set; }

    [JsonProperty("sumCelkemMen")]
    public decimal TotalAmountCurrency { get; set; }

    [JsonProperty("storno")]
    public bool Cancelled { get; set; }

    [JsonProperty("zuctovano")]
    public bool Posted { get; set; }

    [JsonProperty("bezPolozek")]
    public bool WithoutItems { get; set; }

    [JsonProperty("nazFirmy")]
    public string CompanyName { get; set; }

    [JsonProperty("typDokl")]
    public List<StockMovementDocumentTypeFlexiDto> DocumentTypeList { get; set; }

    [JsonProperty("sklad")]
    public List<StockMovementWarehouseFlexiDto> WarehouseList { get; set; }

    [JsonProperty("mena")]
    public List<StockMovementCurrencyFlexiDto> CurrencyList { get; set; }

    [JsonProperty("stredisko")]
    public List<StockMovementDepartmentFlexiDto> DepartmentList { get; set; }

    [JsonProperty("skladovePolozky")]
    public List<StockItemMovementFlexiDto> Items { get; set; }

    [JsonProperty("varSym")]
    public string VariableSymbol { get; set; }

    [JsonProperty("cisObj")]
    public string OrderNumber { get; set; }

    [JsonProperty("cisDodak")]
    public string DeliveryNote { get; set; }
}
```

**Step 2: Build to verify syntax**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementFlexiDto.cs
git commit -m "feat: add StockMovementFlexiDto model"
```

---

## Task 3: Create Related Nested DTOs

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementDocumentTypeFlexiDto.cs`
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementWarehouseFlexiDto.cs`
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementCurrencyFlexiDto.cs`
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementDepartmentFlexiDto.cs`

**Step 1: Create StockMovementDocumentTypeFlexiDto**

```csharp
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementDocumentTypeFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }
}
```

**Step 2: Create StockMovementWarehouseFlexiDto**

```csharp
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementWarehouseFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }
}
```

**Step 3: Create StockMovementCurrencyFlexiDto**

```csharp
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementCurrencyFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }
}
```

**Step 4: Create StockMovementDepartmentFlexiDto**

```csharp
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementDepartmentFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }
}
```

**Step 5: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded

**Step 6: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovement*FlexiDto.cs
git commit -m "feat: add StockMovement nested DTOs"
```

---

## Task 4: Create StockMovementResult (Response Wrapper)

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementResult.cs`

**Step 1: Create result wrapper class**

```csharp
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("skladovy-pohyb")]
    public List<StockMovementFlexiDto> StockMovements { get; set; }
}
```

**Step 2: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementResult.cs
git commit -m "feat: add StockMovementResult wrapper"
```

---

## Task 5: Create StockMovementRequest (Query Parameters)

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementRequest.cs`

**Step 1: Create request class**

```csharp
using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class StockMovementRequest
{
    public StockMovementRequest(int id)
    {
        Filter = $"(id eq {id})";
    }

    public StockMovementRequest(string code)
    {
        Filter = $"(kod eq \"{code}\")";
    }

    public StockMovementRequest(
        DateTime dateFrom,
        DateTime dateTo,
        StockMovementDirection? direction = null,
        string? warehouseCode = null,
        int? documentTypeId = null)
    {
        var directionFilter = GetDirectionFilter(direction);
        var warehouseFilter = GetWarehouseFilter(warehouseCode);
        var docTypeFilter = GetDocumentTypeFilter(documentTypeId);

        Filter = $"((datVyst gte \"{dateFrom:yyyy-MM-dd}\" and datVyst lte \"{dateTo:yyyy-MM-dd}\"){directionFilter}{warehouseFilter}{docTypeFilter})";
    }

    [JsonProperty("add-row-count")]
    public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:id,kod,lastUpdate,datVyst,datUcto,typPohybuK,typPohybuSkladK,popis,poznam,sumCelkem,sumCelkemMen,storno,zuctovano,bezPolozek,nazFirmy,typDokl(id,kod,nazev),sklad(id,kod,nazev),mena(id,kod,nazev),stredisko(id,kod,nazev),varSym,cisObj,cisDodak,skladovePolozky";

    [JsonProperty("limit")]
    public int Limit { get; set; } = 0;

    [JsonProperty("start")]
    public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } =
        "/skladovy-pohyb/typDokl,/skladovy-pohyb/sklad,/skladovy-pohyb/mena,/skladovy-pohyb/stredisko,/skladovy-pohyb/skladovePolozky";

    [JsonProperty("order")]
    public string Order { get; set; } = "datVyst@D";

    [JsonProperty("use-internal-id")]
    public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")]
    public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")]
    public string Version { get; set; } = "1.0";

    [JsonProperty("filter")]
    public string Filter { get; private set; }

    private string GetDirectionFilter(StockMovementDirection? direction)
    {
        if (!direction.HasValue || direction == StockMovementDirection.Any)
            return string.Empty;

        var directionValue = direction == StockMovementDirection.In ? "prijem" : "vydej";
        return $" and typPohybuK eq \"typPohybu.{directionValue}\"";
    }

    private string GetWarehouseFilter(string? warehouseCode)
    {
        if (string.IsNullOrEmpty(warehouseCode))
            return string.Empty;

        return $" and sklad.kod eq \"{warehouseCode}\"";
    }

    private string GetDocumentTypeFilter(int? documentTypeId)
    {
        if (!documentTypeId.HasValue)
            return string.Empty;

        return $" and typDokl eq {documentTypeId.Value}";
    }
}
```

**Step 2: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/StockMovement/StockMovementRequest.cs
git commit -m "feat: add StockMovementRequest query builder"
```

---

## Task 6: Create CreateStockMovementFlexiDto (Create/Update Request)

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/CreateStockMovementFlexiDto.cs`

**Step 1: Create request DTO**

```csharp
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class CreateStockMovementFlexiDto
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public int? Id { get; set; }

    [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
    public string Code { get; set; }

    [JsonProperty("datVyst")]
    public DateTime IssueDate { get; set; }

    [JsonProperty("datUcto", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? AccountingDate { get; set; }

    [JsonProperty("typPohybuK")]
    public string MovementTypeRaw => $"typPohybu.{(Direction == StockMovementDirection.In ? "prijem" : "vydej")}";

    [JsonIgnore]
    public StockMovementDirection Direction { get; set; }

    [JsonProperty("typPohybuSkladK")]
    public string StockMovementTypeRaw { get; set; }

    [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string Note { get; set; }

    [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
    public bool? WithoutItems { get; set; }

    [JsonProperty("nazFirmy", NullValueHandling = NullValueHandling.Ignore)]
    public string CompanyName { get; set; }

    [JsonProperty("typDokl")]
    public string DocumentTypeCode { get; set; }

    [JsonProperty("sklad")]
    public string WarehouseCode { get; set; }

    [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
    public string CurrencyCode { get; set; }

    [JsonProperty("stredisko", NullValueHandling = NullValueHandling.Ignore)]
    public string DepartmentCode { get; set; }

    [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
    public string VariableSymbol { get; set; }

    [JsonProperty("cisObj", NullValueHandling = NullValueHandling.Ignore)]
    public string OrderNumber { get; set; }

    [JsonProperty("cisDodak", NullValueHandling = NullValueHandling.Ignore)]
    public string DeliveryNote { get; set; }

    [JsonProperty("skladovePolozky", NullValueHandling = NullValueHandling.Ignore)]
    public List<CreateStockMovementItemFlexiDto> Items { get; set; }
}
```

**Step 2: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded (may have error about CreateStockMovementItemFlexiDto - we'll create it next)

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/StockMovement/CreateStockMovementFlexiDto.cs
git commit -m "feat: add CreateStockMovementFlexiDto request model"
```

---

## Task 7: Create CreateStockMovementItemFlexiDto

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/CreateStockMovementItemFlexiDto.cs`

**Step 1: Create item request DTO**

```csharp
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class CreateStockMovementItemFlexiDto
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public int? Id { get; set; }

    [JsonProperty("cenik")]
    public string ProductCode { get; set; }

    [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
    public string WarehouseCode { get; set; }

    [JsonProperty("mnozMj")]
    public decimal Quantity { get; set; }

    [JsonProperty("cenaMj", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? PricePerUnit { get; set; }

    [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
    public string Note { get; set; }

    [JsonProperty("sarze", NullValueHandling = NullValueHandling.Ignore)]
    public string Batch { get; set; }

    [JsonProperty("expirace", NullValueHandling = NullValueHandling.Ignore)]
    public string Expiration { get; set; }
}
```

**Step 2: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/StockMovement/CreateStockMovementItemFlexiDto.cs
git commit -m "feat: add CreateStockMovementItemFlexiDto"
```

---

## Task 8: Create Request Envelope

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/StockMovement/CreateStockMovementEnvelopeFlexiDto.cs`

**Step 1: Create envelope wrapper**

```csharp
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockMovement;

public class CreateStockMovementEnvelopeFlexiDto
{
    public CreateStockMovementEnvelopeFlexiDto(CreateStockMovementFlexiDto stockMovement)
    {
        StockMovements = new List<CreateStockMovementFlexiDto> { stockMovement };
    }

    [JsonProperty("winstrom")]
    public WinstromEnvelope Winstrom { get; set; } = new();

    public class WinstromEnvelope
    {
        [JsonProperty("skladovy-pohyb")]
        public List<CreateStockMovementFlexiDto> StockMovements { get; set; }

        [JsonProperty("@version")]
        public string Version { get; set; } = "1.0";
    }

    [JsonIgnore]
    private List<CreateStockMovementFlexiDto> StockMovements
    {
        set => Winstrom.StockMovements = value;
    }
}
```

**Step 2: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Model/`

Expected: Build succeeded

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/StockMovement/CreateStockMovementEnvelopeFlexiDto.cs
git commit -m "feat: add CreateStockMovementEnvelopeFlexiDto wrapper"
```

---

## Task 9: Create IStockMovementClient Interface

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Client/Clients/Products/StockMovement/IStockMovementClient.cs`

**Step 1: Create interface**

```csharp
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public interface IStockMovementClient
{
    /// <summary>
    /// Get stock movement by ID
    /// </summary>
    Task<StockMovementFlexiDto?> GetAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get stock movement by code
    /// </summary>
    Task<StockMovementFlexiDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get stock movements by date range with optional filters
    /// </summary>
    Task<IReadOnlyList<StockMovementFlexiDto>> GetAsync(
        DateTime dateFrom,
        DateTime dateTo,
        StockMovementDirection? direction = null,
        string? warehouseCode = null,
        int? documentTypeId = null,
        int limit = 0,
        int skip = 0,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new stock movement
    /// </summary>
    Task<OperationResult<OperationResultDetail>> SaveAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update existing stock movement
    /// </summary>
    Task<OperationResult<OperationResultDetail>> UpdateAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default);
}
```

**Step 2: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Client/`

Expected: Build succeeded

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Client/Clients/Products/StockMovement/IStockMovementClient.cs
git commit -m "feat: add IStockMovementClient interface"
```

---

## Task 10: Implement StockMovementClient

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Client/Clients/Products/StockMovement/StockMovementClient.cs`

**Step 1: Create client implementation**

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;

public class StockMovementClient : ResourceClient, IStockMovementClient
{
    public StockMovementClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<StockMovementClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.StockMovement;
    protected override string? RequestIdentifier => null;

    public async Task<StockMovementFlexiDto?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var request = new StockMovementRequest(id);
        var query = new FlexiQuery();
        var result = await PostAsync<StockMovementRequest, StockMovementResult>(
            request,
            query,
            cancellationToken: cancellationToken);

        return result?.Result?.StockMovements?.FirstOrDefault();
    }

    public async Task<StockMovementFlexiDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var request = new StockMovementRequest(code);
        var query = new FlexiQuery();
        var result = await PostAsync<StockMovementRequest, StockMovementResult>(
            request,
            query,
            cancellationToken: cancellationToken);

        return result?.Result?.StockMovements?.FirstOrDefault();
    }

    public async Task<IReadOnlyList<StockMovementFlexiDto>> GetAsync(
        DateTime dateFrom,
        DateTime dateTo,
        StockMovementDirection? direction = null,
        string? warehouseCode = null,
        int? documentTypeId = null,
        int limit = 0,
        int skip = 0,
        CancellationToken cancellationToken = default)
    {
        var request = new StockMovementRequest(dateFrom, dateTo, direction, warehouseCode, documentTypeId)
        {
            Limit = limit,
            Start = skip
        };

        var query = new FlexiQuery();
        var result = await PostAsync<StockMovementRequest, StockMovementResult>(
            request,
            query,
            cancellationToken: cancellationToken);

        return result?.Result?.StockMovements ?? new List<StockMovementFlexiDto>();
    }

    public Task<OperationResult<OperationResultDetail>> SaveAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default)
    {
        var envelope = new CreateStockMovementEnvelopeFlexiDto(stockMovement);
        return PostAsync(envelope, cancellationToken: cancellationToken);
    }

    public Task<OperationResult<OperationResultDetail>> UpdateAsync(
        CreateStockMovementFlexiDto stockMovement,
        CancellationToken cancellationToken = default)
    {
        var envelope = new CreateStockMovementEnvelopeFlexiDto(stockMovement);
        return PostAsync(envelope, cancellationToken: cancellationToken);
    }
}
```

**Step 2: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Client/`

Expected: Build succeeded

**Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Client/Clients/Products/StockMovement/StockMovementClient.cs
git commit -m "feat: implement StockMovementClient"
```

---

## Task 11: Register in Dependency Injection

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Client/DI/ServiceCollectionExtensions.cs:35`

**Step 1: Add using statement**

Add after line 14:

```csharp
using Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;
```

**Note:** This line might already exist - verify first with grep.

**Step 2: Add client registration**

Add after line 35 (after `IStockItemsMovementClient` registration):

```csharp
services.AddSingleton<IStockMovementClient, StockMovementClient>();
```

**Step 3: Verify the change**

Run: `grep -n "IStockMovementClient" src/Rem.FlexiBeeSDK.Client/DI/ServiceCollectionExtensions.cs`

Expected: Shows line with registration

**Step 4: Build to verify**

Run: `dotnet build src/Rem.FlexiBeeSDK.Client/`

Expected: Build succeeded

**Step 5: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Client/DI/ServiceCollectionExtensions.cs
git commit -m "feat: register StockMovementClient in DI"
```

---

## Task 12: Build Entire Solution

**Files:**
- None (verification only)

**Step 1: Clean previous builds**

Run: `dotnet clean`

Expected: Clean succeeded

**Step 2: Build entire solution**

Run: `dotnet build`

Expected: Build succeeded with no errors

**Step 3: Verify all projects built**

Run: `dotnet build --no-incremental`

Expected: All projects build successfully

---

## Task 13: Manual Testing Setup (Optional)

**Files:**
- Create: `examples/StockMovementExample.cs` (if examples directory exists)

**Step 1: Create example usage file**

```csharp
using System;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Client.Clients.Products.StockMovement;
using Rem.FlexiBeeSDK.Model.Products.StockMovement;

namespace Rem.FlexiBeeSDK.Examples;

public class StockMovementExample
{
    private readonly IStockMovementClient _client;

    public StockMovementExample(IStockMovementClient client)
    {
        _client = client;
    }

    public async Task GetByIdExample()
    {
        var movement = await _client.GetAsync(89204);
        Console.WriteLine($"Movement: {movement?.Code} - {movement?.Description}");
    }

    public async Task GetByCodeExample()
    {
        var movement = await _client.GetByCodeAsync("P-00036/2026");
        Console.WriteLine($"Movement: {movement?.Code} - Total: {movement?.TotalAmount}");
    }

    public async Task GetByDateRangeExample()
    {
        var movements = await _client.GetAsync(
            new DateTime(2026, 1, 1),
            new DateTime(2026, 1, 31),
            direction: StockMovementDirection.In,
            limit: 10
        );

        Console.WriteLine($"Found {movements.Count} movements");
    }

    public async Task CreateExample()
    {
        var newMovement = new CreateStockMovementFlexiDto
        {
            IssueDate = DateTime.Today,
            Direction = StockMovementDirection.In,
            StockMovementTypeRaw = "typPohybuSklad.prijemHoly",
            Description = "Test stock movement",
            DocumentTypeCode = "code:YOUR-TYPE",
            WarehouseCode = "code:YOUR-WAREHOUSE",
            WithoutItems = false
        };

        var result = await _client.SaveAsync(newMovement);
        Console.WriteLine($"Created: {result.Success}");
    }
}
```

**Step 2: Commit example**

```bash
git add examples/StockMovementExample.cs
git commit -m "docs: add StockMovementClient usage example"
```

---

## Task 14: Final Verification

**Files:**
- None (verification only)

**Step 1: Verify all models exist**

Run:
```bash
ls -la src/Rem.FlexiBeeSDK.Model/Products/StockMovement/ | grep -E "(StockMovement|CreateStockMovement)"
```

Expected: All model files listed

**Step 2: Verify client exists**

Run:
```bash
ls -la src/Rem.FlexiBeeSDK.Client/Clients/Products/StockMovement/ | grep "StockMovementClient"
```

Expected: Both interface and implementation files

**Step 3: Verify DI registration**

Run:
```bash
grep "IStockMovementClient" src/Rem.FlexiBeeSDK.Client/DI/ServiceCollectionExtensions.cs
```

Expected: Registration line found

**Step 4: Final build**

Run: `dotnet build --configuration Release`

Expected: Build succeeded

**Step 5: Check for warnings**

Review build output for any warnings that should be addressed.

---

## Summary

This plan implements complete CRUD operations (except DELETE - not supported by API) for FlexiBee stock movement headers:

**Implemented:**
- ✅ Read operations: by ID, by code, by date range with filters
- ✅ Create operation: new stock movements with items
- ✅ Update operation: modify existing stock movements
- ✅ Full model support with nested DTOs
- ✅ Dependency injection registration
- ✅ Follows existing SDK patterns

**Not Implemented:**
- ❌ Delete operation (API limitation: `@deletable: false`)
- ❌ Unit tests (should be added in separate task)
- ❌ Integration tests (should be added in separate task)

**Next Steps:**
- Add unit tests for StockMovementClient
- Add integration tests with test FlexiBee instance
- Update documentation/README
- Consider adding helper methods for common operations
