<template>
  <div class="q-px-md q-pt-md row q-gutter-xs">
    <div>
      <q-btn
        text-color="primary"
        flat
        no-caps
        rounded
        label="Filtro"
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
import { useActuariaDetalleNotificacionesStore } from "../stores/actuaria-detalle-notificaciones-store";
import FiltrosColumnas from "src/components/FiltrosColumnas.vue";
import { FiltrosColumnasNotificacionesDatos } from "../data/filtrosColumnasNotificaciones";
import { manejoErrores } from "src/helpers/manejo-errores";
const emit = defineEmits(["cambioFiltro"]);
const actuariaNotificacionesStore = useActuariaDetalleNotificacionesStore();
const valoresFiltros = reactive(new FiltrosColumnasNotificacionesDatos());
const mostrarFiltros = ref(false);
const copiaValoresFiltros = reactive({ ...valoresFiltros });

const filtros = ref([
  {
    label: "Tipo de partes",
    opciones: [],
    filtroNombre: "tipoParte",
    filtroValor: "filtroTipoParteID",
    width: "180px",
  },
  {
    label: "Tipo de notificaciones",
    opciones: [],
    filtroNombre: "tipoNotificacion",
    filtroValor: "filtroTipoNotificacionID",
    width: "180px",
  },
  {
    label: "Actuario",
    opciones: [],
    filtroNombre: "actuario",
    filtroValor: "filtroActuarioID",
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
  const datosFiltros =
    await actuariaNotificacionesStore.getNotificacionesFiltros();
  // Definir un objeto que mapea los nombres de filtros a sus funciones de mapeo
  const mapeosFiltro = {
    tipoParte: (opcion) => ({
      label: opcion.sDescripcion,
      value: opcion.id,
    }),
    tipoNotificacion: (opcion) => ({
      label: opcion.sDescripcion,
      value: opcion.kIdCatNotificaciones,
    }),
    actuario: (opcion) => ({
      label: opcion.nombreActuario,
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
  valoresFiltros[seleccionado.filtroValor] = seleccionado.value;
  if (seleccionado.value == null) return;
  emit("cambioFiltro", valoresFiltros);
}
async function eliminarFiltros() {
  Object.assign(valoresFiltros, copiaValoresFiltros);
  emit("cambioFiltro", copiaValoresFiltros);
}
</script>
