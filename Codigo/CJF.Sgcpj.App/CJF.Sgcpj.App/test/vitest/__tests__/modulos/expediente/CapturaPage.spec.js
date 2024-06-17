import { shallowMount } from "@vue/test-utils";
import CapturaPage from "../../../../../src/modules/expediente/pages/CapturaPage.vue";
import { expect, test } from "vitest";

test("AddPromocion renderiza el componente", () => {
  const wrapper = shallowMount(CapturaPage);
  expect(wrapper.exists()).toBeTruthy();
});
