<template>
  <div class="row content-stretch">
    <div
      class="col-6"
      v-if="
        detalle?.nombreArchivo !== null ||
        detalle?.numeroAnexos > 0 ||
        detalle?.conArchivo == 1
      "
    >
      <VerPromociones
        ref="child"
        promocionDocumento
        :promocion="
          modelValue.kIdElectronica || modelValue.esPromocionE
            ? modelValue
            : detalle
        "
        :esDetalle="true"
      />
    </div>
    <div
      style="background: white"
      :class="
        detalle?.nombreArchivo !== null ||
        detalle?.numeroAnexos > 0 ||
        detalle?.conArchivo == 1
          ? 'col-6'
          : 'col-12'
      "
    >
      <q-card flat style="min-height: 80vh; border-radius: unset !important">
        <q-toolbar>
          <q-toolbar-title>
            <q-item>
              <q-item-section>
                <q-item-label class="text-bold text-secondary"
                  ><q-icon :name="'insert_drive_file'" color="secondary" />
                  Promoción
                  {{ detalle.numeroRegistro }}</q-item-label
                >
              </q-item-section>
              <div v-if="props.modelValue.firmado == 1">
                <q-item-label>
                  La promoción ya se ha firmado
                </q-item-label>
              </div>
              <div v-else>
                <q-item-section>
                  <q-btn color="primary" label="Firmar Promoción" width="2px" @click="firmar()"/>
                </q-item-section>
              </div>
            </q-item>
          </q-toolbar-title>
          <q-btn flat round dense icon="mdi-close" v-close-popup />
        </q-toolbar>
        <q-separator />
        <q-scroll-area style="width: 100%; height: 80vh">
          <q-list class="q-pt-sm">
            <template v-if="electronica">
              <q-item-label class="text-bold pad-left"
                >Datos de registro</q-item-label
              >
              <q-item-label class="pad-left"
                >Esta promoción ingresó por medio electrónico, a continuación se
                muestra la información capturada.
              </q-item-label>
              <div class="row wrap q-mb-md">
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Fecha</q-item-label>
                    <q-item-label>{{
                      formateaFecha(detalle?.fechaAlta)
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Hora</q-item-label>
                    <q-item-label>
                      {{
                        date.formatDate(detalle?.fechaAlta, "HH:mm") ||
                        detalle?.horaPresentacion
                      }}</q-item-label
                    >
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Registró la promoción</q-item-label
                    >
                    <q-item-label>{{
                      detalle?.promoventeRegistro ||
                      getNombrePromoventeCompleto()
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4" v-if="detalle?.boletaOCC">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Boleta OCC</q-item-label>
                    <q-btn
                      flat
                      noCaps
                      color="light-blue"
                      :label="detalle?.boletaOCC"
                      align="left"
                      class="q-pa-none tex-grey"
                      @click="verBoletaOCC"
                    >
                      <q-tooltip>Ver Boleta OCC </q-tooltip>
                    </q-btn>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">OCC</q-item-label>
                    <q-item-label>{{ detalle?.occ }}</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Folio de registro</q-item-label
                    >
                    <q-item-label>{{ detalle?.folio }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Origen</q-item-label>
                    <q-item-label
                      ><span style="text-transform: capitalize">{{
                        detalle?.origenPromocion?.toString()
                      }}</span>
                    </q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Archivos</q-item-label>
                    <q-item-label>{{
                      detalle?.archivos
                        ? JSON.parse(detalle?.archivos)?.length
                        : ""
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
            </template>
            <template
              v-if="
                !esDemandaElectronica ||
                (esDemandaElectronica && modelValue.estado !== 1)
              "
            >
              <q-item-label class="text-subtitle1 text-bold pad-left"
                >Datos del expediente</q-item-label
              >
              <div class="row wrap q-mb-md">
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Expediente</q-item-label>
                    <q-item-label>{{ detalle?.expediente }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo de asunto</q-item-label
                    >
                    <q-item-label>{{ detalle?.catTipoAsunto }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Cuaderno</q-item-label>
                    <q-item-label>{{ detalle?.cuaderno }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item v-if="detalle?.catTipoAsuntoId === 18" class="col-12">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo de Procedimiento</q-item-label
                    >
                    <q-item-label :lines="2">{{
                      detalle?.tipoProcedimiento
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
              <q-item-label class="text-subtitle1 text-bold pad-left"
                >Información de la promoción</q-item-label
              >
              <div class="row wrap q-mb-md">
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Contenido</q-item-label>
                    <q-item-label>{{ detalle?.contenido }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-2">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Copias</q-item-label>
                    <q-item-label>{{ detalle?.numeroCopias }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-2">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Fojas</q-item-label>
                    <q-item-label>{{ detalle?.fojas || 0 }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-2">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Anexos</q-item-label>
                    <q-item-label>{{ detalle?.numeroAnexos }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>

              <q-item-label
                v-if="detalle.numeroAnexos > 0"
                class="text-subtitle1 text-bold pad-left"
                >Anexos</q-item-label
              >
              <div class="q-mt-lg" v-if="detalle.numeroAnexos === 0"></div>
              <div
                v-for="(anexo, index) in detalle?.anexos"
                v-bind:key="anexo"
                class="row wrap q-mb-md"
              >
                <div class="row col-4">
                  <q-item>
                    <q-item-section>
                      <q-icon :name="'mdi-file-multiple'" color="primary" />
                    </q-item-section>
                  </q-item>
                  <q-item>
                    <q-item-section>
                      <q-item-label class="text-grey-6"
                        >Tipo de anexo</q-item-label
                      >
                      <q-item-label>{{ anexo.TipoAnexo }}</q-item-label>
                    </q-item-section>
                  </q-item>
                </div>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Descripción</q-item-label>
                    <q-item-label>{{ anexo.Descripcion }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Carácter</q-item-label>
                    <q-item-label>{{ anexo.Caracter }}</q-item-label>
                  </q-item-section>
                </q-item>
                <div
                  v-if="index !== detalle.anexos.length - 1"
                  class="line"
                ></div>
              </div>

              <q-item-label class="text-subtitle1 text-bold pad-left"
                >Datos de presentación</q-item-label
              >
              <div class="row wrap q-mb-md">
                <q-item class="col-3">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Fecha</q-item-label>
                    <q-item-label>{{
                      formateaFecha(detalle?.fechaPresentacion)
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-3">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Hora</q-item-label>
                    <q-item-label>{{ detalle?.horaPresentacion }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-3" v-if="!electronica">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Origen</q-item-label>
                    <q-item-label
                      ><span style="text-transform: capitalize">{{
                        detalle?.origenPromocion.toLowerCase()
                      }}</span></q-item-label
                    >
                  </q-item-section>
                </q-item>
                <q-item class="col-3" v-if="detalle?.numeroOrden">
                  <q-item-section>
                    <q-item-label class="text-grey-6">
                      Número de orden
                    </q-item-label>
                    <q-item-label>
                      {{ detalle?.numeroOrden }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </div>
              <q-item-label class="text-subtitle1 text-bold pad-left"
                >Mesa asignada</q-item-label
              >
              <div class="row wrap q-mb-md">
                <q-item>
                  <q-item-section>
                    <q-item-label class="text-grey-6">Secretario</q-item-label>
                    <q-item-label>{{
                      (detalle?.secretarioNombre || "") +
                      `${detalle.mesa ? " - " + detalle.mesa : ""}`
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
              <q-item-label class="text-subtitle1 text-bold pad-left"
                >Promovente</q-item-label
              >
              <div
                class="row wrap q-mb-md"
                v-if="
                  detalle?.clasePromoventeDescripcion?.toLowerCase() ===
                    'autoridad judicial' ||
                  detalle?.clasePromoventeDescripcion?.toLowerCase() ===
                    'promovente'
                "
              >
                <q-item
                  class="col-4"
                  v-if="
                    detalle?.clasePromoventeDescripcion?.toLowerCase() ===
                    'promovente'
                  "
                >
                  <q-item-section>
                    <q-item-label class="text-grey-6">Tipo</q-item-label>
                    <q-item-label>{{ detalle?.tipoPromovente }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item
                  class="col-4"
                  v-if="
                    detalle?.clasePromoventeDescripcion?.toLowerCase() ===
                    'autoridad judicial'
                  "
                >
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo de persona</q-item-label
                    >
                    <q-item-label>{{
                      detalle?.clasePromoventeDescripcion
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-8">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Nombre</q-item-label>
                    <q-item-label>{{
                      detalle?.promovente || getNombrePromoventeCompleto()
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
              <q-item-label
                v-if="
                  detalle?.clasePromoventeDescripcion?.toLowerCase() ===
                    'promovente' &&
                  tipoAsuntoSinParte &&
                  detalle?.tipoPersonaParteAsociadaPromovente
                "
                class="text-subtitle1 text-bold pad-left"
                >Parte</q-item-label
              >
              <div
                class="row wrap q-mb-md"
                v-if="
                  detalle?.clasePromoventeDescripcion?.toLowerCase() ===
                    'promovente' &&
                  tipoAsuntoSinParte &&
                  detalle?.tipoPersonaParteAsociadaPromovente
                "
              >
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo de persona</q-item-label
                    >
                    <q-item-label class="maxWidth">{{
                      detalle?.tipoPersonaParteAsociadaPromovente
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Carácter</q-item-label>
                    <q-item-label class="maxWidth">{{
                      capitalizeWords(detalle?.caracterParteAsociadaPromovente)
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section v-if="detalle.denominacionAutoridad === ''">
                    <q-item-label class="text-grey-6">Nombre</q-item-label>
                    <q-item-label>{{
                      getNombreParteNombreCompleto()
                    }}</q-item-label>
                  </q-item-section>
                  <q-item-section v-else>
                    <q-item-label class="text-grey-6"
                      >Denominación</q-item-label
                    >
                    <q-item-label>{{
                      detalle.denominacionAutoridad || ""
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
              <div
                class="row wrap q-mb-md"
                v-if="
                  detalle?.clasePromoventeDescripcion?.toLowerCase() ===
                  'partes'
                "
              >
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo de persona</q-item-label
                    >
                    <q-item-label>{{ detalle?.tipoPersonaParte }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Carácter</q-item-label>
                    <q-item-label>{{ detalle?.caracterParte }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section v-if="detalle.denominacionAutoridad !== ''">
                    <q-item-label class="text-grey-6"
                      >Denominación</q-item-label
                    >
                    <q-item-label>{{
                      detalle?.denominacionAutoridad || ""
                    }}</q-item-label>
                  </q-item-section>
                  <q-item-section v-else>
                    <q-item-label class="text-grey-6">Nombre</q-item-label>
                    <q-item-label>{{
                      getNombreParteNombreCompleto()
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
              <q-item-label class="text-subtitle1 text-bold pad-left"
                >Oficial de partes</q-item-label
              >
              <div class="row wrap q-mb-md">
                <q-item class="col-8">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Capturó</q-item-label>
                    <q-item-label>{{ detalle?.capturo }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Fecha de captura</q-item-label
                    >
                    <q-item-label>{{
                      formateaFecha(detalle?.fechaCaptura)
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
            </template>
          </q-list>
        </q-scroll-area>
      </q-card>
    </div>
  </div>
</template>

<script setup>
import { date } from "quasar";
import { onMounted, ref, computed } from "vue";
import VerPromociones from "../components/VerPromociones.vue";
import { useOficialiaStore } from "../stores/oficialia-store";
import { DetallePromocion } from "../data/detalle-promocion";
import { Firmador } from "src/helpers/firmadorInicio";
import { manejoErrores } from "src/helpers/manejo-errores";
const oficialiaStore = useOficialiaStore();
const electronica = ref(false);
const esDemandaElectronica = ref(false);
const promocionEncontrada = ref(false);
const props = defineProps({
  modelValue: {
    type: Object,
    default: new Object(),
  },
  esTramite: {
    default: false,
  },
});
const child = ref(null);
const detalle = ref(new DetallePromocion());
function formateaFecha(fecha) {
  return date.formatDate(fecha, "DD/MM/YYYY");
}
const emit = defineEmits(["update:promocionFound"]);

const tipoAsuntoSinParte = computed(() => {
  if (
    detalle.value?.catTipoAsuntoId == 18 ||
    detalle.value?.catTipoAsuntoId == 19 ||
    detalle.value?.catTipoAsuntoId == 28 ||
    detalle.value?.catTipoAsuntoId == 44 ||
    detalle.value?.catTipoAsuntoId == 45 ||
    detalle.value?.catTipoAsuntoId == 55 ||
    detalle.value?.catTipoAsuntoId == 56 ||
    detalle.value?.catTipoAsuntoId == 69 ||
    detalle.value?.catTipoAsuntoId == 72 ||
    detalle.value?.catTipoAsuntoId == 77 ||
    detalle.value?.catTipoAsuntoId == 78 ||
    detalle.value?.catTipoAsuntoId == 82 ||
    detalle.value?.catTipoAsuntoId == 83 ||
    detalle.value?.catTipoAsuntoId == 128
  )
    return false;
  else return true;
});

onMounted(async () => {
  esDemandaElectronica.value = props.modelValue.esDemandaElectronica;
  electronica.value =
    esDemandaElectronica.value || props.modelValue.esPromocionE;
  const parametros = {
    asuntoNeunId: props.modelValue.expediente?.asuntoNeunId,
    origen: props.modelValue.origen,
    numeroOrden: props.modelValue.numeroOrden,
    yearPromocion: props.modelValue.yearPromocion,
    kIdElectronica: props.modelValue.kIdElectronica,
    catOrganismoId: props.modelValue.expediente?.catOrganismoId,
    esPromocionE: props.modelValue.esPromocionE, //diferente de oficialia
    estado: props.esTramite ? 4 : props.modelValue.estado,
    tipo:
      props.modelValue.origenPromocionDescripcion ||
      props.modelValue.nombreOrigen,
    subTipo: props.modelValue.nombreOrigen,
  };

  try {
    await oficialiaStore.detallePromocion(parametros);
  } catch (error) {
    manejoErrores.mostrarError(error);
  } finally {
    detalle.value = oficialiaStore.promocion;
    if (
      detalle.value.asuntoNeunId === undefined &&
      detalle.value.kIdElectronica === undefined
    ) {
      promocionEncontrada.value = false;
    } else {
      promocionEncontrada.value = true;
    }
    emit("update:promocionFound", promocionEncontrada.value);
  }
});

function getNombrePromoventeCompleto() {
  return `${detalle.value?.promoventeNombre || ""} ${
    detalle.value?.promoventeApellidoPaterno || ""
  } ${detalle.value?.promoventeApellidoMaterno || ""}`;
}

function getNombreParteNombreCompleto() {
  return (
    (detalle.value.parteAsociadaNombre || detalle.value.parteNombre || "") +
    " " +
    (detalle.value.parteAsociadaApellidoPaterno ||
      detalle.value.parteApellidoPaterno ||
      "") +
    " " +
    (detalle.value.parteAsociadaApellidoMaterno ||
      detalle.value.parteApellidoMaterno ||
      "")
  );
}

function capitalizeWords(inputString) {
  if (inputString) {
    const words = inputString.split(" ");

    const capitalizedWords = words.map((word) => {
      if (word.length === 0) {
        return word;
      }
      const firstLetter = word.charAt(0).toUpperCase();
      const restOfWord = word.slice(1).toLowerCase();
      return firstLetter + restOfWord;
    });

    const resultString = capitalizedWords.join(" ");

    return resultString;
  }
}
async function verBoletaOCC() {
  await child.value.mostrarPdf(detalle.value?.boletaOCC);
}

async function firmar(){
  if(promocionEncontrada.value){
    const nombreArchivo = oficialiaStore.archivos.archivos[0].nombre;
    const guidDocumento = oficialiaStore.archivos.archivos[0].guidDocumento;
    const estadoCat = 1;
    const params = [];
    params[0] = { ...detalle.value };
    
    localStorage.setItem("cambioEstadoPromocion", estadoCat);
    localStorage.setItem("promocionFirmar", JSON.stringify(params));
    
    let documentos = [{}];

    documentos = [{
      nombre: nombreArchivo,
      id: guidDocumento,
      tipoArchivo: "promocion",
      modulo: 1,
    }];

    const documentosAFirmar = {
      documentos: documentos,
      firmarOficios: true,
      accion: estadoCat,
    };
    await Firmador.obtenerURLGraficoOficialia(documentosAFirmar);
  }
  
}
</script>

<style scoped>
.pad-left {
  padding-left: 1em;
}

.maxWidth {
  word-wrap: break-word;
}
.q-card {
  border-radius: 0px !important;
}
.line {
  margin-left: 10px;
  width: 90%;
  border-bottom: 1px solid #9e9e9e;
}
</style>
