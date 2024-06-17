import { shallowMount } from "@vue/test-utils";
import { expect, test } from "vitest";
import ActuariaPartes from "../../../../../src/modules/actuaria/components/ActuariaPartes.vue";

test("ActuariaPartes renderiza el componente", () => {
  const parte = { asignadoActuario: true };

  const wrapper = shallowMount(ActuariaPartes, {
    props: { parte },
  });
  expect(wrapper.exists()).toBeTruthy();
});

test("Function cambioFiltro", async () => {
  const wrapper = shallowMount(ActuariaPartes);
  const seleccionado = {
    actuario: "1284",
  };
  await wrapper.vm.cambioFiltro(seleccionado);

  for (const key in seleccionado) {
    expect(wrapper.vm.valoresFiltros[key]).toBe(seleccionado[key]);
  }
});

test("Function onRequest", () => {
  const propsSimulados = {
    pagination: { page: 1, rowsPerPage: 10 },
  };
  const wrapper = shallowMount(ActuariaPartes, {
    props: { propsSimulados },
  });

  wrapper.vm.onRequest(propsSimulados);
});

test("Function setFilter", () => {
  const wrapper = shallowMount(ActuariaPartes);

  wrapper.vm.updateFilterStatus("pendiente");
  expect(wrapper.vm.pagination.page).toBe(1);
});
