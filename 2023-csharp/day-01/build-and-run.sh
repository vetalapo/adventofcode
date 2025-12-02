#!/bin/bash
hasDotNet=$(dotnet --version)

if [ $? -ne 0 ]; then
  echo "Install dotnet to run the Program"
  exit 1
fi

echo "dotnet version:"
echo $hasDotNet
echo
echo "Bulding the app..."

dotnet build --configuration Release

echo
echo

dotnet run input.txt
