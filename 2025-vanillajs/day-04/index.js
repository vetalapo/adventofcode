import fs from "node:fs";

const fileData = fs.readFileSync("input.txt", "utf-8");

const grid = fileData
    .split(/\r?\n/)
    .map((line) => line.split(''));

// Part I
let paperRollsCount = 0;

for (let row = 0; row < grid.length; row++) {
    for (let col = 0; col < grid[row].length; col++) {
        if (grid[row][col] === '@') {
            let currCount = 0;

            for (let i = row - 1; i <= row + 1; i++) {
                for (let j = col - 1; j <= col + 1; j++) {
                    if (i >= 0 && i < grid.length &&
                        j >= 0 && j < grid[row].length &&
                        !(i === row && j === col) &&
                        grid[i][j] === '@'
                    ) {
                        currCount++;
                    }
                }
            }

            if (currCount < 4) {
                paperRollsCount++;
            }
        }
    }
}

// Part II
let canBeRemovedCount = 0;
let isAllProcessed = false;

while (!isAllProcessed) {
    let lastCount = canBeRemovedCount;

    for (let row = 0; row < grid.length; row++) {
        for (let col = 0; col < grid[row].length; col++) {
            if (grid[row][col] === '@') {
                let currCount = 0;
    
                for (let i = row - 1; i <= row + 1; i++) {
                    for (let j = col - 1; j <= col + 1; j++) {
                        if (i >= 0 && i < grid.length &&
                            j >= 0 && j < grid[row].length &&
                            !(i === row && j === col) &&
                            grid[i][j] === '@'
                        ) {
                            currCount++;
                        }
                    }
                }
    
                if (currCount < 4) {
                    canBeRemovedCount++;
                    grid[row][col] = '.';
                }
            }
        }
    }

    isAllProcessed = lastCount === canBeRemovedCount;
}

console.log("Amount of rolls can be accessed by a forklift:", paperRollsCount);
console.log("Amount of paper can be removed:", canBeRemovedCount);
