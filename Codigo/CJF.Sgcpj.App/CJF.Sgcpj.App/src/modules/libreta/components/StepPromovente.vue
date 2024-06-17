<template>
  <!-- <q-card flat class="no-padding no-margin">
		<q-card-section> -->
  <q-item-label class="text-bold q-my-md text-subtitle1"
    >Información del promovente</q-item-label
  >
  <div class="text-body2">
    Selecciona el tipo de figura que promovió la promoción.
  </div>
  <div class="q-py-md">
    <div class="row q-gutter-sm">
      <q-radio
        class="col-4"
        v-model="parametros.tipoPromovente"
        val="promovente"
        label="Promovente"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="col-4"
        v-model="parametros.tipoPromovente"
        val="parte"
        label="Parte"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="col"
        v-model="parametros.tipoPromovente"
        val="autoridad"
        label="Autoridad Judicial"
        @update:model-value="cambiaronParametros"
      />
    </div>
  </div>
  <div
    v-if="
      parametros.tipoPromovente === 'promovente' ||
      parametros.tipoPromovente === 'parte'
    "
  >
    <template v-if="parametros.tipoPromovente === 'promovente'">
      <template v-if="!expedienteNuevo">
        <q-item-label class=""
          >Selecciona un promovente ya existente o agrega uno
          nuevo.</q-item-label
        >
        <div class="q-py-md">
          <div class="row q-gutter-sm">
            <q-radio
              class="col-4"
              v-model="parametros.esPromoventeExistente"
              :val="true"
              label="Promovente existente"
              @update:model-value="cambiaronParametros"
            />
            <q-radio
              class="col-4"
              v-model="parametros.esPromoventeExistente"
              :val="false"
              label="Nuevo promovente"
              @update:model-value="cambiaronParametros"
            />
          </div>
        </div>
      </template>
      <template v-if="parametros.esPromoventeExistente">
        <div class="row q-gutter-sm">
          <q-select
            v-cortarLabel
            @input-value="parametros.promoventeExistente = null"
            dense
            filled
            use-input
            input-debounce="0"
            option-label="label"
            option-value="promoventeId"
            label="Selecciona un promovente *"
            class="col"
            :options="promoventeExistenteOptions"
            v-model="parametros.promoventeExistente"
            @filter="filtrarPromovente"
            lazy-rules
            :rules="[
              (val) => Validaciones.validaSelectRequerido(val?.promoventeId),
            ]"
            @update:model-value="cambiaronParametros"
          >
            <template v-slot:no-option>
              <q-item>
                <q-item-section class="text-red row">
                  <span v-if="usuariosStore.promoventeExistente.length === 0">
                    <q-icon name="info" /> No existen promoventes
                    registrados</span
                  >
                  <span v-else>
                    <q-icon name="info" /> Promovente no encontrado</span
                  >
                </q-item-section>
              </q-item>
            </template>
          </q-select>
        </div>
      </template>
      <template v-else>
        <div class="row q-gutter-lg">
          <q-select
            v-cortarLabel
            @input-value="parametros.tipoPromoventeCat = null"
            dense
            filled
            use-input
            input-debounce="0"
            option-label="descripcion"
            option-value="id"
            label="Tipo *"
            class="col"
            :options="tipoPromoventeOptions"
            v-model="parametros.tipoPromoventeCat"
            @filter="filtrarTipoPromovente"
            lazy-rules
            :rules="reglasPromovente"
            @update:model-value="cambiaronParametros"
          >
          </q-select>
          <div class="col"></div>
        </div>
        <div class="row q-gutter-lg">
          <q-input
            dense
            filled
            v-model="parametros.promoventeNombre"
            label="Nombre *"
            class="col-4"
            @update:model-value="cambiaronParametros"
            :rules="reglasPromoventeNom"
          />
          <q-input
            dense
            filled
            v-model="parametros.promoventeApellidoPaterno"
            label="Apellido paterno *"
            class="col-4"
            @update:model-value="cambiaronParametros"
            :rules="reglasPromoventeAP"
          />
          <q-input
            dense
            filled
            v-model="parametros.promoventeApellidoMaterno"
            label="Apellido materno"
            class="col"
            @update:model-value="cambiaronParametros"
            :rules="reglasApellidoMaterno"
          />
        </div>
      </template>
      <q-item-label class="text-bold q-my-md text-subtitle1"
        >Agrega la parte asociada</q-item-label
      >
    </template>
    <template v-if="!expedienteNuevo">
      <q-item-label class=""
        >Selecciona una parte ya existente o agrega una nueva.</q-item-label
      >
      <div class="q-py-md">
        <div class="row q-gutter-sm">
          <q-radio
            class="col-4"
            v-model="parametros.tipoParte"
            val="parteExistente"
            label="Parte existente"
            @update:model-value="cambiaronParametros"
          />
          <q-radio
            class="col-4"
            v-model="parametros.tipoParte"
            val="parteNueva"
            label="Parte nueva"
            @update:model-value="cambiaronParametros"
          />
        </div>
      </div>
    </template>
    <div v-if="parametros.tipoParte == 'parteExistente'">
      <div class="q-gutter-lg">
        <q-select
          v-cortarLabel
          @input-value="parametros.promoventeAutoridadExistente = null"
          dense
          filled
          use-input
          input-debounce="0"
          v-model="parametros.promoventeAutoridadExistente"
          option-label="personaTipo"
          option-value="personaId"
          label="Busca una parte existente *"
          lazy-rules
          :options="parteExistenteOptions"
          :rules="[(val) => Validaciones.validaSelectRequerido(val?.personaId)]"
          @filter="filtrarParteExistente"
          @update:model-value="cambiaronParametros"
        >
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-red row">
                <span v-if="usuariosStore.parteExistente.length === 0">
                  <q-icon name="info" /> No existen partes registradas</span
                >
                <span v-else> <q-icon name="info" /> Parte no encontrada</span>
              </q-item-section>
            </q-item>
          </template>
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label
                  >{{ scope.opt.nombre }} {{ scope.opt.aPaterno }}
                  {{ scope.opt.aMaterno }}</q-item-label
                >
                <q-item-label caption>{{
                  scope.opt.descripcionCaracterPersona
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </template>
        </q-select>
      </div>
    </div>
    <div v-if="parametros.tipoParte == 'parteNueva'">
      <div class="q-gutter-lg row">
        <q-select
          v-cortarLabel
          @input-value="parametros.parteCatTipoPersona = null"
          dense
          filled
          use-input
          input-debounce="0"
          v-model="parametros.parteCatTipoPersona"
          option-label="descripcion"
          option-value="catTipoPersonaId"
          label="Tipo de persona *"
          :options="tipoPersonaOptions"
          class="col-4"
          @filter="filtrarTipoPersona"
          :rules="reglasParteTP"
          @update:model-value="cambiaronParametros"
        >
        </q-select>
        <q-select
          v-cortarLabel
          @input-value="parametros.parteCatTipoPersonaCaracter = null"
          dense
          filled
          use-input
          input-debounce="0"
          v-model="parametros.parteCatTipoPersonaCaracter"
          option-label="caracterPersona"
          option-value="caracterPersonaId"
          label="Carácter *"
          :options="tipoPersonaCaracterOptions"
          :rules="reglasPartePC"
          class="col-4"
          @filter="filtrarTipoCaracter"
          @update:model-value="cambiaronParametros"
        >
        </q-select>
      </div>
      <div class="q-gutter-lg row">
        <div class="col-4">
          <q-input
            dense
            filled
            v-model="parametros.parteNombre"
            label="Nombre *"
            :rules="reglasParteNom"
            @update:model-value="cambiaronParametros"
          />
        </div>
        <div class="col-4">
          <q-input
            dense
            filled
            v-model="parametros.parteApellidoPaterno"
            label="Apellido paterno *"
            :rules="reglasParteAP"
            @update:model-value="cambiaronParametros"
          />
        </div>
        <div class="col">
          <q-input
            dense
            filled
            v-model="parametros.parteApellidoMaterno"
            label="Apellido materno"
            @update:model-value="cambiaronParametros"
            :rules="reglasApellidoMaterno"
          />
        </div>
      </div>
    </div>
  </div>
  <div
    v-if="parametros.tipoPromovente == 'autoridad'"
    class="column content-stretchs"
  >
    <div class="q-gutter-lg q-mb-md row items-start">
      <q-select
        v-cortarLabel
        @input-value="parametros.promoventeAutoridad = null"
        dense
        filled
        use-input
        input-debounce="0"
        class="col"
        v-model="parametros.promoventeAutoridad"
        option-label="nombreCompleto"
        option-value="empleadoId"
        label="Busca una autoridad judicial *"
        lazy-rules
        :options="autoridadJudicialOptions"
        :rules="reglasAutoridadJudicial"
        @update:model-value="cambiaronParametros"
        @filter="buscarAutoridadPorTexto"
      >
        <template v-slot:option="scope">
          <q-item v-bind="scope.itemProps">
            <q-item-section>
              <q-item-label>{{ scope.opt.nombreCompleto }}</q-item-label>
              <q-item-label caption
                >{{ scope.opt.cargoDescripcion }} -
                {{ scope.opt.nombreOficial }}</q-item-label
              >
            </q-item-section>
          </q-item>
        </template>
      </q-select>
      <div class="col"></div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount, watch } from "vue";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { Utils } from "src/helpers/utils";
import { FormPromocion } from "../data/form-promocion";
import { Validaciones } from "../../../helpers/validaciones";
import { Promocion } from "../data/promocion";
import { manejoErrores } from "src/helpers/manejo-errores";

const props = defineProps({
  // v-model
  modelValue: {
    default: "",
  },
  detallePromocion: {
    default: {},
  },
  expedienteNuevo: {
    default: false,
  },
  promocion: {
    type: Promocion,
  },
});

const parametros = ref(new FormPromocion());
const catalogosStore = useCatalogosStore();
const usuariosStore = useUsuariosStore();

let tipoPromoventeOptions = ref([]);

let tipoPersonaOptions = ref([]);
let parteExistenteOptions = ref([]);
const promoventeExistenteOptions = ref([]);

let tipoPersonaCaracterOptions = ref([]);

let autoridadJudicialOptions = ref([]);

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  "params:cambio": (val) => val !== null,
});
function cambiaronParametros() {
  emit("params:cambio", parametros);
  // setTimeout(() => {}, 300);
}

