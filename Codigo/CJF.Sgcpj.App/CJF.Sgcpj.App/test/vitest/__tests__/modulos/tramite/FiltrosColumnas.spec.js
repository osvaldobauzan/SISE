import { shallowMount } from "@vue/test-utils";
import { describe, expect, test } from "vitest";
import FiltrosColumnas from "../../../../../src/modules/tramite/components/FiltrosColumnas.vue";

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
  const valoresFiltros = {
    asunto: "",
    autorizo: "",
    cancelo: "",
    capturo: "",
    origen: "",
    preautorizo: "",
    secretario: "",
  };
  const wrapper = shallowMount(FiltrosColumnas, {
    propsData: { modelValue: valoresFiltros },
  });

  wrapper.vm.valoresFiltros = { ...valoresFiltros };

  const copiaValoresFiltros = { ...valoresFiltros };
  await wrapper.vm.eliminarFiltros();

  expect(wrapper.vm.valoresFiltros).toEqual(copiaValoresFiltros);

  expect(wrapper.emitted()).toHaveProperty("cambioFiltro");
});

describe("cargaCatalogosFiltros", () => {
  test("should load catalog filters correctly", async () => {
    const datosFiltros = {
      secretario: [
        { secretario: "Secretario 1", empleadoId: 1, mesa: "Mesa 1" },
        { secretario: "Secretario 2", empleadoId: 2, mesa: "Mesa 2" },
      ],
      origen: [
        { sNombreOrigenPromocion: "Origen 1", empleadoId: 1 },
        { sNombreOrigenPromocion: "Origen 2", empleadoId: 2 },
      ],
      tipoAsunto: [
        { tipoAsunto: "Tipo 1", catTipoAsuntoId: 1 },
        { tipoAsunto: "Tipo 2", catTipoAsuntoId: 2 },
      ],
      capturo: [
        { capturo: "Capturo 1", empleadoId: 1, userName: "Usuario 1" },
        { capturo: "Capturo 2", empleadoId: 2, userName: "Usuario 2" },
      ],
      autorizo: [
        {
          autorizo: "Autorizo 1",
          empleadoId: 1,
          userName: "Usuario 1",
        },
        {
          autorizo: "Autorizo 2",
          empleadoId: 2,
          userName: "Usuario 2",
        },
      ],
      preautorizo: [
        {
          autorizo: "Preautorizo 1",
          empleadoId: 1,
          userName: "Usuario 1",
        },
        {
          autorizo: "Preautorizo 2",
          empleadoId: 2,
          userName: "Usuario 2",
        },
      ],
      cancelo: [
        { autorizo: "Cancelo 1", empleadoId: 1, userName: "Usuario 1" },
        { autorizo: "Cancelo 2", empleadoId: 2, userName: "Usuario 2" },
      ],
    };

    const wrapper = shallowMount(FiltrosColumnas);

    await wrapper.vm.$nextTick();
    wrapper.vm.cargaCatalogosFiltros(datosFiltros);
  });
});
