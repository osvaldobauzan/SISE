<template>
  <div class="row">
    <q-card flat bordered>
      <q-btn-dropdown
        flat
        no-caps
        style="min-height: 50.19px"
        class="bg-white text-grey-8"
        v-close-popup
      >
        <template v-slot:label>
          <div class="row items-center no-wrap">
            <q-icon left :name="iconDate" />
            <div class="text-left text-secondary">
              <q-item-label caption>{{ title }}</q-item-label>
              {{ selectedDateCopy }}
            </div>
          </div>
        </template>
        <q-list>
          <slot></slot>
          <q-item
            clickable
            v-ripple
            v-close-popup
            @click="setHoy"
            :disable="!filtrarPorFecha"
          >
            <q-item-section avatar>
              <q-icon
                color="primary"
                name="mdi-calendar-today"
                class="text-grey-8"
              />
            </q-item-section>
            <q-item-section>Hoy</q-item-section>
          </q-item>
          <q-separator></q-separator>
          <q-expansion-item :disable="!filtrarPorFecha">
            <template v-slot:header>
              <q-item-section avatar>
                <q-icon color="grey-8" name="mdi-calendar-range" />
              </q-item-section>
              <q-item-section> Seleccionar fecha </q-item-section>
            </template>
            <q-date
              v-model="selectedDate"
              today-btn
              :mask="'DD/MM/YYYY'"
              navigation-min-year-month="1900/01"
              :title="title"
              :options="
                (d) =>
                  d >= '1900/01/01' &&
                  d <= date.formatDate(maxSelectDate, 'YYYY/MM/DD')
              "
              @update:model-value="setFechaUnica"
            />
          </q-expansion-item>
          <template v-if="isSeguimientoPage">
            <div class="row items-center justify">
              <q-toggle
                v-model="filtrarPorFecha"
                :label="
                  filtrarPorFecha ? 'Filtrar por fecha' : 'No filtrar por fecha'
                "
                @click="triggerFilter"
              >
              </q-toggle>
            </div>
          </template>
        </q-list>
      </q-btn-dropdown>
    </q-card>
  </div>
</template>

<script setup>
import { date } from "quasar";
import { ref, onMounted } from "vue";

const filtrarPorFecha = ref(true);

defineProps({
  title: {
    type: String,
    default: "Fecha de presentación",
  },
  isSeguimientoPage: {
    type: Boolean,
    default: false,
  },
  maxSelectDate: {
    type: Date,
    default: new Date(2101, 0, 1),
  },
});
defineExpose({
  setFecha,
});
function triggerFilter() {
  emit("update:selectedDate", selectedDate.value);
}

onMounted(() => setHoy());
const emit = defineEmits(["update:selectedDate"]);
const iconDate = ref("mdi-calendar-today");

const selectedDate = ref({
  date: date.formatDate(Date.now(), "DD/MM/YYYY"),
  filter: filtrarPorFecha,
});

const selectedDateCopy = ref(date.formatDate(Date.now(), "DD/MM/YYYY"));
function setFecha(fecha) {
  selectedDate.value = fecha;
  selectedDateCopy.value = fecha;
}
/**
 * Establece el filtro de fechas a hoy
 */
function setHoy() {
  iconDate.value = "mdi-calendar-today";
  selectedDate.value = date.formatDate(Date.now(), "DD/MM/YYYY");
  esNullFecha();
  emit("update:selectedDate", selectedDate.value);
}

function setFechaUnica() {
  if (!selectedDate.value) {
    const fecha = selectedDate.value;
    selectedDate.value = fecha;
  }
  if (esNullFecha()) {
    return;
  }
  iconDate.value = "mdi-calendar-range";
  emit("update:selectedDate", selectedDate.value);
}
function esNullFecha() {
  if (!selectedDate.value) {
    return true;
  } else {
    selectedDateCopy.value = selectedDate.value;
    return false;
  }
}
</script>
