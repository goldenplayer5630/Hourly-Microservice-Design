#!/bin/sh

echo "Generating ocelot.json from environment variables..."
envsubst < /app/ocelot.template.json > /app/ocelot.json

echo "Starting API Gateway..."
exec dotnet Hourly.Gateway.dll