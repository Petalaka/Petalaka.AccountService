#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Petalaka.Account.API/Petalaka.Account.API.csproj", "Petalaka.Account.API/"]
COPY ["Petalaka.Account.Service/Petalaka.Account.Service.csproj", "Petalaka.Account.Service/"]
COPY ["Petalaka.Account.Contract.Service/Petalaka.Account.Contract.Service.csproj", "Petalaka.Account.Contract.Service/"]
COPY ["Petalaka.Account.Repository/Petalaka.Account.Repository.csproj", "Petalaka.Account.Repository/"]
COPY ["Petalaka.Account.Contract.Repository/Petalaka.Account.Contract.Repository.csproj", "Petalaka.Account.Contract.Repository/"]
COPY ["Petalaka.Account.Core/Petalaka.Account.Core.csproj", "Petalaka.Account.Core/"]
RUN dotnet restore "./Petalaka.Account.API/Petalaka.Account.API.csproj"
COPY . .
WORKDIR "/src/Petalaka.Account.API"
RUN dotnet build "./Petalaka.Account.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Petalaka.Account.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Petalaka.Account.API.dll"]