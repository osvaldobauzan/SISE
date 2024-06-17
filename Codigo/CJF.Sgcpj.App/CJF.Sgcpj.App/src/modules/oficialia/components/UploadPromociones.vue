<template>
  <q-card class="QcardStyle">
    <q-toolbar>
      <q-toolbar-title class="text-bold" v-if="expediente"
        >Vincular Promoción</q-toolbar-title
      >
      <q-toolbar-title class="text-bold" v-else
        >Vincular varias promociones
      </q-toolbar-title>
      <q-btn
        v-if="(file || files.length) && !envioAGuardar"
        flat
        round
        dense
        icon="mdi-close"
        @click="emit('cancelar')"
      />
      <q-btn v-else flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-form @submit="guardar" ref="myForm">
      <q-scroll-area class="QscrollStyle">
        <q-card-section class="q-gutter-sm">
          <template v-if="expediente">
            <div class="row">
              <div class="col">
                <q-item-label caption>Expediente</q-item-label>
                <q-item-label class="text-bold">
                  {{ expediente.expediente?.asuntoAlias }}
                </q-item-label>
                <q-item-label caption>
                  {{ expediente.expediente?.catTipoAsunto }}
                </q-item-label>
              </div>
              <div class="col" v-if="expediente.expediente?.tipoProcedimiento">
                <q-item-label caption>Tipo de procedimiento</q-item-label>
                <q-item-label class="text-bold">
                  {{ expediente.expediente?.tipoProcedimiento }}
                </q-item-label>
              </div>
            </div>

            <div class="row" v-if="expediente">
              <div class="col">
                <q-input
                  :model-value="fechaPromocion"
                  disable
                  label="Fecha presentación"
                >
                  <template v-slot:append>
                    <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                      <q-popup-proxy
                        cover
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-date v-model="fechaPromocion" mask="DD-MM-YYYY">
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Close"
                              color="primary"
                              flat
                            />
                          </div>
                        </q-date>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>
              </div>
              <div class="col q-ml-sm">
                <q-input
                  :model-value="horaPromocion"
                  disable
                  label="Hora presentación"
                >
                  <template v-slot:append>
                    <q-icon
                      name="mdi-clock-time-four-outline"
                      class="cursor-pointer"
                    >
                      <q-popup-proxy
                        cover
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-time
                          with-seconds
                          v-model="horaPromocion"
                          mask="HH:mm"
                        >
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Close"
                              color="primary"
                              flat
                            />
                          </div>
                        </q-time>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>
              </div>
            </div>
          </template>
          <template v-else>
            <q-item-label>
              Si has capturado varias promociones pero aún no has subido el
              archivo escaneado de cada una de ellas, aquí podrás subirlas al
              mismo tiempo y se vincularán automáticamente.
            </q-item-label>
            <q-banner class="light-blue-3 doc-note doc-note--tip">
              <q-icon
                style="position: absolute; left: 24px"
                size="1.2em"
                color="secondary"
                name="info"
                class="q-mx-sm"
              />
              <q-item-label class="q-pl-md"
                >Deberás nombrar el archivo con el número de la promoción con la
                que deseas vincularlo. Ejemplo: 20.pdf</q-item-label
              >
            </q-banner>
            <div class="row">
              <q-select
                class="col-6"
                dense
                filled
                use-input
                input-debounce="0"
                v-model="anio"
                label="Selecciona un año *"
                :options="anioOpciones"
                @filter="filtrarAnio"
                :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
              >
                <template v-slot:hint>
                  <q-item-label
                    style="font-size: 1.3em; position: absolute; left: 0"
                  >
                    <q-icon size="1.2em" color="light-blue" name="info" />
                    Serán asignadas a este año
                  </q-item-label>
                </template>
              </q-select>
            </div>

            <template v-for="(archivo, i) in files" v-bind:key="archivo">
              <div class="full-width full-height">
                <q-file
                  readonly
                  :model-value="archivo"
                  borderless
                  class="full-width q-py-none"
                  :rules="[
                    async (val) =>
                      await Validaciones.validaExtension(val, 'pdf'),
                  ]"
                >
                  <template v-if="files[i] === null" v-slot:prepend>
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
                    <div class="column full-width q-pl-sm">
                      <div>
                        <q-chip
                          class="absolute-position full-width full-height q-py-auto q-my-xs"
                          square
                          style="min-height: 50px"
                        >
                          <q-avatar v-if="envioAGuardar">
                            <q-icon
                              size="1.2em"
                              :name="
                                files[i].correcto
                                  ? 'mdi-check-circle'
                                  : 'mdi-alpha-x-circle'
                              "
                              :color="
                                files[i].correcto ? 'green-6' : 'negative'
                              "
                            />
                          </q-avatar>
                          <q-avatar>
                            <q-icon
                              :name="'insert_drive_file'"
                              color="primary"
                            />
                          </q-avatar>
                          <div
                            class="ellipsis row full-width relative-position text-bold"
                            :class="{
                              'justify-between': envioAGuardar,
                              'justify-start': !envioAGuardar,
                            }"
                            style="width: 40%"
                          >
                            <span>{{ files[i].name }}</span>
                            <q-linear-progress
                              v-if="envioAGuardar"
                              size="1em"
                              style="width: 50%"
                              class="q-pt-md q-ml-lg"
                              rounded
                              :track-color="
                                files[i].correcto ? 'light-blue' : 'negative'
                              "
                            />
                            <span class="q-ml-xs text-grey" style="width: 15%">
                              {{
                                files[i].size / 1024 < 1024
                                  ? (files[i].size / 1024).toFixed(1) + "KB"
                                  : (files[i].size / 1024 / 1024).toFixed(1) +
                                    "MB"
                              }}
                            </span>
                            <q-tooltip>
                              {{ files[i].name }}
                            </q-tooltip>
                          </div>
                        </q-chip>
                      </div>
                      <div
                        v-if="!files[i].correcto && envioAGuardar"
                        class="full-width q-pl-md q-mt-xs"
                      >
                        <q-item-section class="text-red">
                          <span> {{ result[i].mensaje }}</span>
                        </q-item-section>
                      </div>
                      <div
                        v-else-if="envioAGuardar"
                        class="full-width q-pl-md q-mt-xs"
                      >
                        <q-item-section>
                          <span> {{ result[i].expedienteProcesado }}</span>
                        </q-item-section>
                      </div>
                    </div>
                  </template>
                  <template v-slot:after v-if="!envioAGuardar">
                    <q-item
                      dense-toggle
                      class="q-field-after"
                      clickable
                      @click="deleteFile(i)"
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
              </div>
            </template>
          </template>
          <template v-if="!envioAGuardar">
            <div :style="file !== null ? '' : 'border: 3px dashed #ccc'">
              <q-file
                :model-value="file"
                @update:model-value="updateFiles"
                borderless
                :multiple="multiple"
                class="full-width full-height"
                accept=".pdf"
                max-file-size="30000000"
                @rejected="manejoErrores.archivoInvalido"
                :rules="[
                  async (val) => await Validaciones.validaExtension(val, 'pdf'),
                ]"
              >
                <template v-if="!file" v-slot:prepend>
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
                    @click="updateFiles(null)"
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
            </div>

            <div class="text-grey-6">
              <q-icon size="1.2em" color="light-blue" name="info" /> Solo puedes
              subir archivos menores a 30MB en formato PDF.
            </div>
          </template>
        </q-card-section>
      </q-scroll-area>
      <q-separator></q-separator>
      <q-card-actions v-if="expediente" align="left">
        <q-btn
          no-caps
          style="min-width: 220px"
          :disable="!file"
          :color="file ? 'blue' : 'grey-6'"
          type="submit"
          label="Guardar"
        />
        <q-item>
          <q-btn
            v-if="!file"
            no-caps
            outline
            style="min-width: 220px"
            color="blue"
            v-close-popup
            label="Cancelar"
          />
          <q-btn
            v-else
            no-caps
            outline
            style="min-width: 220px"
            color="blue"
            label="Cancelar"
            @click="emit('cancelar')"
          />
        </q-item>
      </q-card-actions>
      <q-card-actions v-else-if="envioAGuardar" align="left">
        <q-btn
          no-caps
          style="min-width: 220px"
          :color="'blue'"
          v-close-popup
          label="Salir"
        />
      </q-card-actions>
      <q-card-actions v-else-if="!envioAGuardar" align="center">
        <q-item class="q-pr-sm">
          <q-btn
            no-caps
            style="min-width: 220px"
            :color="files.length > 0 ? 'blue' : 'grey-6'"
            type="submit"
            :disable="files.length < 1"
            label="Vincular promociones"
          />
        </q-item>
        <q-item>
          <q-btn
            v-if="files.length < 1"
            no-caps
            outline
            style="min-width: 220px"
            color="blue"
            v-close-popup
            label="Cancelar"
          />
          <q-btn
            v-else
            no-caps
            outline
            style="min-width: 220px"
            color="blue"
            label="Cancelar"
            @click="emit('cancelar')"
          />
        </q-item>
      </q-card-actions>
    </q-form>
    <q-inner-loading :showing="cargando" />
  </q-card>
