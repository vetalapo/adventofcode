import fs from "node:fs";

const fileData = fs.readFileSync("input.txt", "utf-8");

const idRanges = fileData
    .split(/\r?\n/)
    .map((line) => line.split(','))
    .flat()
    .filter((item) => item.length > 0)
    .map((item) => {
        const [firstID, lastID] = item.split('-');

        return {
            firstID: Number(firstID),
            lastID: Number(lastID)
        }
    });

let invalidIdsSum = 0;
let invalidIdsSumUpdatedRules = 0;

for (const idRange of idRanges) {
    const invalidIds = getInvalidIdsInRange(idRange.firstID, idRange.lastID);
    invalidIdsSum += invalidIds.ids.reduce((acc, num) => acc + num, 0);
    invalidIdsSumUpdatedRules += invalidIds.newRuleIds.reduce((acc, num) => acc + num, 0);
}

console.log("Invalid IDs sum", invalidIdsSum);
console.log("Invalid IDs sum (updated rules)", invalidIdsSumUpdatedRules);

//
//  Helper functions
//

function getInvalidIdsInRange(start, end) {
    const ids = [];
    const newRuleIds = [];

    while (start <= end) {
        const curr = String(start);
        const middle = Math.trunc(curr.length / 2);

        if (curr.slice(0, middle) === curr.slice(middle)) {
            ids.push(start);
            newRuleIds.push(start);
        } else {
            for (let i = middle; i > 0; i--) {
                if (isPattern(curr, i)) {
                    newRuleIds.push(start);
                }
            }
        }

        start++;
    }

    return {
        ids,
        newRuleIds
    };
}

function isPattern(str, size) {
    const chunk = str.substring(0, size);

    for (let i = size; i < str.length; i += size) {
        if (chunk !== str.slice(i, i + size)) {
            return false;
        }
    }

    return true;
}
