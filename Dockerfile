FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY prediktif.csproj .
RUN dotnet restore prediktif.csproj

COPY . .
RUN dotnet publish prediktif.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# curl is used by the container healthcheck.
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_HTTPS_REDIRECT=false
EXPOSE 8080

COPY --from=build /app/publish .

# Run as the non-root user shipped in the aspnet image.
USER $APP_UID

HEALTHCHECK --interval=10s --timeout=5s --retries=10 --start-period=30s \
    CMD curl -fsS http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "prediktif.dll"]
