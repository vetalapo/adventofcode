import fs from "node:fs";

const fileData = fs.readFileSync("input.txt", "utf-8");

const lines = fileData
    .split(/\r?\n/)
    .map((line) => ({
        direction: line.at(0),
        clicks: Number(line.slice(1))
    }));

let dial = 50;
let pointingZeroCount = 0;
let traveledPastZero = 0;

for (const line of lines) {
    traveledPastZero += countClicksAtZero(dial, line);

    dial = line.direction === 'L'
        ? dial - line.clicks
        : dial + line.clicks;

    dial %= 100;

    if (dial < 0) {
        dial += 100;
    }

    if (dial === 0) {
        pointingZeroCount++;
    }
}

console.log("Actual password to open the door:", pointingZeroCount);
console.log("Password to open the door (0x434C49434B method):", traveledPastZero);

//
// Helper Functions
//
function countClicksAtZero(dl, line) {
    let count = 0;

    for (let i = 0; i < line.clicks; i++) {
        if (line.direction === 'L') {
            dl--;

            if (dl < 0) {
                dl += 100;
            }
        } else {
            dl++;

            if (dl >= 100) {
                dl -= 100;
            }
        }

        if (dl === 0) {
            count++;
        }
    }

    return count;
}
