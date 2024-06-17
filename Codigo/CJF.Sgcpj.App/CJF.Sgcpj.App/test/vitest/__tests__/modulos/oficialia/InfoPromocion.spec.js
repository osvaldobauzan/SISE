import { shallowMount } from "@vue/test-utils";
import InfoPromocion from "../../../../../src/modules/oficialia/components/InfoPromocion.vue";
import { expect, test } from "vitest";

test("InfoPromocion renderiza el componente", () => {
  const wrapper = shallowMount(InfoPromocion);
  expect(wrapper.exists()).toBeTruthy();
});
