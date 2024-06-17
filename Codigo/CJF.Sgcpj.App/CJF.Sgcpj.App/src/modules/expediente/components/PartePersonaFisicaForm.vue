<template>
  <div class="q-py-md col-12">
    <div class="row col-12 q-pb-sm">
      <q-input
        dense
        autofocus
        filled
        v-model="value.nombre"
        label="Nombre(s) *"
        class="q-pa-sm col-xs-12 col-sm-6 col-md-4"
        :rules="reglasRequieridoAfanumerico"
        @update:model-value="cambiaronParametros"
      />
      <q-input
        dense
        filled
        v-model="value.aPaterno"
        label="Apellido paterno *"
        class="q-pa-sm col-xs-12 col-sm-6 col-md-4"
        :rules="reglasRequieridoAfanumerico"
        @update:model-value="cambiaronParametros"
      />
      <q-input
        dense
        filled
        v-model="value.aMaterno"
        label="Apellido materno"
        class="q-pa-sm col-xs-12 col-sm-6 col-md-4"
        :rules="reglasAlfanumerico"
        @update:model-value="cambiaronParametros"
      />
    </div>
    <div class="row">
      <q-select
        v-cortarLabel
        @input-value="(value.caracterPersona = null), cambiaronParametros()"
        dense
        filled
        use-input
        input-debounce="0"
        label-slot
        v-model="value.caracterPersona"
        option-label="caracterPersona"
        option-value="caracterPersonaId"
        :options="tipoPersonaCaracterOptions"
        :rules="[
          (val) => Validaciones.validaSelectRequerido(val?.caracterPersonaId),
        ]"
        class="q-pa-sm col-xs-12 col-sm-6"
        @filter="filtrarTipoCaracter"
        @update:model-value="cambiaronParametros"
      >
        <template v-slot:label>
          <q-item-label>Carácter de la persona *</q-item-label>
        </template>
      </q-select>
    </div>
    <div v-if="value.caracterPersona?.caracterPersonaId == 13" class="row">
      <q-select
        v-cortarLabel
        @input-value="(value.caracterPromueve = null), cambiaronParametros()"
        dense
        filled
        use-input
        input-debounce="0"
        label-slot
        v-model="value.caracterPromueve"
        option-label="descripcion"
        option-value="id"
        :options="caracterPromueveOptions"
        :rules="reglasPartePC"
        class="q-pa-sm col-xs-12 col-sm-8"
        @filter="filtrarCaracterPromueve"
        @update:model-value="cambiaronParametros"
      >
        <template v-slot:label>
          <q-item-label
            >Carácter con el que promueve en su nombre *</q-item-label
          >
        </template>
      </q-select>
    </div>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label>¿Es recurrente?</q-item-label>
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.recurrente"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.recurrente"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
    </div>
    <div class="row q-pt-md">
      <q-select
        v-cortarLabel
        @input-value="(value.sexoJson = null), cambiaronParametros()"
        dense
        filled
        use-input
        input-debounce="0"
        label-slot
        v-model="value.sexoJson"
        option-label="sDescripcion"
        option-value="kIdSexo"
        :options="sexoOptions"
        :rules="[
          (val) => Validaciones.validaSelectRequerido(val?.sDescripcion),
        ]"
        class="q-pa-sm col-xs-12 col-sm-6 col-md-4"
        @filter="filtrarSexo"
        @update:model-value="cambiaronParametros"
      >
        <template v-slot:label>
          <q-item-label>Sexo *</q-item-label>
        </template>
      </q-select>
    </div>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label>¿El sujeto es mayor de edad?</q-item-label>
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2 col-md-2"
        v-model="value.mayorEdad"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2 col-md-2"
        v-model="value.mayorEdad"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-12 col-sm-6 col-md-3"
        v-model="value.mayorEdad"
        :val="2"
        label="Se desconoce"
        @update:model-value="cambiaronParametros"
      />
      <q-select
        v-if="value.mayorEdad == 0 || value.caracterPromueve?.id == 11766"
        v-cortarLabel
        @input-value="(value.edad = null), cambiaronParametros()"
        dense
        filled
        use-input
        input-debounce="0"
        label-slot
        v-model="value.edad"
        option-label="descripcion"
        option-value="id"
        :options="edadOptions"
        :rules="reglasSelectRequeridoPorId"
        class="q-pa-sm col-xs-12 col-sm-12 col-md-5"
        @filter="filtrarEdad"
        @update:model-value="cambiaronParametros"
      >
        <template v-slot:label>
          <q-item-label>Edad *</q-item-label>
        </template>
      </q-select>
    </div>
    <template
      v-if="
        value.caracterPersona?.caracterPersonaId == 13 ||
        value.caracterPersona?.caracterPersonaId == 17
      "
    >
      <q-item class="col-12">
        <q-item-section>
          <q-item-label>¿Es víctima u ofendido del delito?</q-item-label>
        </q-item-section>
      </q-item>
      <div class="row">
        <q-radio
          class="q-pa-sm col-xs-6 col-sm-2"
          v-model="value.victimaOfendidoDelito"
          :val="1"
          label="Sí"
          @update:model-value="cambiaronParametros"
        />
        <q-radio
          class="q-pa-sm col-xs-6 col-sm-2"
          v-model="value.victimaOfendidoDelito"
          :val="0"
          label="No"
          @update:model-value="cambiaronParametros"
        />
      </div>
    </template>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label>¿Es sujeto de derecho agrario?</q-item-label>
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.sujetoDerechoAgrario"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.sujetoDerechoAgrario"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
    </div>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label
          >Oposición para la publicación de datos personales</q-item-label
        >
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.aceptaOponePublicarDatos"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.aceptaOponePublicarDatos"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
      <q-input
        v-if="value.aceptaOponePublicarDatos == 1"
        dense
        filled
        class="q-pa-sm col-xs-12 col-sm-6 col-md-4"
        v-model="value.fechaAceptaOponePublicarDatos"
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
                v-model="value.fechaAceptaOponePublicarDatos"
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
    </div>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label
          >¿Se considera a la persona como parte integrante de un grupo de
          población vulnerable?
        </q-item-label>
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.esParteGrupoVulnerable"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.esParteGrupoVulnerable"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
      <q-select
        v-if="value.esParteGrupoVulnerable == 1"
        v-cortarLabel
        @input-value="(value.grupoVulnerableJson = null), cambiaronParametros()"
        dense
        filled
        use-input
        input-debounce="0"
        label-slot
        v-model="value.grupoVulnerableJson"
        option-label="descripcion"
        option-value="id"
        :options="grupoVulnerableOptions"
        :rules="reglasSelectRequeridoPorId"
        class="q-pa-sm col-xs-12 col-sm-6"
        @filter="filtrarGrupoVulnerable"
        @update:model-value="cambiaronParametros"
      >
        <template v-slot:label>
          <q-item-label
            >Indique el grupo vulnerable al que pertenece *</q-item-label
          >
        </template>
      </q-select>
    </div>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label> ¿Habla alguna lengua?</q-item-label>
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.hablaLengua"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.hablaLengua"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
      <q-select
        v-if="value.hablaLengua == 1"
        v-cortarLabel
        @input-value="(value.lenguaJson = null), cambiaronParametros()"
        dense
        filled
        use-input
        input-debounce="0"
        label-slot
        v-model="value.lenguaJson"
        option-label="descripcion"
        option-value="id"
        :options="lenguaOptions"
        :rules="reglasSelectRequeridoPorId"
        class="q-pa-sm col-xs-12 col-sm-6"
        @filter="filtrarLeguna"
        @update:model-value="cambiaronParametros"
      >
        <template v-slot:label>
          <q-item-label>¿Qué lengua? *</q-item-label>
        </template>
      </q-select>
    </div>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label>¿Cuenta con traductor?</q-item-label>
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.traductor"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.traductor"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
    </div>
  </div>
