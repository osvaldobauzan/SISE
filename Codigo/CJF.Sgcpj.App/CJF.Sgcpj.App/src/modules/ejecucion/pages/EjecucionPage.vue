<template>
  <q-page padding>
    <q-toolbar class="q-gutter-sm q-mb-md">
      <q-tabs
        dense
        v-model="tabActive"
        indicator-color="transparent"
        active-color="primary"
        active-class="text-bold"
        class="text-grey-8"
        inline-label
        shrink
        stretch
        outside-arrows
        mobile-arrows
        no-caps
      >
        <q-tab no-caps name="ejecucion" class="q-pl-sm">
          <q-toolbar-title class="text-bold text-h4">Ejecución</q-toolbar-title>
        </q-tab>
        <q-tab
          no-caps
          :name="tabSentencia.id"
          :label="tabSentencia.AsuntoAlias"
          v-for="(tabSentencia, index) in EjecucionTabStore.tabsSentencia"
          :key="tabSentencia.id"
        >
          <q-btn
            flat
            round
            dense
            size="sm"
            color="negative"
            icon="mdi-close"
            class="q-ml-sm"
            @click="
              EjecucionTabStore.delTabExpediente(index);
              tabActive = 'ejecucion';
            "
          ></q-btn>
        </q-tab>
      </q-tabs>
      <q-space></q-space>
      <q-btn
        no-caps
        unelevated
        label="Acuerdo de cumplimiento"
        icon="mdi-upload"
        color="primary"
        @click="
          selectedItem = null;
          showSubirAcuerdoCumplimiento = true;
        "
        v-if="tabActive === 'ejecucion'"
      ></q-btn>
    </q-toolbar>
    <q-tab-panels v-model="tabActive" keep-alive class="bg-blue-grey-1">
      <q-tab-panel name="ejecucion" class="q-pa-none">
        <q-toolbar class="q-gutter-xs q-mb-md">
          <SelectDateComponent title="Fecha"></SelectDateComponent>
          <q-space></q-space>
          <SelectStatusComponent
            :filter="filter.status"
            :listStatus="coloresList"
            @update:filterStatus="updateFilterStatus"
          ></SelectStatusComponent>
          <q-space></q-space>
          <q-input
            dense
            rounded
            outlined
            bg-color="white"
            v-model="filter.text"
            placeholder="Buscar"
          >
            <template v-slot:append>
              <q-icon name="mdi-magnify" />
            </template>
          </q-input>
        </q-toolbar>
        <q-table
          dense
          wrap-cells
          style="height: 70vh"
          class="q-mx-md my-sticky-header-table"
          :rows="dataResp"
          :columns="columns"
          :filter="filter"
          row-key="AsuntoNeunId"
          :filter-method="filterTerm"
          v-model:pagination="pagination"
          rows-per-page-label="Registros por página:"
          binary-state-sort
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
            <q-tr :class="getColor(props.row.Estado)">
              <q-td style="width: 150px">
                <q-item
                  clickable
                  class="q-pl-none"
                  @click="
                    EjecucionTabStore.addTabExpediente(props.row);
                    tabActive = props.row.AsuntoNeunId;
                  "
                >
                  <q-item-section>
                    <q-item-label
                      class="text-bold text-secondary"
                      style="text-decoration: underline"
                    >
                      {{ props.row.Expediente.AsuntoAlias }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ props.row.Expediente.CatTipoAsunto }}
                    </q-item-label>
                    <q-item-label>
                      <q-badge
                        :class="`bg-${getBookColor(
                          props.row.CuadernoNombre,
                        )} text-black`"
                        :label="props.row.CuadernoNombre"
                        v-if="props.row.CuadernoNombre"
                      >
                      </q-badge>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td align="center">
                <q-btn
                  flat
                  stack
                  color="secondary"
                  icon="mdi-paperclip"
                  @click="
                    title = 'Sentencia de ejecutoria';
                    showDialogPdf = true;
                  "
                  :label="
                    date.formatDate(props.row.FechaPresentacion, 'DD/MM/YYYY')
                  "
                >
                </q-btn>
                <q-item-label>Primera instancia</q-item-label>
              </q-td>
              <q-td align="center" style="border-left: rgb(0, 0, 0) solid 1px">
                <q-item
                  v-if="props.row.UnknownDate != ''"
                  @click="
                    selectedItem = props.row;
                    showVerRequerimientos = true;
                  "
                  clickable
                  v-ripple
                >
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.UnknownDate }}
                    </q-item-label>
                    <q-item-label class="underline">
                      {{ props.row.Lugar }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
                <q-item v-else>
                  <q-item-section>
                    <q-btn
                      icon="mdi-alert"
                      size="md"
                      stack
                      text-color="negative"
                      flat
                      no-caps
                      >Sin requerimiento</q-btn
                    >
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td align="center">
                <q-item v-if="props.row.Desahogo != '' && props.row.Desahogo">
                  <q-item-section>
                    <q-btn
                      flat
                      stack
                      color="secondary"
                      icon="mdi-paperclip"
                      @click="
                        title = 'Promoción desahogo del requerimiento';
                        nombreArchivo = './docs/PromocionPrueba.pdf';
                        showDialogPdf = true;
                      "
                      :label="
                        date.formatDate(
                          props.row.FechaPresentacion,
                          'DD/MM/YYYY',
                        )
                      "
                    >
                    </q-btn>
                    <q-item-label>
                      {{ props.row.Desahogo }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
                <q-item v-if="!props.row.Desahogo && props.row.Desahogo != ''">
                  <q-item-section>
                    <q-btn
                      icon="mdi-alert"
                      size="md"
                      stack
                      text-color="negative"
                      flat
                      no-caps
                      >Sin desahogo</q-btn
                    >
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td style="border-left: rgb(0, 0, 0) solid 1px">
                <q-item
                  class="text-center"
                  v-if="props.row.NumeroPromociones != null"
                >
                  <q-item-section>
                    <q-item-label> 13/11/2023 </q-item-label>
                    <q-item-label class="text-secondary">
                      Notificados
                    </q-item-label>
                    <q-item-label class="text-secondary">
                      10 de 10 partes
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td align="center">
                <q-item v-if="props.row.NumeroPromociones === 0">
                  <q-item-section>
                    <q-item-label> Sin promociones </q-item-label>
                  </q-item-section>
                </q-item>
                <q-item v-else>
                  <q-item-section>
                    <q-btn
                      flat
                      no-caps
                      stack
                      color="secondary"
                      icon="mdi-paperclip"
                      @click="
                        selectedItem = props.row;
                        showDesahogoVista = true;
                      "
                      label="5 partes"
                    >
                    </q-btn>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td align="center" style="border-left: rgb(0, 0, 0) solid 1px">
                <q-item
                  v-if="props.row.ConAcuerdo && props.row.ConAcuerdo != null"
                >
                  <q-item-section>
                    <q-btn
                      flat
                      stack
                      color="secondary"
                      icon="mdi-paperclip"
                      @click="
                        title = 'Acuerdo cumplimiento';
                        nombreArchivo = './docs/AcuerdoPrueba.pdf';
                        showDialogPdf = true;
                      "
                      :label="
                        date.formatDate(
                          props.row.FechaPresentacion,
                          'DD/MM/YYYY',
                        )
                      "
                    >
                    </q-btn>
                    <q-item-label>
                      {{ props.row.Estado }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
                <q-item
                  v-if="!props.row.ConAcuerdo && props.row.ConAcuerdo != null"
                >
                  <q-item-section>
                    <q-btn
                      icon="mdi-alert"
                      size="md"
                      stack
                      text-color="negative"
                      flat
                      no-caps
                      @click="
                        selectedItem = props.row;
                        showSubirAcuerdoCumplimiento = true;
                      "
                      >Sin acuerdo</q-btn
                    >
                    <q-item-label> 15 días transcurridos </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </q-tab-panel>
      <q-tab-panel
        v-for="tabSentencia in EjecucionTabStore.tabsSentencia"
        :name="tabSentencia.id"
        :key="tabSentencia.id"
        class="q-pa-none"
      >
        <ExpedientePage :expediente="tabSentencia"></ExpedientePage>
      </q-tab-panel>
    </q-tab-panels>
    <q-dialog v-model="showDialogPdf" full-height full-width>
      <ViewPdfComponent :nombreArchivo="nombreArchivo" :titulo="title" />
    </q-dialog>
    <q-dialog v-model="showVerRequerimientos" full-height full-width>
      <ListaRequerimientos
        :item="selectedItem"
        :title="title"
      ></ListaRequerimientos>
    </q-dialog>
    <q-dialog v-model="showSubirAcuerdoCumplimiento">
      <SubirAcuerdoCumplimiento :tipo-acuerdo="miTipoAcuerdo" :num-expediente="numExpediente" :item="selectedItem" @cerrar="showSubirAcuerdoCumplimiento = false"></SubirAcuerdoCumplimiento>
    </q-dialog>
    <q-dialog v-model="showDesahogoVista" full-height full-width>
      <DesahogoVista :item="selectedItem"></DesahogoVista>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { ref, reactive, onMounted } from "vue";
import { date } from "quasar";
import { catTipoAsunto } from "src/data/catalogos";
import { useEjecucionTabStore } from "../store/ejecucion-tab-store.js";
import { ejecucion } from "../data/ejecucionData";
import ListaRequerimientos from "../components/ListaRequerimientos.vue";
import DesahogoVista from "../components/DesahogoVista.vue";
import ViewPdfComponent from "components/ViewPdfComponent.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import SubirAcuerdoCumplimiento from "../components/SubirAcuerdoCumplimiento.vue";
import { useRoute } from "vue-router";

const route = useRoute();
const dataResp = ref(ejecucion);
const showSubirAcuerdoCumplimiento = ref(false);
const textoBuscar = ref("");
const showDesahogoVista = ref(false);
//const showSubirEngrose = ref(false);
const showVerRequerimientos = ref(false);
//const showSubirProyecto = ref(false);
const EjecucionTabStore = useEjecucionTabStore();
const miTipoAcuerdo = ref("");
const numExpediente = ref("");
const nombreArchivo = ref("./docs/SentenciaEjemplo.pdf");
const showDialogPdf = ref(false);
const selectedDate = ref({});
const selectedItem = ref({});
const tabActive = ref("ejecucion");
const title = ref("");
const filter = reactive({
  text: "",
  status: "Ejecución",
});

onMounted(() => {
  selectedDate.value = {
    from: date.formatDate(Date.now(), "DD/MM/YYYY"),
    to: date.formatDate(Date.now(), "DD/MM/YYYY"),
  };
  const status = coloresList.find((i) => i.label === route.params.status);
  filter.status = status?.status || "Ejecución";
});

const getBookColor = (ta) => catTipoAsunto.find((t) => t.book === ta).shortName;
const getColor = (e) => coloresList.find((i) => i.status.startsWith(e)).color;

const pagination = ref({
  rowsPerPage: 0,
});

function filterTerm(rows, terms, cols, getCellValue) {
  if (terms.status === "Ejecución") {
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
        terms.status === row.Estado.toString() &&
        cols.some(
          (col) =>
            (getCellValue(col, row) + "")
              .toUpperCase()
              .indexOf(terms.text.toUpperCase()) !== -1,
        ),
    );
  }
}
function updateFilterStatus(value) {
  filter.status = value;
}

const coloresList = [
  {
    color: "bg-grey-4",
    status: "Ejecución",
    label: "Ver todas",
    number: ejecucion.length,
    icon: "mdi-filter-off",
  },
  {
    color: "bg-grey-2",
    status: "En cumplimiento",
    label: "En vías de cumplimiento",
    number: ejecucion.filter((e) => e.Estado === "En cumplimiento").length,
    width: 400,
  },
  {
    color: "bg-red-2",
    status: "No cumplida",
    label: "No cumplida",
    number: ejecucion.filter((e) => e.Estado === "No cumplida").length,
  },
  {
    color: "bg-green-2",
    status: "Cumplida",
    label: "Cumplida",
    number: ejecucion.filter((e) => e.Estado === "Cumplida").length,
  },
];

const columns = [
  {
    name: "expediente",
    align: "left",
    label: "Expediente",
    field: "AsuntoAlias",
    sortable: true,
  },
  {
    name: "ejecutoria",
    align: "center",
    label: "Ejecutoria",
    //field: "FechaAudiencia",
    sortable: true,
  },
  {
    name: "requerimiento cumplimiento",
    align: "center",
    label: "Requerimiento cumplimiento",
    //field: "Transcurridos",
    sortable: true,
  },
  {
    name: "desahogo",
    align: "center",
    label: "Desahogo",
    //field: "Secretario",
    sortable: true,
  },
  {
    name: "vista",
    align: "center",
    label: "Vista",
    //field: "FechaAlta",
    sortable: true,
  },
  {
    name: "desahogodos",
    align: "center",
    label: "Desahogo",
    //field: "Estado",
    sortable: true,
  },
  {
    name: "cumplimiento",
    align: "center",
    label: "Cumplimiento",
    //field: "Sentido",
    sortable: true,
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
