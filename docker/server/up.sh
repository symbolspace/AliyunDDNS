source ./config.sh

if [ "$1" = "sync" ]; then
    docker-compose -f ./docker-compose.yml -p $project_name up --force-recreate
else
    docker-compose -f ./docker-compose.yml -p $project_name up -d --force-recreate
fi