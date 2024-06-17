<template>
  <q-card
    class="full-height"
    style="
      min-width: 95%;
      padding-top: 8px;
      padding-left: 8px;
      padding-bottom: 8px;
    "
  >
    <div v-if="!props.esEdicion && !props.esDetalle">
      <q-toolbar>
        <q-toolbar-title class="text-bold">
          Oficio
          {{ promocionDocumentos?.folio || "" }}/{{
            promocionDocumentos?.anio || ""
          }}
        </q-toolbar-title>
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
      :style="
        props.esDetalle
          ? 'height: 100%;  background-color: gray;'
          : 'height:  90%;  background-color: gray;'
      "
    >
      <!-- <div
        class="col-2 column justify-center items-stretch content-center"
        style="height: height: 100%;     background-color: gray;"
      >
        <div class="blue-grey-8" v-if="archivosAnexos.archivos?.length > 0">
          <div class="text-subtitle1">Archivos</div>
          <ul>
            <li
              style="cursor: pointer"
              v-for="(item, index) in archivosAnexos.archivos"
              :key="index"
            >
              <a
                class="text-body2"
                style="cursor: pointer; font-weight: bold"
                @click="mostrarPdf(item.ruta)"
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
                @click="mostrarPdf(item.ruta)"
              >
                {{ item.descripcion }}</a
              >
            </li>
          </ul>
        </div>
      </div> -->
      <div class="col-12" style="background-color: gray">
        <template v-if="documentoPDF">
          <q-pdfviewer
            :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
            :src="documentoPDF"
          />
        </template>
      </div>
    </div>
  </q-card>
</template>
<script setup>
import { Promocion } from "../data/promocion";
import { useLibretaStore } from "../stores/libreta-store";
//import { ArchivosAnexos } from 'src/data/archivos-anexos';
import { onMounted, ref, computed } from "vue";
import { loader } from "src/helpers/loader";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
const libretaStore = useLibretaStore();

// eslint-disable-next-line no-unused-vars
const props = defineProps({
  nombreArchivo: {
    type: String,
    required: true,
  },
  promocion: {
    type: Promocion,
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
//let archivosAnexos = ref({});
let documentoPDF = ref("");

onMounted(async () => {
  mostrarPdf("ruta");

  // loader.show();
  // documentoPDF.value = null;
  // archivosAnexos.value = {};
  // oficialiaStore.archivos = {};
  // oficialiaStore.archivoBase64 = [];
  // let tipoModulo = 1; //Promociones: 1 Acuerdos: 2
  // if (promocionDocumentos.value?.expediente) {
  //   try {
  //     await oficialiaStore.obtenerArchivosYAnexos(
  //       promocionDocumentos.value.expediente.asuntoNeunId,
  //       promocionDocumentos.value.yearPromocion,
  //       promocionDocumentos.value.numeroOrden,
  //       tipoModulo,
  //       null,
  //       promocionDocumentos.value.origen
  //     );
  //   } catch (error) {
  //     manejoErrores.mostrarError(error);
  //     loader.hide();
  //   }

  //   archivosAnexos.value = oficialiaStore.archivos;
  //   if (archivosAnexos.value.archivos.length > 0) {
  //     try {
  //       await oficialiaStore.recuperarArchivo(
  //         archivosAnexos.value.archivos[0].ruta
  //       );
  //     } catch (error) {
  //       manejoErrores.mostrarError(error);
  //     }
  //   } else if (archivosAnexos.value.anexos.length > 0) {
  //     try {
  //       await oficialiaStore.recuperarArchivo(
  //         archivosAnexos.value.anexos[0].ruta
  //       );
  //     } catch (error) {
  //       manejoErrores.mostrarError(error);
  //     }
  //   }

  //   if (oficialiaStore.archivoBase64.length > 0) {
  //     documentoPDF.value = Utils.base64ToUrlObj(oficialiaStore.archivoBase64);
  //   }
  // }
  loader.hide();
});

async function mostrarPdf(ruta) {
  loader.show();
  try {
    await libretaStore.recuperarArchivo(ruta);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  documentoPDF.value = Utils.base64ToUrlObj(libretaStore.archivoBase64);
  loader.hide();
}
</script>
<style>
.listaArchivos ul {
  list-style-type: none;
  padding-inline-start: 0px;
}
</style>
