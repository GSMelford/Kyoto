FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kyoto.Bot.Factory/Kyoto.Bot.Factory.csproj", "Kyoto.Bot.Factory/"]
RUN dotnet restore "Workers/Kyoto.Bot.Factory/Kyoto.Bot.Factory.csproj"
COPY . .
WORKDIR "/src/Kyoto.Bot.Factory"
RUN dotnet build "Kyoto.Bot.Factory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kyoto.Bot.Factory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kyoto.Bot.Factory.dll"]
