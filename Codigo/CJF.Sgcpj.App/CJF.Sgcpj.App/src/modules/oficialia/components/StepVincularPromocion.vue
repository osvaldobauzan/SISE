<template>
  <q-card flat>
    <q-item-label class="text-bold q-my-md text-subtitle1"
      >Vincular promoción</q-item-label
    >
    <q-item v-if="expediente">
      <q-item-section>
        <q-item-label class="text-bold">{{
          expediente.expediente?.asuntoAlias
        }}</q-item-label>
        <q-item-label caption>{{
          expediente.expediente?.catTipoAsunto
        }}</q-item-label>
      </q-item-section>
    </q-item>
    <div class="row" v-if="expediente">
      <div class="col">
        <q-input :model-value="fechaPromocion" disable label="Fecha promoción">
          <template v-slot:append>
            <q-icon name="mdi-calendar-blank" class="cursor-pointer">
              <q-popup-proxy
                cover
                transition-show="scale"
                transition-hide="scale"
              >
                <q-date v-model="fechaPromocion" mask="DD-MM-YYYY">
                  <div class="row items-center justify-end">
                    <q-btn v-close-popup label="Close" color="primary" flat />
                  </div>
                </q-date>
              </q-popup-proxy>
            </q-icon>
          </template>
        </q-input>
      </div>
      <div class="col q-ml-sm">
        <q-input :model-value="horaPromocion" disable label="Hora acuerdo">
          <template v-slot:append>
            <q-icon name="mdi-clock-time-four-outline" class="cursor-pointer">
              <q-popup-proxy
                cover
                transition-show="scale"
                transition-hide="scale"
              >
                <q-time with-seconds v-model="horaPromocion" mask="HH:mm">
                  <div class="row items-center justify-end">
                    <q-btn v-close-popup label="Close" color="primary" flat />
                  </div>
                </q-time>
              </q-popup-proxy>
            </q-icon>
          </template>
        </q-input>
      </div>
    </div>
    <template
      v-if="
        (esEdicion &&
          mostrarArchivoAnterior &&
          !origenesElectronicos.some(
            (o) => o == promocion?.origen || o == detalle.origenPromocionId,
          )) ||
        (esEdicion &&
          mostrarArchivoAnterior &&
          origenesElectronicos.some(
            (o) => o == promocion?.origen || o == detalle.origenPromocionId,
          ) &&
          !(detalle.nombreArchivo == null || detalle.nombreArchivo == ''))
      "
    >
      <div
        class="row wrap justify-between items-center content-around q-my-md"
        style="background: rgb(0 0 0 / 3%)"
      >
        <div class="row wrap justify-center items-center content-around">
          <div class="q-avatar">
            <div class="q-avatar__content row flex-center overflow-hidden">
              <i
                class="q-icon text-primary notranslate material-icons"
                aria-hidden="true"
                role="presentation"
                >insert_drive_file</i
              >
            </div>
          </div>
          <div class="ellipsis" style="font-weight: bold">
            {{ detalle.nombreArchivo }}
          </div>
        </div>
        <div class="column justify-end items-end content-end">
          <q-btn
            @click="cargarNuevoArchivo(true)"
            color="blue"
            flat
            dense
            class="q-mr-md"
          >
            <q-tooltip>Reemplazar</q-tooltip>
            <span class="text-caption text-capitalize"
              ><q-icon :name="'mdi-replay'"></q-icon> <br />Reemplazar</span
            >
          </q-btn>
        </div>
      </div>
    </template>
    <template
      v-if="
        esEdicion &&
        mostrarArchivoAnterior &&
        origenesElectronicos.some(
          (o) => o == promocion?.origen || o == detalle.origenPromocionId,
        ) &&
        detalle.conArchivo == 1
      "
    >
      <template
        v-for="archivo in JSON.parse(detalle.archivos)"
        v-bind:key="archivo"
      >
        <!-- <div
          v-if="archivo.Archivo !== detalle.boletaOCC"
          class="row wrap justify-between items-center content-around q-my-md"
          style="background: rgb(0 0 0 / 3%)"
        > -->
        <div
          v-if="archivo.Archivo !== detalle.boletaOCC"
          style="background: rgb(0 0 0 / 3%)"
        >
          <div
            class="row wrap justify-between items-center content-around q-my-md"
            style="background: rgb(0 0 0 / 3%)"
          >
            <div class="row wrap justify-center items-center content-around">
              <div class="q-avatar">
                <div class="q-avatar__content row flex-center overflow-hidden">
                  <i
                    class="q-icon text-primary notranslate material-icons"
                    aria-hidden="true"
                    role="presentation"
                    >insert_drive_file</i
                  >
                </div>
              </div>
              <div class="ellipsis" style="font-weight: bold">
                {{ archivo.Archivo }}
              </div>
            </div>
            <div class="column justify-end items-end content-end">
              <q-btn
                @click="cargarNuevoArchivo(true)"
                color="blue"
                flat
                dense
                class="q-mr-md"
              >
                <q-tooltip>Reemplazar</q-tooltip>
                <span class="text-caption text-capitalize"
                  ><q-icon :name="'mdi-replay'"></q-icon> <br />Reemplazar</span
                >
              </q-btn>
            </div>
          </div>

          <!--   <div class="row wrap justify-center items-center content-around">
            <div class="q-avatar">
              <div class="q-avatar__content row flex-center overflow-hidden">
                <i
                  class="q-icon text-primary notranslate material-icons"
                  aria-hidden="true"
                  role="presentation"
                  >insert_drive_file</i
                >
              </div>
            </div>
            <div class="ellipsis" style="font-weight: bold">
              {{ archivo.Archivo }}
            </div>
          </div> -->
        </div>
      </template>
      <div
        v-if="detalle?.boletaOCC"
        class="row wrap justify-between items-center content-around q-my-md"
        style="background: rgb(0 0 0 / 3%)"
      >
        <div class="row wrap justify-center items-center content-around">
          <div class="q-avatar">
            <div class="q-avatar__content row flex-center overflow-hidden">
              <i
                class="q-icon text-primary notranslate material-icons"
                aria-hidden="true"
                role="presentation"
                >insert_drive_file</i
              >
            </div>
          </div>
          <q-btn
            flat
            noCaps
            color="light-blue"
            :label="detalle?.boletaOCC"
            align="left"
            class="q-pa-none tex-grey"
            @click="emit('event:verBoletaOcc')"
          >
            <q-tooltip>Ver Boleta OCC </q-tooltip>
          </q-btn>
        </div>
      </div>
      <div
        v-if="!detalle.archivos && detalle.nombreArchivo"
        v-bind:key="archivo"
        class="row wrap justify-between items-center content-around q-my-md"
        style="background: rgb(0 0 0 / 3%)"
      >
        <div class="row wrap justify-center items-center content-around">
          <div class="q-avatar">
            <div class="q-avatar__content row flex-center overflow-hidden">
              <i
                class="q-icon text-primary notranslate material-icons"
                aria-hidden="true"
                role="presentation"
                >insert_drive_file</i
              >
            </div>
          </div>
          <div class="ellipsis" style="font-weight: bold">
            {{ detalle.nombreArchivo }}
          </div>
        </div>
      </div>
    </template>
    <div
      class="column justify-end content-end"
      v-if="!esEdicion || (esEdicion && !mostrarArchivoAnterior)"
      :style="
        parametros.archivoAVincular !== null ? '' : 'border: 3px dashed #ccc'
      "
    >
      <q-file
        :model-value="parametros.archivoAVincular"
        @update:model-value="updateFiles"
        borderless
        class="full-width full-height"
        accept=".pdf"
        max-file-size="30000000"
        @rejected="manejoErrores.archivoInvalido"
        :rules="[
          (val) =>
            esEdicion && detalle.nombreArchivo !== null && archivoRequerido
              ? Validaciones.validaInputRequerido(val)
              : true,
          async (val) => await Validaciones.validaExtension(val, 'pdf'),
        ]"
        :lazy-rules="true"
        :reactive-rules="true"
      >
        <template v-if="!parametros.archivoAVincular" v-slot:prepend>
          <div class="row label-file">
            <div class="col">
              <q-item-label
                ><q-icon name="mdi-upload" />Arrastra y suelta o
                <q-btn no-caps flat padding="0px" color="light-blue"
                  >busca un archivo</q-btn
                ></q-item-label
              >
            </div>
          </div>
        </template>
        <template v-slot:file>
          <q-chip class="full-width full-height q-my-xs" square>
            <q-avatar>
              <q-icon :name="'insert_drive_file'" color="primary" />
            </q-avatar>
            <div style="width: 25%" class="ellipsis relative-position">
              <span class="text-bold text-body2">{{
                parametros.archivoAVincular.name
              }}</span>
              <span class="q-ml-sm text-caption">{{
                parametros.archivoAVincular.size / 1024 < 1024
                  ? (parametros.archivoAVincular.size / 1024).toFixed(1) + "KB"
                  : (parametros.archivoAVincular.size / 1024 / 1024).toFixed(
                      1,
                    ) + "MB"
              }}</span>
            </div>
            <q-tooltip>
              {{ parametros.archivoAVincular.name }}
            </q-tooltip>
          </q-chip>
        </template>
        <template v-if="parametros.archivoAVincular" v-slot:after>
          <q-item
            dense-toggle
            class="q-field-after"
            clickable
            @click="updateFiles(null)"
          >
            <q-item-section align="left">
              <q-icon size="1.2em" :name="'mdi-close'" color="primary" />
            </q-item-section>
          </q-item>
        </template>
      </q-file>

      <div
        class="column justify-end content-end"
        v-if="
          esEdicion && !mostrarArchivoAnterior && detalle.nombreArchivo !== null
        "
      >
        <q-btn
          @click="
            () => {
              cargarNuevoArchivo(false), emit('event:cancelarReemplazo');
            }
          "
          color="blue"
          flat
          dense
          class="q-mr-ms"
        >
          <span class="text-caption text-capitalize"> Cancelar reemplazo</span>
        </q-btn>
      </div>
    </div>
    <div class="text-grey-6 row row justify-between q-pt-xs q-pl-xs">
      <div>
        <q-icon size="1.2em" color="light-blue" name="info" /> Solo puedes subir
        archivos menores a 30MB en formato PDF.
      </div>
    </div>
    <q-banner class="light-blue-3 doc-note doc-note--tip">
      <q-icon
        style="position: absolute; left: 0px"
        size="1.2em"
        color="light-blue"
        name="info"
        class="q-ml-xs"
      />
      Podrás vincular la promoción aún posteriormente a su captura, pero no será
      asignada a un secretario hasta contar con un archivo vinculado.
    </q-banner>
  </q-card>
