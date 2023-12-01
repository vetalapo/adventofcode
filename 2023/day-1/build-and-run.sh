#!/bin/bash
echo "Bulding the app..."

dotnet build --configuration Release

echo
echo

dotnet run input.txt
