FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FekrBekrGame/FekrBekrGame.csproj", "FekrBekrGame/"]
RUN dotnet restore "FekrBekrGame/FekrBekrGame.csproj"
COPY . .
WORKDIR "/src/FekrBekrGame"
RUN dotnet build "FekrBekrGame.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FekrBekrGame.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FekrBekrGame.dll"]
