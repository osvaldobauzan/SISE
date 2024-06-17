<template>
  <q-card style="min-width: 700px">
    <q-toolbar>
      <q-toolbar-title class="text-bold">Exhorto / Despacho</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section>
      <q-item-label header>
        Selecciona la Oficina de Correspondencia Común (OCC)
      </q-item-label>
      <q-item-label header>Selecciona el Órgano Jurisdiccional</q-item-label>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions>
      <q-space></q-space>
      <q-btn color="primary" label="Guardar" style="width: 120px"></q-btn>
      <q-btn flat label="Cancelar" style="width: 120px" v-close-popup></q-btn>
    </q-card-actions>
  </q-card>
</template>
