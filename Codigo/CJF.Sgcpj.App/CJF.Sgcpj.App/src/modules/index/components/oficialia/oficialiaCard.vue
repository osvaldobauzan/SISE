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
            @click="goOficialia"
          >
            Oficialía de partes
          </q-toolbar-title>
        </q-toolbar>
        <q-card-section v-if="props.rowsOficialia.length > 0">
          <apexchart
            type="donut"
            height="280"
            :options="chartOptionsPromociones"
            :series="seriesPromociones"
            @dataPointSelection="clickHandler"
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
              Sin Promociones
            </div>
          </div>
        </q-card-section>
      </div>
      <div
        class="col-sm-12 col-md-7"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-toolbar>
          <q-toolbar-title> Tipo de asunto</q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="bar"
            height="280"
            :options="optionsTipoPromocionEscritoInicial"
            :series="seriesTipoPromocionEscrito"
            @dataPointSelection="handleChartClick"
            ref="refTipoPromocion"
          ></apexchart>
        </q-card-section>
      </div>
      <q-inner-loading :showing="isLoading"> </q-inner-loading>
    </div>
  </q-card>
 
  <TabsProfileOficialia :selectedDate="selectedDate"> </TabsProfileOficialia>
</template>

<script setup>
import { ref, computed } from "vue";
import { useRouter } from "vue-router";
import TabsProfileOficialia from "./tabsProfileOficialia.vue";
import { useOficialiaStore } from "src/modules/oficialia/stores/oficialia-store";

const isLoading = ref(false);
const seriesTipoPromocion = ref([]);
const router = new useRouter();
const refTipoPromocion = ref(null);
const oficialiaStore = useOficialiaStore();

const props = defineProps({
  rowsOficialia: {
    default: []
  },
  selectedDate: {
      from: '',
      to: '',
   }
});

const seriesPromociones = computed(() => {
  return [
    calcularPromociones(1),
    calcularPromociones(2),
    calcularPromociones(4),
  ];
});

function goOficialia() {
  router.push("oficialia");
}

function calcularPromociones(num) {
  let promociones = 0;
  props.rowsOficialia.forEach((e) => {
    if (e.estado === num) {
      promociones++;
    }
  });
  return promociones;
}

seriesTipoPromocion.value = [
  {
    name: "Cantidad",
    data: [40, 10],
  },
];

const labels = computed(() => {
  return [
    {
      name: "Todos",
      data: calcularTipoAsunto(0),
    },
    {
      name: "Electrónicas sin captura",
      data: calcularTipoAsunto(1),
    },
    {
      name: "Físicas sin archivo",
      data: calcularTipoAsunto(2),
    },
    {
      name: "Turnadas a mesa",
      data: calcularTipoAsunto(4),
    },
  ];
});

function showAllMesasBars() {
  refTipoPromocion.value.updateSeries([
    {
      data: calcularTipoAsunto(0),
    },
  ]);
}

function clickHandler(event, chartContext, config) {
  const selectedOption = config.w.config.labels[config.dataPointIndex];
  const newValues = labels.value.filter((e) => e.name === selectedOption)[0]
    .data;
  refTipoPromocion.value.updateSeries([
    {
      data: newValues,
    },
  ]);
}
const seriesTipoPromocionEscrito = computed(() => {
  return [
    {
      name: "",
      data: calcularTipoAsunto(0),
    },
  ];
});

const chartOptionsPromociones = {
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
            color: "#000",
            offsetY: 30,
          },
          total: {
            show: true,
            showAlways: true,
            label: "Promociones",
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
  colors: ["#90CAF9", "#FFF59D", "#A5D6A7"],
  labels: ["Electrónicas sin captura", "Físicas sin archivo", "Turnadas a mesa"]
};

function calcularTipoAsunto(numEstado) {
  const optionLabel = optionsLabelTipoAsunto();
  let array = Array.from({ length: optionLabel.length }, () => 0);
  optionLabel.forEach((tipoAsunto, index) => {
    props.rowsOficialia.forEach((e) => {
      if (
        parseInt(e.estado) === numEstado &&
        e.expediente.catTipoAsunto === tipoAsunto
      ) {
        array[index]++;
      } else if (numEstado === 0 && e.expediente.catTipoAsunto === tipoAsunto) {
        array[index]++;
      }
    });
  });
  return array;
}

const optionsLabelTipoAsunto = () => {
  let array = [];
  props.rowsOficialia.forEach((e) => {
    if (e.expediente.catTipoAsunto === null) {
      e.expediente.catTipoAsunto = "Sin Registro";
    }
    if (!array.includes(e.expediente.catTipoAsunto)) {
      array.push(e.expediente.catTipoAsunto);
    }
  });
  array = array.sort((a, b) => {
    return a.localeCompare(b, undefined, {
      numeric: true,
      sensitivity: "base",
    });
  });
  return array;
};
function handleChartClick(event, chartContext, config) {
  const selectedOption = config.w.config.labels[config.dataPointIndex];
  oficialiaStore.actualizaTextoBuscar(selectedOption);
  router.push("oficialia");
}

const optionsTipoPromocionEscritoInicial = computed(() => {
  return {
    responsive: [
      {
        breakpoint: 1000,
        options: {
          plotOptions: {
            bar: {
              horizontal: false,
            },
          },
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
    colors: ["#FFB74D"],
    labels: optionsLabelTipoAsunto(),
    plotOptions: {
      bar: {
        distributed: true,
        horizontal: true,
      },
    },
    legend: {
      show: false,
      offsetX: 0,
      offsetY: 50,
      showForSingleSeries: true,
      position: "right",
      horizontalAlign: "right",
      customLegendItems: optionsLabelTipoAsunto(),
    },
    dataLabels: {
      enabled: true,
    },
    xaxis: {
      categories: optionsLabelTipoAsunto(),
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
</script>
