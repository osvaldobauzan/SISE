import { shallowMount } from "@vue/test-utils";
import StepVincularPromocion from "../../../../../src/modules/oficialia/components/StepVincularPromocion.vue";
import { expect, test } from "vitest";

test("StepVincularPromocion renderiza el componente", () => {
  const wrapper = shallowMount(StepVincularPromocion);
  expect(wrapper.exists()).toBeTruthy();
});
