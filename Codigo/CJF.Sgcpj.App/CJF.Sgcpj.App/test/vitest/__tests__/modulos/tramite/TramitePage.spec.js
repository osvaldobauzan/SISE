import { shallowMount } from "@vue/test-utils";
import { expect, test } from "vitest";
import TramitePage from "../../../../../src/modules/tramite/pages/TramitePage.vue";

test("TramitePage renderiza el componente", () => {
  const wrapper = shallowMount(TramitePage);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function setFilter", () => {
  const wrapper = shallowMount(TramitePage);

  wrapper.vm.setFilterStatus("activo");
  expect(wrapper.vm.pagination.page).toBe(1);
});

test("Function setSelectedDate", () => {
  const wrapper = shallowMount(TramitePage);

  wrapper.vm.setSelectedDate("11-10-2023");
  expect(wrapper.vm.pagination.page).toBe(1);
});
test("Function cambioFiltro", async () => {
  const wrapper = shallowMount(TramitePage);
  const seleccionado = {
    asunto: "asuntoTest",
  };
  await wrapper.vm.cambioFiltro(seleccionado);

  for (const key in seleccionado) {
    expect(wrapper.vm.valoresFiltros[key]).toBe(seleccionado[key]);
  }
});
test("Function setDocumento", () => {
  const wrapper = shallowMount(TramitePage);
  wrapper.vm.setDocumento("acuerdo");
  expect(wrapper.vm.showDialogPdf).toBeTruthy();
  wrapper.vm.setDocumento("promo");
  expect(wrapper.vm.showPromocion).toBeTruthy();
});
test("Function setColoresList", () => {
  const wrapper = shallowMount(TramitePage);
  wrapper.vm.setColoresList();
  expect(wrapper.vm.coloresList.length).toEqual(6);
});
