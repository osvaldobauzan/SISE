<template>
  <q-card>
    <div class="row">
      <div
        class="col-sm-12 col-md-5"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-toolbar>
          <q-btn
            flat
            round
            icon="mdi-refresh"
            @click="showAllMesasBars"
          ></q-btn>
          <q-toolbar-title
            class="text-secondary underline cursor-pointer"
            @click="goTramite"
          >
            Trámite
          </q-toolbar-title>
        </q-toolbar>
        <q-card-section v-if="props.rowsTramite.length > 0">
          <apexchart
            type="donut"
            height="280"
            :options="chartOptionsDonutAcuerdos"
            :series="seriesDonutAcuerdos"
            @dataPointSelection="clickHandler"
            ref="donutTramite"
          >
          </apexchart>
        </q-card-section>
        <q-card-section v-else>
          <div
            class="column items-center justify-center"
            style="min-height: 20vh; min-width: 100%"
          >
            <q-icon size="6em" :name="'mdi-file'" color="grey-6 q-mb-lg" />
            <div class="text-h4 text-secondary text-bold q-mb-md">
              Sin Trámite
            </div>
          </div>
        </q-card-section>
      </div>
      <div
        class="col-sm-12 col-md-7"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-toolbar>
          <q-toolbar-title>Mesas</q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="bar"
            height="265"
            :options="chartOptionsAcuerdos"
            :series="seriesAcuerdos"
            ref="barMesas"
            @dataPointSelection="clickMesas"
          >
          </apexchart>
        </q-card-section>
      </div>
      <q-inner-loading :showing="isLoading"></q-inner-loading>
    </div>
  </q-card>
  <!-- TODO: falta implementar por eso se oculta -->
  <TabsProfileTramite v-if="false"></TabsProfileTramite>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { useTramiteStore } from "/src/modules/tramite/store/tramite-store.js";
import { useUsuariosStore } from "src/stores/usuarios-store";
import { manejoErrores } from "src/helpers/manejo-errores";

const tramiteStore = useTramiteStore();
const usuariosStore = useUsuariosStore();

import TabsProfileTramite from "./tabsProfileTramite.vue";
const barMesas = ref(null);
const donutTramite = ref(null);
const isLoading = ref(false);
const router = new useRouter();

const props = defineProps({
  rowsTramite: {
    default: [],
  },
});

const secretariosUsuarios = computed(() => {
  return usuariosStore.secretarios;
});

function getSecretarioID(mesa) {
  const secretario = secretariosUsuarios.value.find((e) => e.area === mesa);
  return secretario ? secretario.empleadoId : null;
}

