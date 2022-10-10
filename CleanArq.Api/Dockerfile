#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CleanArq.Api/CleanArq.Api.csproj", "CleanArq.Api/"]
COPY ["CleanArq.Application/CleanArq.Application.csproj", "CleanArq.Application/"]
COPY ["CleanArq.Domain/CleanArq.Domain.csproj", "CleanArq.Domain/"]
COPY ["CleanArq.Infrastructure/CleanArq.Infrastructure.csproj", "CleanArq.Infrastructure/"]
RUN dotnet restore "CleanArq.Api/CleanArq.Api.csproj"
COPY . .
WORKDIR "/src/CleanArq.Api"
RUN dotnet build "CleanArq.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArq.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArq.Api.dll"]