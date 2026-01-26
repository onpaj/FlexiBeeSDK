# FlexiBee SDK Documentation

Dokumentace pro .NET SDK pro integraci se systémem Abra FlexiBee. Tato dokumentace je určena primárně pro zpracování AI agenty v jiných projektech.

## Pro AI agenty v consumer projektech

Pokud jsi AI agent pracující v projektu, který používá FlexiBeeSDK NuGet package, tato dokumentace ti poskytne:
- Kompletní seznam dostupných klientů a jejich metod
- Příklady použití pro běžné operace
- FlexiBee API resource názvy pro debugging a troubleshooting
- Datové modely a jejich FlexiBee property mapping

**Kde najít tuto dokumentaci:**
- V NuGet cache: `~/.nuget/packages/rem.flexibeesdk.client/*/content/FlexiBeeSDK_Documentation.md`
- V projektu (pokud byla zkopírována): hledej soubor `FlexiBeeSDK_Documentation.md`

**Jak přidat do consumer CLAUDE.md:**
```markdown
## NuGet Package Documentation

Projekt používá FlexiBee SDK. Dokumentace je dostupná v NuGet cache:
~/.nuget/packages/rem.flexibeesdk.client/*/content/FlexiBeeSDK_Documentation.md

Když pracuješ s FlexiBee typy (IIssuedInvoiceClient, IContactClient, QueryBuilder, atd.),
přečti tuto dokumentaci pro pochopení API.
```

## Přehled SDK

FlexiBee SDK poskytuje typovaný .NET wrapper pro Abra FlexiBee API, ekonomický a skladový systém. SDK se skládá ze tří hlavních komponent:

- **Rem.FlexiBeeSDK.Model** - datové modely a response typy
- **Rem.FlexiBeeSDK.Client** - HTTP klienti a komunikační logika
- **Rem.FlexiBeeSDK.Tests** - unit testy

## Kompletní API Reference

Tabulka všech dostupných klientů v SDK:

| Interface | FlexiBee Resource | Hlavní metody | Popis |
|-----------|-------------------|---------------|-------|
| `IIssuedInvoiceClient` | `faktura-vydana` | Get, Save | Vydané faktury |
| `IReceivedInvoiceClient` | `faktura-prijata` | Get, Search, AddTag, RemoveTag, GetTags | Přijaté faktury |
| `IIssuedOrdersClient` | `objednavka-vydana` | Get, GetByCode, Save, Finalize | Vydané objednávky |
| `IContactClient` | `kontakt` | Get, GetById | Kontakty (podle kódu nebo IČ) |
| `IContactListClient` | `adresar` | Get | Seznam kontaktů s filtry |
| `IBankClient` | `banka` | UnPairPayment | Bankovní operace |
| `IBankAccountClient` | `bankovni-ucet` | ImportStatement | Bankovní účty |
| `IBoMClient` | `kusovnik` | Get, GetByIngredient, RecalculatePurchasePrice, GetBomWeight | Kusovníky |
| `IPriceListClient` | `cenik` | Save | Ceníky |
| `IProductSetsClient` | Product sets | Get | Product sets |
| `IStockToDateClient` | `stav-skladu-k-datu` | Get | Stavy skladu k datu |
| `IStockMovementClient` | `skladovy-pohyb` | Get, GetByCode, Save, Update | Hlavičky skladových pohybů |
| `IStockItemsMovementClient` | `skladovy-pohyb-polozka` | Get, Save | Položky skladových pohybů |
| `ILotsClient` | `sarze-expirace` | Get | Šarže/expirace |
| `IStockTakingClient` | `inventura` | GetHeader, CreateHeader, AddMissingLots, Recompute, Submit | Hlavičky inventur |
| `IStockTakingItemsClient` | `inventura-polozka` | GetStockTakings, AddStockTakings | Položky inventur |
| `ILedgerClient` | Účetní kniha | Get | Účetní položky |
| `IAccountingTemplateClient` | Účetní předpisy | Get, UpdateInvoice | Účetní předpisy |
| `IDepartmentClient` | Střediska | Get | Střediska |
| `IUserQueryClient<T>` | Generické | Get | User-defined queries |

## Konfigurace a Setup

### 1. Dependency Injection Setup

```csharp
// V Startup.cs nebo Program.cs
services.AddFlexiBee(configuration);
```

