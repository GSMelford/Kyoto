FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kyoto.Bot/Kyoto.Bot.csproj", "Kyoto.Bot/"]
RUN dotnet restore "Kyoto.Bot/Kyoto.Bot.csproj"
COPY . .
WORKDIR "/src/Kyoto.Bot"
RUN dotnet build "Kyoto.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kyoto.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kyoto.Bot.dll"]
