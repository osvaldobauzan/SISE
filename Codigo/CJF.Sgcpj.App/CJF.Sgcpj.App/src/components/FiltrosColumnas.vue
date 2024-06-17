<template>
  <q-select
    dense
    rounded
    outlined
    use-input
    input-debounce="0"
    @input-value="
      emit('cambioFiltro', { value: null, filtroValor: props.filtroValor })
    "
    v-model="filtroSeleccionado"
    :label="label"
    :options="opcionesDatosCopia"
    @update:modelValue="opcionSeleccionada"
    @filter="filtrarOpciones"
  >
    <template v-slot:option="scope">
      <q-item v-bind="scope.itemProps">
        <q-item-section>
          <q-item-label>{{ scope.opt.label }}</q-item-label>
          <q-item-label caption>{{ scope.opt.subLabel }}</q-item-label>
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script setup>
import { ref, computed } from "vue";
import { Utils } from "src/helpers/utils";

const props = defineProps({
  label: String,
  opciones: Array,
  filtroValor: String,
  valoresFiltros: Object,
  labelDefault: {
    default: "Ninguno",
  },
  valorDefault: {
    default: "",
  },
});

const emit = defineEmits(["cambioFiltro"]);
const opcionesDatosCopia = ref([]);

const opcionesConValDefault = computed(() => {
  return [
    { label: props.labelDefault, value: props.valorDefault },
    ...props.opciones,
  ];
});

const filtroSeleccionado = computed(() => {
  const valorActual = props.valoresFiltros[props.filtroValor];
  const opcionSeleccionada = props.opciones.find(
    (opcion) => opcion.value === valorActual,
  );
  return opcionSeleccionada || null;
});
function opcionSeleccionada(opcion) {
  emit("cambioFiltro", { value: opcion.value, filtroValor: props.filtroValor });
}

/**
 * filtra tipo procedimiento en combo
 * @param {*} val valor a buscar
 */
function filtrarOpciones(val, update) {
  update(
    async () => {
      opcionesDatosCopia.value = Utils.filtrarCombo(
        val,
        opcionesConValDefault.value,
        "label",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>
