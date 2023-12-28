# Inventory System

## Intro

This system creates purchase orders connected to the inventory, which allows the company to have control over orders, product counts (in and out), suppliers, prices, and more. It is convenient and avoids stock miscounting while keeping track of all transactions.

## Prerequisites

Ensure you have the following installed on your machine:

- [**.NET SDK**](https://dotnet.microsoft.com/download) compatible with the project version `net6.0` (Only if there is an intent to work directly on the application outside of Docker)
- [**Docker**](https://www.docker.com/products/docker-desktop) and [**Docker Compose**](https://docs.docker.com/compose/install/)

## Project Structure

- `docker-compose.yml`: Docker Compose configuration for setting up SQL Server and .NET application.
- `Dockerfile`: Defines the Docker image configuration for the .NET application.

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone git@github.com:carlospsvieira/inventory-system.git 
   ```

2. **Navigate to the Project Directory**:
   ```bash
   cd inventory-system
   ```

## Docker Compose Configuration

The `docker-compose.yml` file defines services for both the SQL Server database and the .NET application, enabling seamless container orchestration.

### SQL Server Service

- **Image**: SQL Server 2019 from Microsoft Container Registry.
- **Ports**: Exposes port 1433 for SQL Server connections.
- **Environment Variables**: 
  - `ACCEPT_EULA`: Accept the End-User License Agreement.
  - `SA_PASSWORD`: Set the SQL Server `sa` user password.

### .NET Service

- **Image**: ASP.NET Core runtime image.
- **Ports**: Maps port 80 inside the container to port 5000 on the host.
- **Environment Variables**: 
  - `ConnectionStrings__DefaultConnection`: Specifies the connection string for EF Core to connect to the SQL Server container.

#### Starting the Services

To start the services, run:

```bash
docker-compose up -d
```

## Building and Running the Application

1. **Build the .NET Docker Image**:
   ```bash
   docker build -t inventory-system-image .
   ```

2. **Run Docker Compose Again**:
   ```bash
   docker-compose up -d
   ```

## Accessing the Application

Once the services are up and running, you can access the application through:

- **API**: `http://localhost:5000`
- **SQL Server**: 
  - **Server**: `localhost:1433`
  - **Username**: `sa`
  - **Password**: Specified in the `docker-compose.yml` (default is set in `.env` file)

## Troubleshooting

For any issues or errors during setup or execution, refer to Docker and .NET documentation for troubleshooting steps. Ensure that Docker, Docker Compose, and the required .NET SDK version are correctly installed and configured.

---

For additional support or inquiries, please contact me at <a href="mailto:carlepsvieira@gmail.com">carlepsvieira@gmail.com</a>.
