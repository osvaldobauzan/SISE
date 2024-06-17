<template>
  <q-card style="min-width: 90vw; min-height: 70vh">
    <q-splitter v-model="splitterModel" :before-class="full - height">
      <template v-slot:before>
        <VerAcuerdo v-if="esActuaria" :model-value="acuerdo" :esDialogo="false">
        </VerAcuerdo>
        <q-scroll-area v-else style="width: 100%; height: 100%">
          <div class="col-10" id="containerWord"></div>
        </q-scroll-area>
      </template>

      <template v-slot:after>
        <div class="q-pl-md q-pr-md q-pb-sm">
          <q-toolbar>
            <q-toolbar-title>Oficio libre </q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <div class="row wrap q-pb-md">
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Nombre</q-item-label>
                <q-item-label>{{ value.nombre }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Expediente</q-item-label>
                <q-item-label>{{ value?.expediente }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <q-item-section>
            <SummernoteEditor
              v-model="value.text"
              @update:model-value="
                () => (esActuaria ? emit('update:modelValue', false) : null)
              "
            />
          </q-item-section>
          <q-card-actions align="left">
            <q-btn
              no-caps
              @click="guardar()"
              :color="!formValido ? 'grey-6' : 'blue'"
              :disable="!formValido"
              :label="!edicion ? 'Crear oficio' : 'Modificar oficio'"
              style="min-width: 164px"
              class="q-ml-sm"
            />
            <q-btn
              no-caps
              @click="cancelar()"
              outline
              label="Cancelar"
              :color="'blue'"
              style="min-width: 164px"
            />
          </q-card-actions>
        </div>
      </template>
    </q-splitter>
  </q-card>
  <DialogConfirmacion
    v-model="showCancelarEditarOficio"
    label-btn-cancel="No"
    label-btn-ok="Sí, cancelar"
    titulo="¿Deseas cancelar el oficio libre?"
    :subTitulo="`No se guardará ninguna información que hayas agregado`"
    @aceptar="
      () => {
        emit('cerrarEditar', {
          value: true,
          text: originalText,
        });
        showCancelarEditarOficio = false;
      }
    "
    @cancelar="showCancelarEditarOficio = false"
  ></DialogConfirmacion>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import * as docx from "docx-preview";
import { noty } from "../helpers/notify";
import { useTramiteStore } from "../modules/tramite/store/tramite-store";
import SummernoteEditor from "src/components/SummernoteEditor.vue";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";

const originalText = ref("");
const splitterModel = ref(50);
const showCancelarEditarOficio = ref(false);
const tramiteStore = useTramiteStore();
const props = defineProps({
  // v-model
  modelValue: {
    default: "",
  },
  cambioOficioLibre: {
    type: Boolean,
    default: false,
  },
  edicion: {
    type: Boolean,
    default: false,
  },
  esActuaria: {
    default: false,
  },
  acuerdo: {
    type: Object,
  },
});

const value = computed({
  get() {
    return props.modelValue;
  },
  set() {},
});

let formValido = computed(() => {
  if (props.cambioOficioLibre) return true;
  const cambioForm = props.modelValue.text !== originalText.value;
  tramiteStore.actualizarOficioLibre(cambioForm);
  return cambioForm;
});

let documentSize = computed(() => {
  if (props.modelValue.text) {
    return new Blob([props.modelValue.text]).size;
  } else {
    return 0;
  }
});
const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  cerrarEditar: (value) => value,
});

onMounted(() => {
  if (value.value.archivo) {
    onGetFile(value.value.archivo);
  }
  originalText.value = props.modelValue.text;
});

async function onGetFile(file) {
  const options = {
    renderHeaders: true, //enables headers rendering
    renderFooters: false, //enables footers rendering
    renderFootnotes: false, //enables footnotes rendering
    renderEndnotes: false, //enables endnotes rendering
  };
  await docx.renderAsync(
    file,
    document.getElementById("containerWord"),
    null,
    options,
  );
}

