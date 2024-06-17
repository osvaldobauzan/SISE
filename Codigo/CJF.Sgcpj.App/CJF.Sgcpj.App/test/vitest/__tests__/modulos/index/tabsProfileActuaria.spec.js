import { shallowMount } from "@vue/test-utils";
import tabsProfileActuaria from "../../../../../src/modules/index/components/actuaria/tabsProfileActuaria.vue";
import { expect, test } from "vitest";

test("tabsProfileActuaria renderiza el componente", () => {
  const wrapper = shallowMount(tabsProfileActuaria);
  expect(wrapper.exists()).toBeTruthy();
});
