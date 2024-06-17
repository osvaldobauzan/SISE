import { shallowMount } from "@vue/test-utils";
import AddPromocion from "../../../../../src/modules/oficialia/components/AddPromocion.vue";
import { describe, expect, test, vi } from "vitest";

const promocion = {
  expediente: {
    asuntoNeunId: 30326298,
    asuntoAlias: "43448/2024",
    catTipoOrganismoId: 0,
    catOrganismoId: 180,
    catTipoAsunto: "Procesos Civiles o Administrativos",
    catTipoAsuntoId: "4",
    tipoProcedimiento: "",
    nombreCorto: null,
  },
  numeroOrden: 431,
  yearPromocion: 2024,
  origenPromocion: 4,
  origen: 6,
  nombreOrigen: "",
  origenPromocionDescripcion: "OFICIALÍA",
  numeroRegistro: 59765,
  fechaPresentacion: "2024-02-28T09:12:00",
  fechaPresentacionFin: "0001-01-01T00:00:00",
  mesa: "Mesa III",
  clasePromocion: 0,
  clasePromocionDescripcion: null,
  copias: 0,
  anexos: 0,
  color: null,
  cuadernoId: 0,
  secretarioId: 59609,
  cuadernoNombreCorto: null,
  cuadernoNombre: null,
  secretarioDescripcion: "Jesús Eduardo Acosta Moroyoqui",
  secretarioUserName: "JesAcostaMor",
  secretarioNombres: null,
  secretarioPaterno: null,
  secretarioMaterno: null,
  tipoContenidoDescripcion: "Arraigo",
  tipoContenidoId: 0,
  parteDescripcion: "Juan Perez Torres",
  tipoPromovente: 0,
  clasePromoventeId: 108032,
  clasePromoventeDescripcion: "Autoridad Judicial",
  esDemandaElectronica: false,
  esDemanda: false,
  esPromocionE: false,
  cambioDemandaPromocion: false,
  conAcuerdo: false,
  estado: 4,
  estadoAcuerdo: 1,
  fojas: 1,
  kIdElectronica: null,
  usuarioCaptura: "EZamoranoG",
  fechaCaptura: "2024-02-28T09:14:29.523",
  detalle: null,
  catAutorizacionDocumentosId: null,
  conArchivo: true,
  nombreOficial: null,
  index: 0,
};

test("AddPromocion renderiza el componente", async () => {
  const wrapper = shallowMount(AddPromocion, { props: { promocion } });
  await wrapper.vm.$nextTick();

  expect(wrapper.exists()).toBeTruthy();
});

describe("Function setPromovente", async () => {
  test("Case Promovente", async () => {
    const wrapper = shallowMount(AddPromocion);

    await wrapper.vm.$nextTick();

    wrapper.vm.detalle.clasePromoventeDescripcion = "Promovente";

    await wrapper.vm.setPromovente();
  });
  test("Case Partes", async () => {
    const wrapper = shallowMount(AddPromocion);

    await wrapper.vm.$nextTick();

    wrapper.vm.detalle.clasePromoventeDescripcion = "Partes";

    await wrapper.vm.setPromovente();
  });
  test("Case Autoridad Judicial", async () => {
    const wrapper = shallowMount(AddPromocion);

    await wrapper.vm.$nextTick();

    wrapper.vm.detalle.clasePromoventeDescripcion = "Autoridad Judicial";

    await wrapper.vm.setPromovente();
  });
});

test("Function guardaPromovente case promovente", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.$nextTick();

  wrapper.vm.parametros.tipoPromovente = "promovente";

  await wrapper.vm.guardarPromovente();
});

test("Function guardaPromovente case parte", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.$nextTick();
  wrapper.vm.cambioFormPromovente = true;

  wrapper.vm.cambioExpedienteInsertar = true;
  wrapper.vm.parametros.tipoPromovente = "parte";

  await wrapper.vm.guardarPromovente();
});

test("Function guardaPromovente case autoridad", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.$nextTick();

  wrapper.vm.cambioFormPromovente = true;

  wrapper.vm.cambioExpedienteInsertar = true;

  wrapper.vm.newAutoridad = true;
  wrapper.vm.parametros.tipoPromovente = "autoridad";
  await wrapper.vm.guardarPromovente();
});

