#!/bin/bash
dotnetVersion=$(dotnet --version)

if [ $? -ne 0 ]; then
    echo "dotnet is not installed on your System"
    exit 1
fi

echo "dotnet version: $dotnetVersion"
echo "Watching now..."
echo

dotnet watch input.txt --configuration Release
