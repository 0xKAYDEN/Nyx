# Nyx.Server Game Server - Logging System

## Overview

The Nyx.Server Game Server now includes a comprehensive logging system built with Serilog that provides structured logging for all major server operations.

## Features

- **Structured Logging**: All log entries include structured data for easy filtering and analysis
- **Multiple Output Sinks**: Logs are written to both console and daily rotating files
- **Log Levels**: Information, Warning, Error, and Debug levels for different types of events
- **Context Enrichment**: Logs include machine name, thread ID, and custom context properties
- **Performance Monitoring**: Built-in performance metrics logging
- **Security Logging**: Specialized logging for security events and suspicious activities

## Configuration

The logging system is configured via `appsettings.json`:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/Nyx.Server-.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 30,
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}",
          "fileSizeLimitBytes": 10485760
        }
      }
    ]
  }
}
```

## Log Files

Log files are stored in the `logs/` directory with the following naming convention:
- `Nyx.Server-YYYYMMDD.log` (daily rotation)
- Maximum file size: 10MB
- Retention: 30 days

## Logging Categories

### Server Logging
- Server startup and shutdown
- Configuration loading
- Critical errors

### Database Logging
- Connection events
- Query execution (debug level)
- Database errors

### Client Logging
- Connection/disconnection events
- Login success/failure
- Brute force detection
- Packet processing

### Game Logging
- Player actions and level-ups
- Player deaths
- Item transactions
- Guild actions
- Tournament events

### Security Logging
- Suspicious activities
- IP bans
- Security events

### System Logging
- General system information
- Performance metrics
- Component-specific messages

## Usage Examples

### Basic Logging
```csharp
// Information level
Log.Information("Server started successfully");

// Warning level
Log.Warning("High memory usage detected");

// Error level
Log.Error(ex, "Database connection failed");
```

### Structured Logging
```csharp
// Using LoggingService
LoggingService.ClientLoginSuccess("PlayerName", "192.168.1.1");
LoggingService.PlayerAction("PlayerName", "LevelUp", "Reached level 50");
LoggingService.SystemError("Database", "Connection timeout", exception);
```

### Game Client Extensions
```csharp
// Using extension methods
client.LogPlayerAction("Attack", "Attacked monster Dragon");
client.LogPlayerLevelUp(50);
client.LogItemTransaction("DragonSword", "Obtained", 1);
client.LogSuspiciousActivity("Speed hacking detected");
```

### Performance Logging
```csharp
var stopwatch = Stopwatch.StartNew();
// ... perform operation
stopwatch.Stop();
LoggingService.PerformanceMetric("DatabaseQuery", stopwatch.Elapsed);
```

### Context Logging
```csharp
using (LoggingService.CreateLogContext("PlayerID", playerId))
{
    Log.Information("Processing player action");
}
```

## Log Levels

- **Information**: Normal server operations, player actions, successful events
- **Warning**: Potential issues, suspicious activities, non-critical errors
- **Error**: Errors that need attention but don't crash the server
- **Debug**: Detailed information for troubleshooting (only in debug builds)

## Monitoring and Analysis

### Log Analysis Tools
- Use tools like ELK Stack (Elasticsearch, Logstash, Kibana) for advanced log analysis
- Parse structured logs for metrics and alerting
- Set up alerts for security events and critical errors

### Common Log Patterns
- **High Error Rate**: Look for repeated database connection errors
- **Security Issues**: Monitor for brute force attempts and suspicious activities
- **Performance Issues**: Track performance metrics and slow operations
- **Player Issues**: Monitor player disconnections and login failures

## Best Practices

1. **Use Structured Logging**: Always use structured logging methods instead of string concatenation
2. **Include Context**: Add relevant context information to log entries
3. **Avoid Sensitive Data**: Never log passwords, personal information, or sensitive data
4. **Performance**: Use appropriate log levels to avoid performance impact
5. **Monitoring**: Set up monitoring and alerting for critical log events

## Troubleshooting

### Common Issues
1. **Log files not created**: Check if the `logs/` directory exists and has write permissions
2. **High disk usage**: Adjust `retainedFileCountLimit` and `fileSizeLimitBytes` in configuration
3. **Performance impact**: Reduce log level or disable debug logging in production

### Debug Mode
To enable debug logging, change the minimum level in `appsettings.json`:
```json
"MinimumLevel": {
  "Default": "Debug"
}
```

## Integration with Existing Code

The logging system is designed to be non-intrusive and can be gradually integrated into existing code. The `LoggingService` provides a centralized interface for all logging operations, making it easy to add logging to new or existing components. 