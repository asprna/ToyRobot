# ToyRobot

I have added docker container image support for this application.

The docker container is configured to execute the unit tests, upon success it will build the container.

You can follow below instructions to run the application.

#### Using Docker Image
1. Navigate to ToyRobot solution folder
2. Initially you need to build the application using the below following
`docker build -t toyrobot:v1 .`
3. Once the build is successful, use the below following to execute the application.
`docker run -it --rm toyrobot:v1`

#### Using .NET CLI
1. Navigate to ToyRobot solution folder
2. Run the following command to restore packages 
`dotnet restore`
3. Run the following command to build the application
`dotnet publish "ToyRobot/ToyRobot.csproj" -c Release`
4. Run the following command to publish the application 
`dotnet publish "ToyRobot/ToyRobot.csproj" -c Release -o ./app/publish`
5. Navigate to ./app/publish folder
6. Execute "ToyRobot.exe" to run the application 
