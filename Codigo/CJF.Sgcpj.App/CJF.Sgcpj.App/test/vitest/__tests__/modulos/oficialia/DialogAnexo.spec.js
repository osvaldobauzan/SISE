import { shallowMount } from "@vue/test-utils";
import DialogAnexo from "../../../../../src/modules/oficialia/components/DialogAnexo.vue";
import { expect, test, vi } from "vitest";
import { Utils } from "../../../../../src/helpers/utils.js";

const mockFiltrarCombo = vi
  .spyOn(Utils, "filtrarCombo")
  .mockImplementation(() => []);
const mockMarcaPrimeraOpcionCombo = vi
  .spyOn(Utils, "marcaPrimeraOpcionCombo")
  .mockImplementation(() => {});

const updateMock = (asyncCallback, syncCallback) => {
  asyncCallback();
  syncCallback();
};

test("Dialog renderiza el componente", () => {
  const wrapper = shallowMount(DialogAnexo);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function filtrarTiposAnexo", async () => {
  const wrapper = shallowMount(DialogAnexo);
  const val = "original";
  const tipoProcedimiento = [
    { descripcion: "original" },
    { descripcion: "simple" },
  ];

  await wrapper.vm.filtrarTiposAnexo(val, updateMock);

  expect(mockFiltrarCombo(val, tipoProcedimiento, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarDescripcionAnexo", async () => {
  const wrapper = shallowMount(DialogAnexo);
  const val = "acciones";
  const tipoProcedimiento = [
    { descripcion: "acciones" },
    { descripcion: "cheque" },
  ];

  await wrapper.vm.filtrarDescripcionAnexo(val, updateMock);

  expect(mockFiltrarCombo(val, tipoProcedimiento, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});
test("Function filtrarCaracterAnexo", async () => {
  const wrapper = shallowMount(DialogAnexo);
  const val = "prueba";
  const tipoProcedimiento = [
    { descripcion: "prueba" },
    { descripcion: "anexo" },
  ];

  await wrapper.vm.filtrarCaracterAnexo(val, updateMock);

  expect(mockFiltrarCombo(val, tipoProcedimiento, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function guardaAnexo", async () => {
  const propsData = {
    esEditar: true,
  };
  const wrapper = shallowMount(DialogAnexo, { propsData });

  await wrapper.vm.guardaAnexo();

  if (propsData.esEditar) {
    expect(wrapper.emitted("update:anexoValue")).toBeTruthy();
  } else {
    expect(wrapper.emitted("add:anexoValue")).toBeTruthy();
  }
});
