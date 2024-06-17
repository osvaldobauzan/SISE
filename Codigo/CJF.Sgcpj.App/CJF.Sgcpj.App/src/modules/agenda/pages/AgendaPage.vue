<template>
  <q-page class="q-pa-sm">
    <q-toolbar>
      <q-toolbar-title class="text-bold text-h4 text-primary"
        >Agenda</q-toolbar-title
      >
      <q-space></q-space>
      <navigation-bar @today="onToday" @prev="onPrev" @next="onNext" />
      <q-icon
        name="mdi-table-search"
        class="cursor-pointer q-px-lg"
        size="lg"
        @click="showReporteAgenda = true"
      >
        <q-tooltip>Reporte agenda</q-tooltip>
      </q-icon>
      <q-btn
        dense
        no-caps
        color="primary"
        label="Agendar"
        icon="mdi-plus"
        @click="(currentDay = null), (showAgendar = true)"
        class="q-ml-sm q-px-lg"
        style="min-width: 200px"
      >
      </q-btn>
      <q-select
        v-model="selectedView"
        label="Modo"
        outlined
        dense
        options-dense
        :options="opcionesVistaCalendario"
        option-label="label"
        option-value="value"
        class="q-ml-sm q-px-lg"
        style="min-width: 250px"
        @update:model-value="cambioVistaCalendario"
      />
    </q-toolbar>
    <div style="max-width: 100%; width: 100%">
      <q-card flat class="bg-primary">
        <q-item-label class="text-white text-center text-bold text-h5">
          {{ getMonth() }}
        </q-item-label>
      </q-card>
      <q-card>
        <q-calendar
          ref="calendar"
          v-model="selectedDate"
          :mode="modoCalendario"
          :view="selectedView.value"
          :weekdays="weekdays"
          locale="es-MX"
          :day-min-height="160"
          bordered
          animated
          :disabledWeekdays="[0, 6]"
          @change="moveDate"
          @update:model-value="(val) => (selectedDateOption = val)"
        >
          <template #day="{ scope: { timestamp } }">
            <div
              style="min-height: 100%; min-height: 100%"
              @dblclick="agendar(timestamp)"
            >
              <template
                v-for="a in getAgenda(timestamp)"
                :key="timestamp.date + a.horaAudiencia"
              >
                <q-btn
                class="full-width"
                  flat
                  :ripple="false"
                  color="blue"
                  @click="
                    (selectedItem = a),
                      (selectedItem.label = `${weekdaysLabel[timestamp.weekday]}, ${timestamp.day} de ${getMonth().toLocaleLowerCase()} de ${timestamp.year} ${a.horaAudiencia}`),
                      (showDetalleAudiencia = true)
                  "
                >
                  <div
                    :label="`${a.horaAudiencia}`"
                    :class="`full-width justify-start q-ma-sm shadow-5 ${getColor(a.resultado)} text-dark`"
                    style="max-width: 100%; width: 100%"
                  >
                    <div class="col-12 q-px-sm">
                      <strong>{{ a.horaAudiencia }}</strong>
                    </div>
                    <div class="col-12 q-px-sm" style="font-size: 10px">
                      {{ `${a.expediente} ${a.tipoAsunto}` }}
                    </div>
                  </div>
                </q-btn>
              </template>
            </div>
          </template>
        </q-calendar>
        <q-inner-loading :showing="cargandoEventos"> </q-inner-loading>
      </q-card>
    </div>
    <q-dialog v-model="showAgendar" persistent>
      <AgendarAudiencia
        :currentDay="currentDay"
        :item="selectedItem"
        @cerrar="showAgendar = false"
      ></AgendarAudiencia>
    </q-dialog>
    <q-dialog v-model="showAudienciaSinResultado" persistent>
      <AudienciaSinResultado
        v-model="selectedItem"
        @cerrar="showAudienciaSinResultado = false"
      ></AudienciaSinResultado>
    </q-dialog>
    <q-dialog v-model="showReporteAgenda" persistent>
      <ReporteAgenda
        :items="selectedItems"
        @cerrar="showReporteAgenda = false"
      ></ReporteAgenda>
    </q-dialog>
    <q-dialog v-model="showDetalleAudiencia">
      <DetalleAudiencia
        :model-value="selectedItem"
        @success="(showDetalleAudiencia = false), moveDate()"
      ></DetalleAudiencia>
    </q-dialog>
  </q-page>
</template>

