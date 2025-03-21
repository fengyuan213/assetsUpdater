# AssetsUpdater

[![DeepSource](https://deepsource.io/gh/fengyuan213/assetsUpdater.svg/?label=active+issues&show_trend=true&token=vIt2XDX1HyreNvY-58O4tSVG)](https://deepsource.io/gh/fengyuan213/assetsUpdater/?ref=repository-badge)

A .NET library for managing and updating assets with built-in version control and file verification. It provides a high-level abstraction for handling asset updates in a reliable and efficient manner.

## Features

- **Version Control**: Manage assets with major and minor versioning
- **File Verification**: Built-in SHA1 hash verification
- **Efficient Updates**: Smart diff-based updates
- **Progress Tracking**: Real-time download progress monitoring
- **Safe Operations**: Atomic file operations with rollback support

## Quick Start

### Installation

```bash
dotnet add package assertUpdater
```

### Basic Usage

```csharp
// Configure your asset database
var config = new DbConfig
{
    MajorVersion = 1,
    MinorVersion = 0,
    VersionControlFolder = "path/to/assets"
};

// Create storage provider
var storageProvider = await DataManager.BuildDatabase<YourStorageProvider>(config);

// Create managers
var localManager = new LocalDataManager(storageProvider);
var remoteManager = new RemoteDataManager(storageProvider);

// Get and apply updates
var upgradePackage = AssertVerifier.DatabaseCompare(localManager, remoteManager);
foreach (var operation in upgradePackage.Operations)
{
    await operation.Execute();
}
```

## Publishing New Versions

### 1. Prepare Your Assets
```csharp
// Configure the database for your new version
var config = new DbConfig
{
    MajorVersion = 1,
    MinorVersion = 1,  // Increment version
    VersionControlFolder = "path/to/assets"
};

// Create a storage provider for your assets
var storageProvider = await DataManager.BuildDatabase<YourStorageProvider>(config);
```

### 2. Build and Upload Database
```csharp
// Build the database with your new assets
var remoteManager = new RemoteDataManager(storageProvider);

// Upload the database to your cloud storage
await storageProvider.UploadAsync();
```

### 3. Client Update Process
```csharp
// On the client side, the library will:
// 1. Download the new database
// 2. Compare with local database
// 3. Generate necessary updates
var upgradePackage = AssertVerifier.DatabaseCompare(localManager, remoteManager);

// Apply updates
foreach (var operation in upgradePackage.Operations)
{
    await operation.Execute();
}
```

## Testing

The project includes comprehensive tests. Run them using:

```bash
dotnet test
```

## Contributing

Contributions are welcome! Please ensure:
1. Add tests for new functionality
2. Update documentation
3. Follow existing code style

## License

This project is licensed under the MIT License - see the LICENSE file for details.
