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
            <q-item-label>{{ user.cargoDescripcion }}</q-item-label>
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
    <q-card-section >
      <div class="row">
        <apexchart
          type="rangeBar"
          height="320"
          style="width: 100%"
          :options="chartOptionsTiempo"
          :series="seriesTiempo"
           ref="rangeBarTime"
        ></apexchart>
      </div>
      <div class="row">
        <apexchart
          type="bar"
          height="265"
          style="width: 100%"
          :options="chartOptionsAcuerdos"
          :series="newSeriesAcuerdos"
          ref="barMesas"
        >
        </apexchart>
      </div>
    </q-card-section>
  </q-card>
</template>
<script setup>

import { ref, onMounted } from 'vue';

const props = defineProps({
  user: {
    type: Object,
    required: true,
  }
});
 
const rangeBarTime = ref(null);

const seriesTiempo = ref([
      {
        data: []
      }
    ]);

    const newSeriesAcuerdos = ref([]);
 
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

onMounted(() =>
{
  if (props.user && props.user.seriesTiempo !== undefined) {
     updateSeriesLine(props.user.seriesTiempo);
     newSeriesAcuerdos.value = props.user.newSeriesAcuerdos;
  }
});

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
    categories: props.user && props.user.dynamicLabels ? props.user.dynamicLabels : [],
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

const chartOptionsTiempo = {
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
    text: "Promociones capturadas",
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
</script>
