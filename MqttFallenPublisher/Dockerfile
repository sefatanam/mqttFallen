FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MqttFallenPublisher/MqttFallenPublisher.csproj", "MqttFallenPublisher/"]
RUN dotnet restore "MqttFallenPublisher/MqttFallenPublisher.csproj"
COPY . .
WORKDIR "/src/MqttFallenPublisher"
RUN dotnet build "MqttFallenPublisher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MqttFallenPublisher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MqttFallenPublisher.dll"]