</template>

<script setup>
import { date } from "quasar";
import { ref, onMounted } from "vue";
import { useOficialiaStore } from "../stores/oficialia-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Validaciones } from "src/helpers/validaciones";
import { Utils } from "src/helpers/utils";
import { noty } from "src/helpers/notify";

const oficialiaStore = useOficialiaStore();
const fechaPromocion = ref("");
const horaPromocion = ref("");
const anio = ref(null);
const myForm = ref(null);
const props = defineProps({
  expediente: {
    type: Object,
  },
  multiple: {
    type: Boolean,
    default: false,
  },
  promociones: {
    type: Array,
  },
});
const emit = defineEmits({
  refrescarTabla: (value) => value !== null,
  cancelar: (value) => value !== null,
  cerrar: (value) => value !== null,
});
const files = ref([]);
const file = ref(null);
const envioAGuardar = ref(false);
const anioOpciones = ref([]);
const anios = ref([]);
const result = ref([]);
const cargando = ref(false);

async function updateFiles(newFiles) {
  if (props.multiple) {
    if (!files.value) {
      file.value = [];
    }
    newFiles.forEach(async (a) => {
      if (!files.value.find((f) => f.name === a.name)) {
        files.value.push(await Utils.fileToBlob(a));
      }
    });
  } else {
    file.value = await Utils.fileToBlob(newFiles);
  }
  await myForm.value.validate(false);
}
function deleteFile(index) {
  files.value.splice(index, 1);
}

