#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OnlineShop.WebApi/OnlineShop.WebApi.csproj", "src/OnlineShop.WebApi/"]
COPY ["src/OnlineShop.Application/OnlineShop.Application.csproj", "src/OnlineShop.Application/"]
COPY ["src/OnlineShop.Domain/OnlineShop.Domain.csproj", "src/OnlineShop.Domain/"]
COPY ["src/OnlineShop.Infrastructure/OnlineShop.Infrastructure.csproj", "src/OnlineShop.Infrastructure/"]
RUN dotnet restore "src/OnlineShop.WebApi/OnlineShop.WebApi.csproj"
COPY . .
WORKDIR "/src/src/OnlineShop.WebApi"
RUN dotnet build "OnlineShop.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineShop.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShop.WebApi.dll"]
