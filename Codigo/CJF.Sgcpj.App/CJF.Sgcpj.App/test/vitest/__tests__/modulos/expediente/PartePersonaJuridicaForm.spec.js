import { shallowMount } from "@vue/test-utils";
import { expect, test, vi } from "vitest";
import PartePersonaJuridicaForm from "../../../../../src/modules/expediente/components/PartePersonaJuridicaForm.vue";
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
const caracterPersona = [
  { caracterPersonaId: 1, caracterPersona: "Quejoso" },
  { caracterPersonaId: 2, caracterPersona: "Autoridad" },
];
const caracterPromueveOptions = [
  { caracterPromueveNombre: 1, descripcion: "Promueve por propio" },
  { caracterPromueveNombre: 2, descripcion: "En representacion" },
];

const tipoPersonaJuridica = [
  { catTipoPerJuridicaId: 1, descripcion: "Sociedad cooperativa" },
  { catTipoPerJuridicaId: 1, descripcion: "Sociedad civil" },
];

test("PartePersonaJuridicaForm renderiza el componente", async () => {
  const wrapper = shallowMount(PartePersonaJuridicaForm, {
    props: {
      esEditar: true,
      value: {
        caracterPersonaId: 1,
        catTipoPerJuridicaId: 2,
        caracterPromueveNombre: 1,
      },
    },
  });
  expect(wrapper.exists()).toBeTruthy();

  const tipoCaracter = caracterPersona.find(
    (option) => option.caracterPersonaId == wrapper.vm.value.caracterPersonaId,
  );
  expect(wrapper.vm.value.caracterPersona).toEqual(tipoCaracter);
  const tipoPromueve = caracterPromueveOptions.find(
    (option) =>
      option.caracterPromueveNombre == wrapper.vm.value.caracterPromueveNombre,
  );
  expect(wrapper.vm.value.caracterPromueve).toEqual(tipoPromueve);
  const tipoJuridica = tipoPersonaJuridica.find(
    (option) =>
      option.catTipoPerJuridicaId == wrapper.vm.value.catTipoPerJuridicaId,
  );
  expect(wrapper.vm.value.catTipoPersonaJuridica).toEqual(tipoJuridica);
});

test("Function filtrarTipoCaracter", async () => {
  const wrapper = shallowMount(PartePersonaJuridicaForm);

  const val = "Quejoso";
  await wrapper.vm.filtrarTipoCaracter(val, updateMock);

  expect(mockFiltrarCombo(val, caracterPersona, "caracterPersona"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarCaracterPromueve", async () => {
  const wrapper = shallowMount(PartePersonaJuridicaForm);

  const val = "En representacion";
  await wrapper.vm.filtrarCaracterPromueve(val, updateMock);

  expect(mockFiltrarCombo(val, caracterPromueveOptions, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarPersonaJuridica", async () => {
  const wrapper = shallowMount(PartePersonaJuridicaForm);

  const val = "Sociedad cooperativa";
  await wrapper.vm.filtrarPersonaJuridica(val, updateMock);

  expect(mockFiltrarCombo(val, tipoPersonaJuridica, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test('emite el evento "update:modelValue"', () => {
  const value = { value: "Administrativa local" };
  const wrapper = shallowMount(PartePersonaJuridicaForm);

  wrapper.vm.cambiaronParametros(value);

  expect(wrapper.emitted("update:modelValue")).toBeTruthy();
});
