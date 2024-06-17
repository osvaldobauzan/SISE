<template>
  <q-card>
    <q-toolbar v-if="esDialogo">
      <q-toolbar-title class="text-bold">
        {{ titulo }} -
        <q-btn
          flat
          no-caps
          icon="mdi-file-word"
          @click="descargarArchivo()"
          color="primary"
          label="Descargar Word"
        >
          <q-tooltip>Descargar Acuerdo en Word</q-tooltip>
        </q-btn>
      </q-toolbar-title>
      <q-space> </q-space>
      <slot> </slot>
      <q-btn flat round dense icon="close" v-close-popup />
    </q-toolbar>
    <q-card-section class="q-ma-none q-pa-none" :style="` min-height: 70vh`">
      <q-pdfviewer
        v-if="tipoAcuerdo === 'pdf' && nombreArchivo"
        :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
        :src="nombreArchivo"
        :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
      />
      <q-scroll-area
        v-show="nombreArchivo && tipoAcuerdo == 'word'"
        :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
        class="q-ma-none q-pa-none"
      >
        <div class="col-10" id="containerWord"></div>
      </q-scroll-area>
      <q-inner-loading :showing="cargandoAcuerdo"></q-inner-loading>
    </q-card-section>
    <slot name="loading"></slot>
  </q-card>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useTramiteStore } from "src/modules/tramite/store/tramite-store";
import { Utils } from "src/helpers/utils";
import { manejoErrores } from "src/helpers/manejo-errores";
import { noty } from "src/helpers/notify";
import * as docx from "docx-preview";

const tramiteStore = useTramiteStore();
const cargandoAcuerdo = ref(false);
const nombreArchivo = ref("");
const tipoAcuerdo = ref("");
const props = defineProps({
  // v-model
  modelValue: {
    default: {},
  },
  esDialogo: {
    type: Boolean,
    default: true,
  },
  titulo: {
    default: "Acuerdo",
  },
});

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
});

const acuerdo = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});
onMounted(() => {
  verAcuerdo();
});

async function verAcuerdo() {
  cargandoAcuerdo.value = true;
  nombreArchivo.value = null;
  tipoAcuerdo.value = null;
  try {
    const parametrosArchivo = {
      tipoModulo: 2,
      asuntoNeunId: acuerdo.value.expediente.asuntoNeunId,
      asuntoDocumentoId: acuerdo.value.asuntoDocumentoId,
    };
    await tramiteStore.obtenerArchivoAcuerdo(parametrosArchivo);
    if (tramiteStore.archivoAcuerdo?.anexos[0]?.guidDocumento) {
      await tramiteStore.obtenerAcuerdoEnBase64(
        tramiteStore.archivoAcuerdo.anexos[0].guidDocumento,
      );
    }
    if (tramiteStore.acuerdoBase64) {
      if (tramiteStore.acuerdoNombre.includes(".pdf")) {
        nombreArchivo.value = Utils.base64ToUrlObj(tramiteStore.acuerdoBase64);
        tipoAcuerdo.value = "pdf";
      } else {
        nombreArchivo.value = Utils.base64ToBlobWord(
          tramiteStore.acuerdoBase64,
        );
        tipoAcuerdo.value = "word";
      }
    } else {
      noty.error("No se encontró el archivo");
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (tipoAcuerdo.value == "word") {
    await rederWord();
  }
  cargandoAcuerdo.value = false;
}

async function rederWord() {
  const options = {
    renderHeaders: true, //enables headers rendering
    renderFooters: false, //enables footers rendering
    renderFootnotes: false, //enables footnotes rendering
    renderEndnotes: false, //enables endnotes rendering
  };
  if (document.getElementById("containerWord"))
    await docx.renderAsync(
      nombreArchivo.value,
      document.getElementById("containerWord"),
      null,
      options,
    );
}

async function descargarArchivo() {
  const guid = tramiteStore.archivoAcuerdo.anexos[0].guidDocumento;
  await tramiteStore.descargarDocumentos(guid);
}
</script>

<style scoped lang="css">
.docx_textoindependiente span div img {
  left: -65px !important;
  mix-blend-mode: multiply;
  z-index: -1;
}
.docx span div img {
  left: -65px !important;
  mix-blend-mode: multiply;
  z-index: -1;
}
</style>
