#!/bin/bash

dotnet build --configuration Release
dotnet watch run input.txt
