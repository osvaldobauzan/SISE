<template>
  <q-card style="min-width: 40vw">
    <q-toolbar>
      <q-toolbar-title class="q-ml-md text-weight-bold"
        >Agendar audiencia</q-toolbar-title
      >
      <q-btn flat round dense icon="mdi-close" @click="cancelar" />
    </q-toolbar>
    <q-card-section class="q-gutter-sm">
      <q-form ref="form">
        <div class="row">
          <q-item-label class="q-pa-md text-weight-bold col-12">
            Datos del expediente
          </q-item-label>
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
            @input-value="audiencia = null"
            dense
            filled
            class="q-pa-md col-xs-12 col-sm-6"
            use-input
            input-debounce="0"
            v-model="audiencia"
            label="Audiencia *"
            option-label="descripcion"
            option-value="id"
            @filter="filtrarAudiencia"
            @update:model-value="cambioForm"
            :options="audienciaOptions"
            :loading="cargandoAudiencia"
            :rules="[
              (val) => Validaciones.validaSelectRequerido(val?.descripcion),
            ]"
          >
          </q-select>
          <q-item-label class="q-pl-md q-pt-md text-weight-bold col-12">
            Secretario
          </q-item-label>
          <q-select
            v-cortarLabel
            @input-value="secretario = null"
            dense
            filled
            class="q-pa-md col-12"
            @filter="filtrarSecretarios"
            use-input
            input-debounce="0"
            v-model="secretario"
            option-value="empleadoId"
            label="Secretario *"
            :options="secretarioOptions"
            :rules="[
              (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
            ]"
            @update:model-value="cambioForm"
          >
            <template v-slot:selected>
              {{
                (secretario?.completo || "") +
                (secretario?.completo ? " - " : "") +
                (secretario?.area || "")
              }}
            </template>
            <template v-slot:option="scope">
              <q-item v-bind="scope.itemProps">
                <q-item-section>
                  <q-item-label>{{ scope.opt.completo }}</q-item-label>
                  <q-item-label caption>{{ scope.opt.area }}</q-item-label>
                </q-item-section>
              </q-item>
            </template>
          </q-select>
          <q-item-label
            class="q-pl-md q-pt-md text-weight-bold col-12"
            v-if="expedienteEncontrado"
          >
            {{ detalleActor.descripcionCaracterPersona || "" }}
          </q-item-label>
          <q-item-label
            class="q-pl-md q-pb-md text-grey-6 col-12"
            v-if="expedienteEncontrado"
          >
            <template v-if="detalleActor.catTipoPersonaId == 3">
              {{ detalleActor.denominacionDeAutoridad || "" }}
            </template>
            <template v-else>
              {{
                `${detalleActor.nombre || ""} ${detalleActor.aPaterno || ""} ${detalleActor.aMaterno || ""}`
              }}
            </template>
          </q-item-label>

          <q-input
            dense
            filled
            class="q-pa-md col-xs-12 col-sm-6"
            v-model="fecha"
            label="Fecha *"
            :rules="reglasFecha"
            mask="##/##/####"
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
                    v-model="fecha"
                    @update:model-value="cambioForm()"
                    mask="DD/MM/YYYY"
                    :options="
                      (d) =>
                        d >= date.formatDate(new Date(), 'YYYY/MM/DD') &&
                        d <= '2199/01/01'
                    "
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
            class="q-pa-md col-xs-12 col-sm-6"
            v-model="hora"
            label="Hora *"
            :rules="reglasHora"
            @update:model-value="cambioForm"
          >
            <template v-slot:append>
              <q-icon name="mdi-clock-time-four-outline" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-time
                    v-model="hora"
                    @update:model-value="cambioForm"
                    mask="HH:mm"
                    format24h
                    :hour-options="[9, 10, 11, 12, 13, 14]"
                  >
                    <div class="row items-center justify-end">
                      <q-btn
                        v-close-popup
                        label="Cerrar"
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
      </q-form>
    </q-card-section>
    <q-card-actions class="q-ml-lg" align="left">
      <q-btn
        class="col-xs-5 col-sm-3 col-md-2"
        no-caps
        :disable="!formValido"
        :color="formValido ? 'blue' : 'grey-6'"
        label="Guardar"
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
    <q-inner-loading :showing="cargandoGuardado"> </q-inner-loading>
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
import { ref, onMounted } from "vue";
import { Validaciones } from "src/helpers/validaciones";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { Utils } from "src/helpers/utils";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useSentenciasStore } from "src/modules/sentencias/store/sentencias-store";
import { useUsuariosStore } from "src/stores/usuarios-store";
import { useAgendaStore } from "src/modules/agenda/stores/agenda-store";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { date } from "quasar";

const agendaStore = useAgendaStore();
const usuariosStore = useUsuariosStore();
const catalogosStore = useCatalogosStore();
const secretarioOptions = ref(usuariosStore.secretarios);
const secretario = ref(null);
const sentenciasStore = useSentenciasStore();
const expedienteEncontrado = ref(null);
const opcionesExpediente = ref([]);

const hora = ref();
//const submitForm = ref();
const audiencia = ref(null);
const audienciaOptions = ref([]);
const cambioFormulario = ref(false);
const showCancelar = ref(false);
const cargandoAudiencia = ref(false);
const detalleActor = ref({});
const form = ref(null);
const formValido = ref(false);
const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);
const reglasHora = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaHoraLaboral(val),
]);

const props = defineProps({
  item: {
    type: Object,
    required: false,
    default: () => ({}),
  },
  currentDay: {
    type: String,
    default: null,
  },
});
const fecha = ref(props.currentDay);
const emit = defineEmits({
  cerrar: () => true,
});
onMounted(async () => {
  
  try {
    await usuariosStore.obtenerSecretarios();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  secretarioOptions.value = usuariosStore.secretarios;
});
function cancelar() {
  if (cambioFormulario.value) {
    showCancelar.value = true;
  } else {
    emit("cerrar", false);
  }
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
      }, 100),
  );
}
async function cambioExpediente(val) {
  audiencia.value = null;
  if (val) {
    await obtenCatalogosDependientes(
      val.asuntoNeunId,
      val.asuntoAlias,
      val.catTipoAsuntoId,
    );
  }
  cambioForm();
}
/**
 * filtra audiencia en combo
 * @param {*} val valor a buscar
 */
function filtrarAudiencia(val, update) {
  update(
    async () => {
      audienciaOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.audiencias,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
async function obtenCatalogosDependientes(
  asuntoNeunId,
  numeroExpediente,
  catTipoAsuntoId,
) {
  cargandoAudiencia.value = true;
  try {
    await catalogosStore.obtenerAdiencia(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoAudiencia.value = false;
  try {
    detalleActor.value = await agendaStore.obtenerDetalleCarcater(
      catTipoAsuntoId,
      asuntoNeunId,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  audienciaOptions.value = catalogosStore.audiencias;
  await secretarioSugerido(asuntoNeunId);
}

async function cambioForm() {
  formValido.value = await form.value?.validate(false);
  cambioFormulario.value = true;
}
async function secretarioSugerido(val) {
  try {
    const secretarioSugerido =
      await usuariosStore.obtenerSecretarioSugerido(val);
    if (secretarioSugerido && secretarioSugerido.secretario) {
      secretario.value = secretarioOptions.value?.find(
        (t) => t.empleadoId == secretarioSugerido.secretario,
      );
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
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
</script>
