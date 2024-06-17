<template>
  <q-page class="q-pa-md">
    <q-toolbar>
      <q-toolbar-title class="text-bold text-h4 text-primary"
        >Proyectos</q-toolbar-title
      >
      <q-space></q-space>
      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        label="Reasignar secretario"
        icon="mdi-account"
        v-permitido="71"
        @click="
          showReasignarSecretarios = true;
        "
      />
      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        label="Subir proyecto sin audiencia"
        icon="mdi-upload"
        v-permitido="70"
        @click="
          selectedItem = null;
          showSubirProyecto = true;
        "
      />
    </q-toolbar>
    <q-toolbar>
      <SelectDateComponent
        title="Fecha de proyecto"
        ref="setFecha"
        @update:selectedDate="setSelectedDate"
      >
      </SelectDateComponent>
      <q-space></q-space>
      <SelectStatusComponent
        :filter="filter.status"
        :listStatus="coloresList.filter((color) => color.visible)"
        @update:filterStatus="setFilterStatus"
      >
      </SelectStatusComponent>
      <q-space></q-space>
      <InputSearchTable v-model="textoBuscar" @onSearch="buscaEnBD()" />
    </q-toolbar>
    <div class="row q-mt-sm">
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
          :loading="loading"
          :filter-method="filterTerm"
          row-key="asuntoNeunId"
          v-model:pagination="pagination"
          rows-per-page-label="Registros por página:"
          :rows-per-page-options="rowsPerPageOptions"
          @request="onRequest"
        >
          <template v-slot:header-cell-selector>
            <th v-permitido="71">
            </th>
          </template>
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
            <q-tr :class="getColor(props.row.estadoProyecto)">
              <q-td
                v-permitido="71">
                <q-checkbox
                  v-if="props.row.estadoProyecto !== 6"
                  v-model="props.row.selected"
                />
              </q-td>
              <q-td
                :style="`width: 200px; border-left: 10px solid ${getBookColorHex(
                  props.row.expediente.catTipoAsunto,
                  props.row.expediente.cuaderno,
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
                        props.row.expediente.cuaderno,
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
                              props.row.expediente.cuaderno,
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
                        props.row.expediente.cuaderno
                      "
                    >
                      {{ props.row.expediente.catTipoAsunto }}
                    </q-item-label>
                    <q-item-label>
                      <q-badge
                        :class="`bg-${getBookColor(
                          props.row.expediente.catTipoAsunto,
                          props.row.expediente.cuaderno,
                        )} text-${getBookColor(
                          props.row.expediente.catTipoAsunto,
                          props.row.expediente.cuaderno,
                        )}`"
                        :label="props.row.expediente.cuaderno"
                      >
                      </q-badge>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td align="left">
                <q-btn
                  flat
                  stack
                  color="secondary"
                  icon="mdi-paperclip"
                  v-if="props.row.archivoAudiencia"
                  @click="
                    selectedItem = props.row;
                    title = 'Audiencia/Turno';
                    showDialogPdf = true;
                    mostrarPdf();
                  "
                  :label="
                    date.formatDate(props.row.fechaAudiencia, 'DD/MM/YYYY')
                  "
                >
                </q-btn>
              </q-td>
              <q-td>
                <div v-if="props.row.Transcurridos > 0">
                  <q-item v-if="props.row.Transcurridos > 30">
                    <q-item-section side class="q-pr-sm">
                      <q-icon
                        name="mdi-alert"
                        color="negative"
                        size="xs"
                      ></q-icon>
                    </q-item-section>
                    <q-item-section>
                      <q-item-label class="text-negative">
                        {{ props.row.Transcurridos }} días
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                  <q-item v-else>
                    <q-item-label>
                      {{ props.row.Transcurridos }} días
                    </q-item-label>
                  </q-item>
                </div>
              </q-td>

              <q-td>
                <q-item class="text-center">
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.secretario || "" }}
                    </q-item-label>
                    <q-item-label caption class="text-secondary">
                      Mesa: {{ props.row.mesa }}
                    </q-item-label>
                  </q-item-section>
                  <q-tooltip v-if="props.row.secretario">
                    {{ props.row.secretario }}
                  </q-tooltip>
                </q-item>
              </q-td>
              <q-td align="center">
                <template v-if="props.row.descripcionEstadoProyecto || props.row.estadoProyecto == 1">
                  <q-btn
                    v-permitido="70"
                    flat
                    stack
                    no-caps
                    icon="mdi-upload"
                    color="negative"
                    label="Sin proyecto"
                    v-if="props.row.estadoProyecto == 1"
                    @click="
                      selectedItem = props.row;
                      showSubirProyecto = true;
                    "
                  >
                  </q-btn>
                  <q-btn
                    v-else
                    flat
                    stack
                    color="secondary"
                    icon="mdi-paperclip"
                    @click="
                      selectedItem = props.row;
                      showVerProyecto = true;
                      cancelacion = false;
                      title = `Versión ${props.row.numeroVersionProyecto}`;
                    "
                  >
                    {{
                      date.formatDate(
                        props.row.fechaCargaProyecto,
                        "DD/MM/YYYY",
                      )
                    }}
                    <q-item-label
                      caption
                      no-caps
                      class="text-secondary"
                      v-if="props.row.numeroVersionProyecto > 0"
                      >Versión
                      {{ props.row.numeroVersionProyecto }}</q-item-label
                    >
                  </q-btn>
                </template>
                <template v-else>
                  <q-icon
                    color="secondary"
                    name="mdi-file-lock-outline"
                    size="md"
                  >
                    <q-tooltip
                      >Información confidencial: Acceso restringido</q-tooltip
                    >
                  </q-icon>
                </template>
              </q-td>
              <q-td align="center">
                <template v-if="props.row.descripcionEstadoProyecto || props.row.estadoProyecto == 1">
                  <q-item
                    class="q-pl-none"
                    v-if="props.row.estadoProyecto !== null"
                  >
                    <q-item-section>
                      <q-item-label>
                        {{ props.row.descripcionEstadoProyecto }}</q-item-label
                      >
                      <q-item-label v-if="props.row.SubEstado">
                        {{ props.row.SubEstado }}
                      </q-item-label>
                      <q-item-label caption
                        >{{
                          date.formatDate(
                            props.row.fechaCargaProyecto,
                            "DD/MM/YYYY",
                          )
                        }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </template>
                <template v-else>
                  <q-icon color="secondary" name="mdi-eye-lock" size="md">
                    <q-tooltip
                      >Información confidencial: Acceso restringido</q-tooltip
                    >
                  </q-icon>
                </template>
              </q-td>
              <q-td align="center">
                <template v-if="props.row.descripcionEstadoProyecto || props.row.estadoProyecto == 1">
                  <q-item>
                    <q-item-section>
                      <q-item-label caption>{{
                        props.row.descripcionTipoSentencia
                      }}</q-item-label>
                    </q-item-section>
                  </q-item>
                </template>
                <template v-else>
                  <q-icon color="secondary" name="mdi-eye-lock" size="md">
                    <q-tooltip
                      >Información confidencial: Acceso restringido</q-tooltip
                    >
                  </q-icon>
                </template>
              </q-td>
              <q-td>
                <div class="row">
                  <div class="row">
                    <div class="col-6">
                      <q-btn
                        flat
                        round
                        color="blue"
                        v-permitido="71"
                        v-if="
                          estatusProyecto.Aprobado == props.row.estadoProyecto
                        "
                        icon="mdi-dots-vertical"
                      >
                        <q-menu auto-close>
                          <q-list style="min-width: 100px">
                            <q-item
                              clickable
                              v-ripple
                              @click="
                                selectedItem = props.row;
                                cancelacion = true;
                                selectedItem.estado =
                                  estatusProyecto.ParaRevision;
                                cambiarEstadoProyecto();
                              "
                            >
                              <q-item-section side>
                                <q-icon name="mdi-close" color="secondary" />
                              </q-item-section>
                              <q-item-section>
                                Cancelar aprobación
                              </q-item-section>
                            </q-item>
                          </q-list>
                        </q-menu>
                      </q-btn>
                    </div>
                  </div>
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
          :cuadernoDesc="selectedItem.cuaderno"
        ></ExpedientePage>
      </ModalWindowComponent>
    </q-dialog>
    <q-dialog v-model="showDialogPdf" full-height full-width>
      <ViewPdfComponent
        :nombreArchivo="documentoPDF"
        :titulo="title"
        :showing="!cargando"
      >
        <template v-slot:loading>
          <q-inner-loading :showing="cargando">
            <q-spinner-gears size="50px" color="primary" />
          </q-inner-loading>
        </template>
      </ViewPdfComponent>
    </q-dialog>
    <q-dialog persistent v-model="showSubirProyecto">
      <SubirProyecto
        :item="selectedItem"
        @refrescar-tabla="setRows"
        @cancelar="showAlertaCancelarSubidaProyecto = true"
        @cerrar="showSubirProyecto = false"
      >
      </SubirProyecto>
    </q-dialog>
    <q-dialog v-model="showVerProyecto" full-height full-width>
      <VerProyecto
        :item="selectedItem"
        @refrescar-tabla="setRows"
        :title="title"
        :cancelacion="cancelacion"
      >
      </VerProyecto>
    </q-dialog>
    <q-dialog v-model="showSubirCorrecciones" persistent>
      <CorreccionesTitular
        :item="selectedItem"
        :proyecto-id="selectedItem.proyectoId"
        @refrescar="
          setRows();
          showSubirCorrecciones = false;
        "
        v-on:close-dialog="cerrarDialog"
      >
      </CorreccionesTitular>
    </q-dialog>
    <q-dialog v-model="showReasignarSecretarios">
      <ReasignarSecretarios
        :proyectos="rows"
        @refrescarTabla="
          setRows();
          showReasignarSecretarios = false;
        "
        @cancelar= "
          showReasignarSecretarios = false;
        "
      ></ReasignarSecretarios>
    </q-dialog>
    <DialogConfirmacion
      v-model="showAlertaCancelarSubidaProyecto"
      :titulo="`¿Deseas descartar el proyecto?`"
      :subTitulo="`Se perderá la información capturada.`"
      @aceptar="showSubirProyecto = false"
    >
    </DialogConfirmacion>
  </q-page>
</template>

<script setup>
import { date } from "quasar";
import { noty } from "src/helpers/notify";
import { Utils } from "src/helpers/utils";
import { catTipoAsunto } from "src/data/catalogos";
import { manejoErrores } from "src/helpers/manejo-errores.js";
import { useTramiteStore } from "src/modules/tramite/store/tramite-store";
import { ref, reactive, onMounted } from "vue";
import {
  useProyectosStore,
  estatusProyecto,
} from "../store/proyectos-store.js";
import TablaSinDatos from "components/TablaSinDatos.vue";
import ViewPdfComponent from "components/ViewPdfComponent.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import SubirProyecto from "../components/SubirProyecto.vue";
import VerProyecto from "../components/VerProyecto.vue";
import InputSearchTable from "src/components/InputSearchTable.vue";
import CorreccionesTitular from "../components/CorreccionesTitular.vue";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import ReasignarSecretarios from "../components/ReasignarSecretarios.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";

const showVerProyecto = ref(false);
const showSubirProyecto = ref(false);
const showSubirCorrecciones = ref(false);
const showAlertaCancelarSubidaProyecto = ref(false);
const showReasignarSecretarios = ref(false);
const ProyectosStore = useProyectosStore();
const tramiteStore = useTramiteStore();
const showDialogPdf = ref(false);
const selectedDate = ref({});
const selectedItem = ref({});
const setFecha = ref(null);
const updatingStatus = ref(false);
const tipoDocumento = { WORD: "word", PDF: "pdf" };
const tipoDoc = ref(tipoDocumento.PDF);
const cancelacion = ref(false);
const maximizedToggle = ref(false);
const expedientes = ref([]);
const showExpediente = ref(false);

const title = ref("");
// const sentidoSelected = ref("Sentido");
// const secretarioSelected = ref("Secretario");
// const tipoProyectoSelected = ref("Tipo de Proyecto");

const filter = reactive({
  text: "",
  status: 0,
});

onMounted(async () => {
  selectedDate.value = {
    from: date.formatDate(Date.now(), "DD/MM/YYYY"),
    to: date.formatDate(Date.now(), "DD/MM/YYYY"),
  };
});

async function onRequest(props) {
  pagination.value = props.pagination;
  await setRows();
}

function setSelectedDate(value) {
  selectedDate.value = value;
  pagination.value.page = 1;
  setRows();
}

async function setRows() {
  loading.value = true;

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

  try {
    await ProyectosStore.obtenerProyectos({
      ...selectedDate.value,
      status: filter.status,
      ...pagination.value,
      text: textoBuscar.value,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rows.value = ProyectosStore.data.datos;
  rows.value.forEach((row, index) => {
    row.index = index;
    row.selected = false;
  });
  pagination.value.rowsNumber = ProyectosStore.data.totalRegistros;
  rowsPerPageOptions.value =
    selectedDate.value.from === selectedDate.value.to
      ? [5, 7, 10, 15, 20, 25, 50, 0]
      : [5, 7, 10, 15, 20, 25, 50];
  setColoresList();

  loading.value = false;
}

let loading = ref(false);
let refrescar = ref(false);
let rows = ref([]);
let textoBuscar = ref(ProyectosStore.textoBuscar);
let rowsPerPageOptions = ref([5, 7, 10, 15, 20, 25, 50, 0]);
let documentoPDF = ref("");
let cargando = ref(false);

async function mostrarPdf() {
  cargando.value = true;
  try {
    const parametrosArchivo = {
      tipoModulo: 2,
      asuntoNeunId: selectedItem.value.expediente.asuntoNeunId,
      asuntoDocumentoId: selectedItem.value.asuntoDocumentoId,
    };

    await tramiteStore.obtenerArchivoAcuerdo(parametrosArchivo);
    if (tramiteStore.archivoAcuerdo?.anexos[0]?.guidDocumento) {
      await tramiteStore.obtenerAcuerdoEnBase64(
        tramiteStore.archivoAcuerdo.anexos[0].guidDocumento,
      );
    }

    if (tramiteStore.acuerdoBase64) {
      if (tramiteStore.acuerdoNombre.includes(".pdf")) {
        documentoPDF.value = Utils.base64ToUrlObj(tramiteStore.acuerdoBase64);
        tipoDoc.value = "pdf";
      } else {
        documentoPDF.value = Utils.base64ToBlobWord(tramiteStore.acuerdoBase64);
        tipoDoc.value = "word";
      }
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargando.value = false;
}

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

const pagination = ref({
  rowsPerPage: 0,
  descending: false,
});

/**
 * Busca en el server (Api)
 */
async function buscaEnBD() {
  if (/[0-9]{4}/.test(textoBuscar.value)) {
    filter.status = 0;
    selectedDate.value = {
      from: "01/01/1900",
      to: date.formatDate(Date.now(), "DD/MM/YYYY"),
    };
    setFecha.value.setFecha(selectedDate.value);
  }
  pagination.value.page = 1;
  if (textoBuscar.value?.trim()) {
    refrescar.value = true;
    filter.text = textoBuscar.value;
  } else if (refrescar.value) {
    refrescar.value = false;
    filter.text = textoBuscar.value;
  }
}

async function cambiarEstadoProyecto() {
  updatingStatus.value = true;
  let data = new FormData();

  data.append("asuntoNeunId", selectedItem.value.expediente.asuntoNeunId);
  data.append("proyectoId", selectedItem.value.proyectoId);
  data.append("catOrganismoId", selectedItem.value.expediente.catOrganismoId);
  data.append("estadoId", selectedItem.value.estado);
  data.append("correcciones", "");

  try {
    await ProyectosStore.addCorreccionVersion(data);
  } catch (error) {
    noty.error("Error al subir la versión del proyecto.");
  }
  //emit("refrescar");
  updatingStatus.value = false;
  showVerProyecto.value = true;
  await setRows();
}

function filterTerm(rows, terms, cols, getCellValue) {
  if (terms.status === 0) {
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
function setFilterStatus(value) {
  filter.status = value;
  pagination.value.page = 1;
}

const coloresList = ref([]);
const getColor = (e) =>
  coloresList.value.find((i) => i.id == e)?.color ?? "bg-red-4";
function setColoresList() {
  coloresList.value = [
    {
      id: 0,
      color: "bg-grey-4",
      visible: true,
      status: 0,
      label: "Ver todas",
      number: ProyectosStore.data.metaDatos.totalProyectos || 0,
      icon: "mdi-filter-off",
    },
    {
      id: 1,
      color: "bg-red-2",
      visible: true,
      status: 1,
      label: "Sin proyecto",
      number: ProyectosStore.data.metaDatos.totalSinProyecto || 0,
    },
    {
      id: 2,
      color: "bg-purple-2",
      visible: true,
      status: 2,
      label: "Para revisión",
      number: ProyectosStore.data.metaDatos.totalParaRevision || 0,
    },
    {
      id: -1,
      color: "bg-yellow-2",
      visible: true,
      status: 4,
      label: "Con ajustes",
      number: ProyectosStore.data.metaDatos.totalConAjustes || 0,
    },
    {
      id: 4,
      color: "bg-yellow-2",
      visible: false,
      status: 4,
      label: "Con ajustes de fondo",
      number: ProyectosStore.data.metaDatos.totalConAjustes || 0,
    },
    {
      id: 5,
      color: "bg-yellow-2",
      visible: false,
      status: 5,
      label: "Con ajustes de forma",
      number: ProyectosStore.data.metaDatos.totalConAjustes || 0,
    },
    {
      id: 3,
      color: "bg-orange-2",
      visible: true,
      status: 3,
      label: "No aprobado",
      number: ProyectosStore.data.metaDatos.totalNoAprobado || 0,
    },
    {
      id: 6,
      color: "bg-green-2",
      visible: true,
      status: 5,
      label: "Aprobado",
      number: ProyectosStore.data.metaDatos.totalAprobado || 0,
    },
  ];
}

const columns = [
  {
    name: "selector",
    align: "left",
    label: "",
    sortable: false,
  },
  {
    name: "asuntoAlias",
    align: "left",
    label: "Expediente",
    field: "asuntoAlias",
    sortable: true,
  },
  {
    name: "audiencia",
    align: "left",
    label: "Audiencia/Turno",
    field: "audiencia",
    sortable: true,
  },
  /*
  {
    name: "fechaAudiencia",
    align: "center",
    label: "Fecha de celebración de audiencia",
    field: "fechaAudiencia",
    sortable: true,
  },
  */
  {
    name: "fechaAudiencia",
    align: "left",
    label: "Días transcurridos",
    field: "fechaAudiencia",
    sortable: true,
  },
  {
    name: "secretario",
    align: "center",
    label: "Secretario/Mesa",
    field: "secretario",
    sortable: true,
  },
  {
    name: "fechaCargaProyecto",
    align: "center",
    label: "Proyectos",
    field: "fechaCargaProyecto",
    sortable: true,
  },
  {
    name: "estadoProyecto",
    align: "center",
    label: "Estado del proyecto",
    field: "estadoProyecto",
    sortable: true,
  },
  {
    name: "descripcionSentido",
    align: "center",
    label: "Tipo de sentencia",
    field: "sentidoProyecto",
    sortable: true,
  },
  {
    name: "acciones",
    align: "center",
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

.no-button-aud
  font-weight: 500
  color: var(--q-secondary) !important
  font-size: 14px
</style>
