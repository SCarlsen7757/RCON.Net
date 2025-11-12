# RCON.Net

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)
[![NuGet](https://img.shields.io/nuget/v/RCON.Net?label=NuGet)](https://www.nuget.org/packages/RCON.Net/)

RCON.Net is a small, modular .NET library that implements an RCON client and a set of command helpers (including Minecraft Java commands) to interact with servers supporting the RCON protocol.

## Key Features

- **Lightweight, dependency-free core client** (`RCON.Net.Core`) - minimal overhead for RCON connectivity.
- **Command libraries** (`RCON.Net.Commands`, `RCON.Net.Commands.Minecraft`) - building typed commands and parsing responses.
- **Comprehensive unit tests** (`RCON.Net.Tests`) - ensuring reliability across core scenarios.
- **Multi-target support** - Projects target `.NET8` and `.NET9` where applicable.
- **Automatic versioning** - Repository-wide `GitVersion.MsBuild` integration via `Directory.Build.props` injects version information at build time.

## Requirements

- **.NET SDK 8.0** or later (9.0 is supported where applicable).

## Getting Started

### Clone and Build

```bash
git clone https://github.com/SCarlsen7757/RCON.Net.git
cd RCON.Net
dotnet restore
dotnet build
```

### Running Tests

```bash
dotnet test
```

To run tests with verbose output:

```bash
dotnet test --verbosity detailed
```

To run a specific test project:

```bash
dotnet test RCON.Net.Tests/RCON.Net.Tests.csproj
```

## Installation

Install the package via NuGet:

```bash
dotnet add package RCON.Net
```

Or through the NuGet Package Manager:

```
Install-Package RCON.Net
```

## Usage Example

```csharp
// Build an RCON client and execute a command
var client = RconClientBuilder.Create()
    .WithHost("127.0.0.1")
    .WithPort(25575)
    .WithPassword("your-password")
    .WithTimeout(5000)
    .Build();

await client.ConnectAsync();

// Execute commands to interact with the server
// Example: Get list of players
var listCommand = PlayerCommands.GetList();
var response = await client.ExecuteCommandAsync(listCommand);

await client.DisconnectAsync();
client.Dispose();
```

## Project Layout

| Project | Purpose |
|---------|---------|
| `RCON.Net` | Core RCON client and builder pattern implementation. |
| `RCON.Net.Commands` | Generic command helpers and base types for command building. |
| `RCON.Net.Commands.Minecraft` | Minecraft Java Edition-specific commands and response models. |
| `RCON.Net.Tests` | Unit tests covering core functionality and command scenarios. |

## Versioning

This repository integrates `GitVersion.MsBuild` to automatically calculate and inject version information during the build process. Version calculations are configured in `gitversion.yml` and applied via `Directory.Build.props`.

- Package version is automatically updated on each build.
- Versions follow [Semantic Versioning](https://semver.org/).

## Contributing

We welcome contributions! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to:

- Report bugs or request features
- Set up your development environment
- Write and run tests
- Submit pull requests

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For issues, questions, or feature requests, please [open an issue](https://github.com/SCarlsen7757/RCON.Net/issues) on GitHub.
