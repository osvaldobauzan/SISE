import { shallowMount } from "@vue/test-utils";
import UploadPromociones from "../../../../../src/modules/oficialia/components/UploadPromociones.vue";
import { expect, test, vi } from "vitest";
import { Utils } from "../../../../../src/helpers/utils.js";

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
test("UploadPromociones renderiza el componente", () => {
  const wrapper = shallowMount(UploadPromociones);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function filtrarAnio", async () => {
  const wrapper = shallowMount(UploadPromociones);
  const val = "2023";
  const tipoProcedimiento = [{ label: "2023" }, { label: "2024" }];

  await wrapper.vm.filtrarAnio(val, updateMock);

  expect(mockFiltrarCombo(val, tipoProcedimiento, "label"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});
