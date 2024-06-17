import { shallowMount } from "@vue/test-utils";
import tabsProfileTramite from "../../../../../src/modules/index/components/tramite/tabsProfileTramite.vue";
import { expect, test } from "vitest";

test("tabsProfileTramite renderiza el componente", () => {
  const wrapper = shallowMount(tabsProfileTramite);
  expect(wrapper.exists()).toBeTruthy();
});
