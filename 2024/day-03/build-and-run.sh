#!/bin/bash

echo "Building..."
echo

gcc main.c -o main

if [ ! -f main.exe ]; then
    echo
    echo "Built has failed to build"
    exit
fi

echo "Running the app"
echo "----------------------------"

./main.exe input.txt

echo "----------------------------"
echo

echo "Cleaning up..."

rm main.exe

echo "Done"
