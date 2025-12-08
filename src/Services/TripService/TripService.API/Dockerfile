# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/TripService/TripService.API/TripService.API.csproj", "src/Services/TripService/TripService.API/"]
COPY ["src/Services/TripService/TripService.Application/TripService.Application.csproj", "src/Services/TripService/TripService.Application/"]
COPY ["src/Services/TripService/TripService.Domain/TripService.Domain.csproj", "src/Services/TripService/TripService.Domain/"]
COPY ["src/Services/TripService/TripService.Infrastructure/TripService.Infrastructure.csproj", "src/Services/TripService/TripService.Infrastructure/"]
RUN dotnet restore "./src/Services/TripService/TripService.API/TripService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/TripService/TripService.API"
RUN dotnet build "./TripService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TripService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TripService.API.dll"]