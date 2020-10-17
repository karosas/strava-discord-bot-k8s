FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /sln

# Copy & Restore
COPY "./src/StravaDiscordBot.Workers/StravaDiscordBot.Workers.csproj" "./src/StravaDiscordBot.Workers/"
COPY "./src/StravaDiscordBot.Shared/StravaDiscordBot.Shared.csproj" "./src/StravaDiscordBot.Shared/"
RUN dotnet restore ./src/StravaDiscordBot.Workers/StravaDiscordBot.Workers.csproj

# Copy source
COPY "./src/StravaDiscordBot.Workers/" "./src/StravaDiscordBot.Workers/"
COPY "./src/StravaDiscordBot.Shared/" "./src/StravaDiscordBot.Shared/"

# Build & Publish
RUN dotnet publish "./src/StravaDiscordBot.Workers/StravaDiscordBot.Workers.csproj" -c Release -o /app/publish


# Final image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "StravaDiscordBot.Workers.dll" ]