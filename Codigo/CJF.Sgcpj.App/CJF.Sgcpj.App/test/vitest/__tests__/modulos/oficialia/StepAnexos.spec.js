import { shallowMount } from "@vue/test-utils";
import StepAnexos from "../../../../../src/modules/oficialia/components/StepAnexos.vue";
import { expect, test } from "vitest";

test("StepAnexos renderiza el componente", () => {
  const wrapper = shallowMount(StepAnexos);
  expect(wrapper.exists()).toBeTruthy();
});
