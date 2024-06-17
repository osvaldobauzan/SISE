import { shallowMount } from "@vue/test-utils";
import DetallePromocion from "../../../../../src/modules/oficialia/components/DetallePromocion.vue";
import { expect, test } from "vitest";

test("DetallePromocion renderiza el componente", () => {
  const wrapper = shallowMount(DetallePromocion);
  expect(wrapper.exists()).toBeTruthy();
});
