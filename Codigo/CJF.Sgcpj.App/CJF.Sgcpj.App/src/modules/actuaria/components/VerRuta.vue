<template>
  <q-card style="width: 600px">
    <q-toolbar>
      <q-toolbar-title class="text-bold">Ver ruta</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section>
      <div class="row">
        <div class="col">
          <GoogleMap
            :api-key="GOOGLE_MAPS_API_KEY"
            style="width: 100%; height: 500px"
            :center="center"
            :zoom="11"
          >
            <Polyline :options="flightPath" />
            <MarkerCluster>
              <Marker
                v-for="(item, i) in selected"
                :options="{ position: item.location }"
                :key="i"
              >
                <InfoWindow>
                  <q-item>
                    <q-item-section>
                      <q-item-label class="text-bold">{{
                        item.Parte
                      }}</q-item-label>
                      <q-item-label caption>{{ item.Zona }}</q-item-label>
                      <q-item-label>
                        Tipo de Notificación:
                        {{ item.TipoNotificacion }}</q-item-label
                      >
                    </q-item-section>
                  </q-item>
                </InfoWindow>
              </Marker>
            </MarkerCluster>
          </GoogleMap>
        </div>
        <!-- <div class="col">
</div> -->
      </div>
    </q-card-section>
    <q-card-actions align="right">
      <q-btn
        flat
        no-caps
        unelevated
        color="primary"
        label="Ver en Google"
        @click="openGoogleMap"
      >
      </q-btn>
      <q-btn
        flat
        no-caps
        unelevated
        color="primary"
        label="Ruta"
        @click="getRuta"
      >
      </q-btn>
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { onMounted } from "vue";
import {
  GoogleMap,
  Marker,
  MarkerCluster,
  InfoWindow,
  Polyline,
} from "vue3-google-map";

const center = { lat: 19.340944282745152, lng: -99.19093366507741 };
const flightPath = ref([]);
const locations = ref([]);

const props = defineProps({
  selected: {
    type: Object,
    required: true,
  },
});

onMounted(() => {
  locations.value = props.selected;
  locations.value.push({
    location: { lat: 19.340944282745152, lng: -99.19093366507741 },
    label: "A",
    Parte: "Inicio",
    Zona: "Posición actual",
    TipoNotificacion: "Inicio de ruta",
  });

  // center.value = props.selected.location;
  const pathdecoded = decodePolyline(
    "kp`uBrgl|Qw@S_KqByFaA{Dg@_ACaAFcEh@{Gz@}@J^TLHVd@Vt@Jb@B^@~@Ix@?^_BnLy@fG}@bH?`@HRLPhAz@JVDV?TOx@sAbHKLOt@Cd@Nr@RfAq@KoCa@oDi@eEi@QFMDyC_@oASiBa@cBe@gAa@gAo@uA{@s@e@}@s@c@i@w@uAi@yAo@mCs@sCQs@cA_Ck@y@cC{CwA_BeFiGy@w@k@c@gAo@m@[k@UcA[kAUuAMmF_@wOy@g@?_@@u@Jg@Ns@ZsB`Am@Xy@VyAXo@FaB?cIa@cBNkBh@s@T{@PaABq@EmGe@oJc@sCIkB@oCJ}CHmCCiGGeD?y@Cw@IkCy@eAg@sB{@m@UgAa@uC{@gGgA}AUqHgA{HoAi@Ky@I{Ck@mDy@a@Im@C{@Bm@JoCz@oAb@uT|GuCbAmAt@}@l@i@h@uAnBg@d@c@Rg@Ne@Dm@?qBM{DQkBBiI_@w@Ai@Du@Ny@Z]T[VmAzA{BlCOPq@`@a@Ny@Lk@B}BO_UgBuDYaA?]@wARs@R{NtGqAl@aATu@JwA?sACiA?w@Dy@Ns@T]?oAj@gAh@c@Zo@l@i@n@o@lAWr@Qr@Mr@AZGf@GFCBARALKf@Wb@WVa@PeAd@u@`@uAbAy@n@k@b@o@f@u@l@s@l@qA`Bu@x@[Tu@R]Fk@@s@@ULUTIPQf@Il@Af@BZFz@Ad@P`@HXJb@@PH~@^nCJ\\Xz@b@|@b@t@pArBvCtEhAtBf@tAb@`Bf@rCv@~EpAzIrDlUNv@Nj@r@pAb@h@^Zt@`@j@Td@LbAJ~CGhCAvAB\\BrCf@vMzCfB`@HJXFZA\\HrA\\fBf@zAj@`A^h@Zz@b@rBnAlBrAXPnCfBrClB`BbA~Az@hChA~Ah@~C~@t@d@`@b@NTTp@Nv@@hAM`BOxAOzAQtA?X?^D^Ld@^bALVXVpAhAr@f@bClB?HDP?HGNuArBk@jAI`@@f@Np@Nr@?ZOd@]\\qBf@o@RWNQVUj@c@`@EHUDa@EE@O@{AJcBRg@RgAb@i@LyA@g@@i@Ja@Na@Vc@d@[p@Ib@Gt@H|@ZbApAvDvDfLXt@f@fA\\p@j@`Ap@`A`@d@nApA`@`@@N@NC^CJIPKHOHSB[IMIMQGSAUBUFQ|AmBZURGTAN?j@HjCf@^TLJL@`@NbB|@VPTVDHBQFQLOFA",
  );

  flightPath.value = {
    path: pathdecoded,
    geodesic: true,
    strokeColor: "blue",
    strokeOpacity: 0.5,
    strokeWeight: 5,
  };
});

function decodePolyline(encoded) {
  if (!encoded) {
    return [];
  }
  var poly = [];
  var index = 0,
    len = encoded.length;
  var lat = 0,
    lng = 0;

  while (index < len) {
    var b,
      shift = 0,
      result = 0;

    do {
      b = encoded.charCodeAt(index++) - 63;
      result = result | ((b & 0x1f) << shift);
      shift += 5;
    } while (b >= 0x20);

    var dlat = (result & 1) != 0 ? ~(result >> 1) : result >> 1;
    lat += dlat;

    shift = 0;
    result = 0;

    do {
      b = encoded.charCodeAt(index++) - 63;
      result = result | ((b & 0x1f) << shift);
      shift += 5;
    } while (b >= 0x20);

    var dlng = (result & 1) != 0 ? ~(result >> 1) : result >> 1;
    lng += dlng;

    var p = {
      lat: lat / 1e5,
      lng: lng / 1e5,
    };
    poly.push(p);
  }
  return poly;
}

function getRuta() {
  // const locations = props.selected.map((item) => (item.location));
  // const urlSuffix = props.selected[0].location;
  // const url = "https://maps.googleapis.com/maps/api/directions/json?origin=19.340944282745152,-99.19093366507741&destination=19.4050262275085,-99.24076960382367&key=" + process.env.GOOGLE_MAPS_API_KEY;
  // const url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + center.lat + "," + center.lng +
  //   "&destination=" + urlSuffix.lat + "," + urlSuffix.lng +
  //   "&key=" + GOOGLE_MAPS_API_KEY;
  // axios.get(url)
  //   .then(function (response) {
  // // handle success
  // console.log(response);
  //   })
  //   .catch(function (error) {
  // // handle error
  // console.log(error);
  //   })
  //   .finally(function () {
  // // always executed
  //   });
}
function openGoogleMap() {
  const locations = props.selected.map((item) => item.location);
  const urlSuffix = locations.join(",");

  window.open(
    "https://www.google.com/maps/search/?api=1&query=" + urlSuffix,
    "_blank",
  );
}
</script>
