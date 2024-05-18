FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Authenticacion.Api/Authenticacion.Api.csproj", "Authenticacion.Api/"]
COPY ["Authentication.Aplication/Authentication.Application.csproj", "Authentication.Aplication/"]
COPY ["Authentication.Infrastrunture/Authentication.Infrastructure.csproj", "Authentication.Infrastrunture/"]
COPY ["Authentication.Domain/Authentication.Domain.csproj", "Authentication.Domain/"]
RUN dotnet restore "./Authenticacion.Api/./Authenticacion.Api.csproj"
COPY . .
WORKDIR "/src/Authenticacion.Api"
RUN dotnet build "./Authenticacion.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authenticacion.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authenticacion.Api.dll"]