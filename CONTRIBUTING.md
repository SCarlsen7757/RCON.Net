# Contributing to RCON.Net

Thank you for your interest in contributing to RCON.Net! We appreciate your support and look forward to working with you. This document provides guidelines and instructions for contributing to the project.

## Code of Conduct

Please be respectful and constructive in all interactions. We're committed to providing a welcoming and inclusive environment for all contributors.

## How to Contribute

### Reporting Bugs

Before creating a bug report, please check the [issue tracker](https://github.com/SCarlsen7757/RCON.Net/issues) to see if the problem has already been reported.

When creating a bug report, include:

- **Clear title and description** of the issue
- **Steps to reproduce** the problem with specific examples
- **Expected behavior** vs. actual behavior
- **.NET version** and **OS** you're using
- **Relevant code snippets** or project files (if applicable)

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When suggesting an enhancement:

- Use a **clear and descriptive title**
- Provide a **detailed description** of the suggested enhancement
- List **specific use cases** where this enhancement would be useful
- Reference **existing related functionality** if applicable

### Pull Requests

We welcome pull requests! Before starting, please:

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/YOUR_USERNAME/RCON.Net.git
   cd RCON.Net
   ```
3. **Create a feature branch** from `main`:
   ```bash
 git checkout -b feature/your-feature-name
   ```

## Development Setup

### Prerequisites

- **.NET SDK 8.0** or later (9.0 supported where applicable)
- A code editor or IDE (Visual Studio, Visual Studio Code, Rider, etc.)

### Building the Project

```bash
dotnet restore
dotnet build
```

### Running Tests

```bash
dotnet test
```

To run tests with detailed output:

```bash
dotnet test --verbosity detailed
```

To run tests for a specific project:

```bash
dotnet test RCON.Net.Tests/RCON.Net.Tests.csproj --verbosity detailed
```

## Testing Requirements

**All pull requests must include unit tests.** Follow these guidelines:

### Test Structure

- Place tests in the appropriate folder under `RCON.Net.Tests/Commands/`
- Use **xUnit** for testing framework (following existing patterns)
- Follow the **Arrange-Act-Assert (AAA)** pattern

### Test Naming Convention

Test method names should be descriptive and follow this pattern:
```
[MethodName]_[Scenario]_[ExpectedResult]
```

Example:
```csharp
[Fact]
public void AddPlayer_WithValidPlayerName_ReturnsCorrectCommand()
{
  // Arrange
    var command = WhitelistCommands.AddPlayer("Player1");

    // Act
    var result = command.Build();

    // Assert
    Assert.Equal("whitelist add Player1", result);
}
```

### Test Coverage Requirements

When adding new features:

- Test **happy path** scenarios (valid inputs)
- Test **error handling** (null, empty, invalid inputs)
- Test **edge cases** (boundary conditions)
- Test **response parsing** (if applicable)

### Example Test File

```csharp
using RCON.Commands.Minecraft.Java.SomeFeature;
using Xunit;

namespace RCON.Net.Tests.Commands.Minecraft.Java
{
    public class SomeFeatureCommandTests
    {
[Fact]
        public void CommandMethod_WithValidInput_ReturnsExpectedResult()
        {
            // Arrange
         var expectedCommand = "expected command string";

            // Act
  var command = SomeFeatureCommands.CommandMethod("input");

      // Assert
   Assert.Equal(expectedCommand, command.Build());
}

      [Fact]
        public void CommandMethod_WithNullInput_ThrowsArgumentException()
      {
            // Act & Assert
          Assert.Throws<ArgumentException>(() => 
         SomeFeatureCommands.CommandMethod(null!));
}

        [Fact]
        public void CommandMethod_Parse_WithValidResponse_ReturnsCorrectResult()
    {
            // Arrange
        var command = SomeFeatureCommands.CommandMethod("input");
       var response = "Valid response message";

    // Act
          var result = command.Parse(response);

     // Assert
            Assert.NotNull(result);
    // Add additional assertions based on expected structure
        }
    }
}
```

## Code Style Guidelines

### General Principles

- Follow **C# naming conventions** (PascalCase for public members, camelCase for private/parameters)
- Use **meaningful variable names** - avoid abbreviations unless widely recognized
- Keep **methods small and focused** - single responsibility principle
- Add **XML documentation comments** for public members

### Example Code Style

```csharp
/// <summary>
/// Adds a player to the whitelist.
/// </summary>
/// <param name="playerName">The name of the player to add.</param>
/// <returns>A command object that can be executed.</returns>
/// <exception cref="ArgumentException">Thrown when playerName is null or empty.</exception>
public static ICommand AddPlayer(string playerName)
{
    if (string.IsNullOrWhiteSpace(playerName))
    {
        throw new ArgumentException("Player name cannot be null or empty.", nameof(playerName));
 }

    return new AddPlayerCommand(playerName);
}
```

### Formatting

- Use **4 spaces** for indentation
- Use **implicit usings** where available (.NET 6+)
- Use **nullable reference types** (`#nullable enable`)

## Submission Process

1. **Commit your changes** with clear, descriptive commit messages:
   ```bash
   git commit -m "Add feature: brief description of changes"
   ```

2. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```

3. **Create a Pull Request** on GitHub:
   - Reference any related issues using `#issue-number`
- Provide a clear description of changes
   - Ensure all tests pass locally before submitting

4. **Respond to feedback** - maintainers may request changes or clarifications

## Pull Request Checklist

Before submitting your PR, verify:

- [ ] Your feature branch is based on the latest `main`
- [ ] All tests pass locally: `dotnet test`
- [ ] New tests are included for new functionality
- [ ] Code follows the style guidelines above
- [ ] XML documentation comments are added for public members
- [ ] No breaking changes are introduced (or clearly documented)
- [ ] Commit messages are clear and descriptive

## Project Structure

```
RCON.Net/
??? RCON.Net/     # Core RCON client
??? RCON.Net.Commands/          # Generic command interfaces and helpers
??? RCON.Net.Commands.Minecraft/       # Minecraft-specific commands
??? RCON.Net.Tests/        # Unit tests
?   ??? Commands/Minecraft/Java/       # Minecraft command tests
??? Directory.Build.props        # Shared build configuration
??? gitversion.yml             # Version management
```

## Questions?

- Check existing issues and discussions
- Create a new discussion for questions or ideas
- Reach out to the maintainers if you need clarification

Thank you for contributing to RCON.Net! ??
