FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kyoto/Kyoto.csproj", "Kyoto/"]
RUN dotnet restore "Kyoto/Kyoto.csproj"
COPY . .
WORKDIR "/src/Kyoto"
RUN dotnet build "Kyoto.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kyoto.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kyoto.dll"]
