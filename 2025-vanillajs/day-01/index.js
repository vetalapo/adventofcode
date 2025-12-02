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
