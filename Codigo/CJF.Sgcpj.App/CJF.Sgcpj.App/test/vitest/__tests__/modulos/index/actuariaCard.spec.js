import { shallowMount } from "@vue/test-utils";
import actuariaCard from "../../../../../src/modules/index/components/actuaria/actuariaCard.vue";
import { expect, test } from "vitest";

test("actuariaCard renderiza el componente", () => {
  const wrapper = shallowMount(actuariaCard);
  expect(wrapper.exists()).toBeTruthy();
});
