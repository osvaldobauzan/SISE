<template>
  <q-toolbar class="bg-primary text-white">
    <q-toolbar-title class="text-bold"> Expediente {{ title }}</q-toolbar-title>
    <q-space></q-space>
    <q-item>
      <div class="text-h2">
        <q-btn
          flat
          round
          icon="mdi-close"
          color="white"
          @click="$router.push('expediente')"
          ><q-tooltip>Cerrar</q-tooltip>
        </q-btn>
      </div>
    </q-item>
  </q-toolbar>

  <q-card class="q-ma-md">
    <q-tab-panels v-model="tab" animated>
      <q-tab-panel name="tabVincular">
        <VincularExpediente></VincularExpediente>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>
<script setup>
import { ref } from "vue";

import VincularExpediente from "../components/VincularExpediente.vue";

const tab = ref("tabVincular");

const dataParam = JSON.parse(localStorage.getItem("expedienteSelect"));

const title = ref(
  dataParam ? `: ${dataParam.Expediente} (${dataParam.TipoAsunto})` : "",
);
</script>
