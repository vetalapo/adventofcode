#!/bin/bash

dotnetVersion=$(dotnet --version)

if [ $? -ne 0 ]; then
    echo "dotnet is not installed on the System"
    exit 1
fi

echo "dotnet version: $dotnetVersion"
echo "Running..."
echo

dotnet watch input.txt --configuration Release
