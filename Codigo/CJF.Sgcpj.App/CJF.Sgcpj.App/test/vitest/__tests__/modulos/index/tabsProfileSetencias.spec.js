import { shallowMount } from "@vue/test-utils";
import tabsProfileSentencias from "../../../../../src/modules/index/components/sentencias/tabsProfileSentencias.vue";
import { expect, test } from "vitest";

test("tabsProfileSentencias renderiza el componente", () => {
  const wrapper = shallowMount(tabsProfileSentencias);
  expect(wrapper.exists()).toBeTruthy();
});
