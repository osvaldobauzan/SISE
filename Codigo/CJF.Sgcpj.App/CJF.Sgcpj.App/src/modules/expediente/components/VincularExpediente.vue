<template>
  <q-card style="min-width: 700px">
    <q-toolbar>
      <q-toolbar-title>Solicitar vinculación de Expediente</q-toolbar-title>
      <q-btn flat round icon="mdi-close" v-close-popup></q-btn>
    </q-toolbar>
    <q-card-section class="q-gutter-sm">
      <q-item-label class="text-subtitle1 text-bold pad-left"
        >Datos del expediente</q-item-label
      >
      <div class="row wrap">
        <q-item class="col-12">
          <q-select
            v-cortarLabel
            @input-value="parametros.organoJurisddicional = null"
            dense
            filled
            use-input
            input-debounce="0"
            class="col"
            v-model="parametros.organoJurisddicional"
            option-label="nombreOficial"
            option-value="catOrganismoId"
            label="Busca un órgano jurisdiccional *"
            lazy-rules
            :options="autoridadJudicialOptions"
            :rules="reglasAutoridadJudicial"
            @filter="buscarAutoridadPorTexto"
          >
            <template v-slot:option="scope">
              <q-item v-bind="scope.itemProps">
                <q-item-section>
                  <q-item-label>{{ scope.opt.nombreOficial }}</q-item-label>
                </q-item-section>
              </q-item>
            </template>
          </q-select>
        </q-item>

        <q-item class="col-6">
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
            option-label="tipoAsunto"
            option-value="catTipoAsuntoId"
            :disable="esEdicion && !editaExpediente"
          />
        </q-item>
        <q-item class="col-6">
          <q-input
            ref="numExpediente"
            dense
            filled
            class="col"
            v-model="parametros.numeroExpediente"
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
              <span class="red"><q-icon name="info" /></span> Expediente ya
              existente
            </template>
          </q-input>
        </q-item>
      </div>
    </q-card-section>
    <q-separator spaced></q-separator>
    <q-card-actions align="right">
      <q-btn
        no-caps
        label="Vincular expediente"
        color="primary"
        @click="showMensaje"
        style="min-width: 120px"
      />
      <q-btn no-caps flat label="Cancelar" color="secondary" v-close-popup />
    </q-card-actions>
  </q-card>
</template>
<script setup>
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { FormSolicitarVinculacion } from "../data/formSolicitarVincular";
import { ref, onMounted } from "vue";
import { manejoErrores } from "src/helpers/manejo-errores";

const autoridadJudicialOptions = ref([]);
const usuariosStore = useUsuariosStore();
const parametros = ref(new FormSolicitarVinculacion());
const selectTipoAsunto = ref(null);
const catalogosStore = useCatalogosStore();
const tipoAsuntosOpciones = ref(catalogosStore.asuntos);

onMounted(async () => {
  try {
    await catalogosStore.obtenerAsuntos();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoAsuntosOpciones.value = catalogosStore.asuntos;
});

async function buscarAutoridadPorTexto(val, update, abort) {
  update(async () => {
    if (val === "" || val.length < 4) {
      abort();
      return;
    } else {
      try {
        await usuariosStore.obtenerAutoridadJudicial(val);
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
      autoridadJudicialOptions.value = usuariosStore.autoridadJudicial.filter(
        (value, index, self) => {
          return (
            self.findIndex((v) => v.nombreOficial === value.nombreOficial) ===
            index
          );
        },
      );
    }
  });
}
async function showMensaje() {
  alert("dentri");
}
</script>
