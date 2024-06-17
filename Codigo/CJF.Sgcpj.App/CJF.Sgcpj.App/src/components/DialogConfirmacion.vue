<template>
  <q-dialog v-model="value" persistent>
    <q-card style="min-width: 400px" class="dimensions">
      <q-card-section>
        <q-item-section class="items-center q-pb-xs">
          <q-icon :name="icon" :color="color" size="xl" />
        </q-item-section>
        <q-item-section class="q-mt-lg">
          <q-item-label class="textTitulo">{{ titulo }}</q-item-label>
          <q-item-label class="textSub q-pt-xs">{{ subTitulo }}</q-item-label>
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
          :label="labelBtnCancel"
          class="q-ml-sm boton"
          v-close-popup
        />
        <q-btn
          no-caps
          style="min-width: 164px"
          color="blue"
          class="boton"
          :label="labelBtnOk"
          @click="
            emit('aceptar');
            value = false;
          "
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup>
import { computed } from "vue";
const props = defineProps({
  // v-model
  modelValue: {
    default: false,
  },
  titulo: {
    default: "",
  },
  subTitulo: {
    default: "",
  },
  labelBtnOk: {
    default: "Sí",
  },
  labelBtnCancel: {
    default: "No",
  },
  showCancelar: {
    default: true,
  },
  icon: {
    default: "fa-solid fa-triangle-exclamation",
  },
  color: {
    default: "negative",
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
