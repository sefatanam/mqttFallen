FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MqttFallenConsole/MqttFallenConsole.csproj", "MqttFallenConsole/"]
RUN dotnet restore "MqttFallenConsole/MqttFallenConsole.csproj"
COPY . .
WORKDIR "/src/MqttFallenConsole"
RUN dotnet build "MqttFallenConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MqttFallenConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MqttFallenConsole.dll"]
