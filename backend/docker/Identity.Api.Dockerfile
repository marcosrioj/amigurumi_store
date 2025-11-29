FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props ./Directory.Build.props
COPY src/Services/Identity/Identity.Api/Identity.Api.csproj ./src/Services/Identity/Identity.Api/Identity.Api.csproj
COPY src/Services/Identity/Identity.Application/Identity.Application.csproj ./src/Services/Identity/Identity.Application/Identity.Application.csproj
RUN dotnet restore ./src/Services/Identity/Identity.Api/Identity.Api.csproj

COPY src/Services/Identity/Identity.Api ./src/Services/Identity/Identity.Api
COPY src/Services/Identity/Identity.Application ./src/Services/Identity/Identity.Application
RUN dotnet publish ./src/Services/Identity/Identity.Api/Identity.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
