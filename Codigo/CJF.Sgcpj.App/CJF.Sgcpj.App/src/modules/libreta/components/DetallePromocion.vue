<template>
  <div class="row content-stretch">
    <div
      class="col-6"
      v-if="detalle?.nombreArchivo !== null || detalle?.numeroAnexos > 0"
    >
      <VerPromociones
        promocionDocumento
        :promocion="modelValue"
        :esDetalle="true"
      />
    </div>
    <div
      style="background: white"
      :class="
        detalle?.nombreArchivo !== null || detalle?.numeroAnexos > 0
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
            </q-item>
          </q-toolbar-title>
          <q-btn flat round dense icon="mdi-close" v-close-popup />
        </q-toolbar>
        <q-separator />
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
                    formateaFecha(detalle?.fechaPresentacion)
                  }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6">Hora</q-item-label>
                  <q-item-label>{{ detalle?.horaPresentacion }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6"
                    >Registró la promoción</q-item-label
                  >
                  <q-item-label>{{
                    detalle?.promovente || getNombrePromoventeCompleto()
                  }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6">Boleta OCC</q-item-label>
                  <q-item-label>{{ detalle?.boletaOCC }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6">OCC</q-item-label>
                  <q-item-label>{{ detalle?.oCC }}</q-item-label>
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
                      detalle?.origenPromocion.toLowerCase()
                    }}</span>
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6">Archivos</q-item-label>
                  <q-item-label>{{ detalle?.totalArchivos }}</q-item-label>
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

            <q-item-label class="text-subtitle1 text-bold pad-left"
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
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6">Fecha</q-item-label>
                  <q-item-label>{{
                    formateaFecha(detalle?.fechaPresentacion)
                  }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6">Hora</q-item-label>
                  <q-item-label>{{ detalle?.horaPresentacion }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item class="col-4" v-if="!electronica">
                <q-item-section>
                  <q-item-label class="text-grey-6">Origen</q-item-label>
                  <q-item-label
                    ><span style="text-transform: capitalize">{{
                      detalle?.origenPromocion.toLowerCase()
                    }}</span></q-item-label
                  >
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
            <div class="row wrap q-mb-md" v-if="electronica">
              <q-item class="col-4">
                <q-item-section>
                  <q-item-label class="text-grey-6">Nombre</q-item-label>
                  <q-item-label>{{
                    detalle?.promovente || getNombrePromoventeCompleto()
                  }}</q-item-label>
                </q-item-section>
              </q-item>
            </div>
            <div
              class="row wrap q-mb-md"
              v-if="
                modelValue.clasePromoventeDescripcion?.toLowerCase() ===
                  'autoridad judicial' ||
                modelValue.clasePromoventeDescripcion?.toLowerCase() ===
                  'promovente'
              "
            >
              <q-item
                class="col-4"
                v-if="
                  modelValue.clasePromoventeDescripcion?.toLowerCase() ===
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
                  modelValue.clasePromoventeDescripcion?.toLowerCase() ===
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
                modelValue.clasePromoventeDescripcion?.toLowerCase() ===
                'promovente'
              "
              class="text-subtitle1 text-bold pad-left"
              >Parte</q-item-label
            >
            <div
              class="row wrap q-mb-md"
              v-if="
                modelValue.clasePromoventeDescripcion?.toLowerCase() ===
                'promovente'
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
                <q-item-section>
                  <q-item-label class="text-grey-6">Nombre</q-item-label>
                  <q-item-label>{{
                    getNombreParteNombreCompleto()
                  }}</q-item-label>
                </q-item-section>
              </q-item>
            </div>
            <div
              class="row wrap q-mb-md"
              v-if="
                modelValue.clasePromoventeDescripcion?.toLowerCase() ===
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
                <q-item-section>
                  <q-item-label class="text-grey-6">Nombre</q-item-label>
                  <q-item-label>{{
                    getNombreParteNombreCompleto() ||
                    getNombrePromoventeCompleto()
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

          <!-- <q-separator></q-separator> -->
          <!-- <q-item v-for="item in getTableRows(selectedItem.Detalle)" :key="item">
          <q-item-section>
            <q-item-label>
              {{ item.Valor }}
            </q-item-label>
            <q-item-label class="text-grey-6">{{ item.Propiedad }}</q-item-label>
          </q-item-section>
        </q-item> -->
        </q-list>
      </q-card>
    </div>
  </div>
</template>

<script setup>
import { date } from "quasar";
import { watch, onMounted, onUpdated, onBeforeUnmount, ref } from "vue";
import { Promocion } from "../data/promocion";
import VerPromociones from "../components/VerPromociones.vue";
import { useLibretaStore } from "../stores/libreta-store";
import { DetallePromocion } from "../data/detalle-promocion";
const oficialiaStore = useLibretaStore();
const electronica = ref(false);
const esDemandaElectronica = ref(false);
const props = defineProps({
  // v-model
  modelValue: {
    type: Promocion,
    default: {},
  },
});
const detalle = ref(new DetallePromocion());
function formateaFecha(fecha) {
  return date.formatDate(fecha, "DD/MM/YYYY");
}

// const emit = defineEmits({
// 	// v-model event with validation
// 	'update:modelValue': (value) => value !== null,
// });

// const value = computed({
// 	get () {
// 		return props.modelValue;
// 	},
// 	// set (value) {
// 	// 	emit('update:modelValue', value);
// 	// },
// });
const stopWatch = watch(
  () => props.modelValue,
  async () => {
    // do something
  },
  {
    immediate: true,
  },
);

onMounted(async () => {
  esDemandaElectronica.value = props.modelValue.esDemandaElectronica;
  electronica.value =
    esDemandaElectronica.value || props.modelValue.esPromocionE;
  const parametros = {
    asuntoNeunId: props.modelValue.expediente.asuntoNeunId,
    origen: props.modelValue.origen,
    numeroOrden: props.modelValue.numeroOrden,
    yearPromocion: props.modelValue.yearPromocion,
    kIdElectronica: props.modelValue.kIdElectronica,
    catOrganismoId: props.modelValue.expediente.catOrganismoId,
    esPromocionE: props.modelValue.esPromocionE,
    estado: props.modelValue.estado,
  };

  try {
    await oficialiaStore.detallePromocion(parametros);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  detalle.value = oficialiaStore.promocion;
});

onUpdated(() => {});

onBeforeUnmount(() => {
  stopWatch();
});

function getNombrePromoventeCompleto() {
  return (
    (detalle.value?.promoventeNombre || "") +
    (" " + detalle.value.promoventeApellidoPaterno || "") +
    (" " + detalle.value.promoventeApellidoMaterno || "")
  );
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
