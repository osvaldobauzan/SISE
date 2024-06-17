<template>
  <q-page padding>
    <q-toolbar>
      <q-toolbar-title>Configuración</q-toolbar-title>
    </q-toolbar>
    <q-tabs
      no-caps
      stretch
      switch-indicator
      v-model="tab"
      align="left"
      class="q-mt-lg bg-grey-4"
      active-class="bg-white"
      indicator-color="primary"
    >
      <q-tab name="zonas" label="Zonas" class="q-px-xl" />
      <q-tab name="areas" label="Áreas" class="q-px-xl" />
    </q-tabs>

    <q-tab-panels
      v-model="tab"
      animated
      swipeable
      transition-prev="jump-left"
      transition-next="jump-right"
    >
      <q-tab-panel name="areas">
        <AreasComponent />
      </q-tab-panel>
      <q-tab-panel name="zonas">
        <ZonasComponent />
      </q-tab-panel>
    </q-tab-panels>
  </q-page>
</template>

<script setup>
import { ref } from "vue";
import ZonasComponent from "../components/ZonasComponent.vue";
import AreasComponent from "../components/AreasComponent.vue";

const tab = ref("zonas");
</script>
