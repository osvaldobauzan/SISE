<template>
  <q-card style="min-width: 90vw; min-height: 70vh">
    <q-splitter v-model="splitterModel" :before-class="full - height">
      <template v-slot:before>
        <q-scroll-area style="width: 100%; height: 65vh">
          <div class="col-10" id="containerWord"></div>
        </q-scroll-area>
      </template>

      <template v-slot:after>
        <div class="q-pl-md q-pr-md q-pb-sm">
          <q-toolbar>
            <q-toolbar-title>Oficio libre</q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <div class="row wrap q-pb-md">
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Autoridad</q-item-label>
                <q-item-label>{{ value.nombres }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Expediente</q-item-label>
                <q-item-label>{{ value?.expediente }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <q-item-section>
            <q-editor v-model="value.text" min-height="40vh" />
          </q-item-section>
          <q-card-actions align="left">
            <q-btn
              :color="!formValido ? 'grey-6' : 'primary'"
              @click="formValido ? submitForm() : null"
              :disable="!formValido"
              :label="'Guardar'"
              style="min-width: 164px"
              class="q-ml-sm"
            />
            <q-btn
              v-close-popup
              outline
              label="Cancelar"
              :color="'secondary'"
              style="min-width: 164px"
            />
          </q-card-actions>
        </div>
      </template>
    </q-splitter>
  </q-card>
</template>

<script setup>
import {
  computed,
  watch,
  onMounted,
  onUpdated,
  onBeforeUnmount,
  ref,
} from "vue";
import * as docx from "docx-preview";

const splitterModel = ref(50);
const props = defineProps({
  // v-model
  modelValue: {
    default: "",
  },
});

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
});

const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});

const stopWatch = watch(
  () => props.modelValue,
  // eslint-disable-next-line no-unused-vars
  async (_newValue, _oldValue) => {
    // do something
  },
  {
    immediate: true,
  },
);

onMounted(() => {
  if (value.value.archivo) {
    onGetFile(value.value.archivo);
  }
});

onUpdated(() => {});

onBeforeUnmount(() => {
  stopWatch();
});
async function onGetFile(file) {
  const options = {
    //inWrapper: false,
    // ignoreWidth: true,
    //   ignoreHeight: true,
    renderHeaders: true, //enables headers rendering
    renderFooters: false, //enables footers rendering
    renderFootnotes: false, //enables footnotes rendering
    renderEndnotes: false, //enables endnotes rendering
  };
  await docx.renderAsync(
    file,
    document.getElementById("containerWord"),
    null,
    options,
  );
}
</script>

<style scoped>
:deep(.q-splitter--vertical > .q-splitter__panel) {
  height: unset;
}
.docx_textoindependiente span div img {
  left: -65px !important;
  mix-blend-mode: multiply;
}
</style>
