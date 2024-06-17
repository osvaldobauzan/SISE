import { shallowMount } from "@vue/test-utils";
import { expect, test } from "vitest";
import FiltrosColumnas from "../../../../../src/modules/tramite/components/FiltrosColumnas.vue";

test("FiltrosColumnas renderiza el componente", async () => {
  const wrapper = shallowMount(FiltrosColumnas);
  expect(wrapper.exists()).toBeTruthy();
});
