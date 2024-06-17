<template>
  <q-card class="full-height" style="min-width: 95%">
    <div v-if="!props.esEdicion && !props.esDetalle">
      <q-toolbar>
        <q-toolbar-title class="text-bold">
          Promoción
          {{ promocionDocumentos?.numeroRegistro || "" }}</q-toolbar-title
        >
        <q-space> </q-space>
        <q-btn
          flat
          round
          dense
          icon="close"
          v-close-popup
          v-if="!props.esDetalle"
        />
      </q-toolbar>
    </div>
    <div
      class="listaArchivos row wrap justify-start items-stretch content-stretch"
      :style="'height: 100%; min-height:89vh;  background-color: gray;'"
    >
      <div
        class="col-2 column justify-center items-stretch content-center q-pa-md"
        style="height: height: 100%;  background-color: gray;"
      >
        <div class="blue-grey-8" v-if="archivosAnexos.archivos?.length > 0">
          <!-- <div class="text-subtitle1">Archivos</div> -->
          <ul>
            <li
              style="cursor: pointer"
              v-for="(item, index) in archivosAnexos.archivos"
              :key="index"
            >
              <a
                class="text-body2"
                style="cursor: pointer; font-weight: bold"
                @click="mostrarPdf(item.nombre)"
              >
                {{ item.descripcion }}</a
              >
            </li>
          </ul>
        </div>

        <div>
          <div class="text-subtitle1" v-if="archivosAnexos.anexos?.length > 0">
            Anexos
          </div>
          <ul>
            <li
              style="cursor: pointer"
              class="text-body2"
              v-for="(item, index) in archivosAnexos.anexos"
              :key="index"
            >
              <a
                class="text-body2"
                style="cursor: pointer; font-weight: bold"
                @click="mostrarPdf(item.nombre)"
              >
                {{ item.descripcion }}</a
              >
            </li>
          </ul>
        </div>
        <div class="blue-grey-8" v-if="archivosAnexos.electronicos?.length > 0">
          <div class="text-subtitle1">Electrónicos</div>
          <ul>
            <li
              style="cursor: pointer"
              v-for="(item, index) in archivosAnexos.electronicos"
              :key="index"
            >
              <a
                class="text-body2"
                style="cursor: pointer; font-weight: bold"
                @click="mostrarPdf(item.nombre)"
              >
                {{ item.descripcion }}</a
              >
            </li>
          </ul>
        </div>
      </div>
      <q-card-section class="col-10 q-pa-none" style="background-color: gray">
        <template v-if="documentoPDF && tipoDoc == tipoDocumento.PDF">
          <q-pdfviewer
            :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
            :src="documentoPDF"
          />
        </template>
        <q-scroll-area
          v-show="documentoPDF && tipoDoc == tipoDocumento.WORD"
          :style="`width: 100%; height: ${esEdicion ? '89.6vh' : '94.9vh'}`"
          class="q-pr-xs"
        >
          <div id="containerWord"></div>
        </q-scroll-area>
        <q-inner-loading :showing="cargando" />
      </q-card-section>
    </div>
  </q-card>
</template>
<script setup>
import { useOficialiaStore } from "../stores/oficialia-store";
import { onMounted, ref, computed } from "vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import * as docx from "docx-preview";
const oficialiaStore = useOficialiaStore();
const tipoDocumento = { WORD: "word", PDF: "pdf" };
const tipoDoc = ref(tipoDocumento.PDF);
const cargando = ref(false);
// eslint-disable-next-line no-unused-vars
const props = defineProps({
  nombreArchivo: {
    type: String,
  },
  promocion: {
    type: Object,
  },
  esDetalle: {
    type: Boolean,
  },
  esEdicion: {
    type: Boolean,
  },
});

