# Build stage (Windows container)
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
WORKDIR C:\source

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o C:\app

# Runtime stage (Windows container)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022
WORKDIR C:\app
COPY --from=build C:\app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "MyRestAPI.dll"]
