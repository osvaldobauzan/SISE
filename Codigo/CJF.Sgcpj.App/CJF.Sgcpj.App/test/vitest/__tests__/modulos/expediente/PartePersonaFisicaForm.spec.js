import { shallowMount } from "@vue/test-utils";
import { expect, test, vi } from "vitest";
import PartePersonaFisicaForm from "../../../../../src/modules/expediente/components/PartePersonaFisicaForm.vue";
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
const edadData = [
  { edadMenor: 17, descripcion: "17 años" },
  { edadMenor: 16, descripcion: "16 años" },
];
const sexoOptions = [
  { kIdSexo: "hombre", sDescripcion: "Hombre" },
  { kIdSexo: "mujer", sDescripcion: "Mujer" },
];
const lenguaData = [
  { leguaId: "zoqueNorte", lenguadescripcion: "Zoque del norte" },
  { leguaId: "zoqueSur", descripcion: "Zoque del sur" },
];
const grupoVulnerableJson = [
  { grupoVulnerableId: "mig", descripcion: "Migrantes" },
  { grupoVulnerableId: "tra", descripcion: "Trabajadores" },
];
test("PartePersonaFisicaForm renderiza el componente", async () => {
  const wrapper = shallowMount(PartePersonaFisicaForm, {
    props: {
      esEditar: true,
      value: {
        caracterPersonaId: 1,
        caracterPromueveNombre: 1,
        edadMenor: 17,
        grupoVulnerableId: 1,
        sexoId: "hombre",
        leguaId: 1,
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

  const edad = edadData.find(
    (option) => option.edadMenor == wrapper.vm.value.edadMenor,
  );
  expect(wrapper.vm.value.edadMenor).toEqual(edad);

  const grupoVulnerable = grupoVulnerableJson.find(
    (option) => option.grupoVulnerableId == wrapper.vm.value.grupoVulnerableId,
  );
  expect(wrapper.vm.value.grupoVulnerable).toEqual(grupoVulnerable);

  const sexo = sexoOptions.find(
    (option) => option.kIdSexo == wrapper.vm.value.kIdSexo,
  );
  expect(wrapper.vm.value.sexo).toEqual(sexo);

  const lengua = lenguaData.find(
    (option) => option.leguaId == wrapper.vm.value.leguaId,
  );
  expect(wrapper.vm.value.lenguaJson).toEqual(lengua);
});

test("Function filtrarTipoCaracter", async () => {
  const wrapper = shallowMount(PartePersonaFisicaForm);

  const val = "Quejoso";
  await wrapper.vm.filtrarTipoCaracter(val, updateMock);

  expect(mockFiltrarCombo(val, caracterPersona, "caracterPersona"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarCaracterPromueve", async () => {
  const wrapper = shallowMount(PartePersonaFisicaForm);

  const val = "En representacion";
  await wrapper.vm.filtrarCaracterPromueve(val, updateMock);

  expect(mockFiltrarCombo(val, caracterPromueveOptions, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarEdad", async () => {
  const wrapper = shallowMount(PartePersonaFisicaForm);

  const val = "17 años";
  await wrapper.vm.filtrarEdad(val, updateMock);

  expect(mockFiltrarCombo(val, edadData, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarSexo", async () => {
  const wrapper = shallowMount(PartePersonaFisicaForm);

  const val = "Hombre";
  await wrapper.vm.filtrarSexo(val, updateMock);

  expect(mockFiltrarCombo(val, sexoOptions, "sDescripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarLeguna", async () => {
  const wrapper = shallowMount(PartePersonaFisicaForm);

  const val = "Zoque del norte";
  await wrapper.vm.filtrarLeguna(val, updateMock);

  expect(mockFiltrarCombo(val, lenguaData, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function grupoVulnerableJson ", async () => {
  const wrapper = shallowMount(PartePersonaFisicaForm);

  const val = "Trabajadores";
  await wrapper.vm.filtrarGrupoVulnerable(val, updateMock);

  expect(mockFiltrarCombo(val, grupoVulnerableJson, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test('emite el evento "update:modelValue"', () => {
  const value = { value: "Hombre" };
  const wrapper = shallowMount(PartePersonaFisicaForm);

  wrapper.vm.cambiaronParametros(value);

  expect(wrapper.emitted("update:modelValue")).toBeTruthy();
});
