<template>
  <q-toolbar class="bg-primary text-white">
    <q-toolbar-title class="text-bold">
      Expediente electrónico{{ title }}</q-toolbar-title
    >
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
  <q-toolbar inset class="bg-primary text-white">
    <q-item>
      <q-item-section>
        <q-item-label class="text-bold"> 21938287</q-item-label>
        <q-item-label caption class="bg-primary text-white"
          >Número de Expediente Único Nacional</q-item-label
        >
      </q-item-section>
    </q-item>
    <q-item>
      <q-item-section>
        <q-item-label class="text-bold"> 000000/0000</q-item-label>
        <q-item-label caption class="bg-primary text-white"
          >Numero de Control Oficina de Correspondencia Común:
        </q-item-label>
      </q-item-section>
    </q-item>

    <!--q-tabs v-model="tab" shrink>
      <q-tab name="tabCaptura" label="Captura" />
    </q-tabs-->
  </q-toolbar>
  <q-card class="q-ma-md">
    <q-tab-panels v-model="tab" animated>
      <q-tab-panel name="tabCaptura" style="padding: 0%">
        <CapturaPartes></CapturaPartes>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import CapturaPartes from "../components/CapturaPartes.vue";

const tab = ref("tabCaptura");

const dataParam = JSON.parse(localStorage.getItem("expedienteSelect"));

const title = ref(
  dataParam ? `: ${dataParam.Expediente} (${dataParam.TipoAsunto})` : "",
);
</script>
