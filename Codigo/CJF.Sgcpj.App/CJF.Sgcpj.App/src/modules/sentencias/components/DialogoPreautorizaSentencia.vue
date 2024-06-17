<template>
    <q-card style="width: 700px; max-width: 80vw;">
    <q-card-section class="row items-center">
        <span class="text-h6">
        <span v-if="registrosSeleccionados.length > 0">Preautorizar Sentencias</span>
        <span v-else>Error</span>
        </span>
    </q-card-section>
    <q-separator />        
    <q-card-section class="q-pt-none">
        <span v-if="registrosSeleccionados.length > 0">Se preautorizarán las sentencias: {{ registrosSeleccionados.map(c => c.expediente.asuntoAlias).join(', ') }} seleccionadas<br>¿Desea continuar?</span>
        <span v-else>No se ha seleccionado ningún acuerdo</span>
    </q-card-section>
    <q-separator />
    <q-card-actions align="right">
        <q-btn v-if="registrosSeleccionados.length > 0 && sonCJPF || true" flat label="Continuar sin firma" color="primary" v-close-popup @click="preautorizarSinFirma();" />
        <q-btn @click="handleClick" v-if="registrosSeleccionados.length > 0 && sonCJPF" label="Continuar con firma" color="primary" unelevated v-close-popup />
        <q-btn @click="handleClick" v-if="registrosSeleccionados.length > 0 && !sonCJPF" flat label="Continuar" color="primary" v-close-popup />
        <q-btn flat label="Cancelar" color="primary" v-close-popup />
    </q-card-actions>
    </q-card>
</template>

<script setup>
import { ref, onMounted, defineEmits } from "vue";
import { manejoErrores } from "src/helpers/manejo-errores.js";
import { useSentenciasStore } from "../store/sentencias-store.js";

const props = defineProps({
    registrosSeleccionados:{
        default: ref([])
    }
});
const emit = defineEmits(["solicitudExitosa", "buttonClicked"]);

const handleClick = () => {
  emit('buttonClicked');
};

const tipoOrganismo = {
  Juzgado_de_Distrito: 2,
  Tribunal_Unitario_de_Circuito: 3,
  Tribuna_Colegiado_de_Circuito: 4,
  Tribuna_Unitario_Auxiliar: 32,
  Centro_de_Justicia_Penal_Federal: 65
};

const sonCJPF = ref(false);
const sentenciasStore = useSentenciasStore();

onMounted(async () => {
    sonCJPF.value = props.registrosSeleccionados.every(c => c.tipoOrganismoId === tipoOrganismo.Centro_de_Justicia_Penal_Federal);
});

async function preautorizarSinFirma () {
    
    const sentencias = props.registrosSeleccionados.map((c) => ({'AsuntoNeunId': c.expediente.asuntoNeunId, 'AsuntoDocumentoId':c.asuntoDocumentoId}));
    try {
        await sentenciasStore.preautorizarSinFirma(sentencias).then(emit("solicitudExitosa", null));
  } 
  catch (error) {
    manejoErrores.mostrarError(error);
  }
}

</script>
