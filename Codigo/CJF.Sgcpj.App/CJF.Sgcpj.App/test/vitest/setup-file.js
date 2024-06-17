// This file will be run before each test file
import { config } from "@vue/test-utils";
import { createTestingPinia } from "@pinia/testing";
import { Quasar, Notify } from "quasar";
import { vi } from "vitest";

config.global.plugins.push([
  Quasar,
  {
    plugins: { Notify },
  },
]);

config.global.plugins.push(
  createTestingPinia({
    createSpy: vi.fn,
  }),
);
