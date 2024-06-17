<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title>Datos generales</q-toolbar-title>
    </q-toolbar>
    <q-card-section>
      <div class="row">
        <q-item v-if="datosGenerales?.fechaOCC">
          <q-item-section>
            <q-item-label>{{
              datosGenerales?.fechaOCC || ""
            }}</q-item-label>
            <q-item-label caption>Presentación OCC</q-item-label>
          </q-item-section>
        </q-item>
        <q-item v-if="datosGenerales?.fechaOrg">
          <q-item-section>
            <q-item-label>{{
              datosGenerales?.fechaOrg || ""
            }}</q-item-label>
            <q-item-label caption>Ingreso al órgano</q-item-label>
          </q-item-section>
        </q-item>
        <q-item v-if="datosGenerales?.secretario">
          <q-item-section>
            <q-item-label>{{
              datosGenerales?.secretario || ""
            }}</q-item-label>
            <q-item-label caption>Secretario</q-item-label>
          </q-item-section>
        </q-item>
        <q-item v-if="datosGenerales?.mesa">
          <q-item-section>
            <q-item-label>{{ datosGenerales?.mesa || "" }}</q-item-label>
            <q-item-label caption>Mesa</q-item-label>
          </q-item-section>
        </q-item>
      </div>
    </q-card-section>
    <q-inner-loading :showing="isLoading"></q-inner-loading>
  </q-card>
</template>

<script setup>
import { onMounted, ref } from 'vue';
import { manejoErrores } from "src/helpers/manejo-errores";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store";

const expedienteElectronicoStore = useExpedienteElectronicoStore();
const isLoading = ref(false);
const datosGenerales = ref({});

const props = defineProps({
  asuntoNeunId: {
    type: Number,
    required: true,
  },
});

onMounted(async () => {
  isLoading.value = true;
  try {
    // if (expedienteElectronicoStore.expediente[props.asuntoNeunId]?.datos) {
      const datos = await expedienteElectronicoStore.obtenerDatosGenerales(props.asuntoNeunId);
      datosGenerales.value = datos;
    // }
    datosGenerales.value = expedienteElectronicoStore.expediente[props.asuntoNeunId]?.datos;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  isLoading.value = false;
});

</script>
