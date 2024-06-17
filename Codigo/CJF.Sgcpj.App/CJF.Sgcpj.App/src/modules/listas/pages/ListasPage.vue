<template>
  <q-page class="q-pa-sm">
    <q-toolbar>
      <q-toolbar-title class="text-h4 text-bold text-primary"
        >Listas de sesión</q-toolbar-title
      >
      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        label="Crear lista"
        class="q-px-lg"
        icon="mdi-plus"
        @click="showCrearLista = true"
      />
    </q-toolbar>
    <q-toolbar>
      <SelectRangeDateComponent
        title="Fecha de lista"
      ></SelectRangeDateComponent>
      <q-space></q-space>
      <SelectStatusComponent
        :filter="filter.status"
        :listStatus="statusList"
        @update:filterStatus="setFilterStatus"
      >
      </SelectStatusComponent>
      <q-space></q-space>
      <q-input
        dense
        rounded
        outlined
        v-model="filter.text"
        placeholder="Buscar"
        bg-color="white"
      >
        <template v-slot:append>
          <q-icon name="mdi-magnify" />
        </template>
      </q-input>
    </q-toolbar>
    <q-table
      flat
      bordered
      dense
      wrap-cells
      binary-state-sort
      class="my-sticky-header-table q-ma-sm"
      :rows="ListasStore.listas"
      :columns="columns"
      :filter="filter"
      row-key="AsuntoNeunId"
      :filter-method="filterTerm"
      v-model:pagination="pagination"
      rows-per-page-label="Registros por página:"
    >
      <template v-slot:loading>
        <q-inner-loading showing color="primary" />
      </template>

      <template #no-data>
        <TablaSinDatos
          :titulo="
            textoBuscar || filter.status !== 0
              ? 'Sin resultados'
              : 'Sin registros'
          "
          :subTitulo="
            textoBuscar || filter.status !== 0
              ? 'Intenta seleccionar otros criterios para realizar tu filtrado.'
              : 'No hay documentos.'
          "
          :icono="
            textoBuscar || filter.status !== 0 ? 'mdi-filter' : 'mdi-file'
          "
        ></TablaSinDatos>
      </template>

      <template v-slot:body="props">
        <q-tr
          :props="props"
          :class="props.row.publicada ? 'bg-green-2' : 'bg-red-2'"
        >
          <q-td>
            {{ date.formatDate(props.row.fechaLista, "DD/MM/YYYY") }}
          </q-td>
          <q-td>
            {{ date.formatDate(props.row.fechaSesion, "DD/MM/YYYY") }}
          </q-td>
          <q-td>
            <q-btn
              flat
              stack
              no-caps
              :color="props.row.publicada ? 'primary' : 'negative'"
              :icon="props.row.publicada ? 'mdi-paperclip' : 'mdi-alert'"
              :label="
                props.row.publicada
                  ? date.formatDate(props.row.fechaPublicada, 'DD/MM/YYYY')
                  : 'Pendiente'
              "
              @click="showDialogPdf = true"
            ></q-btn>
          </q-td>
          <q-td>
            {{ props.row.listados }}
          </q-td>
          <q-td>
            {{ props.row.primeraVez }}
          </q-td>
          <q-td>
            {{ props.row.previamente }}
          </q-td>
          <q-td>
            <q-item>
              <q-item-section>
                <q-item-label>
                  {{ props.row.tipo }}
                </q-item-label>
                <q-item-label caption>
                  {{ props.row.audiencia }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </q-td>
          <q-td>
            {{ props.row.firma }}
          </q-td>
          <q-td>
            {{ props.row.sentidos }}
          </q-td>
          <q-td>
            <q-btn
              flat
              round
              dense
              icon="mdi-eye"
              color="grey-9"
              @click="detalleLista(props.row)"
            >
              <q-tooltip>Ver detalle</q-tooltip>
            </q-btn>
            <q-btn
              flat
              round
              dense
              icon="mdi-dots-vertical"
              color="grey-9"
              v-if="!props.row.publicada"
            >
              <q-menu auto-close>
                <q-list>
                  <q-item clickable v-ripple>
                    <q-item-section side
                      ><q-icon name="mdi-pencil"></q-icon
                    ></q-item-section>
                    <q-item-section>Editar</q-item-section>
                  </q-item>
                  <q-item clickable v-ripple @click="showPublicarLista = true">
                    <q-item-section side
                      ><q-icon name="mdi-check"></q-icon
                    ></q-item-section>
                    <q-item-section>Publicar</q-item-section>
                  </q-item>
                  <q-item clickable v-ripple @click="quitarDeLista">
                    <q-item-section side
                      ><q-icon name="mdi-close" color="negative"></q-icon
                    ></q-item-section>
                    <q-item-section
                      ><q-item-label class="text-negative"
                        >Eliminar lista</q-item-label
                      ></q-item-section
                    >
                  </q-item>
                </q-list>
              </q-menu>
            </q-btn>
          </q-td>
        </q-tr>
      </template>
    </q-table>

    <q-dialog v-model="showDialogPdf" full-height full-width>
      <ViewPdfComponent
        :nombreArchivo="nombreArchivo"
        titulo="Lista de sesión"
      />
    </q-dialog>
    <q-dialog v-model="showCrearLista">
      <CrearListaComponent></CrearListaComponent>
    </q-dialog>
    <q-dialog v-model="showDetalleLista" full-height full-width>
      <DetalleLista
        :publicada="selectedRow.publicada"
        :fechaSesion="selectedRow.fechaSesion"
        :listados="selectedRow.listados"
      ></DetalleLista>
    </q-dialog>
    <q-dialog v-model="showPublicarLista" full-height full-width>
      <PublicarListaComponent></PublicarListaComponent>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { date, Notify, useQuasar } from "quasar";
import { ref, reactive, onMounted } from "vue";
import { useListasStore } from "../store/listas-store.js";
import TablaSinDatos from "components/TablaSinDatos.vue";
import SelectRangeDateComponent from "src/components/SelectRangeDateComponent.vue";
import CrearListaComponent from "../components/CrearListaComponent.vue";
import DetalleLista from "../components/DetalleListaComponent.vue";
import PublicarListaComponent from "../components/PublicarListaComponent.vue";
import ViewPdfComponent from "src/components/ViewPdfComponent.vue";
import listaRasd from "../docs/LISTA_RASD.pdf";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";

const nombreArchivo = ref(listaRasd);
const ListasStore = useListasStore();
const selectedDate = ref({});
const showCrearLista = ref(false);
const showDetalleLista = ref(false);
const showPublicarLista = ref(false);
const selectedRow = ref({});
const showDialogPdf = ref(false);
const $q = useQuasar();

function quitarDeLista() {
  $q.dialog({
    title: "Eliminar la lista",
    message: "¿Está seguro de eliminar la lista?",
    cancel: true,
  }).onOk(() => {
    Notify.create({
      message: "Lista eliminada correctamente",
      color: "positive",
      position: "top-right",
      icon: "mdi-check",
    });
  });
}

const statusList = ref([
  {
    color: "bg-grey-4",
    color_active: "bg-grey-5",
    color_number: "grey-7",
    label: "Ver todo",
    value: "listas",
    status: "listas",
    number: 3,
  },
  {
    color: "bg-red-2",
    color_active: "bg-red-3",
    color_number: "negative",
    label: "Pendientes",
    value: "pendientes",
    status: false,
    number: 2,
  },
  {
    color: "bg-green-2",
    color_active: "bg-green-3",
    color_number: "positive",
    label: "Publicadas",
    value: "publicadas",
    status: true,
    number: 1,
  },
]);

const filter = reactive({
  text: "",
  status: "listas",
});

function setFilterStatus(value) {
  filter.status = value;
  pagination.value.page = 1;
}

function detalleLista(row) {
  selectedRow.value = row;
  showDetalleLista.value = true;
}

onMounted(() => {
  selectedDate.value = {
    from: date.formatDate(Date.now(), "DD/MM/YYYY"),
    to: date.formatDate(Date.now(), "DD/MM/YYYY"),
  };
});

const pagination = ref({
  rowsPerPage: 0,
});

function filterTerm(rows, terms, cols, getCellValue) {
  if (terms.status === "listas") {
    return rows.filter((row) =>
      cols.some(
        (col) =>
          (getCellValue(col, row) + "")
            .toUpperCase()
            .indexOf(terms.text.toUpperCase()) !== -1,
      ),
    );
  } else {
    return rows.filter(
      (row) =>
        terms.status === row.publicada &&
        cols.some(
          (col) =>
            (getCellValue(col, row) + "")
              .toUpperCase()
              .indexOf(terms.text.toUpperCase()) !== -1,
        ),
    );
  }
}

const columns = [
  {
    name: "fechaLista",
    align: "left",
    label: "Fecha de lista",
    field: "fechaLista",
    sortable: true,
  },
  {
    name: "fechaSesion",
    align: "left",
    label: "Fecha de sesión",
    field: "fechaSesion",
    sortable: true,
  },
  {
    name: "fechaPublicada",
    align: "left",
    label: "Lista de sesión",
    field: "fechaPublicada",
    sortable: true,
  },
  {
    name: "listados",
    align: "left",
    label: "Asuntos listados",
    field: "listados",
    sortable: true,
  },
  {
    name: "primeraVez",
    align: "left",
    label: "Listados por primera vez",
    field: "primeraVez",
    sortable: true,
  },
  {
    name: "previamente",
    align: "left",
    label: "Listados previamente",
    field: "previamente",
    sortable: true,
  },
  {
    name: "tipo",
    align: "left",
    label: "Tipo de sesión",
    field: "tipo",
    sortable: true,
  },
  {
    name: "firma",
    align: "left",
    label: "Firmadas",
    field: "firma",
    sortable: true,
  },
  {
    name: "sentidos",
    align: "left",
    label: "Sentidos",
    field: "sentidos",
    sortable: true,
  },
  {
    name: "acciones",
    align: "left",
    label: "",
    sortable: false,
  },
];
</script>

<style lang="sass">
.my-sticky-header-table
  thead tr th
    position: sticky
    z-index: 1
  thead tr:first-child th
    top: 0
    background-color: #fff
</style>
