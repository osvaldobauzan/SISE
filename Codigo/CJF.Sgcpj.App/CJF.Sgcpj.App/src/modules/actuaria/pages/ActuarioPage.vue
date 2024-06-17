<template>
  <q-page class="q-pa-sm">
    <q-toolbar>
      <q-toolbar-title class="text-bold text-h4 text-primary"
        >Actuario</q-toolbar-title
      >
      <q-space></q-space>
      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        icon="mdi-file-arrow-left-right"
        label="Generar Acuses Oficios"
        v-permitido="26"
        @click="verAcusePdf()"
        class="q-px-lg"
      >
      </q-btn>
    </q-toolbar>
    <q-toolbar class="q-gutter-xs">
      <SelectDateComponent
        title="Fecha de ingreso actuaría"
        @update:selectedDate="setSelectedDate"
      >
      </SelectDateComponent>
      <q-space></q-space>
      <SelectStatusComponent
        :filter="filter.status"
        :listStatus="coloresList"
        @update:filterStatus="updateFilterStatus"
      >
      </SelectStatusComponent>
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
    <q-toolbar class="q-py-sm flex-center q-gutter-sm">
      <q-btn
        rounded
        clickable
        v-ripple
        outline
        v-for="actuario in optionsActuario"
        :key="actuario"
        @click="
          actuarioSelected = actuario;
          consultaNotificaciones(actuario);
        "
        :class="{
          'bg-primary text-white': actuarioSelected === actuario,
          'text-primary': actuarioSelected !== actuario,
        }"
      >
        {{ actuario.nombreEmpleado }}
      </q-btn>
      <!-- <q-select
