FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /sln

# Copy & Restore
COPY "./src/StravaDiscordBot.DiscordApi/StravaDiscordBot.DiscordApi.csproj" "./src/StravaDiscordBot.DiscordApi/"
COPY "./src/StravaDiscordBot.Shared/StravaDiscordBot.Shared.csproj" "./src/StravaDiscordBot.Shared/"
RUN dotnet restore ./src/StravaDiscordBot.DiscordApi/StravaDiscordBot.DiscordApi.csproj

# Copy source
COPY "./src/StravaDiscordBot.DiscordApi/" "./src/StravaDiscordBot.DiscordApi/"
COPY "./src/StravaDiscordBot.Shared/" "./src/StravaDiscordBot.Shared/"

# Build & Publish
RUN dotnet publish "./src/StravaDiscordBot.DiscordApi/StravaDiscordBot.DiscordApi.csproj" -c Release -o /app/publish


# Final image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "StravaDiscordBot.DiscordApi.dll" ]