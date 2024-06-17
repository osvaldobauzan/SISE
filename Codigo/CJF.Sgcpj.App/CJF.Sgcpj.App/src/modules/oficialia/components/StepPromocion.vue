<template>
  <p>
    Elige una opción, si la promoción es un "Escrito inicial" crea un
    expediente. En caso contrario busca un expediente ya existente.
  </p>
  <div class="row q-gutter-sm q-pr-xs" v-if="origen !== 14 || origen != 22">
    <div class="col q-pr-sm">
      <q-select
        ref="expedienteRef"
        v-cortarLabel
        v-permitido="5"
        label="Buscar un expediente existente *"
        v-model="parametros.expedienteEncontrado"
        @input="funcionInput"
        @filter="buscarExpedientePorNumero"
        :options="opcionesExpediente"
        option-value="asuntoNeunId"
        use-input
        input-debounce="0"
        @update:model-value="expedienteEncontrado"
        dense
        filled
        :rules="[
          (val) =>
            expedienteNuevo ||
            esEdicion ||
            Validaciones.validaSelectRequerido(val),
        ]"
      >
        <template v-slot:hint>
          <q-item-label
            ><q-icon size="1.2em" color="light-blue" name="info" /> Formato
            Número/AAAA</q-item-label
          >
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
              <q-item-label caption>{{ scope.opt.tipoAsunto }}</q-item-label>
              <q-item-label
                class="text-caption"
                v-if="scope.opt.tipoProcedimiento !== ''"
                >{{ scope.opt.tipoProcedimiento }}</q-item-label
              >
            </q-item-section>
          </q-item>
        </template>
      </q-select>
    </div>
    <div class="col q-gutter-x-sm">
      <q-btn
        outline
        v-permitido="4"
        @click="setExpedienteNuevo"
        icon="mdi-folder-plus"
        no-caps
        color="primary"
        :label="'Crear un expediente'"
      ></q-btn>
      <q-btn
        @click="
          maximizedToggle = false;
          showExpediente = true;
        "
        icon="mdi-book-open-variant-outline-outline"
        no-caps
        flat
        color="primary"
        :label="'Ver expediente electrónico'"
        v-if="parametros.expedienteEncontrado"
      ></q-btn>
    </div>
  </div>
  <q-item-label
    v-if="expedienteNuevo || esEdicion"
    class="text-bold q-my-md text-subtitle1"
    >Datos del expediente</q-item-label
  >
  <div v-if="expedienteNuevo || esEdicion" class="row q-gutter-lg">
    <q-select
      v-cortarLabel
      v-focus
      ref="selectTipoAsunto"
      class="col"
      dense
      filled
      use-input
      input-debounce="0"
      v-model="parametros.tipoAsunto"
      label="Tipo de Asunto *"
      :options="tipoAsuntosOpciones"
      @filter="filtrarTipoAsunto"
      @update:model-value="
        (val) => {
          refrescaCatalogosDependientes(val), emit('cambioExpediente', true);
        }
      "
      option-label="tipoAsunto"
      option-value="catTipoAsuntoId"
      :rules="[
        (val) => Validaciones.validaSelectRequerido(val?.catTipoAsuntoId),
      ]"
      :disable="esEdicion && !editaExpediente"
    />
    <q-select
      v-cortarLabel
      @input-value="parametros.tipoProcedimiento = null"
      dense
      filled
      use-input
      input-debounce="0"
      ref="tipoP"
      class="col"
      :options="tipoProcedimientoOptions"
      v-if="labelTipoProcedimiento.includes('*')"
      v-model="parametros.tipoProcedimiento"
      option-label="descripcion"
      option-value="id"
      :label="labelTipoProcedimiento"
      @filter="filtrarTipoProcedimiento"
      @update:model-value="cambioTipoProcedimento"
      :rules="reglasTipoProcedimiento"
      :label-slot="true"
      :disable="esEdicion && !editaExpediente"
    />
  </div>
  <div v-if="expedienteNuevo || esEdicion" class="row q-gutter-lg">
    <q-input
      ref="numExpediente"
      dense
      filled
      class="col"
      v-model="parametros.numeroExpediente"
      @update:model-value="
        () => {
          cambiaronParametros();
          emit('cambioExpediente', true);
        }
      "
      label="Crea el número de expediente *"
      :rules="reglasNoExpediente"
      :error="esExpedienteYaUtilizado"
      :disable="esEdicion && !editaExpediente"
    >
      <template v-slot:hint>
        <q-item-label
          ><q-icon size="1.2em" color="light-blue" name="info" /> Formato
          Número/AAAA</q-item-label
        >
      </template>
      <template v-slot:error>
        <span class="red"><q-icon name="info" /></span> Expediente ya existente
      </template>
    </q-input>
    <!----<q-input
      dense
      filled
      class="col"
      v-model="parametros.numeroOCC"
      @update:model-value="
        () => {
          entradaOCC();
          cambiaronParametros();
          emit('cambioExpediente', true);
        }
      "
      label="Oficina de Correspondencia Común *"
      :rules="reglasOCC"
      :disable="esEdicion && !editaExpediente"
    >-->

    <q-input
      dense
      filled
      class="col"
      v-model="parametros.numeroOCC"
      @update:model-value="
        () => {
          entradaOCC();
          cambiaronParametros();
          emit('cambioExpediente', true);
        }
      "
      label="Oficina de Correspondencia Común"
      :rules="reglasOCC"
      :disable="esEdicion && !editaExpediente"
    >
    </q-input>
  </div>
  <q-item-label class="text-bold text-subtitle1 q-mb-md q-mt-lg"
    >Selecciona un tipo de cuaderno</q-item-label
  >
  <div class="row q-gutter-lg">
    <q-select
      v-cortarLabel
      @input-value="parametros.amparoEnRevision = null"
      dense
      filled
      use-input
      input-debounce="0"
      ref="tipoAR"
      class="col"
      :options="amparoEnRevisionOptions"
      v-if="labelAmparoEnRevision.includes('*')"
      v-model="parametros.amparoEnRevision"
      option-label="descripcion"
      option-value="id"
      :label="labelAmparoEnRevision"
      @filter="filtrarAmparoEnRevision"
      @update:model-value="cambioAmparoEnRevision"
      :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
      :label-slot="true"
      :loading="buscandoAmparoEnRevision"
    />
    <q-select
      v-cortarLabel
      dense
      filled
      class="col"
      use-input
      input-debounce="0"
      v-model="parametros.cuaderno"
      label="Cuaderno *"
      option-label="cuaderno"
      option-value="cuadernoId"
      @filter="filtrarCuaderno"
      :options="cuadernoOptions"
      @update:model-value="cambiaronParametros"
      :rules="[(val) => Validaciones.validaSelectRequerido(val?.cuadernoId)]"
      :loading="buscandoCuadernos"
    ></q-select>
    <div class="col"></div>
  </div>
  <q-item-label class="text-bold q-my-md text-subtitle1"
    >Información de la promoción</q-item-label
  >
  <div class="row q-gutter-sm">
    <div class="row col-6">
      <q-input
        dense
        filled
        class="col-8 q-pr-lg"
        type="number"
        @keydown="onlyNumber($event)"
        v-model.number="parametros.registro"
        label="Número de registro"
        @update:model-value="cambiaronParametros"
        maxlength="6"
        min="1"
        :rules="reglasNoPromocion"
        :error="esPromocionYaExiste"
      >
        <template v-slot:error>
          <span class="red"><q-icon name="info" /></span> Número ya existente
        </template>
      </q-input>

      <q-input
        dense
        filled
        class="col-4 q-pr-md"
        v-model.number="parametros.copias"
        label="Copias"
        type="number"
        min="0"
        maxlength="2"
        :rules="[
          (val) => Validaciones.validaValorMin(val, 0),
          (val) => Validaciones.validaValorMax(val, 99),
        ]"
        @update:model-value="cambiaronParametros"
        @click="borrarSiEsCero('copias')"
      ></q-input
      ><!--Aqui va valor min-->
    </div>

    <q-input
      dense
      filled
      class="col-2"
      v-model.number="parametros.fojas"
      label="Fojas"
      min="1"
      type="number"
      :rules="[
        (val) => Validaciones.validaValorMin(val, 0),
        (val) => Validaciones.validaValorMax(val, 32767),
      ]"
      @update:model-value="cambiaronParametros"
      @click="borrarSiEsCero('fojas')"
    ></q-input
    ><!--Aqui va valor min-->
    <q-select
      v-cortarLabel
      @input-value="parametros.contenido = null"
      dense
      filled
      class="col q-pl-md"
      v-model="parametros.contenido"
      label="Contenido"
      use-input
      input-debounce="0"
      option-label="descripcion"
      @filter="filtrarContenido"
      option-value="id"
      :options="contenidoOptions"
      @update:model-value="cambiaronParametros"
      :rules="[(val) => Validaciones.validaSelectRequeridoCont(val?.id)]"
      :loading="buscandoContenido"
    ></q-select>
  </div>
  <q-item-label class="text-bold q-my-md text-subtitle1"
    >Datos de presentación</q-item-label
  >
  <div class="row">
    <q-input
      dense
      filled
      class="col-3 q-pr-lg"
      v-model="parametros.fechaPresentacion"
      label="Fecha *"
      :rules="reglasFecha"
    >
      <template v-slot:append>
        <q-icon name="mdi-calendar-month" class="cursor-pointer">
          <q-popup-proxy cover transition-show="scale" transition-hide="scale">
            <q-date
              v-model="parametros.fechaPresentacion"
              @update:model-value="cambiaronParametros"
              mask="DD/MM/YYYY"
            >
              <div class="row items-center justify-end">
                <q-btn v-close-popup label="Cerrar" color="primary" flat />
              </div>
            </q-date>
          </q-popup-proxy>
        </q-icon>
      </template>
    </q-input>
    <q-input
      dense
      filled
      class="col-3 q-pr-md q-ml-xs"
      v-model="parametros.horaPresentacion"
      label="Hora *"
      :rules="reglasHora"
      @update:model-value="cambiaronParametros"
    >
      <template v-slot:append>
        <q-icon name="mdi-clock-time-four-outline" class="cursor-pointer">
          <q-popup-proxy cover transition-show="scale" transition-hide="scale">
            <q-time
              v-model="parametros.horaPresentacion"
              @update:model-value="cambiaronParametros"
              mask="HH:mm"
              format24h
            >
              <div class="row items-center justify-end">
                <q-btn v-close-popup label="Cerrar" color="primary" flat />
              </div>
            </q-time>
          </q-popup-proxy>
        </q-icon>
      </template>
    </q-input>
    <q-input
      dense
      borderless
      class="col q-pl-md"
      :model-value="parametros.origenDescripcion"
      label="Origen"
      disable
    ></q-input>
  </div>
  <q-item-label class="text-bold q-my-md text-subtitle1"
    >Asigna la promoción a una mesa</q-item-label
  >
  <div class="row q-gutter-lg">
    <q-select
      v-cortarLabel
      @input-value="parametros.secretario = null"
      dense
      filled
      class="col"
      @filter="filtrarSecretarios"
      use-input
      input-debounce="0"
      v-model="parametros.secretario"
      option-value="empleadoId"
      label="Secretario *"
      :options="secretarioOptions"
      :rules="[(val) => Validaciones.validaSelectRequerido(val?.empleadoId)]"
      @update:model-value="cambiaronParametros"
    >
      <template v-slot:selected>
        {{
          (parametros.secretario?.completo || "") +
          (parametros.secretario?.completo ? " - " : "") +
          (parametros.secretario?.area || "")
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
    <div class="col"></div>
  </div>
  <q-dialog v-model="showExpediente" :maximized="maximizedToggle">
    <ModalWindowComponent
      :maximizedToggle="maximizedToggle"
      @toggle-maximized="maximizedToggle = !maximizedToggle"
    >
      <ExpedientePage
        :asuntoNeunId="parametros.expedienteEncontrado.asuntoNeunId"
        :asuntoAlias="parametros.expedienteEncontrado.asuntoAlias"
        :tipoAsunto="parametros.expedienteEncontrado.tipoAsunto"
      />
    </ModalWindowComponent>
  </q-dialog>
</template>

<script setup>
import {
  computed,
  watch,
  onMounted,
  onUpdated,
  onBeforeUnmount,
  ref,
} from "vue";
import { Utils } from "../../../helpers/utils";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { useOficialiaStore } from "../stores/oficialia-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { usePromoventesStore} from "../stores/promoventes-store";
import { Validaciones } from "../../../helpers/validaciones";
import { date } from "quasar";
import { FormPromocion } from "../data/form-promocion";
import { DetallePromocion } from "../data/detalle-promocion";
import { manejoErrores } from "../../../helpers/manejo-errores";

import ModalWindowComponent from "src/components/ModalWindowComponent.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";

const maximizedToggle = ref(false);
const showExpediente = ref(false);
const numExpediente = ref(null);
const selectTipoAsunto = ref(null);
const numeroCopias = ref();
const numeroFojas = ref();
const tipoAsuntoOrigen = ref("");
const contenidoOriginal = ref("");
const tipoAsuntoReadOnly = ref("");
const cuadernoReadOnly = ref("");
const contenidoReadOnly = ref("");
const catalogosStore = useCatalogosStore();
const oficialiaStore = useOficialiaStore();
const usuariosStore = useUsuariosStore();
const promoventeStore = usePromoventesStore();
const labelTipoProcedimiento = ref(`Tipo de Procedimiento`);
const labelAmparoEnRevision = ref(`Clasificación Amparo en Revisión`);
const tipoAsuntosOpciones = ref(catalogosStore.asuntos);
const cuadernoOptions = ref([]);
const contenidoOptions = ref([]);
const tipoProcedimientoOptions = ref([]);
const amparoEnRevisionOptions = ref([]);
const parametros = ref(new FormPromocion());
const buscarExpediente = ref("");
const expedienteNuevo = ref(false);
const editaExpediente = ref(false);
const secretarioOptions = ref(usuariosStore.secretarios);
const detalle = ref(new DetallePromocion());
const opcionesExpediente = ref([]);
const expedienteRef = ref(null);
const buscandoCuadernos = ref(false);
const buscandoContenido = ref(false);
const buscandoAmparoEnRevision = ref(false);
const reglasNoExpediente = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaNoExpediente(val),
  (val) => {
    return validaNoExpedienteApi(val);
  },
]);
const reglasNoPromocion = ref([
  (val) => Validaciones.validaValorMinPromocion(val, 1),
  //(val) => Validaciones.validaValorMin(val, 1),
  (val) => Validaciones.validaValorMax(val, 999999),
  (val) => {
    return numeroPromocionExistente(val);
  },
]);

