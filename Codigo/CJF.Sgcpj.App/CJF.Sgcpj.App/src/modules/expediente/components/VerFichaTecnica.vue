<template>
  <q-list>
    <div
      v-for="(item, index) in fichas?.sort((a, b) => a.orden - b.orden)"
      :key="index"
    >
      <q-item v-if="item.valor !== ''">
        <q-item-section side>
          <q-icon :name="item.icon"></q-icon>
        </q-item-section>
        <q-item-section v-if="item.valor !== ''">
          <q-item-label caption>{{ item.campo }}</q-item-label>
          <q-item-label>{{ item.valor }}</q-item-label>
        </q-item-section>
      </q-item>
    </div>
    <q-inner-loading :showing="isLoading"> </q-inner-loading>
  </q-list>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store.js";

const expedienteElectronicoStore = useExpedienteElectronicoStore();
const fichas = ref([]);
const isLoading = ref(false);

const props = defineProps({
  asuntoNeunId: {
    type: Number,
    required: true,
  },
});

onMounted(async () => {
  isLoading.value = true;
  try {
    // if (expedienteElectronicoStore.expediente[props.asuntoNeunId]?.ficha) {
    const ficha =
      await expedienteElectronicoStore.obtenerFichaTecnicaExpedienteElectronico(
        props.asuntoNeunId,
      );
    fichas.value = ficha;
    // }
    // fichas.value =
    //   expedienteElectronicoStore.expediente[props.asuntoNeunId]?.ficha;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  isLoading.value = false;
});
</script>
