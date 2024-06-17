<template>
  <q-card style="min-width: 25vw">
    <q-toolbar>
      <q-toolbar-title class="text-bold">Detalle de la parte</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section>
      <q-list>
        <q-item>
          <q-item-section>
            <q-item-label caption>{{ parte.caracter }}</q-item-label>
            <q-item-label>{{ parte.parte }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label caption>Tipo de Notificación</q-item-label>
            <q-item-label>{{ parte.tipo }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label caption>Domicilio</q-item-label>
            <q-item-label>{{
              parte.domicilioParte || "Desconocido"
            }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-card-section>
  </q-card>
</template>

<script setup>
defineProps({
  parte: {
    type: Object,
    required: true,
  },
});
</script>