<script setup>
import {
  computed,
  watch,
  onMounted,
  onUpdated,
  onBeforeUnmount,
  ref,
} from "vue";
import NavigationBar from "../components/NavigationBar.vue";
import {
  QCalendar,
  today,
  createNativeLocaleFormatter,
  parseTimestamp,
} from "@quasar/quasar-ui-qcalendar/src/index.js";
import AgendarAudiencia from "../components/AgendarAudiencia.vue";
import AudienciaSinResultado from "../components/AudienciaSinResultado.vue";
import { useAgendaStore } from "src/modules/agenda/stores/agenda-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { AgendaDto } from "../data/agendadto";
import DetalleAudiencia from "../components/DetalleAudiencia.vue";
import ReporteAgenda from "../components/ReporteAgenda.vue";

const agendaStore = useAgendaStore();

const selectedView = ref({ label: "Mes", value: "month" });
const monthFormatter = monthFormatterFunc();
// const locale = ref('es-MX');
const calendar = ref(null);
const showAgendar = ref(false);
const showDetalleAudiencia = ref(false);
const showAudienciaSinResultado = ref(false);
const showReporteAgenda = ref(false);
const selectedItems = ref([]);
const cargandoEventos = ref(false);
const selectedDate = ref(today());
const selectedDateOption = ref(today());
const modoCalendario = ref("month");
const opcionesVistaCalendario = [
  { label: "Día", value: "day" },
  { label: "Semana", value: "week" },
  { label: "Semana laboral", value: "week-agenda" },
  { label: "Mes", value: "month" },
];
const weekdays = ref([0, 1, 2, 3, 4, 5, 6]);
const weekdaysLabel = [
  "domingo",
  "lunes",
  "martes",
  "miércoles",
  "jueves",
  "viernes",
  "sábado",
];

const currentDay = ref(null);
const fechaInicio = ref(null);
const fechaFin = ref(null);
const agenda = ref(Array(new AgendaDto()));
const props = defineProps({
  // v-model
  modelValue: {
    default: "",
  },
});
const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
});

// eslint-disable-next-line no-unused-vars
const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});

const stopWatch = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.modelValue,
  // eslint-disable-next-line no-unused-vars
  async (_newValue, _oldValue) => {
    // do something
  },
  {
    immediate: true,
  },
);

onMounted(() => {});

onUpdated(() => {});

onBeforeUnmount(() => {
  stopWatch();
});
function onToday() {
  calendar.value.moveToToday();
}
function onPrev() {
  calendar.value.prev();
}
function onNext() {
  calendar.value.next();
}
function cambioVistaCalendario(val) {
  if (val && val.value == "month") {
    weekdays.value = [0, 1, 2, 3, 4, 5, 6];
    modoCalendario.value = "month";
  } else if (val && val.value == "week-agenda") {
    weekdays.value = [1, 2, 3, 4, 5];
    modoCalendario.value = "agenda";
  } else {
    weekdays.value = [0, 1, 2, 3, 4, 5, 6];
    modoCalendario.value = "agenda";
  }
}
function getAgenda(day) {
  return agenda.value?.filter((x) => x.fechaAudiencia === day.date);
}
function getMonth() {
  if (!selectedDate.value) {
    return;
  }
  return monthFormatter(
    parseTimestamp(selectedDate.value),
    false,
  )?.toUpperCase();
}
function monthFormatterFunc() {
  const longOptions = { timeZone: "UTC", month: "long" };
  const shortOptions = { timeZone: "UTC", month: "short" };

  return createNativeLocaleFormatter("es-MX", (_tms, short) =>
    short ? shortOptions : longOptions,
  );
}
async function moveDate(val) {
  if (val) {
    fechaInicio.value = val.start.split("-").reverse().join("/");
    fechaFin.value = val.end.split("-").reverse().join("/");
  }
  try {
    cargandoEventos.value = true;
    agenda.value = await agendaStore.obtenerAgendaAudienciaPorFecha(
      fechaInicio.value,
      fechaFin.value,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoEventos.value = false;
}
function agendar(day) {
  if (day.weekday != 0 && day.weekday != 6) {
    currentDay.value = day.date.split("-").reverse().join("/");
    let hoy = new Date();
    hoy = new Date(
      hoy.getFullYear(),
      hoy.getMonth(),
      hoy.getDate(),
      0,
      0,
      0,
      0,
    );
    //TODO: agregar dias getDiasInhabiles
    if (
      new Date(
        day.date.split("-")[0],
        day.date.split("-")[1] - 1,
        day.date.split("-")[2],
        14,
        0,
        0,
        0,
      ) >= hoy
    )
      showAgendar.value = true;
  }
}
function getColor(estado) {
  switch (estado) {
    case "Celebrada":
      return "bg-green-13";
    case "Diferida":
      return "bg-yellow-6";
    case "Cancelada (No pasó)":
      return "bg-red-6";

    default:
      return "bg-light-blue-3";
  }
}
</script>
<style scoped lang="css"></style>
