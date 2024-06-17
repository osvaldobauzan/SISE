import { shallowMount } from "@vue/test-utils";
import profileActuaria from "../../../../../src/modules/index/components/actuaria/profileActuaria.vue";
import { expect, test } from "vitest";

test("profileActuaria renderiza el componente", () => {
  const wrapper = shallowMount(profileActuaria);
  expect(wrapper.exists()).toBeTruthy();
});