### 2. Konfigurace FlexiBeeSettings

```json
{
  "FlexiBeeSettings": {
    "Server": "https://your-flexibee-server.com",
    "Company": "your-company-code",
    "Login": "username",
    "Password": "password"
  }
}
```

### 3. Dostupné klienti po registraci

- `IIssuedInvoiceClient` - vydané faktury
- `IReceivedInvoiceClient` - přijaté faktury
- `IBankClient` - bankovní operace
- `IBankAccountClient` - bankovní účty
- `IContactClient` - kontakty/firmy
- `IBoMClient` - kusovníky
- `IStockToDateClient` - stavy skladu k datu
- `IStockTakingClient` - inventury
- `ILotsClient` - šarže/expirace

## Query System

### QueryBuilder Použití

```csharp
// Základní query
var query = new QueryBuilder()
    .WithFullDetail()
    .WithRelation(Relations.Items)
    .WithLimit(100)
    .ByCode("INV-001")
    .Build();

// Custom detail query
var customQuery = new QueryBuilder()
    .WithCustomDetail("id,kod,nazev,cenik")
    .WithNoLimit()
    .Raw("(stav='active')")
    .Build();
```

### Query Metody

- `Raw(string)` - surový query string
- `ByCode(string)` - filtr podle kódu
- `ById(int)` - filtr podle ID
- `WithFullDetail()` - plné detaily entity
- `WithCustomDetail(string)` - vlastní výběr polí
- `WithRelation(Relations)` - včetně souvisejících dat
- `WithLimit(int)` / `WithNoLimit()` - omezení počtu výsledků
- `WithParameter(key, value)` - vlastní parametry

### Relations

```csharp
Relations.Items        // "polozkyDokladu" - položky dokladu
Relations.References   // "vazby" - vazby na jiné doklady  
Relations.ReferenceDocs // "vazbaDokl" - vazby na dokumenty
```

## Klienti a jejich funkce

### 1. IssuedInvoiceClient (Vydané faktury)

**FlexiBee Resource**: `faktura-vydana`

```csharp
// Získání faktury podle kódu
var invoice = await client.GetAsync("FAV-2024-001", cancellationToken);

// Uložení faktury
var result = await client.SaveAsync(invoice, cancellationToken);
```

**Automaticky načítá**: položky (`polozkyDokladu`) a vazby (`vazby`)

### 2. ReceivedInvoiceClient (Přijaté faktury)

**FlexiBee Resource**: `faktura-prijata`

```csharp
// Získání přijaté faktury (read-only)
var invoice = await client.GetAsync("FAP-2024-001", cancellationToken);
```

**Automaticky načítá**: položky (`polozkyDokladu`)

### 3. ContactClient (Kontakty)

**FlexiBee Resource**: `kontakt`

```csharp
// Podle kódu
var contact = await client.GetAsync("DODAV-001", cancellationToken);

// Podle IČ
var contact = await client.GetByIdAsync("12345678", cancellationToken);
```

### 4. BankClient (Bankovní operace)

**FlexiBee Resource**: `banka`

```csharp
// Odpárování platby
var result = await client.UnPairPayment(paymentId, cancellationToken);
```

### 5. BankAccountClient (Bankovní účty)

**FlexiBee Resource**: `bankovni-ucet`

```csharp  
// Import bankovního výpisu
var result = await client.ImportStatement(accountId, statementData);
```

**Speciální**: podporuje ISO-8859-2 encoding pro české bankovní formáty

### 6. BoMClient (Kusovníky)

**FlexiBee Resource**: `kusovnik`

```csharp
// Kusovník podle kódu nadřazeného produktu
var bomItems = await client.GetAsync("PROD-001", cancellationToken);

// Kusovník kde je produkt ingrediencí
var bomItems = await client.GetByIngredientAsync("INGR-001", cancellationToken);

// Přepočet nákupní ceny
var success = await client.RecalculatePurchasePrice(bomId, cancellationToken);
```

### 7. StockToDateClient (Stavy skladu k datu)

**FlexiBee Resource**: `stav-skladu-k-datu`

```csharp
// Stav skladu k určitému datu
var stockItems = await client.GetAsync(
    date: DateTime.Now,
    warehouseId: 1,
    limit: 1000,
    skip: 0,
    cancellationToken
);
```

