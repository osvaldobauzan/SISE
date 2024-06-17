<template>
	<div class="row justify-center">
	  <div class="q-pa-md q-gutter-sm row">
		<q-btn
		  no-caps
		  class="button"
		  style="margin: 2px;"
		  @click="$emit('today')"
		>
		  Hoy
		</q-btn>
		<q-btn
		  no-caps
		  class="button"
		  style="margin: 2px;"
		  @click="$emit('prev')"
		  icon="mdi-chevron-left"
		>
		<q-tooltip>Anterior</q-tooltip>
		</q-btn>
		<q-btn
		  no-caps
		  class="button"
		  style="margin: 2px;"
		  @click="$emit('next')"
		  icon="mdi-chevron-right"
		>
		<q-tooltip>Siguiente</q-tooltip>
		</q-btn>
	  </div>
	</div>
  </template>

<script setup>
</script>