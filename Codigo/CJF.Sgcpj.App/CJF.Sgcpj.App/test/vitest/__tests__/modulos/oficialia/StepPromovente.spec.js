import { shallowMount } from "@vue/test-utils";
import StepPromovente from "../../../../../src/modules/oficialia/components/StepPromovente.vue";
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

test("StepPromovente renderiza el componente", () => {
  const wrapper = shallowMount(StepPromovente);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function filtrarTipoPersona", async () => {
  const wrapper = shallowMount(StepPromovente);
  const val = "fisica";
  const tipoProcedimiento = [
    { descripcion: "fisica" },
    { descripcion: "juridica" },
  ];

  await wrapper.vm.filtrarTipoPersona(val, updateMock);

  expect(mockFiltrarCombo(val, tipoProcedimiento, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarTipoPromovente", async () => {
  const wrapper = shallowMount(StepPromovente);
  const val = "parteQuejosa";
  const tipoProcedimiento = [
    { descripcion: "parteQuejosa" },
    { descripcion: "autoridadResponsable" },
  ];

  await wrapper.vm.filtrarTipoPromovente(val, updateMock);

  expect(mockFiltrarCombo(val, tipoProcedimiento, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarTipoCaracter", async () => {
  const wrapper = shallowMount(StepPromovente);

  const val = "Quejoso";
  const caracterPersona = [
    { caracterPersonaId: 1, caracterPersona: "Quejoso" },
    { caracterPersonaId: 2, caracterPersona: "Autoridad" },
  ];
  await wrapper.vm.filtrarTipoCaracter(val, updateMock);

  expect(mockFiltrarCombo(val, caracterPersona, "caracterPersona"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarParteExistente", async () => {
  const wrapper = shallowMount(StepPromovente);

  const val = "personaTipo";
  const parteExistente = [
    { personaTipo: "personaTipo" },
    { personaTipo: "personaTipo2" },
  ];
  await wrapper.vm.filtrarParteExistente(val, updateMock);

  expect(mockFiltrarCombo(val, parteExistente, "personaTipo"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});
