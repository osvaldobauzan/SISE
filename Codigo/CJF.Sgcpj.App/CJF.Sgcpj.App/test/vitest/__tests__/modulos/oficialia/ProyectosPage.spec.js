import { shallowMount } from "@vue/test-utils";
import ProyectosPage from "../../../../../src/modules/proyectos/pages/ProyectosPage.vue";
import { expect, test } from "vitest";

test("ProyectosPage renderiza el componente", () => {
  const wrapper = shallowMount(ProyectosPage);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function setFilter", () => {
  const wrapper = shallowMount(ProyectosPage);

  wrapper.vm.setFilterStatus("activo");
  expect(wrapper.vm.pagination.page).toBe(1);
});

test("Function setSelectedDate", () => {
  const wrapper = shallowMount(ProyectosPage);

  wrapper.vm.setSelectedDate("11-10-2023");
  expect(wrapper.vm.pagination.page).toBe(1);
});

test("Function setColoresList", () => {
  const wrapper = shallowMount(ProyectosPage);
  wrapper.vm.setColoresList();
  expect(wrapper.vm.coloresList.length).toEqual(8);
});
/*

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



*/
