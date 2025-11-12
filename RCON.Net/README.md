# RCON.Net.Core

[![NuGet](https://img.shields.io/nuget/v/RCON.Net?label=NuGet)](https://www.nuget.org/packages/RCON.Net/)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)

The core RCON client library - a lightweight, dependency-free implementation of the RCON protocol.

## Overview

`RCON.Net.Core` provides the fundamental building blocks for RCON communication:

- **RconClient** - The main client for connecting to and executing commands on RCON-enabled servers
- **RconClientBuilder** - A fluent builder for configuring and creating RCON client instances
- **Command Framework** - Base interfaces and classes for building typed command objects
- **RCON Protocol** - Implementation of the RCON packet format and protocol handling

## Installation

Install via NuGet:

```bash
dotnet add package RCON.Net
```

## Key Components

### RconClient

Manages RCON connections and command execution.

```csharp
var client = new RconClient("127.0.0.1", 25575, "password");
await client.ConnectAsync();
var response = await client.ExecuteAsync("command");
await client.DisconnectAsync();
```

### RconClientBuilder

Fluent API for configuring RCON clients with validation and default values.

```csharp
var client = RconClientBuilder.Create()
    .WithHost("127.0.0.1")
    .WithPort(25575)
    .WithPassword("your-password")
    .WithTimeout(TimeSpan.FromSeconds(5))
    .Build();
```

**Configuration Options:**
- `WithHost(string)` - Server hostname or IP address
- `WithPort(int)` - Server RCON port (default: 25575)
- `WithPassword(string)` - RCON authentication password
- `WithTimeout(TimeSpan)` - Command execution timeout

### ICommand Interface

Base interface for all command implementations.

```csharp
public interface ICommand
{
    string Build();
    T? Parse<T>(string response) where T : class;
}
```

Commands implement:
- `Build()` - Generates the command string to send to the server
- `Parse<T>()` - Parses server response into a typed result object

## RCON Protocol

The RCON protocol is handled internally:

- **Packet Structure** - Standard RCON packet format with ID, type, and payload
- **Authentication** - Secure password-based authentication
- **Request/Response** - Async request/response communication
- **Error Handling** - Comprehensive exception handling for connection and protocol errors

## Exceptions

- `RconException` - Base exception for all RCON-related errors
- `RconConnectionException` - Connection failures
- `RconAuthenticationException` - Authentication failures
- `RconTimeoutException` - Command execution timeouts

## Multi-Targeting

- **.NET 8.0, .NET 9.0, .NET 10.0**

## Usage Example

```csharp
using RCON.Core;

// Create and configure client
var client = RconClientBuilder.Create()
    .WithHost("127.0.0.1")
    .WithPort(25575)
    .WithPassword("admin-password")
    .WithTimeout(TimeSpan.FromSeconds(10))
    .Build();

try
{
    // Connect to server
    await client.ConnectAsync();
    
    // Execute raw command
    var response = await client.ExecuteAsync("say Hello World");
    Console.WriteLine(response);
}
finally
{
    await client.DisconnectAsync();
    client.Dispose();
}
```

## Features

? No external dependencies  
? Async/await support  
? Configurable timeouts  
? Connection pooling support  
? Thread-safe operations  
? Comprehensive error handling  

## Documentation

For more information about the RCON protocol, see:
- [RCON Protocol Specification](https://wiki.vg/RCON)

## Related Packages

- [RCON.Net.Commands](https://www.nuget.org/packages/RCON.Net.Commands/) - Generic command helpers
- [RCON.Net.Commands.Minecraft.Java](https://www.nuget.org/packages/RCON.Net.Commands.Minecraft.Java/) - Minecraft Java Edition commands

## License

MIT License - See LICENSE file for details.
