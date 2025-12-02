#!/bin/bash

dotnetVersion=$(dotnet --version)

if [ $? -ne 0 ]; then
    echo "Dotnet is not installed on your System"
    exit 1
fi

echo "Building ad running the project..."
echo

dotnet watch input.txt --configuration Release
