<template>
  <div>
    <q-toolbar>
      <q-space></q-space>
      <q-btn
        unelevate
        icon="add"
        label="Agregar"
        @click="
          title = 'Agregar';
          zonaSelected = {};
          showAgregarZona = true;
        "
        color="primary"
      />
    </q-toolbar>
    <q-list bordered separator class="q-mt-lg">
      <q-item v-for="zona in zonas" :key="zona.id">
        <q-item-section>{{ zona.nombre }}</q-item-section>
        <q-item-section side>
          <div class="row">
            <q-btn
              flat
              round
              unelevate
              icon="mdi-pencil-outline"
              color="primary"
              @click="
                zonaSelected = zona;
                title = 'Editar';
                showAgregarZona = true;
              "
            />
            <q-btn
              flat
              round
              unelevate
              icon="mdi-delete-outline"
              color="negative"
            />
          </div>
        </q-item-section>
      </q-item>
    </q-list>
  </div>
  <q-dialog v-model="showAgregarZona">
    <AgregarZona :title="title" :zona="zonaSelected" />
  </q-dialog>
</template>

<script setup>
import { ref } from "vue";
import AgregarZona from "./AgregarZona.vue";

const showAgregarZona = ref(false);
const title = ref("Agregar");
const zonas = ref([
  {
    id: 1,
    nombre: "Zona Norte",
    color: "#FF0000",
    center: { lat: 19.47959, lng: -99.14059 },
    poligono: [
      { lat: 19.5927, lng: -99.1219 },
      { lat: 19.55024, lng: -99.16021 },
      { lat: 19.54441, lng: -99.15198 },
      { lat: 19.52727, lng: -99.15781 },
      { lat: 19.51497, lng: -99.20828 },
      { lat: 19.4593, lng: -99.23815 },
      { lat: 19.41155, lng: -99.19403 },
      { lat: 19.41737, lng: -99.1803 },
      { lat: 19.40264, lng: -99.16983 },
      { lat: 19.40879, lng: -99.0979 },
      { lat: 19.40183, lng: -99.05876 },
      { lat: 19.48455, lng: -99.0301 },
      { lat: 19.51982, lng: -99.12331 },
      { lat: 19.53762, lng: -99.12966 },
      { lat: 19.56415, lng: -99.10786 },
      { lat: 19.5927, lng: -99.1219 },
    ],
  },
  {
    id: 2,
    nombre: "Zona Sur",
    color: "#FF0000",
    center: { lat: 19.33765, lng: -99.1202 },
    poligono: [
      { lat: 19.41155, lng: -99.19403 },
      { lat: 19.32953, lng: -99.25764 },
      { lat: 19.25824, lng: -99.24528 },
      { lat: 19.25824, lng: -99.15464 },
      { lat: 19.17006, lng: -99.07087 },
      { lat: 19.20897, lng: -98.99122 },
      { lat: 19.33471, lng: -99.00908 },
      { lat: 19.40183, lng: -99.05876 },
      { lat: 19.40879, lng: -99.0979 },
      { lat: 19.40264, lng: -99.16983 },
      { lat: 19.41737, lng: -99.1803 },
      { lat: 19.41155, lng: -99.19403 },
    ],
  },
]);
</script>

<style>
.onmouseover {
  display: block;
}

.onmouseover:hover {
  display: none;
}
</style>
