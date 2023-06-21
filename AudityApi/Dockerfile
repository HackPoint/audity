FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Set an environment variable to tell Chrome to run in headless mode
ENV ASPNETCORE_URLS=http://+:5000 \
    ASPNETCORE_ENVIRONMENT=Production \
    CHROME_BIN=/usr/bin/google-chrome
    
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AudityApi/AudityApi.csproj", "AudityApi/"]
RUN dotnet restore "AudityApi/AudityApi.csproj"
COPY . .
WORKDIR "/src/AudityApi"
RUN dotnet build "AudityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AudityApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudityApi.dll"]
