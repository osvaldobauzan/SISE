<template>
  <div class="q-px-md q-pt-md row q-gutter-xs">
    <div>
      <q-btn
        flat
        no-caps
        rounded
        label="Filtro"
        text-color="primary"
        icon="mdi-filter-variant"
        @click="mostrarFiltros = !mostrarFiltros"
      >
      </q-btn>
    </div>
    <div class="col" v-if="mostrarFiltros">
      <div class="row q-gutter-xl">
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
    </div>
  </div>
</template>
<script setup>
import { reactive, ref, onMounted } from "vue";
import { useActuariaStore } from "../stores/actuaria-store";
import FiltrosColumnas from "src/components/FiltrosColumnas.vue";
import { FiltrosColumnasDatos } from "../data/filtrosColumnas";
import { manejoErrores } from "src/helpers/manejo-errores";
const emit = defineEmits(["cambioFiltro"]);
const actuariaStore = useActuariaStore();
const valoresFiltros = reactive(new FiltrosColumnasDatos());
const mostrarFiltros = ref(false);
const copiaValoresFiltros = reactive({ ...valoresFiltros });

const filtros = ref([
  {
    label: "Estado",
    opciones: [],
    filtroNombre: "filtroEstado",
    filtroValor: "estado",
  },
  {
    label: "Contenido",
    opciones: [],
    filtroNombre: "filtroContenido",
    filtroValor: "contenido",
    width: "170px",
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
  const datosFiltros = await actuariaStore.obtenerCatalogosFiltros();
  // Definir un objeto que mapea los nombres de filtros a sus funciones de mapeo
  const mapeosFiltro = {
    filtroEstado: (opcion) => ({
      label: opcion.estado,
      value: opcion.estado,
    }),
    filtroContenido: (opcion) => ({
      label: opcion.descripcion,
      value: opcion.id,
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
