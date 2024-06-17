<template>
  <q-card style="min-width: 700px">
    <q-toolbar>
      <q-toolbar-title>Síntesis manual</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-form ref="form" @submit.prevent="onSubmit">
      <q-card-section class="q-gutter-sm">
        <q-item>
          <q-item-section>
            <div class="row q-gutter-md">
              <div class="col">
                <q-select ref="expedienteRef" v-cortarLabel v-permitido="5" label="Buscar un expediente existente *"
                  v-model="varExpedienteEncontrado" @filter="buscarExpedientePorNumero" :options="opcionesExpediente"
                  option-value="asuntoNeunId" use-input input-debounce="0" @update:model-value="expedienteEncontrado"
                  dense filled>
                  <template v-slot:hint>
                    <q-item-label><q-icon size="1.2em" color="light-blue" name="info" />
                      Formato número/AAAA</q-item-label>
                  </template>
                  <template v-slot:append>
                    <q-btn flat round icon="mdi-magnify" />
                  </template>
                  <template v-slot:no-option>
                    <q-item>
                      <q-item-section class="text-red row">
                        <span> <q-icon name="info" /> Expediente inexistente</span>
                      </q-item-section>
                    </q-item>
                  </template>
                  <template v-slot:option="scope">
                    <q-item v-bind="scope.itemProps">
                      <q-item-section>
                        <q-item-label>{{ scope.opt.asuntoAlias }}</q-item-label>
                        <q-item-label caption>{{
                          scope.opt.tipoAsunto
                        }}</q-item-label>
                        <q-item-label class="text-caption" v-if="scope.opt.tipoProcedimiento !== ''">
                          {{ scope.opt.tipoProcedimiento }}</q-item-label>
                      </q-item-section>
                    </q-item>
                  </template>
                </q-select>
              </div>
              <div class="col">
                <q-select v-cortarLabel dense filled use-input input-debounce="0" v-model="cuaderno" label="Cuaderno *"
                  option-label="cuaderno" option-value="cuadernoId" @filter="filtrarCuaderno" :options="cuadernoOptions"
                  @update:model-value="cambiaronParametros"
                  :rules="[(val) => Validaciones.validaSelectRequerido(val?.cuadernoId)]" :loading="buscandoCuadernos">
                </q-select>
              </div>
            </div>
          </q-item-section>
        </q-item>
        <q-item>
          <!-- Fechas -->
          <q-item-section>
            <div class="row q-gutter-md">
              <q-input label="Fecha de auto" filled v-model="fechaAuto" :rules="reglasFecha" class="col">
                <template v-slot:append>
                  <q-icon name="event" class="cursor-pointer">
                    <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                      <q-date v-model="fechaAuto" @update:model-value="onChangeAnyValueForm" mask="DD/MM/YYYY">
                        <div class="row items-center justify-end">
                          <q-btn v-close-popup label="Close" color="primary" flat />
                        </div>
                      </q-date>
                    </q-popup-proxy>
                  </q-icon>
                </template>
              </q-input>
              <q-input label="Fecha de publicación" filled v-model="fechaPublicacion" :rules="reglasFecha" class="col">
                <template v-slot:append>
                  <q-icon name="event" class="cursor-pointer">
                    <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                      <q-date v-model="fechaPublicacion" @update:model-value="onChangeAnyValueForm" mask="DD/MM/YYYY">
                        <div class="row items-center justify-end">
                          <q-btn v-close-popup label="Close" color="primary" flat />
                        </div>
                      </q-date>
                    </q-popup-proxy>
                  </q-icon>
                </template>
              </q-input>
            </div>
          </q-item-section>
        </q-item>
        <q-item>
          <!-- Titular y actuario -->
          <q-item-section>
            <div class="row q-gutter-md">
              <q-select dense @input-value="titularSelected == null" filled class="col" label="Titular"
                :options="optionsTitular" v-model="titularSelected" option-value="empleadoId"
                option-label="nombreEmpleado" @update:model-value="onChangeAnyValueForm" :loading="loadingCatalogs"
                :rules="[(val) => Validaciones.validaSelectRequerido(val?.empleadoId)]">
              </q-select>
              <q-select dense @input-value="actuarioSelected" filled class="col" label="Actuario"
                :options="optionsActuario" v-model="actuarioSelected" option-value="empleadoId"
                option-label="nombreEmpleado" @update:model-value="onChangeAnyValueForm" :loading="loadingCatalogs"
                :rules="[(val) => Validaciones.validaSelectRequerido(val?.empleadoId)]">
              </q-select>
            </div>
          </q-item-section>
        </q-item>
        <q-item>
          <!-- Partes y autoridad -->
          <q-item-section>
            <div class="row q-gutter-md">
              <q-select v-cortarLabel dense filled class="col" use-input input-debounce="0" label-slot
                v-model="partesSelected" option-label="personaTipo" option-value="personaId" lazy-rules
                :options="parteExistenteOptions" @filter="filtrarParteExistente" :loading="loadingPartes"
                @update:model-value="onChangeAnyValueForm"
                :rules="[(val) => Validaciones.validaSelectRequerido(val?.personaId)]">
                <template v-slot:label> Parte </template>
                <template v-slot:no-option>
                  <q-item>
                    <q-item-section class="text-red row">
                      <span v-if="usuariosStore.parteExistente.length === 0">
                        <q-icon name="info" /> No existen partes registradas
                      </span>
                      <span v-else>
                        <q-icon name="info" /> Parte no encontrada</span>
                    </q-item-section>
                  </q-item>
                </template>
                <template v-slot:option="scope">
                  <q-item v-bind="scope.itemProps">
                    <q-item-section>
                      <q-item-label v-if="scope.opt.nombre && scope.opt.aPaterno">
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
              <q-select v-cortarLabel dense filled class="col" use-input input-debounce="0" label-slot
                v-model="autoridadSelected" option-label="personaTipo" option-value="personaId" lazy-rules
                :options="parteExistenteOptions" @filter="filtrarParteExistente"
                @update:model-value="onChangeAnyValueForm"
                :rules="[(val) => Validaciones.validaSelectRequerido(val?.personaId)]">
                <template v-slot:label> Autoridad </template>
                <template v-slot:no-option>
                  <q-item>
                    <q-item-section class="text-red row">
                      <span v-if="usuariosStore.parteExistente.length === 0">
                        <q-icon name="info" /> No existen autoridades registradas
                      </span>
                      <span v-else>
                        <q-icon name="info" /> Autoridad no encontrada</span>
                    </q-item-section>
                  </q-item>
                </template>
                <template v-slot:option="scope">
                  <q-item v-bind="scope.itemProps">
                    <q-item-section>
                      <q-item-label v-if="scope.opt.nombre && scope.opt.aPaterno">
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
        <div class="row q-gutter-sm">
          <div class="col">
            <q-item-label class="q-my-xs">Síntesis del acuerdo</q-item-label>
            <q-editor v-model="sintesis" @update:model-value="onChangeAnyValueForm" />
          </div>
        </div>
      </q-card-section>
      <q-card-actions class="q-gutter-sm">
        <q-space></q-space>
        <q-btn class="q-ml-sm" label="Añadir" :color="formValido ? 'blue' : 'grey-6'" style="min-width: 164px"
          type="submit" :disable="!formValido || !sintesis?.trim()" />
        <q-btn outline label="Cancelar" color="blue" style="width: 200px" @click="showDialogCancelar = true" />
      </q-card-actions>
    </q-form>
    <q-inner-loading :showing="cargando" />
  </q-card>
  <DialogConfirmacion v-model="showDialogCancelar" label-btn-cancel="Cancelar" label-btn-ok="Aceptar"
    titulo="¿Deseas cancelar la síntesis?" :subTitulo="`No se guardará ninguna información que se haya agregado`"
    @aceptar="closeDialogSintesis" @cancelar="showDialogCancelar = false"></DialogConfirmacion>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { noty } from "src/helpers/notify";
