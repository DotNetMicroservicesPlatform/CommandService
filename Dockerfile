FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5265

ENV ASPNETCORE_URLS=http://+:5265

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ["src/CommandService/CommandService.csproj", "src/CommandService/"]
RUN dotnet nuget add source --name baget "http://host.docker.internal:5555/v3/index.json"
RUN dotnet restore "src/CommandService/CommandService.csproj"
COPY ./src ./src
WORKDIR "/src/CommandService"
RUN dotnet publish "CommandService.csproj" -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CommandService.dll"]