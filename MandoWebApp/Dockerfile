# syntax=docker/dockerfile:1

# Specify build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
ENV ASPNETCORE_ENVIRONMENT=Production

# Install node
RUN curl -sL https://deb.nodesource.com/setup_16.x  | bash -
RUN apt-get -y install nodejs

# Copy files, restore project as distinct layers and build app
WORKDIR /app
COPY ./ ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
FROM mcr.microsoft.com/dotnet/sdk:6.0

# Install mkcert and generate CA + certificate
ENV DOMAINS='localhost 127.0.0.1'
ARG MKCERT_VERSION=v1.4.3
RUN apt-get update && \
    apt-get install -y \
        libnss3-tools curl
RUN curl -L https://github.com/FiloSottile/mkcert/releases/download/$MKCERT_VERSION/mkcert-$MKCERT_VERSION-linux-arm64 \
        > /usr/local/bin/mkcert && \
        chmod +x /usr/local/bin/mkcert
RUN mkcert -install
RUN mkcert -p12-file /etc/ssl/private/cert.pfx -pkcs12 $DOMAINS

# dotnet specific env vars for kestrel, cert handling
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80;https://+:443
ENV ASPNETCORE_HTTPS_PORT=443
ENV Kestrel:Certificates:Default:Path=/etc/ssl/private/cert.pfx
# Password is a hardcoded legacy value, read more on this here:
# https://github.com/FiloSottile/mkcert/pull/58#issuecomment-451782395
ENV Kestrel:Certificates:Default:Password=changeit
ENV Kestrel:Certificates:Default:AllowInvalid=true
ENV Kestrel:EndPointDefaults:Protocols=Http1AndHttp2

WORKDIR /app
COPY --from=build-env /app/ ./
# For some reason copying client app is not done by csproj publish
COPY --from=build-env /app/ClientApp/dist/ ./wwwroot

# Install dotnet-ef to run DB migrations upon container startup
RUN dotnet tool install --global dotnet-ef

ENTRYPOINT ["/app/entrypoint.sh"]
EXPOSE 80 443
