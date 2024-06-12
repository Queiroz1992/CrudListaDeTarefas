#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ListaDeTarefas.API/ListaDeTarefas.API.csproj", "ListaDeTarefas.API/"]
COPY ["ListaDeTarefas.Application/ListaDeTarefas.Application.csproj", "ListaDeTarefas.Application/"]
COPY ["ListaDeTarefas.Domain/ListaDeTarefas.Domain.csproj", "ListaDeTarefas.Domain/"]
COPY ["ListaDeTarefas.Infrastructure/ListaDeTarefas.Infrastructure.csproj", "ListaDeTarefas.Infrastructure/"]
RUN dotnet restore "./ListaDeTarefas.API/ListaDeTarefas.API.csproj"
COPY . .
WORKDIR "/src/ListaDeTarefas.API"
RUN dotnet build "./ListaDeTarefas.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ListaDeTarefas.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ListaDeTarefas.API.dll"]