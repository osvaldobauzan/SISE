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
    <div v-if="mostrarFiltros" class="row q-gutter-sm">
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
import { reactive, ref, onMounted } from "vue";
import { useOficialiaStore } from "../stores/oficialia-store";
import FiltrosColumnas from "src/components/FiltrosColumnas.vue";
import { FiltrosColumnasDatos } from "../data/filtros-columnas";
import { manejoErrores } from "src/helpers/manejo-errores";
const emit = defineEmits(["cambioFiltro"]);
const oficialiaStore = useOficialiaStore();
const valoresFiltros = reactive(new FiltrosColumnasDatos());
const mostrarFiltros = ref(false);
const copiaValoresFiltros = reactive({ ...valoresFiltros });

const filtros = ref([
  {
    label: "Origen",
    opciones: [],
    filtroNombre: "origen",
    filtroValor: "origen",
  },
  {
    label: "Mesa",
    opciones: [],
    filtroNombre: "secretario",
    filtroValor: "secretario",
  },
  {
    label: "Capturó",
    opciones: [],
    filtroNombre: "capturo",
    filtroValor: "capturo",
  },
]);

onMounted(async () => {
  try {
    await cargaCatalogosFiltros();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
});

async function cargaCatalogosFiltros() {
  const datosFiltros = await oficialiaStore.obtenerCatalogosFiltros();
  if (!datosFiltros) return;
  // Definir un objeto que mapea los nombres de filtros a sus funciones de mapeo
  const mapeosFiltro = {
    //cambiar a como lo regrese el back para ajustar el label y valor
    origen: (opcion) => ({
      label: opcion.sNombreOrigenPromocion,
      value: opcion.sNombreOrigenPromocion,
    }),
    secretario: (opcion) => ({
      label: opcion.secretario,
      value: opcion.empleadoId,
      subLabel: opcion.mesa,
    }),
    capturo: (opcion) => ({
      label: opcion.capturo,
      value: opcion.empleadoId,
      subLabel: opcion.userName,
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
  valoresFiltros[seleccionado.filtroValor] = seleccionado.value;
  if (seleccionado.value == null) return;
  emit("cambioFiltro", valoresFiltros);
}
async function eliminarFiltros() {
  Object.assign(valoresFiltros, copiaValoresFiltros);
  emit("cambioFiltro", copiaValoresFiltros);
}
</script>
