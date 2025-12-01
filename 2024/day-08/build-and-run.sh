#!/bin/bash

echo "Building..."
echo

gcc main.c -o main

if [ ! -f main.exe ]; then
    echo
    echo "Build has failed to"
    exit
fi

echo "Running"
echo "---------------------------"

./main.exe input.txt

echo "---------------------------"
echo

echo "Cleaning up..."

rm main.exe

echo "Done"