import { useOficialiaStore } from "src/modules/oficialia/stores/oficialia-store";
import { Utils } from "../../../helpers/utils";
import { Validaciones } from "../../../helpers/validaciones";
import { manejoErrores } from "../../../helpers/manejo-errores";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { Cuaderno } from "src/data/cuaderno";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useActuariaListaStore } from "../stores/actuaria-lista_acuerdos-store";
import { useUsuariosStore } from "src/stores/usuarios-store";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { date } from "quasar";

const props = defineProps({
  item: {
    type: Object,
    required: true,
  },
  title: {
    type: String,
    default: "Crear síntesis manual"
  },
});

const oficialiaStore = useOficialiaStore();
const catalogosStore = useCatalogosStore();
const usuariosStore = useUsuariosStore();
const authStore = useAuthStore();
const actuariaListaStore = useActuariaListaStore();
let parteExistenteOptions = ref([]);

const isChangedSintesis = ref(false);
const showDialogCancelar = ref(false);
const cargando = ref(false);
const loadingCatalogs = ref(false);
const cuaderno = ref(new Cuaderno());
const buscandoCuadernos = ref(false);
const sintesis = ref(null);
const partesSelected = ref(null);
const autoridadSelected = ref(null);
const actuarioSelected = ref(null);
const titularSelected = ref(null);
const optionsTitular = ref([]);
const optionsActuario = ref([]);
const opcionesExpediente = ref([]);
const cuadernoOptions = ref([]);
const tipoProcedimientoOptions = ref([]);

