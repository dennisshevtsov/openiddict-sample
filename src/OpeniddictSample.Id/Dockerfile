FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/OpeniddictSample.IdentityProviderApi/OpeniddictSample.IdentityProviderApi.csproj", "src/OpeniddictSample.IdentityProviderApi/"]
RUN dotnet restore "src/OpeniddictSample.IdentityProviderApi/OpeniddictSample.IdentityProviderApi.csproj"
COPY . .
WORKDIR "/src/src/OpeniddictSample.IdentityProviderApi"
RUN dotnet build "OpeniddictSample.IdentityProviderApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpeniddictSample.IdentityProviderApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS openiddict-sample-id
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpeniddictSample.IdentityProviderApi.dll"]
