<template>
  <q-dialog v-model="value" persistent>
    <q-card style="min-height: 3vh; min-width: 45vw">
      <q-toolbar>
        <q-toolbar-title class="text-bold"> Agregar anexo </q-toolbar-title>
      </q-toolbar>
      <q-card-section>
        <q-form ref="formAnexo" @submit="guardaAnexo">
          <div class="row q-gutter-lg">
            <div class="col">
              <q-select
                v-cortarLabel
                @input-value="anexo.tipoAnexo = null"
                v-model="anexo.tipoAnexo"
                dense
                filled
                use-input
                input-debounce="0"
                label="Tipo de anexo *"
                @update:model-value="cambioForm"
                :options="tiposAnexoOptions"
                @filter="filtrarTiposAnexo"
                option-label="descripcion"
                option-value="id"
                :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
              />
            </div>
            <div class="col">
              <q-select
                v-cortarLabel
                @input-value="anexo.descripcion = null"
                dense
                filled
                use-input
                input-debounce="0"
                v-model="anexo.descripcion"
                label="Descripción *"
                @update:model-value="cambioForm"
                :options="descripcionesAnexoOptions"
                @filter="filtrarDescripcionAnexo"
                option-label="descripcion"
                option-value="id"
                :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
              />
            </div>
            <div class="col">
              <q-select
                v-cortarLabel
                @input-value="anexo.caracter = null"
                dense
                filled
                use-input
                input-debounce="0"
                v-model="anexo.caracter"
                label="Carácter *"
                @update:model-value="cambioForm"
                :options="caracteresAnexoOptions"
                @filter="filtrarCaracterAnexo"
                option-label="descripcion"
                option-value="id"
                :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
              />
            </div>
          </div>

          <template v-if="esEdicionv1 && mostrarArchivoAnterior">
            <div
              class="row wrap justify-between items-center content-around q-my-md"
              style="background: rgb(0 0 0 / 3%)"
            >
              <div class="row wrap justify-center items-center content-around">
                <div class="q-avatar">
                  <div
                    class="q-avatar__content row flex-center overflow-hidden"
                  >
                    <i
                      class="q-icon text-primary notranslate material-icons"
                      aria-hidden="true"
                      role="presentation"
                      >insert_drive_file</i
                    >
                  </div>
                </div>
                <div class="ellipsis" style="font-weight: bold">
                  {{ anexo.nombreArchivo }}
                </div>
              </div>
              <div class="column justify-end items-end content-end">
                <q-btn
                  @click="cargarNuevoArchivo()"
                  color="secondary"
                  flat
                  dense
                  class="q-mr-md"
                >
                  <q-tooltip>Reemplazar</q-tooltip>
                  <span class="text-caption text-capitalize"
                    ><q-icon :name="'mdi-replay'"></q-icon>
                    <br />Reemplazar</span
                  >
                </q-btn>
              </div>

              <!---->
            </div>
          </template>

          <div
            v-if="!esEdicionv1 || (esEdicionv1 && !mostrarArchivoAnterior)"
            :style="anexo.file !== null ? '' : 'border: 3px dashed #ccc'"
          >
            <q-file
              :model-value="anexo.file"
              borderless
              @update:model-value="(val) => updateFiles(val, index)"
              class="full-width full-height"
              accept=".pdf"
              max-file-size="30000000"
              @rejected="manejoErrores.archivoInvalido"
              :rules="[
                (val) => Validaciones.validaInputRequerido(val),
                async (val) => await Validaciones.validaExtension(val, 'pdf'),
              ]"
            >
              <template v-if="!anexo.file" v-slot:prepend>
                <div class="row label-file">
                  <div class="col">
                    <q-item-label
                      ><q-icon name="mdi-upload" />Arrastra y suelta o
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
                    <span class="text-bold">
                      {{ anexo.file.name }}
                    </span>
                    <span
                      class="q-ml-md text-grey text-caption"
                      style="width: 15%"
                    >
                      {{
                        anexo.file.size / 1024 < 1024
                          ? (anexo.file.size / 1024).toFixed(1) + "KB"
                          : (anexo.file.size / 1024 / 1024).toFixed(1) + "MB"
                      }}
                    </span>
                  </div>

                  <q-tooltip>
                    {{ anexo.file.name }}
                  </q-tooltip>
                </q-chip>
              </template>
              <template v-if="anexo.file" v-slot:after>
                <q-item
                  dense-toggle
                  class="q-field-after"
                  clickable
                  @click="updateFiles(null)"
                >
                  <q-item-section align="left">
                    <q-icon size="1.1em" :name="'mdi-close'" color="primary" />
                  </q-item-section>
                </q-item>
              </template>
            </q-file>
            <div
              class="column justify-end content-end"
              v-if="
                esEdicionv1 &&
                !mostrarArchivoAnterior &&
                anexo.nombreArchivo !== ''
              "
            >
              <q-btn
                @click="cargarNuevoArchivo()"
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
          <div class="row">
            <div class="col">
              <q-item-label class="q-pt-sm q-pb-sm text-grey-6"
                ><q-icon
                  name="mdi-information"
                  size="1.2em"
                  color="light-blue"
                />Solo puedes subir archivos menores a 20MB en formato
                PDF.</q-item-label
              >
            </div>
          </div>
        </q-form>
      </q-card-section>
      <q-card-actions align="left" class="q-px-md">
        <q-btn
          class="q-ml-sm"
          :color="formValido ? 'secondary' : 'grey-6'"
          style="min-width: 164px"
          @click="guardaAnexo"
          :disable="!formValido"
        >
          Crear anexo
        </q-btn>
        <q-btn
          outline
          style="min-width: 164px"
          color="secondary"
          @click="value = false"
        >
          Cancelar
        </q-btn>
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup>
import {
  ref,
  computed,
  watch,
  onMounted,
  onUpdated,
  onBeforeUnmount,
} from "vue";
import { FormAnexos } from "../data/form-anexos";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Validaciones } from "src/helpers/validaciones";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { Utils } from "src/helpers/utils";
import { loader } from "src/helpers/loader";
import { Promocion } from "../data/promocion";
const catalogosStore = useCatalogosStore();
const tiposAnexoOptions = ref(catalogosStore.tiposAnexo);
const descripcionesAnexoOptions = ref(catalogosStore.descripcionesAnexo);
const caracteresAnexoOptions = ref(catalogosStore.caracteresAnexo);
const formAnexo = ref(null);
const formValido = ref(false);

