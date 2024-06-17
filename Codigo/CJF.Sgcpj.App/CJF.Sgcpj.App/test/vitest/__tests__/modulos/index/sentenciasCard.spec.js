import { shallowMount } from "@vue/test-utils";
import sentenciasCard from "../../../../../src/modules/index/components/sentencias/sentenciasCard.vue";
import { expect, test } from "vitest";

test("sentenciasCard renderiza el componente", () => {
  const wrapper = shallowMount(sentenciasCard);
  expect(wrapper.exists()).toBeTruthy();
});
