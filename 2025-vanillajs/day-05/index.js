import fs from "node:fs";

const fileContents = fs.readFileSync("input.txt", "utf-8");

const ingredientLines = fileContents.split(/\r?\n/);

let freshIngredientsCount = 0;

let isInRanges = true;
let ranges = [];

for (let i = 0; i < ingredientLines.length; i++) {
    const line = ingredientLines[i];

    if (line.length === 0) {
        isInRanges = false;
        continue;
    }

    if (isInRanges) {
        // Parse and merge all ranges together if intersect
        let [newLeft, newRight] = line.split('-').map(Number);
        
        for (let i = 0; i < ranges.length; i++) {
            const [left, right] = ranges[i];
            
            if ((newLeft >= left && newLeft <= right) ||
                (newRight >= left && newRight <= right) ||
                (left >= newLeft && left <= newRight) ||
                (right >= newLeft && right <= newRight)
            ) {
                newLeft = Math.min(left, newLeft);
                newRight = Math.max(right, newRight);
                
                ranges.splice(i, 1);
                i = -1;
            }
        }
        
        ranges.push([newLeft, newRight]);
    } else {
        // Parsing IDs and check if it is in ranges
        const id = Number(line);

        for (let i = 0; i < ranges.length; i++) {
            const [left, right] = ranges[i];

            if (id >= left && id <= right) {
                freshIngredientsCount++;
                break;
            }
        }
    }
}

console.log("Amount of fresh ingredients:", freshIngredientsCount);
console.log("Amount of fresh ingredient ids:", ranges.reduce((acc, range) => acc + (range[1] - range[0] + 1), 0));
