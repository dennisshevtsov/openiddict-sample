FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OpeniddictSample.ApiServer/OpeniddictSample.ApiServer.csproj", "src/OpeniddictSample.ApiServer/"]
RUN dotnet restore "./src/OpeniddictSample.ApiServer/./OpeniddictSample.ApiServer.csproj"
COPY . .
WORKDIR "/src/src/OpeniddictSample.ApiServer"
RUN dotnet build "./OpeniddictSample.ApiServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OpeniddictSample.ApiServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpeniddictSample.ApiServer.dll"]
