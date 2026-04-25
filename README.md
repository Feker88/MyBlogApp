# CodePulse API

A .NET 10 backend API for a blogging platform with domain-driven design principles.

## Project Structure

- **CodePulse.API** - ASP.NET Core Web API layer
- **CodePulse.Application** - Application services and use cases
- **CodePulse.Domain** - Core business logic and entities

## Features

### BlogPost Entity

The `BlogPost` entity implements comprehensive business rules and validation:

#### Business Rules
✓ Can only set IsVisible = true if Content is not empty
✓ UrlHandle must be URL-friendly (no special chars)
✓ FeatureImageUrl must be a valid URL format (or empty)
✓ Cannot modify certain properties after creation

#### Validation Rules
✓ Title must not be empty (min 3 chars, max 200)
✓ Content must not be empty (min 50 chars)
✓ ShortDescription (max 500 chars)
✓ UrlHandle must be URL-friendly (alphanumeric, hyphens, underscores)
✓ Author must not be empty

## Getting Started

### Prerequisites
- .NET 10 SDK
- Visual Studio Community 2026 (or compatible IDE)

### Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/CodePulse.git
cd CodePulse/API/CodePulse.API

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the API
dotnet run
```

## Technologies

- .NET 10
- ASP.NET Core
- Domain-Driven Design (DDD)
- Clean Architecture

## License

MIT License

