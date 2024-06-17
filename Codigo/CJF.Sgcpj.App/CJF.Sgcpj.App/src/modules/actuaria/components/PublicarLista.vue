<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title class="text-bold">Publicar lista</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-banner class="light-blue-3 doc-note doc-note--tip">
      <template v-slot:avatar>
        <q-icon name="mdi-information" color="secondary" size="sm" />
      </template>
      Permitirá la visualización en el Portal de Servicios en Línea.
    </q-banner>
    <q-card-section>
      <div class="row justify-end">
        <q-item class="col">
          <q-item-section>
            <div class="q-pa-md" style="max-width: 300px">
              <q-label>Fecha de consulta lista publicación</q-label>
              <q-input filled v-model="fechaPublicacion">
                <template v-slot:append>
                  <q-icon name="event" class="cursor-pointer">
                    <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                      <q-date v-model="fechaPublicacion" @update:model-value="consultarLista">
                        <div class="row items-center justify-end">
                          <q-btn v-close-popup label="Cerrar" color="primary" flat />
                        </div>
                      </q-date>
                    </q-popup-proxy>
                  </q-icon>
                </template>
              </q-input>
            </div>
          </q-item-section>
        </q-item>
        <q-item class="col-6">
          <q-btn v-permitido="31" flat no-caps color="secondary" icon="mdi-plus-circle" label="Síntesis manual"
            @click="showAcuerdoManual = true" />
        </q-item>
      </div>
      <div class="row justify-center">
        <q-markup-table v-if="listaSintesisManual.length !== 0" class="col-12">
          <thead>
            <tr>
              <th>Expediente</th>
              <th>Tipo Asunto</th>
              <th>Organismo</th>
              <th>sintesis</th>
              <th>Nombre del Quejoso</th>
              <th>Autoridad(es)</th>
              <th>Fecha del acuerdo</th>
              <th>Fecha Publicación</th>
            </tr>
          </thead>
          <tbody>
            <tr :key="index" v-for="(sintesisManual, index) in listaSintesisManual">
              <td>{{ sintesisManual.asuntoAlias }}</td>
              <td>{{ sintesisManual.catTipoAsunto }}</td>
              <td>{{ sintesisManual.catOrganismo }}</td>
              <td>{{ sintesisManual.sintesis }}</td>
              <td>{{ getNombreCompleto(sintesisManual.quejoso) }}</td>
              <td>{{ getNombreCompleto(sintesisManual.autoridad) }}</td>
              <td>
                {{
                  date.formatDate(sintesisManual.fechaPublicacion, "DD-MM-YYYY")
                }}
              </td>
              <td>
                {{
                  date.formatDate(sintesisManual.fechaPublicacion, "DD-MM-YYYY")
                }}
              </td>
              <td></td>
            </tr>
          </tbody>
        </q-markup-table>
        <q-circular-progress v-else-if="loading" indeterminate rounded size="50px" color="blue" class="q-ma-md"
          :value="'cargando...'" />
        <p v-else>No se encontraron resultados</p>
      </div>
    </q-card-section>
    <q-card-actions class="q-gutter-sm">
      <q-space></q-space>
      <q-btn no-caps outline color="primary" label="Cerrar" style="width: 120px" v-close-popup></q-btn>
    </q-card-actions>
  </q-card>

  <q-dialog v-model="showAcuerdoManual" persistent>
    <AcuerdoManual @cerrar="showAcuerdoManual = false" @refrescar="consultarLista" />
  </q-dialog>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { date } from "quasar";
import AcuerdoManual from "./AcuerdoManual.vue";
import { useActuariaListaStore } from "../stores/actuaria-lista_acuerdos-store";
const actuariaListaStore = useActuariaListaStore();

const fechaPublicacion = ref(new Date());
const showAcuerdoManual = ref(false);
const listaSintesisManual = ref([]);
const loading = ref(false);
onMounted(async () => {
  consultarLista();
});

function getNombreCompleto(persona) {
  const nombre = persona?.nombre ?? "";
  const materno = persona?.aMaterno ?? "";
  const paterno = persona?.aPaterno ?? "";
  return nombre + " " + materno + " " + paterno;
}

async function consultarLista() {
  fechaPublicacion.value = date.formatDate(fechaPublicacion.value, "DD-MM-YYYY");
  loading.value = true;
  listaSintesisManual.value = [];
  await actuariaListaStore
    .getListaSintesisManual(date.formatDate(fechaPublicacion.value, "YYYY-MM-DD"))
    .then(() => (loading.value = false));
  listaSintesisManual.value = actuariaListaStore.listaSintesisManual;
}
</script>
