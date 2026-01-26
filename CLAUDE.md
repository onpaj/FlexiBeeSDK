# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

FlexiBeeSDK is a .NET SDK for integrating with the FlexiBee accounting system API. The project consists of three main components:

- **Rem.FlexiBeeSDK.Model** - Data models and response types for FlexiBee API entities
- **Rem.FlexiBeeSDK.Client** - HTTP client implementations and API communication logic
- **Rem.FlexiBeeSDK.Tests** - Unit tests using xUnit, AutoFixture, and Moq

## Development Commands

### Building the Solution
```bash
dotnet build                    # Build entire solution
dotnet build --configuration Release  # Build in Release mode
```

### Running Tests
```bash
dotnet test                     # Run all tests
dotnet test --logger trx        # Run tests with TRX output
dotnet test --collect:"XPlat Code Coverage"  # Run with code coverage
```

### Running Individual Test Projects
```bash
dotnet test test/Rem.FlexiBeeSDK.Tests/
```

## Architecture

### Client Architecture
The SDK follows a client-per-resource pattern where each FlexiBee resource (invoices, products, contacts, etc.) has its own client implementation:

- **ResourceClient** (`src/Rem.FlexiBeeSDK.Client/Clients/ResourceClient.cs:19`) - Abstract base class for all API clients containing common HTTP operations, authentication, and JSON serialization
- **Specific Clients** - Individual clients inherit from ResourceClient (e.g., IssuedInvoiceClient, ProductClient, BankAccountClient)
- **Result Filters** - Pipeline filters in `ResultFilters/` for processing API responses and handling common scenarios

### Configuration and DI
- **FlexiBeeSettings** (`src/Rem.FlexiBeeSDK.Client/FlexiBeeSettings.cs:5`) - Configuration class for server connection details
- **ServiceCollectionExtensions** (`src/Rem.FlexiBeeSDK.Client/DI/ServiceCollectionExtensions.cs:14`) - Dependency injection setup for registering all clients and filters

### Query System
- **Query** and **QueryBuilder** - Fluent query building for FlexiBee API filters and parameters
- **FlexiQuery** (`src/Rem.FlexiBeeSDK.Client/Clients/ResourceClient.cs:223`) - Parameter container for API requests

### Response Handling
- **FlexiResultEnvelope** and **WinstromEnvelope** - Wrapper classes for FlexiBee API responses
- **OperationResult<T>** - Standardized result type with status codes and error handling
- **IResultHandler** - Applies filters to process API responses consistently

## Testing Setup

Tests use FlexiFixture (`test/Rem.FlexiBeeSDK.Tests/FlexiFixture.cs:11`) which:
- Loads configuration from user secrets for integration testing
- Sets up AutoFixture with AutoMoq for dependency injection mocking
- Provides mock HttpClientFactory for isolated testing

To run integration tests, configure user secrets with FlexiBeeSettings containing valid server credentials.

## Key Patterns

1. **Resource Clients** - Each FlexiBee resource has a dedicated client inheriting from ResourceClient
2. **Result Filtering** - Response data is processed through a pipeline of IResultFilter implementations
3. **Fluent Queries** - Use QueryBuilder for constructing API queries with filters and parameters
4. **Configuration-Based** - All connection settings managed through IConfiguration and FlexiBeeSettings

## Documentation for Consumer Projects

This SDK includes AI-ready documentation designed specifically for AI agents working in consumer projects:

### Documentation Files

- **FlexiBeeSDK_Documentation.md** - Comprehensive guide for AI agents with:
  - Complete API reference for all clients
  - Code examples for common operations
  - FlexiBee resource mappings
  - Data models and DTOs

- **CLAUDE.md** (this file) - Development guidance for working on this SDK project

### NuGet Package Structure

When published as a NuGet package, documentation is included in the package:

```
rem.flexibeesdk.client/
  content/
    FlexiBeeSDK_Documentation.md  # Main documentation
    CLAUDE.md                      # SDK development guide
```

Files are automatically copied to the `content/` folder in the NuGet package and to consumer project's output directory.

### For Consumer Projects

When a project uses this NuGet package, AI agents should add this to their `CLAUDE.md`:

```markdown
## NuGet Package Documentation

Projekt používá FlexiBee SDK. Dokumentace je dostupná v NuGet cache:
~/.nuget/packages/rem.flexibeesdk.client/*/content/FlexiBeeSDK_Documentation.md

Když pracuješ s FlexiBee typy (IIssuedInvoiceClient, IContactClient, QueryBuilder, atd.),
přečti tuto dokumentaci pro pochopení API.
```

This enables AI agents to discover and use the SDK documentation automatically.