filled
clearable
v-cortarLabel
virtual-scroll-slice-size="100"
class="listActuario"
@input-value="actuarioSelected = null"
@update:model-value="consultaNotificaciones"
:options="optionsActuario"
v-model="actuarioSelected"
label="Selecciona una Actuario *"
option-value="empleadoId"
option-label="nombreEmpleado"
  /> -->
    </q-toolbar>
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
            <div class="col-4 text-right">
              <q-btn
                flat
                no-caps
                unelevated
                color="primary"
                icon="mdi-map-marker-path"
                label="Ver ruta"
                @click="showVerRuta = true"
              ></q-btn>
              <q-btn
                flat
                no-caps
                color="primary"
                icon="mdi-upload-multiple"
                label="Vincular acuse"
                @click="showSubirAcuse = true"
              ></q-btn>
              <q-btn
                flat
                dense
                round
                icon="mdi-close"
                @click="selected = []"
              ></q-btn>
            </div>
          </div>
        </q-card-section>
      </q-card>
    </q-toolbar>
    <div class="row">
      <div class="col">
        <q-table
          flat
          dense
          bordered
          wrap-cells
          binary-state-sort
          class="my-sticky-header-table q-mx-md"
          :rows="listNotificaciones"
          :columns="columns"
          :filter="filter"
          :filter-method="filterTerm"
          :loading="showLoader"
          :rows-per-page-options="[listNotificaciones.length]"
          row-key="PersonaId"
          v-model:pagination="pagination"
          @request="onRequest"
          rows-per-page-label="Registros por página:"
          v-model:selected="selected"
        >
          <template v-slot:loading>
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
              <q-td style="width: 200px">
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
                      icon="mdi-book-open-variant-outline"
                      :text-color="`${getBookColor(
                        props.row.tipoAsunto,
                        props.row.tipoCuaderno,
                      )} `"
                      :color="`${getBookColor(
                        props.row.tipoAsunto,
                        props.row.tipoCuaderno,
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
                      <q-badge
                        :class="`bg-${props.row.tipoCuaderno}`"
                        :label="props.row.tipoCuaderno"
                        v-if="props.row.tipoCuaderno"
                      >
                      </q-badge>
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
              <q-td class="q-pl-none">
                <!-- Direccion -->
                <q-item-section>
                  <q-item-label>{{ props.row.domicilioParte }}</q-item-label>
                </q-item-section>
              </q-td>
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
                <div class="row">
                  <q-btn
                    v-permitido="38"
                    v-if="props.row.estadoId < 3"
                    flat
                    round
                    color="secondary"
                    icon="mdi-upload"
                    @click="
                      selected = [];
                      selected.push(props.row);
                      showSubirAcuse = true;
                    "
                  >
                  </q-btn>
                  <q-btn
                    v-else
                    v-permitido="41"
                    flat
                    round
                    color="secondary"
                    icon="mdi-paperclip"
                    @click="verAcuse(props.row)"
                  >
                    <q-tooltip>Ver acuses notificaciones</q-tooltip>
                  </q-btn>
                </div>
              </q-td>
              <q-td class="text-center">
                <!-- Ruta -->
                <div>
                  <q-btn
                    v-permitido="38"
                    flat
                    round
                    color="secondary"
                    icon="mdi-map-marker-path"
                    @click="showVerRuta = true"
                  >
                    <q-tooltip>Ver ruta</q-tooltip>
                  </q-btn>
                </div>
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
            :key="expediente.no_Exp"
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
                    {{ expediente.no_Exp }}
                  </q-item-label>

                  <q-item-label caption>
                    {{ expediente.tipoAsunto }}
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
    <q-dialog v-model="showAcuerdo" full-height full-width>
      <VerAcuerdo :model-value="selectedAcuerdo"></VerAcuerdo>
    </q-dialog>
    <q-dialog v-model="showSintesisManual">
      <SintesisManual :item="acuerdo"></SintesisManual>
    </q-dialog>
    <q-dialog v-model="showDetalleAcuerdo" full-height full-width>
      <DetalleAcuerdo :item="acuerdo"></DetalleAcuerdo>
    </q-dialog>
    <q-dialog v-model="showAsignarZona">
      <AsignarZona :parte="parteSelected"></AsignarZona>
    </q-dialog>
    <q-dialog v-model="showAsignarNotificaciones" full-height full-width>
      <AsignarNotificaciones :partes="selected"></AsignarNotificaciones>
    </q-dialog>
    <q-dialog v-model="showSubirAcuse">
      <SubirAcuse
        :partes="selected"
        @cerrar="
          () => {
            showSubirAcuse = false;
            setRows();
          }
        "
      >
      </SubirAcuse>
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
    <q-dialog v-model="showDetalleParte">
      <DetalleParte :parte="parteSelected"></DetalleParte>
    </q-dialog>
    <q-dialog v-model="showRutaDetalle">
      <RutaDetalle :parteSelected="parteSelected"></RutaDetalle>
    </q-dialog>
    <q-dialog v-model="showVerRuta">
      <VerRuta :selected="selected"></VerRuta>
    </q-dialog>
    <!-- <q-inner-loading :showing="cargando" color="primary" /> -->
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
import { date } from "quasar";
import { Utils } from "src/helpers/utils";
import { useActuariaStore } from "../stores/actuaria-store";
import { useActuarioTabStore } from "../stores/actuario-tab-store";
import { ref, reactive, onMounted } from "vue";
// Servios
import { manejoErrores } from "src/helpers/manejo-errores";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useActuariaDetalleNotificacionesStore } from "../stores/actuaria-detalle-notificaciones-store";
// Datos
import { catTipoAsunto } from "src/data/catalogos";
import { FiltrosColumnasNotificacionesDatos } from "../data/filtrosColumnasNotificaciones";
// Componentes
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import RutaDetalle from "../components/RutaDetalle.vue";
import DetalleAcuerdo from "../components/DetalleAcuerdo.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import AsignarZona from "../components/AsignarZona.vue";
import SubirAcuse from "../components/SubirAcuse.vue";
import ViewPdfComponent from "components/ViewPdfComponent.vue";
import AsignarNotificaciones from "../components/AsignarNotificaciones.vue";
import SintesisManual from "../components/SintesisManual.vue";
import DetalleParte from "../components/DetalleParte.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import VerRuta from "../components/VerRuta.vue";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";

const loadingPdf = ref(false);
const actuariaNotificacionesStore = useActuariaDetalleNotificacionesStore();
const actuarioTabStore = useActuarioTabStore();
const catalogosStore = useCatalogosStore();
const actuariaStore = useActuariaStore();

const showLoader = ref(false);
const showVerRuta = ref(false);
const showRutaDetalle = ref(false);
const showDetalleParte = ref(false);
const showSintesisManual = ref(false);
const showAsignarNotificaciones = ref(false);
const showDetalleAcuerdo = ref(false);
const showAsignarZona = ref(false);
const showSubirAcuse = ref(false);
const showDialogPdf = ref(false);
const showAcuerdo = ref(false);

const tituloDialogFolio = ref("Ver Oficio");
const nombreArchivo = ref(null);
const tipoArchivo = ref("pdf");
const selected = ref([]);
const parteSelected = ref(null);
const actuarioSelected = ref(null);
const acuerdo = ref(null);
const textoBuscar = ref("");
const coloresList = ref([]);
const listNotificaciones = ref([]);
const optionsActuario = ref([]);
const selectedDate = ref({});
const selectedAcuerdo = ref({});
const selectedItem = ref(null);
const maximizedToggle = ref(false);
const expedientes = ref([]);
const showExpediente = ref(false);

const valoresFiltros = reactive(new FiltrosColumnasNotificacionesDatos());

const getBookColor = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name?.toLowerCase() === ta?.toLowerCase() &&
      t.book?.toLowerCase() === nc,
  )?.shortName || "empty";

