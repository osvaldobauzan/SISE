<template>
  <q-card style="min-width: 90vw; min-height: 95vh; max-height: 95vh">
    <q-splitter v-model="splitterModel" :before-class="'full-height'">
      <template v-slot:before>
        <VerAcuerdo
          style="width: 100%; height: 100%"
          :model-value="acuerdo"
          :esDialogo="false"
        >
        </VerAcuerdo>
      </template>

      <template v-slot:after>
        <div class="q-pl-sm q-pr-md q-pb-sm">
          <div class="stickyTop">
            <q-toolbar>
              <q-toolbar-title class="text-bold"
                >Comunicaciones Oficiales Enviadas</q-toolbar-title
              >
              <q-btn flat round dense icon="mdi-close" @click="cancelar" />
            </q-toolbar>
            <q-separator></q-separator>
          </div>
          <q-scroll-area style="width: 100%; height: 80vh">
            <q-form ref="formCOE" no-focus-error>
              <div class="row q-pl-sm">
                <div
                  class="q-pa-md col-12 justify-between items-center content-around q-my-md"
                  style="background: rgb(0 0 0 / 3%)"
                >
                  <div class="row wrap items-center content-around">
                    <div class="q-avatar">
                      <div
                        class="q-avatar__content row flex-center overflow-hidden"
                      >
                        <i
                          class="q-icon text-secondary notranslate material-icons"
                          aria-hidden="true"
                          role="presentation"
                          >insert_drive_file</i
                        >
                      </div>
                    </div>
                    <div class="ellipsis" style="font-weight: bold">
                      {{ nombreArchivo }}
                    </div>
                    <div
                      class="ellipsis text-grey-6 q-pl-md"
                      style="font-weight: bold"
                    >
                      {{ sizeArchivo }}
                    </div>
                  </div>
                </div>
                <q-item class="q-pa-md col-xs-12 col-sm-6 col-md-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Expediente origen</q-item-label
                    >
                    <q-item-label>{{
                      acuerdo.expediente.asuntoAlias || ""
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="q-pa-md col-xs-12 col-sm-6 col-md-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo de asunto</q-item-label
                    >
                    <q-item-label>{{
                      acuerdo.expediente.catTipoAsunto || ""
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item
                  v-if="acuerdo.expediente.tipoProcedimiento"
                  class="q-pa-md col-xs-12 col-sm-6 col-md-4"
                >
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo Procedimiento</q-item-label
                    >
                    <q-item-label>{{
                      acuerdo.expediente.tipoProcedimiento || ""
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="q-pa-md col-xs-12 col-sm-6 col-md-4">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Cuaderno</q-item-label>
                    <q-item-label>{{
                      acuerdo.tipoCuadernoDesc || ""
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="q-pa-md col-12">
                  <q-item-label class="text-subtitle1 text-bold"
                    >Datos generales</q-item-label
                  >
                </q-item>
                <q-input
                  ref="numExpediente"
                  dense
                  autofocus
                  filled
                  class="q-pa-md col-xs-12 col-md-6"
                  v-model="value.numeroExpediente"
                  label="Nuevo número de expediente *"
                  :rules="reglasNoExpediente"
                  :readonly="formReadOnly"
                  @update:model-value="cambiaronParametros()"
                >
                  <template v-slot:hint>
                    <q-item-label
                      ><q-icon size="1.2em" color="light-blue" name="info" />
                      Asigna un número de expediente a esta COE</q-item-label
                    >
                  </template>
                </q-input>
                <q-select
                  v-cortarLabel
                  @input-value="
                    (value.tipoComunicacion = null), cambiaronParametros()
                  "
                  dense
                  filled
                  use-input
                  input-debounce="0"
                  label-slot
                  v-model="value.tipoComunicacion"
                  option-label="descripcion"
                  option-value="id"
                  :options="tipoComunicacionOptions"
                  :rules="[
                    (val) => Validaciones.validaSelectRequerido(val?.id),
                  ]"
                  :readonly="formReadOnly"
                  class="q-pa-md col-xs-12 col-md-6"
                  @filter="filtrarTipoComunicacion"
                  @update:model-value="cambiaronParametros()"
                >
                  <template v-slot:label>
                    <q-item-label>Tipo de comunicación *</q-item-label>
                  </template>
                </q-select>
                <q-input
                  dense
                  filled
                  class="q-pa-md col-xs-12 col-md-6"
                  v-model="value.numeroOrigen"
                  label="Número de origen *"
                  :rules="reglasNoOrigen"
                  :readonly="formReadOnly"
                  @update:model-value="cambiaronParametros()"
                >
                  <template v-slot:hint>
                    <q-item-label
                      ><q-icon size="1.2em" color="light-blue" name="info" />
                      Subclasificación</q-item-label
                    >
                  </template>
                </q-input>
                <q-input
                  dense
                  filled
                  class="q-pa-md col-xs-12 col-md-6"
                  v-model="value.fechaEnvio"
                  label="Fecha de envio *"
                  :rules="reglasFecha"
                  :readonly="formReadOnly"
                  @update:model-value="cambiaronParametros()"
                >
                  <template v-slot:append>
                    <q-icon name="mdi-calendar-month" class="cursor-pointer">
                      <q-popup-proxy
                        cover
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-date
                          v-model="value.fechaEnvio"
                          @update:model-value="cambiaronParametros()"
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
                <q-select
                  v-cortarLabel
                  @input-value="
                    (value.secretario = null), cambiaronParametros()
                  "
                  dense
                  filled
                  class="q-pa-md col-xs-12 col-md-9"
                  @filter="filtrarSecretarios"
                  use-input
                  input-debounce="0"
                  v-model="value.secretario"
                  option-value="empleadoId"
                  label="Secretario *"
                  :options="secretarioOptions"
                  :rules="[
                    (val) =>
                      Validaciones.validaSelectRequerido(val?.empleadoId),
                  ]"
                  :readonly="formReadOnly"
                  @update:model-value="cambiaronParametros()"
                >
                  <template v-slot:selected>
                    {{
                      (value.secretario?.completo || "") +
                      (value.secretario?.completo ? " - " : "") +
                      (value.secretario?.area || "")
                    }}
                  </template>
                  <template v-slot:option="scope">
                    <q-item v-bind="scope.itemProps">
                      <q-item-section>
                        <q-item-label>{{ scope.opt.completo }}</q-item-label>
                        <q-item-label caption>{{
                          scope.opt.area
                        }}</q-item-label>
                      </q-item-section>
                    </q-item>
                  </template>
                </q-select>

                <q-item class="q-pa-md col-12">
                  <q-item-label class="text-subtitle1 text-bold"
                    >Destinatario</q-item-label
                  >
                </q-item>
                <q-item class="q-pa-md col-12">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Autoridad responsable</q-item-label
                    >
                    <q-item-label>{{ parte.parte || "" }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-input
                  class="q-pa-md col-12"
                  dense
                  outlined
                  maxlength="30000000"
                  v-model="value.objetivo"
                  label="Objetivo *"
                  type="textarea"
                  :rules="reglasObjectivo"
                  :readonly="formReadOnly"
                  @update:model-value="cambiaronParametros()"
                ></q-input>

                <q-item class="q-pa-md col-12">
                  <q-item-label class="text-subtitle1 text-bold"
                    >Autoridad a la que se envia</q-item-label
                  >
                </q-item>
                <q-select
                  v-cortarLabel
                  @input-value="
                    (value.catOrganismo = null), cambiaronParametros()
                  "
                  dense
                  filled
                  use-input
                  input-debounce="0"
                  label-slot
                  v-model="value.catOrganismo"
                  option-label="nombreOficial"
                  option-value="catOrganismoId"
                  :options="organismosOptions"
                  :rules="[
                    (val) =>
                      Validaciones.validaSelectRequerido(val?.catOrganismoId),
                  ]"
                  :readonly="formReadOnly"
                  class="q-pa-md col-xs-12 col-md-9"
                  @filter="filtrarOrganismo"
                  @update:model-value="cambiaronParametros()"
                >
                  <template v-slot:label>
                    <q-item-label
                      >Oficina de Correspondencia Común *</q-item-label
                    >
                  </template>
                </q-select>
              </div>
            </q-form>
          </q-scroll-area>
          <div class="stickyBottom">
            <q-separator></q-separator>
            <q-card-actions v-if="props.parte.asuntoNEUNCOE === 0" align="left">
              <q-btn
                no-caps
                @click="guardarCOE"
                :color="!formValido ? 'grey-6' : 'blue'"
                :disable="!formValido"
                :label="'Enviar'"
                :style="!$q.screen.xs ? 'max-width: 200px' : ''"
                class="q-ml-sm col"
              />
              <q-btn
                no-caps
                @click="cancelar()"
                outline
                label="Cancelar"
                :color="'blue'"
                :style="!$q.screen.xs ? 'max-width: 200px' : ''"
                class="col"
              />
            </q-card-actions>
          </div>
        </div>
      </template>
    </q-splitter>
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
import { computed, onMounted, ref, watch } from "vue";
import { date } from "quasar";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
import { useTramiteStore } from "src/modules/tramite/store/tramite-store";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import { useUsuariosStore } from "src/stores/usuarios-store";
import { Validaciones } from "src/helpers/validaciones";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { noty } from "src/helpers/notify";
import { useActuariaStore } from "src/modules/actuaria/stores/actuaria-store";

const usuariosStore = useUsuariosStore();
const tramitestore = useTramiteStore();
const catalogosStore = useCatalogosStore();
const organismosOptions = ref(null);
const splitterModel = ref(55);
const formCOE = ref(null);
const infoCOE = ref(null);
const cambioFormulario = ref(false);
const showCancelar = ref(false);
const formValido = ref(false);
const formReadOnly = ref(false);
const secretarioOptions = ref([]);
const tipoComunicacionOptions = ref([]);
const reglasNoExpediente = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaNoExpediente(val),
]);
const reglasNoOrigen = ref([(val) => Validaciones.validaInputRequerido(val)]);
const reglasObjectivo = ref([(val) => Validaciones.validaInputRequerido(val)]);
const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);

const actuariaStore = useActuariaStore();

const props = defineProps({
  acuerdo: {
    type: Object,
  },
  parte: {
    type: Object,
  },
  notElecId: {
    type: Number,
  },
});

const emit = defineEmits({
  cerrar: () => true,
});

// eslint-disable-next-line no-unused-vars
const value = ref({});
const nombreArchivo = computed(() => tramitestore.acuerdoNombre);
const sizeArchivo = computed(() =>
  tramitestore.acuerdoBase64?.length / 1024 < 1024
    ? (tramitestore.acuerdoBase64?.length / 1024).toFixed(1) + "KB"
    : (tramitestore.acuerdoBase64?.length / 1024 / 1024).toFixed(1) + "MB",
);
const valoresCatalogos = computed(() => {
  let tipoCom = [];
  let secretario = [];
  let autoridad = [];
  if (
    props.parte.asuntoNEUNCOE !== 0 &&
    tipoCom.length === 0 &&
    tipoComunicacionOptions.value?.length > 0
  ) {
    tipoCom = tipoComunicacionOptions.value?.filter(
      (opc) => opc.id === infoCOE.value.tipoComunicacion,
    );
  }
  if (
    props.parte.asuntoNEUNCOE !== 0 &&
    secretario.length === 0 &&
    secretarioOptions.value?.length > 0
  ) {
    secretario = secretarioOptions.value?.filter(
      (opc) => opc.empleadoId === infoCOE.value.secretario,
    );
  }
  if (
    props.parte.asuntoNEUNCOE !== 0 &&
    autoridad.length === 0 &&
    organismosOptions.value?.length > 0
  ) {
    autoridad = organismosOptions.value?.filter(
      (opc) => opc.catOrganismoId === infoCOE.value.oficinaCorrespondenciaComun,
    );
  }
  return {
    tipoComunicacion: tipoCom,
    secretario: secretario,
    organismo: autoridad,
  };
});

watch(
  valoresCatalogos,
  () => {
    asignarDatosCOE();
  },
  {
    deep: true,
  },
);

onMounted(async () => {
  if (props.parte.asuntoNEUNCOE !== 0) {
    formReadOnly.value = true;
    try {
      infoCOE.value = await actuariaStore.consultarCOE(props.parte.notElecId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }

  try {
    await usuariosStore.obtenerSecretarios();
    secretarioOptions.value = usuariosStore.secretarios;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogosStore.getTipoComunicacion();
    tipoComunicacionOptions.value = catalogosStore.tipoComunicacion;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogosStore.getOrganismosOCC();
    organismosOptions.value = catalogosStore.organismosOcc;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (props.acuerdo.secretarioPId) {
    value.value.secretario = secretarioOptions.value.find(
      (x) => x.empleadoId == props.acuerdo.secretarioPId,
    );
  }
});
function cancelar() {
  if (cambioFormulario.value) {
    showCancelar.value = true;
  } else {
    emit("cerrar", false);
  }
}
/**
 * filtra secretarios en combo
 * @param {*} val valor a buscar
 */
function filtrarSecretarios(val, update) {
  update(
    async () => {
      secretarioOptions.value = Utils.filtrarCombo(
        val,
        usuariosStore.secretarios,
        "completo",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
/**
 * filtra tipo comunicacion en combo
 * @param {*} val valor a buscar
 */
function filtrarTipoComunicacion(val, update) {
  update(
    async () => {
      tipoComunicacionOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tipoComunicacion,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function filtrarOrganismo(val, update) {
  update(
    async () => {
      organismosOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.organismosOcc,
        "nombreOficial",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
async function cambiaronParametros() {
  cambioFormulario.value = true;
  formValido.value = await formCOE.value?.validate(false);
}
async function guardarCOE() {
  if (!(await formCOE.value.validate(false))) {
    return;
  }
  let formData = value.value;

  const fecSplit = formData.fechaEnvio.split("/");

  let coe = {
    notElecId: props.notElecId,
    expediente: formData.numeroExpediente,
    tipoComunicacion: formData.tipoComunicacion.id,
    numeroOrigen: formData.numeroOrigen,
    fechaEnvio: `${fecSplit[2]}-${fecSplit[1]}-${fecSplit[0]}`,
    secretario: formData.secretario.empleadoId,
    mesa: formData.secretario.area,
    tipoAsunto: props.acuerdo.expediente.catTipoAsuntoId,
    numeroExpedienteOrigen: props.acuerdo.expediente.asuntoAlias,
    destinatario: props.parte.parte,
    objetivo: formData.objetivo,
    oficinaCorrespondenciaComun: formData.catOrganismo.catOrganismoId,
  };

  await actuariaStore.insertarCOE(coe).then((res) => {
    if (res) {
      noty.correcto(
        `Notificación C.O.E. asignada correctamente a ${props.parte.parte}`,
      );
      emit("cerrar");
    }
  });
}
async function asignarDatosCOE() {
  if (infoCOE.value) {
    value.value.numeroExpediente = infoCOE.value.asuntoAlias;
    value.value.numeroOrigen = infoCOE.value.numeroOrigen;
    value.value.fechaEnvio = date.formatDate(
      infoCOE.value.fechaEnvio,
      "DD/MM/YYYY",
    );
    value.value.objetivo = infoCOE.value.objetivo;
    value.value.tipoComunicacion =
      valoresCatalogos.value.tipoComunicacion?.length > 0
        ? valoresCatalogos.value.tipoComunicacion[0]
        : {};
    value.value.secretario =
      valoresCatalogos.value.secretario?.length > 0
        ? valoresCatalogos.value.secretario[0]
        : {};
    value.value.catOrganismo =
      valoresCatalogos.value.organismo?.length > 0
        ? valoresCatalogos.value.organismo[0]
        : {};
  }
}
</script>

<style scoped></style>
