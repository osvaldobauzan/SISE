<template>
  <q-card class="card-fixed-width">
    <q-toolbar>
      <q-toolbar-title>Firma Electrónica</q-toolbar-title>
      <q-space />
      <q-btn flat round dense icon="close" v-close-popup />
    </q-toolbar>
    <q-tabs
      no-caps
      stretch
      switch-indicator
      v-model="tabName"
      align="left"
      class="q-mt-lg bg-grey-4"
      active-class="bg-white"
      indicator-color="primary"
    >
      <q-tab name="pfx" label="PFX" class="q-px-xl" />
      <q-tab name="keycer" label="KEY - CER" class="q-px-xl" />
    </q-tabs>
    <q-tab-panels class="bg-white" v-model="tabName" animated>
      <q-tab-panel name="pfx" class="q-gutter-md">
        <div :style="file !== null ? '' : 'border: 3px dashed #ccc'">
          <q-file
            :readonly="edicion && file !== null && file?.name == fileCopy?.name"
            ref="fileAcuerdo"
            :model-value="file"
            borderless
            @update:model-value="(val) => updateFiles(val, index)"
            class="full-width full-height"
            accept=".docx"
            max-file-size="30000000"
            @rejected="(err) => manejoErrores.archivoInvalido(err, 'Word')"
            :rules="[
              (val) => Validaciones.validaInputRequerido(val),
              async (val) => await Validaciones.validaExtension(val, 'docx'),
            ]"
          >
            <template v-if="!file" v-slot:prepend>
              <div class="row label-file">
                <div class="col">
                  <q-item-label
                    ><q-icon name="mdi-upload" size="sm" />Arrastra y suelta o
                    <q-btn no-caps flat padding="0px" color="light-blue"
                      >busca un archivo</q-btn
                    ></q-item-label
                  >
                </div>
              </div>
            </template>
            <template v-slot:file>
              <q-chip class="full-width full-height q-my-xs" square>
                <q-avatar>
                  <q-icon :name="'insert_drive_file'" color="primary" />
                </q-avatar>
                <div style="width: 30%" class="ellipsis relative-position">
                  <span class="text-bold text-body2">{{ file.name }}</span>
                  <span
                    class="q-ml-md text-grey text-caption"
                    style="width: 15%"
                    >{{
                      file.size / 1024 < 1024
                        ? (file.size / 1024).toFixed(1) + "KB"
                        : (file.size / 1024 / 1024).toFixed(1) + "MB"
                    }}</span
                  >
                </div>
                <q-tooltip>
                  {{ file.name }}
                </q-tooltip>
              </q-chip>
            </template>
            <template v-if="file" v-slot:after>
              <q-btn
                v-if="
                  edicion && (fileCopy == null || file?.name == fileCopy?.name)
                "
                dense-toggle
                class="q-field-after"
                color="blue"
                flat
                dense
                no-caps
                @click="
                  fileCopy = fileCopy ? fileCopy : file;
                  updateFiles(null);
                "
              >
                <q-tooltip>Reemplazar</q-tooltip>
                <q-item-section
                  class="text-caption text-capitalize items-center justify"
                >
                  <q-icon :name="'mdi-replay'" color="blue" />
                  Reemplazar
                </q-item-section>
              </q-btn>
              <q-item
                v-else
                dense-toggle
                class="q-field-after"
                clickable
                @click="updateFiles(null)"
              >
                <q-item-section align="left">
                  <q-icon size="1.2em" :name="'mdi-close'" color="primary" />
                </q-item-section>
              </q-item>
            </template>
          </q-file>
          <div
            class="column justify-end content-end"
            v-if="edicion && fileCopy && fileCopy != file"
          >
            <q-btn
              @click="
                file = fileCopy;
                cambioArchivo = false;
              "
              color="secondary"
              flat
              dense
              class="q-mr-ms"
            >
              <span class="text-caption text-capitalize">
                Cancelar reemplazo</span
              >
            </q-btn>
          </div>
        </div>
      </q-tab-panel>
      <q-tab-panel name="keycer" class="q-gutter-md"> </q-tab-panel>
    </q-tab-panels>
  </q-card>
</template>

<script setup>
import { ref } from "vue";

const tabName = ref("pfx");
const file = ref(null);
const fileCopy = ref(null);
</script>
