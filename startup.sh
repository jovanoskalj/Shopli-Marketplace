#!/bin/bash
set -e

echo "Starting Shopli Marketplace..."

# Create data directory if it doesn't exist
mkdir -p /app/Data

# Set proper permissions
chmod 755 /app/Data

echo "Starting application..."
exec dotnet EShop.Web.dll
