import { shallowMount } from "@vue/test-utils";
import OficialiaPage from "../../../../../src/modules/oficialia/pages/OficialiaPage.vue";
import { expect, test } from "vitest";

test("OficialiaPage renderiza el componente", () => {
  const wrapper = shallowMount(OficialiaPage);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function setFilter", () => {
  const wrapper = shallowMount(OficialiaPage);

  wrapper.vm.setFilterStatus("activo");
  expect(wrapper.vm.pagination.page).toBe(1);
});

test("Function setSelectedDate", () => {
  const wrapper = shallowMount(OficialiaPage);

  wrapper.vm.setSelectedDate("11-10-2023");
  expect(wrapper.vm.pagination.page).toBe(1);
});

test("Function cambioFiltro", async () => {
  const wrapper = shallowMount(OficialiaPage);
  const seleccionado = {
    asunto: "asuntoTest",
  };
  await wrapper.vm.cambioFiltro(seleccionado);

  for (const key in seleccionado) {
    expect(wrapper.vm.valoresFiltros[key]).toBe(seleccionado[key]);
  }
});

test("Function setColoresList", () => {
  const wrapper = shallowMount(OficialiaPage);
  wrapper.vm.setColoresList();
  expect(wrapper.vm.coloresList.length).toEqual(4);
});
