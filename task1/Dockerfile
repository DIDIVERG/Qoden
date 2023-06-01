FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["task1/task1.csproj", "task1/"]
RUN dotnet restore "task1/task1.csproj"
COPY . .
WORKDIR "/src/task1"
RUN dotnet build "task1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "task1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "task1.dll"]
