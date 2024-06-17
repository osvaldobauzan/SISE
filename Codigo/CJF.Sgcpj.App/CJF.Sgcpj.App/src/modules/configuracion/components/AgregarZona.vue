<template>
  <q-card style="width: 600px">
    <q-toolbar>
      <q-toolbar-title>{{ title }} zona</q-toolbar-title>
      <q-btn flat round icon="mdi-close" v-close-popup></q-btn>
    </q-toolbar>
    <q-card-section class="q-gutter-md">
      <q-input
        filled
        v-model="zonaLocal.nombre"
        label="Indica el nombre de la zona"
      />
      <GoogleMap
        :api-key="GOOGLE_MAPS_API_KEY"
        style="width: 100%; height: 500px"
        :center="currentPosition"
        :zoom="title === 'Agregar' ? 15 : 11"
      >
        <Polygon :options="defaultPoligon" />
      </GoogleMap>
    </q-card-section>
    <q-card-actions align="right">
      <q-btn label="Guardar" color="primary" v-close-popup />
      <q-btn flat outlined label="Cerrar" color="primary" v-close-popup />
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { GoogleMap, Polygon } from "vue3-google-map";

const currentPosition = ref({ lat: 19.43637, lng: -99.13246 });
const defaultPoligon = ref({});
const props = defineProps({
  title: {
    type: String,
    default: "Agregar",
  },
  zona: {
    type: Object,
    required: true,
  },
});

const triangleCoords = [
  { lat: 19.44103, lng: -99.14285 },
  { lat: 19.43217, lng: -99.14298 },
  { lat: 19.43197, lng: -99.12397 },
  { lat: 19.4408, lng: -99.12389 },
  { lat: 19.44103, lng: -99.14285 },
];
defaultPoligon.value = {
  paths: triangleCoords,
  strokeColor: "#FF0000",
  strokeOpacity: 0.8,
  strokeWeight: 2,
  fillColor: "#FF0000",
  fillOpacity: 0.35,
  editable: true,
  draggable: true,
};

const zonaLocal = ref({
  nombre: "",
  center: currentPosition.value,
  poligono: defaultPoligon,
});
onMounted(() => {
  if (props.title === "Agregar") {
    navigator.geolocation.getCurrentPosition((position) => {
      currentPosition.value = {
        lat: position.coords.latitude,
        lng: position.coords.longitude,
      };
    });
  } else {
    zonaLocal.value = props.zona;
    currentPosition.value = props.zona.center;
    defaultPoligon.value = {
      paths: props.zona.poligono || triangleCoords,
      strokeColor: props.zona.color,
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: props.zona.color,
      fillOpacity: 0.35,
    };
  }
});
</script>
