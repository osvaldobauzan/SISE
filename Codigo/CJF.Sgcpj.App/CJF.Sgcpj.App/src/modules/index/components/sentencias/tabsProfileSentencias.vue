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
        <ProfileSentencias
          :user="user"
          v-for="user in mesa.users"
          :key="user.displayName"
        ></ProfileSentencias>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import ProfileSentencias from "./profileSentencias.vue";
const tab = ref(null);

onMounted(() => {
  tab.value = mesas[0].name;
});

const mesas = [
  {
    name: "GJuarezF",
    displayName: "GJuarezF",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        hoy: [
          { title: "Proyectos hoy", value: 5, total: 15, caption: "Aprobados" },
        ],
        cards: [
          { title: "Proyectos por semana", value: 2, caption: "Promedio" },
          {
            title: "Creación de proyectos",
            value: "2 días",
            caption: "Promedio",
          },
          { title: "Efectividad", value: "2", caption: "Versiones promedio" },
        ],
      },
    ],
  },
  {
    name: "JHernandezH",
    displayName: "JHernandezH",
    users: [
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Secretario",
        hoy: [
          { title: "Proyectos hoy", value: 5, total: 15, caption: "Aprobados" },
        ],
        cards: [
          { title: "Proyectos por semana", value: 2, caption: "Promedio" },
          {
            title: "Creación de proyectos",
            value: "2 días",
            caption: "Promedio",
          },
          { title: "Efectividad", value: "2", caption: "Versiones promedio" },
        ],
      },
    ],
  },
  {
    name: "OJuarezF",
    displayName: "OJuarezF",
    users: [
      {
        displayName: "OJuarezF",
        fullName: "Oscar Juárez Flores",
        cargoDescripcion: "Secretario",
        hoy: [
          { title: "Proyectos hoy", value: 5, total: 15, caption: "Aprobados" },
        ],
        cards: [
          { title: "Proyectos por semana", value: 2, caption: "Promedio" },
          {
            title: "Creación de proyectos",
            value: "2 días",
            caption: "Promedio",
          },
          { title: "Efectividad", value: "2", caption: "Versiones promedio" },
        ],
      },
    ],
  },
];
</script>
