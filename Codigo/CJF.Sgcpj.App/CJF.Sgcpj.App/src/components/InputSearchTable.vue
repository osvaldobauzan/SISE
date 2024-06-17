<template>
  <q-input
    :class="styleClass"
    dense
    rounded
    outlined
    bg-color="white"
    v-model="value"
    placeholder="Buscar"
    @keyup.enter="search()"
    max-width="100px"
  >
    <template v-slot:append>
      <q-icon
        v-if="value?.trim()"
        class="cursor-pointer"
        name="mdi-close"
        @click="(value = ''), search()"
      />
      <q-icon class="cursor-pointer" name="mdi-magnify" @click="search()" />
    </template>
  </q-input>
</template>

<script setup>
import { computed } from "vue";

const props = defineProps({
  // v-model
  modelValue: {
    default: "",
  },
  styleClass: {
    default: "col-xs-12 col-sm-3 col-md-2 q-pt-md q-pr-xs",
  },
});

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  onSearch: () => true,
});

const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});
function search() {
  emit("onSearch");
}
</script>

<style scoped></style>
