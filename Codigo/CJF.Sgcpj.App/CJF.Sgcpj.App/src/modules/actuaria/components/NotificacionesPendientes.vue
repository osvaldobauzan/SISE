<template>
  <q-page padding>
    <q-toolbar class="q-gutter-sm q-mb-md">
      <q-tabs
        v-model="actuarioTabStore.tabActive"
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
        <q-tab no-caps name="actuario" class="q-pl-sm"> </q-tab>
        <q-tab
          no-caps
          :name="tabAcuerdo.id"
          :label="tabAcuerdo.Expediente"
          v-for="(tabAcuerdo, index) in actuarioTabStore.tabsAcuerdos"
          :key="tabAcuerdoKey(index)"
        >
          <q-btn
            flat
            round
            dense
            size="sm"
            color="negative"
            icon="mdi-close"
            class="q-ml-sm"
            @click="actuarioTabStore.delTabExpediente(index)"
          ></q-btn>
        </q-tab>
      </q-tabs>
    </q-toolbar>
    <q-tab-panels
      v-model="actuarioTabStore.tabActive"
      keep-alive
      class="bg-blue-grey-1"
    >
      <q-tab-panel name="actuario" class="q-pa-none">
        <q-toolbar class="q-gutter-xs q-pb-md">
          <SelectDateComponent
            title="Periodo del Reporte"
            @update:selectedDate="setSelectedDate"
          ></SelectDateComponent>
          <q-space></q-space>
          <SelectStatusComponent
            :filter="filter.status"
            :listStatus="coloresList"
            @update:filterStatus="updateFilterStatus"
          >
          </SelectStatusComponent>
          <q-select
            filled
            clearable
            v-cortarLabel
            virtual-scroll-slice-size="100"
            class="listActuario"
            @input-value="actuarioSelected = null"
            :options="optionsActuario"
            v-model="actuarioSelected"
            @update:model-value="consultaNotificaciones"
            label="Selecciona una Actuario *"
            option-value="empleadoId"
            option-label="nombreEmpleado"
          />
          <q-space></q-space>
          <q-input
            dense
            rounded
            outlined
            widht="200px"
            bg-color="white"
            v-model="filter.text"
            placeholder="Buscar"
          >
            <template v-slot:append>
              <q-icon name="mdi-magnify" />
            </template>
          </q-input>
        </q-toolbar>
        <q-toolbar class="q-pb-md"> </q-toolbar>
        <q-toolbar v-if="selected.length > 0" class="q-mb-xs">
          <q-card flat bordered style="width: 100%">
            <q-card-section class="q-pa-xs">
              <div class="row">
                <div class="col">
                  <q-chip v-for="chip in selected.slice(0, 3)" :key="chip">
                    {{ chip.Parte }}
                  </q-chip>
                  <q-chip v-if="selected.length > 3">
                    + {{ selected.length - 3 }}</q-chip
                  >
                </div>
                <div class="col-4 text-right"></div>
              </div>
            </q-card-section>
          </q-card>
        </q-toolbar>
        <q-table
          dense
          bordered
          wrap-cells
          style="height: 65vh"
          class="q-mx-md my-sticky-header-table"
          :rows="listNotificaciones"
          :columns="columns"
          :filter="filter"
          :filter-method="filterTerm"
          :loading="showLoader"
          :rows-per-page-options="[listNotificaciones.length]"
          row-key="PersonaId"
          v-model:pagination="pagination"
          @request="onRequest"
          loading-label="Cargando..."
          no-data-label="No se encontraron registros"
          no-results-label="No se encontraron registros"
          rows-per-page-label="Registros por página:"
          v-model:selected="selected"
        >
          <template v-slot:showLoader>
            <q-inner-loading showing color="primary" />
          </template>
          <template #no-data>
            <TablaSinDatos
              :titulo="
                textoBuscar || filter.status !== 0
                  ? 'Sin resultados'
                  : 'Sin acuerdos'
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
            <q-tr :class="getColor(props.row.estadoId)">
              <!-- <q-td style="width: 170px" :class="`bg-${getBookColor(
                          props.row.tipoAsunto,
                          props.row.tipoCuadernoDesc
                        )}`" class="text-black">

                  <q-item
                    clickable
                    class="q-pa-none"
                  >
                    <q-item-section>
                      <q-item-label
                      class="text-bold text-secondary"
                      style="text-decoration: underline"
                    >
                    {{ props.row.no_Exp }}
                  </q-item-label>

                      <q-item-label caption>
                        {{ props.row.tipoAsunto }}
                      </q-item-label>
                      <q-item-label>
                        <q-badge
                          :class="`bg-${props.row.NombreCortoCuaderno} text-black`"
                          :label="getBook(props.row.NombreCortoCuaderno)"
                          v-if="props.row.NombreCortoCuaderno"
                        >
                        </q-badge>
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </q-td> -->
              <q-item
                v-ripple
                clickable
                class="q-pa-none"
                @click="
                  selectedItem = props.row;
                  maximizedToggle = false;
                  expedientes.push(props.row);
                  showExpediente = true;
                "
              >
                <q-item-section avatar>
                  <q-avatar
                    icon="mdi-book-open-page-variant"
                    :text-color="`${getBookColor(
                      props.row.tipoAsunto,
                      props.row.tipoCuadernoDesc,
                    )} `"
                    :color="`${getBookColor(
                      props.row.tipoAsunto,
                      props.row.tipoCuadernoDesc,
                    )} `"
                  ></q-avatar>
                </q-item-section>
                <q-item-section>
                  <q-item-label
                    class="text-bold text-secondary"
                    style="text-decoration: underline"
                  >
                    {{ props.row.no_Exp }}
                  </q-item-label>
                  <q-item-label caption>
                    {{ props.row.tipoAsunto }}
                  </q-item-label>
                  <q-item-label>
                    <!-- <q-badge
                        :class="`bg-${props.row.tipoAsunto}`"
                        :label="props.row.tipoCuadernoDesc"
                        v-if="props.row.tipoCuadernoDesc"
                      >
                      </q-badge> -->
                    <!-- <q-badge
                            v-if="
                              props.row.tipoAsunto?.toLowerCase() !=
                              props.row.tipoCuadernoDesc?.toLowerCase()
                            "
                            :class="`bg-${getBookColor(
                              props.row.tipoAsunto,
                              props.row.tipoCuadernoDesc,
                            )}`"
                            :label="props.row.tipoCuadernoDesc"
                            class="text-black"
                          >
                      </q-badge> -->

                    <q-badge
                      :text-color="`${getBookColor(
                        props.row.tipoAsunto,
                        props.row.tipoCuadernoDesc,
                      )} `"
                      :color="`${getBookColor(
                        props.row.tipoAsunto,
                        props.row.tipoCuadernoDesc,
                      )} `"
                      :label="props.row.tipoCuadernoDesc"
                    >
                    </q-badge>
                  </q-item-label>
                </q-item-section>
              </q-item>
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
                    <q-item-section>
                      <q-item-label>Fecha</q-item-label>
                      <q-item-label caption class="text-secondary">
                        {{ props.row.fechaAuto_F }}</q-item-label
                      >
                    </q-item-section>
                  </q-item-section>
                  <!-- <q-item-section>
                      <q-item-label>Fecha</q-item-label>
                      <q-item-label caption class="text-secondary">
                      {{ props.row.fechaAuto_F }}</q-item-label>
                    </q-item-section> -->
                </q-item>
              </q-td>
              <q-td>
                <q-item class="q-pl-none">
                  <q-item-section>
                    <q-item-label>{{ props.row.parte }}</q-item-label>
                    <q-item-label caption class="text-primary">
                      {{ props.row.caracter }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item-section>
                  <q-item-label>{{ props.row.asignadoActuario }}</q-item-label>
                </q-item-section>
              </q-td>
              <q-td v-if="props.row.transcurrido > 1">
                {{ props.row.transcurrido }} días
              </q-td>

              <q-td v-else>{{ props.row.transcurrido }} día</q-td>

              <q-td>
                <q-item class="q-pl-none">
                  <q-item-section>
                    <q-item-label>{{ props.row.estado }}</q-item-label>
                    <q-item-label>
                      {{ date.formatDate(props.row.estadoFecha, "DD/MM/YYYY") }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item
                  v-permitido="43"
                  :clickable="props.row.tipoId === 5 || props.row.tipoId === 11"
                  v-ripple
                  class="q-pl-none q-px-sm"
                  @click="verOficio(props.row)"
                >
                  <q-item-section>
                    <q-item-label
                      v-if="
                        props.row.folio > 0 &&
                        (props.row.tipoId === 5 || props.row.tipoId === 11)
                      "
                      class="text-secondary"
                    >
                      {{ props.row.folio }}
                    </q-item-label>
                    <q-item-label
                      :class="{
                        tipoFolio:
                          props.row.tipoId === 5 || props.row.tipoId === 11,
                      }"
                    >
                      {{ props.row.tipo }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td class="text-center">
                <div class="row"></div>
              </q-td>
              <q-td class="text-center">
                <!-- Ruta -->
                <div></div>
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </q-tab-panel>
      <q-tab-panel
        v-for="tabAcuerdo in actuarioTabStore.tabsAcuerdos"
        :name="tabAcuerdo.id"
        :key="tabAcuerdo.id"
        class="q-pa-none"
      >
        <ExpedientePage :expediente="tabAcuerdo"></ExpedientePage>
      </q-tab-panel>
    </q-tab-panels>
    <q-dialog v-model="showAcuerdo" full-height full-width>
      <VerAcuerdo :model-value="selectedAcuerdo"></VerAcuerdo>
    </q-dialog>

    <q-dialog v-model="showDetalleAcuerdo" full-height full-width>
      <DetalleAcuerdo :item="acuerdo"></DetalleAcuerdo>
    </q-dialog>

    <q-dialog
      v-model="showDialogPdf"
      full-height
      transition-show="scale"
      transition-hide="scale"
      backdrop-filter="blur(4px)"
    >
      <ViewPdfComponent
        :titulo="tituloDialogFolio"
        :nombreArchivo="nombreArchivo"
        :tipo-archivo="tipoArchivo"
      />
    </q-dialog>
  </q-page>

  <q-inner-loading
    :showing="loadingPdf"
    label="Please wait..."
    label-class="text-teal"
    label-style="font-size: 1.1em"
  />
</template>

<script setup>
// Librerias
import { ref, reactive, onMounted } from "vue";
import { date } from "quasar";
import { Utils } from "src/helpers/utils";
import { useActuariaStore } from "../stores/actuaria-store";
import { useActuarioTabStore } from "../stores/actuario-tab-store";
// Componentes
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";

import DetalleAcuerdo from "../components/DetalleAcuerdo.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";

import ViewPdfComponent from "components/ViewPdfComponent.vue";

import TablaSinDatos from "src/components/TablaSinDatos.vue";

import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
// Servios
import { manejoErrores } from "src/helpers/manejo-errores";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useActuariaDetalleNotificacionesStore } from "../stores/actuaria-detalle-notificaciones-store";
// Datos
import { catTipoAsunto } from "src/data/catalogos";
import { FiltrosColumnasNotificacionesDatos } from "../data/filtrosColumnasNotificaciones";

const getBookColor = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name.toLowerCase() === ta.toLowerCase() &&
      t.book.toLowerCase() === nc.toLowerCase(),
  )?.shortName;
const props = defineProps(["selectedDateInicial"]);
const loadingPdf = ref(false);
const actuariaNotificacionesStore = useActuariaDetalleNotificacionesStore();
const actuarioTabStore = useActuarioTabStore();
const catalogosStore = useCatalogosStore();

const actuariaStore = useActuariaStore();

const showLoader = ref(false);

const showDetalleAcuerdo = ref(false);

const showDialogPdf = ref(false);
const showAcuerdo = ref(false);

const tituloDialogFolio = ref("Ver Oficio");
const nombreArchivo = ref(null);
const tipoArchivo = ref("pdf");
const selected = ref([]);

const actuarioSelected = ref(null);
const acuerdo = ref(null);
const textoBuscar = ref("");
const coloresList = ref([]);
const listNotificaciones = ref([]);
const optionsActuario = ref([]);
const optionsActuarioAll = ref([]);
const selectedDate = ref(props.selectedDateInicial);
const selectedAcuerdo = ref({});

const valoresFiltros = reactive(new FiltrosColumnasNotificacionesDatos());

optionsActuarioAll.value = optionsActuario.value.push({
  areaId: 0,
  nombre: "Zona 0",
  empleadoId: 0,
  nombreEmpleado: "Todos",
});

onMounted(async () => {
  consultaNotificacionesInicial();
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: "Acuerdos",
      label: "Ver todo",
      number: actuariaStore.notificaciones.length,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-red-2",
      status: "Pendiente",
      label: "Pendientes",
      number: actuariaStore.notificaciones.filter(
        (i) => i.Estado === "Pendiente",
      ).length,
    },
    {
      color: "bg-yellow-2",
      status: "Citatorio",
      label: "En proceso",
      number: actuariaStore.notificaciones.filter(
        (i) => i.Estado === "Citatorio",
      ).length,
    },
  ];
  consultaActuarios();
});

async function onRequest(props) {
  pagination.value = props.pagination;
  await consultaNotificaciones();
}

//const getBook = (ta) => catTipoAsunto.find((t) => t.shortName === ta).book;

function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      label: "Ver todo",
      number:
        actuariaNotificacionesStore.actuarioNotificaciones.datos.filter(
          (i) => i.estadoId !== 3 && i.asignadoActuario !== null,
        ).length || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-red-2",
      status: 1,
      label: "Pendientes",
      number:
        actuariaNotificacionesStore.actuarioNotificaciones.datos.filter(
          (i) => i.estadoId === 1 && i.asignadoActuario !== null,
        ).length || 0,
    },
    {
      color: "bg-yellow-2",
      status: 2,
      label: "En proceso",
      number:
        actuariaNotificacionesStore.actuarioNotificaciones.datos.filter(
          (i) => i.estadoId === 2 && i.asignadoActuario !== null,
        ).length || 0,
    },
  ];
}

const filter = reactive({
  text: "",
  status: 0,
});

function filterTerm(rows, terms, cols, getCellValue) {
  if (terms.status === "Acuerdos") {
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

const pagination = ref({
  sortBy: "",
  descending: true,
  page: 1,
  rowsPerPage: 0,
  rowsNumber: 100,
});

function updateFilterStatus(value) {
  filter.status = value;
  pagination.value.page = 1;
}

const getColor = (e) => coloresList.value.find((i) => i.status === e)?.color;

const columns = [
  {
    name: "expediente",
    align: "left",
    label: "Expediente",
    field: "no_Exp",
    sortable: true,
  },
  {
    name: "acuerdo",
    align: "left",
    label: "Acuerdo",
    field: "asuntoDocumentoId",
    sortable: false,
  },
  {
    name: "parte",
    align: "left",
    label: "Parte",
    field: "parte",
    sortable: true,
  },
  {
    name: "actuario",
    align: "left",
    label: "Actuario",
    field: "nombreActuario",
    sortable: true,
  },
  {
    name: "transcurrido",
    align: "left",
    label: "Transcurrido",
    field: "diasTranscurridos",
    sortable: true,
  },
  {
    name: "estado",
    align: "left",
    label: "Estado",
    field: "Estado",
    sortable: true,
  },
  {
    name: "tipo",
    label: "Tipo",
    align: "left",
    field: "TipoNotificacion",
    sortable: true,
  },
];

async function consultaActuarios() {
  showLoader.value = true;
  try {
    await catalogosStore.getZonas();
    optionsActuario.value = catalogosStore.zonas;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  showLoader.value = false;
}

async function consultaNotificacionesInicial() {
  listNotificaciones.value = [];
  showLoader.value = true;
  try {
    selectedDate.value = props.selectedDateInicial;
    valoresFiltros.filtroActuarioID = 0;
    const parametros = {
      ...selectedDate.value,
      status: filter.status,
      ...pagination.value,
      text: textoBuscar.value,
      valoresFiltros,
    };
    await actuariaNotificacionesStore.getNotificacionesPorActuario(parametros);
    listNotificaciones.value =
      actuariaNotificacionesStore.actuarioNotificaciones.datos.filter(
        (i) => i.estadoId !== 3 && i.asignadoActuario !== null,
      );
    pagination.value.rowsNumber = listNotificaciones.value.length;
    setColoresList();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  showLoader.value = false;
}

async function consultaNotificaciones() {
  listNotificaciones.value = [];
  showLoader.value = true;
  try {
    if (actuarioSelected.value === null) {
      valoresFiltros.filtroActuarioID = 0;
    } else {
      valoresFiltros.filtroActuarioID = actuarioSelected.value.empleadoId;
    }
    const parametros = {
      ...selectedDate.value,
      status: filter.status,
      ...pagination.value,
      text: textoBuscar.value,
      valoresFiltros,
    };
    await actuariaNotificacionesStore.getNotificacionesPorActuario(parametros);
    listNotificaciones.value =
      actuariaNotificacionesStore.actuarioNotificaciones.datos.filter(
        (i) => i.estadoId !== 3 && i.asignadoActuario !== null,
      );
    pagination.value.rowsNumber = listNotificaciones.value.length;
    setColoresList();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  showLoader.value = false;
}

async function verAcuerdo(row) {
  selectedAcuerdo.value = {
    asuntoDocumentoId: row.asuntoDocumentoId,
    expediente: { asuntoNeunId: row.asuntoNeunId },
  };
  showAcuerdo.value = true;
}

async function verOficio(row_noti) {
  if (row_noti.folio === 0) {
    return;
  }

  showLoader.value = true;
  tituloDialogFolio.value = `Ver Oficio (${row_noti.folio})`;
  nombreArchivo.value = null;
  tipoArchivo.value = null;
  try {
    const parametrosArchivo = {
      Id: row_noti.guid,
    };
    const infoArchivo =
      await actuariaStore.obtenerArchivoB64(parametrosArchivo);
    if (infoArchivo.base64) {
      if (infoArchivo.nombreArchivo.includes(".pdf")) {
        nombreArchivo.value = Utils.base64ToUrlObj(infoArchivo.base64);
        tipoArchivo.value = "pdf";
      } else {
        nombreArchivo.value = Utils.base64ToBlobWord(infoArchivo.base64);
        tipoArchivo.value = "word";
      }
    } else {
      noty.error("No se encontró el archivo");
    }
    showDialogPdf.value = true;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  showLoader.value = false;
}

function tabAcuerdoKey(index) {
  return "tabAcuerdoKey" + index;
}

async function setSelectedDate(value) {
  selectedDate.value = value;
  pagination.value.page = 1;
  consultaNotificaciones();
}
</script>

<style scoped>
.listActuario {
  min-width: 260px;
  height: 50px;
}
.tipoFolio {
  text-decoration: underline;
  color: var(--q-secondary);
  font-weight: bold;
}
</style>
