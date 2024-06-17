import { shallowMount } from "@vue/test-utils";
import ActuarioPage from "../../../../../src/modules/actuaria/pages/ActuarioPage.vue";
import { expect, test } from "vitest";

test("ActuarioPage renderiza el componente", () => {
  const wrapper = shallowMount(ActuarioPage);
  expect(wrapper.exists()).toBeTruthy();
});
