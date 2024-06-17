<template>
  <q-card flat>
    <q-toolbar>
      <q-toolbar-title>Fechas</q-toolbar-title>
    </q-toolbar>
    <q-card-section>
      <div class="row q-gutter-md q-my-md">
        <div class="col">
          <q-input dense filled v-model="fechaInicio" label="Fecha Inicio">
            <template v-slot:append>
              <q-icon name="mdi-calendar" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date v-model="fechaInicio" mask="YYYY-MM-DD">
                    <q-btn v-close-popup label="Cerrar" color="primary" flat />
                  </q-date>
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input dense filled v-model="fechaFin" label="Fecha Fin">
            <template v-slot:append>
              <q-icon name="mdi-calendar" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date v-model="fechaFin" mask="YYYY-MM-DD">
                    <q-btn v-close-popup label="Cerrar" color="primary" flat />
                  </q-date>
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
        </div>
      </div>
      <div class="row q-gutter-md">
        <div class="col">
          <q-card>
            <q-item>
              <q-item-section>
                <q-item-label caption>Fecha compuesta</q-item-label>
                <q-item-label class="text-bold">{{
                  fechaCompuesta
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-card>
        </div>
        <div class="col-2">
          <q-card>
            <q-item>
              <q-item-section>
                <q-item-label caption>Días</q-item-label>
                <q-item-label class="text-bold">{{
                  date.getDateDiff(fechaFin, fechaInicio, "days")
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-card>
        </div>
        <div class="col-2">
          <q-card>
            <q-item>
              <q-item-section>
                <q-item-label caption>Semanas</q-item-label>
                <q-item-label class="text-bold">{{
                  Math.floor(
                    date.getDateDiff(fechaFin, fechaInicio, "days") / 7,
                  )
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-card>
        </div>
        <div class="col-2">
          <q-card>
            <q-item>
              <q-item-section>
                <q-item-label caption>Meses</q-item-label>
                <q-item-label class="text-bold">{{
                  date.getDateDiff(fechaFin, fechaInicio, "months")
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-card>
        </div>
        <div class="col-2">
          <q-card>
            <q-item>
              <q-item-section>
                <q-item-label caption>Años</q-item-label>
                <q-item-label class="text-bold">{{
                  date.getDateDiff(fechaFin, fechaInicio, "years")
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-card>
        </div>
      </div>
    </q-card-section>
    <q-card-section>
      <div class="row q-gutter-md">
        <div class="col">
          <q-input dense filled v-model="fecha" label="Fecha">
            <template v-slot:append>
              <q-icon name="mdi-calendar" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date v-model="fecha" mask="YYYY-MM-DD">
                    <q-btn v-close-popup label="Cerrar" color="primary" flat />
                  </q-date>
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input
            dense
            filled
            v-model="agregar"
            label="Agregar días"
            type="number"
          >
          </q-input>
        </div>
        <div class="col">
          <q-input
            dense
            filled
            v-model="restar"
            label="Restar días"
            type="number"
          >
          </q-input>
        </div>
      </div>
      <div class="row q-my-md">
        <div class="col"></div>
        <q-card class="col">
          <q-item>
            <q-item-section side>
              <q-icon name="mdi-calendar"></q-icon>
            </q-item-section>
            <q-item-section>
              <q-item-label caption>Resultado</q-item-label>
              <q-item-label class="text-bold">{{ resultado }}</q-item-label>
            </q-item-section>
          </q-item>
        </q-card>
        <div class="col"></div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script setup>
import _ from "lodash";
import { date } from "quasar";
import { ref, computed, onMounted } from "vue";

const fechaInicio = ref("");
const fechaFin = ref("");
const fecha = ref("");
const agregar = ref(0);
const restar = ref(0);

onMounted(() => {
  fechaInicio.value = date.formatDate(_.now(), "YYYY-MM-DD");
  fechaFin.value = date.formatDate(_.now(), "YYYY-MM-DD");
  fecha.value = date.formatDate(_.now(), "YYYY-MM-DD");
});

const resultado = computed(() => {
  return date.formatDate(
    date.addToDate(fecha.value, {
      days: agregar.value - restar.value,
      months: 0,
      years: 0,
    }),
    "YYYY-MM-DD",
  );
});

const fechaCompuesta = computed(() => {
  return `${date.getDateDiff(fechaFin.value, fechaInicio.value, "years")} años ${Math.floor(date.getDateDiff(fechaFin.value, fechaInicio.value, "days") / 12 / 12 / 12)} meses ${date.getDateDiff(fechaFin.value, fechaInicio.value, "days")} días  `;
});
</script>