onMounted(async () => {
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
    {
      color: "bg-green-2",
      status: "Notificado",
      label: "Notificados",
      number: actuariaStore.notificaciones.filter(
        (i) => i.Estado === "Notificado",
      ).length,
    },
  ];
  consultaActuarios();
});
async function onRequest(props) {
  pagination.value = props.pagination;
  await consultaNotificaciones(actuarioSelected.value);
}

// const getBook = (ta) => catTipoAsunto.find((t) => t.shortName === ta).book;

function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      label: "Ver todo",
      number:
        actuariaNotificacionesStore.actuarioNotificaciones.totalRegistros || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-red-2",
      status: 1,
      label: "Pendientes",
      number:
        actuariaNotificacionesStore.actuarioNotificaciones.metaDatos
          .pendiente || 0,
    },
    {
      color: "bg-yellow-2",
      status: 2,
      label: "En proceso",
      number:
        actuariaNotificacionesStore.actuarioNotificaciones.metaDatos
          .enProceso || 0,
    },
    {
      color: "bg-green-2",
      status: 3,
      label: "Notificados",
      number:
        actuariaNotificacionesStore.actuarioNotificaciones.metaDatos
          .notificados || 0,
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
  rowsNumber: 50,
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
    name: "direccion",
    align: "left",
    label: "Dirección",
    field: "direccionParte",
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
  {
    name: "actions",
    align: "left",
    label: "Acuse",
  },
  {
    name: "ruta",
    align: "center",
    label: "Ruta",
  },
];

async function consultaActuarios() {
  showLoader.value = true;
  try {
    await catalogosStore.getZonas();
    optionsActuario.value = catalogosStore.zonas;
    // const actuariosId = optionsActuario.value.map((obj) => obj.empleadoId);
    // console.log('Actuarios:',  actuariosId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  showLoader.value = false;
}

async function consultaNotificaciones(actuario) {
  listNotificaciones.value = [];
  if (!actuario) {
    return;
  }
  showLoader.value = true;
  try {
    valoresFiltros.filtroActuarioID = actuario.empleadoId;
    const parametros = {
      ...selectedDate.value,
      status: filter.status,
      ...pagination.value,
      text: textoBuscar.value,
      valoresFiltros,
    };
    await actuariaNotificacionesStore.getNotificacionesPorActuario(parametros);
    listNotificaciones.value =
      actuariaNotificacionesStore.actuarioNotificaciones.datos;
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

async function verAcuse(row_noti) {
  const nombreAcuse = row_noti.archivoAcuse;
  const archivoInStore = actuariaStore.acuses.find(
    (acuse) => acuse.nombreArchivo === nombreAcuse,
  );
  if (archivoInStore) {
    nombreArchivo.value = Utils.base64ToUrlObj(archivoInStore.base64);
    showDialogPdf.value = true;
  } else {
    await consultarAcuses(row_noti);
    const rutaArchivo = actuariaStore.acuses.find(
      (acuse) => acuse.nombreArchivo === nombreAcuse,
    );
    nombreArchivo.value = Utils.base64ToUrlObj(rutaArchivo.base64);
    showDialogPdf.value = true;
  }
}

async function consultarAcuses(notificacion) {
  showLoader.value = true;
  try {
    // Tipo 3 -> Acuses
    const params = {
      asuntoNeunId: notificacion.asuntoNeunId,
      anioPromocion: 0,
      numeroOrden: 0,
      tipoModulo: 3,
      origen: 0,
      asuntoDocumentoId: notificacion.asuntoDocumentoId,
      sintesisOrden: notificacion.sintesisOrden,
      nombre: notificacion.archivoAcuse,
    };
    await actuariaStore.obtenerAcusesNas(params);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  showLoader.value = false;
}

async function setSelectedDate(value) {
  selectedDate.value = value;
  pagination.value.page = 1;
  consultaNotificaciones(actuarioSelected.value);
}

async function verAcusePdf() {
  loadingPdf.value = true;
  tipoArchivo.value = "pdf";
  tituloDialogFolio.value = "Acuse";

  try {
    await actuarioTabStore
      .getAcuseOficioPorFecha(
        selectedDate.value,
        actuarioSelected.value.empleadoId,
      )
      .then(() => {
        showDialogPdf.value = true;
        nombreArchivo.value = Utils.base64ToUrlObj(
          actuarioTabStore.acuseOficioPdf.mensaje,
        );
      });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  loadingPdf.value = false;
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
