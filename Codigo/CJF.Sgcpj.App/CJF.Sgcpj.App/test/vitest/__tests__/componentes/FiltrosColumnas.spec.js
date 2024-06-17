import { shallowMount } from "@vue/test-utils";
import { describe, it, expect } from "vitest";
import FiltrosColumnas from "../../../../src/components/FiltrosColumnas.vue";

describe("QSelect component tests", () => {
  const label = "Test Label";
  const opciones = [
    { label: "Option 1", value: "opt1" },
    { label: "Option 2", value: "opt2" },
  ];
  const filtroValor = "filterValue";
  const valoresFiltros = { filterValue: "opt1" };
  const labelDefault = "Ninguno";
  const valorDefault = "";

  const factory = (propsData = {}) => {
    return shallowMount(FiltrosColumnas, {
      propsData: {
        label,
        opciones,
        filtroValor,
        valoresFiltros,
        labelDefault,
        valorDefault,
        ...propsData,
      },
      global: {
        mocks: {
          $t: (msg) => msg,
        },
      },
    });
  };

  it("renders default option and provided options", () => {
    const wrapper = factory();
    expect(wrapper.vm.opcionesConValDefault).toEqual([
      { label: labelDefault, value: valorDefault },
      ...opciones,
    ]);
  });

  it("computes selected filter correctly", () => {
    const wrapper = factory();
    expect(wrapper.vm.filtroSeleccionado).toEqual(opciones[0]);
  });

  it("defaults to null when selected filter does not exist", () => {
    const wrapper = factory({ valoresFiltros: { filterValue: "nonexistent" } });
    expect(wrapper.vm.filtroSeleccionado).toBeNull();
  });

  it("emits cambioFiltro event when an option is selected", async () => {
    const wrapper = factory();
    wrapper.vm.opcionSeleccionada(opciones[1]);
    await wrapper.vm.$nextTick();
    expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
    expect(wrapper.emitted().cambioFiltro[0]).toEqual([
      { value: "opt2", filtroValor },
    ]);
  });
});