async function guardar() {
  let correcto = false;
  let data = new FormData();
  const valido = await myForm.value.validate(false);
  if (!valido) return;
  cargando.value = true;
  if (props.expediente) {
    const fecha =
      date.formatDate(
        Date.parse(props.expediente.fechaPresentacion?.split("T")[0]),
        "DD/MM/YYYY",
      ) || date.formatDate(Date.now(), "DD/MM/YYYY");
    const year = fecha.substring(6);
    data.append("noRegistro", props.expediente.numeroRegistro);
    data.append("asuntoNeunId", props.expediente.expediente.asuntoNeunId);
    data.append("numeroOrden", props.expediente.numeroOrden);
    data.append("origen", props.expediente.origenPromocion);
    data.append("fojas", props.expediente.fojas);
    data.append("yearPromocion", props.expediente.yearPromocion || year);
    data.append(
      file.value.name,
      Utils.blobToFile(file.value.blob, file.value.name),
      file.value.name,
    );
    data.append("tipoAsunto", props.expediente.expediente.catTipoAsunto);
    data.append(
      "tipoProcedimiento",
      props.expediente.expediente.tipoProcedimiento,
    );
    data.append("numeroExpediente", props.expediente.expediente.asuntoAlias);
    data.append("mesa", props.expediente.mesa);
    data.append("secretarioId", props.expediente.secretarioId);
    data.append("enviarAlerta", true);
    try {
      result.value = await oficialiaStore.subirArchivo(data);
      correcto = true;
      noty.correcto(
        `Se han vinculado la promoción ${
          props.expediente.numeroRegistro || ""
        } al expediente ${props.expediente.expediente.asuntoAlias}`,
      );
      emit("refrescarTabla");
      emit("cerrar");
    } catch (error) {
      correcto = false;
      manejoErrores.mostrarError(error);
    }
  } else {
    data.append("yearPromocion", anio.value.value);
    const arrayFiltrado = props.promociones.filter(
      (item) => item.numeroRegistro !== 0,
    );

    data.append("promociones", JSON.stringify(arrayFiltrado));
    files.value.forEach((element) => {
      data.append(
        element.name,
        Utils.blobToFile(element.blob, element.name),
        element.name,
      );
    });
    try {
      result.value = await oficialiaStore.subirArchivos(data);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    files.value = files.value.map((x, i) => {
      x.correcto = result.value[i]?.correcto || false;
      return x;
    });
    if (result.value.filter((a) => a.correcto).length > 0) {
      noty.correcto(
        `Se han vinculado ${
          result.value.filter((a) => a.correcto).length || ""
        } promociones para el año ${anio.value.value}`,
      );
    }
    if (result.value.filter((a) => !a.correcto).length > 0) {
      noty.error(
        `No se han vinculado ${
          result.value.filter((a) => !a.correcto).length || ""
        } promociones para el año ${anio.value.value}`,
      );
    }
    envioAGuardar.value = true;
  }
  if (correcto) {
    file.value = null;
    myForm.value.reset();
    myForm.value.resetValidation();
  }
  emit("refrescarTabla");
  cargando.value = false;
}
onMounted(() => {
  if (props.expediente) {
    fechaPromocion.value =
      date.formatDate(
        Date.parse(props.expediente.fechaPresentacion),
        "DD/MM/YYYY",
      ) || date.formatDate(Date.now(), "DD/MM/YYYY");
    horaPromocion.value =
      date.formatDate(props.expediente.fechaPresentacion, "HH:mm") || "00:00";
  } else {
    for (let index = 2011; index <= new Date().getFullYear(); index++) {
      anios.value.push({ label: index + "", value: index });
      anioOpciones.value.push({ label: index + "", value: index });
    }
    anio.value = anios.value.find((a) => a.value === new Date().getFullYear());
    fechaPromocion.value = date.formatDate(
      new Date().toISOString(),
      "DD-MM-YYYY",
    );
    horaPromocion.value = date.formatDate(new Date().toISOString(), "HH:mm");
  }
});
/**
 * filtra cacateres en combo
 * @param {*} val valor a buscar
 */
function filtrarAnio(val, update) {
  update(
    async () => {
      anioOpciones.value = Utils.filtrarCombo(val, anios.value, "label");
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>
<style scoped>
:deep(.q-field__after) {
  padding-left: unset;
}

.bg-warning {
  background: #ffdcdc !important;
  margin-top: 0px;
  position: relative;
  top: -5px;
}

.QcardStyle {
  min-width: 550px;
}

.QscrollStyle {
  height: 370px;
}
</style>