</template>

<script setup>
import { Utils } from "src/helpers/utils";
import { Validaciones } from "src/helpers/validaciones";
import { computed, ref, onMounted } from "vue";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Edad } from "src/data/catalogo-edad";
import { PersonaAsunto } from "../data/form-parte";

const catalogosStore = useCatalogosStore();
const tipoPersonaCaracterOptions = ref([]);
const caracterPromueveData = ref([]);
const caracterPromueveOptions = ref([]);
const sexoOptions = ref([]);
const edadData = ref(Edad);
const edadOptions = ref(Edad);
const lenguaData = ref([]);
const lenguaOptions = ref([]);
const grupoVulnerableData = ref([]);
const grupoVulnerableOptions = ref([]);
const reglasRequieridoAfanumerico = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);
const reglasAlfanumerico = ref([
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);
const reglasPartePC = ref([(val) => Validaciones.validaInputRequerido(val)]);
const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);
const reglasSelectRequeridoPorId = ref([
  (val) => Validaciones.validaSelectRequerido(val?.id),
]);
const props = defineProps({
  // v-model
  modelValue: {
    default: {},
  },
  esEditar: {
    default: false,
  },
});

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value,
});

const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});
onMounted(async () => {
  try {
    caracterPromueveData.value =
      await catalogosStore.obtenerCatalogoGenerico(521);
    caracterPromueveOptions.value = caracterPromueveData.value;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoPersonaCaracterOptions.value = catalogosStore.tipoPersonaCaracter;
  try {
    await catalogosStore.obtenerSexo();
    sexoOptions.value = catalogosStore.sexo;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    grupoVulnerableData.value =
      await catalogosStore.obtenerCatalogoGenerico(832);
    grupoVulnerableOptions.value = grupoVulnerableData.value;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    lenguaData.value = await catalogosStore.obtenerCatalogoGenerico(2156);
    lenguaOptions.value = lenguaData.value;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (props.esEditar) {
    value.value.caracterPersona = tipoPersonaCaracterOptions.value?.find(
      (x) => x.caracterPersonaId == value.value.catCaracterPersonaAsuntoId,
    );
    value.value.caracterPromueve = caracterPromueveOptions.value?.find(
      (x) => x.id == value.value.caracterPromueveNombre,
    );
    value.value.sexoJson = sexoOptions.value?.find(
      (x) => x.kIdSexo == value.value.sexo,
    );
    value.value.edad = edadData.value?.find(
      (x) => x.id == value.value.edadMenor,
    );
    value.value.grupoVulnerableJson = grupoVulnerableData.value?.find(
      (x) => x.id == value.value.grupoVulnerable,
    );
    value.value.lenguaJson = lenguaData.value?.find(
      (x) => x.id == value.value.lengua,
    );
  } else {
    Object.assign(value.value, new PersonaAsunto());
  }
});
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
/**
 * filtra CaracterPromueve en combo
 * @param {*} val valor a buscar
 */
function filtrarCaracterPromueve(val, update) {
  update(
    async () => {
      caracterPromueveOptions.value = Utils.filtrarCombo(
        val,
        caracterPromueveData.value,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function filtrarEdad(val, update) {
  update(
    async () => {
      edadOptions.value = Utils.filtrarCombo(
        val,
        edadData.value,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function filtrarSexo(val, update) {
  update(
    async () => {
      sexoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.sexo,
        "sDescripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function filtrarLeguna(val, update) {
  update(
    async () => {
      lenguaOptions.value = Utils.filtrarCombo(
        val,
        lenguaData.value,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function filtrarGrupoVulnerable(val, update) {
  update(
    async () => {
      grupoVulnerableOptions.value = Utils.filtrarCombo(
        val,
        grupoVulnerableData.value,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function cambiaronParametros() {
  emit("update:modelValue", value.value);
}
</script>

<style scoped lang="css"></style>
