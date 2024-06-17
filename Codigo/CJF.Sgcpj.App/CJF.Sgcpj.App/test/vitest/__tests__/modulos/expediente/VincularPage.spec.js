import { shallowMount } from "@vue/test-utils";
import VincularPage from "../../../../../src/modules/expediente/pages/VincularPage.vue";
import { expect, test } from "vitest";

test("VincularPage renderiza el componente", () => {
  const wrapper = shallowMount(VincularPage);
  expect(wrapper.exists()).toBeTruthy();
});
