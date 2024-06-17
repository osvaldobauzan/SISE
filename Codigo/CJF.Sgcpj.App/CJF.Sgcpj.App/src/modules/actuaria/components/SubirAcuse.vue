<template>
  <q-card class="widthCard">
    <q-form ref="form" @submit="envioGuardar">
      <q-toolbar>
        <q-toolbar-title class="text-bold"> Subir acuse </q-toolbar-title>
        <q-btn flat round dense icon="mdi-close" @click="showModal()" />
      </q-toolbar>

      <div class="no-wrap">
        <q-list>
          <q-item v-ripple v-for="parte in partes" :key="parte.parteId">
            <q-item-section>
              <q-item-label>{{ parte.parte }}</q-item-label>
              <q-item-label class="text-bold text-capitalize">{{
                capitalizeWords(parte.caracter)
              }}</q-item-label>
            </q-item-section>
            <q-item-section avatar>
              <div class="q-mb-sm">
                <q-input
                  dense
                  v-model="parte.estadoFecha"
                  filled
                  label="Fecha de la notificación"
                  style="width: 164px"
                  :rules="reglasFecha"
                  @update:model-value="cambioFechaNotificacion()"
                >
                  <template v-slot:append>
                    <q-icon name="mdi-calendar-month" class="cursor-pointer">
                      <q-popup-proxy
                        cover
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-date
                          @update:model-value="cambioFechaNotificacion()"
                          v-model="parte.estadoFecha"
                          mask="DD/MM/YYYY"
                        >
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Cerrar"
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
            </q-item-section>
          </q-item>
        </q-list>
      </div>

      <q-separator />

      <q-list>
        <q-item>
          <q-item-section>
            <q-item-label>Tipo de notificación</q-item-label>
            <q-item-label class="text-bold text-capitalize">
              {{ partes[0].tipo }}</q-item-label
            >
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-select
              filled
              v-model="tipoAcuseSelected"
              :options="tipoacuse"
              label="Seleccione el tipo acuse"
              style="width: 515px"
              v-cortarLabel
              v-focus
              dense
              use-input
              input-debounce="0"
              option-label="descripcion"
              @update:model-value="cambioForm()"
              :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
            >
              <template v-slot:option="scope">
                <q-item v-bind="scope.itemProps">
                  <q-item-section avatar>
                    <q-icon name="mdi-circle-medium" :color="scope.opt.color" />
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>{{ scope.opt.descripcion }}</q-item-label>
                  </q-item-section>
                </q-item>
              </template>
              <template v-slot:selected>
                <div class="row q-py-xs" v-if="tipoAcuseSelected">
                  <q-item-section avatar>
                    <q-icon
                      name="mdi-circle-medium"
                      :color="tipoAcuseSelected.color"
                    />
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>{{
                      tipoAcuseSelected.descripcion
                    }}</q-item-label>
                  </q-item-section>
                </div>
              </template>
            </q-select>
          </q-item-section>
        </q-item>
        <q-list v-if="tipoAcuseSelected?.descripcion === 'Citatorio'">
          <q-item-label class="q-pl-md q-mb-xs"
            >Capture la síntesis del citatorio</q-item-label
          >
          <q-item>
            <q-input
              class="full-width"
              dense
              outlined
              autofocus
              maxlength="30000000"
              :rules="[(val) => Validaciones.validaInputRequerido(val)]"
              v-model="sintesis"
              @update:model-value="cambioForm()"
              type="textarea"
            ></q-input>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >Programar fecha de notificación del citatorio</q-item-label
              >
              <div class="row justify-between">
                <q-input
                  dense
                  filled
                  v-model="fechaNotificacionCitatorio"
                  class="col-3 q-mt-sm"
                  label="Fecha"
                  style="width: 164px"
                  :rules="reglasFecha"
                  @update:model-value="
                    () => {
                      cambioFechaCitatorio();
                      cambioForm();
                    }
                  "
                >
                  <template v-slot:append>
                    <q-icon name="mdi-calendar-month" class="cursor-pointer">
                      <q-popup-proxy
                        cover
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-date
                          @update:model-value="
                            () => {
                              cambioForm();
                              cambioFechaCitatorio();
                            }
                          "
                          v-model="fechaNotificacionCitatorio"
                          mask="DD/MM/YYYY"
                        >
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Cerrar"
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
            </q-item-section>
          </q-item>
        </q-list>
        <div
          style="width: 515px; margin-left: 20px"
          :style="file !== null ? '' : 'border: 2px dashed #ccc'"
        >
          <q-file
            :readonly="edicion && file !== null && file?.name == fileCopy?.name"
            ref="fileAcuerdo"
            :model-value="file"
            borderless
            @update:model-value="(val) => updateFiles(val, index)"
            class="full-width full-height"
            accept=".pdf"
            max-file-size="30000000"
            @rejected="(err) => manejoErrores.archivoInvalido(err, 'Word')"
            :rules="[
              (val) => Validaciones.validaInputRequerido(val),
              async (val) =>
                edicion && (fileCopy == null || file?.name == fileCopy?.name)
                  ? true
                  : await Validaciones.validaExtension(val, 'pdf'),
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
        <q-item v-if="file === null">
          <q-item-label>
            <q-icon name="mdi-information" color="info" class="q-mr-sm" />
            <span class="text-grey-8"
              >Sólo puedes subir archivos menores a 30 Mb en formato PDF</span
            >
          </q-item-label>
        </q-item>
      </q-list>
      <q-separator></q-separator>
      <q-card-actions class="row q-px-md justify-between">
        <q-btn
          class="q-ml-sm"
          style="min-width: 164px"
          :color="!formValido ? 'grey-6' : 'blue'"
          no-caps
          label="Subir"
          type="submit"
          :disable="!formValido"
        ></q-btn>
        <q-btn
          style="min-width: 164px"
          :color="'secondary'"
          outline
          no-caps
          label="Cancelar"
          @click="showModal()"
        ></q-btn>
      </q-card-actions>
    </q-form>
    <q-inner-loading :showing="cargandoGuardado">
      <template v-slot>
        <q-spinner size="40px" />
        <div v-html="`<b>Subiendo acuse</b>`"></div>
      </template>
    </q-inner-loading>
  </q-card>
  <DialogConfirmacion
    v-model="showCancelarSubirAcuse"
    label-btn-cancel="No borrar"
    label-btn-ok="Sí, borrar"
    titulo="¿Deseas cancelar?"
    :subTitulo="`Si continúas se perderán los cambios que has realizado.`"
    @aceptar="
      () => {
        showCancelarSubirAcuse = false;
        emit('cerrar');
      }
    "
    @cancelar="
      () => {
        showCancelarSubirAcuse = false;
      }
    "
  ></DialogConfirmacion>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import { noty } from "src/helpers/notify";