async function getSecretarios() {
  try {
    await usuariosStore.obtenerSecretarios();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

onMounted(async () => {
  await getSecretarios();
});

const seriesAcuerdos = computed(() => {
  return [
    {
      name: "Sin Acuerdo",
      data: calcularTramites(1),
    },
    {
      name: "Cancelados",
      data: calcularTramites(5),
    },
    {
      name: "Con Acuerdo",
      data: calcularTramites(2),
    },
    {
      name: "Preautorizados",
      data: calcularTramites(3),
    },
    {
      name: "Autorizados",
      data: calcularTramites(4),
    },
  ];
});

const mesasArray = computed(() => {
  let arr = noSortedArray;
  const sorted = arr.value.sort((a, b) => {
    return a.localeCompare(b, undefined, {
      numeric: true,
      sensitivity: "base",
    });
  });
  return sorted;
});

const noSortedArray = computed(() => {
  let array = [];
  props.rowsTramite.forEach((e) => {
    if (!array.includes(e.mesa)) {
      array.push(e.mesa);
    }
  });
  return array;
});

function calcularEstadoAcuerdo(num) {
  let i = 0;
  props.rowsTramite.forEach((e) => {
    if (parseInt(e.estado) === num) {
      i++;
    }
  });
  return i;
}

function calcularTramites(numEstado) {
  let array = Array.from({ length: mesasArray.value.length }, () => 0);
  mesasArray.value.forEach((mesa, index) => {
    props.rowsTramite.forEach((e) => {
      if (parseInt(e.estado) === numEstado && e.mesa === mesa) {
        array[index]++;
      }
    });
  });
  return array;
}

function goTramite() {
  router.push("tramite");
}

const seriesDonutAcuerdos = computed(() => {
  return [
    calcularEstadoAcuerdo(1),
    calcularEstadoAcuerdo(5),
    calcularEstadoAcuerdo(2),
    calcularEstadoAcuerdo(3),
    calcularEstadoAcuerdo(4),
  ];
});

const chartOptionsDonutAcuerdos = {
  responsive: [
    {
      breakpoint: 1000,
      options: {
        legend: {
          position: "bottom",
        },
      },
    },
  ],
  dataLabels: {
    enabled: true,
    dropShadow: {
      enabled: false,
    },
    style: {
      fontSize: "12px",
      colors: ["#755"],
    },
  },
  tooltip: {
    enabled: true,
    fillSeriesColor: false,
  },
  plotOptions: {
    pie: {
      donut: {
        labels: {
          show: true,
          name: {
            show: true,
            fontSize: "22px",
            fontFamily: "Helvetica, Arial, sans-serif",
            fontWeight: 600,
            color: undefined,
            offsetY: 30,
          },
          total: {
            show: true,
            showAlways: true,
            label: "Acuerdos",
            fontSize: "16px",
            fontFamily: "Helvetica, Arial, sans-serif",
            color: "#373d3f",
            formatter: function (w) {
              return w.globals.seriesTotals.reduce((a, b) => {
                return a + b;
              }, 0);
            },
          },
          value: {
            show: true,
            fontSize: "40px",
            fontFamily: "Helvetica, Arial, sans-serif",
            fontWeight: 800,
            color: undefined,
            offsetY: -15,

            formatter: function (val) {
              return val;
            },
          },
        },
      },
    },
  },
  chart: {
    height: 390,
    type: "donut",
  },
  colors: ["#EF9A9A", "#FFCC80", "#E0BEE7", "#BBDEFB", "#A5D6A7"],
  labels: [
    "Sin Acuerdo",
    "Cancelados",
    "Con Acuerdo",
    "Preautorizados",
    "Autorizados",
  ],
};

const chartOptionsAcuerdos = computed(() => {
  return {
    responsive: [
      {
        breakpoint: 1000,
        options: {
          legend: {
            position: "bottom",
          },
        },
      },
    ],
    chart: {
      type: "bar",
      height: 350,
      stacked: true,
      toolbar: {
        show: false,
      },
      zoom: {
        enabled: true,
      },
    },
    colors: ["#EF9A9A", "#FFCC80", "#E0BEE7", "#BBDEFB", "#A5D6A7"],
    plotOptions: {
      bar: {
        horizontal: false,
        borderRadius: 0,
        borderRadiusApplication: "end", // 'around', 'end'
        borderRadiusWhenStacked: "last", // 'all', 'last'
        dataLabels: {
          total: {
            enabled: true,
            style: {
              fontSize: "13px",
              fontWeight: 900,
            },
          },
        },
      },
    },
    xaxis: {
      type: "text",
      categories: mesasArray.value,
    },
    legend: {
      position: "right",
    },
    fill: {
      opacity: 1,
    },
    yaxis: {
      labels: {
        formatter: (value) => {
          return `${value}`.includes(".") && typeof value == "number"
            ? Number.parseFloat(value).toFixed(1)
            : value;
        },
      },
    },
  };
});

function clickMesas(event, chartContext, config) {
  const selectedOption = {
    secretarioId: getSecretarioID(
      config.w.config.xaxis.categories[config.dataPointIndex],
    ),
    estado: getState(config.seriesIndex),
  };
  tramiteStore.dashboardSelection = selectedOption;
  // const selectedOption = config.w.config.labels[config.dataPointIndex];
  router.push("tramite/");
}
function getState(val) {
  if (val === 1) {
    return 5;
  } else if (val === 0) {
    return 1;
  }
  return val;
}
function clickHandler(event, chartContext, config) {
  barMesas.value.hideSeries("Sin Acuerdo");
  barMesas.value.hideSeries("Cancelados");
  barMesas.value.hideSeries("Con Acuerdo");
  barMesas.value.hideSeries("Preautorizados");
  barMesas.value.hideSeries("Autorizados");
  barMesas.value.toggleSeries(config.w.config.labels[config.dataPointIndex]);
}

function showAllMesasBars() {
  barMesas.value.showSeries("Sin Acuerdo");
  barMesas.value.showSeries("Cancelados");
  barMesas.value.showSeries("Con Acuerdo");
  barMesas.value.showSeries("Preautorizados");
  barMesas.value.showSeries("Autorizados");
}
</script>
