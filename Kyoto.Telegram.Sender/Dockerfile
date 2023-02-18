FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kyoto.Telegram.Sender/Kyoto.Telegram.Sender.csproj", "Kyoto.Telegram.Sender/"]
RUN dotnet restore "Kyoto.Telegram.Sender/Kyoto.Telegram.Sender.csproj"
COPY . .
WORKDIR "/src/Kyoto.Telegram.Sender"
RUN dotnet build "Kyoto.Telegram.Sender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kyoto.Telegram.Sender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kyoto.Telegram.Sender.dll"]
