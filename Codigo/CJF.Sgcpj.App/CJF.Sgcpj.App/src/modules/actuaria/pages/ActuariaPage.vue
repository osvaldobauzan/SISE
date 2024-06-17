<template>
  <q-page class="q-pa-sm">
    <q-tabs
      v-model="tab"
      dense
      class="text-grey"
      active-color="primary"
      indicator-color="primary"
      narrow-indicator
    >
      <q-toolbar>
        <q-tab no-caps name="actuaria" label="">
          <q-toolbar-title class="text-bold text-h4 text-primary">
            Actuaría
          </q-toolbar-title>
        </q-tab>
        <q-tab :dense="true" name="lootro" label="">
          <q-toolbar-title class="text-bold text-h6 text-primary">
            Reporte de notificaciones pendientes
          </q-toolbar-title>
        </q-tab>
        <q-space></q-space>
        <q-btn
          no-caps
          unelevated
          icon="mdi-file-arrow-left-right"
          color="primary"
          @click="showRecibirOficios = true"
          v-permitido="26"
          label="Recibir oficios"
          class="q-px-lg"
        >
        </q-btn>
        <q-btn
          outline
          no-caps
          icon="mdi-format-list-bulleted"
          color="primary"
          @click="ShowListaAcuerdos = true"
          v-permitido="27"
          label="Lista de acuerdos"
          class="q-ml-sm q-px-lg"
        >
        </q-btn>
      </q-toolbar>
    </q-tabs>
    <q-tab-panels v-model="tab" animated class="bg-blue-grey-1">
      <q-tab-panel name="actuaria">
        <q-toolbar class="q-gutter-xs">
          <SelectDateComponent
            title="Fecha de ingreso actuaría"
            @update:selectedDate="setSelectedDate"
          ></SelectDateComponent>
          <q-space></q-space>
          <SelectStatusComponent
            :filter="filter.status"
            :listStatus="coloresList"
            @update:filterStatus="setFilterStatus"
          ></SelectStatusComponent>
          <q-space></q-space>
          <InputSearchTable v-model="textoBuscar" @onSearch="buscaEnBD()" />
        </q-toolbar>
        <FiltrosColumnas @cambio-filtro="cambioFiltro" />
        <div class="row q-mt-">
          <div class="col">
            <q-table
              flat
              dense
              bordered
              wrap-cells
              binary-state-sort
              class="my-sticky-header-table q-mx-md"
              :rows="rows"
              :columns="columns"
              :filter="filter"
              row-key="index"
              v-model:pagination="pagination"
              :rows-per-page-options="rowsPerPageOptions"
              @request="onRequest"
              rows-per-page-label="Registros por página:"
              :loading="loading"
              loading-label="Cargando..."
            >
              <template v-slot:loading>
                <q-inner-loading showing color="primary" />
              </template>
              <template #no-data>
                <TablaSinDatos
                  :titulo="
                    textoBuscar ||
                    filter.status !== 'Acuerdos' ||
                    (selectedDate.from == selectedDate.to &&
                      selectedDate.from !=
                        date.formatDate(new Date(), 'DD/MM/YYYY')) ||
                    selectedDate.from != selectedDate.to
                      ? 'Sin resultados'
                      : 'Sin acuerdos'
                  "
                  :subTitulo="
                    textoBuscar ||
                    filter.status !== 'Acuerdos' ||
                    (selectedDate.from == selectedDate.to &&
                      selectedDate.from !=
                        date.formatDate(new Date(), 'DD/MM/YYYY')) ||
                    selectedDate.from != selectedDate.to
                      ? 'Intenta seleccionar otros criterios para realizar tu filtrado.'
                      : 'No hay documentos.'
                  "
                  :icono="
                    textoBuscar || filter.status !== 'Acuerdos'
                      ? 'mdi-filter'
                      : 'mdi-file'
                  "
                ></TablaSinDatos>
              </template>
              <template v-slot:body="props">
                <q-tr :class="getColor(props.row)">
                  <q-td
                    :style="`width: 200px; border-left: 10px solid ${getBookColorHex(
                      props.row.expediente.catTipoAsunto,
                      props.row.tipoCuadernoDesc,
                    )}`"
                  >
                    <q-item
                      v-ripple
                      clickable
                      class="q-pa-none"
                      @click="
                        selectedItem = props.row.expediente;
                        maximizedToggle = false;
                        expedientes.push(props.row.expediente);
                        showExpediente = true;
                      "
                    >
                      <q-item-section side>
                        <q-icon
                          size="md"
                          :color="`${getBookColor(
                            props.row.expediente.catTipoAsunto,
                            props.row.tipoCuadernoDesc,
                          )}`"
                        >
                          <svg
                            width="24px"
                            height="24px"
                            viewBox="0 0 24 24"
                            fill="none"
                            xmlns="http://www.w3.org/2000/svg"
                          >
                            <path
                              fill-rule="evenodd"
                              clip-rule="evenodd"
                              d="M5.99976 2C4.89519 2 3.99976 2.89543 3.99976 4V20C3.99976 21.1046 4.89519 22 5.99976 22H17.9998C19.1043 22 19.9998 21.1046 19.9998 20V4C19.9998 2.89543 19.1043 2 17.9998 2H5.99976ZM12 10.0001C13.6569 10.0001 15 8.65698 15 7.00012C15 5.34327 13.6569 4.00012 12 4.00012C10.3431 4.00012 9 5.34327 9 7.00012C9 8.65698 10.3431 10.0001 12 10.0001ZM18 12.0001V14.0001H6V12.0001H18ZM13 15.0001H6V17.0001H13V15.0001Z"
                              :fill="
                                getBookColorHex(
                                  props.row.expediente.catTipoAsunto,
                                  props.row.tipoCuadernoDesc,
                                )
                              "
                            />
                          </svg>
                        </q-icon>
                      </q-item-section>
                      <q-item-section>
                        <q-item-label
                          class="text-bold text-secondary"
                          style="text-decoration: underline"
                        >
                          {{ props.row.expediente.asuntoAlias }}
                        </q-item-label>
                        <q-item-label
                          v-if="
                            props.row.expediente.catTipoAsunto !==
                            props.row.tipoCuadernoDesc
                          "
                        >
                          {{ props.row.expediente.catTipoAsunto }}
                        </q-item-label>
                        <q-item-label>
                          <q-badge
                            :class="`bg-${getBookColor(
                              props.row.expediente.catTipoAsunto,
                              props.row.tipoCuadernoDesc,
                            )} text-${getBookColor(
                              props.row.expediente.catTipoAsunto,
                              props.row.tipoCuadernoDesc,
                            )}`"
                            :label="props.row.tipoCuadernoDesc"
                          >
                          </q-badge>
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.expediente.tipoProcedimiento }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td class="q-pl-none">
                    <!-- Acuerdo -->
                    <q-item class="q-pl-none">
                      <q-item-section side>
                        <q-btn
                          flat
                          round
                          color="secondary"
                          icon="mdi-paperclip"
                          @click="verAcuerdo(props.row)"
                          v-if="props.row.nombreArchivo !== null"
                          v-permitido="32"
                        >
                          <q-tooltip>Ver acuerdo</q-tooltip>
                        </q-btn>
                      </q-item-section>
                      <q-item-section>
                        <q-item-label> {{ props.row.contenido }}</q-item-label>
                        <q-item-label caption class="text-secondary">
                          {{ props.row.fechaAuto_F }}</q-item-label
                        >
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-item-label>
                      {{
                        date.formatDate(props.row.fechaAutoriza, "DD/MM/YYYY")
                      }}
                    </q-item-label>
                    <q-item-label caption class="text-secondary">
                      {{
                        date.formatDate(props.row.fechaAutoriza, "HH:mm") +
                        " hrs"
                      }}
                    </q-item-label>
                  </q-td>
                  <q-td v-if="props.row.transcurrido > 1"
                    >{{ props.row.transcurrido }} días
                  </q-td>
                  <q-td v-else>{{ props.row.transcurrido }} día </q-td>
                  <q-td>
                    <q-item-label>Notificados</q-item-label>
                    <q-item-label class="text-secondary">
                      {{ props.row.conAcuse }} de
                      {{ props.row.notificados }}</q-item-label
                    ></q-td
                  >
                  <q-td> {{ props.row.estado }} </q-td>
                  <q-td style="max-width: 35rem">
                    <q-btn
                      dense
                      flat
                      no-caps
                      class="q-px-md"
                      icon="mdi-upload"
                      label="Sin síntesis"
                      color="negative"
                      @click="
                        selectedItem = props.row;
                        ShowAcuerdoSintesis = true;
                      "
                      v-if="!props.row.sintesis"
                      v-permitido="33"
                    ></q-btn>
                    <q-item
                      v-else
                      clickable
                      v-ripple
                      v-permitido="34"
                      @click="
                        selectedItem = props.row;
                        ShowAcuerdoSintesis = true;
                      "
                    >
                      {{ props.row.sintesis.substring(0, 150) }}
                      {{ props.row.sintesis.length > 150 ? "..." : "" }}
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-btn
                      v-permitido="35"
                      flat
                      round
                      color="secondary"
                      icon="mdi-eye"
                      @click="
                        selectedItem = props.row;
                        showDetalleNotficaciones = true;
                      "
                    >
                      <q-tooltip>Ver detalle</q-tooltip>
                    </q-btn>
                  </q-td>
                </q-tr>
              </template>
            </q-table>
          </div>
          <div class="col-1" v-if="expedientes.length > 0">
            <q-list class="q-gutter-y-xs">
              <q-card
                flat
                bordered
                v-for="(expediente, index) in expedientes"
                :key="expediente.asuntoAlias"
                class=""
              >
                <q-card-section class="q-pa-none">
                  <q-btn
                    flat
                    dense
                    round
                    size="sm"
                    color="negative"
                    icon="mdi-close"
                    style="
                      position: absolute;
                      top: 0px;
                      right: 0px;
                      margin: 2px;
                      z-index: 1;
                    "
                    @click="expedientes.splice(index, 1)"
                  ></q-btn>

                  <q-item
                    clickable
                    v-ripple
                    @click="
                      selectedItem = expediente;
                      showExpediente = true;
                      maximizedToggle = false;
                    "
                  >
                    <q-item-section>
                      <q-item-label
                        class="text-bold text-secondary"
                        style="text-decoration: underline"
                      >
                        {{ expediente.asuntoAlias }}
                      </q-item-label>

                      <q-item-label caption>
                        {{ expediente.catTipoAsunto }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </q-card-section>
              </q-card>
            </q-list>
          </div>
        </div>
        <q-dialog v-model="showExpediente" :maximized="maximizedToggle">
          <ModalWindowComponent
            :maximizedToggle="maximizedToggle"
            @toggle-maximized="maximizedToggle = !maximizedToggle"
          >
            <ExpedientePage
              :asuntoNeunId="selectedItem.asuntoNeunId"
              :asuntoAlias="selectedItem.asuntoAlias"
              :tipoAsunto="selectedItem.catTipoAsunto"
              :cuadernoDesc="selectedItem.nombreCorto"
            ></ExpedientePage>
          </ModalWindowComponent>
        </q-dialog>
        <q-dialog v-model="showDialogPdf" full-height full-width>
          <VerAcuerdo :model-value="selectedItem"></VerAcuerdo>
        </q-dialog>
        <q-dialog
          v-model="ShowAcuerdoSintesis"
          full-height
          full-width
          persistent
        >
          <AcuerdoSintesis
            @cerrar="
              async (val) => {
                if (val) await setRows();
                ShowAcuerdoSintesis = false;
              }
            "
            :item="selectedItem"
            :title="
              !selectedItem.sintesis ? 'Crear síntesis' : 'Editar síntesis'
            "
          ></AcuerdoSintesis>
        </q-dialog>
        <q-dialog v-model="showDetalleNotficaciones" full-height full-width>
          <ActuariaPartes :acuerdo="selectedItem"></ActuariaPartes>
        </q-dialog>
      </q-tab-panel>
      <q-tab-panel name="lootro">
        <NotificacionesPendientes
          :selected-date-inicial="selectedDate"
        ></NotificacionesPendientes>
      </q-tab-panel>
    </q-tab-panels>
    <q-dialog v-model="ShowListaAcuerdos" full-height full-width>
      <ListaAcuerdos :date-range="selectedDate"></ListaAcuerdos>
    </q-dialog>
    <q-dialog v-model="showRecibirOficios" full-height full-width persistent>
      <RecibirOficios
        @addOficio="(n) => actuariaTabStore.addTabAcuerdo(n)"
        @cerrar="showRecibirOficios = false"
      >
      </RecibirOficios>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { date } from "quasar";
import { catTipoAsunto } from "src/data/catalogos";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useActuariaStore } from "../stores/actuaria-store";
import { useActuariaTabStore } from "../stores/actuaria-tab-store";
import { FiltrosColumnasDatos } from "../data/filtrosColumnas";
import { ref, reactive, computed, onBeforeUnmount } from "vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import AcuerdoSintesis from "../components/AcuerdoSintesis.vue";
import ActuariaPartes from "../components/ActuariaPartes.vue";
import ListaAcuerdos from "../components/ListaAcuerdos.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import RecibirOficios from "../components/RecibirOficios.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import FiltrosColumnas from "../components/FiltrosColumnas.vue";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
import InputSearchTable from "src/components/InputSearchTable.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";
import NotificacionesPendientes from "../components/NotificacionesPendientes.vue";
const showDetalleNotficaciones = ref(false);
const actuariaStore = useActuariaStore();
const actuariaTabStore = useActuariaTabStore();
const showRecibirOficios = ref(false);
const ShowListaAcuerdos = ref(false);
const ShowAcuerdoSintesis = ref(false);
const showDialogPdf = ref(false);
const selectedDate = ref({});
const selectedItem = ref({});
const loading = ref(false);
const rows = ref([]);
let refrescar = ref(false);
let textoBuscar = ref("");
const maximizedToggle = ref(false);
const expedientes = ref([]);
const showExpediente = ref(false);
const tab = ref("actuaria");

const valoresFiltros = reactive(new FiltrosColumnasDatos());
let rowsPerPageOptions = ref([5, 7, 10, 15, 20, 25, 50, 0]);

const getBookColor = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name?.toLowerCase() === ta?.toLowerCase() &&
      t.book?.toLowerCase() === nc?.toLowerCase(),
  )?.shortName || "empty";

const getBookColorHex = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name?.toLowerCase() === ta?.toLowerCase() &&
      t.book?.toLowerCase() === nc?.toLowerCase(),
  )?.color || "empty";

const coloresList = computed(() => {
  return [
    {
      color: "bg-grey-4",
      status: "todo",
      label: "Ver todo",
      number: actuariaStore.dataActuaria.metaDatos?.totalNotificaciones || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-green-2",
      status: "Notificados",
      label: "Notificados",
      number: actuariaStore.dataActuaria.metaDatos?.totalNotificados || 0,
    },
    {
      color: "bg-red-2",
      status: ">=3",
      label: "+3 días",
      number: actuariaStore.dataActuaria.metaDatos?.totalMasTresDias || 0,
    },
    {
      color: "bg-orange-2",
      status: "===2",
      label: "2 días",
      number: actuariaStore.dataActuaria.metaDatos?.totalDosDias || 0,
    },
    {
      color: "bg-yellow-2",
      status: "<=1",
      label: "1 día",
      number: actuariaStore.dataActuaria.metaDatos?.totalUnDia || 0,
    },
  ];
});
const filter = reactive({
  text: "",
  status: "Acuerdos",
  tipoFiltro: 0,
});

async function verAcuerdo(row) {
  selectedItem.value = row;
  showDialogPdf.value = true;
}

async function setRows() {
  if (!selectedDate.value) {
    return;
  } else if (selectedDate.value && !selectedDate.value.from) {
    const fecha = `${selectedDate.value.split("/")[1]}/${
      selectedDate.value.split("/")[0]
    }/${selectedDate.value.split("/")[2]}`;
    selectedDate.value = {
      from: date.formatDate(Date.parse(fecha), "DD/MM/YYYY"),
      to: date.formatDate(Date.parse(fecha), "DD/MM/YYYY"),
    };
    return;
  }
  pagination.value.rowsPerPage =
    pagination.value.rowsPerPage === 0 &&
    selectedDate.value.from !== selectedDate.value.to
      ? 50
      : pagination.value.rowsPerPage;

  loading.value = true;
  try {
    await actuariaStore.getAcuerdos({
      ...selectedDate.value,
      typeFilter: filter.tipoFiltro,
      text: textoBuscar.value,
      ...pagination.value,
      valoresFiltros,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rows.value = actuariaStore.dataActuaria.datos || [];
  rows.value.forEach((row, index) => {
    row.index = index;
  });
  pagination.value.rowsNumber = actuariaStore.dataActuaria.totalRegistros;
  rowsPerPageOptions.value = [actuariaStore.dataActuaria.totalRegistros];
  loading.value = false;
}

const getColor = (e) => {
  return coloresList.value.find((i) => evalTranscurridos(e, i.status))?.color;
};

function evalTranscurridos(e, status) {
  if (status === "Notificados") {
    return e.estado === "Notificados";
  } else if (status === ">=3") {
    return e.transcurrido >= 3;
  } else if (status === "===2") {
    return e.transcurrido === 2;
  } else if (status === "<=1") {
    return e.transcurrido <= 1;
  }
}

rows.value.forEach((row, index) => {
  row.index = index;
});
const pagination = ref({
  sortBy: "IngresoActuaria",
  descending: true,
  page: 1,
  rowsPerPage: 0,
  rowsNumber: 50,
});

function setFilterStatus(value) {
  filter.tipoFiltro = tipoFiltroFunction(value);
  filter.status = value;
  pagination.value.page = 1;
}

function tipoFiltroFunction(status) {
  const statusMap = {
    Acuerdos: 0,
    "===2": 1,
    ">=3": 2,
    "<=1": 3,
    Notificados: 4,
  };

  return statusMap[status] !== undefined ? statusMap[status] : 0;
}

const columns = [
  {
    name: "Expediente",
    align: "left",
    label: "Expediente",
    field: "Expediente",
    sortable: true,
  },
  {
    name: "Acuerdo",
    label: "Acuerdo",
    align: "left",
    field: "Archivo",
    sortable: true,
  },
  {
    name: "IngresoActuaria",
    align: "left",
    label: "Ingreso actuaría",
    field: "FechaAcuerdo",
    sortable: true,
  },
  {
    name: "Transcurrido",
    align: "left",
    label: "Transcurrido",
    field: "PorVencer",
    sortable: true,
  },
  {
    name: "Partes",
    align: "left",
    label: "Partes",
    field: "Partes",
    sortable: true,
  },
  {
    name: "Estado",
    align: "left",
    label: "Estado",
    field: "Estatus",
    sortable: true,
  },
  {
    name: "Sintesis",
    align: "left",
    label: "Síntesis",
    field: "Sintesis",
    sortable: false,
  },
  {
    name: "ver",
    align: "center",
    label: "",
  },
];

async function cambioFiltro(seleccionado) {
  Object.assign(valoresFiltros, seleccionado);
  await setRows();
}

async function onRequest(props) {
  pagination.value = props.pagination;
  await setRows();
}

async function setSelectedDate(value) {
  selectedDate.value = value;
  pagination.value.page = 1;
  await setRows();
}

async function buscaEnBD() {
  pagination.value.page = 1;
  if (textoBuscar.value?.trim()) {
    refrescar.value = true;
    filter.text = textoBuscar.value;
  } else if (refrescar.value) {
    refrescar.value = false;
    filter.text = textoBuscar.value;
  }
}
onBeforeUnmount(() => {
  actuariaTabStore.setTabActive("actuaria");
});
</script>

<style lang="sass">
.my-sticky-header-table
  thead tr th
    position: sticky
    z-index: 1
  thead tr th:has(i)
    min-width:121px
  thead tr:first-child th
    top: 0
    background-color: #fff
</style>
