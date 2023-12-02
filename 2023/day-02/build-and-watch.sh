#!/bin/bash

dotnetVersion=$(dotnet --version)

if [ $? -ne 0 ]; then
    echo "dotnet is not installed on your system"
    exit 1
fi

echo "dotnet version"
echo $dotnetVersion
echo
echo "Building the Program..."

dotnet build --configuration Release

echo "Running now..."
echo

dotnet watch input.txt
