<template>
  <q-card style="min-width: 60%">
    <q-toolbar>
      <q-toolbar-title> Resultado sesión </q-toolbar-title>
      <q-btn flat round icon="mdi-close" v-close-popup></q-btn>
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section>
      <div class="row">
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption> Expediente </q-item-label>
              <q-item-label class="text-bold">10/2023</q-item-label>
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption> Tipo asunto</q-item-label>
              <q-item-label class="text-bold">Amparo indirecto</q-item-label>
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption> Quejoso </q-item-label>
              <q-item-label class="text-bold"
                >José Luis Vargas Valdez</q-item-label
              >
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption> Autoridad </q-item-label>
              <q-item-label class="text-bold"
                >Instituto Mexicano del Seguro Social</q-item-label
              >
            </q-item-section>
          </q-item>
        </div>
      </div>
    </q-card-section>
    <q-card-section>
      <div class="row">
        <div class="col q-gutter-md">
          <q-radio v-model="resultado" val="sentido" label="Sentido" />
          <q-radio v-model="resultado" val="retirado" label="Retirado" />
          <q-radio v-model="resultado" val="desechado" label="Desechado" />
          <q-radio v-model="resultado" val="aplazado" label="Aplazado" />
          <q-radio v-model="resultado" val="aprobado" label="Aprobado" />
          <q-radio v-model="resultado" val="enlista" label="En lista" />
          <q-radio v-model="resultado" val="returnado" label="Returnado" />
        </div>
      </div>
      <div class="row">
        <div class="col">
          <q-select
            filled
            dense
            v-if="resultado === 'sentido'"
            v-model="model"
            :options="sentidoOptions"
            label="Sentido"
          />
        </div>
      </div>
      <q-item-label header
        >Opcionalmente agrega una descripción breve del sentido.</q-item-label
      >
      <q-editor v-model="editor" min-height="5rem" />
    </q-card-section>
  </q-card>
</template>

<script setup>
import { ref } from "vue";

const editor = ref("");
const model = ref("Ampara");
const resultado = ref("sentido");

const sentidoOptions = ["Ampara", "No ampara", "Sobresee"];
</script>
