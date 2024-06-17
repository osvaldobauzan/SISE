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
              {{ selectedDateCopy.from + " - " + selectedDateCopy.to }}
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
          <q-item
            clickable
            v-ripple
            v-close-popup
            @click="setUltimos7dias"
            :disable="!filtrarPorFecha"
          >
            <q-item-section avatar>
              <q-icon
                color="primary"
                name="mdi-calendar-week"
                class="text-grey-8"
              />
            </q-item-section>
            <q-item-section>Últimos 7 días</q-item-section>
          </q-item>
          <q-item
            clickable
            v-ripple
            v-close-popup
            @click="setUltimos30dias"
            :disable="!filtrarPorFecha"
          >
            <q-item-section avatar>
              <q-icon
                color="primary"
                name="mdi-calendar-month"
                class="text-grey-8"
              />
            </q-item-section>
            <q-item-section>Últimos 30 días</q-item-section>
          </q-item>
          <q-separator></q-separator>
          <q-expansion-item :disable="!filtrarPorFecha">
            <template v-slot:header>
              <q-item-section avatar>
                <q-icon color="grey-8" name="mdi-calendar-range" />
              </q-item-section>
              <q-item-section> Rango de fechas </q-item-section>
            </template>
            <q-date
              v-model="selectedDate"
              range
              today-btn
              :mask="'DD/MM/YYYY'"
              navigation-min-year-month="1900/01"
              :title="
                selectedDate
                  ? `${Math.round(
                      date.getDateDiff(
                        new Date(
                          +selectedDate.to.split('/')[2],
                          +selectedDate.to.split('/')[1] - 1,
                          +selectedDate.to.split('/')[0],
                        ),
                        new Date(
                          +selectedDate.from.split('/')[2],
                          +selectedDate.from.split('/')[1] - 1,
                          +selectedDate.from.split('/')[0],
                        ),
                      ) + 1,
                    )} días`
                  : ''
              "
              :options="
                (d) =>
                  d >= '1900/01/01' &&
                  d <= date.formatDate(maxSelectDate, 'YYYY/MM/DD')
              "
              @update:model-value="setRangoFechas"
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
  from: date.formatDate(Date.now(), "DD/MM/YYYY"),
  to: date.formatDate(Date.now(), "DD/MM/YYYY"),
  filter: filtrarPorFecha,
});

const selectedDateCopy = ref({
  from: date.formatDate(Date.now(), "DD/MM/YYYY"),
  to: date.formatDate(Date.now(), "DD/MM/YYYY"),
});
function setFecha(fecha) {
  selectedDate.value = fecha;
  selectedDateCopy.value = fecha;
}
/**
 * Establece el filtro de fechas a hoy
 */
function setHoy() {
  iconDate.value = "mdi-calendar-today";
  selectedDate.value = {
    from: date.formatDate(Date.now(), "DD/MM/YYYY"),
    to: date.formatDate(Date.now(), "DD/MM/YYYY"),
    filter: filtrarPorFecha,
  };
  esNullFecha();
  emit("update:selectedDate", selectedDate.value);
}
/**
 * Establece el filtro de fechas a rango de ultimos 7 dias
 */
function setUltimos7dias() {
  iconDate.value = "mdi-calendar-week";
  selectedDate.value = {
    from: date.formatDate(Date.now() - 1000 * 60 * 60 * 24 * 6, "DD/MM/YYYY"),
    to: date.formatDate(Date.now(), "DD/MM/YYYY"),
    filter: filtrarPorFecha,
  };
  esNullFecha();
  emit("update:selectedDate", selectedDate.value);
}
/**
 * Establece el filtro de fechas a rango de ultimos 30 dias
 */
function setUltimos30dias() {
  iconDate.value = "mdi-calendar-month";
  selectedDate.value = {
    from: date.formatDate(Date.now() - 1000 * 60 * 60 * 24 * 29, "DD/MM/YYYY"),
    to: date.formatDate(Date.now(), "DD/MM/YYYY"),
    filter: filtrarPorFecha,
  };
  esNullFecha();
  emit("update:selectedDate", selectedDate.value);
}

function setRangoFechas() {
  if (selectedDate.value && !selectedDate.value.from) {
    const fecha = selectedDate.value;
    selectedDate.value = {
      from: fecha,
      to: fecha,
    };
  }
  if (esNullFecha()) {
    return;
  }
  iconDate.value = "mdi-calendar-range";
  const minDate = new Date(1899, 11, 31);
  const maxDate = new Date(2101, 0, 1);
  const fechaFrom = new Date(
    +selectedDate.value.from.split("/")[2],
    +selectedDate.value.from.split("/")[1] - 1,
    +selectedDate.value.from.split("/")[0],
  );
  const fechaTo = new Date(
    +selectedDate.value.to.split("/")[2],
    +selectedDate.value.to.split("/")[1] - 1,
    +selectedDate.value.to.split("/")[0],
  );
  if (!date.isBetweenDates(fechaFrom, minDate, maxDate)) {
    if (fechaFrom < minDate) {
      selectedDate.value.from = date.formatDate(minDate, "DD/MM/YYYY");
    } else {
      selectedDate.value.from = date.formatDate(Date.now(), "DD/MM/YYYY");
    }
  }
  if (!date.isBetweenDates(fechaTo, minDate, maxDate)) {
    if (fechaTo < minDate) {
      selectedDate.value.to = date.formatDate(minDate, "DD/MM/YYYY");
    } else {
      selectedDate.value.to = date.formatDate(Date.now(), "DD/MM/YYYY");
    }
  }
  selectedDate.value.filter = filtrarPorFecha;
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
