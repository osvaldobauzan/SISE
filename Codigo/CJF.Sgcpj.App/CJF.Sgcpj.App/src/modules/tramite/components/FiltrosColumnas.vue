<template>
  <q-toolbar class="q-gutter-sm">
    <q-btn
      flat
      no-caps
      rounded
      text-color="primary"
      label="Filtro"
      icon="mdi-filter-variant"
      color="primary"
      @click="mostrarFiltros = !mostrarFiltros"
    >
    </q-btn>
    <div class="row q-gutter-sm" v-if="mostrarFiltros">
      <FiltrosColumnas
        v-for="filtro in filtros"
        :key="filtro.filtroNombre"
        :label="filtro.label"
        :opciones="filtro.opciones"
        :valoresFiltros="valoresFiltros"
        :filtroValor="filtro.filtroValor"
        valor-default=""
        label-default="Ninguno"
        @cambio-filtro="cambioFiltro"
        :style="
          filtro.width ? `width: ${filtro.width} !important` : 'width: 150px'
        "
      />
      <q-btn
        v-if="Object.values(valoresFiltros).some((valor) => valor !== '')"
        unelevated
        no-caps
        rounded
        color="primary"
        label="Borrar filtros"
        @click="eliminarFiltros"
        class="col-auto"
      />
    </div>
  </q-toolbar>
</template>
<script setup>
import { reactive, ref, onMounted, computed } from "vue";
import { useTramiteStore } from "../store/tramite-store";
import FiltrosColumnas from "src/components/FiltrosColumnas.vue";
import { FiltrosColumnasDatos } from "../data/filtros-columnas";
import { manejoErrores } from "src/helpers/manejo-errores";
const emit = defineEmits(["cambioFiltro"]);
const props = defineProps({
  modelValue: {
    default: new FiltrosColumnasDatos(),
  },
});
const tramiteStore = useTramiteStore();
const valoresFiltros = computed({
  get() {
    return props.modelValue;
  },
  set() {},
});
const mostrarFiltros = ref(false);
const copiaValoresFiltros = reactive({ ...valoresFiltros.value });

const filtros = ref([
  {
    label: "Mesa",
    opciones: [],
    filtroNombre: "secretario",
    filtroValor: "secretario",
  },
  {
    label: "Origen",
    opciones: [],
    filtroNombre: "origen",
    filtroValor: "origen",
  },
  {
    label: "Tipo de asunto",
    opciones: [],
    filtroNombre: "tipoAsunto",
    filtroValor: "asunto",
    width: "130px",
  },
  {
    label: "Capturó",
    opciones: [],
    filtroNombre: "capturo",
    filtroValor: "capturo",
  },
  {
    label: "Preautorizó",
    opciones: [],
    filtroNombre: "preautorizo",
    filtroValor: "preautorizo",
  },
  {
    label: "Autorizó",
    opciones: [],
    filtroNombre: "autorizo",
    filtroValor: "autorizo",
  },
  {
    label: "Canceló",
    opciones: [],
    filtroNombre: "cancelo",
    filtroValor: "cancelo",
  },
]);

onMounted(async () => {
  try {
    const datosFiltros = await tramiteStore.obtenerCatalogosFiltros();
    if (!datosFiltros) return;
    cargaCatalogosFiltros(datosFiltros);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
});

function cargaCatalogosFiltros(datosFiltros) {
  // Definir un objeto que mapea los nombres de filtros a sus funciones de mapeo
  const mapeosFiltro = {
    secretario: (opcion) => ({
      label: opcion.secretario,
      subLabel: opcion.mesa,
      value: opcion.empleadoId,
    }),
    origen: (opcion) => ({
      label: opcion.sNombreOrigenPromocion,
      value: opcion.sNombreOrigenPromocion,
    }),
    tipoAsunto: (opcion) => ({
      label: opcion.tipoAsunto,
      value: opcion.catTipoAsuntoId,
    }),
    capturo: (opcion) => ({
      label: opcion.capturo,
      subLabel: opcion.userName,
      value: opcion.empleadoId,
    }),
    autorizo: (opcion) => ({
      label: opcion.autorizo,
      subLabel: opcion.userName,
      value: opcion.empleadoId,
    }),
    preautorizo: (opcion) => ({
      label: opcion.preautorizo,
      subLabel: opcion.userName,
      value: opcion.empleadoId,
    }),
    cancelo: (opcion) => ({
      label: opcion.cancelo,
      subLabel: opcion.userName,
      value: opcion.empleadoId,
    }),
  };
  // se guardan las opciones de cada filtro de acuerdo al objeto mapeosFiltro
  filtros.value.forEach((filtro) => {
    const opcionesCorrespondientes = datosFiltros[filtro.filtroNombre];
    if (opcionesCorrespondientes) {
      const funcionMapeo = mapeosFiltro[filtro.filtroNombre];
      if (funcionMapeo) {
        filtro.opciones = opcionesCorrespondientes.map(funcionMapeo);
      }
    }
  });
}

async function cambioFiltro(seleccionado) {
  valoresFiltros.value[seleccionado.filtroValor] = seleccionado.value;
  if (seleccionado.value == null) return;
  emit("cambioFiltro", valoresFiltros.value);
}
async function eliminarFiltros() {
  Object.assign(valoresFiltros.value, copiaValoresFiltros);
  emit("cambioFiltro", copiaValoresFiltros);
}
</script>
