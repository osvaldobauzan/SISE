import { ref } from "vue";

export class DetallePromocion {
  constructor() {
    this.anexos = [];
  }
  no = ref("");
  asuntoNeunId = ref(0);
  expediente = ref("");
  catTipoAsunto = ref("");
  catTipoAsuntoId = ref("");
  tipoProcedimientoId = ref(0);
  tipoProcedimiento = ref("");
  cuaderno = ref("");
  cuadernoId = ref(0);
  numeroRegistro = ref("");
  origenPromocion = ref("");
  secretarioNombre = ref("");
  secretarioId = ref(0);
  userName = ref("");
  mesa = ref("");
  fechaPresentacion = ref("");
  horaPresentacion = ref("");
  tipoPromociones = ref("");
  contenido = ref("");
  contenidoId = ref("");
  procedimientoId = ref(0);
  promovente = ref("");
  idPromovente = ref("");
  clasePromoventeDescripcion = ref("");
  numeroCopias = ref(0);
  fojas = ref(0);
  copias = ref(0);
  numeroAnexos = ref(0);
  registrada = ref("");
  conArchivo = ref(0);
  esDemanda = ref(0);
  parteAsociadaId = ref(0);
  origenPromocionId = 5;
  folio = ref(0);
  esPromocionE = ref(0);
  catAutorizacionDocumentosId = ref(null);
  nombreArchivo = ref(null);
  origen = ref(0);
  numeroOrden = ref(0);
  tipoPromovente = ref(null);
  parteAsociadaPromovente = ref(null);
  caracterParteAsociadaPromovente = ref(null);
  tipoPersonaParteAsociadaPromovente = ref(null);
  capturo = ref("");
  caracterParte = ref("");
  fechaCaptura = ref("");
  nombreArchivoPromocion = ref(null);
  anexos = ref(new Array(new DetalleAnexos()));
  occ = ref("");
  boletaOCC = ref("");
  tipo = ref("");

  tipoPromoventeId = ref(0);
  promoventeNombre = ref("");
  promoventeApellidoPaterno = ref("");
  promoventeApellidoMaterno = ref("");

  tipoParteAsociadaPromoventeId = ref(0);
  caracterParteAsociadaId = ref(0);
  parteAsociadaNombre = ref("");
  parteAsociadaApellidoPaterno = ref("");
  parteAsociadaApellidoMaterno = ref("");

  caracterParteId = ref(0);
  tipoPersonasParteId = ref(0);
  parteNombre = ref("");
  parteApellidoPaterno = ref("");
  parteApellidoMaterno = ref("");

  autoridadJudicialId = ref(0);

  catOrganismoId = ref(0);
  catOrganismo = ref("");
}
export class DetalleAnexos {
  tipoAnexo = ref("");
  descripcionAnexo = ref("");
  caracterAnexo = ref("");
  nombre = ref("");
  consecutivo = ref(1);
}
