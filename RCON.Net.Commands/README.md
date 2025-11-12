# RCON.Net.Commands

[![NuGet](https://img.shields.io/nuget/v/RCON.Net.Commands?label=NuGet)](https://www.nuget.org/packages/RCON.Net.Commands/)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)

Generic command helpers and base types for building typed RCON commands.

## Overview

`RCON.Net.Commands` provides the foundational abstractions for command building and response parsing:

- **ICommand Interface** - Abstract command implementation contract
- **CommandBase** - Base class for implementing custom commands
- **Response Parsing** - Generic type-safe response parsing framework
- **Command Builders** - Utilities for building command strings

## Installation

Install via NuGet:

```bash
dotnet add package RCON.Net.Commands
```

## Dependencies

- **RCON.Net.Core** - Core RCON client library

## Key Components

### ICommand Interface

The core abstraction for all command implementations.

```csharp
public interface ICommand
{
    /// <summary>
    /// Builds the command string to send to the server.
    /// </summary>
    string Build();

    /// <summary>
    /// Parses a server response into a typed result.
    /// </summary>
    T? Parse<T>(string response) where T : class;
}
```

### CommandBase

Base class for implementing custom commands with common functionality.

```csharp
public abstract class CommandBase : ICommand
{
    public abstract string Build();
    
    public virtual T? Parse<T>(string response) where T : class
    {
        // Default parsing logic
        return response as T;
    }
}
```

## Creating Custom Commands

Implement the `ICommand` interface or extend `CommandBase`:

```csharp
public class CustomCommand : CommandBase
{
    private readonly string _argument;
    
    public CustomCommand(string argument)
    {
        _argument = argument ?? throw new ArgumentNullException(nameof(argument));
    }
    
    public override string Build()
    {
        return $"custom {_argument}";
    }
    
    public override T? Parse<T>(string response) where T : class
    {
        // Parse response and return typed result
        return null;
    }
}
```

## Usage Pattern

```csharp
using RCON.Core;
using RCON.Commands;

// Create client
var client = RconClientBuilder.Create()
    .WithHost("127.0.0.1")
    .WithPort(25575)
    .WithPassword("password")
    .Build();

await client.ConnectAsync();

// Create and execute command
var command = new CustomCommand("value");
var response = await client.ExecuteAsync(command.Build());
var result = command.Parse<CustomResult>(response);

await client.DisconnectAsync();
```

## Response Parsing

Commands support type-safe response parsing:

```csharp
public class ListCommand : CommandBase
{
    public override string Build() => "list";
    
    public override T? Parse<T>(string response) where T : class
    {
        if (typeof(T) == typeof(PlayerList))
        {
            var lines = response.Split('\n');
            var playerList = new PlayerList { Players = ParsePlayers(lines) };
            return playerList as T;
        }
        return null;
    }
}
```

## Multi-Targeting

- **.NET 8.0, .NET 9.0, .NET 10.0**

## Features

✅ Type-safe command building  
✅ Generic response parsing  
✅ Extensible architecture  
✅ No external dependencies  
✅ Simple, intuitive API  

## Related Packages

- [RCON.Net](https://www.nuget.org/packages/RCON.Net/) - Core RCON client
- [RCON.Net.Commands.Minecraft.Java](https://www.nuget.org/packages/RCON.Net.Commands.Minecraft.Java/) - Minecraft Java Edition commands

## License

MIT License - See LICENSE file for details.
