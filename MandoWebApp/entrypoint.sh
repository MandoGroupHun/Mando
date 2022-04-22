#!/bin/bash

### Entrypoint of mando-app containers

# Make dotnet-ef available on PATH
export PATH="$PATH:/root/.dotnet/tools"

# Run DB migrations
dotnet ef database update

# Start app
dotnet "./bin/Release/net6.0/MandoWebApp.dll"
