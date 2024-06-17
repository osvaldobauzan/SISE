<template>
  <q-card flat>
    <q-tabs
      no-caps
      stretch
      switch-indicator
      v-model="tab"
      align="left"
      class="q-mt-lg bg-grey-4"
      active-class="bg-white"
      indicator-color="primary"
    >
      <q-tab
        :name="mesa.name"
        :label="mesa.displayName"
        class="q-px-xl"
        v-for="(mesa, index) in mesas" :key="index"
      />
    </q-tabs>
    <q-tab-panels class="bg-white" v-model="tab" animated>
      <q-tab-panel :name="mesa.name" v-for="(mesa, index) in mesas" :key="index" class="q-gutter-md">
        <ProfileOficialia :user="mesa.user" :key="mesa.user.displayName" :selectedDate="selectedDate"></ProfileOficialia>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import ProfileOficialia from './profileOficialia.vue';
import { useOficialiaStore } from "src/modules/oficialia/stores/oficialia-store";
import { useIndexStore } from "src/modules/index/stores/index-store";
import { manejoErrores } from "src/helpers/manejo-errores.js";

const tab = ref(null);
const oficialiaStore = useOficialiaStore();
const indexStore = useIndexStore();
  // Obtener el año actual
const currentYear = new Date().getFullYear();
const props = defineProps({
  selectedDate: {
      from: '',
      to: '',
   },
   user: {
    type: Object,
    required: true,
  }
});
  const mesas = ref([]);

  function timeStringToNumber(timeString) {
    const [hours, minutes] = timeString.split(':').map(Number);
    const timeInHours = hours + minutes / 60;
    return Math.round(timeInHours);
  }

async function fetchKpis(){
  try {
    mesas.value = [];
    tab.value = [];
    const indexValues = [];
    for (const i in indexStore.kpiSummaryData) {
      const element = indexStore.kpiSummaryData[i];
      indexValues.push(element.empleadoId);
      mesas.value.push(
       {
        name: element.nombreOficial,
        displayName: element.userName,
        user: 
          {
            empleadoId: element.empleadoId,
            displayName: element.userName,
            fullName: element.nombreOficial,
            cargoDescripcion: "Oficial de partes",
            hoy: [{
              title: "Promociones",
              value: element.promocionesTurnadas,
              total: element.totalPromociones,
              caption: "Turnadas",
            }],
            cards: [
              { title: `Promociones ${currentYear}`, value: element.totalPromocionesAnoActual, caption: "Capturadas" },
              { title: "Por día", value: element.promedioPromocionesTurnadasPorDia, caption: "Capturadas" },
              { title: "Turna a mesa en", value: element.tiempoPromedioMins, caption: "Promedio" },
            ],
            newSeriesAcuerdos: [],
            seriesTiempo: [],
            dynamicLabels: []
          }
      });
    }

    let auxNewSeriesAcuerdos = [];
    let auxSeriesTiempo = [];
    for (const key in indexValues) {
      auxNewSeriesAcuerdos = await fetchConteoMes(indexValues[key]);
      auxSeriesTiempo = await fetchTiemposTurnos(indexValues[key]);
      mesas.value[key].user.newSeriesAcuerdos = auxNewSeriesAcuerdos;
      mesas.value[key].user.seriesTiempo = auxSeriesTiempo;
      mesas.value[key].user.dynamicLabels = getMonthsInReverseOrder();
    }
  
    tab.value = mesas.value[0]?.name || '';
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
};

// Función para obtener tiempos de turnos y retornar los datos
const fetchTiemposTurnos = async (empleadoId) => {
  try {
    await oficialiaStore.obtenerTiemposTurnos(props.selectedDate.from, props.selectedDate.to, empleadoId);
    const auxSeriesTiempo = [{ data: [] }];
    oficialiaStore.intervalTimeData.forEach(a => {
      if (a.horaMinutoTurnado !== '00:00') {
        auxSeriesTiempo[0].data.push({
          x: a.numeroRegistro.toString(),
          y: [
            timeStringToNumber(a.horaMinutoAlta),
            timeStringToNumber(a.horaMinutoTurnado) // No es necesario el ternario
          ]
        });
      }
    });

    return auxSeriesTiempo;
  } catch (error) {
    manejoErrores.mostrarError(error);
    return [];
  }
};


// Función para obtener conteo de mes y retornar los datos
const fetchConteoMes = async (empleadoId) => {
  try {
    // Esperar a obtener los datos del conteo de meses
    await oficialiaStore.obtenerConteoMes(empleadoId);
    
    // Inicializar la estructura para almacenar los datos de las series
    const auxNewSeriesAcuerdos = [];
    const monthList = getMonthsInReverseOrder();

    // Iterar sobre los datos de conteo por mes
    oficialiaStore.countMonthData.forEach(e => {
      const monthIndex = monthList.indexOf(e.mes);
      if (monthIndex >= 0) {
        // Encontrar o crear una entrada para el tipo de asunto
        let series = auxNewSeriesAcuerdos.find(series => series.name === e.tipoAsunto);
        if (!series) {
          series = { name: e.tipoAsunto, data: Array(12).fill(0) };
          auxNewSeriesAcuerdos.push(series);
        }
        // Asignar el valor total al índice correspondiente del mes
        series.data[monthIndex] = e.total;
      }
    });

    return auxNewSeriesAcuerdos;
  } catch (error) {
    // Manejo de errores
    manejoErrores.mostrarError(error);
    return [];
  }
};

/**
 * Función para obtener un array con los nombres de los meses
 * ordenados desde el mes actual hacia atrás, es decir, el último elemento
 * del array será el mes actual.
 * 
 * @returns {string[]} Array de nombres de los meses.
 */
function getMonthsInReverseOrder() {
  const monthNames = [
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ];

  const currentMonthIndex = new Date().getMonth(); // Obtener el índice del mes actual (0-11)
  const reversedMonths = [];

  for (let i = 0; i < 12; i++) {
    const monthIndex = (currentMonthIndex - i + 12) % 12;
    reversedMonths.push(monthNames[monthIndex]);
  }

  return reversedMonths.reverse();
}


onMounted(async () => {
  await fetchKpis();
});

watch(async () => props.selectedDate, async () => {
  await fetchKpis();
}, { deep: true, immediate: true });

</script>
