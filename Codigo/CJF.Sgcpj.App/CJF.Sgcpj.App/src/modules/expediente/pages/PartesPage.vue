<template>
  <q-toolbar>
    <q-space></q-space>
    <q-btn
      no-caps
      flat
      label="Ficha técnica"
      icon="mdi-arrow-left"
      text-color="secundary"
      @click="back()"
    >
    </q-btn>
  </q-toolbar>
  <q-toolbar inner>
    <q-item>
      <q-item-section>
        <q-item-label class="text-h6 text-grey"
          >Expediente {{ expedienteSeleccionado.asuntoAlias }}</q-item-label
        >
        <q-item-label class="text-h5 text-bold">Partes</q-item-label>
      </q-item-section>
    </q-item>
    <q-space></q-space>
    <q-btn
      no-caps
      class="q-px-xl q-py-sm"
      label="Captura"
      icon="mdi-playlist-edit"
      color="secondary"
      @click="$router.replace('expedienteCaptura')"
    >
    </q-btn>
  </q-toolbar>
  <q-card class="q-ma-md">
    <q-toolbar>
      <q-item-label class="text-h6 text-bold">Datos generales</q-item-label>
    </q-toolbar>
    <q-item>
      <div class="col-2">
        <q-item-section>
          <q-item-label class="text-h6">{{
            formatoFecha(datosGenerales.fechaOCC)
          }}</q-item-label>
          <q-item-label caption>Presentación OCC</q-item-label>
        </q-item-section>
      </div>
      <div class="col-2">
        <q-item-section>
          <q-item-label class="text-h6">{{
            formatoFecha(datosGenerales.fechaOrg)
          }}</q-item-label>
          <q-item-label caption>Ingreso al órgano</q-item-label>
        </q-item-section>
      </div>
      <div class="col-2">
        <q-item-section>
          <q-item-label class="text-h6">{{
            datosGenerales.secretario
          }}</q-item-label>
          <q-item-label caption>Secretario</q-item-label>
        </q-item-section>
      </div>
      <div class="col">
        <q-item-section>
          <q-item-label class="text-h6">{{ datosGenerales.mesa }}</q-item-label>
          <q-item-label caption>Mesa</q-item-label>
        </q-item-section>
      </div>
    </q-item>
  </q-card>

  <div class="row q-ma-md">
    <div class="col-5">
      <q-card style="height: 100%">
        <q-toolbar>
          <q-toolbar-title v-if="totalPartes === 0">Sin Partes</q-toolbar-title>
          <q-toolbar-title v-else-if="totalPartes === 1">
            {{ totalPartes }} Parte</q-toolbar-title
          >

          <q-toolbar-title v-else>{{ totalPartes }} Partes</q-toolbar-title>
          <q-space></q-space>
          <q-input
            dense
            rounded
            outlined
            placeholder="Buscar"
            v-model="searchText"
          >
            <template v-slot:append>
              <q-icon name="mdi-magnify" />
            </template>
          </q-input>
        </q-toolbar>
        <q-separator></q-separator>
        <q-list>
          <q-item
            v-for="parte in filteredParts"
            :key="parte"
            clickable
            v-ripple
            @click="parteSeleccionadaInfo(parte)"
          >
            <q-item-section avatar>
              <q-icon color="grey-7" :name="parte.icon" />
            </q-item-section>
            <q-item-section>
              <q-item-label>{{
                parte.nombre + " " + parte.aPaterno + " " + parte.aMaterno
              }}</q-item-label>
              <q-item-label caption>{{
                parte.descripcionCaracterPersona
              }}</q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </q-card>
    </div>
    <div class="col q-ml-md">
      <q-card>
        <q-toolbar>
          <q-toolbar-title
            >Información de
            {{ (parteSeleccionada && parteSeleccionada.nombre) || "" }}
          </q-toolbar-title>
          <q-tooltip v-if="parteSeleccionada && parteSeleccionada.nombre">
            {{ parteSeleccionada.nombre }}
          </q-tooltip>
        </q-toolbar>
        <q-separator></q-separator>
        <q-expansion-item
          expand-separator
          default-opened
          v-for="(seccion, index) in datosParteSeleccionada"
          :key="index"
          :label="seccion.padreDescripcion"
          icon="mdi-view-list-outline"
          header-class="bg-grey-2"
        >
          <q-list>
            <q-item v-for="(dato, indexx) in seccion.datos" :key="indexx">
              <q-item-section>
                <q-item-label>{{ dato.valor }}</q-item-label>
                <q-item-label caption>{{ dato.descripcion }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </q-expansion-item>
      </q-card>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store";
import { useRouter } from "vue-router";
import { useOficialiaTabStore } from "src/modules/oficialia/stores/oficialia-tab-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { useTramiteTabStore } from "src/modules/tramite/store/tramite-tab-store";
import { useActuariaTabStore } from "src/modules/actuaria/stores/actuaria-tab-store";
import { manejoErrores } from "src/helpers/manejo-errores";

const tramiteTabStore = useTramiteTabStore();
const actuariaTabStore = useActuariaTabStore();
const oficialiaTabStore = useOficialiaTabStore();
const usuariosStore = useUsuariosStore();
const router = new useRouter();
const parteSeleccionada = ref();
const datosParteSeleccionada = ref();
const expedienteElectronicoStore = useExpedienteElectronicoStore();
const expedienteSeleccionado = ref({});
const partes = ref([]);
const searchText = ref("");
const datosGenerales = ref({});

const totalPartes = computed(() => {
  return partes.value?.length || null;
});

function formatoFecha(value) {
  if (value != undefined) {
    let aux = value.toString();
    var dateParts = aux.split("/");
    const fecha = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
    if (isValidDate(fecha)) {
      const dia = fecha.getDate().toString().padStart(2, "0");
      const mes = (fecha.getMonth() + 1).toString().padStart(2, "0");
      const año = fecha.getFullYear().toString();
      return `${dia}/${mes}/${año}`;
    } else {
      return "";
    }
  } else {
    return "";
  }
}

function isValidDate(d) {
  return d instanceof Date && !isNaN(d);
}

const filteredParts = computed(() => {
  if (searchText.value !== "") {
    const lowerSearchText = quitarAcentos(searchText.value.toLowerCase());
    return partes.value.filter(
      (parte) =>
        quitarAcentos(parte.nombre).toLowerCase().includes(lowerSearchText) ||
        parte.descripcionCaracterPersona
          .toLowerCase()
          .includes(lowerSearchText),
    );
  } else {
    return partes.value;
  }
});

function quitarAcentos(val) {
  return val.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}

const parametros = ref({
  asuntoNeunId: 0,
  personaId: 0,
});

onMounted(async () => {
  expedienteSeleccionado.value =
    expedienteElectronicoStore.expedienteSeleccionado;

  if (expedienteSeleccionado.value) {
    try {
      partes.value = await usuariosStore.obtenerParteExistente(
        expedienteSeleccionado.value.asuntoNeunId,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    if (partes.value) {
      parametros.value.asuntoNeunId = expedienteSeleccionado.value.asuntoNeunId;
      parametros.value.personaId = partes.value[0].personaId;
      parteSeleccionada.value = partes.value[0];
      obtenerInformacion();
    }
  }
});
function agrupaOrdenaDatos(capturaParte) {
  const datosAgrupados = capturaParte.reduce((acc, obj) => {
    const padre = obj.padre;
    if (
      !acc[padre] &&
      (obj.personaId === parametros.value.personaId || obj.personaId === 0)
    ) {
      acc[padre] = {
        padreOrden: obj.padreOrden,
        padreDescripcion: obj.padreDescripcion,
        datos: [],
      };
    }

    if (obj.personaId === parametros.value.personaId) {
      acc[padre].datos.push(obj);
    }
    return acc;
  }, {});

  const datosOrdenados = Object.values(datosAgrupados).sort((a, b) => {
    if (a.padreOrden !== b.padreOrden) {
      return a.padreOrden - b.padreOrden;
    }
    return a.padreDescripcion.localeCompare(b.padreDescripcion);
  });

  return datosOrdenados;
}

function parteSeleccionadaInfo(parte) {
  parametros.value.asuntoNeunId = expedienteSeleccionado.value.asuntoNeunId;
  parametros.value.personaId = parte.personaId;
  parteSeleccionada.value = parte;
  const informacionParteArray = expedienteElectronicoStore.parteInformacion;
  const objetoEncontrado = informacionParteArray.find(
    (objeto) =>
      objeto.asuntoNeunId === parametros.value.asuntoNeunId &&
      objeto.personaId === parametros.value.personaId,
  );

  if (objetoEncontrado) {
    datosParteSeleccionada.value = objetoEncontrado.datos;
  } else {
    obtenerInformacion();
  }
}
async function obtenerInformacion() {
  try {
    const resultado = await expedienteElectronicoStore.obtenerParteSeleccionada(
      parametros.value,
    );

    datosParteSeleccionada.value = agrupaOrdenaDatos(resultado);
    const datos = {
      asuntoNeunId: parametros.value.asuntoNeunId,
      personaId: parametros.value.personaId,
      datos: datosParteSeleccionada.value,
    };
    expedienteElectronicoStore.setInformacionParte(datos);
  } catch (error) {
    manejoErrores(error);
  }
}

function back() {
  switch (router.options.history.state.back) {
    case "/oficialia":
      oficialiaTabStore.setTabActive(
        expedienteElectronicoStore.expedienteSeleccionado.asuntoNeunId,
      );
      break;
    case "/tramite":
      tramiteTabStore.setTabActive(
        expedienteElectronicoStore.expedienteSeleccionado.asuntoNeunId,
      );
      break;
    case "/actuaria":
      actuariaTabStore.setTabActive(
        expedienteElectronicoStore.expedienteSeleccionado.asuntoNeunId,
      );
      break;

    default:
      break;
  }
  router.back();
}
</script>
