#!/bin/bash

### Entrypoint of mando-app containers

# Make dotnet-ef available on PATH
export PATH="$PATH:/root/.dotnet/tools"

# Run DB migrations
dotnet ef database update

# Create symlink for webroot content
rm -rf ./wwwroot && ln -s ./out/wwwroot wwwroot

# Start app
dotnet "./bin/Release/net6.0/MandoWebApp.dll"