const promocionDocumentos = computed({
  get() {
    return props.promocion;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});
let archivosAnexos = ref({});
let documentoPDF = ref("");
defineExpose({
  mostrarPdf,
});

onMounted(async () => { 
  cargando.value = true;
  documentoPDF.value = null;
  archivosAnexos.value = {};
  oficialiaStore.archivos = {};
  oficialiaStore.archivoBase64 = [];
  let tipoModulo = 1; //Promociones: 1 Acuerdos: 2
  if (promocionDocumentos.value) {
    const parametros = {
      asuntoNeunId:
        promocionDocumentos.value.asuntoNeunId ||
        promocionDocumentos.value.expediente?.asuntoNeunId ||
        0,
      anioPromocion: promocionDocumentos.value.yearPromocion,
      numeroOrden: promocionDocumentos.value.numeroOrden,
      tipoModulo: tipoModulo,
      origen:
        promocionDocumentos.value.origenPromocionId ||
        promocionDocumentos.value.origen,
      kIdElectronica: promocionDocumentos.value.kIdElectronica,
    };
    try {
      await oficialiaStore.obtenerArchivosYAnexos(
        promocionDocumentos.value.asuntoNeunId ||
          promocionDocumentos.value.expediente?.asuntoNeunId ||
          0,
        promocionDocumentos.value.yearPromocion,
        promocionDocumentos.value.numeroOrden,
        tipoModulo,
        null,
        promocionDocumentos.value.origenPromocionId ||
          promocionDocumentos.value.origen,
        promocionDocumentos.value.kIdElectronica,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
      cargando.value = false;
    }

    archivosAnexos.value = oficialiaStore.archivos;
    if (archivosAnexos.value.archivos?.length > 0) {
      try {
        await oficialiaStore.recuperarArchivo({
          ...parametros,
          nombre: archivosAnexos.value.archivos[0].nombre,
        });
        if (archivosAnexos.value.archivos[0].nombre.includes(".docx")) {
          tipoDoc.value = tipoDocumento.WORD;
        } else {
          tipoDoc.value = tipoDocumento.PDF;
        }
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
    } else if (archivosAnexos.value.anexos?.length > 0) {
      try {
        await oficialiaStore.recuperarArchivo({
          ...parametros,
          nombre: archivosAnexos.value.anexos[0].nombre,
        });
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
    } else if (archivosAnexos.value.electronicos?.length > 0) {
      try {
        await oficialiaStore.recuperarArchivo({
          ...parametros,
          nombre: archivosAnexos.value.electronicos[0].nombre,
        });
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
    }

    if (
      oficialiaStore.archivoBase64 &&
      oficialiaStore.archivoBase64.length > 0
    ) {
      if (tipoDoc.value == tipoDocumento.WORD) {
        documentoPDF.value = Utils.base64ToBlobWord(
          oficialiaStore.archivoBase64,
        );
        await rederWord();
      } else {
        documentoPDF.value = Utils.base64ToUrlObj(oficialiaStore.archivoBase64);
      }
    }
  }
  cargando.value = false;
});

async function mostrarPdf(nombre) {
  if (nombre.includes(".docx")) {
    tipoDoc.value = tipoDocumento.WORD;
  } else {
    tipoDoc.value = tipoDocumento.PDF;
  }
  cargando.value = true;
  let tipoModulo = 1; //Promociones: 1 Acuerdos: 2
  const parametros = {
    asuntoNeunId:
      promocionDocumentos.value?.asuntoNeunId ||
      promocionDocumentos.value?.expediente.asuntoNeunId ||
      0,
    anioPromocion: promocionDocumentos.value?.yearPromocion,
    numeroOrden: promocionDocumentos.value?.numeroOrden,
    tipoModulo: tipoModulo,
    origen:
      promocionDocumentos.value?.origenPromocionId ||
      promocionDocumentos.value?.origen,
    kIdElectronica: promocionDocumentos.value?.kIdElectronica,
    nombre: nombre,
  };
  try {
    await oficialiaStore.recuperarArchivo(parametros);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (tipoDoc.value == tipoDocumento.WORD) {
    documentoPDF.value = Utils.base64ToBlobWord(oficialiaStore.archivoBase64);
    await rederWord();
  } else {
    documentoPDF.value = Utils.base64ToUrlObj(oficialiaStore.archivoBase64);
  }
  cargando.value = false;
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
      documentoPDF.value,
      document.getElementById("containerWord"),
      null,
      options,
    );
}
</script>
<script>
export default {
  inheritAttrs: false,
};
</script>
<style>
.listaArchivos ul {
  list-style-type: none;
  padding-inline-start: 0px;
}
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
.docx-wrapper {
  padding: 0px;
}
</style>
