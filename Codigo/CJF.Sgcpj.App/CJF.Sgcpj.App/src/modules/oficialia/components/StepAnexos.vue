<template>
  <div class="row">
    <q-item-label class="text-bold q-my-md text-subtitle1"
      >Vincular anexos</q-item-label
    >
    <div class="row wrap full-width">
      <template v-for="(anexo, i) in value.anexos" v-bind:key="anexo.id">
        <q-card class="col-3 q-ml-xl q-mb-sm">
          <q-toolbar>
            <q-toolbar-title>
              <q-item>
                <q-item-section>
                  <div class="q-pb-sm text-subtitle1 text-bold">
                    Anexo {{ i + 1 }}
                  </div>
                </q-item-section>
              </q-item>
            </q-toolbar-title>
            <q-btn
              color="blue"
              flat
              round
              dense
              icon="mdi-close"
              @click="() => eliminarAnexo(anexo, i)"
            >
              <q-tooltip>Eliminar</q-tooltip></q-btn
            >
          </q-toolbar>
          <q-card-section>
            <q-item-label class="q-pb-xs">
              <span class="mdi mdi-circle-medium"></span
              >{{ anexo.tipoAnexo?.descripcion }}</q-item-label
            >
            <q-item-label class="q-pb-xs"
              ><span class="mdi mdi-circle-medium"></span
              >{{
                anexo.descripcion?.descripcion
                  ? anexo.descripcion.descripcion
                  : "Sin descripción"
              }}</q-item-label
            >
            <q-item-label class="q-pb-xs"
              ><span class="mdi mdi-circle-medium"></span
              >{{
                anexo.caracter?.descripcion
                  ? anexo.caracter.descripcion
                  : "Sin carácter"
              }}</q-item-label
            >
          </q-card-section>
          <q-card-actions align="center">
            <q-btn
              flat
              size="12px"
              color="blue"
              icon="mdi-file-edit-outline"
              @click="emit('update:anexo', anexo)"
            >
              Editar
            </q-btn>
          </q-card-actions>
        </q-card>
      </template>
    </div>
    <div
      @click="emit('add:anexo')"
      class="row full-width"
      style="border: 3px dashed #ccc"
    >
      <div class="col centrar-btn">
        <q-btn icon="mdi-plus-circle" no-caps flat padding="0px" color="grey-6"
          >Añadir anexo</q-btn
        >
      </div>
    </div>
  </div>
</template>

<script setup>
import {
  computed,
  watch,
  onMounted,
  onUpdated,
  onBeforeUnmount,
  ref,
} from "vue";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { FormPromocion } from "../data/form-promocion";
import { EliminarAnexo } from "../data/eliminar-anexo";
import { date } from "quasar";
const catalogosStore = useCatalogosStore();

const conAnexos = ref(false);
const tiposAnexoOptions = ref(catalogosStore.tiposAnexo);
const descripcionesAnexoOptions = ref(catalogosStore.descripcionesAnexo);
const caracteresAnexoOptions = ref(catalogosStore.caracteresAnexo);
const props = defineProps({
  modelValue: {
    default: "",
  },
  detallePromocion: {
    type: Object,
    default: new FormPromocion(),
  },
  esEdicion: {
    type: Boolean,
  },
});
catalogosStore.$subscribe(() => {
  tiposAnexoOptions.value = catalogosStore.tiposAnexo;
  descripcionesAnexoOptions.value = catalogosStore.descripcionesAnexo;
  caracteresAnexoOptions.value = catalogosStore.caracteresAnexo;
});
const emit = defineEmits({
  "params:cambio": (value) => value !== null,
  continuar: () => true,
  tieneAnexos: () => true,
  "add:anexo": true,
  "update:anexo": (value) => value !== null,
});

// eslint-disable-next-line no-unused-vars
const value = computed({
  get() {
    return props.detallePromocion;
  },
  set(value) {
    emit("params:cambio", value);
  },
});

const stopWatch = watch(
  () => conAnexos.value,
  // eslint-disable-next-line no-unused-vars
  async (_newValue, _oldValue) => {
    emit("tieneAnexos", _newValue);
  },
  {
    immediate: true,
  },
);
const stopWatchDetalle = watch(
  () => props.detallePromocion,
  // eslint-disable-next-line no-unused-vars
  async (_newValue, _oldValue) => {},
  {
    immediate: true,
  },
);
onMounted(() => {});

onUpdated(() => {});

onBeforeUnmount(() => {
  stopWatch();
  stopWatchDetalle();
});

async function eliminarAnexo(anexo, index) {
  if (value.value.anexos[index].guardadoEnBD) {
    const fecha =
      date.formatDate(props.detallePromocion.fechaPresentacion, "DD/MM/YYYY") ||
      date.formatDate(Date.now(), "DD/MM/YYYY");
    const year = fecha.substring(6);
    const params = new EliminarAnexo();
    params.asuntoID = props.detallePromocion.tipoAsunto.catTipoAsuntoId;
    params.asuntoNeunId = props.detallePromocion.asuntoNeunId;
    params.numeroRegistro = props.detallePromocion.registro;
    params.numeroOrden = props.detallePromocion.numeroOrden;
    params.origen = props.detallePromocion.origen;
    params.consecutivo = anexo.consecutivo;
    params.yearPromocion = year;
    value.value.anexosAEliminar.push(params);
    value.value.anexos.splice(index, 1);
    if (value.value.anexos.length === 0) {
      conAnexos.value = false;
    }
    emit("params:cambio", value);
  } else {
    value.value.anexos.splice(index, 1);
    if (value.value.anexos.length === 0) {
      conAnexos.value = false;
    }
    emit("params:cambio", value);
  }
}
</script>

<style scoped>
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
  padding-bottom: 1.5em;
  padding-top: 1.5em;
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
</style>
