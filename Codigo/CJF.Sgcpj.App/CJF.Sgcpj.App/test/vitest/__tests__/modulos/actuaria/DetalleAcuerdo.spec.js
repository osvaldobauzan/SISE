import { shallowMount } from "@vue/test-utils";
import { expect, test } from "vitest";
import DetalleAcuerdo from "../../../../../src/modules/actuaria/components/DetalleAcuerdo.vue";

test("DetalleAcuerdo renderiza el componente", () => {
  const item = { parte: "detalleParte" };
  const wrapper = shallowMount(DetalleAcuerdo, {
    props: { item },
  });
  expect(wrapper.exists()).toBeTruthy();
});
