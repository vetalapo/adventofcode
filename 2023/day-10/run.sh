#!/bin/bash
dotnetVersion=$(dotnet --version)

if [ $? -ne 0 ]; then
    echo "dotnet is not installed on your System"
    exit 1
fi

echo "dotnet version: $dotnetVersion"
echo "Running now..."
echo

dotnet run input.txt --configuration Release
