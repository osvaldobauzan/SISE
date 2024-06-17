<template>
  <div class="q-gutter-md">
    <div class="row">
      <div
        class="col-sm-12 col-md-4"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-card bordered class="text-center">
          <q-card-section>
            <div class="text-h4">10</div>
            <q-item>
              <q-item-section side>
                <q-icon name="mdi-alert" color="negative"></q-icon>
              </q-item-section>
              <q-item-section>
                <div class="text-subtitle1">
                  Sin requerimiento de cumplimiento
                </div>
              </q-item-section>
            </q-item>
          </q-card-section>
        </q-card>
      </div>
      <div
        class="col-sm-12 col-md-4"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-card bordered class="text-center">
          <q-card-section>
            <div class="text-h4">34</div>
            <q-item>
              <q-item-section side>
                <q-icon name="mdi-alert" color="negative"></q-icon>
              </q-item-section>
              <q-item-section>
                <div class="text-subtitle1">Sin desahogo de requerimiento</div>
              </q-item-section>
            </q-item>
          </q-card-section>
        </q-card>
      </div>
      <div
        class="col-sm-12 col-md-4"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-card bordered class="text-center">
          <q-card-section>
            <div class="text-h4">14</div>
            <q-item>
              <q-item-section side>
                <q-icon name="mdi-alert" color="negative"></q-icon>
              </q-item-section>
              <q-item-section>
                <div class="text-subtitle1">Sin acuerdo de cumplimiento</div>
              </q-item-section>
            </q-item>
          </q-card-section>
        </q-card>
      </div>
    </div>
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
              @click="goEjecucion"
            >
              Ejecución
            </q-toolbar-title>
          </q-toolbar>
          <q-card-section>
            <apexchart
              type="donut"
              height="280"
              :options="chartOptionsDonutAcuerdos"
              :series="seriesDonutAcuerdos"
              @dataPointSelection="clickHandler"
              ref="donutEjecucion"
            >
            </apexchart>
          </q-card-section>
        </div>
        <div
          class="col-sm-12 col-md-4"
          :class="!$q.screen.gt.xs ? 'full-width' : ''"
        >
          <q-toolbar>
            <q-toolbar-title> Ejecución por secretario </q-toolbar-title>
          </q-toolbar>
          <q-card-section>
            <apexchart
              type="bar"
              height="265"
              :options="chartOptionsSecretarios"
              :series="seriesSecretarios"
              ref="barMesas"
              @dataPointSelection="clickEjecucion"
            ></apexchart>
          </q-card-section>
        </div>
      </div>
    </q-card>
    <TabsProfileEjecucion></TabsProfileEjecucion>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import TabsProfileEjecucion from "./tabsProfileEjecucion.vue";

const router = new useRouter();
const barMesas = ref(null);
const donutEjecucion = ref(null);
const seriesSecretarios = ref([]);
const seriesDonutAcuerdos = ref([]);
function goEjecucion() {
  router.push("ejecucion");
}

seriesDonutAcuerdos.value = [6, 2, 1];

seriesSecretarios.value = [
  {
    name: "En vías de cumplimiento",
    data: [3, 3],
  },
  {
    name: "No cumplido",
    data: [1, 1],
  },
  {
    name: "Cumplida",
    data: [1, 0],
  },
];

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
  chart: {
    height: 390,
    type: "donut",
  },
  colors: ["#CFD8DB", "#EF9A9A", "#A5D6A7"],
  labels: ["En vías de cumplimiento", "No cumplida", "Cumplida"],
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
            label: "Sentencias",
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
};

const chartOptionsSecretarios = {
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
  },
  colors: ["#CFD8DB", "#EF9A9A", "#A5D6A7"],
  labels: ["En vías de cumplimiento", "No cumplida", "Cumplida"],
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
  dataLabels: {
    enabled: true,
  },
  xaxis: {
    type: "text",
    categories: ["GJuarezF", "JHernandezH"],
  },
  legend: {
    show: true,
    position: "right",
    offsetY: 40,
  },
  fill: {
    opacity: 1,
  },
};

function clickEjecucion(event, chartContext, config) {
  const selectedOption = config.w.config.labels[config.dataPointIndex];
  router.push("/ejecucion/" + selectedOption);
}

function clickHandler(event, chartContext, config) {
  barMesas.value.hideSeries("En vías de cumplimiento");
  barMesas.value.hideSeries("No cumplida");
  barMesas.value.hideSeries("Cumplida");
  barMesas.value.toggleSeries(config.w.config.labels[config.dataPointIndex]);
}

function showAllMesasBars() {
  barMesas.value.showSeries("En vías de cumplimiento");
  barMesas.value.showSeries("No cumplida");
  barMesas.value.showSeries("Cumplida");
}
</script>
