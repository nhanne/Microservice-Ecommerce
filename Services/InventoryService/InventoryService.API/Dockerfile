# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development
#ENV ConnectionString=mongodb://mongodb:27017/inventory

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["InventoryService.API/InventoryService.API.csproj", "InventoryService.API/"]
COPY ["InventoryService.Application/InventoryService.Application.csproj", "InventoryService.Application/"]
COPY ["InventoryService.Domain/InventoryService.Domain.csproj", "InventoryService.Domain/"]
COPY ["InventoryService.Infrastructure/InventoryService.Infrastructure.csproj", "InventoryService.Infrastructure/"]
RUN dotnet restore "InventoryService.API/InventoryService.API.csproj"

# Build Application
COPY . .
WORKDIR "/src/InventoryService.API"
RUN dotnet build "./InventoryService.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InventoryService.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Final Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.API.dll"]