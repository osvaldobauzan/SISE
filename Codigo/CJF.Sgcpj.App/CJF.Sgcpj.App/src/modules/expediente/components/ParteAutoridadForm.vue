<template>
  <div class="q-py-md col-12">
    <div class="row">
      <q-input
        class="q-pa-sm col-12"
        dense
        filled
        v-model="value.denominacionDeAutoridad"
        label="Denominación *"
        @update:model-value="cambiaronParametros"
        :rules="reglasInputRequerido"
      />
    </div>
    <div class="row">
      <q-select
        class="q-pa-sm col-9"
        label-slot
        :options="caracterPersona"
        option-label="caracterPersona"
        option-value="caracterPersonaId"
        v-model="value.caracterPersona"
        @filter="filtrarTipoCaracter"
        v-cortarLabel
        dense
        filled
        use-input
        input-debounce="0"
        @input-value="(value.caracterPersona = null), cambiaronParametros()"
        @update:model-value="cambiaronParametros"
        :rules="reglasInputRequerido"
      >
        <template v-slot:label>
          <q-item-label>Carácter de la persona *</q-item-label>
        </template>
      </q-select>
    </div>
    <div class="row" v-if="mostrarPregunta('PPromueveNombre')">
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
        :rules="reglasInputRequerido"
        class="q-pa-sm col-9"
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
    <div class="row">
      <q-select
        class="q-pa-sm col-12"
        label-slot
        v-model="value.clasificaAutoridadGenerica"
        :options="clasificacionAutoridadGenerica"
        option-label="descripcion"
        option-value="clasificaAutoridadGenericaId"
        v-cortarLabel
        dense
        filled
        use-input
        input-debounce="0"
        @filter="filtrarClasificacionAutoridad"
        @input-value="
          (value.clasificaAutoridadGenerica = null), cambiaronParametros()
        "
        @update:model-value="cambiaronParametros"
        :rules="reglasInputRequerido"
      >
        <template v-slot:label>
          <q-item-label>Clasificación como autoridad genérica *</q-item-label>
        </template>
      </q-select>
    </div>
    <q-item class="col-12">
      <q-item-section>
        <q-item-label>¿Es Foráneo?</q-item-label>
      </q-item-section>
    </q-item>
    <div class="row">
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.foraneo"
        :val="1"
        label="Sí"
        @update:model-value="cambiaronParametros"
      />
      <q-radio
        class="q-pa-sm col-xs-6 col-sm-2"
        v-model="value.foraneo"
        :val="0"
        label="No"
        @update:model-value="cambiaronParametros"
      />
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { Utils } from "src/helpers/utils";
import { Validaciones } from "src/helpers/validaciones";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store";
import { PersonaAsunto } from "../data/form-parte";

const expedienteStore = useExpedienteElectronicoStore();
const catalogosStore = useCatalogosStore();
const caracterPersona = ref(catalogosStore.tipoPersonaCaracter);
const clasificacionAutoridadGenerica = ref(
  catalogosStore.clasificacionAutoridadGenerica,
);
const caracterPromueveOptions = ref([]);

const reglasInputRequerido = ref([
  (val) => Validaciones.validaInputRequerido(val),
]);

const emit = defineEmits({
  "update:modelValue": (value) => value,
});

// eslint-disable-next-line no-unused-vars
const props = defineProps({
  // v-model
  modelValue: {
    default: {},
  },
  esEditar: {
    default: false,
  },
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
    caracterPromueveOptions.value =
      await catalogosStore.obtenerCatalogoGenerico(521);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (props.esEditar) {
    value.value.caracterPersona = caracterPersona.value?.find(
      (x) => x.caracterPersonaId == value.value.catCaracterPersonaAsuntoId,
    );
    value.value.caracterPromueve = caracterPromueveOptions.value?.find(
      (x) => x.id == value.value.caracterPromueveNombre,
    );
    value.value.clasificaAutoridadGenerica =
      clasificacionAutoridadGenerica.value?.find(
        (x) =>
          x.clasificaAutoridadGenericaId ==
          value.value.clasificaAutoridadGenericaId,
      );
  } else {
    Object.assign(value.value, new PersonaAsunto());
  }
});
function cambiaronParametros() {
  emit("update:modelValue", value.value);
}

const mostrarPregunta = computed(() => {
  return (pregunta) => {
    const caracterPersonaId = value.value.caracterPersona?.caracterPersonaId;
    return expedienteStore.debeMostrarPregunta(pregunta, caracterPersonaId);
  };
});
function filtrarCaracterPromueve(val, update) {
  update(
    async () => {
      caracterPromueveOptions.value = Utils.filtrarCombo(
        val,
        caracterPromueveOptions.value,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function filtrarTipoCaracter(val, update) {
  update(
    async () => {
      caracterPersona.value = Utils.filtrarCombo(
        val,
        catalogosStore.tipoPersonaCaracter,
        "caracterPersona",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
function filtrarClasificacionAutoridad(val, update) {
  update(
    async () => {
      clasificacionAutoridadGenerica.value = Utils.filtrarCombo(
        val,
        catalogosStore.clasificacionAutoridadGenerica,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>

<style scoped lang="css"></style>
