const gb = require("../gbtest")(__filename);

test("mod", () => {
  gb.run();
  expect(gb.stack).toEqual([0, 11, 0, 3, 11, 0, 1, gb.int(-3)]);
});
