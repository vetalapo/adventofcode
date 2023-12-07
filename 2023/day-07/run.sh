#!/bin/bash
dotnetVersion=$(dotnet --version)

if [ $? -ne 0 ]; then
    echo "dotnet is not installed. Aborting..."
    exit 1
fi

echo "dotnet version: $dotnetVersion"
echo "Running..."
echo

dotnet run input.txt --configuration Release
