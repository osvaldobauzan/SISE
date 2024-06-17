import { shallowMount } from "@vue/test-utils";
import profileTramite from "../../../../../src/modules/index/components/tramite/profileTramite.vue";
import { expect, test } from "vitest";

test("profileTramiterenderiza el componente", () => {
  const wrapper = shallowMount(profileTramite);
  expect(wrapper.exists()).toBeTruthy();
});
