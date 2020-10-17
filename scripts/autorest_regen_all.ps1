# Yes, it's stupid, but I can't get autorest to work on WSL, so this is a ps1 instead

# DiscordApi
autorest --csharp --input-file=src/StravaDiscordBot.ParticipantApi/swagger.json --output-folder=src/StravaDiscordBot.DiscordApi/Clients/ParticipantApi/ --namespace=StravaDiscordBot.DiscordApi.Clients.ParticipantApi
autorest --csharp --input-file=src/StravaDiscordBot.LeaderboardApi/swagger.json --output-folder=src/StravaDiscordBot.DiscordApi/Clients/LeaderboardApi/ --namespace=StravaDiscordBot.DiscordApi.Clients.LeaderboardApi

# LeaderboardApi
autorest --csharp --input-file=src/StravaDiscordBot.ParticipantApi/swagger.json --output-folder=src/StravaDiscordBot.LeaderboardApi/Clients/ParticipantApi/ --namespace=StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi

# ParticipantApi
autorest --csharp --input-file=src/StravaDiscordBot.DiscordApi/swagger.json --output-folder=src/StravaDiscordBot.ParticipantApi/Clients/DiscordApi/ --namespace=StravaDiscordBot.ParticipantApi.Clients.DiscordApi

# Workers
autorest --csharp --input-file=src/StravaDiscordBot.DiscordApi/swagger.json --output-folder=src/StravaDiscordBot.Workers/Clients/DiscordApi/ --namespace=StravaDiscordBot.Workers.Clients.DiscordApi
autorest --csharp --input-file=src/StravaDiscordBot.ParticipantApi/swagger.json --output-folder=src/StravaDiscordBot.Workers/Clients/ParticipantApi/ --namespace=StravaDiscordBot.Workers.Clients.ParticipantApi
autorest --csharp --input-file=src/StravaDiscordBot.LeaderboardApi/swagger.json --output-folder=src/StravaDiscordBot.Workers/Clients/LeaderboardApi/ --namespace=StravaDiscordBot.Workers.Clients.LeaderboardApi
