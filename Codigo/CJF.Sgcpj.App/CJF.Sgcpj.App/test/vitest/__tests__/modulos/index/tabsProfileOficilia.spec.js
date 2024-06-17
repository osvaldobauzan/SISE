import { shallowMount } from "@vue/test-utils";
import tabsProfileOficialia from "../../../../../src/modules/index/components/oficialia/tabsProfileOficialia.vue";
import { expect, test } from "vitest";

test("tabsProfileOficialia renderiza el componente", () => {
  const wrapper = shallowMount(tabsProfileOficialia);
  expect(wrapper.exists()).toBeTruthy();
});
