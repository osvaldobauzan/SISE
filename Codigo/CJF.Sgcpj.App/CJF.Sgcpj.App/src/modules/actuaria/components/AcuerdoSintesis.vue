<template>
  <q-card>
    <q-form ref="form" @submit="guardarSintesis">
      <q-splitter
        v-model="splitterModel"
        :limits="[50, 100]"
        style="width: 100%; height: 100%"
      >
        <template v-slot:before>
          <VerAcuerdo :model-value="item" :esDialogo="false"> </VerAcuerdo>
        </template>

        <template v-slot:after>
          <q-toolbar>
            <q-toolbar-title class="text-bold">{{ title }}</q-toolbar-title>
            <q-btn flat round dense icon="mdi-close" @click="cerrarSintesis" />
          </q-toolbar>
          <q-separator></q-separator>
          <q-card-section>
            <div class="row">
              <q-item>
                <q-item-section>
                  <q-item-label caption>Expediente </q-item-label>
                  <q-item-label>
                    {{ item.expediente.asuntoAlias }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Tipo de asunto </q-item-label>
                  <q-item-label>
                    {{ item.expediente.catTipoAsunto }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item v-if="item.expediente.tipoProcedimiento">
                <q-item-section>
                  <q-item-label caption>Tipo de Procedimiento</q-item-label>
                  <q-item-label>
                    {{ item.expediente.tipoProcedimiento }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Acuerdo</q-item-label>
                  <q-item-label> {{ item.contenido }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption> Creación del acuerdo</q-item-label>
                  <q-item-label>{{ item.fechaAuto_F }}</q-item-label>
                </q-item-section>
              </q-item>
            </div>
            <q-item>
              <q-item-section>
                <div class="row q-gutter-md">
                  <q-input
                    dense
                    filled
                    class="col-3 q-pr-lg"
                    v-model="fechaPublicacion"
                    label="Fecha *"
                    :rules="reglasFecha"
                  >
                    <template v-slot:append>
                      <q-icon name="mdi-calendar-month" class="cursor-pointer">
                        <q-popup-proxy
                          cover
                          transition-show="scale"
                          transition-hide="scale"
                        >
                          <q-date
                            v-model="fechaPublicacion"
                            @update:model-value="cambioSintesis = true"
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
                    dense
                    @input-value="titularId == null"
                    filled
                    class="col"
                    label="Titular"
                    :options="optionsTitular"
                    v-model="titularId"
                    option-value="empleadoId"
                    option-label="nombreEmpleado"
                    @update:model-value="cambioForm"
                    :loading="loadingCatalogs"
                    :rules="[
                    (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
                    ]"
                  >
                  </q-select>
                  <q-select
                    dense
                    @input-value="actuarioId == null"
                    filled
                    class="col"
                    label="Actuario"
                    :options="optionsActuario"
                    v-model="actuarioSelected"
                    option-value="empleadoId"
                    option-label="nombreEmpleado"
                    @update:model-value="cambioForm"
                    :loading="loadingCatalogs"
                    :rules="[
                    (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
                    ]"
                  >
                  </q-select>
                </div>
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <div class="row q-gutter-md">
                  <q-select
                    v-cortarLabel
                    dense
                    filled
                    class="col"
                    use-input
                    input-debounce="0"
                    label-slot
                    v-model="parte"
                    option-label="personaTipo"
                    option-value="personaId"
                    lazy-rules
                    :options="parteExistenteOptions"
                    @filter="filtrarParteExistente"
                    :loading="loadingPartes"
                    @update:model-value="cambioForm"
                    :rules="[
                    (val) => Validaciones.validaSelectRequerido(val?.personaId),
                    ]"
                  >
                    <template v-slot:label> Parte </template>
                    <template v-slot:no-option>
                      <q-item>
                        <q-item-section class="text-red row">
                          <span v-if="usuariosStore.parteExistente.length === 0">
                            <q-icon name="info" /> No existen partes registradas
                          </span>
                          <span v-else>
                            <q-icon name="info" /> Parte no encontrada</span
                          >
                        </q-item-section>
                      </q-item>
                    </template>
                    <template v-slot:option="scope">
                      <q-item v-bind="scope.itemProps">
                        <q-item-section>
                          <q-item-label
                            v-if="scope.opt.nombre && scope.opt.aPaterno"
                          >
                            {{ scope.opt.nombre }} {{ scope.opt.aPaterno }}
                            {{ scope.opt.aMaterno }}
                          </q-item-label>
                          <q-item-label v-else>
                            {{ scope.opt.denominacionDeAutoridad }}
                          </q-item-label>

                          <q-item-label caption>
                            {{ scope.opt.descripcionCaracterPersona }}
                          </q-item-label>
                        </q-item-section>
                      </q-item>
                    </template>
                  </q-select>

                  <q-select
                    v-cortarLabel
                    dense
                    filled
                    class="col"
                    use-input
                    input-debounce="0"
                    label-slot
                    v-model="autoridad"
                    option-label="personaTipo"
                    option-value="personaId"
                    lazy-rules
                    :options="parteExistenteOptions"
                    @filter="filtrarParteExistente"
                    @update:model-value="cambioForm"
                    :rules="[
                    (val) => Validaciones.validaSelectRequerido(val?.personaId),
                    ]"
                  >
                    <template v-slot:label> Autoridad </template>
                    <template v-slot:no-option>
                      <q-item>
                        <q-item-section class="text-red row">
                          <span v-if="usuariosStore.parteExistente.length === 0">
                            <q-icon name="info" /> No existen partes registradas
                          </span>
                          <span v-else>
                            <q-icon name="info" /> Parte no encontrada</span
                          >
                        </q-item-section>
                      </q-item>
                    </template>
                    <template v-slot:option="scope">
                      <q-item v-bind="scope.itemProps">
                        <q-item-section>
                          <q-item-label
                            v-if="scope.opt.nombre && scope.opt.aPaterno"
                          >
                            {{ scope.opt.nombre }} {{ scope.opt.aPaterno }}
                            {{ scope.opt.aMaterno }}
                          </q-item-label>
                          <q-item-label v-else>
                            {{ scope.opt.denominacionDeAutoridad }}
                          </q-item-label>

                          <q-item-label caption>
                            {{ scope.opt.descripcionCaracterPersona }}
                          </q-item-label>
                        </q-item-section>
                      </q-item>
                    </template>
                  </q-select>
                </div>
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-field
                  v-model="sintesis"
                  class="full-width"
                  borderless
                >
                  <q-editor
                    :toolbar="[]"
                    @update:model-value="cambioForm"
                    toolbar-outline="true"
                    class="full-width"
                    v-model="sintesis"
                  >
                  </q-editor>
                </q-field>
              </q-item-section>
            </q-item>
          </q-card-section>
          <q-card-actions>
            <q-btn
              class="q-ml-sm"
              :label="title.includes('Crear') ? 'Crear síntesis' : 'Guardar'"
              :color="formValido ? 'blue' : 'grey-6'"
              style="min-width: 164px"
              type="submit"
              :disable="!formValido || !sintesis?.trim()"
            />
            <q-btn
              outline
              label="Cancelar"
              color="blue"
              style="width: 200px"
              @click="cerrarSintesis"
            />
          </q-card-actions>
        </template>
      </q-splitter>
    </q-form>
    <q-inner-loading :showing="cargando" />
  </q-card>
  <DialogConfirmacion
    v-model="showCancelar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="¿Deseas cancelar la síntesis?"
    :subTitulo="`No se guardará ninguna información que se haya agregado`"
    @aceptar="emit('cerrar', false)"
  ></DialogConfirmacion>
</template>
<script setup>
import { ref, onMounted } from "vue";
import { useActuariaSintesisStore } from "../stores/actuaria-sintesis-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { noty } from "src/helpers/notify";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { date } from "quasar";
import { Utils } from "../../../helpers/utils";
import { Validaciones } from "src/helpers/validaciones";
const catalogosStore = useCatalogosStore();
const showCancelar = ref(false);
const cargando = ref(false);
const cambioSintesis = ref(false);
const sintesis = ref("");
const detalleSintesis = ref(null);
const fechaPublicacion = ref("");
const optionsTitular = ref([]);
const optionsActuario = ref([]);
const loadingCatalogs = ref(false);
const loadingPartes = ref(false);
const titularId = ref("");
const actuarioSelected = ref(null);
let parteExistenteOptions = ref([]);
const actuariaSintesisStore = useActuariaSintesisStore();
const usuariosStore = useUsuariosStore();
const parte = ref("");
const autoridad = ref("");
const splitterModel = ref(50);
const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);

const form = ref(null);
const formValido = ref(false);
// eslint-disable-next-line no-unused-vars
const props = defineProps({
  item: {
    type: Object,
    required: true,
  },
  title: {
    type: String,
    default: "Crear Síntesis",
  },
});
const emit = defineEmits({
  // v-model event with validation
  cerrar: (value) => value,
});

async function cambioForm() {
  formValido.value =
    (await form.value?.validate(false));
}

function cerrarSintesis() {
  //if (cambioSintesis.value && sintesis.value != props.item.sintesis) {
  if (cambioSintesis.value) {
    showCancelar.value = true;
  } else {
    emit("cerrar", false);
  }
}
onMounted(async () => {
  sintesis.value = props.item.sintesis || "";

  loadingCatalogs.value = true;
  try {
    await catalogosStore.obtenerTitulares();
    await catalogosStore.getZonas();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  optionsTitular.value = catalogosStore.titulares;
  optionsActuario.value = catalogosStore.zonas;
  loadingCatalogs.value = false;

  loadingCatalogs.value = true;
  try {
    await usuariosStore.obtenerParteExistente(
      props.item?.expediente.asuntoNeunId,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  parteExistenteOptions.value = usuariosStore.parteExistente;
  loadingCatalogs.value = false;

  const detalle = await actuariaSintesisStore.getSintesis({
    params: {
      asuntoNeunId: props.item?.expediente?.asuntoNeunId,
      sintesisOrden: props.item?.sintesisOrden,
    },
  });
  if (detalle && detalle.length > 0) {
    detalleSintesis.value = detalle[0];
    fechaPublicacion.value = date.formatDate(
      new Date(detalleSintesis.value.fechaPublicacion),
      "DD/MM/YYYY",
    );

    if (detalleSintesis.value.titular > 0) {
      titularId.value = optionsTitular.value.find(
        (opcion) => opcion.empleadoId === detalleSintesis.value.titular,
      );
    }
    if (detalleSintesis.value.actuarioId > 0) {
      actuarioSelected.value = optionsActuario.value.find(
        (opcion) => opcion.empleadoId === detalleSintesis.value.actuarioId,
      );
    }
    if (detalleSintesis.value.parte1 > 0) {
      parte.value = parteExistenteOptions.value.find(
        (opcion) => opcion.personaId === detalleSintesis.value.parte1,
      );
    }
    if (detalleSintesis.value.parte2 > 0) {
      autoridad.value = parteExistenteOptions.value.find(
        (opcion) => opcion.personaId === detalleSintesis.value.parte2,
      );
    }
  }
});

async function guardarSintesis() {
  cargando.value = true;

  const vpublicacion = fechaPublicacion.value.split("/");
  //const fPublicacion = new Date(vpublicacion[1]+'/'+vpublicacion[0]+'/'+vpublicacion[2]);

  const params = {
    asuntoNeunId: props.item?.expediente.asuntoNeunId,
    tipoCuaderno: props.item?.tipoCuaderno,
    nombreDocumento: props.item?.nombreArchivo,
    extensionDocumento: "",
    contenido: props.item?.contenidoId,
    fechaAcuerdo: props.item?.fechaAutoriza,
    usuarioCaptura: props.item?.usuarioCaptura,
    asuntoDocumentoId: props.item?.asuntoDocumentoId,
    sintesis: cleanString(sintesis.value?.trim()),
    sintesisOrden: props.item?.sintesisOrden,
    nombreArchivo: "",
    fechaPublicacion:
      vpublicacion[1] + "-" + vpublicacion[0] + "-" + vpublicacion[2],
    titular: titularId.value?.empleadoId,
    parte1: parte.value?.personaId,
    parte2: autoridad.value.personaId,
    ActuarioId : actuarioSelected.value?.empleadoId,
  };

  if (props.title.includes("Crear")) {
    try {
      await actuariaSintesisStore.postSintesis(params);
      noty.correcto(
        `Se generó la síntesis para un acuerdo del expediente  ${props.item.expediente.asuntoAlias}`,
      );
      emit("cerrar", true);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  } else {
    try {
      await actuariaSintesisStore.putSintesis(params);
      noty.correcto(
        `Se ha realizado una edición en la síntesis correspondiente al acuerdo con fecha ${props.item.fechaAuto_F} del expediente ${props.item.expediente.asuntoAlias}`,
      );
      emit("cerrar", true);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
  cargando.value = false;
}

function cleanString(string) {
  return string.replaceAll(/&nbsp;/g, " ").replaceAll(/<[^>]*>?/gm, "");
}

function filtrarParteExistente(val, update) {
  update(
    async () => {
      parteExistenteOptions.value = Utils.filtrarCombo(
        val,
        usuariosStore.parteExistente,
        "personaTipo",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>
<style>
.q-field__control {
  color: black;
}
</style>