import fs from "node:fs";

const fileData = fs.readFileSync("input.txt", "utf-8");

const banks = fileData
    .split(/\r?\n/)
    .map((line) => line.split('').map(Number));

const totalOutputJoltage = banks.map((b) => getMaxJoltageForBank(b, 2)).reduce((acc, num) => acc + num, 0);

console.log("Total output joltage:", totalOutputJoltage);

const total12PackOutputJoltage = banks.map((b) => getMaxJoltageForBank(b, 12)).reduce((acc, num) => acc + num, 0);

console.log("Total 12 pack output joltage:", total12PackOutputJoltage);

//
// Helper functions
//
function getMaxJoltageForBank(bank, limit = 2) {
    const result = [];

    for (let i = 0; i < bank.length; i++) {
        const bankLeft = bank.length - i;

        if (
            (result.length < limit && limit - result.length === bankLeft) ||
            (result.length === 0 || (result.length < limit && bank[i] <= result.at(-1)))
        ) {
            result.push(bank[i]);
        } else if (bank[i] > result.at(-1)) {
            result.pop();
            i--;
        }
    }

    return result.reduce((acc, num) => acc * 10 + num, 0);
}
