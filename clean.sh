#!/bin/bash
echo "Removing auto-generated garbage files and folders..."

find . -type d -iname bin | xargs rm -r
find . -type d -iname obj | xargs rm -r

echo "Done"
