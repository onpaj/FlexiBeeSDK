# BoM UpdateIngredientAmountAsync Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add `UpdateIngredientAmountAsync(productCode, ingredientCode, newAmount)` to `IBoMClient` / `BoMClient` so Heblo can stop manually constructing FlexiBee HTTP requests.

**Architecture:** New request model carries the PUT payload. `BoMClient` fetches the BoM, locates the ingredient by code (stripping the `code:` prefix FlexiBee returns), then delegates the PUT to the existing `ResourceClient.PutAsync` infrastructure — same pattern as `RecalculatePurchasePrice`. Versioning is automatic via GitVersion / CI-CD on push.

**Tech Stack:** C# / netstandard2.0, Newtonsoft.Json, xUnit + FluentAssertions (integration tests against live FlexiBee)

---

## File Map

| Action | Path | Responsibility |
|--------|------|----------------|
| Create | `src/Rem.FlexiBeeSDK.Model/Products/UpdateBoMIngredientAmountRequest.cs` | PUT payload DTO |
| Modify | `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs` | New method on interface |
| Modify | `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs` | Implementation + private prefix-strip helper |
| Modify | `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs` | Integration tests |

---

### Task 1: Add the PUT request model

**Files:**
- Create: `src/Rem.FlexiBeeSDK.Model/Products/UpdateBoMIngredientAmountRequest.cs`

- [ ] **Step 1: Create the file**

```csharp
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products;

public class UpdateBoMIngredientAmountRequest
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("mnozstvi")]
    public double Amount { get; set; }
}
```

- [ ] **Step 2: Build to confirm it compiles**

Run from repo root:
```bash
dotnet build src/Rem.FlexiBeeSDK.Model/Rem.FlexiBeeSDK.Model.csproj
```
Expected: `Build succeeded.`

- [ ] **Step 3: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Model/Products/UpdateBoMIngredientAmountRequest.cs
git commit -m "feat: add UpdateBoMIngredientAmountRequest model"
```

---

### Task 2: Add method to interface and implement in BoMClient

**Files:**
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs`
- Modify: `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs`

- [ ] **Step 1: Add method to `IBoMClient`**

Open `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs` and add the new method:

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
    }
}
```

- [ ] **Step 2: Implement in `BoMClient`**

Open `src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs`. Add the new method and a private helper **before the closing `}`** of the class:

```csharp
        public async Task UpdateIngredientAmountAsync(
            string productCode,
            string ingredientCode,
            double newAmount,
            CancellationToken cancellationToken = default)
        {
            var bom = await GetAsync(productCode, cancellationToken);

            var ingredient = bom.FirstOrDefault(item =>
                item.Level != 1 &&
                StripCodePrefix(item.IngredientCode) == ingredientCode);

            if (ingredient == null)
                throw new InvalidOperationException(
                    $"Ingredient '{ingredientCode}' not found in BoM for product '{productCode}'");

            var document = new Dictionary<string, object>
            {
                {
                    ResourceIdentifier,
                    new UpdateBoMIngredientAmountRequest { Id = ingredient.Id, Amount = newAmount }
                }
            };

            await PutAsync(document, cancellationToken: cancellationToken);
        }

        private static string StripCodePrefix(string code)
        {
            if (code == null) return null;
            var trimmed = code.Trim();
            return trimmed.StartsWith("code:") ? trimmed.Substring(5).Trim() : trimmed;
        }
