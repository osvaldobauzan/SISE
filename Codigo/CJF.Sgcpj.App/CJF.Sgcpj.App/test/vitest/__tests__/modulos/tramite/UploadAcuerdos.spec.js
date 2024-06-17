import { shallowMount } from "@vue/test-utils";
import { expect, test, vi } from "vitest";
import UploadAcuerdos from "../../../../../src/modules/tramite/components/UploadAcuerdos.vue";
import { Utils } from "../../../../../src/helpers/utils.js";

test("UploadAcuerdos renderiza el componente", () => {
  const wrapper = shallowMount(UploadAcuerdos);

  expect(wrapper.exists()).toBeTruthy();
});

test("Function cambioFecha", () => {
  const wrapper = shallowMount(UploadAcuerdos);

  wrapper.vm.cambioFecha("11-10-2023");
});

test("Function filtrarCuaderno", async () => {
  const mockFiltrarCombo = vi
    .spyOn(Utils, "filtrarCombo")
    .mockImplementation(() => []);
  const mockMarcaPrimeraOpcionCombo = vi
    .spyOn(Utils, "marcaPrimeraOpcionCombo")
    .mockImplementation(() => {});
  const cuadernos = [{ cuaderno: "Cuaderno1" }, { cuaderno: "Cuaderno2" }];

  const updateMock = (asyncCallback, syncCallback) => {
    asyncCallback();
    syncCallback();
  };

  const wrapper = shallowMount(UploadAcuerdos);

  const val = "cuaderno1";
  await wrapper.vm.filtrarContenido(val, updateMock);

  expect(mockFiltrarCombo(val, cuadernos, "cuaderno"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function filtrarContenido", async () => {
  const mockFiltrarCombo = vi
    .spyOn(Utils, "filtrarCombo")
    .mockImplementation(() => []);
  const mockMarcaPrimeraOpcionCombo = vi
    .spyOn(Utils, "marcaPrimeraOpcionCombo")
    .mockImplementation(() => {});
  const contenidos = [{ cuaderno: "contenido1" }, { cuaderno: "contenido2" }];

  const updateMock = (asyncCallback, syncCallback) => {
    asyncCallback();
    syncCallback();
  };

  const wrapper = shallowMount(UploadAcuerdos);
  const val = "contenido1";

  await wrapper.vm.filtrarCuaderno(val, updateMock);

  expect(mockFiltrarCombo(val, contenidos, "contenido"));

  expect(mockMarcaPrimeraOpcionCombo).toHaveBeenCalled();
});

test("Function updateFiles", async () => {
  const blobMock = new Blob(["test content"], { type: "text/plain" });
  const mockFileToBlob = vi
    .spyOn(Utils, "fileToBlob")
    .mockResolvedValue(blobMock);

  const wrapper = shallowMount(UploadAcuerdos);

  const newFile = new File(["test"], "test.txt", { type: "text/plain" });

  await wrapper.vm.updateFiles(newFile);

  expect(mockFileToBlob).toHaveBeenCalledWith(newFile);
  expect(wrapper.vm.file).toBeInstanceOf(Blob);
  expect(wrapper.vm.cambioArchivo).toBe(true);
});

test("cambioExpediente actualiza el estado y llama funciones correctamente", async () => {
  // Monta el componente
  const wrapper = shallowMount(UploadAcuerdos);

  // Llama a cambioExpediente
  const val = {
    asuntoNeunId: "123",
    asuntoAlias: "Alias",
    catTipoAsuntoId: "456",
  };
  await wrapper.vm.cambioExpediente(val);

  // Verifica el reinicio de estado
  expect(wrapper.vm.cuaderno).toBeNull();
  expect(wrapper.vm.contenido).toBeNull();
  expect(wrapper.vm.promociones).toBeNull();
  await expect(
    wrapper.vm.obtenCatalogosDependientes(
      val.asuntoNeunId,
      val.asuntoAlias,
      val.catTipoAsuntoId,
    ),
  ).toBeTruthy();
});
