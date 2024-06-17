<template>
  <div
    class="column items-center justify-center"
    v-if="!documentoPDF && !cargandoProyecto"
    style="min-height: 100%; min-width: 100%"
  >
    <q-icon size="6em" name="mdi-file" color="grey-6 q-mb-lg" />
    <div class="text-h4 text-secondary text-bold q-mb-md">Sin datos</div>
    <div class="text-subtitle1">No hay documentos.</div>
  </div>
  <q-pdfviewer
    v-if="tipoProyecto === 'pdf' && documentoPDF"
    :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
    :src="documentoPDF"
    :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
  />
  <q-scroll-area
    v-show="documentoPDF && tipoProyecto == 'word'"
    :style="`width: 100%; height: ${esDialogo ? '89.6vh' : '94.9vh'}`"
    class="q-ma-none q-pa-none"
  >
    <div class="col-10" id="containerWord"></div>
  </q-scroll-area>
  <q-inner-loading :showing="cargandoProyecto"></q-inner-loading>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { Utils } from "src/helpers/utils";
import * as docx from "docx-preview";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useProyectosStore } from "../store/proyectos-store.js";

const cargandoProyecto = ref(false);
const documentoPDF = ref(null);
const tipoProyecto = ref("");
const nombreArchivo = ref("./docs/Version1.pdf");
const ProyectosStore = useProyectosStore();
const props = defineProps({
  id: {
    type: Number,
    required: true,
  },
});

onMounted(() => {
  cargandoArchivoDeProyecto();
});

async function cargandoArchivoDeProyecto() {
  try {
    cargandoProyecto.value = true;
    nombreArchivo.value = null;
    let index = await ProyectosStore.obtenerProyectoEnBase64(props.id);
    if (ProyectosStore.proyectosBase64[index].archivoBase64) {
      if (
        ProyectosStore.proyectosBase64[index].proyectoNombre.includes(".pdf")
      ) {
        documentoPDF.value = Utils.base64ToUrlObj(
          ProyectosStore.proyectosBase64[index].archivoBase64,
        );
        tipoProyecto.value = "pdf";
      } else {
        documentoPDF.value = Utils.base64ToBlobWord(
          ProyectosStore.proyectosBase64[index].archivoBase64,
        );
        tipoProyecto.value = "word";
      }
    } else {
      noty.error("No se encontró el archivo");
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (tipoProyecto.value == "word") {
    renderWord();
  }
  cargandoProyecto.value = false;
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
</script>
