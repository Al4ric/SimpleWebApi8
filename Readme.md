# SimpleWebApi8 Project

This is a simple ASP.NET 8.0 web API project implemented with modern practices.

## Description

The API has been developed using ASP.NET 8.0 with minimalist programming style. It provides an API interface for managing devices.

## Features

- Communication with a PostgreSQL database using Marten, a .NET transactional document database and event store library.
- API documentation using SwaggerUI/OpenAPI for better interface visibility.
- Endpoints for device data management including creating and retrieving devices.

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- PostgreSQL database

### Installation

1. Clone the repository to your machine.
2. Update the connection string in the `builder.Configuration.GetConnectionString("Postgres")` line in `Program.cs`.
3. Restore the dependencies: `dotnet restore`
4. Build the project: `dotnet build`
5. Run the project: `dotnet run`

## Usage

Visit `localhost:5000/swagger` in your browser to see and test the available API endpoints:

- `createDevice`: Creates a new device.
- `device/{deviceId:guid}`: Gets the specified device by its GUID.

## Contributing

Contributions are welcome. Create a fork, make your changes, and then issue a pull request.

Remember to maintain the minimalist programming style and include unit tests where applicable.

## License

Open for modification and redistribution under MIT License.