import { shallowMount } from "@vue/test-utils";
import profileSentencias from "../../../../../src/modules/index/components/sentencias/profileSentencias.vue";
import { expect, test } from "vitest";

test("profileSentencias renderiza el componente", () => {
  const wrapper = shallowMount(profileSentencias);
  expect(wrapper.exists()).toBeTruthy();
});
