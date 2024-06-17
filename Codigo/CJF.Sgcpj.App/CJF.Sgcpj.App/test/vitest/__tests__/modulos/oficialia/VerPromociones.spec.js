import { shallowMount } from "@vue/test-utils";
import VerPromociones from "../../../../../src/modules/oficialia/components/VerPromociones.vue";
import { expect, test } from "vitest";

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

test("VerPromociones renderiza el componente", async () => {
  const wrapper = shallowMount(VerPromociones, {
    props: { promocion },
  });
  expect(wrapper.exists()).toBeTruthy();
});
test("Function mostrarPdf", async () => {
  const wrapper = shallowMount(VerPromociones);

  wrapper.vm.mostrarPdf("archivo.docx");
});

test("Function mostrarPdf", async () => {
  const wrapper = shallowMount(VerPromociones);

  wrapper.vm.mostrarPdf("archivo.pdf");
});
