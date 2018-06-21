const gb = require("../gbtest")(__filename);

test("plus-store", () => {
  gb.run();

  expect(gb.depth).toBe(0);
  expect(gb.memory[0x8501]).toBe(0x99);
  expect(gb.memory[0x8502]).toBe(0xcc);
});
