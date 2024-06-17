<template>
  <div class="row q-gutter-sm q-pr-xs">
    <div class="col q-pr-sm">
      <q-select
        clearable
        v-cortarLabel
        label="Buscar un expediente *"
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
    <!-- <div class="col">
      <q-btn
        @click="setExpedienteNuevo"
        icon="mdi-folder-plus"
        no-caps
        flat
        color="primary"
        :label="'Crear un expediente'"
      ></q-btn>
    </div> -->
  </div>
  <q-item-label
    v-if="expedienteNuevo || esEdicion"
    class="text-bold q-my-md text-subtitle1"
    >Datos del expediente</q-item-label
  >
  <div v-if="expedienteNuevo || esEdicion" class="row q-gutter-lg">
    <q-select
      v-cortarLabel
      @input-value="parametros.tipoAsunto = null"
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
      label="Oficina de Correspondencia Común *"
      :rules="reglasOCC"
      :disable="esEdicion && !editaExpediente"
    >
      <template v-slot:hint>
        <q-item-label
          ><q-icon size="1.2em" color="light-blue" name="info" /> Formato
          0000000000000/0000</q-item-label
        >
      </template>
    </q-input>
  </div>

  <q-item-label class="text-bold text-subtitle1 q-mb-md q-mt-lg"
    >Datos del oficio</q-item-label
  >
  <div class="row q-gutter-lg q-mb-md">
    <q-select
      v-cortarLabel
      @input-value="parametros.cuaderno = null"
      dense
      filled
      class="col"
      use-input
      input-debounce="0"
      v-model="parametros.cuaderno"
      label="Vincular acuerdo"
      option-label="cuaderno"
      option-value="cuadernoId"
      @filter="filtrarCuaderno"
      :options="cuadernoOptions"
      @update:model-value="cambiaronParametros"
    ></q-select>
    <div class="col"></div>
    <!-- </div>

  <div class="row q-gutter-sm"> -->
    <!-- <div class="row col-6">
      <q-input
        dense
        filled
        class="col-8 q-pr-lg"
        type="number"
        @keydown="onlyNumber($event)"
        v-model.number="parametros.registro"
        label="Número de promoción"
        @update:model-value="cambiaronParametros"
        maxlength="6"
        min="1"
        :rules="reglasNoPromocion"
        :error="esPromocionYaExiste"
      >
        <template v-slot:error>
          <span class="red"><q-icon name="info" /></span> Número ya existente
        </template>
      </q-input> -->

    <!-- <q-input
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
      ></q-input>
    </div> -->

    <!-- <q-input
      dense
      filled
      class="col-2"
      v-model.number="parametros.fojas"
      label="Fojas"
      min="0"
      type="number"
      maxlength="2"
      :rules="[
        (val) => Validaciones.validaValorMin(val, 0),
        (val) => Validaciones.validaValorMax(val, 99),
      ]"
      @update:model-value="cambiaronParametros"
    ></q-input> -->
    <!-- <q-select
      v-cortarLabel
      @input-value="parametros.contenido = null"
      dense
      filled
      class="col q-pl-md"
      v-model="parametros.contenido"
      label="Contenido *"
      use-input
      option-label="descripcion"
      @filter="filtrarContenido"
      option-value="id"
      :options="contenidoOptions"
      @update:model-value="cambiaronParametros"
      :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
    ></q-select>-->
  </div>
  <!-- <q-item-label class="text-bold q-my-md text-subtitle1"
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
              mask="HH:mm:ss"
              with-seconds
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
  </div> -->

  <q-item-label class="text-subtitle1 text-bold pad-left q-pb-md">
    Parte</q-item-label
  >
  <q-item-label class="pad-left q-pb-lg"
    >Indica la forma como serán notificadas y generará automáticamente el oficio
    de las autoridades. Oficio libre da oportunidad de tener un contenido
    diferente.</q-item-label
  >

  <q-tabs
    v-model="tab"
    keep-alive
    dense
    class="text-grey"
    active-color="primary"
    indicator-color="primary"
    align="justify"
    narrow-indicator
  >
    <div class="row q-ml-xs">
      <q-tab name="autoridades" label="Autoridades" no-caps />
      <q-tab name="partes" label="Partes" no-caps />
      <q-tab name="otros" label="Otros" no-caps />
    </div>
    <q-space></q-space>
    <q-input
      v-if="tab === 'autoridades'"
      underline
      label="Buscar autoridades"
      dense
      debounce="300"
      color="primary"
      v-model="filter"
    >
      <template v-slot:append>
        <q-icon name="search" />
      </template>
    </q-input>
    <q-input
      v-else
      underline
      label="Buscar partes"
      dense
      debounce="300"
      color="primary"
      v-model="filterPartes"
    >
      <template v-slot:append>
        <q-icon name="search" />
      </template>
    </q-input>
  </q-tabs>
  <q-tab-panels v-model="tab" animated class="q-mt-md">
    <q-tab-panel name="autoridades" class="q-pa-none">
      <q-table
        style="max-height: 20em"
        virtual-scroll
        :pagination="{ rowsPerPage: 0 }"
        flat
        :filter="filter"
        :rows="rows"
        :columns="columns"
        row-key="index"
        hide-pagination
      >
        <template v-slot:body="props">
          <q-tr>
            <q-td>
              <q-item class="text-left">
                <q-item-section>
                  <q-item-label>
                    {{ props.row.nombres.split("-")[0] || "" }}
                  </q-item-label>
                  <q-item-label class="text-secondary" caption>
                    {{ props.row.autoridadJudicialDescripcion }}
                  </q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td class="text-center justify-center">
              <q-item class="text-center justify-center">
                <q-radio
                  class="justify-between items-center"
                  name="noty"
                  v-model="props.row.noty"
                  val="21"
                />
              </q-item>
            </q-td>
            <q-td class="text-center justify-center">
              <q-item class="text-center justify-center">
                <q-radio
                  @click="
                    emit('showOficio', {
                      ...props.row,
                      archivo: file?.blob,
                    })
                  "
                  name="noty"
                  v-model="props.row.noty"
                  val="6"
                  class="text-center justify-center"
                />
              </q-item>
            </q-td>
            <q-td>
              <q-item-section v-show="props.row.noty === '6'">
                <q-item>
                  <q-icon
                    class="q-pr-xs cursor-pointer"
                    size="1.7em"
                    color="secondary"
                    name="mdi-file-document-edit-outline"
                    @click="
                      emit('showOficio', {
                        ...props.row,
                        archivo: file?.blob,
                      })
                    "
                  >
                    <q-tooltip>Editar</q-tooltip>
                  </q-icon>
                  <q-icon
                    class="cursor-pointer"
                    size="1.7em"
                    color="red-6"
                    name="mdi-delete"
                  >
                    <q-tooltip>Borrar oficio libre</q-tooltip>
                  </q-icon>
                </q-item>
              </q-item-section>
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </q-tab-panel>

    <q-tab-panel name="partes" class="q-pa-none">
      <div class="q-pa-xs">
        <q-table
          flat
          style="max-height: 20em"
          virtual-scroll
          :pagination="{ rowsPerPage: 0 }"
          :filter="filterPartes"
          :rows="rowsPartes"
          :columns="columns"
          row-key="autoridadJudicialId"
          hide-pagination
        >
          <template v-slot:body="props">
            <q-tr>
              <q-td>
                <q-item class="text-left">
                  <q-item-section>
                    <q-item-label>
                      {{
                        props.row.nombre +
                          " " +
                          props.row.aPaterno +
                          " " +
                          props.row.aMaterno || ""
                      }}
                    </q-item-label>
                    <q-item-label class="text-secondary" caption>
                      {{ props.row.descripcionCaracterPersona }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item class="text-center justify-center">
                  <q-radio
                    class="text-center justify-center"
                    name="noty"
                    v-model="props.row.noty"
                    val="6"
                  />
                </q-item>
              </q-td>
              <!-- <q-td>
                <q-item class="text-center justify-center">
                  <q-radio
                    class="text-center justify-center"
                    name="noty"
                    v-model="props.row.noty"
                    val="1"
                  />
                </q-item>
              </q-td> -->
              <!-- <q-td>
                      <q-item class="text-center justify-center">
                        <q-radio
                          class="text-center justify-center"
                          name="noty"
                          v-model="props.row.noty"
                          val="3"
                        />
                      </q-item>
                    </q-td> -->
              <q-td class="text-center justify-center">
                <q-item class="text-center justify-center">
                  <q-radio
                    @click="
                      emit('showOficio', {
                        ...props.row,
                        archivo: file?.blob,
                      })
                    "
                    name="noty"
                    v-model="props.row.noty"
                    val="12"
                    class="text-center justify-center"
                  />
                </q-item>
              </q-td>
              <q-td>
                <q-item-section v-show="props.row.noty === '12'">
                  <q-item>
                    <q-icon
                      class="q-pr-xs cursor-pointer"
                      size="1.7em"
                      color="secondary"
                      name="mdi-file-document-edit-outline"
                      @click="
                        emit('showOficio', {
                          ...props.row,
                          archivo: file?.blob,
                        })
                      "
                    >
                      <q-tooltip>Editar</q-tooltip>
                    </q-icon>
                    <q-icon
                      class="cursor-pointer"
                      size="1.7em"
                      color="red-6"
                      name="mdi-delete"
                    >
                      <q-tooltip>Borrar oficio libre</q-tooltip>
                    </q-icon>
                  </q-item>
                </q-item-section>
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </div>
    </q-tab-panel>

    <q-tab-panel name="otros" class="q-pa-none">
      <q-table
        style="max-height: 20em"
        virtual-scroll
        :pagination="{ rowsPerPage: 0 }"
        flat
        :filter="filter"
        :rows="rowsOtros"
        :columns="columns"
        row-key="index"
        hide-pagination
      >
        <template #top>
          <q-input
            label="Agregar parte"
            class="q-pl-xl col-7"
            dense
            debounce="300"
            color="primary"
            v-model="filterOtro"
            @keydown.enter.prevent="AgregarOtro($event)"
          >
            <template v-slot:append>
              <q-icon name="add" />
            </template>
          </q-input>
        </template>
        <template v-slot:body="props">
          <q-tr>
            <q-td>
              <q-item class="text-left">
                <q-item-section>
                  <q-item-label>
                    {{ props.row.nombres.split("-")[0] || "" }}
                  </q-item-label>
                  <q-item-label class="text-secondary" caption>
                    Otro
                  </q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td class="text-center justify-center">
              <q-item class="text-center justify-center">
                <q-radio
                  class="justify-between items-center"
                  name="noty"
                  v-model="props.row.noty"
                  val="21"
                />
              </q-item>
            </q-td>
            <q-td class="text-center justify-center">
              <q-item class="text-center justify-center">
                <q-radio
                  @click="
                    emit('showOficio', {
                      ...props.row,
                      archivo: file?.blob,
                    })
                  "
                  name="noty"
                  v-model="props.row.noty"
                  val="6"
                  class="text-center justify-center"
                />
              </q-item>
            </q-td>
            <q-td>
              <q-item-section v-show="props.row.noty === '6'">
                <q-item>
                  <q-icon
                    class="q-pr-xs cursor-pointer"
                    size="1.7em"
                    color="secondary"
                    name="mdi-file-document-edit-outline"
                    @click="
                      emit('showOficio', {
                        ...props.row,
                        archivo: file?.blob,
                      })
                    "
                  >
                    <q-tooltip>Editar</q-tooltip>
                  </q-icon>
                  <q-icon
                    class="cursor-pointer"
                    size="1.7em"
                    color="red-6"
                    name="mdi-delete"
                  >
                    <q-tooltip>Borrar oficio libre</q-tooltip>
                  </q-icon>
                </q-item>
              </q-item-section>
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </q-tab-panel>
  </q-tab-panels>
</template>

<script setup lang="ts">
import {
  computed,
  watch,
  onMounted,
  onUpdated,
  onBeforeUnmount,
  ref,
} from "vue";
import { Utils } from "src/helpers/utils";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { useLibretaStore } from "../stores/libreta-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { Validaciones } from "../../../helpers/validaciones";

import { FormPromocion } from "../data/form-promocion";
import { Promocion } from "../data/promocion";
import { DetallePromocion } from "../data/detalle-promocion";
import { manejoErrores } from "src/helpers/manejo-errores";

const numExpediente = ref(null);
const selectTipoAsunto = ref(null);
const catalogosStore = useCatalogosStore();
const libretaStore = useLibretaStore();
const usuariosStore = useUsuariosStore();
const labelTipoProcedimiento = ref(`Tipo de Procedimiento`);
const tipoAsuntosOpciones = ref(catalogosStore.asuntos);
const cuadernoOptions = ref([]);
const contenidoOptions = ref([]);
const tipoProcedimientoOptions = ref([]);
const parametros = ref(new FormPromocion());
const buscarExpediente = ref("");
const expedienteNuevo = ref(false);
const editaExpediente = ref(false);
const secretarioOptions = ref(usuariosStore.secretarios);
const detalle = ref(new DetallePromocion());
//const loadingExpediente = ref(false);
const opcionesExpediente = ref([]);
const reglasNoExpediente = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaNoExpediente(val),
  (val) => {
    return validaNoExpedienteApi(val);
  },
]);
// const reglasNoPromocion = ref([
//   (val) => Validaciones.validaValorMin(val, 1),
//   (val) => Validaciones.validaValorMax(val, 200000),
//   (val) => {
//     return numeroPromocionExistente(val);
//   },
// ]);

