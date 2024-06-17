<template>
  <q-card bordered flat>
    <q-card-section>
      <div class="q-gutter-md">
        <q-btn flat color="grey-8" icon="mdi-notebook" size="xl" to="oficios">
          <q-tooltip>Libreta de oficios</q-tooltip>
        </q-btn>
        <q-btn
          flat
          color="grey-8"
          icon="mdi-arrow-decision"
          size="xl"
          to="seguimiento"
        >
          <q-tooltip>Seguimiento de expedientes</q-tooltip>
        </q-btn>
        <q-btn flat color="grey-8" icon="mdi-folder" size="xl" to="expediente">
          <q-tooltip>Expediente electrónico</q-tooltip>
        </q-btn>
        <q-btn
          flat
          color="grey-8"
          icon="mdi-calculator"
          size="xl"
          to="calculadoras"
        >
          <q-tooltip>Calculadoras</q-tooltip>
        </q-btn>
        <q-btn flat color="grey-8" icon="mdi-cog" size="xl" to="configuracion">
          <q-tooltip>Configuración</q-tooltip>
        </q-btn>
      </div>
    </q-card-section>
  </q-card>
</template>

<script setup></script>
