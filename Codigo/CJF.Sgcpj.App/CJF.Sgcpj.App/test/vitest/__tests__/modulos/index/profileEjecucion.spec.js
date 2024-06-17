import { shallowMount } from "@vue/test-utils";
import profileEjecucion from "../../../../../src/modules/index/components/ejecucion/profileEjecucion.vue";
import { expect, test } from "vitest";

test("profileEjecucion renderiza el componente", () => {
  const wrapper = shallowMount(profileEjecucion);
  expect(wrapper.exists()).toBeTruthy();
});
