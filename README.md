# add migrations (run on layer infrastructure)

dotnet ef migrations add InitialCreate --output-dir ./Persistence/migrations --startup-project ../Api/Api.csproj

# update database(run on shell app)

dotnet ef database update --project ./Infrastructure/Infrastructure.csproj --startup-project ./Api/Api.csproj

<!-- publish app -->

# generate publish release app

dotnet publish -c Release

# docker build

docker build -t fabjiro/system_inventory .

# push to docker

docker push fabjiro/system_inventory

# check image on docker hub