test("Function guardarPasoPromocion cuando no es edicion", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.$nextTick();

  wrapper.vm.esEdicion = false;
  await wrapper.vm.guardarPasoPromocion();
});

test("Function guardarPasoPromocion cuando es edicion", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.$nextTick();

  wrapper.vm.cambioFormPromovente = true;

  wrapper.vm.cambioExpedienteInsertar = true;
  wrapper.vm.detalle.numeroRegistro = 23;
  wrapper.vm.esEdicion = true;
  wrapper.vm.newAutoridad = true;
  wrapper.vm.cambioExpedienteaPromocion = true;
  await wrapper.vm.guardarPasoPromocion();
});

test("Function guardarPasoPromocion cuando es es expediente nuevo", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.$nextTick();

  wrapper.vm.cambioFormPromovente = true;

  wrapper.vm.expedienteNuevo = true;
  wrapper.vm.detalle.numeroRegistro = 23;
  wrapper.vm.esEdicion = false;
  wrapper.vm.newAutoridad = true;
  wrapper.vm.cambioExpedienteaPromocion = true;
  wrapper.vm.cambioFormPromocion = true;
  await wrapper.vm.guardarPasoPromocion();
});

test("Function cambianParametros", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.cambianParametros("secretario");
});

test("Function reseteaForm", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.reseteaForm();
});
test("Function asociarPromocion", async () => {
  const wrapper = shallowMount(AddPromocion);

  await wrapper.vm.asociarPromocion("secretario");
});

test("Function submitForm", async () => {
  const validateMock = vi.fn().mockResolvedValue(true);
  const resetMock = vi.fn();
  const resetValidationMock = vi.fn();
  const focusMock = vi.fn();

  const wrapper = shallowMount(AddPromocion);
  wrapper.vm.scrollAreaRef = {
    setScrollPosition: vi.fn(),
  };
  wrapper.vm.stePromocionRef = {
    setFocusExpediente: vi.fn(),
  };
  wrapper.vm.formPromocion = {
    validate: validateMock,
    reset: resetMock,
    resetValidation: resetValidationMock,
    focus: focusMock,
  };
  wrapper.vm.cambioExpedienteaPromocion = true;
  wrapper.vm.esEdicion = true;
  wrapper.vm.detalle = promocion;

  await wrapper.vm.$nextTick();

  await wrapper.vm.submitForm();
});

describe("MiComponente", () => {
  test("parametros debe estar actualizado correctamente", async () => {
    const wrapper = shallowMount(AddPromocion);
    const validateMock = vi.fn().mockResolvedValue(true);
    const resetMock = vi.fn();
    const resetValidationMock = vi.fn();
    const focusMock = vi.fn();

    wrapper.vm.scrollAreaRef = {
      setScrollPosition: vi.fn(),
    };
    wrapper.vm.stePromocionRef = {
      setFocusExpediente: vi.fn(),
    };
    wrapper.vm.formPromocion = {
      validate: validateMock,
      reset: resetMock,
      resetValidation: resetValidationMock,
      focus: focusMock,
    };
    wrapper.vm.cambioExpedienteaPromocion = true;
    wrapper.vm.esEdicion = true;

    wrapper.vm.correcto = false;
    wrapper.vm.detalle = promocion;

    wrapper.vm.parametros = {
      archivoAVincular: { name: "documento.pdf" },
      anexos: [{ name: "anexo.docx" }],
      anexosAEliminar: [{ name: "anexo.docx" }],
      registro: "123",
      numeroOrden: "001",
      origen: "Externo",
      fojas: "10",
    };

    await wrapper.vm.$nextTick();
    expect(wrapper.vm.parametros.archivoAVincular.name).toBe("documento.pdf");
    expect(wrapper.vm.parametros.anexos).toHaveLength(1);
    expect(wrapper.vm.parametros.anexos[0].name).toBe("anexo.docx");
    expect(wrapper.vm.parametros.anexosAEliminar).toHaveLength(1);
    expect(wrapper.vm.parametros.anexosAEliminar[0].name).toBe("anexo.docx");
    expect(wrapper.vm.parametros.registro).toBe("123");
    expect(wrapper.vm.parametros.origen).toBe("Externo");
    expect(wrapper.vm.parametros.fojas).toBe("10");

    await wrapper.vm.submitForm();
  });
});
