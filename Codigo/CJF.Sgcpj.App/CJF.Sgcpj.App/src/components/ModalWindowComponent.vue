<template>
  <q-card
    class="bg-blue-grey-1"
    :style="dialogStyle"
    style="resize: both; max-width: 100%;"
  >
    <q-bar class="bg-primary text-white" v-touch-pan.mouse="onPan">
      <q-space></q-space>
      <!-- <q-btn
        dense
        flat
        icon="minimize"
        @click="$emit('toggle-maximized', false)"
        :disable="!maximizedToggle"
      >
        <q-tooltip v-if="maximizedToggle" class="bg-white text-primary"
          >Minimizar</q-tooltip
        >
      </q-btn> -->
      <q-btn dense flat icon="minimize" v-close-popup>
        <q-tooltip class="bg-white text-primary">Minimizar</q-tooltip>
      </q-btn>
      <q-btn
        dense
        flat
        :icon="maximizedToggle ? 'mdi-window-restore' : 'mdi-window-maximize' "
        @click="
          dialogPos.x = 0;
          dialogPos.y = 0;
          $emit('toggle-maximized', !maximizedToggle);
        "
      >
        <q-tooltip class="bg-white text-primary"
          >{{ maximizedToggle ? 'Restaurar' : 'Maximizar' }}</q-tooltip
        >
      </q-btn>
    </q-bar>
    <slot></slot>
  </q-card>
</template>

<script setup>
import { ref, computed, onActivated } from "vue";

const emit = defineEmits(['toggle-maximized']);

defineProps({
  maximizedToggle: Boolean,
});

const dialogPos = ref({
  x: 0,
  y: 0,
});

const dialogStyle = computed(() => {
  return {
    transform: `translate(${dialogPos.value.x}px, ${dialogPos.value.y}px)`,
  };
});

onActivated(() => {
  emit("toggle-maximized", false);
  dialogPos.value = {
    x: 0,
    y: 0,
  };
});

function onPan(evt) {
  dialogPos.value = {
    x: dialogPos.value.x + evt.delta.x,
    y: dialogPos.value.y + evt.delta.y,
  };
}
</script>
