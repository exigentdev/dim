FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5190

ENV ASPNETCORE_URLS=http://+:5190

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["ExigentDev.DIM.Api.csproj", "./"]
RUN dotnet restore "ExigentDev.DIM.Api.csproj"

COPY . .
RUN dotnet build "ExigentDev.DIM.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ExigentDev.DIM.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExigentDev.DIM.Api.dll"]
