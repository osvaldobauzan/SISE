import { CatalogoAsunto } from "../../../data/catalogo-asunto";
import { Contenido } from "../../../data/contenido";
import { Cuaderno } from "../../../data/cuaderno";
import { AmparoEnRevision } from "../../../data/amparo-revision";
import { Procedimiento } from "../../../data/procedimiento";
import { CatalogoPromovente } from "../../../data/catalogo-promovente";
import { CatalogoTipoPersonaCaracter } from "../../../data/catalogo-tipo-persona-caracter";
import { ref } from "vue";
import { CatalogoTipoPersona } from "../../../data/catalogo-tipo-persona";
import { AutoridadJudicial } from "../../../data/autoridad-judicial";
import { ParteExistente } from "../../../data/parte-existente";
import { ParteExistenteGeneral } from "../../../data/parte-existente-general";
import { ExpedienteEncontrado } from "../../../data/expediente-encontrado";
import { Secretario } from "../../../data/secretario";
import { FormAnexos } from "./form-anexos";
import { FormPromoventes } from "./form-promoventes";
import { PromoventeExistente } from "../../../data/promovente-existente";

export class FormPromocion {
  constructor() {
    this.tipoAsunto = null;
    this.cuaderno = null;
    this.contenido = null;
    this.amparoEnRevision = null;
    this.tipoProcedimiento = null;
    this.tipoPromoventeCat = null;
    this.pAsignadaCaracter = null;
    this.parteCatTipoPersona = null;
    this.parteCatTipoPersonaCaracter = null;
    this.promoventeAutoridad = null;
    this.expedienteEncontrado = null;
    this.secretario = null;
    this.promoventeAutoridadExistente = null;
    this.promoventeExistente = null;
    this.parteExistenteGeneral = null;
    this.anexos = [];
    this.partePromoventeAutoridadOptions = [];
    this.tipoDePromovente = null;
    this.isSaved = false;
  }
  clasePromovente = ref(0);
  tipoDePromovente = ref(0);
  isSaved = ref(false);
  selected = ref([]);
  numeroExpediente = ref("");
  tipoAsunto = ref(new CatalogoAsunto());
  numeroOCC = ref("");
  tipoProcedimiento = ref(new Procedimiento());
  cuaderno = ref(new Cuaderno());
  contenido = ref(new Contenido());
  amparoEnRevision = ref(new AmparoEnRevision());
  registro = ref("");
  fechaPresentacion = ref("");
  horaPresentacion = ref("");
  copias = ref(null);
  fojas = ref(null);
  secretarioId = ref(0);
  secretario = ref(new Secretario());
  origen = ref(4);
  origenDescripcion = ref("OFICIALÍA");
  asuntoNeunId = ref(0);
  AsuntoNeunIdNuevo = ref(0);
  tipoPromovente = ref("promovente");
  tipoPromoventeCat = ref(new CatalogoPromovente());
  expedienteEncontrado = ref(new ExpedienteEncontrado());
  parteCatTipoPersona = ref(new CatalogoTipoPersona());
  parteCatTipoPersonaCaracter = ref(new CatalogoTipoPersonaCaracter());
  parteNombre = ref("");
  parteApellidoPaterno = ref("");
  parteApellidoMaterno = ref("");
  anexos = ref(new Array(new FormAnexos()));
  numeroAnexos = this.anexos.value.length;
  tipoParte = ref("parteNueva");
  pAsignadaCaracter = ref(new CatalogoTipoPersonaCaracter());
  esPromoventeExistente = ref(false);
  promoventeExistente = ref(new PromoventeExistente());
  promoventeAutoridad = ref(new AutoridadJudicial());
  promoventeAutoridadExistente = ref(new ParteExistente());
  parteExistenteGeneral = ref(new ParteExistenteGeneral());
  promoventeNombre = ref("");
  promoventeApellidoPaterno = ref("");
  promoventeApellidoMaterno = ref("");
  anexosAEliminar = ref([]);
  partePromoventeAutoridadOptions = ref(new Array(new FormPromoventes()));
  parteAutoridadDenominacion = ref("");
  archivoAVincular = ref(null);
  numeroOrden = ref(0);
  yearPromocion = ref(1900);
  filtroTipoPromovente = ref({ label: "Todas las partes", status: 0 });
  filterText = ref("");
  getNombrePromoventeCompleto = function () {
    return (
      this.promoventeNombre +
      " " +
      this.promoventeApellidoPaterno +
      " " +
      this.promoventeApellidoMaterno
    );
  };

  getNombreParteNombreCompleto = function () {
    return (
      this.parteNombre +
      " " +
      this.parteApellidoPaterno +
      " " +
      this.parteApellidoMaterno
    );
  };
}
