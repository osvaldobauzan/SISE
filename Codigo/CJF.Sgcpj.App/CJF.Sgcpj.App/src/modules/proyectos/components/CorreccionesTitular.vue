<template>
  <q-card style="min-width: 700px">
    <q-form ref="form" @submit="guardar">
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
                  item?.expediente.asuntoAlias
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label caption>Tipo de asunto</q-item-label>
                <q-item-label class="text-bold">{{
                  item?.expediente.catTipoAsunto
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label caption>Cuaderno</q-item-label>
                <q-item-label class="text-bold">{{
                  item?.expediente.cuaderno
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

        <q-item v-if="item.estado != estatusProyecto.Aprobado">
          <q-item-section class="fileUp">
            <q-item-label> Agrega el documento de corrección </q-item-label>
            <q-item-label
              :style="file !== null ? '' : 'border: 3px dashed #ccc'"
            >
              <q-file
                v-model="file"
                @update:model-value="cambioForm"
                @clear:model-value="cambioForm"
                borderless
                class="full-width full-height"
                accept=".doc, .docx, .pdf"
                max-file-size="30000000"
              >
                <template v-if="!file" v-slot:prepend>
                  <div class="row label-file">
                    <div class="col">
                      <q-item-label>
                        <q-icon name="mdi-upload" />
                        Arrastra y suelta o
                        <q-btn no-caps flat padding="0px" color="light-blue">
                          busca un archivo
                        </q-btn>
                      </q-item-label>
                    </div>
                  </div>
                </template>
                <template v-slot:file>
                  <q-chip class="full-width full-height q-my-xs" square>
                    <q-avatar>
                      <q-icon :name="'insert_drive_file'" color="primary" />
                    </q-avatar>
                    <span class="ellipsis relative-position text-bold">
                      {{ file.name }}
                    </span>
                    <span
                      class="q-ml-xs text-grey q-pl-sm text-caption"
                      style="width: 15%"
                    >
                      {{
                        file.size / 1024 < 1024
                          ? (file.size / 1024).toFixed(1) + "KB"
                          : (file.size / 1024 / 1024).toFixed(1) + "MB"
                      }}
                    </span>
                    <q-tooltip>
                      {{ file.name }}
                    </q-tooltip>
                  </q-chip>
                </template>
                <template v-if="file" v-slot:after>
                  <q-item
                    dense-toggle
                    class="q-field-after"
                    clickable
                    @click="updateFile(null)"
                  >
                    <q-item-section align="left">
                      <q-icon
                        size="1.2em"
                        :name="'mdi-close'"
                        color="primary"
                      />
                    </q-item-section>
                  </q-item>
                </template>
              </q-file>
            </q-item-label>
            <q-item-label class="q-mt-xs">
              <q-icon name="mdi-information" color="info" class="q-mr-sm" />
              <span class="text-grey-8">
                Sólo puedes subir archivos menores a 30 Mb en formato .doc,
                .docx o .pdf
              </span>
            </q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label> O escribe los comentarios</q-item-label>
            <q-field class="full-width" v-model="comentarioTitular">
              <q-editor
                :toolbar="[['undo', 'redo']]"
                @update:model-value="(val) => updateQEditor(val)"
                toolbar-outline="true"
                class="full-width"
                v-model="comentarioTitular"
              >
              </q-editor>
            </q-field>
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
          @click="guardar"
        ></q-btn>
        <q-btn
          class="col"
          outline
          no-caps
          label="Cancelar"
          v-close-popup
        ></q-btn>
      </q-card-actions>
      <q-inner-loading :showing="uploadingFiles"></q-inner-loading>
    </q-form>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { noty } from "src/helpers/notify";
import { useProyectosStore, estatusProyecto } from "../store/proyectos-store";
import { Utils } from "src/helpers/utils";

const proyectosStore = useProyectosStore();
const comentarioTitular = ref("");
const file = ref(null);
const form = ref(null);
const uploadingFiles = ref(false);

const emit = defineEmits({
  refrescar: (value) => value !== null,
  cancelar: (value) => value !== null,
  cerrar: (value) => value !== null,
});

const props = defineProps({
  item: {
    type: Object,
    required: false,
    default: null,
  },
  proyectoId: {
    type: Number,
    required: true,
  },
  estado: {
    type: Number,
    required: true,
  },
});

async function updateFile(newFile) {
  file.value = await Utils.fileToBlob(newFile);
  cambioForm();
}

async function updateQEditor(value) {
  comentarioTitular.value = value;
  cambioForm();
}

function cleanString(string) {
  return string.replaceAll(/&nbsp;/g, " ").replaceAll(/<[^>]*>?/gm, "");
}

async function guardar() {
  uploadingFiles.value = true;
  let data = new FormData();

  data.append("asuntoNeunId", props.item.expediente.asuntoNeunId);
  data.append("proyectoId", props.proyectoId);
  data.append("catOrganismoId", props.item.expediente.catOrganismoId);
  data.append("estadoId", props.item.estado);
  data.append("correcciones", cleanString(comentarioTitular.value));

  if (file.value)
    data.append(file.value.name, Utils.blobToFile(file.value), file.value.name);

  try {
    await proyectosStore.addCorreccionVersion(data);
    noty.correcto("Corrección subida correctamente.");
  } catch (error) {
    noty.error("Error al subir la versión del proyecto.");
  }
  emit("refrescar");
  uploadingFiles.value = false;
}
</script>
<style>
.q-file {
  [role="alert"] {
    text-align: center;
    margin-bottom: 10px;
  }
}
.fileUp {
  .q-field__bottom {
    margin-bottom: 1rem;
  }
}
</style>
