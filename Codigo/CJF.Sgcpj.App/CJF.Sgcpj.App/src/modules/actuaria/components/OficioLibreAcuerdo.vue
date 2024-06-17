<template>
  <q-card style="min-width: 90vw; min-height: 65vh">
    <OficioLibre
      v-model="oficioLibre"
      :edicion="props.parte.text"
      :cambio-oficio-libre="cambioOficioLibre"
      @cerrarEditar="
        (val) => {
          emit('cerrar', true);
        }
      "
      @update:modelValue="
        (val) => {
          if (val) {
            oficioLibre = val;
            emit('cerrar', true);
          }

          cambioOficioLibre = true;
        }
      "
      :es-actuaria="true"
      :acuerdo="acuerdo"
    >
    </OficioLibre>
  </q-card>
  <DialogConfirmacion
    v-model="showCancelarEditarOficio"
    label-btn-cancel="No"
    label-btn-ok="Sí, cancelar"
    titulo="¿Deseas cancelar el oficio libre?"
    :subTitulo="`No se guardará ninguna información que hayas agregado`"
    @aceptar="
      () => {
        emit('cerrar', {
          value: true,
          text: originalText,
        });
        showCancelarEditarOficio = false;
      }
    "
    @cancelar="showCancelarEditarOficio = false"
  ></DialogConfirmacion>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";
import OficioLibre from "src/components/OficioLibre.vue";
import { useTramiteStore } from "src/modules/tramite/store/tramite-store";

const tramiteStore = useTramiteStore();
const showCancelarEditarOficio = ref(false);
const cambioOficioLibre = ref(false);
const originalText = ref("");

const oficioLibre = computed({
  get() {
    return {
      ...props.parte,
      nombres: props.parte.parte,
      expediente: props.acuerdo.expediente.asuntoAlias,
    };
  },
  set() {},
});
onMounted(() => {
  originalText.value = props.parte.text;
  tramiteStore.actualizarOficioLibre(false);
});

const props = defineProps({
  parte: {
    default: {},
  },
  acuerdo: {
    type: Object,
  },
});
const emit = defineEmits({
  // v-model event with validation
  cerrar: (value) => value !== null,
});
</script>

<style scoped>
:deep(.q-splitter--vertical > .q-splitter__panel) {
  height: unset;
}
:deep(.note-editor.note-frame .note-editing-area) {
  min-height: 60vh;
  max-height: 65vh;
}
:deep(.note-editor.note-frame .note-editing-area .note-editable) {
  min-height: 50vh;
  max-height: 60vh;
}
</style>
