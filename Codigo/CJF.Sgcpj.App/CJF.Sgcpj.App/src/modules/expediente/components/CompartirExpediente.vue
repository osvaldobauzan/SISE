<template>
  <q-card style="min-width: 700px">
    <q-toolbar>
      <q-toolbar-title> Compartir Expediente </q-toolbar-title>
      <q-btn flat round icon="mdi-close" v-close-popup></q-btn>
    </q-toolbar>
    <q-card-section>
      <div class="row">
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption>Expediente</q-item-label>
              <q-item-label>10/2023</q-item-label>
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption>Tipo de asunto</q-item-label>
              <q-item-label>Amparo indirecto</q-item-label>
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption>Cuaderno</q-item-label>
              <q-item-label>Principal</q-item-label>
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption>NEUN</q-item-label>
              <q-item-label>7654321</q-item-label>
            </q-item-section>
          </q-item>
        </div>
      </div>
      <div class="row">
        <q-item>
          <q-item-section>
            <q-item-label>
              Podrás permitir el acceso a este expediente a cualquier órgano
              jurisdiccional que selecciones.
            </q-item-label>
            <q-item-label class="q-mt-xl">
              <q-select
                filled
                use-input
                @input-value="parametros.organoJurisddicional = null"
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
            </q-item-label>
          </q-item-section>
        </q-item>
      </div>
    </q-card-section>
    <q-separator spaced></q-separator>
    <q-card-actions align="right">
      <q-btn no-caps class="q-px-lg" label="Compartir" color="primary" />
      <q-btn
        flat
        no-caps
        class="q-px-lg"
        label="Cancelar"
        color="primary"
        v-close-popup
      />
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { FormSolicitarVinculacion } from "../data/formSolicitarVincular";

const parametros = ref(new FormSolicitarVinculacion());
const usuariosStore = useUsuariosStore();
const autoridadJudicialOptions = ref([]);
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
</script>
