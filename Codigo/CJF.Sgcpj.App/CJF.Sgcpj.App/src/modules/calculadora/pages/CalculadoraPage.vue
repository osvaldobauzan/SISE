<template>
  <q-page padding>
    <q-toolbar>
      <q-toolbar-title class="text-h4">Calculadoras</q-toolbar-title>
    </q-toolbar>
    <q-card class="q-mt-lg">
      <q-splitter v-model="splitterModel" style="height: 450px">
        <template v-slot:before>
          <q-list>
            <q-item
              clickable
              v-ripple
              v-for="item in calculadoras_list"
              :key="item.name"
              @click="tab = item.name"
              :class="tab === item.name ? 'text-primary bg-grey-3' : ''"
            >
              <q-item-section>{{ item.displayName }}</q-item-section>
            </q-item>
          </q-list>
        </template>

        <template v-slot:after>
          <q-tab-panels
            v-model="tab"
            animated
            swipeable
            vertical
            transition-prev="jump-up"
            transition-next="jump-up"
          >
            <q-tab-panel name="fechas">
              <CalculadoraFechas />
            </q-tab-panel>
          </q-tab-panels>
        </template>
      </q-splitter>
    </q-card>
  </q-page>
</template>

<script setup>
import { ref } from "vue";
import CalculadoraFechas from "../components/CalculadoraFechas.vue";

const tab = ref("fechas");
const splitterModel = ref(20);

const calculadoras_list = [
  {
    name: "trabajo",
    displayName: "Ley Federal del Trabajo",
    icon: "mdi-calculator",
    route: "calculadora-lft",
  },
  {
    name: "imss",
    displayName: "Ley del Seguro Social",
    icon: "mdi-calculator",
    route: "calculadora-lft",
  },
  {
    name: "amparo",
    displayName: "Ley de Amparo",
    icon: "mdi-calculator",
    route: "calculadora-lft",
  },
  {
    name: "salario",
    displayName: "Salario promedio",
    icon: "mdi-calculator",
    route: "calculadora-lft",
  },
  {
    name: "inflacion",
    displayName: "Incremento de la inflación",
    icon: "mdi-calculator",
    route: "calculadora-lft",
  },
  {
    name: "fechas",
    displayName: "Fechas",
    icon: "mdi-calculator",
    route: "calculadora-lft",
  },
];
</script>
