FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SlackBotDemo/SlackBotDemo.csproj", "SlackBotDemo/"]
RUN dotnet restore "SlackBotDemo/SlackBotDemo.csproj"
COPY . .
WORKDIR "/src/SlackBotDemo"
RUN dotnet build "SlackBotDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SlackBotDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SlackBotDemo.dll"]
