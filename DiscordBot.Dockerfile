FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
COPY ./AssassinsManager.DiscordBot ./src/AssassinsManager.DiscordBot
WORKDIR /src/AssassinsManager.DiscordBot
RUN dotnet build -o /app
RUN dotnet publish -o /publish

FROM mcr.microsoft.com/dotnet/runtime:8.0-noble-chiseled as base
COPY --from=build /publish /app
WORKDIR /app
ENTRYPOINT [ "./AssassinsManager.DiscordBot" ]