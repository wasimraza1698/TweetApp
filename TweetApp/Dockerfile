#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY "*.csproj" .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o publish

#Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Development
ENV UserSettings:ConnectionString mongodb://tweetappdatabee:MJsDTtX8g6OfJPi4Wq93CghcZGh7K5gviFB0fGIApH9RlUmSMBPTzKRP9nWIGDL6FfPfRAgz6x041YJCXTEszg==@tweetappdatabee.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@tweetappdatabee@
ENV TweetSettings:ConnectionString mongodb://tweetappdatabee:MJsDTtX8g6OfJPi4Wq93CghcZGh7K5gviFB0fGIApH9RlUmSMBPTzKRP9nWIGDL6FfPfRAgz6x041YJCXTEszg==@tweetappdatabee.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@tweetappdatabee@
ENV ReplySettings:ConnectionString mongodb://tweetappdatabee:MJsDTtX8g6OfJPi4Wq93CghcZGh7K5gviFB0fGIApH9RlUmSMBPTzKRP9nWIGDL6FfPfRAgz6x041YJCXTEszg==@tweetappdatabee.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@tweetappdatabee@
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TweetApp.dll"]