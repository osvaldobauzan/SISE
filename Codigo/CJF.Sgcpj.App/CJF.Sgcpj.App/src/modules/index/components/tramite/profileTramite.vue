<template>
  <q-card flat bordered class="bg-grey-1">
    <q-toolbar>
      <q-toolbar-title>{{ user.cargoDescripcion }} </q-toolbar-title>
    </q-toolbar>
    <q-toolbar class="justify-center">
      <q-card flat class="q-mr-xl bg-transparent">
        <q-item>
          <q-item-section side>
            <q-avatar size="60px">
              <q-img src="images/user-icon.png"> </q-img>
            </q-avatar>
          </q-item-section>
          <q-item-section class="text-left">
            <q-item-label class="text-h6">{{ user.fullName }}</q-item-label>
            <q-item-label caption> {{ user.displayName }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-card>

      <div class="row q-gutter-md q-my-xs">
        <q-card
          flat
          bordered
          class="bg-grey-3 text-center card-status-fixed-width"
          v-for="card in user.hoy"
          :key="card.title"
        >
          <q-card-section>
            <q-item-label class="text-bold"> {{ card.title }} </q-item-label>
            <q-item-label class="text-h5 text-bold">
              {{ card.value }}
              <span v-if="card.total" class="text-subtitle2">
                de {{ card.total }}</span
              ></q-item-label
            >
            <q-linear-progress
              rounded
              size="md"
              v-if="card.total"
              :value="card.value / card.total"
              color="green-3"
              class="bg-red-3 q-my-xs"
            >
            </q-linear-progress>
            <div v-if="!card.total" class="q-my-sm"></div>
            <q-item-label caption>{{ card.caption }}</q-item-label>
          </q-card-section>
        </q-card>
        <q-card flat bordered class="bg-grey-3">
          <div class="text-center text-bold q-my-sm">{{ user.titleCards }}</div>
          <div class="row">
            <q-item
              v-for="card in user.cards"
              :key="card.title"
              class="text-center"
              style="border-right: 1px dashed; border-color: #c0c0c0"
            >
              <q-item-section>
                <q-item-label class="text-bold">
                  {{ card.title }}
                </q-item-label>
                <q-item-label class="text-h5 text-bold">
                  {{ card.value }}</q-item-label
                >
                <q-item-label caption>{{ card.caption }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
        </q-card>
      </div>
    </q-toolbar>
    <q-card-section>
      <div class="row">
        <apexchart
          type="rangeBar"
          height="320"
          style="width: 100%"
          :options="chartOptionsTiempoRange"
          :series="seriesTiempoRange"
        ></apexchart>
      </div>
      <div class="row q-gutter-lg q-my-xl">
        <div class="col">
          <apexchart
            type="bar"
            height="265"
            :options="chartOptionsAcuerdos"
            :series="seriesAcuerdos"
            ref="barMesas"
          >
          </apexchart>
        </div>
        <div class="col">
          <apexchart
            type="line"
            height="350"
            class="fit"
            :options="chartOptionsTiempo"
            :series="seriesTiempo"
          >
          </apexchart>
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script setup>
const props = defineProps({
  user: {
    type: Object,
    required: true,
  },
});

const chartOptionsTiempoRange = {
  chart: {
    height: 350,
    type: "rangeBar",
    zoom: {
      enabled: false,
    },
    toolbar: {
      show: false,
    },
  },
  plotOptions: {
    bar: {
      isDumbbell: true,
      columnWidth: 3,
      dumbbellColors: [["#EF9A9A", "#A5D6A7"]],
    },
  },
  legend: {
    show: true,
    showForSingleSeries: true,
    position: "top",
    horizontalAlign: "right",
    customLegendItems: ["Recepción", "Turno"],
  },
  colors: ["#EF9A9A", "#A5D6A7"],
  fill: {
    type: "gradient",
    gradient: {
      type: "vertical",
      gradientToColors: ["#A5D6A7"],
      inverseColors: true,
      stops: [0, 100],
    },
  },
  title: {
    text: props.user?.titleTiempo,
    align: "left",
  },
  grid: {
    xaxis: {
      lines: {
        show: false,
      },
    },
    yaxis: {
      lines: {
        show: true,
      },
    },
  },
  xaxis: {
    title: {
      text: "Número de promoción",
    },
  },
};
const seriesTiempoRange = [
  {
    data: [
      {
        x: "805",
        y: [9.1, 9.35],
      },
      {
        x: "806",
        y: [9.2, 9.25],
      },
      {
        x: "807",
        y: [9.1, 9.55],
      },
      {
        x: "808",
        y: [9.0, 9.45],
      },
      {
        x: "809",
        y: [9.1, 10.15],
      },
      {
        x: "810",
        y: [9.55, 10.1],
      },
      {
        x: "811",
        y: [10.11, 10.45],
      },
      {
        x: "812",
        y: [10.12, 10.3],
      },
      {
        x: "813",
        y: [10.23, 10.45],
      },
      {
        x: "814",
        y: [10.24, 10.4],
      },
      {
        x: "815",
        y: [10.35, 10.45],
      },
      {
        x: "816",
        y: [10.36, 11.45],
      },
      {
        x: "817",
        y: [10.47, 11.5],
      },
      {
        x: "818",
        y: [10.48, 11.0],
      },
      {
        x: "819",
        y: [10.59, 11.0],
      },
      {
        x: "820",
        y: [10.51, 11.0],
      },
      {
        x: "821",
        y: [11.02, 12.16],
      },
      {
        x: "822",
        y: [11.03, 12.17],
      },
      {
        x: "823",
        y: [11.14, 12.38],
      },
      {
        x: "824",
        y: [11.15, 12.36],
      },
      {
        x: "825",
        y: [11.26, 12.35],
      },
      {
        x: "826",
        y: [11.27, 12.34],
      },
      {
        x: "827",
        y: [11.38, 12.44],
      },
      {
        x: "828",
        y: [11.39, 12.43],
      },
      {
        x: "829",
        y: [11.44, 12.52],
      },
      {
        x: "830",
        y: [11.43, 12.02],
      },
      {
        x: "831",
        y: [11.52, 12.01],
      },
      {
        x: "832",
        y: [11.56, 12.02],
      },
      {
        x: "833",
        y: [11.08, 12.13],
      },
      {
        x: "834",
        y: [11.08, 12.14],
      },
      {
        x: "835",
        y: [11.17, 12.35],
      },
      {
        x: "836",
        y: [11.16, 12.36],
      },
      {
        x: "837",
        y: [11.25, 12.37],
      },
      {
        x: "838",
        y: [11.24, 12.38],
      },
      {
        x: "839",
        y: [11.33, 12.49],
      },
      {
        x: "840",
        y: [11.32, 12.4],
      },
    ],
  },
];
const seriesAcuerdos = [
  {
    name: "Amparo Indirecto",
    data: [10, 15, 10, 15],
  },
  {
    name: "Causa penal",
    data: [2, 3, 2, 3],
  },
  {
    name: "Medidas precautorias",
    data: [4, 5, 4, 5],
  },
  {
    name: "Procedimientos de extradición",
    data: [8, 7, 8, 7],
  },
  {
    name: "Proceso civiles o administrativos",
    data: [8, 7, 8, 7],
  },
];

const chartOptionsAcuerdos = {
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
  colors: ["#EF9A9A", "#FFCC80", "#F3E5F5", "#BBDEFB", "#A5D6A7"],
  responsive: [
    {
      breakpoint: 480,
      options: {
        legend: {
          position: "bottom",
          offsetX: -10,
          offsetY: 0,
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
    categories: ["Ago", "Sep", "Oct", "Nov"],
  },
  title: {
    text: "Tipos de asunto",
    align: "left",
  },
  legend: {
    show: true,
    position: "bottom",
  },
  fill: {
    opacity: 1,
  },
};

const seriesTiempo = [
  {
    name: "Promedio",
    data: [22, 21, 24, 28, 27, 23, 23],
  },
];

const chartOptionsTiempo = {
  chart: {
    height: 350,
    type: "line",
    dropShadow: {
      enabled: true,
      color: "#000",
      top: 18,
      left: 7,
      blur: 10,
      opacity: 0.2,
    },
    toolbar: {
      show: false,
    },
  },
  colors: ["#64B5F6", "#FFB74D", "#A5D6A7"],
  dataLabels: {
    enabled: true,
  },
  stroke: {
    curve: "smooth",
  },
  title: {
    text: props.user?.titleTiempo,
    align: "left",
  },
  grid: {
    borderColor: "#e7e7e7",
    row: {
      colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
      opacity: 0.5,
    },
  },
  markers: {
    size: 1,
  },
  xaxis: {
    categories: [
      "Ene",
      "Feb",
      "Mar",
      "Abr",
      "May",
      "Jun",
      "Jul",
      "Ago",
      "Sep",
      "Oct",
      "Nov",
      "Dic",
    ],
  },
  yaxis: {
    title: {
      text: "Minutos",
    },
    min: 5,
    max: 60,
  },
  legend: {
    horizontalAlign: "center",
  },
};
</script>
