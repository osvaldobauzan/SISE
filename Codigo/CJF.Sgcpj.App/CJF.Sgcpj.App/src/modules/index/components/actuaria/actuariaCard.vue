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
            @click="goActuaria"
          >
            Actuaría
          </q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="donut"
            height="280"
            :options="chartOptionsDonutAcuerdos"
            :series="seriesDonutAcuerdos"
            @dataPointSelection="clickHandler"
            ref="donutActuaria"
          >
          </apexchart>
        </q-card-section>
      </div>
      <div
        class="col-sm-12 col-md-3"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-toolbar>
          <q-toolbar-title> Pendientes </q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="bar"
            height="280"
            :options="optionsPendientes"
            :series="seriesPendientes"
            ref="refPendientes"
            @dataPointSelection="clickPendientes"
          >
          </apexchart>
        </q-card-section>
      </div>
      <div
        class="col-sm-12 col-md-4"
        :class="!$q.screen.gt.xs ? 'full-width' : ''"
      >
        <q-toolbar>
          <q-toolbar-title> Notificadas </q-toolbar-title>
        </q-toolbar>
        <q-card-section>
          <apexchart
            type="bar"
            height="280"
            :options="optionsNotificadas"
            :series="seriesNotificadas"
            ref="refNotificadas"
          ></apexchart>
        </q-card-section>
      </div>
      <q-inner-loading :showing="isLoading"></q-inner-loading>
    </div>
  </q-card>
  <TabsProfileActuaria  :selectedDate="selectedDate"></TabsProfileActuaria>
</template>

<script setup>
import { ref, onMounted, watch} from "vue";
import { useRouter } from "vue-router";
import TabsProfileActuaria from "./tabsProfileActuaria.vue";
import { useIndexStore } from "../../stores/index-store";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";

const authStore = useAuthStore();
const indexStore = useIndexStore();
const props = defineProps({
  selectedDate: {
      from: '',
      to: '',
   },
});

const isLoadin = ref(false); // Variable reactiva para controlar la carga
const seriesDonutAcuerdos = ref([]); 

// Asignación de datos a la gráfica
const seriesNotificadas = ref([{ // Convierte seriesNotificadas en una referencia
  name: "Cantidad",
  data: [], // Inicializamos data como un array vacío
}]);

const seriesPendientes = ref([{ // Convierte seriesPendientes en una referencia
  name: "Cantidad",
  data: [], // Inicializamos data como un array vacío
}]);


// Función para obtener indicadores (ahora reutilizable)
const obtenerIndicadores = async () => {
  try {
    isLoadin.value = true; // Inicia la carga

    const params = {
      CatOrganismoId: authStore.user.catOrganismoId,
      FiltroActuarioId: 0,
      FechaInicial: props.selectedDate.from,
      FechaFinal: props.selectedDate.to,
    };
    await indexStore.obtenerIndicadores(params);

    // Manejo de resultados
   const notificacionesPendientes = indexStore.notificacionesPendientesPorDias;
   const totalNotificaciones = indexStore.totalNotificaciones;
   const notificacionesPorTipo = indexStore.notificacionesPorTipo;

   seriesDonutAcuerdos.value = [
            totalNotificaciones.notificadas,
            totalNotificaciones.total - totalNotificaciones.notificadas // Calcula pendientes
        ];

    const tiposExistentes = notificacionesPorTipo.map(notificacion => notificacion.tipo); // Tipos de la gráfica
    const datosGrafica = tiposExistentes.map(tipo => {
      const notificacion = notificacionesPorTipo.find(item => item.tipo === tipo);
      return notificacion ? notificacion.total : 0;
    });

    seriesNotificadas.value = [{ // Actualiza seriesNotificadas
      name: "Cantidad",
      data: datosGrafica,
    }];

    const tiposTiempo = optionsPendientes.labels;// Segmentos de días existentes
    const datosGraficaT = tiposTiempo.map(tipo => {
      const notificacion = notificacionesPendientes.find(item => item.dias === tipo);
      return notificacion ? notificacion.total : 0;
    });

    seriesPendientes.value = [{ // Actualiza seriesPendientes
      name: "Cantidad",
      data: datosGraficaT,
    }];
  } catch (error) {
  } finally {
    isLoadin.value = false; // Finaliza la carga
  }
};

// Ejecuta la función al montar el componente
onMounted(obtenerIndicadores);

// Observa cambios en selectedDate y vuelve a ejecutar la función
watch(() => props.selectedDate, obtenerIndicadores, { deep: true });

const refPendientes = ref(null);
const refNotificadas = ref(null);
const donutActuaria = ref(null);
const router = new useRouter();
const isLoading = ref(false);

function goActuaria() {
  router.push("actuaria");
}

const labels = [
  {
    name: "Todos",
    dataPendientes: [30, 90, 60],
    dataNotificadas: [30, 120, 200, 30],
  },
  {
    name: "Pendientes",
    dataPendientes: [30, 60, 90],
    dataNotificadas: [120, 30, 200, 30],
  },
  {
    name: "Notificadas",
    dataPendientes: [90, 60, 30],
    dataNotificadas: [30, 120, 30, 200],
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
            label: "Notificaciones",
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
  colors: ["#EF9A9A", "#A5D6A7"],
  labels: ["Pendientes", "Notificadas"],
};

const optionsNotificadas = {
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
  colors: ["#80CBC4", "#A5D6A7", "#C4E1A5", "#E5EE9B"],
  labels: ["Oficio libre", "Oficio", "Lista", "Electrónica","Personal", "Edicto"],
  plotOptions: {
    bar: {
      distributed: true,
      horizontal: false,
    },
  },
  legend: {
    show: true,
    showForSingleSeries: true,
    position: "right",
    horizontalAlign: "right",
    customLegendItems: ["Oficio libre", "Oficio", "Lista", "Electrónica","Personal", "Edicto"],
  },
  dataLabels: {
    enabled: true,
  },
  xaxis: {
    categories: ["Oficio libre", "Oficio", "Lista", "Electrónica", "Personal", "Edicto"],
  },
};

const optionsPendientes = {
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
  colors: ["#EF9A9A", "#FFCC80", "#FFE082"],
  labels: ["+3 días", "2 días", "1 día"],
  plotOptions: {
    bar: {
      distributed: true,
      horizontal: false,
    },
  },
  legend: {
    show: true,
    showForSingleSeries: true,
    position: "right",
    horizontalAlign: "right",
    customLegendItems: ["+3 días", "2 días", "1 día"],
  },
  dataLabels: {
    enabled: true,
  },
  xaxis: {
    categories: ["+3 días", "2 días", "1 día"],
  },
};

function clickHandler(event, chartContext, config) {
  const selectedOption = config.w.config.labels[config.dataPointIndex];

  refPendientes.value.updateSeries([
    {
      data: labels.filter((e) => e.name === selectedOption)[0].dataPendientes,
    },
  ]);
  refNotificadas.value.updateSeries([
    {
      data: labels.filter((e) => e.name === selectedOption)[0].dataNotificadas,
    },
  ]);
}

function clickPendientes(event, chartContext, config) {
  const selectedOption = config.w.config.labels[config.dataPointIndex];
  router.push("/actuaria/" + selectedOption);
}

function showAllMesasBars() {
  const selectedOption = "Todos";
  refPendientes.value.updateSeries([
    {
      data: labels.filter((e) => e.name === selectedOption)[0].dataPendientes,
    },
  ]);
  refNotificadas.value.updateSeries([
    {
      data: labels.filter((e) => e.name === selectedOption)[0].dataNotificadas,
    },
  ]);
}
</script>
