// eslint-disable-next-line @typescript-eslint/no-var-requires
const tsNameof = require('ts-nameof');

module.exports = () => ({
    before: [tsNameof],
});
