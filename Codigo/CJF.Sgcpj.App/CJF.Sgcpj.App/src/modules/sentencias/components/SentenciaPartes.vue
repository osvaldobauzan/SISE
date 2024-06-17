<template>
  <q-card>
    <q-toolbar>
      <q-item>
        <q-item-section side>
          <q-btn flat bordered round color="secondary" icon="mdi-paperclip" />
        </q-item-section>
        <q-item-section>
          <q-item-label caption>Acuerdo</q-item-label>
          <q-item-label class="text-h6 text-bold">
            Solicita Copias</q-item-label
          >
        </q-item-section>
      </q-item>
      <q-space></q-space>
      <q-item>
        <q-item-section side>
          <q-btn flat round dense icon="mdi-folder" v-close-popup />
        </q-item-section>
        <q-item-section>
          <q-item-label>4/2023</q-item-label>
          <q-item-label caption>Expediente</q-item-label>
        </q-item-section>
      </q-item>
      <q-item>
        <q-item-section side>
          <q-btn flat round dense color="secondary" icon="mdi-paperclips" />
        </q-item-section>
        <q-item-section>
          <q-item-label>03</q-item-label>
          <q-item-label caption>Promociones</q-item-label>
        </q-item-section>
      </q-item>
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section class="q-gutter-sm">
      <q-select
        filled
        v-model="secretario"
        :options="[
          'Gerardo Ortiz Delgado',
          'Alberto Becerril Fuentes',
          'José Ignacio Salinas Diaz',
        ]"
        label="Secretario"
        option-label="name"
      />
      <q-input filled v-model="mesa" label="Mesa"></q-input>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions align="right">
      <q-btn color="primary" label="Guardar" />
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
const secretario = ref("");
const mesa = ref("");
</script>
