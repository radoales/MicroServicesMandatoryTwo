#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CustomerManagement/CustomerManagement.csproj", "CustomerManagement/"]
RUN dotnet restore "CustomerManagement/CustomerManagement.csproj"
COPY . .
WORKDIR "/src/CustomerManagement"
RUN dotnet build "CustomerManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/CustomerManagement.db .
ENTRYPOINT ["dotnet", "CustomerManagement.dll"]