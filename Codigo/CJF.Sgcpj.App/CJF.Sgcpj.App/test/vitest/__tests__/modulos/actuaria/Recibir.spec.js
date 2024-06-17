import { test, expect } from "vitest";
import { shallowMount } from "@vue/test-utils";
import RecibirOficios from "../../../../../src/modules/actuaria/components/RecibirOficios.vue";
import { useActuariaOficiosStore } from "../../../../../src/modules/actuaria/stores/actuaria-oficios-store";

const actuariaOficiosStore = useActuariaOficiosStore();
actuariaOficiosStore.oficios = [
  { id: 1, nombre: "Oficio 1" },
  { id: 2, nombre: "Oficio 2" },
  { id: 3, nombre: "Oficio 3" },
];

test("RecibirOficios renderiza el componente", () => {
  const wrapper = shallowMount(RecibirOficios);
  expect(wrapper.exists()).toBeTruthy();
});

test("delOficioTable elimina el oficio correcto", async () => {
  const wrapper = shallowMount(RecibirOficios, {
    global: {
      provide: {
        actuariaOficiosStore: actuariaOficiosStore,
      },
    },
  });

  await wrapper.vm.delOficioTable({ id: 2, nombre: "Oficio 2" });
  expect(actuariaOficiosStore.oficios.length).toBe(2);
});
