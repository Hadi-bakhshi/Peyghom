# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/Peyghom.Api/Peyghom.Api.csproj", "src/API/Peyghom.Api/"]
COPY ["src/Modules/Users/Peyghom.Modules.Users/Peyghom.Modules.Users.csproj", "src/Modules/Users/Peyghom.Modules.Users/"]
COPY ["src/Common/Peyghom.Common/Peyghom.Common.csproj", "src/Common/Peyghom.Common/"]
RUN dotnet restore "./src/API/Peyghom.Api/Peyghom.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Peyghom.Api"
RUN dotnet build "./Peyghom.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Peyghom.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Peyghom.Api.dll"]