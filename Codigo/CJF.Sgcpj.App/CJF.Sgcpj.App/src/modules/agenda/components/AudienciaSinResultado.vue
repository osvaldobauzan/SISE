<template>
  <q-card style="min-width: 400px" class="dimensions">
    <q-card-section>
      <q-item-section class="items-center q-pb-xs">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="negative"
          size="xl"
        />
      </q-item-section>
      <q-item-section class="q-mt-lg">
        <q-item-label class="textTitulo"
          >Audiencia anterior sin estado</q-item-label
        >
        <q-item-label class="textSub q-pt-xs"
          >Se ha detectado una Audiencia sin resultado para el expediente
          {{ modelValue.expediente }} en la fecha {{ modelValue.fecha }}. Te
          recordamos que para agendar una audiencia debe tener marcado el estado
          de la anterior. ¿Deseas marcar un estado ahora?</q-item-label
        >
      </q-item-section>
    </q-card-section>
    <q-card-actions align="center" class="q-mb-md">
      <q-btn
        v-if="showCancelar"
        no-caps
        style="min-width: 164px"
        outline
        color="blue"
        @click="
          emit('cancelar');
          value = false;
        "
        label="No"
        class="q-ml-sm boton"
        v-close-popup
      />
      <q-btn
        no-caps
        style="min-width: 164px"
        color="blue"
        class="boton"
        label="Sí"
        @click="
          emit('aceptar');
          value = false;
        "
      />
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { computed } from "vue";
const props = defineProps({
  // v-model
  modelValue: {
    default: {},
  },
});

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value,
  aceptar: (value) => value !== null,
  cancelar: (value) => value !== null,
});

const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});
</script>

<style>
.dimensions {
  text-align: center;
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 556px;
  height: 288px;
  border-radius: 6px;
  gap: 10px;
}

.boton {
  position: relative;
  width: 164px;
  height: 40px;
  border-radius: 2px;
  padding: 8px 16px 8px 16px;
  gap: 8px;
  margin: 0px 20px;
}

.textTitulo {
  font-weight: 700;
  font-size: 20px;
  line-height: 19.24px;
}

.textSub {
  font-weight: 400;
  font-size: 14px;
  line-height: 16px;
}
</style>
