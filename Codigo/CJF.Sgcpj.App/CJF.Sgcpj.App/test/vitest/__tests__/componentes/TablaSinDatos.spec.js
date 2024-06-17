import TablaSinDatosVue from "../../../../src/components/TablaSinDatos.vue";
import { shallowMount } from "@vue/test-utils";
import { expect, test } from "vitest";

test("TablaSinDatosVue Component renders the correct titulo", () => {
  const wrapper = shallowMount(TablaSinDatosVue, {
    props: { titulo: "Hola" },
  });

  expect(wrapper.find(".text-h4").text()).toBe("Hola");
});

test("TablaSinDatosVue Component renders the correct subTitulo", () => {
  const wrapper = shallowMount(TablaSinDatosVue, {
    props: { subTitulo: "Mundo" },
  });
  expect(wrapper.find(".text-subtitle1").text()).toBe("Mundo");
});
