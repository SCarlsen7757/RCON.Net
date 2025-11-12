# RCON.Net.Commands.Minecraft.Java

[![NuGet](https://img.shields.io/nuget/v/RCON.Net.Commands.Minecraft.Java?label=NuGet)](https://www.nuget.org/packages/RCON.Net.Commands.Minecraft.Java/)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)
![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)

Minecraft Java Edition-specific RCON commands with full response parsing and typed result models.

## Overview

`RCON.Net.Commands.Minecraft.Java` provides a comprehensive set of pre-built commands for interacting with Minecraft Java Edition servers:

- **Player Commands** - List, kick, teleport, and kill players
- **Whitelist Management** - Add, remove, enable/disable, and list whitelisted players
- **Ban Management** - Ban/pardon players and IPs with full list parsing
- **Operator Management** - Grant and revoke operator status
- **Difficulty Controls** - Set server difficulty level
- **Server Commands** - Version, save, and status commands
- **Type-Safe Responses** - Automatic parsing of server responses into strongly-typed models

## Installation

Install via NuGet:

```bash
dotnet add package RCON.Net.Commands.Minecraft.Java
```

## Dependencies

- **RCON.Net.Core** - Core RCON client library
- **RCON.Net.Commands** - Command base types

## Quick Start

```csharp
using RCON.Core;
using RCON.Commands.Minecraft.Java;

// Create and connect client
var client = RconClientBuilder.Create()
    .WithHost("127.0.0.1")
    .WithPort(25575)
    .WithPassword("your-password")
    .Build();

await client.ConnectAsync();

// Get server version
var versionCommand = ServerCommands.GetVersion();
var versionResponse = await client.ExecuteAsync(versionCommand.Build());
var version = versionCommand.Parse<VersionResult>(versionResponse);

// Get player list
var listCommand = PlayerCommands.GetList();
var listResponse = await client.ExecuteAsync(listCommand.Build());
var playerList = listCommand.Parse<PlayerListResult>(listResponse);

Console.WriteLine($"Players online: {playerList?.OnlineCount}/{playerList?.MaxCount}");
foreach (var player in playerList?.Players ?? Array.Empty<Player>())
{
    Console.WriteLine($"  - {player.Name} ({player.Uuid})");
}

await client.DisconnectAsync();
```

## Command Categories

### Player Commands (`PlayerCommands`)

```csharp
// Get online players
var cmd = PlayerCommands.GetList();
var result = cmd.Parse<PlayerListResult>(response);

// Kick a player
var cmd = PlayerCommands.Kick("PlayerName", "reason");

// Teleport a player
var cmd = PlayerCommands.Teleport("PlayerName", 100, 64, -200);

// Kill a player
var cmd = PlayerCommands.Kill("PlayerName");
```

### Whitelist Commands (`WhitelistCommands`)

```csharp
// Add player to whitelist
var cmd = WhitelistCommands.AddPlayer("PlayerName");

// Remove player from whitelist
var cmd = WhitelistCommands.RemovePlayer("PlayerName");

// List whitelisted players
var cmd = WhitelistCommands.GetList();
var result = cmd.Parse<ListResult>(response);

// Enable/disable whitelist
var cmd = WhitelistCommands.EnableWhitelist();
var cmd = WhitelistCommands.DisableWhitelist();

// Reload whitelist
var cmd = WhitelistCommands.Reload();
```

### Ban Commands (`BanCommands`)

```csharp
// Ban a player
var cmd = BanCommands.BanPlayer("PlayerName", "reason");

// Pardon a player
var cmd = BanCommands.PardonPlayer("PlayerName");

// Ban an IP address
var cmd = BanCommands.BanIp("192.168.1.1", "reason");

// Pardon an IP address
var cmd = BanCommands.PardonIp("192.168.1.1");

// List banned players
var cmd = BanCommands.GetBannedPlayers();
var result = cmd.Parse<BanListResult>(response);

// List banned IPs
var cmd = BanCommands.GetBannedIps();
var result = cmd.Parse<BanListResult>(response);
```

### Operator Commands (`OpCommands`)

```csharp
// Grant operator status
var cmd = OpCommands.GiveOp("PlayerName");

// Revoke operator status
var cmd = OpCommands.RemoveOp("PlayerName");
```

### Difficulty Commands (`DifficultyCommands`)

```csharp
// Set difficulty
var cmd = DifficultyCommands.SetDifficulty(DifficultyLevel.Hard);
// Available levels: Peaceful, Easy, Normal, Hard
```

### Server Commands (`ServerCommands`)

```csharp
// Get server version
var cmd = ServerCommands.GetVersion();
var result = cmd.Parse<VersionResult>(response);
```

## Response Models

Commands automatically parse responses into strongly-typed models:

### PlayerListResult
```csharp
public class PlayerListResult
{
    public int OnlineCount { get; set; }
    public int MaxCount { get; set; }
    public Player[] Players { get; set; }
}

public class Player
{
    public string Name { get; set; }
    public string Uuid { get; set; }
}
```

### BanListResult
```csharp
public class BanListResult
{
    public string[] Entries { get; set; }
}
```

### ListResult
```csharp
public class ListResult
{
    public string[] Players { get; set; }
}
```

### OpResult
```csharp
public class OpResult
{
    public string PlayerName { get; set; }
    public OpStatus Status { get; set; }
}

public enum OpStatus
{
    Granted,
    Revoked
}
```

### DifficultyResult
```csharp
public class DifficultyResult
{
    public DifficultyLevel Level { get; set; }
}

public enum DifficultyLevel
{
    Peaceful = 0,
    Easy = 1,
    Normal = 2,
    Hard = 3
}
```

### VersionResult
```csharp
public class VersionResult
{
    public string Version { get; set; }
    public int Protocol { get; set; }
}
```

## Error Handling

Commands validate inputs and throw `ArgumentException` for invalid arguments:

```csharp
try
{
    var cmd = PlayerCommands.Kick(null!); // Throws ArgumentException
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid argument: {ex.Message}");
}
```

## Multi-Targeting

- **.NET 8.0, .NET 9.0, .NET 10.0**

## Features

✅ Comprehensive Minecraft command library  
✅ Type-safe response parsing  
✅ Strongly-typed result models  
✅ Input validation  
✅ Async/await support  
✅ Well-documented API  

## Minecraft Reference

Commands are based on official Minecraft server commands:
- [Minecraft Wiki - Commands](https://minecraft.wiki/w/Commands)
- [Minecraft Wiki - RCON](https://wiki.vg/RCON)

## Related Packages

- [RCON.Net](https://www.nuget.org/packages/RCON.Net/) - Core RCON client
- [RCON.Net.Commands](https://www.nuget.org/packages/RCON.Net.Commands/) - Command base types

## License

MIT License - See LICENSE file for details.
