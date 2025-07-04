FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar solo el archivo csproj para restaurar dependencias
COPY tienda.microservicios.autor.api.csproj ./

RUN dotnet restore "./tienda.microservicios.autor.api.csproj"

# Copiar el resto del código fuente
COPY . .

# Construir el proyecto
RUN dotnet build "./tienda.microservicios.autor.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publicar el proyecto (usando el .csproj)
RUN dotnet publish "./tienda.microservicios.autor.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "tienda.microservicios.autor.api.dll"]
