<template>
  <q-card style="min-width: 700px">
    <q-toolbar>
      <q-toolbar-title> Subir correcciones </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section>
      <q-toolbar>
        <q-toolbar-title class="text-subtitle1 text-bold">
          Datos del expediente
        </q-toolbar-title>
      </q-toolbar>
      <q-item>
        <div class="row full-width">
          <q-item class="col-4">
            <q-item-section>
              <q-item-label caption>Expediente</q-item-label>
              <q-item-label class="text-bold">{{
                item?.AsuntoAlias
              }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item class="col-4">
            <q-item-section>
              <q-item-label caption>Tipo de asunto</q-item-label>
              <q-item-label class="text-bold">{{
                item?.CatTipoAsunto
              }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item class="col-4">
            <q-item-section>
              <q-item-label caption>Cuaderno</q-item-label>
              <q-item-label class="text-bold">{{
                item?.Cuaderno
              }}</q-item-label>
            </q-item-section>
          </q-item>
        </div>
      </q-item>
      <q-toolbar>
        <q-toolbar-title class="text-subtitle1 text-bold">
          Datos del proyecto
        </q-toolbar-title>
      </q-toolbar>

      <q-item>
        <q-item-section>
          <q-item-label>
            Sube el archivo word con las correcciones
          </q-item-label>
          <q-item-label>
            <q-uploader
              flat
              auto-upload
              url="http://localhost:4444/upload"
              class="full-width"
              color="transparent"
              @uploaded="onUploaded"
              @finish="onFinish"
              style="
                border-radius: 7;
                border-style: dashed;
                border-width: 2px;
                border-color: #cfcfcf;
              "
            >
              <template v-slot:header="scope">
                <q-item>
                  <q-item-section side>
                    <q-icon name="mdi-upload" color="grey-8" />
                  </q-item-section>
                  <q-item-section>
                    <q-item-label class="text-grey-8">
                      Arrastra y suelta el archivo aquí o
                      <q-btn
                        type="a"
                        flat
                        dense
                        no-caps
                        accept=".pdf, application/pdf"
                        class="q-pa-none"
                        color="info"
                        @click="scope.pickFiles"
                        label="busca un archivo"
                      >
                        <q-uploader-add-trigger
                      /></q-btn>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </template>
            </q-uploader>
          </q-item-label>
          <q-item-label class="q-mt-xs">
            <q-icon name="mdi-information" color="info" class="q-mr-sm" />
            <span class="text-grey-8"
              >Sólo puedes subir archivos menores a 20 Mb en formato PDF</span
            >
          </q-item-label>
        </q-item-section>
      </q-item>
      <q-item>
        <q-item-section>
          <q-item-label> Escribe los comentarios </q-item-label>
          <q-editor v-model="sintesis" style="height: 100px"></q-editor>
        </q-item-section>
      </q-item>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions class="q-gutter-xl q-px-lg">
      <q-btn
        class="col"
        unelevated
        no-caps
        color="primary"
        label="Subir"
        v-close-popup
        @click="subirCorreccion"
      ></q-btn>
      <q-btn class="col" outline no-caps label="Cancelar" v-close-popup></q-btn>
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { noty } from "src/helpers/notify";
import { useSentenciasStore } from "../store/sentencias-store";

const sentenciasStore = useSentenciasStore();
const sintesis = ref("");
const file = ref(null);

const props = defineProps({
  item: {
    type: Object,
    required: false,
    default: null,
  },
  version: {
    type: String,
    required: true,
  },
});

function onUploaded(data) {
  file.value = data.files[0].name;
}

function subirCorreccion() {
  const data = {
    correccionFecha: new Date(),
    comentarioTitular: sintesis.value,
    correccionArchivo: file.value,
  };
  sentenciasStore.addCorreccion(props.item, data, props.version);
  noty.correcto("Proyecto subido correctamente.");
}
</script>
