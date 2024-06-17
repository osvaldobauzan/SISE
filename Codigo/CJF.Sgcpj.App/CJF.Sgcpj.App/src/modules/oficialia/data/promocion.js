import { ref } from "vue";

export class Promocion {
  expediente = new Expediente();
  numeroOrden = ref(Number);
  yearPromocion = ref(Number);
  origen = ref(Number);
  origenPromocion = ref(Number);
  origenPromocionDescripcion = ref(String);
  numeroRegistro = ref(Number);
  fechaPresentacion = ref(Date);
  fechaPresentacionFin = ref(Date);
  mesa = ref(String);
  clasePromocion = ref(Number);
  clasePromocionDescripcion = ref(String);
  copias = ref(0);
  anexos = ref(Number);
  color = ref(String);
  cuadernoId = ref(Number);
  cuadernoNombreCorto = ref(String);
  cuadernoNombre = ref(String);
  secretarioDescripcion = ref(String);
  secretarioUserName = ref(String);
  secretarioNombres = ref(String);
  secretarioPaterno = ref(String);
  secretarioMaterno = ref(String);
  tipoContenidoDescripcion = ref(String);
  tipoContenidoId = ref(Number);
  parteDescripcion = ref(String);
  tipoPromovente = ref(Number);
  clasePromovente = ref(Number);
  clasePromoventeDescripcion = ref("");
  esDemandaElectronica = ref(Boolean);
  esDemanda = ref(Boolean);
  esPromocionE = ref(Boolean);
  cambioDemandaPromocion = ref(Boolean);
  conAcuerdo = ref(Boolean);
  estado = ref(Number);
  estadoAcuerdo = ref(Number);
  detalle = ref(new Detalle());
  fojas = 0;
  secretarioId = 0;
  kIdElectronica = ref(0);
  usuarioCaptura = ref("");
  fechaCaptura = ref("");
  catAutorizacionDocumentosId = ref(Number);
  conArchivo = ref(Boolean);
  nombreOrigen = ref("");
}

export class Expediente {
  asuntoNeunId = ref(0);
  asuntoAlias = ref(String);
  catTipoOrganismoId = ref(Number);
  catOrganismoId = ref(Number);
  catTipoAsunto = ref(String);
  catTipoAsuntoId = ref(Number);
  tipoProcedimiento = ref(String);
  nombreCorto = ref(String);
  tipoAsunto = ref(String);
  label = `${this.asuntoAlias} (${this.tipoAsunto})`;
}
export class Detalle {
  folio = ref(String);
  tipo = ref(String);
  fechaDeRegistro = ref(Date);
  usuario = ref(String);
  numeroDeArchivos = ref(String);
  firmado = ref(String);
  noRegistroOCC = ref(String);
  registroOCC = ref(Date);
}
