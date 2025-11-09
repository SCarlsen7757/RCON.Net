# RCON.Net

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)

RCON.Net is a small, modular .NET library that implements an RCON client and a set of command helpers (including Minecraft Java commands) to interact with servers supporting the RCON protocol.

Key features
- Lightweight, dependency-free core client (`RCON.Net.Core`).
- Command libraries for building typed commands and parsing responses (`RCON.Net.Commands`, `RCON.Net.Commands.Minecraft`).
- Unit tests included under `RCON.Net.Tests`.
- Solution targets `.NET8` and `.NET9` multi-targeting where applicable.
- Repository-wide `GitVersion.MsBuild` integration via `Directory.Build.props` (added to inject version information at build time).

Requirements
- .NET SDK8.0 or later installed on your machine (9.0 is supported where applicable).

Getting started
1. Clone the repository.
2. From the solution root run:

```bash
dotnet restore
dotnet build
```

Running tests

```bash
dotnet test
```

Usage example

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

await client.DisconnectAsync();
client.Dispose();
```

Project layout
- `RCON.Net` - core client and builder.
- `RCON.Net.Commands` - generic command helpers and base types.
- `RCON.Net.Commands.Minecraft` - Minecraft-specific commands and models.
- `RCON.Net.Tests` - unit tests covering core scenarios.

Versioning
This repository integrates `GitVersion.MsBuild` via `Directory.Build.props` to inject calculated version information into MSBuild. The package version used in the workspace is `6.5.0` (stable). If you prefer floating or centrally managed versions, consider using `Directory.Packages.props` or updating `Directory.Build.props`.

Contributing
- Open an issue for bugs or feature requests.
- Fork and send pull requests for proposed changes.
- Run tests locally before submitting a PR.

If you want, I can:
- Add this file to the solution's Solution Items for visibility in Visual Studio.
- Commit the change to git.
- Create a `Directory.Packages.props` to centralize package versions.
