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
            <!-- <q-item-label>{{ user.cargoDescripcion }}</q-item-label> -->
            <q-item-label class="text-h6">{{ user.fullName }}</q-item-label>
            <q-item-label caption> {{ user.displayName }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-card>

      <div class="row q-gutter-md q-my-xs flex flex-center">
        <q-card
          flat
          bordered
          class="bg-grey-3"
          v-for="card in user.cards"
          :key="card.title"
        >
          <q-card-section>
            <q-item-label class="text-bold"> {{ card.title }} </q-item-label>
            <div v-if="!card.total" class="q-my-sm"></div>
            <q-item-label class="text-h5">
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
    name: "Acuerdos",
    type: "column",
    data: [14, 19, 17, 16, 14, 15, 10, 18, 16, 19, 16, 0],
  },
];

const seriesTiempo = [
  {
    name: "Acuerdos de cumplimiento",
    data: [22, 21, 24, 28, 27, 23, 23, 22, 21, 24, 28],
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
    text: "Acuerdos de cumplimiento",
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
      text: "Acuerdos",
    },
    min: 1,
    max: 30,
  },
  legend: {
    horizontalAlign: "center",
  },
};
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
    text: "Tiempo de revisión",
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
      text: "Días",
    },
    min: 5,
    max: 60,
  },
  legend: {
    horizontalAlign: "center",
  },
};
</script>
