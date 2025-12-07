import fs from "fs";

const fileContents = fs.readFileSync("input.txt", "utf-8");

// Part I
const lines = fileContents
  .split(/\r?\n/)
  .map((line) => line.split(' ')
  .filter((item) => item));

const numbersPivot = Array.from({ length: lines[0].length }, i => new Array());

let operations = [];

// Init parse
for (let i = 0; i < lines.length; i++) {
    if (isNaN(Number(lines[i][0]))) {
        operations = lines[i];
    } else {
        const lineItems = lines[i].map(Number);
        
        for (let j = 0; j < lineItems.length; j++) {
            numbersPivot[j][i] = lineItems[j];
        }
    }
}

//
// Part II
//

// Cephalopods parse
const cLines = fileContents.split(/\r?\n/);

const numbersPivotCephalopods = Array.from({ length: lines[0].length }, i => new Array());

for (let i = 0, c = 0; i < cLines[0].length; i++) {
    let currNumStr = "";

    for (let j = 0; j < cLines.length - 1; j++) {
        if(cLines[j] !== ' ') {
            currNumStr += cLines[j][i];
        }
    }

    const num = Number(currNumStr)

    if (num === 0) {
        c++;
    } else {
        numbersPivotCephalopods[c].push(Number(currNumStr));
    }
}

//
// Calc Results
//
const totals = new Array(operations.length).fill(0);
const totalsCephalopods = new Array(operations.length).fill(0);

for (let i = 0; i < totals.length; i++) {
    const currOperation = operations[i];

    totals[i] = numbersPivot[i].reduce((acc, num) => evaluate(acc, num, currOperation));
    totalsCephalopods[i] = numbersPivotCephalopods[i].reduce((acc, num) => evaluate(acc, num, currOperation));
}

console.log("Grand total:", totals.reduce((acc, num) => acc + num, 0));
console.log("Grand total Cephalopods:", totalsCephalopods.reduce((acc, num) => acc + num, 0));

//
// Helper functions
//
function evaluate(a, b, op) {
    switch (op) {
        case '+':
            return a + b;
        case '*':
            return a * b;
    }

    return 0;
}