</template>

<script setup>
import { date } from "quasar";
import { Validaciones } from "src/helpers/validaciones";
import { ref, onMounted, onBeforeUnmount, computed, watch } from "vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import { DetallePromocion } from "../data/detalle-promocion";
import { useOficialiaStore } from "../stores/oficialia-store";
import { Utils } from "src/helpers/utils";
const oficialiaStore = useOficialiaStore();
const parametros = computed({
  get() {
    return props.detallePromocion;
  },
  set(value) {
    emit("params:cambio", value);
  },
});
const origenesElectronicos = [5, 15, 29, 6, 14, 22];
const esEdicion = ref(false);
const archivoRequerido = ref(false);

let mostrarArchivoAnterior = ref(Boolean);
const detalle = ref(new DetallePromocion());
const fechaPromocion = ref("");
const horaPromocion = ref("");
const props = defineProps({
  expediente: {
    type: Object,
  },
  multiple: {
    type: Boolean,
    default: false,
  },
  detallePromocion: {
    type: Object,
    required: true,
  },
  esEditar: {
    type: Boolean,
  },
  promocion: {
    type: Object,
  },
});

const uploading = ref(null);

const emit = defineEmits({
  "params:cambio": (value) => value !== null,
  "event:coincideFalse": () => true,
  "event:cancelarReemplazo": () => true,
  "event:verBoletaOcc": () => true,
});

