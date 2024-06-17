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
        v-for="mesa in mesas"
        :key="mesa.name"
      />
    </q-tabs>
    <q-tab-panels class="bg-white" v-model="tab" animated>
      <q-tab-panel
        :name="mesa.name"
        v-for="(mesa, index) in mesas"
        :key="index"
        class="q-gutter-md"
      >
        <ProfileActuaria
          :user="user"
          v-for="user in mesa.users"
          :key="user.displayName"
          :selectedDate="selectedDate"
        ></ProfileActuaria>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import ProfileActuaria from "./profileActuaria.vue";
import { useIndexStore } from "../../stores/index-store";
import { manejoErrores } from "src/helpers/manejo-errores.js";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";

const authStore = useAuthStore();
const indexStore = useIndexStore();
const tab = ref(null);
const mesas = ref([]);

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

const valoresPredeterminados = [
  { value: 0, caption: "Realizadas" },
  { value: 0, caption: "Personales" },
  { value: 0, caption: "Oficios" },
  { value: 0, caption: "Lista" },
  { value: 0, caption: "Electrónicas" },
];
onMounted(() => {
  mesas.value = transformarDatos();

  tab.value = mesas.value[0]?.name || '';
});

watch(async () => props.selectedDate, async () => {
  mesas.value = await transformarDatos();
}, { deep: true, immediate: true });


async function transformarDatos() {
    // Esperan 1 segundo antes de ejecutar el código, ya que aunque es un módulo interno puede cargar antes del index-store
    await new Promise(resolve => setTimeout(resolve, 3000));

    const actuarios = [];
    for (const actuario of indexStore.detalleActuario.detalleActuarios) {
        // Crear una copia de los valores predeterminados
        let cards = JSON.parse(JSON.stringify(valoresPredeterminados));

        // Actualizar los valores de cards con los valores de listaTipos
        actuario.listaTipos.forEach(tipo => {
            let cardTotal = cards.find(card => card.caption === "Realizadas");
            if (cardTotal) {
                cardTotal.value = actuario.totalNotificaciones.notificadas;
            }
            let card = cards.find(card => card.caption === tipo.tipo);
            if (card) {
                card.value = tipo.total;
            }
        });

        const seriesMes = await fetchDetalleMes(actuario.actuario.empleadoId);
        const seriesDias = await fetchDiferenciasDias(actuario.actuario.empleadoId);
        
        actuarios.push({
            name: actuario.actuario.userName,
            displayName: actuario.actuario.nombreArea,
            users: [
                {
                    displayName: actuario.actuario.userName,
                    fullName: actuario.actuario.nombreOficial,
                    actuarioId: actuario.actuario.empleadoId,
                    cargoDescription: "Actuario",
                    hoy: [
                        {
                            title: "Por notificar",
                            value: actuario.totalNotificaciones.pendientes,
                            total: actuario.totalNotificaciones.total,
                            caption: "Hasta hoy",
                        }
                    ],
                    titleCards: "Notificaciones 2024",
                    cards: cards,
                    seriesMes: seriesMes,
                    seriesDias:seriesDias,
                    dynamicMonths: getMonthsInReverseOrder().slice(-6)
                }],
        });
    }

    return actuarios;
}

const fetchDetalleMes = async (empleadoId) => {
  try {
    await indexStore.obtenerDesglosesMes(props.selectedDate.from, props.selectedDate.to, authStore.user.catOrganismoId, empleadoId);
    
    // Inicializar la estructura para almacenar los datos de las series
    const auxvaloresPredeterminados = [];
    const monthList = getMonthsInReverseOrder().slice(-6);// últimos 6 meses en orden inverso
    // Iterar sobre los datos de detalle de mes
    indexStore.detalleMes.notificaciones.forEach(x => {
      const monthIndex = monthList.indexOf(x.mes);
      if (monthIndex >= 0) {
        // Encontrar o crear una entrada para el tipo de asunto
        let cardTotal = auxvaloresPredeterminados.find(card => card.name === x.tipo);
        if (!cardTotal) {
          cardTotal = { name: x.tipo, type: "column", data: Array(6).fill(0) };
          auxvaloresPredeterminados.push(cardTotal);
        }
        // Asignar el valor total al índice correspondiente del mes
        cardTotal.data[monthIndex] = x.total;
      }
    });

    return auxvaloresPredeterminados;
  } catch (error) {
    manejoErrores.mostrarError(error);
    return [];
  }
};


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

const fetchDiferenciasDias = async (empleadoId) => {
  try {
    await indexStore.obtenerDiferenciasDias(empleadoId, props.selectedDate.from, props.selectedDate.to);
    const auxSeriesTiempo = [{ data: [] }];
    indexStore.diferenciasDias.forEach(a => {
      // Verificar si horaMinutoTurnado es diferente de '00:00'
      if (a.diferenciaDias !== 0) {
        auxSeriesTiempo[0].data.push({
          x: a.asuntoAlias,
          y: [
            a.diaAsigna,
            a.diaNotifica // No es necesario el ternario
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

</script>
