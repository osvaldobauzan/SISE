import { CatalogoAsunto } from "src/data/catalogo-asunto";
import { Contenido } from "src/data/contenido";
import { Cuaderno } from "src/data/cuaderno";
import { Procedimiento } from "src/data/procedimiento";
import { CatalogoPromovente } from "src/data/catalogo-promovente";
import { CatalogoTipoPersonaCaracter } from "src/data/catalogo-tipo-persona-caracter";
import { ref } from "vue";
import { CatalogoTipoPersona } from "src/data/catalogo-tipo-persona";
import { AutoridadJudicial } from "src/data/autoridad-judicial";
import { ParteExistente } from "src/data/parte-existente";
import { ExpedienteEncontrado } from "src/data/expediente-encontrado";
import { Secretario } from "src/data/secretario";
import { FormAnexos } from "./form-anexos";
import { PromoventeExistente } from "src/data/promovente-existente";

export class FormPromocion {
  constructor() {
    this.tipoAsunto = null;
    this.cuaderno = null;
    this.contenido = null;
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
    this.anexos = [];
    this.partes = [];
    this.autoridades = [];
    this.otros = [];
  }
  numeroExpediente = ref("");
  tipoAsunto = ref(new CatalogoAsunto());
  numeroOCC = ref("");
  tipoProcedimiento = ref(new Procedimiento());
  cuaderno = ref(new Cuaderno());
  contenido = ref(new Contenido());
  registro = ref("");
  fechaPresentacion = ref("");
  horaPresentacion = ref("");
  copias = ref(0);
  fojas = ref(0);
  secretarioId = ref(0);
  secretario = ref(new Secretario());
  origen = ref(4);
  origenDescripcion = ref("OFICIALÍA");
  asuntoNeunId = ref(0);
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
  promoventeNombre = ref("");
  promoventeApellidoPaterno = ref("");
  promoventeApellidoMaterno = ref("");
  // pAsociadaCaracterNombre = ref('');
  // pAsociadaApellidoPaterno = ref('');
  // pAsociadaApellidoMaterno = ref('');
  anexosAEliminar = ref([]);
  parteAutoridadDenominacion = ref("");
  archivoAVincular = ref(null);
  numeroOrden = ref(0);
  yearPromocion = ref(1900);
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