const esExpedienteYaUtilizado = ref(false);

const esPromocionYaExiste = ref(false);

defineExpose({
  setFocusExpediente() {
    expedienteRef.value?.focus();
  },
});

const origen = computed(() => {
  if (props.promocion) {
    return parseInt(props.promocion.origen.toString());
  } else {
    return null;
  }
});

const reglasTipoProcedimiento = ref([
  (val) =>
    parametros.value?.tipoAsunto &&
    [6, 9, 18, 74, 72, 67, 125, 126, 137].some(
      (x) => x === parametros.value?.tipoAsunto.catTipoAsuntoId,
    )
      ? Validaciones.validaSelectRequerido(val?.id)
      : true,
]);
const reglasOCC = ref([
  //(val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaNoOCC(val),
]);

function ajustarLongitud(numero, longitudDeseada) {
  if (numero.length > longitudDeseada) {
    numero = numero.replace(/^0+/, "");
  }
  return numero.length < longitudDeseada
    ? numero.padStart(longitudDeseada, "0")
    : numero;
}

function entradaOCC() {
  const partes = parametros.value.numeroOCC.split("/");
  const longitudDeseada = 13;
  if (partes.length > 1) {
    partes[0] = ajustarLongitud(partes[0], longitudDeseada);
    parametros.value.numeroOCC = partes.join("/");
  }
}

