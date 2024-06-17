<template>
  <q-card flat>
    <q-toolbar class="justify-center">
      <q-card flat class="q-mr-xl">
        <q-item>
          <q-item-section side>
            <q-avatar size="60px">
              <q-img src="images/user-icon.png"> </q-img>
            </q-avatar>
          </q-item-section>
          <q-item-section class="text-left">
            <q-item-label class="text-caption text-grey">{{
              user.cargoDescripcion
            }}</q-item-label>
            <q-item-label class="text-h6">{{ user.fullName }}</q-item-label>
            <q-item-label caption>{{ user.displayName }}</q-item-label>
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
      <div class="row q-gutter-lg q-my-xl">
        <div class="col">
          <apexchart
            type="line"
            height="350"
            class="fit"
            :options="chartOptions"
            :series="series"
          ></apexchart>
        </div>
        <q-separator vertical></q-separator>
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
defineProps({
  user: {
    type: Object,
    required: true,
  },
});

const series = [
  {
    name: "Proyectos",
    type: "column",
    data: [14, 19, 17, 16, 14, 15, 10, 18, 16, 19, 16, 0],
  },
];

const chartOptions = {
  chart: {
    height: 350,
    type: "bar",
    stacked: false,
    toolbar: {
      show: false,
    },
  },
  colors: ["#64B5F6", "#757575"],
  title: {
    text: "Proyectos entregados",
    align: "left",
  },

  dataLabels: {
    enabled: false,
  },
  stroke: {
    width: [1, 1],
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
  tooltip: {
    fixed: {
      enabled: true,
      position: "topLeft", // topRight, topLeft, bottomRight, bottomLeft
      offsetY: 30,
      offsetX: 60,
    },
  },
  yaxis: {
    title: {
      text: "Proyectos",
    },
    min: 1,
    max: 30,
  },
  legend: {
    horizontalAlign: "center",
  },
};

const seriesTiempo = [
  {
    name: "Proyectos",
    type: "column",
    data: [5, 3, 6, 5],
  },
  {
    name: "Días promedio por proyecto",
    type: "line",
    data: [3, 2, 3, 2],
  },
  {
    name: "Versiones promedio por proyecto",
    type: "line",
    data: [5, 4, 5, 4],
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
  colors: ["#64B5F6", "#FFB74D", "#757575"],
  title: {
    text: "Detalles del mes - Octubre",
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
    categories: ["Semana 1", "Semana 2", "Semana 3", "Semana 4"],
  },
  yaxis: [
    {
      show: false,
      axisTicks: {
        show: false,
      },
      axisBorder: {
        show: false,
        color: "#757575",
      },
      labels: {
        style: {
          colors: "#757575",
        },
      },
      title: {
        text: "Versiones promedio por proyecto",
        style: {
          color: "#757575",
        },
      },
      tooltip: {
        enabled: true,
      },
      min: 1,
      max: 10,
    },
    {
      axisTicks: {
        show: false,
      },
      title: {
        text: "Proyectos y versiones",
      },
      tooltip: {
        enabled: true,
      },
      min: 1,
      max: 10,
    },
    {
      seriesName: "Días promedio por proyecto",
      opposite: true,
      axisTicks: {
        show: true,
      },
      axisBorder: {
        show: true,
        color: "#FEB019",
      },
      labels: {
        style: {
          colors: "#FEB019",
        },
      },
      title: {
        text: "Días promedio por proyecto",
        style: {
          color: "#FEB019",
        },
      },
      min: 1,
      max: 10,
    },
  ],
  legend: {
    horizontalAlign: "center",
  },
};
</script>
