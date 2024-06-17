import { shallowMount } from "@vue/test-utils";
import { describe, expect, test } from "vitest";
import FiltrosColumnas from "../../../../../src/modules/actuaria/components/FiltrosColumnas.vue";

test("FiltrosColumnas renderiza el componente", async () => {
  const wrapper = shallowMount(FiltrosColumnas);
  expect(wrapper.exists()).toBeTruthy();
});

describe("Function cambioFiltro", () => {
  test("cambioFiltro actualiza valoresFiltros y emite el evento", async () => {
    const wrapper = shallowMount(FiltrosColumnas);
    const seleccionado = { filtroValor: "filtro1", value: "valor1" };

    await wrapper.vm.cambioFiltro(seleccionado);

    expect(wrapper.vm.valoresFiltros.filtro1).toBe("valor1");
    expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
  });

  test("cambioFiltro no emite el evento si value es null", async () => {
    const wrapper = shallowMount(FiltrosColumnas);
    const seleccionado = { filtroValor: "filtro1", value: null };

    await wrapper.vm.cambioFiltro(seleccionado);

    expect(wrapper.emitted("cambioFiltro")).toBeFalsy();
  });
});

test("Restablece valoresFiltros y emitir el evento", async () => {
  const wrapper = shallowMount(FiltrosColumnas);
  const valoresFiltros = {
    estado: "",
    contenido: "",
  };
  wrapper.vm.valoresFiltros = { ...valoresFiltros };

  const copiaValoresFiltros = { ...valoresFiltros };
  await wrapper.vm.eliminarFiltros();

  expect(wrapper.vm.valoresFiltros).toEqual(copiaValoresFiltros);

  expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
});
