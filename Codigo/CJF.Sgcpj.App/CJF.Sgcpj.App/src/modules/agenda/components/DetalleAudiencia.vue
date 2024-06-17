<template>
  <q-card style="min-width: 35vw">
    <q-toolbar>
      <q-toolbar-title></q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-item-label
      class="q-pl-md q-pb-md q-pt-md bg-light-blue-2 text-weight-bold text-subtitle2"
    >
      {{ `${value.expediente} ${value.tipoAsunto}` }}
    </q-item-label>
    <q-item-label class="q-pl-md q-pb-md">
      <q-icon name="mdi-clock-outline" :size="'sm'" />
      {{ value.label }}
    </q-item-label>
    <q-item-label class="q-pl-md q-pb-md">
      <q-icon name="mdi-map-marker-outline" :size="'sm'" />
      {{ value.audiencia || "" }}
    </q-item-label>
    <q-item-label class="q-pl-md q-pb-md">
      <q-icon name="mdi-account-outline" :size="'sm'" />
      {{ value.secretario || "" }}
    </q-item-label>
    <q-item-label class="q-pl-md q-pb-md">
      <q-icon name="mdi-account-group-outline" :size="'sm'" />
      {{ value.parte || "" }}
    </q-item-label>
    <q-item-label class="q-pl-md q-pb-md">
      <q-icon name="mdi-laptop" :size="'sm'" />
      {{ value.empleado || "" }}
    </q-item-label>
    <div class="q-pb-md row wrap col-12">
      <q-icon class="col-1" name="mdi-text-box-outline" :size="'sm'" />
      <q-form ref="form" class="col-10">
        <div class="">
          <q-select
            v-cortarLabel
            @input-value="value.resultado = null"
            dense
            filled
            class=""
            use-input
            input-debounce="0"
            v-model="value.resultado"
            label=""
            option-label="descripcion"
            option-value="id"
            @filter="filtrarResultado"
            @update:model-value="cambioForm"
            :options="resultadoOptions"
            :loading="cargandoResultado"
            :rules="[
              (val) => Validaciones.validaSelectRequerido(val?.descripcion),
            ]"
          >
          </q-select>
        </div>
      </q-form>
    </div>
    <q-card-actions class="q-ml-md" align="left">
      <q-btn
        style="min-width: 200px"
        class=""
        no-caps
        :disable="!formValido"
        :color="formValido ? 'blue' : 'grey-6'"
        label="Guardar"
        @click="formValido ? guardar() : null"
      ></q-btn>
    </q-card-actions>
    <q-inner-loading :showing="cargandoGuardado"> </q-inner-loading>
  </q-card>
</template>

<script setup lang="js">
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import { Validaciones } from "src/helpers/validaciones";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { computed, onMounted, ref } from "vue";
import { useAgendaStore } from "../stores/agenda-store";
import { noty } from "src/helpers/notify";

const catalogosStore = useCatalogosStore();
const agendaStore = useAgendaStore();
const resultadoOptions = ref([]);
const cargandoResultado = ref(false);
const cargandoGuardado = ref(false);
const form = ref(null);
const formValido = ref(false);

const props = defineProps({
  // v-model
  modelValue: {
    default: "",
  },
});
const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  success: () => true,
});
onMounted(async () => {
  try {
    cargandoResultado.value = true;
    await catalogosStore.obtenerResultadoAdiencia(value.value.idTipoAudiencia);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoResultado.value = false;
  resultadoOptions.value = catalogosStore.resultadosAudiencia;
});
async function guardar() {
  cargandoGuardado.value = true;
  try {
    const params = {
      idAgenda: value.value.idAgenda,
      idResultado: value.value.resultado.id,
    };
    await agendaStore.modificarEstado(params);
	noty.correcto(`Se guardó el estado ${value.value.resultado.descripcion} con sentencia para el expediente ${value.value.expediente} ${value.value.tipoAsunto}`);
	emit("success");
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoGuardado.value = false;
}

const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});
/**
 * filtra resultado de audiencia en combo
 * @param {*} val valor a buscar
 */
function filtrarResultado(val, update) {
  update(
    async () => {
      resultadoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.resultadosAudiencia,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
async function cambioForm() {
  formValido.value = await form.value?.validate(false);
}
</script>

<style scoped lang="css"></style>
