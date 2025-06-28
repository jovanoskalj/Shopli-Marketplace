# Use the official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["EShop.Web/EShop.Web.csproj", "EShop.Web/"]
COPY ["EShop.Service/EShop.Service.csproj", "EShop.Service/"]
COPY ["EShop.Repository/EShop.Repository.csproj", "EShop.Repository/"]
COPY ["EShop.Domain/EShop.Domain.csproj", "EShop.Domain/"]

# Restore dependencies
RUN dotnet restore "EShop.Web/EShop.Web.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/EShop.Web"
RUN dotnet build "EShop.Web.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "EShop.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy startup script
COPY startup.sh /app/startup.sh
RUN chmod +x /app/startup.sh

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["/app/startup.sh"]