```

Also add the missing `using` at the top of `BoMClient.cs`:

```csharp
using Rem.FlexiBeeSDK.Model.Products;
```

- [ ] **Step 3: Build to confirm it compiles**

```bash
dotnet build src/Rem.FlexiBeeSDK.Client/Rem.FlexiBeeSDK.Client.csproj
```
Expected: `Build succeeded.`

- [ ] **Step 4: Commit**

```bash
git add src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/IBoMClient.cs
git add src/Rem.FlexiBeeSDK.Client/Clients/Products/BoM/BoMClient.cs
git commit -m "feat: implement UpdateIngredientAmountAsync on BoMClient"
```

---

### Task 3: Integration tests

**Files:**
- Modify: `test/Rem.FlexiBeeSDK.Tests/BoMTests.cs`

The existing tests call a real FlexiBee server. These two tests follow the same pattern. `KRE003001M` is already used in `GetKusovnik`; the happy-path test fetches its BoM first to find a real ingredient code and current amount, then updates with the same value (no net change, safe to run repeatedly).

- [ ] **Step 1: Add the two tests to `BoMTests.cs`**

Add inside the `BoMTests` class, after the existing `GetWeightShouldBeNull` test:

```csharp
        [Fact]
        public async Task UpdateIngredientAmount_ShouldSucceed()
        {
            var client = _fixture.Create<BoMClient>();

            // Fetch current BoM to get a real ingredient and its current amount
            var bom = await client.GetAsync("KRE003001M");
            var ingredient = bom.FirstOrDefault(i => i.Level != 1);
            ingredient.Should().NotBeNull("KRE003001M must have at least one non-header BoM item");

            var ingredientCode = ingredient!.IngredientCode;
            if (ingredientCode.StartsWith("code:"))
                ingredientCode = ingredientCode.Substring(5).Trim();

            // Update with the same amount — no net change, safe to run repeatedly
            var act = async () => await client.UpdateIngredientAmountAsync(
                "KRE003001M",
                ingredientCode,
                ingredient.Amount);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task UpdateIngredientAmount_IngredientNotFound_ShouldThrow()
        {
            var client = _fixture.Create<BoMClient>();

            var act = async () => await client.UpdateIngredientAmountAsync(
                "KRE003001M",
                "DOES_NOT_EXIST",
                1.0);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*DOES_NOT_EXIST*");
        }
```

- [ ] **Step 2: Build the test project**

```bash
dotnet build test/Rem.FlexiBeeSDK.Tests/Rem.FlexiBeeSDK.Tests.csproj
```
Expected: `Build succeeded.`

- [ ] **Step 3: Run only the new tests**

Requires user secrets configured with `FlexiBeeSettings` (same as existing integration tests).

```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/ \
  --filter "FullyQualifiedName~UpdateIngredientAmount" \
  --logger "console;verbosity=normal"
```
Expected: 2 tests pass.

- [ ] **Step 4: Commit**

```bash
git add test/Rem.FlexiBeeSDK.Tests/BoMTests.cs
git commit -m "test: integration tests for UpdateIngredientAmountAsync"
```

---

### Task 4: Update Heblo to use the new SDK method

> **Context:** Work in `Anela.Heblo` repo, branch `feat/manufacture-residue-distribution`.

**Files:**
- Modify: `backend/src/Adapters/Anela.Heblo.Adapters.Flexi/Manufacture/FlexiManufactureClient.cs`
- Modify: `backend/src/Adapters/Anela.Heblo.Adapters.Flexi/Anela.Heblo.Adapters.Flexi.csproj`

- [ ] **Step 1: Update NuGet package version in Heblo**

After the SDK is published (push to main triggers CI/CD via GitVersion), update the reference in `Anela.Heblo.Adapters.Flexi.csproj`:

```xml
<PackageReference Include="Rem.FlexiBeeSDK.Client" Version="0.1.123" />
```

Run:
```bash
dotnet restore backend/src/Adapters/Anela.Heblo.Adapters.Flexi/Anela.Heblo.Adapters.Flexi.csproj
```
Expected: package restored at new version.

- [ ] **Step 2: Replace manual HTTP code in `FlexiManufactureClient`**

Find `UpdateBoMIngredientAmountAsync` (around line 615) and replace the entire method body:

```csharp
    public async Task UpdateBoMIngredientAmountAsync(
        string productCode,
        string ingredientCode,
        double newAmount,
        CancellationToken cancellationToken = default)
    {
        await _bomClient.UpdateIngredientAmountAsync(productCode, ingredientCode, newAmount, cancellationToken);
    }
```

- [ ] **Step 3: Remove unused `_httpClientFactory` if no other method uses it**

Search `FlexiManufactureClient.cs` for any remaining usage of `_httpClientFactory`. If none found, remove the field declaration and constructor parameter:

```bash
grep -n "_httpClientFactory" backend/src/Adapters/Anela.Heblo.Adapters.Flexi/Manufacture/FlexiManufactureClient.cs
```

If the only hits were in `UpdateBoMIngredientAmountAsync` (now replaced), remove the field and constructor injection. Also remove unused `using` statements (`System.Net.Http.Headers`, `System.Text`, `System.Text.Json`) if they were only used by the old method.

- [ ] **Step 4: Build Heblo backend**

```bash
dotnet build backend/Anela.Heblo.sln
```
Expected: `Build succeeded. 0 Error(s).`

- [ ] **Step 5: Run Heblo backend tests**

```bash
dotnet test backend/Anela.Heblo.sln --filter "Category!=Integration"
```
Expected: all existing tests pass (no changes to mocked interface).

- [ ] **Step 6: Commit**

```bash
git add backend/src/Adapters/Anela.Heblo.Adapters.Flexi/Manufacture/FlexiManufactureClient.cs
git add backend/src/Adapters/Anela.Heblo.Adapters.Flexi/Anela.Heblo.Adapters.Flexi.csproj
git commit -m "refactor: replace manual FlexiBee HTTP call with SDK UpdateIngredientAmountAsync"
```
