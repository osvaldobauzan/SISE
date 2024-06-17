import { shallowMount } from "@vue/test-utils";
import { describe, expect, test } from "vitest";
import FiltrosColumnasNotificaciones from "../../../../../src/modules/actuaria/components/FiltrosColumnasNotificaciones.vue";

test("FiltrosColumnasNotificaciones renderiza el componente", async () => {
  const wrapper = shallowMount(FiltrosColumnasNotificaciones);
  expect(wrapper.exists()).toBeTruthy();
});

describe("Function cambioFiltro", () => {
  test("cambioFiltro actualiza valoresFiltros y emite el evento", async () => {
    const wrapper = shallowMount(FiltrosColumnasNotificaciones);
    const seleccionado = { filtroValor: "filtro1", value: "valor1" };

    await wrapper.vm.cambioFiltro(seleccionado);

    expect(wrapper.vm.valoresFiltros.filtro1).toBe("valor1");
    expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
  });

  test("cambioFiltro no emite el evento si value es null", async () => {
    const wrapper = shallowMount(FiltrosColumnasNotificaciones);
    const seleccionado = { filtroValor: "filtro1", value: null };

    await wrapper.vm.cambioFiltro(seleccionado);

    expect(wrapper.emitted("cambioFiltro")).toBeFalsy();
  });
});

test("Restablece valoresFiltros y emitir el evento", async () => {
  const wrapper = shallowMount(FiltrosColumnasNotificaciones);
  const valoresFiltros = {
    tipoParte: "",
    tipoNotificacion: "",
    actuario: "",
  };
  wrapper.vm.valoresFiltros = { ...valoresFiltros };

  const copiaValoresFiltros = { ...valoresFiltros };
  await wrapper.vm.eliminarFiltros();

  expect(wrapper.vm.valoresFiltros).toEqual(copiaValoresFiltros);

  expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
});
