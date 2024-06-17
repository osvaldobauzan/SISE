<template>
  <q-card style="min-width: 80vw">
    <q-toolbar>
      <q-toolbar-title> Registrar Engrose (Sentencia) </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-stepper
      header-nav
      v-model="step"
      ref="stepper"
      color="primary"
      animated
      keep-alive
      done-color="positive"
      active-color="primary"
      inactive-color="grey"
      >
      <q-step
      :name="1"
      title="Información principal"
      prefix="1"
      :done="step > 1"
      class="scroll"
        style="max-height: 65vh;"
      >
        <SentenciaEngrose
          :item="item"
          >
        </SentenciaEngrose>
      </q-step>
      <q-step
        :name="2"
        title="Información general"
        prefix="2"
        :done="step > 2"
        class="scroll"
        style="max-height: 65vh;"
      >
        <InformacionSentenciaEngrose :item="item"></InformacionSentenciaEngrose>
      </q-step>

      <q-step
        :name="3"
        title="Información opcional"
        prefix="3" :done="step > 3"
        class="scroll"
        style="max-height: 65vh;"
        >
        <InformacionOpcionalEngrose :item="item"></InformacionOpcionalEngrose>
      </q-step>

      <template v-slot:navigation>
        <q-separator spaced/>
        <q-stepper-navigation>
          <div class="row q-col-gutter-md justify-end">

          <div class="col-4">
            <q-btn
            no-caps
            @click="
            guardar();
            $refs.stepper.next();"
            color="primary"
            :label="step === 3 ? 'Subir' : 'Continuar'"
            class="fit"
          />
          </div>
          <div class="col-4">
            <q-btn
            v-if="step > 1"
            outline color="primary"
            no-caps
            @click="$refs.stepper.previous();"
            label="Regresar"
            class="fit"
          />
          </div>
          </div>
        </q-stepper-navigation>
      </template>
    </q-stepper>

    <q-inner-loading :showing="subiendoProyecto">
      <q-spinner-grid size="2em" />
      <span class="q-mt-md">Cifrando proyecto...</span>
    </q-inner-loading>
  </q-card>

</template>

<script setup>

  import { ref } from "vue";
  import { Utils } from "src/helpers/utils";

  import SentenciaEngrose from "./engroseComponents/SentenciaEngrose.vue";
  import InformacionSentenciaEngrose from "./engroseComponents/InformacionSentenciaEngrose.vue";
  import InformacionOpcionalEngrose from "./engroseComponents/InformacionOpcionalEngrose.vue";
  import { useEngroseStore } from "../store/engrose-store";
  import { manejoErrores } from "src/helpers/manejo-errores";

  import { noty } from "src/helpers/notify";
  // import { noty } from "src/helpers/notify";

  const step = ref(1);
  const subiendoProyecto = ref(false);

  const engroseStore = useEngroseStore();

  const emit = defineEmits({
    refrescarTabla: (value) => value !== null,
  });

  const props = defineProps({
    item: {
      type: Object,
      required: true,
    }
  });

  async function guardar() {
    if(step.value < 3)
    return;

    subiendoProyecto.value = true;
    let data = new FormData();

    engroseStore.sentenciaData.TipoCuadernoId = props.item?.expediente.tipoCuadernoId;
    engroseStore.sentenciaVP.AsuntoNeunId = engroseStore.sentenciaData.AsuntoNeunId = props.item?.expediente.asuntoNeunId;

    data.append("sentencia", JSON.stringify(engroseStore.sentenciaData));
    data.append("sentenciaVP", JSON.stringify(engroseStore.sentenciaVP));
    data.append(engroseStore.file.name, Utils.blobToFile(engroseStore.file), engroseStore.file.name);

    try {
      await engroseStore.addSentencia(data);
      noty.correcto(`Se ha subido el engrose de manera correcta`);
      emit("refrescarTabla");
      //emit("cerrar");
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    subiendoProyecto.value = false;
  }

</script>
