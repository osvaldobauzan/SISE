<template>
  <q-card style="width: 70vw;">
    <section class="q-m-lg">
      <q-toolbar>
        <q-toolbar-title> Reasignar secretario</q-toolbar-title>
        <q-btn flat round dense icon="mdi-close" v-close-popup />
      </q-toolbar>
      <q-toolbar style="color: red;" v-if="contadorSeleccionados == 0">
        Selecciona al menos un expediente en la primer columna
      </q-toolbar>
      <q-toolbar v-else>
          Selecciona al secretario al que deseas reasignarle los proyectos previamente marcados.
      </q-toolbar>
      <q-item>
        <q-select
        dense
        filled
        class="col"
        label="Secretario"
        :options="optionsSecretario"
        v-model="secretarioSelected"
        option-value="empleadoId"
        option-label="nombreEmpleado"
        :loading="loadingCatalogs"
        >
        </q-select>
      </q-item>
      <q-card-actions class="q-gutter-xl q-px-lg">
        <div class="col"></div>
        <q-btn
          class="col"
          no-caps
          label="Continuar"
          :disable="!secretarioSelected || contadorSeleccionados == 0"
          :color="!secretarioSelected || contadorSeleccionados == 0 ? 'grey-7' : 'primary'"
          @click="guardar">
        </q-btn>
        <q-btn
          class="col"
          outline
          no-caps
          label="Cancelar"
          @click="emit('cancelar')"
          color="primary"
          text-color="primary"
        >
        </q-btn>
      </q-card-actions>
    </section>
  </q-card>
</template>

<script setup>
  import { ref, onMounted } from "vue";
  import { useCatalogosStore } from "src/stores/catalogos-store";
  import { useProyectosStore } from "../store/proyectos-store";
  import { manejoErrores } from "src/helpers/manejo-errores";
  import { noty } from "src/helpers/notify";

  const catalogosStore = useCatalogosStore();
  const proyectosStore = useProyectosStore();
  const optionsSecretario = ref([]);
  const secretarioSelected = ref(null);

  const loadingCatalogs = ref(false);
  const contadorSeleccionados = ref(0);

  const props = defineProps({
    proyectos: Array,
    default: []
  });

  const emit = defineEmits({
    refrescarTabla: (value) => value !== null,
    cancelar: (value) => value !== null,
  });

  onMounted(async () => {
    contadorSeleccionados.value = props.proyectos.filter((proyecto) => proyecto.selected).length;
    loadingCatalogs.value = true;
    try {
      await catalogosStore.obtenerSecretarios();
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsSecretario.value = catalogosStore.secretarios;
    loadingCatalogs.value = false;
  });

  function guardar() {
    try {
      proyectosStore.reasignarSecretarios(secretarioSelected.value.empleadoId, props.proyectos.filter((proyecto) => proyecto.selected).map((proyecto) => proyecto.proyectoId));
      noty.correcto(`Se han reasignado correctamente a los secretarios`);
      emit("refrescarTabla", true);
      emit("cerrar");
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
</script>