**Transformace**: FlexiBee komplexní strukturu → `StockToDateSummary`

### 8. StockTakingClient (Inventury)

**FlexiBee Resource**: `inventura`

```csharp
// Získání hlavičky inventury
var header = await client.GetHeaderAsync(headerId, cancellationToken);

// Vytvoření nové inventury
var newHeader = await client.CreateHeaderAsync(request, cancellationToken);

// Přidání chybějících šarží
await client.AddMissingLotsAsync(headerId, productIds, cancellationToken);

// Přepočet stavů
await client.RecomputeAsync(headerId, cancellationToken);

// Generování dokladů
await client.SubmitAsync(headerId, documentTypeId, cancellationToken);
```

### 9. StockTakingItemsClient (Položky inventury)

**FlexiBee Resource**: `inventura-polozka`

```csharp
// Položky inventury
var items = await client.GetStockTakingsAsync(headerId, cancellationToken);

// Přidání položek
await client.AddStockTakingsAsync(headerId, warehouseId, items, cancellationToken);
```

### 10. LotsClient (Šarže/Expirace)

**FlexiBee Resource**: `sarze-expirace`

```csharp
// Šarže produktu s paginací
var lots = await client.GetAsync(
    productCode: "PROD-001",
    limit: 100,
    skip: 0,
    cancellationToken
);
```

**Transformace**: `LotsItem` → `ProductLot` s čištěním prefixů

### 11. StockMovementClient (Skladové pohyby - hlavičky)

**FlexiBee Resource**: `skladovy-pohyb`

```csharp
// Získání skladového pohybu podle ID
var movement = await client.GetAsync(123, cancellationToken);

// Získání podle kódu
var movement = await client.GetByCodeAsync("SP-2024-001", cancellationToken);

// Seznam pohybů s filtry
var movements = await client.GetAsync(
    dateFrom: DateTime.Now.AddMonths(-1),
    dateTo: DateTime.Now,
    direction: StockMovementDirection.In, // nebo Out
    warehouseCode: "SKLAD-01",
    documentTypeId: 5,
    limit: 100,
    skip: 0,
    cancellationToken
);

// Vytvoření nového pohybu
var result = await client.SaveAsync(newMovement, cancellationToken);

// Aktualizace existujícího
var result = await client.UpdateAsync(movement, cancellationToken);
```

**Důležité**: Pro vytvoření kompletního skladového pohybu použijte `StockMovementClient` pro hlavičku a `StockItemsMovementClient` pro položky.

### 12. StockItemsMovementClient (Skladové pohyby - položky)

**FlexiBee Resource**: `skladovy-pohyb-polozka`

```csharp
// Položky pohybu podle data a směru
var items = await client.GetAsync(
    dateFrom: DateTime.Now.AddMonths(-1),
    dateTo: DateTime.Now,
    direction: StockMovementDirection.In,
    storeCode: "SKLAD-01",
    documentTypeId: null,
    documentCode: null,
    limit: 100,
    skip: 0,
    cancellationToken
);

// Položky konkrétního dokladu
var items = await client.GetAsync(documentId: 123, cancellationToken);

// Uložení položek
var result = await client.SaveAsync(itemsRequest, cancellationToken);
```

### 13. IssuedOrdersClient (Vydané objednávky)

**FlexiBee Resource**: `objednavka-vydana`

```csharp
// Seznam objednávek
var orders = await client.GetAsync(
    dateFrom: DateTime.Now.AddMonths(-1),
    dateTo: DateTime.Now,
    documentTypeId: null,
    limit: 100,
    skip: 0,
    cancellationToken
);

// Podle kódu
var order = await client.GetByCodeAsync("OBJ-2024-001", cancellationToken);

// Podle ID
var order = await client.GetAsync(123, cancellationToken);

// Vytvoření objednávky
var result = await client.SaveAsync(newOrder, cancellationToken);

// Finalizace objednávky
var result = await client.FinalizeAsync(finalizeRequest, cancellationToken);
```

### 14. ContactListClient (Seznam kontaktů)

**FlexiBee Resource**: `adresar`

```csharp
// Seznam kontaktů podle typu s paginací
var contacts = await client.GetAsync(
    contactTypes: new[] { ContactType.Customer, ContactType.Supplier },
    limit: 100,
    skip: 0,
    cancellationToken
);
```

