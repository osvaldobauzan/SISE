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
            @click="goSentencias"
          >
            Proyectos
          </q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="donut"
            height="280"
            :options="chartOptionsDonutAcuerdos"
            :series="seriesDonutAcuerdos"
            @dataPointSelection="clickHandler"
            ref="donutSentencias"
          >
          </apexchart>
        </q-card-section>
      </div>
      <div
        class="col-sm-12 col-md-3"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-toolbar>
          <q-toolbar-title> Sentencias </q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="bar"
            height="280"
            :options="optionsTipoAcuerdos"
            :series="seriesTipoAcuerdos"
          ></apexchart>
        </q-card-section>
      </div>
      <div
        class="col-sm-12 col-md-4"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-toolbar>
          <q-toolbar-title> Proyectos por secretario </q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="bar"
            height="265"
            :options="chartOptionsSecretarios"
            :series="seriesSecretarios"
            ref="barSecretarios"
            @dataPointSelection="clickSecretarios"
          ></apexchart>
        </q-card-section>
      </div>
    </div>
  </q-card>
  <TabsProfileSentencias></TabsProfileSentencias>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

import TabsProfileSentencias from "./tabsProfileSentencias.vue";

const router = new useRouter();
const barSecretarios = ref(null);
const donutSentencias = ref(null);
const seriesSecretarios = ref([]);

function goSentencias() {
  router.push("sentencias");
}

const seriesDonutAcuerdos = [10, 3, 7, 10];

seriesSecretarios.value = [
  {
    name: "Sin proyecto",
    data: [4, 5, 7],
  },
  {
    name: "Para revisión",
    data: [2, 1, 4],
  },
  {
    name: "Con ajustes",
    data: [5, 3, 4],
  },
  {
    name: "Autorizados",
    data: [4, 6, 3],
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
            label: "Audiencias / Turno",
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
  colors: ["#EF9A9A", "#F3E5F5", "#FFCC80", "#A5D6A7"],
  labels: ["Sin proyecto", "Para revisión", "Con ajustes", "Autorizados"],
};

const seriesTipoAcuerdos = [
  {
    name: "Cantidad",
    data: [3, 7],
  },
];

const optionsTipoAcuerdos = {
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
    toolbar: {
      show: false,
    },
  },
  colors: ["#90CAF9", "#A5D6A7"],
  labels: ["Preautorizado", "Autorizado"],
  plotOptions: {
    bar: {
      distributed: true,
      horizontal: false,
    },
  },
  dataLabels: {
    enabled: true,
  },
  xaxis: {
    categories: ["Preautorizado", "Autorizado"],
  },
};

const chartOptionsSecretarios = {
  chart: {
    type: "bar",
    height: 350,
    stacked: true,
    toolbar: {
      show: false,
    },
  },
  colors: ["#EF9A9A", "#F3E5F5", "#FFCC80", "#A5D6A7"],
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
    categories: ["GJuarezF", "JHernandezH", "OJuarezF"],
  },
  legend: {
    position: "right",
  },
  fill: {
    opacity: 1,
  },
};
function clickSecretarios(event, chartContext, config) {
  const selectedOption = config.w.config.labels[config.dataPointIndex];
  router.push("/sentencias/" + selectedOption);
}

function clickHandler(event, chartContext, config) {
  barSecretarios.value.hideSeries("Sin proyecto");
  barSecretarios.value.hideSeries("Para revisión");
  barSecretarios.value.hideSeries("Con ajustes");
  barSecretarios.value.hideSeries("Autorizados");
  barSecretarios.value.toggleSeries(
    config.w.config.labels[config.dataPointIndex],
  );
}

function showAllMesasBars() {
  barSecretarios.value.showSeries("Sin proyecto");
  barSecretarios.value.showSeries("Para revisión");
  barSecretarios.value.showSeries("Con ajustes");
  barSecretarios.value.showSeries("Autorizados");
}
</script>
