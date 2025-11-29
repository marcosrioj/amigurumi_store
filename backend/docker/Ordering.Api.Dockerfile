FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props ./Directory.Build.props
COPY src/Services/Ordering/Ordering.Api/Ordering.Api.csproj ./Ordering.Api.csproj
RUN dotnet restore Ordering.Api.csproj

COPY src/Services/Ordering/Ordering.Api/. .
RUN dotnet publish Ordering.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]
