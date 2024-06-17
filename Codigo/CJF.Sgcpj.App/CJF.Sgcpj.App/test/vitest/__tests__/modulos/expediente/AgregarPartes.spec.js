import { shallowMount } from "@vue/test-utils";
import { expect, test } from "vitest";
import AgregarPartes from "../../../../../src/modules/expediente/components/AgregarPartes.vue";
import { useExpedienteElectronicoStore } from "src/modules/expediente/stores/expediente-electronico-store";

const expedienteElectronicoStore = useExpedienteElectronicoStore();

test("AgregarPartes renderiza el componente", async () => {
  const wrapper = shallowMount(AgregarPartes, {
    props: {
      esEditar: true,
      parte: { personaId: 92 },
    },
  });
  expect(wrapper.exists()).toBeTruthy();
});

test("Function submitForm", async () => {
  const wrapper = shallowMount(AgregarPartes, {
    props: {
      esEditar: false,
      parte: { personaId: 92 },
      expediente: { asuntoNeunId: 123 },
    },
  });

  await wrapper.vm.submitForm(false);
});

test("Function agregarParte", async () => {
  const wrapper = shallowMount(AgregarPartes);

  const parametros = {
    caracterPersonaId: 13,
    catTipoPersonaId: 1,
    catCaracterPersonaAsuntoId: 213783,
  };

  await wrapper.vm.agregarParte(parametros, true);

  expect(expedienteElectronicoStore.agregarParte).toHaveBeenCalledWith(
    parametros,
  );
});

test("Function editarParte", async () => {
  const wrapper = shallowMount(AgregarPartes);

  const expedienteElectronicoStore = useExpedienteElectronicoStore();
  const parametros = {
    caracterPersonaId: 21,
    catTipoPersonaId: 1,
    catCaracterPersonaAsuntoId: 213783,
  };

  await wrapper.vm.editarParte(parametros, true);

  expect(expedienteElectronicoStore.editarParte).toHaveBeenCalledWith(
    parametros,
  );
});
