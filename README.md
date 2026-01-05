# D2Bot#

A D2BS (Diablo II Botting System) game manager for Diablo II.

This is a decompiled and cleaned-up version of D2Bot#, modified to build with modern .NET tooling.

## Building

Requires .NET Framework 4.8 SDK.

```
dotnet build
```

## Required Directory Structure

The application expects the following directory structure relative to the executable.

```
D2Bot.exe
d2bs/                 # REQUIRED
    d2bs.ini          # REQUIRED
    D2BS.dll
    kolbot/           # REQUIRED - or any folder matching *bot pattern
        data/
            web/
                limedrop.json
data/
    server.json
    profile.json
    cdkeys.json
    patch.json
    schedules.json
logs/
    exceptions.log
    keyinfo.log
```

The application will show a helpful error message and exit gracefully if required files/directories are missing.

## Changes from Original

- Targets .NET Framework 4.8 (SDK-style project)
- Removed Costura/Fody embedded assembly packaging
- Removed auto-update functionality
- Removed admin privilege requirement
- Resources organized into `Resources/` directory
- Form .resx files placed alongside .cs files

## Dependencies

All dependencies are managed via NuGet:
- BouncyCastle (cryptography)
- Newtonsoft.Json (JSON serialization)
- ObjectListView.Official (ListView control)
- SmartIrc4net (IRC protocol)
- SslCertBinding.Net (SSL certificate binding)
