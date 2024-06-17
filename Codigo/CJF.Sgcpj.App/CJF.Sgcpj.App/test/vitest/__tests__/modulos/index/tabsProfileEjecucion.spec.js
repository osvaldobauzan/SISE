import { shallowMount } from "@vue/test-utils";
import tabsProfileEjecucion from "../../../../../src/modules/index/components/ejecucion/tabsProfileEjecucion.vue";
import { expect, test } from "vitest";

test("tabsProfileEjecucion renderiza el componente", () => {
  const wrapper = shallowMount(tabsProfileEjecucion);
  expect(wrapper.exists()).toBeTruthy();
});
