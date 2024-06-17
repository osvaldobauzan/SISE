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
        <ProfileEjecucion
          :user="user"
          v-for="user in mesa.users"
          :key="user.displayName"
        ></ProfileEjecucion>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import ProfileEjecucion from "./profileEjecucion.vue";

const tab = ref(null);

onMounted(() => {
  tab.value = mesas[0].name;
});

const mesas = [
  {
    name: "mesa1",
    displayName: "Mesa I",
    users: [
      {
        displayName: "GHernandezF",
        fullName: "Gabriela Hernández Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",
        cards: [],
      },
      {
        displayName: "OJuarezF",
        fullName: "Omar Juárez Fuentes",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [],
      },
    ],
  },
  {
    name: "mesa2",
    displayName: "Mesa II",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",
        cards: [],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [],
      },
    ],
  },
  {
    name: "titular",
    displayName: "Titular",
    users: [
      {
        displayName: "AOrtizR",
        fullName: "Alan Ortiz Ramírez",
        cargoDescripcion: "Juez de Distrito",
        titleTiempo: "Tiempo de autorización",
        cards: [],
      },
    ],
  },
];
</script>