function cancelar() {
  if (formValido.value) {
    showCancelarEditarOficio.value = true;
  } else {
    emit("cerrarEditar", { value: formValido.value, text: originalText.value });
  }
}

function guardar() {
  value.value.text = insertStyleAttribute(value.value.text);
  value.value.text = agregarSup(value.value.text);
  if (documentSize.value >= 30000000) {
    noty.error(
      "Error: El peso máximo permitido para la creación del oficio libre es de 30 MB",
    );
  } else {
    const accionMensaje = !props.edicion ? "creado" : "modificado";
    noty.correcto(
      `Se ha ${accionMensaje} el oficio libre del expediente ${value.value.expediente}`,
    );
    emit("update:modelValue", value.value);
  }
}

function agregarSup(htmlString) {
  return htmlString.replace(
    /<blockquote><span([^>]*)>(\s*)(\d+)(\s*)([^<]+)<\/span>/g,
    "<blockquote><span$1><sup>$3</sup>$5</span>",
  );
}

function insertStyleAttribute(originalString) {
  const arrayIndex = [];
  const searchString = "width:";
  const widthTotal = 525;
  let modifiedString = "";
  let imgIndex = value.value.text?.indexOf(searchString);
  let newWidth = null;
  let styleToInsert = "";
  let spaces = 0;

  while (imgIndex !== -1) {
    arrayIndex.push(imgIndex);
    imgIndex = value.value.text?.indexOf(searchString, imgIndex + 1);
  }

  if (arrayIndex.length > 0) {
    arrayIndex.forEach((e, index) => {
      if (index > 0) {
        e = originalString.indexOf(searchString, imgIndex + 1);
      }

      spaces = encontrarAncho(originalString.slice(e - 1, e + 20));

      if (imagesPercentages.value[index] < 1) {
        newWidth = imagesPercentages.value[index] * widthTotal;
      } else {
        newWidth = widthTotal;
      }
      styleToInsert = newWidth.toString() + "px";
      modifiedString =
        originalString.slice(0, e + 6) +
        " " +
        styleToInsert +
        ";" +
        originalString.slice(e + 6 + spaces);

      originalString = modifiedString;
      imgIndex = e;
    });

    return agregarBrAntesDeImg(modifiedString);
  } else {
    return originalString;
  }
}

function agregarBrAntesDeImg(inputString) {
  var resultado = inputString.replace(/(<br\s*\/?>)?<img/g, "<br><img");
  return resultado;
}

function encontrarAncho(str) {
  const match = str.match(/width:/);

  if (match) {
    const startIndex = match.index;

    const endIndex = str.indexOf('"', startIndex + 6);
    const longitud = endIndex - startIndex - 6;

    return longitud;
  }

  return null;
}

const imagesPercentages = computed(() => {
  let coincidencias = value.value.text?.match(/width:([^"]*)/g);
  if (coincidencias) {
    let arrayWidths = [];
    let arrayBase64 = coincidencias.map(function (match) {
      return match.replace("width:", "");
    });
    arrayBase64.forEach((element) => {
      element = element.replace(/[\s;]+/g, "");
      arrayWidths.push(porcentajeADecimal(element));
    });
    return arrayWidths;
  } else {
    return null;
  }
});

function porcentajeADecimal(cadenaPorcentaje) {
  if (cadenaPorcentaje.endsWith("%")) {
    const valorNumerico = parseFloat(cadenaPorcentaje.slice(0, -1));
    if (!isNaN(valorNumerico)) {
      const valorDecimal = valorNumerico / 100;
      return valorDecimal;
    } else {
      return null;
    }
  } else {
    return 1;
  }
}
</script>

<style scoped>
:deep(.q-splitter--vertical > .q-splitter__panel) {
  height: unset;
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
:deep(.note-editor.note-frame .note-editing-area) {
  min-height: 45vh;
  max-height: 55vh;
}
:deep(.note-editor.note-frame .note-editing-area .note-editable) {
  min-height: 40vh;
  max-height: 50vh;
}
</style>
