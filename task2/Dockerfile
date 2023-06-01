FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["task2/task2.csproj", "task2/"]
RUN dotnet restore "task2/task2.csproj"
COPY . .
WORKDIR "/src/task2"
RUN dotnet build "task2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "task2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "task2.dll"]