catalogosStore.$subscribe(() => {
  tiposAnexoOptions.value = catalogosStore.tiposAnexo;
  descripcionesAnexoOptions.value = catalogosStore.descripcionesAnexo;
  caracteresAnexoOptions.value = catalogosStore.caracteresAnexo;
});

const props = defineProps({
  // v-model
  modelValue: {
    default: false,
  },
  anexoValue: {
    type: FormAnexos,
    default: {},
  },
  esEditar: {
    default: false,
  },
  promocion: {
    type: Promocion,
  },
});

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  "update:anexoValue": (value) => value !== null,
  "add:anexoValue": (value) => value !== null,
});

const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});

let esEdicionv1 = ref(false);
let mostrarArchivoAnterior = ref(false);
const anexo = ref(new FormAnexos());
async function updateFiles(newFile) {
  anexo.value.file = await Utils.fileToBlob(newFile);
  anexo.value.nombreArchivo = newFile?.name || "";
  anexo.value.guardadoEnBD = false;
  cambioForm();
}
async function cambioForm() {
  formValido.value = await formAnexo.value?.validate(false);
}

function cargarNuevoArchivo() {
  mostrarArchivoAnterior.value = !mostrarArchivoAnterior.value;
}

// eslint-disable-next-line no-unused-vars
const stopWatch = watch(
  () => props.modelValue,
  // eslint-disable-next-line no-unused-vars
  async (_newValue, _oldValue) => {
    if (props.modelValue) {
      if (props.esEditar) {
        anexo.value = { ...props.anexoValue };
      } else {
        emit("add:anexoValue", null);
        anexo.value = new FormAnexos();
      }
    }
    // do something
  },
  {
    immediate: true,
  },
);

onMounted(() => {
  esEdicionv1.value = false;
  mostrarArchivoAnterior.value = false;

  if (!Utils.isEmpty(props.promocion)) {
    esEdicionv1.value = true;
    if (anexo.value.nombreArchivo) {
      mostrarArchivoAnterior.value = true;
    }
  }
});

onUpdated(() => {});

onBeforeUnmount(() => {
  stopWatch();
});
async function guardaAnexo() {
  loader.show();
  if (props.esEditar) {
    anexo.value.guardadoEnBD = false;
    emit("update:anexoValue", anexo);
    value.value = false;
  } else {
    emit("add:anexoValue", anexo);
    value.value = false;
  }
  loader.hide();
}
/**
 * filtra tipos anexo en combo
 * @param {*} val valor a buscar
 */
function filtrarTiposAnexo(val, update) {
  update(
    async () => {
      tiposAnexoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tiposAnexo,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
/**
 * filtra descripciones en combo
 * @param {*} val valor a buscar
 */
function filtrarDescripcionAnexo(val, update) {
  update(
    async () => {
      descripcionesAnexoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.descripcionesAnexo,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
/**
 * filtra cacateres en combo
 * @param {*} val valor a buscar
 */
function filtrarCaracterAnexo(val, update) {
  update(
    async () => {
      caracteresAnexoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.caracteresAnexo,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>

<style scoped>
.label-file {
  text-align: center;
  font-size: 15px;
  position: absolute;
  min-width: 100%;
}
:deep(.q-field__after) {
  padding-left: unset;
}

.q-field-after {
  min-height: 35px;
}
</style>
