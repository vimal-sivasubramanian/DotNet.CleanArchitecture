#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Service/Api/Api.csproj", "Service/Api/"]
COPY ["Service/Application/Application.csproj", "Service/Application/"]
COPY ["Service/Domain/Domain.csproj", "Service/Domain/"]
COPY ["Common/Core/Core.csproj", "Common/Core/"]
COPY ["Service/Infrastructure/Infrastructure.csproj", "Service/Infrastructure/"]
COPY ["Common/EventStore/EventStore.csproj", "Common/EventStore/"]
COPY ["Common/MessageBrokers/MessageBrokers.csproj", "Common/MessageBrokers/"]
RUN dotnet restore "Service/Api/Api.csproj"
COPY . .
WORKDIR "/src/Service/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