**Rozdíl od ContactClient**: `ContactListClient` načítá seznamy kontaktů s filtry, zatímco `ContactClient` načítá jednotlivé kontakty podle kódu nebo IČ.

### 15. LedgerClient (Účetní kniha)

**FlexiBee Resource**: Účetní kniha

```csharp
// Účetní položky podle období a účtů
var items = await client.GetAsync(
    dateFrom: DateTime.Now.AddMonths(-1),
    dateTo: DateTime.Now,
    debitAccountPrefixes: new[] { "311", "321" },
    creditAccountPrefix: new[] { "601", "602" },
    departmentId: "STR-01",
    limit: 1000,
    skip: 0,
    cancellationToken
);
```

### 16. AccountingTemplateClient (Účetní předpisy)

**FlexiBee Resource**: Účetní předpisy

```csharp
// Všechny účetní předpisy
var templates = await client.GetAsync(cancellationToken);

// Aktualizace faktury s účetním předpisem
var result = await client.UpdateInvoiceAsync(
    invoiceCode: "FAV-2024-001",
    accountingTemplateCode: "PREDPIS-01",
    departmentCode: "STR-01",
    cancellationToken
);
```

### 17. DepartmentClient (Střediska)

**FlexiBee Resource**: Střediska

```csharp
// Všechna střediska
var departments = await client.GetAsync(cancellationToken);
```

### 18. ProductSetsClient (Product Sets)

**FlexiBee Resource**: Product sets

```csharp
// Product sets pro produkt s paginací
var sets = await client.GetAsync(
    productCode: "PROD-001",
    limit: 100,
    skip: 0,
    cancellationToken
);
```

### 19. UserQueryClient&lt;T&gt; (Generické user queries)

**FlexiBee Resource**: Generické

```csharp
// Vlastní query s parametry
var results = await client.GetAsync(
    queryParameters: new Dictionary<string, string>
    {
        { "start", "0" },
        { "limit", "100" },
        { "detail", "full" }
    },
    cancellationToken
);
```

**Použití**: Pro flexibilní queries kdy potřebujete plnou kontrolu nad parametry.

## Datové modely

### Core Business Entities

#### Contact
```csharp
public class Contact
{
    public string Id { get; set; }
    public string Code { get; set; }      // Kód kontaktu
    public string Name { get; set; }      // Název
    public string CIN { get; set; }       // IČ
    public string VATIN { get; set; }     // DIČ
    public string Street { get; set; }    // Ulice
    public string City { get; set; }      // Město
    public string ZipCode { get; set; }   // PSČ
    public string Country { get; set; }   // Země
}
```

#### IssuedInvoice (implementuje IValidate)
```csharp
public class IssuedInvoice : IValidate
{
    // Identifikátory
    public string Id { get; set; }
    public string Code { get; set; }           // Kód faktury
    public string VarSymbol { get; set; }      // Variabilní symbol
    public string OrderNumber { get; set; }    // Číslo objednávky
    
    // Finanční údaje
    public decimal SumTotal { get; set; }      // Celková suma
    public decimal ToPay { get; set; }         // K úhradě
    public decimal SumPrePayment { get; set; } // Záloha
    
    // Datumy
    public DateTime DateCreated { get; set; }  // Datum vytvoření
    public DateTime DateDue { get; set; }      // Datum splatnosti
    
    // Položky a vazby
    public List<IssuedInvoiceItem> Items { get; set; }
    public List<InvoiceReference> References { get; set; }
    
    // Validační logika
    public void Validate() { /* komplexní business pravidla */ }
}
```

#### Product (s novými vlastnostmi)
```csharp
public class Product
{
    public string Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    
    // Nové vlastnosti (volume, weight)
    public decimal? Volume { get; set; }       // Objem
    public decimal? Weight { get; set; }       // Hmotnost
    
    // Tracking
    public bool HasLots { get; set; }          // Sledování šarží
    public bool HasExpiration { get; set; }    // Sledování expirace
}
```

### Response Models

#### OperationResult&lt;T&gt;
```csharp
public class OperationResult<TData>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess => (int)StatusCode < 300;
    public TData Data { get; set; }
    public string ErrorMessage { get; set; }
}
```

