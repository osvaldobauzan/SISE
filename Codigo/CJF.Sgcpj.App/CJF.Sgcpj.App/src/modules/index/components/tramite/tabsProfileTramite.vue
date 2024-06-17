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
        <ProfileTramite
          :user="user"
          v-for="user in mesa.users"
          :key="user.displayName"
        ></ProfileTramite>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import ProfileTramite from "./profileTramite.vue";

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
        hoy: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
        ],
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión por acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "OJuarezF",
        fullName: "Omar Juárez Fuentes",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        hoy: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Elaboradas",
          },
        ],
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
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

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
    ],
  },
  {
    name: "mesa3",
    displayName: "Mesa III",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
    ],
  },
  {
    name: "mesa4",
    displayName: "Mesa IV",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
    ],
  },
  {
    name: "mesa5",
    displayName: "Mesa V",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
    ],
  },
  {
    name: "mesa6",
    displayName: "Mesa VI",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
    ],
  },
  {
    name: "mesa7",
    displayName: "Mesa VII",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
    ],
  },
  {
    name: "mesa8",
    displayName: "Mesa VIII",
    users: [
      {
        displayName: "GJuarezF",
        fullName: "Gabriela Juárez Flores",
        cargoDescripcion: "Secretaria",
        titleTiempo: "Tiempo de revisión",

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Preautorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Revisión de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
      },
      {
        displayName: "JHernandezH",
        fullName: "Juan Hernández Hernández",
        cargoDescripcion: "Oficial Judicial",
        titleTiempo: "Tiempo de elaboración",
        cards: [
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Elaboración de acuerdo",
            value: "5 min.",
            caption: "Promedio",
          },
        ],
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

        cards: [
          {
            title: "Promociones hoy",
            value: 10,
            total: 35,
            caption: "Autorizados",
          },
          { title: "Acuerdos 2023", value: 705, caption: "Autorizados" },
          { title: "Acuerdos por día", value: 70, caption: "Promedio" },
          {
            title: "Autorización por acuerdo",
            value: "20 min.",
            caption: "Promedio",
          },
        ],
      },
    ],
  },
];
</script>
