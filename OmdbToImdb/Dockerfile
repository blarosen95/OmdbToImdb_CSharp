
FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OmdbToImdb/OmdbToImdb.csproj", "OmdbToImdb/"]
RUN dotnet restore "OmdbToImdb/OmdbToImdb.csproj"
COPY . .
WORKDIR "/src/OmdbToImdb"
RUN dotnet build "OmdbToImdb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OmdbToImdb.csproj" -c Release -o /app/publish

FROM base AS final
ARG omdb_key
ENV PAN_CTX_RUN_MODE=Containerized
ENV PAN_CFG_MAKE_SHOW=True
ENV PAN_SEC_OMDB_API_KEY=$omdb_key
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OmdbToImdb.dll"]