const stopWatchDetalle = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.detallePromocion,
  async (_newValue) => {
    // do something
    parametros.value = _newValue;
  },
  {
    immediate: true,
  },
);
onMounted(async () => {
  parametros.value = props.detallePromocion;
  usuariosStore.$subscribe(() => {
    parteExistenteOptions.value = usuariosStore.parteExistente;
    promoventeExistenteOptions.value = usuariosStore.promoventeExistente;
    autoridadJudicialOptions.value = usuariosStore.autoridadJudicial;
  });
  try {
    await catalogosStore.obtenerPromoventes(1);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  tipoPromoventeOptions.value = catalogosStore.tiposPromovente;
  try {
    await catalogosStore.obtenerTipoPersonaCaracter(1);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoPersonaCaracterOptions.value = catalogosStore.tipoPersonaCaracter;
  try {
    await catalogosStore.obtenerTipoPersona(
      parametros.value.tipoAsunto?.catTipoAsuntoId,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoPersonaOptions.value = catalogosStore.tipoPersona;
});

onBeforeUnmount(() => {
  stopWatchDetalle();
});

//Validaciones

const reglasPromovente = ref([
  (val) => Validaciones.validaSelectRequerido(val?.id),
]);

const reglasParteTP = ref([
  (val) => Validaciones.validaSelectRequerido(val?.catTipoPersonaId),
]);
const reglasPartePC = ref([
  (val) => Validaciones.validaSelectRequerido(val?.caracterPersonaId),
]);

const reglasPromoventeNom = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);
const reglasPromoventeAP = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);

const reglasParteNom = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);
const reglasParteAP = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);
const reglasApellidoMaterno = ref([
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);

const reglasAutoridadJudicial = ref([
  (val) =>
    parametros.value?.tipoPromovente == "autoridad"
      ? Validaciones.validaSelectRequerido(val?.empleadoId)
      : true,
]);

/**
 * filtra tipo TipoPromovente
 * @param {*} val valor a buscar
 */
function filtrarTipoPromovente(val, update) {
  update(
    async () => {
      tipoPromoventeOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tiposPromovente,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

/**
 * filtra TipoPersona en combo
 * @param {*} val valor a buscar
 */
function filtrarTipoPersona(val, update) {
  update(
    async () => {
      tipoPersonaOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tipoPersona,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

/**
 * filtra TipoCaracter en combo
 * @param {*} val valor a buscar
 */
function filtrarTipoCaracter(val, update) {
  update(
    async () => {
      tipoPersonaCaracterOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tipoPersonaCaracter,
        "caracterPersona",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

async function buscarAutoridadPorTexto(val, update, abort) {
  update(
    async () => {
      if (val === "" || val.length < 4) {
        abort();
        return;
      } else {
        try {
          await usuariosStore.obtenerAutoridadJudicial(val);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        autoridadJudicialOptions.value = usuariosStore.autoridadJudicial;
      }
    },
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 700),
  );
}

/**
 * filtra TipoCaracter en combo
 * @param {*} val valor a buscar
 */
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
function filtrarPromovente(val, update) {
  update(
    async () => {
      promoventeExistenteOptions.value = Utils.filtrarCombo(
        val,
        usuariosStore.promoventeExistente,
        "label",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>
