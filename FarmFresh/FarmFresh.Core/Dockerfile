#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FarmFresh.Core/FarmFresh.Core.csproj", "FarmFresh.Core/"]
COPY ["FarmFresh.Common/FarmFresh.Common.csproj", "FarmFresh.Common/"]
COPY ["FarmFresh.Framework/FarmFresh.Framework.csproj", "FarmFresh.Framework/"]
COPY ["FarmFresh.Data/FarmFresh.Data.csproj", "FarmFresh.Data/"]
RUN dotnet restore "FarmFresh.Core/FarmFresh.Core.csproj"
COPY . .
WORKDIR "/src/FarmFresh.Core"
RUN dotnet build "FarmFresh.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FarmFresh.Core.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FarmFresh.Core.dll"]