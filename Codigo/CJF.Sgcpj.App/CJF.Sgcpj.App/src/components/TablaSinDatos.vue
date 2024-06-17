<template>
  <div
    class="column items-center justify-center"
    style="min-height: 50vh; min-width: 100%"
  >
    <q-icon size="6em" :name="icono" color="grey-6 q-mb-lg" />
    <div class="text-h4 text-secondary text-bold q-mb-md">{{ titulo }}</div>
    <div class="text-subtitle1">{{ subTitulo }}</div>
  </div>
</template>

<script setup>
// eslint-disable-next-line no-unused-vars
const props = defineProps({
  icono: {
    default: "mdi-filter",
  },
  titulo: {
    default: "Sin datos",
  },
  subTitulo: {
    default: "Sin datos",
  },
});
</script>

<style scoped lang="css"></style>