import { date } from "quasar";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useActuariaDetalleNotificacionesStore } from "src/modules/actuaria/stores/actuaria-detalle-notificaciones-store";
import { Validaciones } from "src/helpers/validaciones";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";

const catalogosStore = useCatalogosStore();
const showCancelarSubirAcuse = ref(false);
const cargandoGuardado = ref(false);
const sintesis = ref("");
// const fechaAcuse = ref(null);
const tipoAcuseSelected = ref(null);
const formValido = ref(false);
const tipoacuse = ref([]);
const tipoComunucacionCOE = ref("");
const form = ref(null);
const fechaNotificacion = ref(null);
const fechaNotificacionCitatorio = ref(null);
// const horaNotificacion = ref(null);
const file = ref(null);
const cambioArchivo = ref(false);
const formModificado = ref(false);
const amarillo = ref([5726, 5731, 5732, 1440]);
const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);
// const reglasHora = ref([
//   (val) => Validaciones.validaInputRequerido(val),
//   (val) => Validaciones.validaHora(val),
// ]);
const actuariaDetalleNotificacionesStore =
  useActuariaDetalleNotificacionesStore();

const emit = defineEmits({
  cerrar: () => true,
});

const props = defineProps({
  partes: {
    type: Array,
    required: true,
  },
  acuerdo: {
    type: Object,
    required: true,
  },
});

function insertColor() {
  tipoacuse.value.forEach((acuse) => {
    if (amarillo.value.includes(acuse.id)) acuse.color = "yellow";
    else acuse.color = "green";
  });
  const orden = { yellow: 1, green: 2 };
  tipoacuse.value = tipoacuse.value.sort(
    (a, b) => orden[a.color] - orden[b.color],
  );
}

onMounted(async () => {
  tipoacuse.value = await catalogosStore.getTipoAcuse();
  insertColor();
  props.partes.forEach(
    (parte) => (parte.estadoFecha = fechaLocal(parte.estadoFecha)),
  );
  if (props.partes[0].asuntoNEUNCOE > 0) {
    const comunicacion = await catalogosStore.getTipoComunicacion();
    tipoComunucacionCOE.value = comunicacion.find(
      (x) => x.id === props.partes[0].tipoComunicacionCOE,
    )?.descripcion;
  }
  precargaTipoAcuse(props.partes[0]);
});