function cleanUp() {
  clearTimeout(uploading.value);
}
async function cargarNuevoArchivo(val) {
  archivoRequerido.value = val;
  emit("event:coincideFalse", val);
  mostrarArchivoAnterior.value = !mostrarArchivoAnterior.value;
}
onBeforeUnmount(cleanUp);

async function updateFiles(newFiles) {
  parametros.value.archivoAVincular = await Utils.fileToBlob(newFiles);
  if (!parametros.value.archivoAVincular) {
    emit("event:coincideFalse");
  }
  emit("params:cambio", parametros);
}

onMounted(async () => {
  if (props.expediente) {
    fechaPromocion.value =
      date.formatDate(
        Date.parse(props.expediente.fechaPresentacion),
        "DD/MM/YYYY",
      ) || date.formatDate(Date.now(), "DD/MM/YYYY");
    horaPromocion.value = props.expediente.fechaPresentacion?.split("T")[1];
  } else {
    fechaPromocion.value = date.formatDate(
      new Date().toISOString(),
      "DD-MM-YYYY",
    );
    horaPromocion.value = date.formatDate(new Date().toISOString(), "HH:mm");
  }
  if (props.promocion) {
    esEdicion.value = true;
    watch(
      () => oficialiaStore.promocion,
      async () => {
        if (!Utils.isEmpty(oficialiaStore.promocion)) {
          detalle.value = oficialiaStore.promocion;
          if (
            (detalle.value.nombreArchivo == null ||
              detalle.value.nombreArchivo === "") &&
            detalle.value.conArchivo != 1
          ) {
            mostrarArchivoAnterior.value = false;
          }
        }
      },
    );
  }
});

onBeforeUnmount(() => {});
</script>
<script>
export default {
  inheritAttrs: false,
};
</script>
<style scoped lang="css">
.doc-note {
  font-size: 16px;
  border-radius: 4px;
  margin: 16px 0 !important;
  padding: 16px !important;
  padding-left: 24px !important;
  border-width: 1px;
  border-style: solid;
}

.doc-note--tip {
  background-color: #bbdefb;
  border-color: #bbdefb;
}
.bg-warning {
  background: #ffdcdc !important;
}

.separar-label-center {
  text-align: center;
  padding-bottom: 2em;
  padding-top: 3em;
}

.separar-label {
  padding-bottom: 2em;
  padding-top: 1em;
}

.centrar-btn {
  display: flex;
  flex-direction: row;
  justify-content: center;
  padding-bottom: 3em;
}

.label-file {
  text-align: center;
  font-size: 15px;
  position: absolute;
  min-width: 100%;
}

.q-chip {
  background: rgb(0 0 0 / 3%);
}
:deep(.q-field__after) {
  padding-left: unset;
}
</style>
