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
              color="red-3"
              class="bg-green-3 q-my-xs"
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
           ref="rangeBarTime"
        ></apexchart>
      </div>
      <div class="row q-gutter-lg q-my-xl">
        <div class="col">
          <apexchart
            type="line"
            height="350"
            class="fit"
            :options="chartOptions"
            :series="series"
            @dataPointSelection="clickHandler"
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

import { onMounted, ref} from "vue";

const props = defineProps({
  selectedDate: {
      from: '',
      to: '',
   },
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
    customLegendItems: ["Asignación", "Notificación"],
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
    text: "Notificaciones Realizadas",
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
      text: "Número de Expediente",
    },
  },
};
const seriesTiempoRange = ref([
      {
        data: []
      }
    ]);
const series = ref([
      {
        data: []
      }
    ]);

const seriesTiempo = ref([]);

const chartOptions = {
  chart: {
    height: 350,
    type: "bar",
    stacked: true,
    toolbar: {
      show: false,
    },
    zoom: {
    enabled: false,
    },
  },
  dataLabels: {
    enabled: false,
  },
  colors: ["#80CBC4", "#A5D6A7", "#C4E1A5", "#E5EE9B"],
  stroke: {
    width: [1, 1, 1, 1],
  },
  title: {
    text: "Notificaciones",
    align: "left",
  },
  xaxis: {
    categories: props.user && props.user.dynamicLabels ? props.user.dynamicMonths : []
  },
  tooltip: {
    intersect : true,
    shared: false,
    fixed: {
      enabled: false,
      position: "topLeft", // topRight, topLeft, bottomRight, bottomLeft
      offsetY: 30,
      offsetX: 60,
    },
    enabled: false,
  },
  legend: {
    horizontalAlign: "center",
  },
};

const chartOptionsTiempo = {
  chart: {
    height: 350,
    type: "bar",
    stacked: true,
    toolbar: {
      show: false,
    },
    zoom: {
    enabled: false,
    },
  },
  dataLabels: {
    enabled: false,
  },
  colors: ["#80CBC4", "#A5D6A7", "#C4E1A5", "#E5EE9B"],
  stroke: {
    width: [1, 1, 1, 1],
  },
  title: {
    text: "Semana",
    align: "left",
  },
  xaxis: {
    categories: ["Semana 1", "Semana 2", "Semana 3", "Semana 4", "Semana 5"],
  },
  tooltip: {
    fixed: {
      enabled: true,
      position: "topLeft",
      offsetY: 30,
      offsetX: 60,
    },
    enabled: false,
  },
  legend: {
    horizontalAlign: "center",
  },
};

import { useIndexStore } from "../../stores/index-store";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";

const authStore = useAuthStore();
const indexStore = useIndexStore();
const rangeBarTime = ref(null);
onMounted(() =>
{
  if (props.user && props.user.seriesTiempo !== undefined) {
  series.value = props.user.seriesMes;
  updateSeriesLine(props.user.seriesDias);
  }
});

function updateSeriesLine(data) {
      setTimeout(()=>{
        if(rangeBarTime.value)
        {
          rangeBarTime.value.updateSeries([{
           data: data[0].data,
      }], false, true);
        }else
        {
           updateSeriesLine(data);
        }
      },300);
    }


async function clickHandler(event, chartContext, config) {
  try {
    await indexStore.obtenerDesglosesSemanas(authStore.user.catOrganismoId, (props.user && props.user.actuarioId ?props.user.actuarioId : 0), props.selectedDate.from, props.selectedDate.to, (config.dataPointIndex + 1));

    // Inicializar la estructura para almacenar los datos de las series
    const auxSeriesTiempo = [];
    const weeks = Array(5).fill(0).map((_, index) => index + 1); // Asume 5 semanas

    // Iterar sobre los datos de detalle de semanas
    indexStore.detalleSemanas.forEach(x => {
      const weekIndex = weeks.indexOf(x.semana);
      if (weekIndex >= 0) {
        // Encontrar o crear una entrada para el tipo de asunto
        let cardTotal = auxSeriesTiempo.find(card => card.name === x.tipo);
        if (!cardTotal) {
          cardTotal = { name: x.tipo, type: "column", data: Array(5).fill(0) };
          auxSeriesTiempo.push(cardTotal);
        }
        // Asignar el valor total al índice correspondiente de la semana
        cardTotal.data[weekIndex] = x.total;
      }
    });

    seriesTiempo.value = auxSeriesTiempo;
  } catch (error) {
  }
}


</script>
