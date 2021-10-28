FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["ToyRobot/ToyRobot.csproj", "ToyRobot/"]
COPY ["ToyRobotTest/ToyRobotTest.csproj", "ToyRobotTest/"]

RUN dotnet restore

# copy full solution over
COPY . .
RUN dotnet build

# create a new build target called testrunner
FROM build AS testrunner
WORKDIR /src/ToyRobotTest
CMD ["dotnet", "test", "--logger:trx"]

# run the unit tests
FROM build AS test
WORKDIR /src/ToyRobotTest
RUN dotnet test --logger:trx

FROM test AS release
WORKDIR "/src/ToyRobot"
RUN dotnet build "ToyRobot.csproj" -c Release -o /app/build

FROM release AS publish
WORKDIR "/src/ToyRobot"
RUN dotnet publish "ToyRobot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToyRobot.dll"]