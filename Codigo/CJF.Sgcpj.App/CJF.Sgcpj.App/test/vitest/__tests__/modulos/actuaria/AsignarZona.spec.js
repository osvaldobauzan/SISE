import { shallowMount } from "@vue/test-utils";
import { expect, test, vi, describe } from "vitest";
import AsignarZona from "../../../../../src/modules/actuaria/components/AsignarZona.vue";
import { Utils } from "../../../../../src/helpers/utils.js";

const parte = { asignadoActuario: true };
vi.mock(
  "../../../../../src/modules/actuaria/stores/actuaria-detalle-notificaciones",
  () => ({
    actuariaDetalleNotificacionesStore: {
      postAgregarActuario: vi.fn(() => Promise.resolve("respuesta simulada")),
      putAgregarActuario: vi.fn(() => Promise.resolve("respuesta simulada")),
    },
  }),
);

test("AsignarZona renderiza el componente", () => {
  const wrapper = shallowMount(AsignarZona, {
    props: { parte },
  });
  expect(wrapper.exists()).toBeTruthy();
});

test("Function cambioForm", async () => {
  const validateMock = vi.fn().mockResolvedValue(true);

  const wrapper = shallowMount(AsignarZona, {
    props: { parte },
  });

  wrapper.vm.selectZona = { validate: validateMock };

  await wrapper.vm.cambioForm("1812");
  expect(validateMock).toHaveBeenCalled();

  expect(wrapper.vm.formValido).toBe(true);
  expect(wrapper.vm.cambioFormulario).toBe(true);
});

test("Function filtraZona", async () => {
  const wrapper = shallowMount(AsignarZona, {
    props: { parte },
  });

  const filtrarCombo = vi
    .spyOn(Utils, "filtrarCombo")
    .mockImplementation(() => []);
  const marcaPrimeraOpcionCombo = vi
    .spyOn(Utils, "marcaPrimeraOpcionCombo")
    .mockImplementation(() => {});

  const updateMock = vi.fn((fnActualizar, fnMarcarPrimeraOpcion) => {
    fnActualizar();
    fnMarcarPrimeraOpcion();
  });

  const empleados = [
    { nombreEmpleado: "Empleado1" },
    { nombreEmpleado: "Empleado2" },
  ];

  const val = "Empleado1";
  await wrapper.vm.filtrarZona(val, updateMock);
  expect(filtrarCombo(val, empleados, "nombreEmpleado"));
  expect(marcaPrimeraOpcionCombo).toHaveBeenCalled();
});
test("Function crearAsignacion", async () => {
  const wrapper = shallowMount(AsignarZona, {
    props: { parte },
  });

  const params = {
    asuntoNeunId: "123",
    asuntoId: "38221",
    actuarioId: "9334833",
    parte: "71623",
  };
  wrapper.vm.crearAsignacion(params);
  wrapper.vm.actuariaDetalleNotificacionesStore.postAgregarActuario(params);
});
test("Function editarAsignacion", async () => {
  const wrapper = shallowMount(AsignarZona, {
    props: { parte },
  });

  const params = {
    asuntoNeunId: "123",
    asuntoId: "38221",
    actuarioId: "93343",
    parte: "71623",
  };
  wrapper.vm.editarAsignacion(params);

  wrapper.vm.actuariaDetalleNotificacionesStore.putAgregarActuario(params);
});
describe("Function cancelar", () => {
  test("ShowCancelar true", () => {
    const wrapper = shallowMount(AsignarZona, {
      props: { parte },
    });

    wrapper.vm.cambioFormulario = true;
    wrapper.vm.showCancelar = false;

    wrapper.vm.cancelar();
    expect(wrapper.vm.showCancelar).toBe(true);
  });

  test("emite evento cerrar", () => {
    const wrapper = shallowMount(AsignarZona, {
      props: { parte },
    });
    wrapper.vm.cambioFormulario = false;

    wrapper.vm.cancelar();

    expect(wrapper.emitted()).toHaveProperty("cerrar");
  });
});
