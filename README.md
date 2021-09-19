# mars-rover

This is about some secret NASA project. We are trying to land some rovers on the Mars surface and control them remotely to change their locations. 

We will use a particular .NET API to comminicate with these rovers remotely. This API has some features to explain:

API (mars-rover) has five end-points which are:
* **GetMarsSurfaceInfo:** `[GET] /marsSurface` use it to get all rovers with their location and heading information on Mars surface
* **LandTheRoverOnMars:** `[POST] /marsSurface/land` use it to land a single rover on Mars surface (jus landing, not any movement)
* **SendMessageToMarsSurface:** `[POST] /marsSurface/sendMessage` use it to send some movement messages to Mars surface. 
* **LandAndMove:** `[POST] /marsSurface/landAndMove` use it to land and move rovers on Mars surface
* **ClearMarsSurface:** `[DELETE] /marsSurface` use it to destroy all rovers on Mars surface

## Data Storing
By default, project use `Redis` to store all information about Mars surface. If you want to store all information in txt file, you can configure it in `appsettings.json` file section `MarsSurfaceRepository:UseFile`. By default `UseRedis:true` is configured.

## Dockerizing
`Docker` option ofcourse on the table. To run API and Redis with docker containers, you may want to use our batch script. To use it: 
* open command prompt (cmd)
* navigate to project path and type **buildall** and hit enter 

This script is restore solution file to get nuget packages which are used in project and build API with Release configuration. After that, script creates docker image from release outputs (in path -> bin/Release/net5.0). Finally, it runs docker compose up! You may want to check docker-compose.yml.

**WARNING:** if you get NuGet package errors while script is running, please 'Rebuild' the solution in Visual Studio. 

## dotnet run option
If you want to use just `dotnet run` command to run API, you may want to change some configuration in `appsettings.json`. Yo must change `Redis:Hosts:Host` value to localhost (or whatever hostname is redis) in case you run redis without using docker. 

If you run redis in docker but not API, you should configure redis hostname given above to container public IP. In order to learn this IP address;
* You should run `docker container ls` and copy the redis container id at this list. 
* Run `docker inspect {{id-of-container}}`
* You would get a big json. You can find the container IP at the bottom of json with `NetworkSettings:Networks::IPAddress`

## Swagger Docs
If you run the API with dotnet run, you can find swagger documantation at http://localhost:5000/swagger/index.html