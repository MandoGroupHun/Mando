# syntax=docker/dockerfile:1

# Specify build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Install node
RUN curl -sL https://deb.nodesource.com/setup_16.x  | bash -
RUN apt-get -y install nodejs

# Copy files, restore project as distinct layers and build app
WORKDIR /app
COPY ./ ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/ ./
ENTRYPOINT ["dotnet", "./bin/Release/net6.0/MandoWebApp.dll"]
EXPOSE 80
