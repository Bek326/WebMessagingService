# Use the official SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY WebMessagingService.API.csproj WebMessagingService.API/
WORKDIR /src/WebMessagingService.API
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet build -c Release -o /app

# Use the official ASP.NET image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebMessagingService.API.dll"]





