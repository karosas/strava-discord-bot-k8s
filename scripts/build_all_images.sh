tag=$(date +"%s")

docker build -f Discord.Service.Dockerfile -t karosas/strava-discord-bot-discord-service:$tag .
docker build -f Leaderboard.Service.Dockerfile -t karosas/strava-discord-bot-leaderboard-service:$tag .
docker build -f Participant.Service.Dockerfile -t karosas/strava-discord-bot-participant-service:$tag .
docker build -f Workers.Service.Dockerfile -t karosas/strava-discord-bot-workers-service:$tag .