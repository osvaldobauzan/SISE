<template>
  <q-card style="min-width: 50vw">
    <q-toolbar>
      <q-toolbar-title class="text-weight-bold"
        >Subir acuerdo de cumplimiento</q-toolbar-title
      >
    </q-toolbar>
    <q-card-section class="q-gutter-sm">
      <q-form ref="form">
        <div class="row">
          <q-item-label class="q-pa-md text-weight-bold col-12">
            Datos del expediente
          </q-item-label>
          <template v-if="item">
            <q-item class="q-pa-md col-xs-12 col-sm-6 col-md-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Expediente</q-item-label>
                <q-item-label>
                  {{ item?.Expediente?.AsuntoAlias }}
                </q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="q-pa-md col-xs-12 col-sm-6 col-md-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Tipo de asunto</q-item-label>
                <q-item-label>
                  {{ item?.Expediente?.CatTipoAsunto }}
                </q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="q-pa-md col-xs-12 col-sm-6 col-md-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Cuaderno</q-item-label>
                <q-item-label>
                  {{ item?.CuadernoNombre }}
                </q-item-label>
              </q-item-section>
            </q-item>
            <q-item
              v-if="item?.Expediente?.TipoProcedimiento"
              class="q-pa-md col-xs-12 col-sm-6 col-md-4"
            >
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Tipo de procedimiento</q-item-label
                >
                <q-item-label>
                  {{ item?.Expediente?.TipoProcedimiento }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </template>
          <template v-else>
            <q-select
              clearable
              v-cortarLabel
              @input-value="expedienteEncontrado = null"
              class="q-pa-md col-xs-12 col-sm-6"
              label="Buscar un expediente existente *"
              v-model="expedienteEncontrado"
              @filter="buscarExpedientePorNumero"
              :options="opcionesExpediente"
              option-value="asuntoNeunId"
              use-input
              input-debounce="0"
              hint="Formato Número/AAAA"
              @update:model-value="cambioExpediente"
              dense
              filled
              :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
            >
              <template v-slot:append>
                <q-btn flat round icon="mdi-magnify" />
              </template>
              <template v-slot:no-option>
                <q-item>
                  <q-item-section class="text-red row">
                    <span>
                      <q-icon name="info" /> No se encontraron resultados
                    </span>
                  </q-item-section>
                </q-item>
              </template>
              <template v-slot:option="scope">
                <q-item v-bind="scope.itemProps">
                  <q-item-section>
                    <q-item-label>{{ scope.opt.asuntoAlias }}</q-item-label>
                    <q-item-label caption>
                      {{ scope.opt.tipoAsunto }}
                    </q-item-label>
                    <q-item-label
                      class="text-caption"
                      v-if="scope.opt.tipoProcedimiento !== ''"
                    >
                      {{ scope.opt.tipoProcedimiento }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </template>
            </q-select>
            <q-select
              v-cortarLabel
              @input-value="cuaderno = null"
              dense
              filled
              class="q-pa-md col-xs-12 col-sm-6"
              use-input
              input-debounce="0"
              v-model="cuaderno"
              label="Cuaderno *"
              option-label="cuaderno"
              option-value="cuadernoId"
              @filter="filtrarCuaderno"
              @update:model-value="cambioForm"
              :options="cuadernoOptions"
              :loading="buscandoCuadernos"
              :rules="[
                (val) => Validaciones.validaSelectRequerido(val?.cuadernoId),
              ]"
            >
            </q-select>
          </template>

          <q-item-label class="q-pa-md text-weight-bold col-12">
            Datos de la ejecucion
          </q-item-label>

          <q-input
            dense
            filled
            class="q-pa-md col-xs-12 col-md-6"
            v-model="fechaRequerimiento"
            label="Fecha de requerimiento de cumplimiento *"
            :rules="reglasFecha"
            mask="##/##/####"
            :readonly="formReadOnly"
            @update:model-value="cambioForm()"
          >
            <template v-slot:append>
              <q-icon name="mdi-calendar-month" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date
                    v-model="fechaRequerimiento"
                    @update:model-value="cambioForm()"
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
          <q-input
            dense
            filled
            class="q-pa-md col-xs-12 col-md-6"
            v-model="fechaDeclaracion"
            label="Fecha de declaración del cumplimiento *"
            :rules="reglasFecha"
            mask="##/##/####"
            :readonly="formReadOnly"
            @update:model-value="cambioForm()"
          >
            <template v-slot:append>
              <q-icon name="mdi-calendar-month" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date
                    v-model="fechaDeclaracion"
                    @update:model-value="cambioForm()"
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
          <q-input
            dense
            filled
            class="q-pa-md col-xs-12 col-md-6"
            v-model="fechaConsentida"
            label="Fecha de consentida la resolución de cumplimiento *"
            :rules="reglasFecha"
            mask="##/##/####"
            :readonly="formReadOnly"
            @update:model-value="cambioForm()"
            ><template v-slot:append>
              <q-icon name="mdi-calendar-month" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date
                    v-model="fechaConsentida"
                    @update:model-value="cambioForm()"
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

          <q-item-label class="q-pa-md text-weight-bold col-12">
            Subir cumplimiento
          </q-item-label>

          <div
            class="q-pa-md q-ml-md col-12"
            style="border: 3px dashed #ccc; max-width: 96%"
          >
            <q-file
              v-model="archivoAcuerdoCumplimiento"
              borderless
              class="full-width full-height"
              accept=".pdf"
              max-file-size="20000000"
            >
              <template v-if="!archivoAcuerdoCumplimiento" v-slot:prepend>
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
                  <div style="width: 25%" class="ellipsis relative-position">
                    <span class="text-bold text-body2">{{
                      archivoAcuerdoCumplimiento.name
                    }}</span>
                    <span class="q-ml-sm text-caption">{{
                      archivoAcuerdoCumplimiento.size / 1024 < 1024
                        ? (archivoAcuerdoCumplimiento.size / 1024).toFixed(1) +
                          "KB"
                        : (
                            archivoAcuerdoCumplimiento.size /
                            1024 /
                            1024
                          ).toFixed(1) + "MB"
                    }}</span>
                  </div>
                  <q-tooltip>
                    {{ archivoAcuerdoCumplimiento.name }}
                  </q-tooltip>
                </q-chip>
              </template>
              <template v-if="archivoAcuerdoCumplimiento" v-slot:after>
                <q-item dense-toggle class="q-field-after" clickable>
                  <q-item-section align="left">
                    <q-icon size="1.2em" :name="'mdi-close'" color="primary" />
                  </q-item-section>
                </q-item>
              </template>
            </q-file>
          </div>
          <div class="q-pa-md">
            <q-icon size="1.2em" color="light-blue" name="info" /> Solo puedes
            subir archivos menores a 20MB en formato PDF.
          </div>
        </div>
      </q-form>
    </q-card-section>
    <q-card-actions class="q-ml-lg" align="left">
      <q-btn
        class="col-xs-5 col-sm-3 col-md-2"
        no-caps
        :disable="!formValido"
        :color="formValido ? 'blue' : 'grey-6'"
        label="Subir"
        @click="formValido ? guardar() : null"
      ></q-btn>
      <q-btn
        class="col-xs-5 col-sm-3 col-md-2"
        color="blue"
        outline
        no-caps
        label="Cancelar"
        @click="cancelar"
      ></q-btn>
    </q-card-actions>
  </q-card>
  <DialogConfirmacion
    v-model="showCancelar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="Se perderán los cambios."
    :subTitulo="`Si continúa se perderán los cambios que ha realizado. ¿Desea continuar?`"
    @aceptar="emit('cerrar')"
  ></DialogConfirmacion>
</template>

<script setup>
import { ref } from "vue";
import { Validaciones } from "src/helpers/validaciones";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { Utils } from "src/helpers/utils";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useSentenciasStore } from "src/modules/sentencias/store/sentencias-store";
const catalogosStore = useCatalogosStore();