const esExpedienteYaUtilizado = ref(false);

// const esPromocionYaExiste = ref(false);

const reglasTipoProcedimiento = ref([
  (val) =>
    parametros.value?.tipoAsunto &&
    [6, 9, 18, 74, 72, 67, 125, 126].some(
      (x) => x === parametros.value?.tipoAsunto.catTipoAsuntoId,
    )
      ? Validaciones.validaSelectRequerido(val?.id)
      : true,
]);
const reglasOCC = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaNoOCC(val),
]);

function ajustarLongitud(numero, longitudDeseada) {
  if (numero.length > longitudDeseada) {
    numero = numero.replace(/^0+/, ""); // Eliminar ceros al inicio
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

// const reglasFecha = ref([
//   (val) => Validaciones.validaInputRequerido(val),
//   (val) => Validaciones.validaFecha(val),
// ]);
// const reglasHora = ref([
//   (val) => Validaciones.validaInputRequerido(val),
//   (val) => Validaciones.validaHora(val),
// ]);
const props = defineProps({
  // v-model
  modelValue: {
    type: FormPromocion,
    default: new FormPromocion(),
  },
  promocion: {
    type: Promocion,
  },
});
const esEdicion = ref(false);
const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  "params:cambio": (val) => val !== null,
  expedienteNuevo: (val) => val !== null,
  cambioExpediente: (val) => val !== null,
  showOficio: (val) => val !== null,
  agregarOtro: (val) => val != null,
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
}

// function onlyNumber(e) {
//   if (
//     e.keyCode === 69 ||
//     e.keyCode === 187 ||
//     e.keyCode === 189 ||
//     e.keyCode === 190
//   ) {
//     e.preventDefault();
//   }
// }
async function validaNoExpedienteApi(val) {
  if (val.length <= 5) return "";
  if (props.promocion && props.promocion.expediente.asuntoAlias == val) {
    return true;
  }
  try {
    await libretaStore.buscarExpediente(
      val,
      parametros.value?.tipoAsunto?.catTipoAsuntoId,
      labelTipoProcedimiento.value.includes("*")
        ? parametros.value?.tipoProcedimiento?.id
        : null,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  if (libretaStore.expediente.length > 0) {
    esExpedienteYaUtilizado.value = true;
    return "";
  } else {
    esExpedienteYaUtilizado.value = false;
    return true;
  }
}
function setParametros(val) {
  parametros.value = val;
}
const stopWatch = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.modelValue,
  async (_newValue) => {
    // do something
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
onMounted(async () => {
  if (props.promocion) {
    esEdicion.value = true;
    watch(
      () => libretaStore.cuadernos,
      async () => {
        if (!isEmpty(libretaStore.cuadernos)) {
          //detalle.value = libretaStore.cuadernos;

          parametros.value.numeroExpediente = detalle.value.expediente;
          parametros.value.registro = detalle.value.numeroRegistro;
          parametros.value.copias = detalle.value.numeroCopias;
          parametros.value.fojas = detalle.value.fojas;
          parametros.value.numeroOCC = detalle.value.occ;
          parametros.value.asuntoNeunId = detalle.value.asuntoNeunId;

          const expeditenTipoAsuntoId = parseInt(detalle.value.catTipoAsuntoId);

          if (expeditenTipoAsuntoId > 0) {
            expedienteNuevo.value = false;
            //  emit("expedienteNuevo", expedienteNuevo.value);
            await getCatalogosPorTipoAsunto(expeditenTipoAsuntoId);

            parametros.value.tipoProcedimiento =
              tipoProcedimientoOptions.value?.find(
                (t) => t.id === detalle.value.tipoProcedimientoId,
              );
          }

          const tipoAsunto = tipoAsuntosOpciones.value.find(
            (s) => s.catTipoAsuntoId == expeditenTipoAsuntoId,
          );
          parametros.value.tipoAsunto = tipoAsunto;

          //myForm.value.resetValidation();
          resetLabelTipoProcedimiento(expeditenTipoAsuntoId);

          //parametros.value.registro = detalle.value.numeroRegistro;
          parametros.value.cuaderno = cuadernoOptions.value?.find(
            (c) => c.cuadernoId === detalle.value.cuadernoId,
          );

          parametros.value.cuaderno = cuadernoOptions.value?.find(
            (c) => c.cuadernoId === detalle.value.cuadernoId,
          );

          parametros.value.contenido = contenidoOptions.value?.find(
            (t) => t.id === detalle.value.contenidoId,
          );

          try {
            await usuariosStore.obtenerSecretarios();
          } catch (error) {
            manejoErrores.mostrarError(error);
          }
          parametros.value.secretario = secretarioOptions.value?.find(
            (t) => t.empleadoId == detalle.value.secretarioId,
          );

          // parametros.value.fechaPresentacion = date.formatDate(
          //   new Date(detalle.value.fechaPresentacion),
          //   "DD/MM/YYYY"
          // );
          // parametros.value.horaPresentacion = date.formatDate(Date.now(), "HH:mm:ss");
          parametros.value.horaPresentacion = detalle.value.horaPresentacion;
        }
      },
    );
  } else {
    // parametros.value.fechaPresentacion = date.formatDate(
    //   Date.now(),
    //   "DD/MM/YYYY"
    // );
    // parametros.value.horaPresentacion = date.formatDate(Date.now(), "HH:mm:ss");

    // if (!parametros.value.registro) {
    //   try {
    //     await libretaStore.calculaRegistro();
    //   } catch (error) {
    //     manejoErrores.mostrarError(error);
    //   }
    //   parametros.value.registro = "" + libretaStore.noRegistro;
    // }

    try {
      await usuariosStore.obtenerSecretarios();
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    cuadernoOptions.value = [];
    catalogosStore.resetCuaderno();
  }
});

onUpdated(() => {});

onBeforeUnmount(() => {
  stopWatch();
});
/**
 * Refresca catalogos dependientes
 * @param {} val
 */
async function refrescaCatalogosDependientes(val) {
  parametros.value.tipoProcedimiento = null;
  tipoProcedimientoOptions.value = [];
  parametros.value.cuaderno = null;
  cuadernoOptions.value = [];
  parametros.value.contenido = null;
  contenidoOptions.value = [];

  //myForm.value.resetValidation();
  resetLabelTipoProcedimiento(parametros.value.tipoAsunto?.catTipoAsuntoId);

  if (val && val.catTipoAsuntoId) {
    await getCatalogosPorTipoAsunto(val.catTipoAsuntoId);
    await numExpediente.value.validate(parametros.value.numeroExpediente);
  }
  cambiaronParametros();
}
async function cambioTipoProcedimento(val) {
  if (val && val.id) {
    await numExpediente.value.validate(parametros.value.numeroExpediente);
  }
  cambiaronParametros();
}

/**
 * Refresca catalogos dependientes
 * @param {} val
 */
async function expedienteEncontrado(val) {
  if (esEdicion.value) {
    emit("cambioExpediente", true);
  }
  parametros.value.tipoProcedimiento = null;
  tipoProcedimientoOptions.value = [];
  parametros.value.cuaderno = null;
  cuadernoOptions.value = [];
  if (
    parametros.value.tipoAsunto?.catTipoAsuntoId !==
      parseInt(parametros.value.expedienteEncontrado.catTipoAsuntoId) ||
    !esEdicion.value
  ) {
    parametros.value.contenido = null;
  }
  contenidoOptions.value = [];
  const expeditenTipoAsuntoId = parseInt(val?.catTipoAsuntoId || 0);
  //myForm.value.resetValidation();
  resetLabelTipoProcedimiento(expeditenTipoAsuntoId);

  if (val && val.catTipoAsuntoId) {
    expedienteNuevo.value = false;
    emit("expedienteNuevo", expedienteNuevo.value);
    await getCatalogosPorTipoAsunto(expeditenTipoAsuntoId);
    await obtenCatalogosDependientes(val.asuntoNeunId, val?.asuntoAlias);
  }

  if (!expedienteNuevo.value) {
    //Actualizar valores en los combos
    const tipoAsunto = tipoAsuntosOpciones.value.find(
      (s) => s.catTipoAsuntoId === val.catTipoAsuntoId,
    );
    parametros.value.tipoAsunto = tipoAsunto;
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

    const secretarioExpediente = usuariosStore.secretarios.find(
      (s) => s.empleadoId == val.secretarioId,
    );

    if (!esEdicion.value) {
      parametros.value.secretario = secretarioExpediente;
    }
  }

  cambiaronParametros();
}

async function getCatalogosPorTipoAsunto(catTipoAsuntoId) {
  try {
    await catalogosStore.obtenerProcedimientos(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoProcedimientoOptions.value = catalogosStore.procedimientos;
  try {
    await catalogosStore.obtenerContenidos(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  obtieneAcuerdos();

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
// function filtrarContenido(val, update) {
//   update(
//     async () => {
//       contenidoOptions.value = Utils.filtrarCombo(
//         val,
//         catalogosStore.contenidos,
//         "descripcion"
//       );
//     },
//     (ref) => Utils.marcaPrimeraOpcionCombo(val, ref)
//   );
// }
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
          await libretaStore.buscarExpediente(val, null);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        opcionesExpediente.value = libretaStore.expediente;
        if (opcionesExpediente.value?.length === 1) {
          buscarExpediente.value = opcionesExpediente.value[0];
        }
      }
    },
    // "ref" is the Vue reference to the QSelect
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
// function filtrarSecretarios(val, update) {
//   update(
//     async () => {
//       secretarioOptions.value = Utils.filtrarCombo(
//         val,
//         usuariosStore.secretarios,
//         "completo"
//       );
//     },
//     (ref) => Utils.marcaPrimeraOpcionCombo(val, ref)
//   );
// }
// function setExpedienteNuevo() {
//   expedienteNuevo.value = true;
//   editaExpediente.value = false;
//   if (!esEdicion.value) {
//     emit("expedienteNuevo", expedienteNuevo.value);
//     parametros.value.expedienteEncontrado = null;
//   } else {
//     // emit("cambioExpediente", expedienteNuevo.value = true);
//     editaExpediente.value = true;
//   }
// }

// async function numeroPromocionExistente(val) {
//   const parametrosRequest = {
//     numeroPromocion: val,
//     anioPromocion: esEdicion.value
//       ? props.promocion.yearPromocion
//       : parametros.value.fechaPresentacion?.split("/")[2] ||
//         new Date().getFullYear(),
//   };
//   let promocionExistente = 0;
//   try {
//     promocionExistente = await libretaStore.revisarNumeroPromocion(
//       parametrosRequest
//     );
//   } catch (error) {
//     manejoErrores.mostrarError(error);
//   }

//   if (promocionExistente === 1 && props.promocion?.numeroRegistro !== val) {
//     esPromocionYaExiste.value = true;
//     return true;
//   } else {
//     esPromocionYaExiste.value = false;
//     return true;
//   }
// }

function resetLabelTipoProcedimiento(expeditenTipoAsuntoId) {
  labelTipoProcedimiento.value = `Tipo de Procedimiento${
    expeditenTipoAsuntoId &&
    [6, 9, 18, 74, 72, 67, 125, 126].some(
      (x) => x === parametros.value.tipoAsunto?.catTipoAsuntoId,
    )
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

/* OFICIOS */
import { AutoridadExistente } from "src/data/autoridad-existente";
import { ParteExistente } from "src/data/parte-existente";
const file = ref(null);
const tab = ref("autoridades");
const filter = ref("");
const filterPartes = ref("");
const filterOtro = ref("");
const rows = ref(new Array(new AutoridadExistente()).splice(0, 0));

const columns = ref([
  {
    name: "oficios",
    label: "",
    align: "center",
    field: (row) => `${row.nombres} ${row.autoridadJudicialDescripcion}`,
    sortable: true,
  },
  {
    name: "Oficio",
    label: "Oficio",
    align: "right",
    sortable: false,
  },
  {
    name: "Oficio_libre",
    label: "Oficio libre",
    align: "left",
    sortable: false,
  },
]);

const rowsPartes = ref(new Array(new ParteExistente()).splice(0, 0));

// const columnsPartes = ref([
//   {
//     name: "oficios",
//     label: "",
//     align: "left",
//     field: (row) => `${row.nombre} ${row.descripcionCaracterPersona}`,
//     sortable: true,
//   },
//   {
//     name: "Lista",
//     label: "Lista",
//     align: "center",
//     sortable: false,
//   },
//   {
//     name: "Personal",
//     label: "Personal",
//     align: "center",
//     sortable: false,
//   },
//   {
//     name: "Electrónica",
//     label: "Electrónica",
//     align: "center",
//     sortable: false,
//   },
// ]);

async function obtenCatalogosDependientes(asuntoNeunId, numeroExpediente) {
  try {
    await usuariosStore.obtenAutoridad(asuntoNeunId, numeroExpediente, 2, 2);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rows.value = usuariosStore.autoridadXExpediente;
  rows.value = rows.value?.map((x) => {
    x.noty = "2";
    x.text = "";
    return x;
  });
  parametros.value.autoridades = rows.value;
  try {
    await usuariosStore.obtenerParteExistente(
      asuntoNeunId,
      numeroExpediente,
      2,
      1,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rowsPartes.value = usuariosStore.parteExistente;
  rowsPartes.value = rowsPartes.value?.map((x) => {
    x.noty = "1";
    return x;
  });
  parametros.value.partes = rowsPartes.value;
}
const rowsOtros = ref(new Array(new AutoridadExistente()).splice(0, 0));
async function AgregarOtro(e) {
  rowsOtros.value.push({
    autoridadJudicialId: 1,
    nombres: e.srcElement.value,
    autoridadJudicialDescripcion: "",
    noty: "",
    text: "",
  });
}

function obtieneAcuerdos() {
  cuadernoOptions.value.push({
    clasificacionCuaderno: "Principal",
    clasificacionCuadernoId: 11793,
    color: "#00c853",
    cuaderno: "Principal",
    cuadernoCorto: "ai",
    cuadernoId: 5645,
  });

  cuadernoOptions.value.push({
    clasificacionCuaderno: "Incicental",
    clasificacionCuadernoId: 11793,
    color: "#00c853",
    cuaderno: "Principal",
    cuadernoCorto: "ai",
    cuadernoId: 5645,
  });
}
</script>

<style scoped lang="css"></style>
