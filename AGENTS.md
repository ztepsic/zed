# Project Guidelines

## Tech Stack

- .NET 10 / C# 14, nullable reference types enabled
- NuGet library published as `Zed` — general-purpose core application concepts
- Central Package Management (Directory.Packages.props)
- xUnit + AutoFixture + Moq for testing
- FluentResults for error handling, FluentValidation for validation
- Conventional Commits enforced via CommitLint/Husky
- GitVersion for semantic versioning

## Architecture

This is a DDD-oriented base library with these core abstractions:

- **Domain**: `Entity<TId>` (identity-based equality), `ValueObject` (structural equality), `ICrudRepository`, `IReadOnlyRepository`
- **Data**: ADO.NET repository base, Unit of Work pattern (`IUnitOfWork`, `IUnitOfWorkManager`), `IDbConnectionFactory`
- **Errors**: `AppError` extending FluentResults `Error`, `ValidationError` with FluentValidation integration
- **Objects**: `ImmutableObject` for frozen-after-construction semantics

## Code Style

- XML documentation (`///`) on all public members with `<summary>`, `<param>`, `<returns>`, `<typeparam>` tags
- Organize class internals with `#region` blocks (Fields, Constructors, Methods, etc.)
- Constants use `UPPER_SNAKE_CASE`
- Private fields use `camelCase` (no underscore prefix)
- Use `virtual` for overridable base class methods, `protected` for base class internals
- Null-guard with throw expressions: `param ?? throw new ArgumentNullException(nameof(param))`

## Build and Test

```sh
# Build
dotnet build

# Run tests
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true
```

## Test Conventions

- Test classes: `[Subject]Tests` (e.g., `EntityTests`)
- Test methods: `[Method]_[Scenario]_[ExpectedResult]` (e.g., `Equals_Returns_False_For_Provided_Null_Value`)
- Use `[Fact]` for single cases, `[Theory, AutoData]` for parameterized, `[Theory, AutoMockData]` for mocked dependencies
- Follow Arrange-Act-Assert pattern

## Commits

Follow [Conventional Commits](https://www.conventionalcommits.org/): `feat:`, `fix:`, `perf:`, `refactor:`, `docs:`, `test:`, `chore:`, `ci:`, `build:`, `style:`, `revert:`
