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
        v-if="tipoSentencia === 'pdf' && nombreArchivo"
        :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
        :src="nombreArchivo"
        :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
      />
      <q-scroll-area
        v-show="nombreArchivo && tipoSentencia == 'word'"
        :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
        class="q-ma-none q-pa-none"
      >
        <div class="col-10" id="containerWord"></div>
      </q-scroll-area>
      <q-inner-loading :showing="cargandoSentencia"></q-inner-loading>
    </q-card-section>
    <slot name="loading"></slot>
  </q-card>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useSentenciasStore } from "src/modules/sentencias/store/sentencias-store.js";
import { Utils } from "src/helpers/utils";
import { manejoErrores } from "src/helpers/manejo-errores";
import { noty } from "src/helpers/notify";
import * as docx from "docx-preview";

const sentenciasStore = useSentenciasStore();
const cargandoSentencia = ref(false);
const nombreArchivo = ref("");
const tipoSentencia = ref("");
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
    default: "Sentencia",
  },
  registrosSeleccionados:{
        default: ref([])
    }  
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
  cargandoArchivoSentencia();
});


async function cargandoArchivoSentencia() {
  try {
    cargandoSentencia.value = true;
    nombreArchivo.value = null;
    const parametrosArchivo = {
      asuntoNeunId: acuerdo.value.expediente.asuntoNeunId,
      asuntoDocumentoId: acuerdo.value.expediente.asuntoDocumentoId,
    };
    let index = await sentenciasStore.obtenerProyectoEnBase64(parametrosArchivo);
    if (sentenciasStore.proyectosBase64[index].archivoBase64) {
      if (
        sentenciasStore.proyectosBase64[index].proyectoNombre.includes(".pdf")
      ) {
        nombreArchivo.value = Utils.base64ToUrlObj(
          sentenciasStore.proyectosBase64[index].archivoBase64,
        );
        tipoSentencia.value = "pdf";
      } else {
        nombreArchivo.value = Utils.base64ToBlobWord(
          sentenciasStore.proyectosBase64[index].archivoBase64,
        );
        tipoSentencia.value = "word";
      }
    } else {
      noty.error("No se encontró el archivo");
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (tipoSentencia.value == "word") {
    renderWord();
  }
  cargandoSentencia.value = false;
}

async function renderWord() {
  const options = {
    renderHeaders: true, //enables headers rendering
    renderFooters: false, //enables footers rendering
    renderFootnotes: false, //enables footnotes rendering
    renderEndnotes: false, //enables endnotes rendering
  };
  if (document.getElementById("containerWord"))
    await docx.renderAsync(
      documentoPDF.value,
      document.getElementById("containerWord"),
      null,
      options,
    );
}

async function descargarArchivo() {
  const guid = acuerdo.value.expediente.guidDocumento;
  await sentenciasStore.descargarDocumentos(guid);
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
