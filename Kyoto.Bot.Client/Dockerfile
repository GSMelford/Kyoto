FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kyoto.Bot.Client/Kyoto.Bot.Client.csproj", "Kyoto.Bot.Client/"]
RUN dotnet restore "Kyoto.Bot.Client/Kyoto.Bot.Client.csproj"
COPY . .
WORKDIR "/src/Kyoto.Bot.Client"
RUN dotnet build "Kyoto.Bot.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kyoto.Bot.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kyoto.Bot.Client.dll"]
