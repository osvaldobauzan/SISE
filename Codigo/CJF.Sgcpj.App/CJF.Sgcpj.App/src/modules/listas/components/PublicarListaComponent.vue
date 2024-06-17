<template>
  <q-card>
    <q-splitter v-model="splitterModel">
      <template v-slot:before>
        <ViewPdfComponent :nombreArchivo="nombreArchivo" class="fit" />
      </template>

      <template v-slot:after>
        <q-toolbar class="bg-white">
          <q-toolbar-title> Publicar </q-toolbar-title>
          <q-btn flat round icon="mdi-close" v-close-popup></q-btn>
        </q-toolbar>
        <q-separator></q-separator>
        <q-card-actions>
          <q-space></q-space>
          <q-btn dense no-caps unelevated color="primary" class="q-px-lg"
            >Publicar</q-btn
          >
          <q-btn dense no-caps outline color="primary" class="q-px-md"
            >Cancelar</q-btn
          >
        </q-card-actions>
        <q-card-section>
          <q-table
            bordered
            flat
            :row-key="expediente"
            :rows="rows"
            :columns="columns"
          >
            <template v-slot:body="props">
              <q-tr>
                <q-td>
                  <q-item class="q-pl-none">
                    <q-item-section>
                      <q-item-label>
                        {{ props.row.expediente }}
                      </q-item-label>
                      <q-item-label caption>
                        {{ props.row.catTipoAsunto }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </q-td>
                <q-td>
                  <q-checkbox v-model="props.row.identidad" />
                </q-td>
                <q-td>
                  <q-item class="q-pl-none">
                    <q-item-section>
                      <q-item-label>
                        {{ props.row.parte }}
                      </q-item-label>
                      <q-item-label caption>
                        {{ props.row.caracterParte }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </q-td>
              </q-tr>
            </template>
          </q-table>
        </q-card-section>
      </template>
    </q-splitter>
    <q-split></q-split>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import ViewPdfComponent from "src/components/ViewPdfComponent.vue";

const splitterModel = ref(50);
const nombreArchivo = ref("./docs/SentenciaEjemplo.pdf");

const rows = [
  {
    expediente: "10/2024",
    catTipoAsunto: "Amparo indirecto",
    cuaderno: "Principal",
    identidad: true,
    parte: "María Hernández Pérez",
    caracterParte: "Quejoso",
  },
  {
    expediente: "120/2024",
    catTipoAsunto: "Amparo indirecto",
    cuaderno: "Principal",
    identidad: true,
    parte: "Pedro Pérez López",
    caracterParte: "Quejoso",
  },
  {
    expediente: "1056/2024",
    catTipoAsunto: "Amparo indirecto",
    cuaderno: "Principal",
    identidad: true,
    parte: "Alfredo Fuentes Barrera",
    caracterParte: "Quejoso",
  },
  {
    expediente: "124/2024",
    catTipoAsunto: "Amparo indirecto",
    cuaderno: "Principal",
    identidad: true,
    parte: "Ricardo Hernández Pérez",
    caracterParte: "Quejoso",
  },
];

const columns = [
  {
    name: "expediente",
    required: true,
    label: "Expediente",
    align: "left",
    field: "expediente",
  },
  {
    name: "identidad",
    required: true,
    label: "Identidad reservada",
    align: "left",
    field: "identidad",
  },
  {
    name: "quejoso",
    required: true,
    label: "Quejoso/Promovente",
    align: "left",
    field: "quejoso",
  },
];
</script>
