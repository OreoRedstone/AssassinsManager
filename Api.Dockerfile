FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
COPY ./AssassinsManager.Api ./src/AssassinsManager.Api
COPY ./AssassinsManager.Core ./src/AssassinsManager.Core
COPY ./AssassinsManager.EntityFramework ./src/AssassinsManager.EntityFramework
WORKDIR /src/AssassinsManager.Api
RUN dotnet build -o /app
RUN dotnet publish -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble-chiseled as base
COPY --from=build /publish /app
ENV ASPNETCORE_HTTP_PORTS 80
EXPOSE 80
WORKDIR /app
ENTRYPOINT [ "./AssassinsManager.Api" ]