const varExpedienteEncontrado = ref("");

const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);
const fechaAuto = ref("");
const fechaPublicacion = ref("");

const emit = defineEmits(
  ["cerrar", "refrescar"]
);

async function onChangeAnyValueForm() {
  isChangedSintesis.value = true;
  formValido.value =
    (await form.value?.validate(false));
}

function closeDialogSintesis() {
  emit("cerrar");
}

const form = ref(null);
const formValido = ref(false);

onMounted(async () => {
  loadingCatalogs.value = true;
  await catalogosStore.obtenerTitulares();
  await catalogosStore.obtenerSecretarios();
  await catalogosStore.getZonas();

  optionsTitular.value = catalogosStore.titulares;
  optionsActuario.value = catalogosStore.zonas;
  loadingCatalogs.value = false;

  actuarioSelected.value = optionsActuario.value.filter(
    (el) =>
      el.nombreEmpleado.toLowerCase() === authStore.user.completo.toLowerCase(),
  ) ?? { empleadoId: "", nombreEmpleado: "" };

  try {
    await usuariosStore.obtenerParteExistente(
      props.item?.expediente.asuntoNeunId,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  loadingCatalogs.value = false;
});

async function onSubmit() {
  cargando.value = true;
  const dataToSend = {
    AsuntoNeunId: varExpedienteEncontrado.value?.asuntoNeunId,
    Titular: titularSelected.value?.empleadoId,
    Actuario: actuarioSelected.value?.empleadoId,
    TipoCuaderno: cuaderno.value?.cuadernoId,
    Sintesis: sintesis.value,
    FechaAuto: date.extractDate(fechaAuto.value, 'DD/MM/YYYY'),
    FechaPublicacion: date.extractDate(fechaPublicacion.value, 'DD/MM/YYYY'),
    Quejoso: partesSelected.value?.personaId,
    Autoridad: autoridadSelected.value?.personaId,
  };

  try {
    const response = await actuariaListaStore.addAcuerdoManual(dataToSend);
    if (!response) {
      noty.error("Error, hubo un problema en el proceso de guardado.");
    } else {
      noty.correcto("Se ha guardado la síntesis manual.");
      emit("refrescar");
      emit("cerrar");
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargando.value = false;
}

/**
 * filtra cuaderno en combo
 * @param {*} val valor a buscar
 */
function filtrarCuaderno(val, update) {
  update(
    async () => {
      cuadernoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.cuadernos,
        "cuaderno",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

async function expedienteEncontrado(val) {
  await getCatalogosPorTipoAsunto(val.catTipoAsuntoId);
  await getCuadernos(val.catTipoAsuntoId, val.asuntoNeunId);
  await obtenerPartes(val.asuntoNeunId);
}

async function obtenerPartes(asuntoNeunId) {
  await usuariosStore.obtenerParteExistente(asuntoNeunId);
  await usuariosStore.obtenAutoridad(asuntoNeunId, null, 2, 2);
  parteExistenteOptions.value = usuariosStore.parteExistente;
  //optionsAutoridad.value = usuariosStore.autoridadXExpediente;
}

async function getCatalogosPorTipoAsunto(catTipoAsuntoId) {
  buscandoCuadernos.value = true;
  try {
    await catalogosStore.obtenerProcedimientos(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoProcedimientoOptions.value = catalogosStore.procedimientos;
}

async function getCuadernos(catTipoAsuntoId, asuntoNeunId) {
  try {
    await catalogosStore.obtenerCuadernos(catTipoAsuntoId, asuntoNeunId);
    cuadernoOptions.value = catalogosStore.cuadernos;
    cuaderno.value = catalogosStore.cuadernos[0];
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  buscandoCuadernos.value = false;
}

async function buscarExpedientePorNumero(val, update, abort) {
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
          await oficialiaStore.buscarExpediente(val, null);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }

        opcionesExpediente.value = oficialiaStore.expediente;
        if (opcionesExpediente.value?.length === 1) {
          buscarExpediente.value = opcionesExpediente.value[0];
        }
      }
    },
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 700),
  );
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
