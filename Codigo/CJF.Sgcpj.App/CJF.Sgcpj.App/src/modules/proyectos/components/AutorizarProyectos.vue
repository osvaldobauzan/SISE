<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title>Autorizar Proyectos</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-item>
      <q-item-section>
        <q-item-label>
          Selecciona de los proyectos que deseas autorizar.
        </q-item-label>
      </q-item-section>
    </q-item>
    <q-toolbar>
      <q-space></q-space>
      <q-input
        dense
        rounded
        outlined
        bg-color="white"
        v-model="textoBuscar"
        placeholder="Buscar expediente"
      >
        <template v-slot:append>
          <q-icon name="mdi-magnify" />
        </template>
      </q-input>
    </q-toolbar>
    <q-table
      flat
      style="max-height: 50vh"
      :filter="textoBuscar"
      ref="tableRef"
      :rows="registros"
      :columns="columns"
      row-key="index"
      selection="multiple"
      v-model:selected="selected"
    >
      <template v-slot:header-selection="scope">
        <q-checkbox v-model="scope.selected" />
      </template>
      <template v-slot:body="props">
        <q-tr>
          <q-td>
            <q-checkbox
              :model-value="props.selected"
              @update:model-value="
                (val, evt) => {
                  Object.getOwnPropertyDescriptor(props, 'selected').set(
                    val,
                    evt,
                  );
                }
              "
          /></q-td>
          <q-td style="max-width: 15em">
            <q-item>
              <q-item-section>
                <q-item-label class="text-bold">
                  {{ props.row.AsuntoAlias }}
                </q-item-label>
                <q-item-label caption>
                  {{ props.row.CatTipoAsunto }}
                </q-item-label>
                <q-item-label>
                  <q-badge
                    :class="`bg-${props.row.NombreCorto} text-black`"
                    :label="getBook(props.row.NombreCorto)"
                    v-if="props.row.NombreCorto"
                  >
                  </q-badge>
                </q-item-label>
              </q-item-section>
            </q-item>
          </q-td>
          <q-td
            style="border-left: rgb(190, 190, 190) solid 1px"
            class="text-center"
          >
            <q-btn
              flat
              stack
              color="secondary"
              icon="mdi-paperclip"
              :label="date.formatDate(props.row.FechaAudiencia, 'DD/MM/YYYY')"
              @click="showDialogPdf = true"
            >
            </q-btn>
          </q-td>
          <q-td class="text-center"
            >{{ props.row.Secretario || "" }}<br />
            <q-item-label class="text-secondary">{{
              date.formatDate(props.row.FechaAudiencia, "DD/MM/YYYY")
            }}</q-item-label>
            <q-item-label class="text-secondary">{{
              date.formatDate(props.row.FechaAudiencia, "HH:mm:ss")
            }}</q-item-label>
          </q-td>
          <q-td>
            <div class="text-center">
              {{ props.row.Preautorizo }}<br />
              <q-item-label class="text-secondary">{{
                date.formatDate(props.row.FechaPreautoriza, "DD/MM/YYYY")
              }}</q-item-label>
              <q-item-label class="text-secondary">{{
                date.formatDate(props.row.FechaPreautoriza, "HH:mm:ss")
              }}</q-item-label>
            </div>
          </q-td>
        </q-tr>
      </template>
    </q-table>
    <q-card-actions align="left">
      <q-btn
        label="Continuar"
        :color="selected.length > 0 ? 'secondary' : 'grey-6'"
        style="min-width: 164px"
        type="submit"
        :disable="!!selected.length < 1"
        @click="changeEstado()"
        v-close-popup
      />
      <q-btn
        v-close-popup
        outline
        label="Cancelar"
        :color="'secondary'"
        style="min-width: 164px"
      />
    </q-card-actions>
  </q-card>
  <q-dialog v-model="showDialogPdf" full-height full-width>
    <ViewPdfComponent :nombreArchivo="SentenciaEjemplo" title="Engrose" />
  </q-dialog>
</template>

<script setup>
import { ref } from "vue";
import { date } from "quasar";
import { catTipoAsunto } from "src/data/catalogos";
import { useProyectosStore } from "../store/proyectos-store";

import SentenciaEjemplo from "../data/docs/SentenciaEjemplo.pdf";
import ViewPdfComponent from "components/ViewPdfComponent.vue";

const SentenciasStore = useProyectosStore();

const registros = SentenciasStore.sentencias.filter(
  (row) => row.Estado === "Preautorizado",
);

const showDialogPdf = ref(false);
const textoBuscar = ref("");
const selected = ref([]);
const tableRef = ref(null);
const getBook = (ta) => catTipoAsunto.find((t) => t.shortName === ta).book;

function changeEstado() {
  selected.value.forEach((i) => {
    i.Estado = "Autorizado";
  });
}
const columns = [
  {
    name: "expediente",
    required: true,
    label: "Expediente",
    align: "left",
    field: (row) => row.AsuntoAlias,
    sortable: true,
  },
  {
    name: "Engrose",
    align: "center",
    label: "Engrose",
    field: "FechaAlta",
  },
  {
    name: "Capturo",
    align: "center",
    label: "Capturó",
    field: "FechaAudiencia",
    sortable: true,
  },
  {
    name: "Preautorizo",
    align: "center",
    label: "Preautorizó",
    field: "EmpleadoPreAutoriza",
    sortable: true,
  },
];
</script>
