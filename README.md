# HotelReservationApp

A simple WPF application for making and listing hotel reservations.

## Requirements

- .NET 10 SDK
- Visual Studio 2022/2022+ (or any editor that supports WPF/.NET 10)

## Build & Run

Using Visual Studio

1. Open the solution file in Visual Studio.
2. Set the `HotelReservationApp` project as the startup project.
3. Run (F5) or Debug &gt; Start Debugging.

Using dotnet CLI

1. Restore and build:

```bash
dotnet build
```

2. Run the WPF project (adjust path if your project file is in a different folder):

```bash
dotnet run --project HotelReservationApp/HotelReservationApp.csproj
```

## Project structure

- `HotelReservationApp` - WPF application project containing views, view models, services and styles.

