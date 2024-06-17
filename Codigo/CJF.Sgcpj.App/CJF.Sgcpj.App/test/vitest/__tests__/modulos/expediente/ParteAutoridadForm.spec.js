import { shallowMount } from "@vue/test-utils";
import { expect, test, vi } from "vitest";
import ParteAutoridadForm from "../../../../../src/modules/expediente/components/ParteAutoridadForm.vue";
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

const clasificacionAutoridadGenerica = [
  { clasificaAutoridadGenericaId: 1, descripcion: "Administrativa federal" },
  { clasificaAutoridadGenericaId: 2, descripcion: "Administrativa local" },
];

test("ParteAutoridadForm renderiza el componente", async () => {
  const wrapper = shallowMount(ParteAutoridadForm, {
    props: {
      esEditar: true,
      value: {
        caracterPersonaId: 1,
        clasificaAutoridadGenericaId: 1,
        caracterPromueveNombre: 1,
      },
    },
  });
  expect(wrapper.exists()).toBeTruthy();

  const tipoCaracter = caracterPersona.find(
    (opcion) => opcion.caracterPersonaId == wrapper.vm.value.caracterPersonaId,
  );
  expect(wrapper.vm.value.caracterPersona).toEqual(tipoCaracter);

  const tipoPromueve = caracterPromueveOptions.find(
    (opcion) =>
      opcion.caracterPromueveNombre == wrapper.vm.value.caracterPromueveNombre,
  );
  expect(wrapper.vm.value.caracterPromueve).toEqual(tipoPromueve);

  const tipoAutoridad = clasificacionAutoridadGenerica.find(
    (opcion) =>
      opcion.clasificaAutoridadGenericaId ==
      wrapper.vm.value.clasificaAutoridadGenericaId,
  );
  expect(wrapper.vm.value.clasificaAutoridadGenerica).toEqual(tipoAutoridad);
});
test("Function filtrarTipoCaracter", async () => {
  const wrapper = shallowMount(ParteAutoridadForm);

  const val = "Quejoso";
  await wrapper.vm.filtrarTipoCaracter(val, updateMock);

  expect(mockFiltrarCombo(val, caracterPersona, "caracterPersona"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarCaracterPromueve", async () => {
  const wrapper = shallowMount(ParteAutoridadForm);

  const val = "En representacion";
  await wrapper.vm.filtrarCaracterPromueve(val, updateMock);

  expect(mockFiltrarCombo(val, caracterPromueveOptions, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarClasificacionAutoridad", async () => {
  const wrapper = shallowMount(ParteAutoridadForm);

  const val = "Administrativa federal";
  await wrapper.vm.filtrarClasificacionAutoridad(val, updateMock);

  expect(mockFiltrarCombo(val, clasificacionAutoridadGenerica, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test('emite el evento "update:modelValue"', () => {
  const value = { value: "Administrativa local" };
  const wrapper = shallowMount(ParteAutoridadForm);

  wrapper.vm.cambiaronParametros(value);

  expect(wrapper.emitted("update:modelValue")).toBeTruthy();
});
