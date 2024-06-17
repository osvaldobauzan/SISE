<template>
  <q-card
    :style="`min-width:50%; width: 900px; max-width: 80vw; min-height: ${esDialogo ? '95%' : '100%'}`"
  >
    <q-toolbar v-if="esDialogo">
      <q-toolbar-title class="text-bold"> {{ titulo }} </q-toolbar-title>
      <q-space> </q-space>
      <slot> </slot>
      <q-btn flat round dense icon="close" v-close-popup />
    </q-toolbar>
    <q-card-section class="q-ma-none q-pa-none set-75-vh">
      <q-pdfviewer
        v-if="tipoArchivo === 'pdf' && nombreArchivo"
        :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
        :src="nombreArchivo"
        :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
      />
      <q-scroll-area
        v-show="nombreArchivo && tipoArchivo === 'word'"
        :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
        class="q-ma-none q-pa-none"
      >
        <div class="col-10" id="containerWord"></div>
      </q-scroll-area>
      <slot name="loading"> </slot>
    </q-card-section>
  </q-card>
</template>

<script setup>
import { onMounted } from "vue";
import * as docx from "docx-preview";

// eslint-disable-next-line no-unused-vars
const props = defineProps({
  nombreArchivo: {
    type: Object,
  },
  titulo: {
    type: String,
    required: true,
  },
  esDialogo: {
    type: Boolean,
    default: true,
  },
  tipoArchivo: {
    type: String,
    default: "pdf",
  },
});

onMounted(() => {
  if (props.tipoArchivo === "word") {
    rederWord();
  }
});

async function rederWord() {
  const options = {
    renderHeaders: true, //enables headers rendering
    renderFooters: false, //enables footers rendering
    renderFootnotes: false, //enables footnotes rendering
    renderEndnotes: false, //enables endnotes rendering
  };
  if (document.getElementById("containerWord"))
    await docx.renderAsync(
      props.nombreArchivo,
      document.getElementById("containerWord"),
      null,
      options,
    );
}
</script>

<style>
.set-75-vh {
  min-height: 75vh;
}
</style>