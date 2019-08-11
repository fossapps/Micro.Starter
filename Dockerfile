FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-preview7-alpine3.9 as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out --self-contained --runtime linux-x64

FROM debian:jessie-slim
WORKDIR /app
COPY --from=build /app/out/ ./
RUN chmod +x ./keystore && apt-get update && apt-get install -y --no-install-recommends libicu-dev && rm -Rf /var/lib/apt/lists/* && apt-get clean
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
STOPSIGNAL SIGTERM
CMD ["./keystore"]
