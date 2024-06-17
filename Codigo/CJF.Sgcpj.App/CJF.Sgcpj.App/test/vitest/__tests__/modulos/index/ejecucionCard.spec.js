import { shallowMount } from "@vue/test-utils";
import ejecucionCard from "../../../../../src/modules/index/components/ejecucion/ejecucionCard.vue";
import { expect, test } from "vitest";

test("ejecucionCard renderiza el componente", () => {
  const wrapper = shallowMount(ejecucionCard);
  expect(wrapper.exists()).toBeTruthy();
});
