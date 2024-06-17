<template>
  <q-card style="min-width: 700px">
    <q-toolbar>
      <q-toolbar-title> Crear lista </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-card-section>
      <q-form class="q-gutter-md">
        <q-input filled v-model="fechaLista" label="Fecha de lista">
          <template v-slot:append>
            <q-icon name="event" />
          </template>
        </q-input>
        <q-input filled v-model="fechaSesion" label="Fecha de sesión">
          <template v-slot:append>
            <q-icon name="event" />
          </template>
        </q-input>
        <q-item-label header class="text-bold">Titular</q-item-label>
        <q-select
          filled
          v-if="!checkSecretarioEnFunciones"
          v-model="magistradoSelected"
          :options="optionsMagistrado"
          label="Magistrado"
        >
        </q-select>
        <q-toggle
          v-model="checkSecretarioEnFunciones"
          checked-icon="check"
          color="primary"
          label="Secretario en funciones"
        />
        <q-select
          filled
          v-if="checkSecretarioEnFunciones"
          v-model="secretarioEnFuncionesSelected"
          :options="optionsSecretarioEnFunciones"
          label="Secretario en funciones"
        ></q-select>

        <q-item-label header class="text-bold">Secretario</q-item-label>
        <q-select
          filled
          v-model="secretarioSelected"
          :options="optionsSecretario"
          label="Secretario"
        >
        </q-select>
      </q-form>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions class="q-gutter-xl q-px-lg">
      <q-btn
        class="col"
        unelevated
        no-caps
        color="primary"
        label="Guardar"
        v-close-popup
      ></q-btn>
      <q-btn
        class="col"
        outline
        no-caps
        color="primary"
        label="Cancelar"
        v-close-popup
      ></q-btn>
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref } from "vue";

const fechaLista = ref("");
const fechaSesion = ref("");
const magistradoSelected = ref("");
const secretarioSelected = ref("");
const secretarioEnFuncionesSelected = ref("");
const checkSecretarioEnFunciones = ref(false);

const optionsMagistrado = ref([
  { label: "Magistrado 1", value: "1" },
  { label: "Magistrado 2", value: "2" },
  { label: "Magistrado 3", value: "3" },
]);

const optionsSecretario = ref([
  { label: "Secretario 1", value: "1" },
  { label: "Secretario 2", value: "2" },
  { label: "Secretario 3", value: "3" },
]);

const optionsSecretarioEnFunciones = ref([
  { label: "Secretario funciones 1", value: "f1" },
  { label: "Secretario funciones 2", value: "f2" },
  { label: "Secretario funciones 3", value: "f3" },
]);
</script>
