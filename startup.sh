#!/bin/bash
set -e

echo "Starting Shopli Marketplace (Free Tier - In-Memory Database)..."

echo "Starting application..."
exec dotnet EShop.Web.dll
