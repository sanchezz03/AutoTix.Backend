FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gateway/ApiGateway.Yarp/ApiGateway.Yarp.csproj", "src/Gateway/ApiGateway.Yarp/"]
RUN dotnet restore "./src/Gateway/ApiGateway.Yarp/ApiGateway.Yarp.csproj"
COPY . .
WORKDIR "/src/src/Gateway/ApiGateway.Yarp"
RUN dotnet build "./ApiGateway.Yarp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiGateway.Yarp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.Yarp.dll"]