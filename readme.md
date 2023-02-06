# SwiftMigrationTool (for 8.0.7)

## Instructions
1. Set SourceConnectionString and DestinationConnectionString in `appsettings.json`. 
2. Add the MT type in `appsettings.json` MigrationMessageTypes to migration MT configration to other environment.
3. `dotnet run` the program. The FCCM SWIFT MT configuration will migration to other environment immediately.

## Motivation
Product migration tool very difficult to use, developed a more useful tool.