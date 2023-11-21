FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/OpeniddictSample.Api/OpeniddictSample.Api.csproj", "src/OpeniddictSample.Api/"]
RUN dotnet restore "src/OpeniddictSample.Api/OpeniddictSample.Api.csproj"
COPY . .
WORKDIR "/src/src/OpeniddictSample.Api"
RUN dotnet build "OpeniddictSample.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpeniddictSample.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS openiddict-sample-api
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpeniddictSample.Api.dll"]