const sentenciasStore = useSentenciasStore();
const expedienteEncontrado = ref(null);
const opcionesExpediente = ref([]);
const fechaRequerimiento = ref();
const fechaDeclaracion = ref();
const fechaConsentida = ref();
const archivoAcuerdoCumplimiento = ref();
//const submitForm = ref();
const cuaderno = ref(null);
const cuadernoOptions = ref([]);
const cambioFormulario = ref(false);
const showCancelar = ref(false);
const form = ref(null);
const formValido = ref(false);
const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);


defineProps({
  item: {
    type: Object,
    required: false,
    default: () => ({}),
  },
  tipoAcuerdo: {
    type: String,
    default: "",
  },
  numExpediente: {
    type: String,
    default: "",
  },
});
const emit = defineEmits({
  cerrar: () => true,
});

function cancelar() {
  if (cambioFormulario.value) {
    showCancelar.value = true;
  } else {
    emit("cerrar", false);
  }
}

async function buscarExpedientePorNumero (val, update, abort) {
  update(
    async () => {
      if (
        val === "" ||
        val.length <= 5 ||
        typeof Validaciones.validaNoExpediente(val) === "string"
      ) {
        abort();
        return;
      } else {
        try {
          await sentenciasStore.buscarExpediente(val, null, 2);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        opcionesExpediente.value = sentenciasStore.expediente;
        if (opcionesExpediente.value?.length === 1) {
          buscarExpediente.value = opcionesExpediente.value[0];
        }
      }
    },
    // "ref" is the Vue reference to the QSelect
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 100)
  );
}
async function cambioExpediente (val) {
  cuaderno.value = null;
  if (val) {
    await obtenCatalogosDependientes(
      val.asuntoNeunId,
      val.asuntoAlias,
      val.catTipoAsuntoId
    );
  }
  cambioForm();
}
/**
 * filtra cuaderno en combo
 * @param {*} val valor a buscar
 */
function filtrarCuaderno (val, update) {
  update(
    async () => {
      cuadernoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.cuadernos,
        "cuaderno"
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref)
  );
}
async function obtenCatalogosDependientes (
  asuntoNeunId,
  numeroExpediente,
  catTipoAsuntoId
) {
  try {
    await catalogosStore.obtenerCuadernos(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  cuadernoOptions.value = catalogosStore.cuadernos;
}

async function cambioForm () {
  formValido.value = await form.value?.validate(false);
  cambioFormulario.value = true;
}
</script>
