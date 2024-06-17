<template>
  <q-card style="width: 600px">
    <q-toolbar>
      <q-toolbar-title class="text-bold"
        >Detalle de la notificación</q-toolbar-title
      >
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-card-section class="q-gutter-sm">
      <q-list>
        <q-item>
          <q-item-section>
            <q-item-label caption>Expediente</q-item-label>
            <q-item-label class="text-bold">{{
              parteSelected.Expediente
            }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label caption>{{ parteSelected.Caracter }}</q-item-label>
            <q-item-label class="text-bold">{{
              parteSelected.Parte
            }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <GoogleMap
              :api-key="GOOGLE_MAPS_API_KEY"
              style="width: 100%; height: 500px"
              :center="center"
              :zoom="15"
            >
              <Marker
                :options="{ position: center, title: parteSelected.Parte }"
              />
            </GoogleMap>
          </q-item-section>
        </q-item>
      </q-list>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions class="justify-center">
      <q-btn
        no-caps
        unelevated
        class="q-mx-lg card-status-fixed-width"
        label="Notificado"
        color="positive"
      />
      <q-btn
        no-caps
        unelevated
        class="q-mx-lg card-status-fixed-width"
        label="En proceso"
        color="warning"
      />
      <q-space></q-space>
      <q-btn
        flat
        no-caps
        color="primary"
        label="Ver en Google"
        @click="openGoogleMap"
      >
      </q-btn>
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { GoogleMap, Marker } from "vue3-google-map";

const props = defineProps({
  parteSelected: {
    type: Object,
    required: true,
  },
});

const center = ref({ lat: 19.340129, lng: -99.190501 });

onMounted(() => {
  center.value = props.parteSelected.location;
});

function openGoogleMap() {
  window.open(
    process.env.URL_GOOGLE_MAPS +
      "?api=1&query=" +
      props.parteSelected.location.lat +
      "," +
      props.parteSelected.location.lng,
    "_blank",
  );
}
</script>
