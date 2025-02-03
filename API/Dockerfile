FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG TARGETARCH
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore -a $TARGETARCH "StreamTools API.sln"
# Build and publish a release
RUN dotnet publish --no-restore -a $TARGETARCH -c Release -o out "Application.API/Application.API.csproj"

# Build runtime image
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Application.API.dll"]
