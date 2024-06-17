import { shallowMount } from "@vue/test-utils";
import StepPromocion from "../../../../../src/modules/oficialia/components/StepPromocion.vue";
import { expect, test, vi } from "vitest";
import { Utils } from "../../../../../src/helpers/utils.js";
const promocion = {
  expediente: {
    asuntoNeunId: 30326298,
    asuntoAlias: "43448/2024",
    catTipoOrganismoId: 0,
    catOrganismoId: 180,
    catTipoAsunto: "Procesos Civiles o Administrativos",
    catTipoAsuntoId: "4",
    tipoProcedimiento: "",
    nombreCorto: null,
  },
  numeroOrden: 431,
  yearPromocion: 2024,
  origenPromocion: 4,
  origen: 6,
  nombreOrigen: "",
  origenPromocionDescripcion: "OFICIALÍA",
  numeroRegistro: 59765,
  fechaPresentacion: "2024-02-28T09:12:00",
  fechaPresentacionFin: "0001-01-01T00:00:00",
  mesa: "Mesa III",
  clasePromocion: 0,
  clasePromocionDescripcion: null,
  copias: 0,
  anexos: 0,
  color: null,
  cuadernoId: 0,
  secretarioId: 59609,
  cuadernoNombreCorto: null,
  cuadernoNombre: null,
  secretarioDescripcion: "Jesús Eduardo Acosta Moroyoqui",
  secretarioUserName: "JesAcostaMor",
  secretarioNombres: null,
  secretarioPaterno: null,
  secretarioMaterno: null,
  tipoContenidoDescripcion: "Arraigo",
  tipoContenidoId: 0,
  parteDescripcion: "Juan Perez Torres",
  tipoPromovente: 0,
  clasePromoventeId: 108032,
  clasePromoventeDescripcion: "Autoridad Judicial",
  esDemandaElectronica: false,
  esDemanda: false,
  esPromocionE: false,
  cambioDemandaPromocion: false,
  conAcuerdo: false,
  estado: 4,
  estadoAcuerdo: 1,
  fojas: 1,
  kIdElectronica: null,
  usuarioCaptura: "EZamoranoG",
  fechaCaptura: "2024-02-28T09:14:29.523",
  detalle: null,
  catAutorizacionDocumentosId: null,
  conArchivo: true,
  nombreOficial: null,
  index: 0,
};

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

test("StepPromocion renderiza el componente", async () => {
  vi.mock(
    "../../../../../src/modules/oficialia/stores/oficialia-store",
    async () => {
      return {
        useOficialiaStore: vi.fn(() => ({
          promocion: {},
        })),
      };
    },
  );
  const wrapper = shallowMount(StepPromocion, { props: { promocion } });
  await wrapper.vm.$nextTick();

  expect(wrapper.exists()).toBeTruthy();
});

test("Function refrescaCatalogosDependientes", async () => {
  const wrapper = shallowMount(StepPromocion);
  await wrapper.vm.$nextTick();
  const valor = {
    catTipoAsuntoId: 12,
  };
  wrapper.vm.refrescaCatalogosDependientes(valor);
});

test("Function ajustarLongitud", async () => {
  const wrapper = shallowMount(StepPromocion);
  wrapper.vm.ajustarLongitud(232, 13);
});
test("Function entradaOCC", async () => {
  const wrapper = shallowMount(StepPromocion);
  wrapper.vm.parametros.numeroOcc = "201123/2024";
  wrapper.vm.entradaOCC();
});
test("Function setExpedienteNuevo cuando es edicion", async () => {
  const wrapper = shallowMount(StepPromocion);
  wrapper.vm.esEdicion = true;
  wrapper.vm.setExpedienteNuevo();
});
test("Function setExpedienteNuevo cuando no es edicion", async () => {
  const wrapper = shallowMount(StepPromocion);
  wrapper.vm.esEdicion = false;
  wrapper.vm.parametros.expedienteEncontrado = {
    id: 812,
  };
  wrapper.vm.setExpedienteNuevo();
});
test("Function numeroPromocionExistente", async () => {
  const wrapper = shallowMount(StepPromocion);
  wrapper.vm.esEdicion = false;
  wrapper.vm.parametros.expedienteEncontrado = {
    id: 812,
  };
  wrapper.vm.numeroPromocionExistente(232);
});

test("Function validaNoExpedienteApi", async () => {
  const wrapper = shallowMount(StepPromocion, { props: { promocion } });
  await wrapper.vm.$nextTick();

  wrapper.vm.validaNoExpedienteApi(121312);
});

test("Function calculaNumeroExpediente", async () => {
  const wrapper = shallowMount(StepPromocion, { props: { promocion } });
  await wrapper.vm.$nextTick();
  wrapper.vm.calculaNumeroExpediente(2112);
});
test("Function filtrarTipoAsunto", async () => {
  const wrapper = shallowMount(StepPromocion);
  const cuadernos = [{ cuaderno: "varios" }, { cuaderno: "causa_penal" }];

  const val = "varios";
  await wrapper.vm.filtrarTipoAsunto(val, updateMock);

  expect(mockFiltrarCombo(val, cuadernos, "cuaderno"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarCuaderno", async () => {
  const wrapper = shallowMount(StepPromocion);
  const tipoAsunto = [{ cuaderno: "principal" }, { cuaderno: "incidental" }];

  const val = "principal";
  await wrapper.vm.filtrarCuaderno(val, updateMock);

  expect(mockFiltrarCombo(val, tipoAsunto, "tipoAsunto"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarContenido", async () => {
  const wrapper = shallowMount(StepPromocion);
  const val = "Autoridad";
  const contenidos = [{ contenido: "Autoridad" }, { contenido: "Audiencia" }];

  await wrapper.vm.filtrarContenido(val, updateMock);

  expect(mockFiltrarCombo(val, contenidos, "contenido"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarTipoProcedimiento", async () => {
  const wrapper = shallowMount(StepPromocion);
  const val = "arraigo";
  const tipoProcedimiento = [
    { descripcion: "arraigo" },
    { descripcion: "cateo" },
  ];

  await wrapper.vm.filtrarTipoProcedimiento(val, updateMock);

  expect(mockFiltrarCombo(val, tipoProcedimiento, "descripcion"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarSecretarios", async () => {
  const wrapper = shallowMount(StepPromocion);
  const val = "123";
  const secretario = [{ completo: "Juan Perez" }, { completo: "Luis Lopez" }];

  await wrapper.vm.filtrarSecretarios(val, updateMock);

  expect(mockFiltrarCombo(val, secretario, "completo"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function secretarioSugerido", async () => {
  const wrapper = shallowMount(StepPromocion);

  await wrapper.vm.secretarioSugerido(483);
});

test("Function expedienteEncontrado", async () => {
  const wrapper = shallowMount(StepPromocion);
  const val = {
    catTipoAsuntoId: 92,
    description: "Causa",
  };
  wrapper.vm.esEdicion = true;
  await wrapper.vm.expedienteEncontrado(val);
});
