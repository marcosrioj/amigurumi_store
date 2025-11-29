FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props ./Directory.Build.props
COPY src/Services/Catalog/Catalog.Api/Catalog.Api.csproj ./Catalog.Api.csproj
RUN dotnet restore Catalog.Api.csproj

COPY src/Services/Catalog/Catalog.Api/. .
RUN dotnet publish Catalog.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]
