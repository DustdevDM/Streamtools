FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG TARGETARCH
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore -a $TARGETARCH "OutBot.sln"
# Build and publish a release
RUN dotnet publish -a $TARGETARCH --no-restore "OutBot.sln" -c Release

# Build runtime image
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0

#install required font
COPY ./OutBot/Resources/Rubik-Bold.ttf /usr/share/fonts/

WORKDIR /App
COPY --from=build-env /App/OutBot/bin/Release/net8.0 .
ENTRYPOINT ["dotnet", "OutBot.dll"]
