import { shallowMount } from "@vue/test-utils";
import { describe, expect, test, vi } from "vitest";
import FiltrosColumnas from "../../../../../src/modules/oficialia/components/FiltrosColumnas.vue";

test("FiltrosColumnas renderiza el componente", async () => {
  const wrapper = shallowMount(FiltrosColumnas);
  expect(wrapper.exists()).toBeTruthy();
});

test("Consumo de API obtenerCatalogosFiltros", async () => {
  vi.mock(
    "../../../../../src/modules/oficialia/stores/oficialia-store",
    async () => {
      return {
        useOficialiaStore: vi.fn(() => ({
          obtenerCatalogosFiltros: vi.fn(async () => ({
            origen: [
              { sNombreOrigenPromocion: "Origen 1" },
              { sNombreOrigenPromocion: "Origen 2" },
            ],
            secretario: [
              { secretario: "Secretario 1", empleadoId: 1, mesa: "Mesa 1" },
              { secretario: "Secretario 2", empleadoId: 2, mesa: "Mesa 2" },
            ],
            capturo: [
              { capturo: "Capturo 1", empleadoId: 1, userName: "Usuario 1" },
              { capturo: "Capturo 2", empleadoId: 2, userName: "Usuario 2" },
            ],
          })),
        })),
      };
    },
  );

  const wrapper = shallowMount(FiltrosColumnas);
  await wrapper.vm.$nextTick();
});

describe("Function cambioFiltro", () => {
  test("cambioFiltro actualiza valoresFiltros y emite el evento", async () => {
    const wrapper = shallowMount(FiltrosColumnas);
    const seleccionado = { filtroValor: "secretario", value: "981" };

    await wrapper.vm.cambioFiltro(seleccionado);

    expect(wrapper.vm.valoresFiltros.secretario).toBe("981");
    expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
  });

  test("cambioFiltro no emite el evento si value es null", async () => {
    const wrapper = shallowMount(FiltrosColumnas);
    const seleccionado = { filtroValor: "secretario", value: null };

    await wrapper.vm.cambioFiltro(seleccionado);

    expect(wrapper.emitted("cambioFiltro")).toBeFalsy();
  });
});

test("Restablece valoresFiltros y emitir el evento", async () => {
  const wrapper = shallowMount(FiltrosColumnas);
  const valoresFiltros = {
    capturo: "",
    origen: "",
    secretario: "",
  };
  wrapper.vm.valoresFiltros = { ...valoresFiltros };

  const copiaValoresFiltros = { ...valoresFiltros };
  await wrapper.vm.eliminarFiltros();

  expect(wrapper.vm.valoresFiltros).toEqual(copiaValoresFiltros);

  expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
});