function fechaLocal(fecha) {
  if (fecha != "0001-01-01T00:00:00")
    return date.formatDate(fecha, "DD/MM/YYYY");
  return date.formatDate(new Date(), "DD/MM/YYYY");
}

function capitalizeWords(inputString) {
  if (inputString) {
    const words = inputString.split(" ");

    const capitalizedWords = words.map((word) => {
      if (word.length === 0) {
        return word;
      }
      const firstLetter = word.charAt(0).toUpperCase();
      const restOfWord = word.slice(1).toLowerCase();
      return firstLetter + restOfWord;
    });

    const resultString = capitalizedWords.join(" ");

    return resultString;
  }
}

function envioGuardar() {
  if (props.partes.length > 1) {
    guardarMasivo();
  } else {
    guardar();
  }
}

async function guardar() {
  let data = new FormData();

  data.append("asuntoNeunId", props.acuerdo.expediente.asuntoNeunId);
  data.append("sintesisOrden", props.acuerdo.sintesisOrden);
  data.append("fechaNotificacion", props.partes[0].estadoFecha);
  data.append("tipoAcuse", tipoAcuseSelected.value.id);
  data.append(
    "personaId",
    props.partes[0].parteId == 0
      ? props.partes[0].promoventeId
      : props.partes[0].parteId,
  );
  data.append("tipoNotificacion", null);
  data.append(
    file.value.name,
    Utils.blobToFile(file.value.blob, file.value.name),
    file.value.name,
  );

  if (tipoAcuseSelected.value.descripcion === "Citatorio") {
    data.append("sintesisCitatorio", sintesis.value);
    data.append("fechaNotificacionCitatorio", fechaNotificacionCitatorio.value);
  }
  cargandoGuardado.value = true;
  try {
    await actuariaDetalleNotificacionesStore.subirAcuse(data);
    noty.correcto("Acuse subido correctamente.");
    cargandoGuardado.value = false;
    emit("cerrar");
  } catch (error) {
    manejoErrores.mostrarError(error);
    cargandoGuardado.value = false;
  }
}

async function guardarMasivo() {
  let data = new FormData();
  // Parametros
  const partesAcuse = props.partes.map((p) => ({
    parteId: p.parteId,
    fechaNotificacion: p.estadoFecha,
  }));

  data.append("ParteNotificacionAcuse", JSON.stringify(partesAcuse));
  data.append("asuntoNeunId", props.acuerdo.expediente.asuntoNeunId);
  data.append("sintesisOrden", props.acuerdo.sintesisOrden);
  data.append("tipoAcuse", tipoAcuseSelected.value.id);
  data.append("tipoNotificacion", null);
  data.append(
    file.value.name,
    Utils.blobToFile(file.value.blob, file.value.name),
    file.value.name,
  );

  if (tipoAcuseSelected.value.descripcion === "Citatorio") {
    data.append("sintesisCitatorio", sintesis.value);
    data.append("fechaNotificacionCitatorio", fechaNotificacionCitatorio.value);
  }
  cargandoGuardado.value = true;
  try {
    await actuariaDetalleNotificacionesStore.subirAcuseMasivo(data);
    noty.correcto("Acuse subido correctamente.");
    cargandoGuardado.value = false;
    emit("cerrar");
  } catch (error) {
    manejoErrores.mostrarError(error);
    cargandoGuardado.value = false;
  }
}

async function cambioFechaNotificacion(val) {
  fechaNotificacion.value = val;
  await cambioForm();
}

async function cambioForm() {
  formValido.value = await form.value?.validate(false);
  formModificado.value = true;
}

async function cambioFechaCitatorio(val) {
  fechaNotificacionCitatorio.value = val;
}

function showModal() {
  if (formModificado.value) showCancelarSubirAcuse.value = true;
  else emit("cerrar");
}

async function updateFiles(newFile) {
  file.value = await Utils.fileToBlob(newFile);
  cambioArchivo.value = true;
  await cambioForm();
}

function precargaTipoAcuse(parte) {
  let descTipo = parte.tipo.toLowerCase();
  if (parte.asuntoNEUNCOE > 0) {
    descTipo = tipoComunucacionCOE.value.toLowerCase();
  }
  const buscaTipo = tipoacuse.value.find((a) =>
    a.descripcion.toLowerCase().includes(descTipo),
  );
  if (buscaTipo) {
    tipoAcuseSelected.value = buscaTipo;
  }
}
</script>

<style>
.widthCard {
  width: 700px;
  height: auto;
  max-width: 80vw;
}
</style>