const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);
const reglasHora = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaHora(val),
]);
const props = defineProps({
  modelValue: {
    type: Object,
    default: new FormPromocion(),
  },
  promocion: {
    type: Object,
  },
});
const esEdicion = ref(false);

const emit = defineEmits({
  "update:modelValue": (value) => value !== null,
  "params:cambio": (val) => val !== null,
  expedienteNuevo: (val) => val !== null,
  cambioExpediente: (val) => val !== null,
});

// eslint-disable-next-line no-unused-vars
const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});

function cambiaronParametros() {
  emit("params:cambio", parametros);
  numeroCopias.value = parametros.value.copias;
  numeroFojas.value = parametros.value.fojas;
  tipoAsuntoOrigen.value = parametros.value.tipoAsunto?.tipoAsunto;
}

function onlyNumber(e) {
  if (
    e.keyCode === 69 ||
    e.keyCode === 187 ||
    e.keyCode === 189 ||
    e.keyCode === 190
  ) {
    e.preventDefault();
  }
}
async function validaNoExpedienteApi(val) {
  emit("cambioExpediente", true);
  esExpedienteYaUtilizado.value = false;
  if (val.length <= 5) return "";
  if (
    props.promocion &&
    props.promocion.expediente.asuntoAlias == val &&
    +props.promocion.expediente.catTipoAsuntoId ==
      +parametros.value.tipoAsunto.catTipoAsuntoId &&
    `${parametros.value.tipoProcedimiento?.descripcion || ""}` ==
      `${props.promocion.expediente.tipoProcedimiento}`
  ) {
    if (esEdicion.value) {
      emit("cambioExpediente", false);
    }
    return true;
  }
  try {
    await oficialiaStore.buscarExpediente(
      val,
      parametros.value?.tipoAsunto?.catTipoAsuntoId,
      labelTipoProcedimiento.value.includes("*")
        ? parametros.value?.tipoProcedimiento?.id
        : null,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  if (oficialiaStore.expediente?.length > 0) {
    esExpedienteYaUtilizado.value = true;
    return "";
  } else {
    esExpedienteYaUtilizado.value = false;
    return true;
  }
}
function setParametros(val) {
  if (parametros.value.expedienteEncontrado) {
    parametros.value = val;
    parametros.value.copias =
      numeroCopias.value == undefined ? 0 : numeroCopias.value;
    parametros.value.fojas =
      numeroFojas.value == undefined ? 0 : numeroFojas.value;

    // parametros.value.copias = numeroCopias.value;
    // parametros.value.fojas = numeroFojas.value;
  } else {
    parametros.value = val;
  }
}
const stopWatch = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.modelValue,
  async (_newValue) => {
    setParametros(_newValue);
  },
  {
    immediate: true,
  },
);
function isEmpty(obj) {
  for (var prop in obj) {
    if (obj.hasOwnProperty(prop)) return false;
  }
  return true;
}

const borrarSiEsCero = (campo) => {
  if (parametros.value[campo] === 0) {
    parametros.value[campo] = null;
  }
};

onMounted(async () => {
  promoventeStore.$subscribe(() => {
   actualizarPromoventes();
  });
  if (props.promocion) {
    esEdicion.value = true;
    if (
      props.promocion.expediente.asuntoNeunId &&
      !props.promocion.secretarioId
    ) {
      await secretarioSugerido(props.promocion.expediente.asuntoNeunId);
    }
    watch(
      () => oficialiaStore.promocion,
      async () => {
        if (!isEmpty(oficialiaStore.promocion)) {
          detalle.value = oficialiaStore.promocion;
          tipoAsuntoReadOnly.value = detalle.value.catTipoAsunto;
          cuadernoReadOnly.value = detalle.value.cuaderno;
          contenidoReadOnly.value = detalle.value.contenido;
          parametros.value.numeroExpediente = detalle.value.expediente;
          parametros.value.registro = detalle.value.numeroRegistro;
          parametros.value.copias = detalle.value.numeroCopias;
          numeroCopias.value = parametros.value.copias;
          parametros.value.fojas = detalle.value.fojas;
          numeroFojas.value = detalle.value.fojas;
          tipoAsuntoOrigen.value = detalle.value.catTipoAsunto;
          detalle.value.occ == "00000000000"
            ? (detalle.value.occ = "")
            : (detalle.value.occ = detalle.value.occ);
          parametros.value.numeroOCC = detalle.value.occ;
          parametros.value.asuntoNeunId = detalle.value.asuntoNeunId;

          const expeditenTipoAsuntoId = parseInt(detalle.value.catTipoAsuntoId);

          if (expeditenTipoAsuntoId > 0) {
            expedienteNuevo.value = false;
            await getCatalogosPorTipoAsunto(expeditenTipoAsuntoId);
            if (expeditenTipoAsuntoId === 11) {
              await getCuadernos(parametros.value?.amparoEnRevision?.id);
            } else {
              await getCuadernos(
                expeditenTipoAsuntoId,
                parametros.value.asuntoNeunId,
              );
            }

            parametros.value.tipoProcedimiento =
              tipoProcedimientoOptions.value?.find(
                (t) => t.id === detalle.value.tipoProcedimientoId,
              );

            parametros.value.amparoEnRevision =
              amparoEnRevisionOptions.value?.find(
                (t) => t.id === detalle.value.amparoEnRevision,
              );
          }
          resetLabelTipoProcedimiento(expeditenTipoAsuntoId);
          resetLabelAmparoEnRevision(expeditenTipoAsuntoId);

          parametros.value.cuaderno = cuadernoOptions.value?.find(
            (c) => c.cuadernoId === detalle.value.cuadernoId,
          );

          parametros.value.contenido = contenidoOptions.value?.find(
            (t) => t.id === detalle.value.contenidoId,
          );
          contenidoOriginal.value = parametros.value.contenido;

          try {
            await usuariosStore.obtenerSecretarios();
          } catch (error) {
            manejoErrores.mostrarError(error);
          }

          if (detalle.value.secretarioId) {
            parametros.value.secretario = secretarioOptions.value?.find(
              (t) => t.empleadoId == detalle.value.secretarioId,
            );
          }

          parametros.value.fechaPresentacion = date.formatDate(
            new Date(detalle.value.fechaPresentacion),
            "DD/MM/YYYY",
          );
          parametros.value.horaPresentacion = detalle.value.horaPresentacion;
        }
      },
    );
  } else {
    parametros.value.fechaPresentacion = date.formatDate(
      Date.now(),
      "DD/MM/YYYY",
    );
    parametros.value.horaPresentacion = date.formatDate(Date.now(), "HH:mm");

    if (!parametros.value.registro) {
      try {
        await oficialiaStore.calculaRegistro();
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
      parametros.value.registro = "" + oficialiaStore.noRegistro;
    }
    parametros.value.secretario = usuariosStore.secretarios[0];
    try {
      await usuariosStore.obtenerSecretarios();
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    cuadernoOptions.value = [];
    catalogosStore.resetCuaderno();
  }
});

async function actualizarPromoventes(){
  try {
      await usuariosStore.obtenerParteExistente(parametros.value.asuntoNeunId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    try {
      await usuariosStore.obtenerPromoventeExistente(
        parametros.value.asuntoNeunId
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  try {
    await usuariosStore.obtenAutoridadXExpediente(
      parametros.value?.asuntoNeunId || 0,
      parametros.value?.tipoAsunto?.tipoAsunto || " "
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

onUpdated(() => {});

onBeforeUnmount(() => {
  usuariosStore.parteExistente = [];
  usuariosStore.promoventeExistente = [];
  usuariosStore.autoridadJudicial = [];
  stopWatch();
});
/**
 * Refresca catalogos dependientes
 * @param {} val
 */
async function refrescaCatalogosDependientes(val) {
  parametros.value.tipoProcedimiento = null;
  tipoProcedimientoOptions.value = [];
  amparoEnRevisionOptions.value = [];
  parametros.value.cuaderno = null;
  cuadernoOptions.value = [];
  parametros.value.contenido = null;
  contenidoOptions.value = [];
  parametros.value.parteCatTipoPersonaCaracter = null;
  parametros.value.numeroExpediente = null;
  parametros.value.amparoEnRevision = null;

  resetLabelTipoProcedimiento(parametros.value.tipoAsunto?.catTipoAsuntoId);
  resetLabelAmparoEnRevision(parametros.value.tipoAsunto?.catTipoAsuntoId);

  if (val && val.catTipoAsuntoId) {
    await getCatalogosPorTipoAsunto(val.catTipoAsuntoId);
    if (parametros.value?.amparoEnRevision?.id) {
      await getCuadernos(parametros.value?.amparoEnRevision?.id);
    } else {
      await getCuadernos(val.catTipoAsuntoId);
    }
    await numExpediente.value?.validate(parametros.value.numeroExpediente);
    try {
      await catalogosStore.obtenerTipoPersonaCaracter(val.catTipoAsuntoId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }

  cambiaronParametros();
}
async function cambioTipoProcedimento(val) {
  parametros.value.numeroExpediente = null;
  if (val && val.id) {
    await calculaNumeroExpediente(
      parametros.value.tipoAsunto?.catTipoAsuntoId,
      val.id,
    );
  } else {
    await calculaNumeroExpediente(parametros.value.tipoAsunto.catTipoAsuntoId);
  }
  await numExpediente.value.validate(parametros.value.numeroExpediente);
  cambiaronParametros();
}

async function cambioAmparoEnRevision() {
  await getCuadernos(parametros.value.amparoEnRevision?.id);
  cambiaronParametros();
}

async function calculaNumeroExpediente(
  catTipoAsuntoId,
  tipoProcedimiento = null,
) {
  if (!catTipoAsuntoId) return;
  try {
    parametros.value.numeroExpediente =
      await oficialiaStore.calculaNumeroExpediente(
        catTipoAsuntoId,
        tipoProcedimiento,
      );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

async function secretarioSugerido(val) {
  try {
    const secretarioSugerido =
      await usuariosStore.obtenerSecretarioSugerido(val);
    if (secretarioSugerido && secretarioSugerido.secretario) {
      parametros.value.secretario = secretarioOptions.value?.find(
        (t) => t.empleadoId == secretarioSugerido.secretario,
      );
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}
/**
 * Refresca catalogos dependientes
 * @param {} val
 */
async function expedienteEncontrado(val) {
  if (esEdicion.value) {
    emit("cambioExpediente", true);
    editaExpediente.value = false;
  }
  parametros.value.tipoProcedimiento = null;
  tipoProcedimientoOptions.value = [];
  amparoEnRevisionOptions.value = [];
  if (
    parametros.value.tipoAsunto?.catTipoAsuntoId !==
      parseInt(parametros.value.expedienteEncontrado.catTipoAsuntoId) ||
    !esEdicion.value
  ) {
    parametros.value.contenido = null;
    parametros.value.cuaderno = null;
    cuadernoOptions.value = [];
  }
  contenidoOptions.value = [];
  const expeditenTipoAsuntoId = val?.catTipoAsuntoId || 0;
  resetLabelTipoProcedimiento(expeditenTipoAsuntoId);
  resetLabelAmparoEnRevision(expeditenTipoAsuntoId);
  if (val && val.catTipoAsuntoId) {
    expedienteNuevo.value = false;
    emit("expedienteNuevo", expedienteNuevo.value);
    await getCatalogosPorTipoAsunto(expeditenTipoAsuntoId);
    if (
      tipoAsuntoOrigen.value !==
      parametros.value.expedienteEncontrado?.tipoAsunto
    ) {
      if (expeditenTipoAsuntoId === 11) {
        await getCuadernos(parametros.value?.amparoEnRevision?.id);
      } else {
        await getCuadernos(expeditenTipoAsuntoId, val.asuntoNeunId);
      }
    } else {
      parametros.value.cuaderno = cuadernoOptions.value?.find(
        (c) => c.cuadernoId === detalle.value.cuadernoId,
      );
      buscandoCuadernos.value = false;
    }
    await secretarioSugerido(val.asuntoNeunId);
  }
  if (labelTipoProcedimiento.value.includes("*")) {
    parametros.value.tipoProcedimiento = tipoProcedimientoOptions.value?.find(
      (t) => t.id === val.catTipoProcedimiento,
    );
  }
  //Actualizar valores en los combos
  if (!expedienteNuevo.value) {
    const tipoAsunto = tipoAsuntosOpciones.value.find(
      (s) => s.catTipoAsuntoId === val.catTipoAsuntoId,
    );
    parametros.value.tipoAsunto = tipoAsunto;
    tipoAsuntoOrigen.value = tipoAsunto?.tipoAsunto;
    parametros.value.numeroOCC = val.numeroOCC;
    parametros.value.asuntoNeunId = val.asuntoNeunId;
    parametros.value.numeroExpediente = val.asuntoAlias;
    try {
      await usuariosStore.obtenerParteExistente(parametros.value.asuntoNeunId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    try {
      await usuariosStore.obtenerPromoventeExistente(
        parametros.value.asuntoNeunId,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
  try {
    await usuariosStore.obtenAutoridadXExpediente(
      val?.asuntoNeunId || 0,
      val?.asuntoAlias || " ",
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  cambiaronParametros();
}

async function getCatalogosPorTipoAsunto(catTipoAsuntoId) {
  buscandoCuadernos.value = true;
  buscandoContenido.value = true;
  try {
    await catalogosStore.obtenerProcedimientos(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoProcedimientoOptions.value = catalogosStore.procedimientos;

  if (labelAmparoEnRevision.value.includes("*") || catTipoAsuntoId === 11) {
    buscandoAmparoEnRevision.value = true;
    try {
      await catalogosStore.obtenerAmparoEnRevision(247);
      amparoEnRevisionOptions.value = catalogosStore.amparoEnRevision;
      if (amparoEnRevisionOptions.value.length > 0) {
        parametros.value.amparoEnRevision = catalogosStore.amparoEnRevision[0];
      }
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    buscandoAmparoEnRevision.value = false;
  }

  try {
    await catalogosStore.obtenerContenidos(catTipoAsuntoId);
    contenidoOptions.value = catalogosStore.contenidos;
    if (
      catalogosStore.contenidos.length > 0 &&
      parametros.value.contenido === null
    ) {
      if (
        tipoAsuntoReadOnly.value ===
        parametros.value.expedienteEncontrado?.tipoAsunto
      ) {
        parametros.value.contenido = contenidoOptions.value?.find(
          (c) => c.descripcion === contenidoReadOnly.value,
        );
      }
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  buscandoContenido.value = false;
  try {
    await catalogosStore.obtenerTiposAnexo(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogosStore.obtenerDescripcionesAnexo(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogosStore.obtenerCaracteresAnexo(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogosStore.obtenerTipoPersonaCaracter(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (expedienteNuevo.value) await calculaNumeroExpediente(catTipoAsuntoId);
}

async function getCuadernos(catTipoAsuntoId, asuntoNeunId) {
  try {
    await catalogosStore.obtenerCuadernos(catTipoAsuntoId, asuntoNeunId);
    cuadernoOptions.value = catalogosStore.cuadernos;
    if (
      catalogosStore.cuadernos.length > 0 &&
      parametros.value.cuaderno === null
    ) {
      if (
        esEdicion.value &&
        tipoAsuntoReadOnly.value ===
          parametros.value.expedienteEncontrado?.tipoAsunto
      ) {
        parametros.value.cuaderno = cuadernoOptions.value?.find(
          (c) => c.cuaderno === cuadernoReadOnly.value,
        );
      } else {
        parametros.value.cuaderno = catalogosStore.cuadernos[0];
      }
    }
    if (parametros.value.tipoAsunto?.catTipoAsuntoId === 11) {
      parametros.value.cuaderno = catalogosStore.cuadernos[0];
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  buscandoCuadernos.value = false;
}
/**
 * filtra tipo asunto en combo
 * @param {*} val valor a buscar
 */
function filtrarTipoAsunto(val, update) {
  update(
    async () => {
      tipoAsuntosOpciones.value = Utils.filtrarCombo(
        val,
        catalogosStore.asuntos,
        "tipoAsunto",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
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
/**
 * filtra contenido en combo
 * @param {*} val valor a buscar
 */
function filtrarContenido(val, update) {
  update(
    async () => {
      contenidoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.contenidos,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
/**
 * filtra tipo procedimiento en combo
 * @param {*} val valor a buscar
 */
function filtrarTipoProcedimiento(val, update) {
  update(
    async () => {
      tipoProcedimientoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.procedimientos,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
/**
 * filtra amparo en revisión en combo
 * @param {*} val valor a buscar
 */
function filtrarAmparoEnRevision(val, update) {
  update(
    async () => {
      amparoEnRevisionOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.amparoEnRevision,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
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
function setExpedienteNuevo() {
  expedienteNuevo.value = true;
  editaExpediente.value = false;
  if (!esEdicion.value) {
    emit("expedienteNuevo", expedienteNuevo.value);
    parametros.value.expedienteEncontrado = null;
  } else {
    editaExpediente.value = true;
    if (parametros.value.expedienteEncontrado != null) {
      parametros.value.expedienteEncontrado = null;
      parametros.value.tipoAsunto = null;
      parametros.value.tipoProcedimiento = null;
      parametros.value.numeroExpediente = null;
      parametros.value.numeroOCC = null;
      refrescaCatalogosDependientes(null);
    }
  }
}

async function numeroPromocionExistente(val) {
  const parametrosRequest = {
    numeroPromocion: val,
    anioPromocion:
      esEdicion.value && props.promocion.yearPromocion
        ? props.promocion.yearPromocion
        : parametros.value.fechaPresentacion?.split("/")[2] ||
          new Date().getFullYear(),
  };
  let promocionExistente = 0;
  if (val === "") {
    try {
      promocionExistente =
        await oficialiaStore.revisarNumeroPromocion(parametrosRequest);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }

  if (promocionExistente === 1 && props.promocion?.numeroRegistro !== val) {
    //Bypass temporal para permitir número de registro repetido
    //Se realiza de esta manera ya que se espera que existan exepciones a esté cambio que están por definirse
    //esPromocionYaExiste.value = true;
    //return false;
    esPromocionYaExiste.value = false;
    return true;
  } else {
    esPromocionYaExiste.value = false;
    return true;
  }
}

function resetLabelTipoProcedimiento(expeditenTipoAsuntoId) {
  labelTipoProcedimiento.value = `Tipo de Procedimiento${
    expeditenTipoAsuntoId &&
    [6, 9, 18, 74, 72, 67, 125, 126, 137].some(
      (x) => x === expeditenTipoAsuntoId,
    )
      ? " *"
      : ""
  }`;
}

function resetLabelAmparoEnRevision(expeditenTipoAsuntoId) {
  labelAmparoEnRevision.value = `Clasificación Amparo en Revisión${
    expeditenTipoAsuntoId && [11].some((x) => x === expeditenTipoAsuntoId)
      ? " *"
      : ""
  }`;
}

function funcionInput() {
  if (parametros.value.expedienteEncontrado) {
    parametros.value.expedienteEncontrado = null;
    cambiaronParametros();
  }
}
</script>
<script>
export default {
  inheritAttrs: false,
};
</script>
<style scoped lang="css"></style>
