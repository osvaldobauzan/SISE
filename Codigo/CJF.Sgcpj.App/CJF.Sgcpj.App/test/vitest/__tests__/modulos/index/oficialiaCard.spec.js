import { shallowMount } from "@vue/test-utils";
import oficialiaCard from "../../../../../src/modules/index/components/oficialia/oficialiaCard.vue";
import { expect, test } from "vitest";

test("OficialiaCard renderiza el componente", () => {
  const wrapper = shallowMount(oficialiaCard);
  expect(wrapper.exists()).toBeTruthy();
});
test("Funcion calcularPromociones", () => {
  const wrapper = shallowMount(oficialiaCard, {
    props: { rowsOficialia: [{ estado: 1 }] },
  });
  expect(wrapper.vm.calcularPromociones(1)).toBe(1);
});
test("computed  labels", () => {
  const wrapper = shallowMount(oficialiaCard, {
    props: { rowsOficialia: [{ estado: 1, expediente: { catTipoAsunto: 0 } }] },
  });
  expect(wrapper.vm.labels[0].name).toEqual("Todos");
});
test("computed  seriesPromociones ", () => {
  const wrapper = shallowMount(oficialiaCard, {
    props: { rowsOficialia: [{ estado: 1, expediente: { catTipoAsunto: 0 } }] },
  });
  expect(wrapper.vm.seriesPromociones[0]).toEqual(1);
});
test("computed  seriesTipoPromocionEscrito  ", () => {
  const wrapper = shallowMount(oficialiaCard, {
    props: { rowsOficialia: [{ estado: 1, expediente: { catTipoAsunto: 0 } }] },
  });
  expect(wrapper.vm.seriesTipoPromocionEscrito[0].name).toEqual("");
});
test("computed  optionsTipoPromocionEscritoInicial   ", () => {
  const wrapper = shallowMount(oficialiaCard, {
    props: { rowsOficialia: [{ estado: 1, expediente: { catTipoAsunto: 0 } }] },
  });
  expect(wrapper.vm.optionsTipoPromocionEscritoInicial).toBeTruthy();
});