#### FlexiBee API Response Envelope
```csharp
// Standardní FlexiBee response
{
  "winstrom": {
    "success": "true",
    "stats": {
      "created": 1,
      "updated": 0,
      "deleted": 0
    },
    "results": [...]
  }
}
```

## Error Handling a Result Filters

### Result Filters Pipeline

SDK implementuje pipeline pro zpracování odpovědí:

1. **AlreadyPairedResultFilter** - detekce již spárovaných plateb
2. **UnknownProductResultFilter** - zpracování neznámých produktů
3. **ParseErrorResultFilter** - zpracování chyb parsování

### Error Types

```csharp
public enum ErrorType
{
    InvoicePaired,      // Faktura už je spárována
    ProductNotFound,    // Produkt nenalezen
    ValidationError     // Validační chyba
}
```

### Exception Handling

- `KeyNotFoundException` - entita nenalezena
- `InvalidOperationException` - neplatná operace
- `ValidationException` - validační chyba

## Testování

### Test Setup s FlexiFixture

```csharp
public class FlexiFixture
{
    public static IFixture Setup()
    {
        // Načte konfiguraci z user secrets
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<FlexiFixture>()
            .Build();
            
        // Setup AutoFixture s Moq
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        
        return fixture;
    }
}
```

### User Secrets pro testy

```json
{
  "FlexiBeeSettings": {
    "Server": "https://demo.flexibee.eu",
    "Company": "demo_s_r_o_",
    "Login": "demo",
    "Password": "demo"
  }
}
```

## Advanced Patterns

### 1. Multi-Currency Support

```csharp
// Částky v základní a cizí měně
public decimal SumTotal { get; set; }           // V základní měně
public decimal SumTotalCur { get; set; }        // V cizí měně
public string Currency { get; set; }            // Kód měny
```

### 2. FlexiBee Reference Pattern

```csharp
// Standard pattern pro reference
public string Contact { get; set; }             // "code:KLIENT-001"
[JsonProperty("firma@ref")]
public string ContactRef { get; set; }          // Interní ID
[JsonProperty("firma@showAs")]
public string ContactShowAs { get; set; }       // Zobrazovaný text
```

### 3. Custom Endpoints

```csharp
// Použití custom endpoint místo standardního resource
var uri = $"{resourceId}/{headerId}/aktualizuj-stavy.json";
await client.PostAsync(uri, request);
```

### 4. Batch Operations

```csharp
// Bulk přidání položek inventury
await client.AddStockTakingsAsync(headerId, warehouseId, items);
```

## API Limitations a Best Practices

### 1. Rate Limiting
- FlexiBee API má omezení na počet requestů
- Implementovat retry logic pro production

### 2. Data Konzistence
- Vždy kontrolovat `OperationResult.IsSuccess`
- Používat transakce pro komplexní operace

### 3. Encoding
- České znaky: použít UTF-8
- Bankovní výpisy: ISO-8859-2

### 4. Optimalizace
- Používat `WithCustomDetail()` pro omezení dat
- Implementovat paginaci pro velké datasety

### 5. Security
- Credentials v konfiguraci nebo user secrets
- HTTPS pro production komunikaci

## Příklady použití pro AI Agenty

### 1. Automatické zpracování faktur

```csharp
// Získání faktury s všemi detaily
var invoice = await issuedInvoiceClient.GetAsync("FAV-2024-001");

// Kontrola stavu platby
if (invoice.PaymentState == "unpaid")
{
    // Logika pro upomínky
}
```

### 2. Sledování inventury

```csharp
// Aktuální stav skladu
var stockItems = await stockToDateClient.GetAsync(
    DateTime.Now, warehouseId, 1000, 0);

// Kontrola minimálních stavů
var lowStockItems = stockItems.Where(item => 
    item.Amount < item.MinimumAmount);
```

### 3. Kusovník management

```csharp
// Analýza kusovníku
var bomItems = await bomClient.GetAsync("MAIN-PRODUCT");

// Výpočet celkové ceny
var totalCost = bomItems.Sum(item => 
    item.Amount * item.PurchasePrice);
```

Tato dokumentace poskytuje kompletní přehled FlexiBee SDK pro efektivní práci AI agentů s českým ekonomickým systémem Abra FlexiBee.