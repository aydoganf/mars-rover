
cls

#dotnet restore MarsRoverSolution.sln
NuGet.exe restore MarsRoverSolution.sln

cd MarsRoverAPI
dotnet restore
dotnet build -c Release
docker build -t mars-rover-api .

cd ..
docker network create resolute
docker compose up -d

pause