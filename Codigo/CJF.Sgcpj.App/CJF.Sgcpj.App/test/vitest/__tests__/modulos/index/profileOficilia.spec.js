import { shallowMount } from "@vue/test-utils";
import profileOficialia from "../../../../../src/modules/index/components/oficialia/profileOficialia.vue";
import { expect, test } from "vitest";

test("profileOficialia renderiza el componente", () => {
  const wrapper = shallowMount(profileOficialia);
  expect(wrapper.exists()).toBeTruthy();
});
