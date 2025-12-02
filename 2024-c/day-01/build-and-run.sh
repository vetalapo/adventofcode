#!/bin/bash

echo "Building..."
echo

gcc main.c -o main

if [ ! -f main.exe ]; then
    echo
    echo "Buld has ended with error"
    exit
fi

echo "Running the app"
echo "---------------------"

./main input.txt

echo "---------------------"
echo

echo "Clean up..."

rm main.exe

echo
echo "All Done"
