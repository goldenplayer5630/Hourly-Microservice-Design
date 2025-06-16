# ---------------------------
# Base image
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ---------------------------
# Build stage
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj and restore
COPY ["Hourly.Gateway/Hourly.Gateway.csproj", "Hourly.Gateway/"]
RUN dotnet restore "Hourly.Gateway/Hourly.Gateway.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/Hourly.Gateway"
RUN dotnet build "Hourly.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ---------------------------
# Publish stage
# ---------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hourly.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---------------------------
# Final runtime image
# ---------------------------
FROM base AS final
COPY Hourly.Gateway/ocelot.generated.json ./ocelot.generated.json
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hourly.Gateway.dll"]
