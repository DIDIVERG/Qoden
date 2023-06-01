FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["task3/task3.csproj", "task3/"]
RUN dotnet restore "task3/task3.csproj"
COPY . .
WORKDIR "/src/task3"
RUN dotnet build "task3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "task3.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "task3.dll"]
