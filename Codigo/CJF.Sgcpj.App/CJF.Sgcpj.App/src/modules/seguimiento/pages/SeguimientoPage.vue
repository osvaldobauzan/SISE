<template>
  <q-page padding>
    <q-toolbar class="q-gutter-xs q-mb-lg">
      <q-tabs
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
        <q-tab no-caps name="seguimiento" class="q-pl-sm">
          <q-item-label class="text-bold text-h4">
            Seguimiento de documentos
          </q-item-label>
        </q-tab> </q-tabs
      ><q-space></q-space>
      <q-input
        dense
        rounded
        outlined
        clearable
        v-model="search"
        style="width: 300px"
        bg-color="white"
        placeholder="Escanear QR"
        @keyup.enter.stop="buscarPorTeclado"
      >
        <template v-slot:append>
          <QrScanner
            class="row items-center"
            my-icon="photo_camera"
            @update:qr-content="buscarPorCamara"
          ></QrScanner>
        </template>
      </q-input>
      <q-btn
        class="q-ml-sm"
        rounded
        dense
        icon="file_open"
        text-color="grey"
        color="white"
        @click="showRecibirDocumento = true"
        ><q-tooltip>Recibir</q-tooltip></q-btn
      >
    </q-toolbar>
    <q-tab-panels v-model="tabActive" keep-alive class="bg-blue-grey-1">
      <q-tab-panel name="seguimiento" class="q-pa-none">
        <q-toolbar class="q-gutter-xs">
          <SelectDateComponent
            title="Fecha de recepción"
            @update:selected-date="setDates"
            :isSeguimientoPage="isSeguimientoPage"
          ></SelectDateComponent>
          <q-space></q-space>
          <q-select
            style="width: auto; min-width: 340px"
            v-cortarLabel
            bg-color="white"
            label="Buscar expediente"
            v-model="numExpediente"
            @update:model-value="funcionInput"
            @filter="buscarExpedientePorNumero"
            :loading="buscandoExpedienteEnBD"
            :options="opcionesExpediente"
            option-value="asuntoNeunId"
            use-input
            input-debounce="0"
            hide-dropdown-icon
            rounded
            dense
            outlined
            clearable
          >
            <template
              v-slot:no-option
              v-if="!findData && !buscandoExpedienteEnBD"
            >
              <q-item>
                <q-item-section class="text-red row">
                  <span> <q-icon name="info" /> Sin resultados</span>
                </q-item-section>
              </q-item>
            </template>
            <template v-slot:append>
              <q-item>
                <q-item-section>
                  <q-icon name="mdi-magnify"></q-icon>
                </q-item-section>
              </q-item>
            </template>
            <template v-slot:option="scope">
              <q-item v-bind="scope.itemProps">
                <q-item-section>
                  <q-item-label>{{ scope.opt.expediente }}</q-item-label>
                  <q-item-label caption>{{
                    scope.opt.tipoAsunto
                  }}</q-item-label>
                  <q-item-label
                    class="text-caption"
                    v-if="scope.opt.tipoProcedimiento"
                    >{{ scope.opt.tipoProcedimiento }}</q-item-label
                  >
                </q-item-section>
              </q-item>
            </template>
          </q-select>
        </q-toolbar>

        <q-toolbar>
          <q-btn
            dense
            flat
            no-caps
            color="grey-8"
            icon="mdi-tune"
            label="Filtrado"
            :disable="!foundTableContent"
            @click="filterDrawerOpen = !filterDrawerOpen"
          >
          </q-btn>
          <q-btn
            class="q-ml-md q-px-sm"
            no-caps
            rounded
            dense
            v-model="borrarFiltro"
            label="Borrar filtros"
            color="primary"
            @click="borrarFiltros"
            v-if="tipoDocumento || nombresEmpleados || tipoArea || expediente"
          ></q-btn>
        </q-toolbar>
        <q-table
          dense
          wrap-cells
          virtual-scroll
          :class="
            rows.length > 7
              ? 'q-ma-md my-sticky-header-table table-pag-height'
              : 'q-ma-md my-sticky-header-table'
          "
          :rows="rows.value"
          :columns="columns"
          :filter="filter"
          :filter-method="filterTerm"
          row-key="index"
          v-model:pagination="pagination"
          loading-label="Cargando..."
          no-data-label="No se encontraron registros"
          no-results-label="No se encontraron registros en este momento"
          rows-per-page-label="Registros por página:"
          :no-data="noData"
          :loading="loading"
        >
          <template v-slot:loading>
            <q-inner-loading showing color="primary" />
          </template>
          <template #no-data>
            <TablaSinDatos
              :titulo="
                numExpediente && !loading ? 'Sin resultados' : 'Sin documentos'
              "
              :subTitulo="
                numExpediente && !loading
                  ? 'Intenta seleccionar otros criterios para realizar tu filtrado.'
                  : 'No hay documentos para mostrar.'
              "
              :icono="numExpediente && !loading ? 'mdi-filter' : 'mdi-file'"
            ></TablaSinDatos>
          </template>
          <template v-slot:body="props">
            <q-tr>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ date.formatDate(props.row.fechaHora, "DD/MM/YYYY") }}
                    </q-item-label>
                    <q-item-label caption>
                      {{
                        date.formatDate(props.row.fechaHora, "HH:mm") + " hrs"
                      }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      <q-btn
                        v-if="props.row.documentoId !== ''"
                        flat
                        stack
                        rounded
                        dense
                        color="secondary"
                        icon="mdi-paperclip"
                        @click="
                          setDocumento('prmocion');
                          showDialogPdf = true;
                        "
                        :label="props.row.documentoId"
                      >
                        <q-tooltip> Ver promoción </q-tooltip>
                      </q-btn>
                    </q-item-label>
                    <q-item-label>
                      {{ props.row.tipoDocumento }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.userName }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ props.row.puestoDescripcion }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.area }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label
                      class="text-bold text-secondary"
                      style="text-decoration: underline"
                    >
                      {{ props.row.expediente }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ props.row.tipoAsunto }}
                    </q-item-label>
                    <q-item-label>
                      <q-badge
                        :class="`bg-green text-white`"
                        label="Principal"
                        v-if="props.row.cuaderno === null"
                      >
                      </q-badge>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </q-tab-panel>
    </q-tab-panels>
    <q-drawer
      overlay
      v-model="filterDrawerOpen"
      bordered
      side="right"
      class="bg-white q-ml-sm"
    >
      <div class="row justify-end q-mr-md">
        <q-btn
          rounded
          icon="close"
          dense
          flat
          @click="filterDrawerOpen = !filterDrawerOpen"
        ></q-btn>
      </div>

      <q-toolbar>
        <q-item-label class="text-h5 q-mb-xl text-weight-bold"
          >Filtrado</q-item-label
        >
      </q-toolbar>
      <div class="q-gutter-lg q-mr-md">
        <q-select
          use-input
          input-debounce="0"
          filled
          dense
          use-chips
          multiple
          clearable
          v-model="tipoDocumento"
          :options="opcionesTipoDocumento"
          @filter="filterTipoDocumento"
          label="Tipo de documento"
        >
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-grey">
                Sin resultados
              </q-item-section>
            </q-item>
          </template>
        </q-select>
        <q-select
          use-input
          input-debounce="0"
          dense
          filled
          hide-dropdown-icon
          label="En posesión"
          v-model="nombresEmpleados"
          :option-label="
            (option) =>
              option.userName && option.puestoDescripcion
                ? `${option.userName} - ${option.puestoDescripcion}`
                : !option.userName && !option.puestoDescripcion
                  ? ''
                  : `${option.userName}`
          "
          :options="opcionesNombreEmpleado"
          clearable
          @filter="filterEmpleados"
        >
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.userName }}</q-item-label>
                <q-item-label caption
                  >{{ scope.opt.puestoDescripcion }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </template>
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-grey">
                Sin resultados
              </q-item-section>
            </q-item>
          </template>
        </q-select>
        <q-select
          use-input
          input-debounce="0"
          dense
          filled
          v-model="tipoArea"
          :options="opcionesAreas"
          use-chips
          multiple
          label="Área"
          @filter="filterArea"
        >
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-grey">
                Sin resultados
              </q-item-section>
            </q-item>
          </template>
        </q-select>
        <q-select
          hide-dropdown-icon
          use-input
          input-debounce="0"
          dense
          v-model="expediente"
          label="Número de expediente"
          :options="opcionesExpediente"
          :option-label="
            (option) =>
              option.expediente && option.tipoAsunto
                ? `${option.expediente} - ${option.tipoAsunto}`
                : ''
          "
          clearable
          filled
          @filter="filterExpediente"
        >
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.expediente }}</q-item-label>
                <q-item-label caption>{{ scope.opt.tipoAsunto }} </q-item-label>
              </q-item-section>
            </q-item>
          </template>
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-grey">
                Sin resultados
              </q-item-section>
            </q-item>
          </template>
        </q-select>
      </div>
    </q-drawer>
    <q-dialog v-model="showDialogPdf" full-height full-width>
      <ViewPdfComponent :nombreArchivo="nombreArchivo" />
    </q-dialog>
    <q-dialog v-model="showRecibirDocumento">
      <ReceiveDocument></ReceiveDocument>
    </q-dialog>
  </q-page>
</template>

<script setup>
import { date, useQuasar, Notify } from "quasar";
import { ref, onMounted, reactive } from "vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import ViewPdfComponent from "components/ViewPdfComponent.vue";
import documentoPDF from "src/assets/PromocionPrueba.pdf";
import acuerdoPDF from "src/assets/AcuerdoPrueba.pdf";
import ReceiveDocument from "../components/ReceiveDocument.vue";
import QrScanner from "../components/QrScanner.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import { useSeguimientoStore } from "../stores/seguimiento-store";
import { manejoErrores } from "src/helpers/manejo-errores";

let loading = ref(false);
const buscandoExpedienteEnBD = ref(false);
const findData = ref(true);
const tabActive = ref("seguimiento");
const $q = useQuasar();
const showDialogPdf = ref(false);
const borrarFiltro = ref();
const showRecibirDocumento = ref(false);
const nombresEmpleados = ref("");
const tipoDocumento = ref(null);
const tipoArea = ref(null);
const expediente = ref("");
const isSeguimientoPage = ref(true);
const filtroFecha = ref(false);
const fechaFiltro = ref(date.formatDate(Date.now(), "DD/MM/YYYY"));
const fechaFin = ref(date.formatDate(Date.now(), "DD/MM/YYYY"));
const filterDrawerOpen = ref(false);
const search = ref("");
const QrString = ref("");
const foundTableContent = ref(false);
const hoy = new Date();
const ayer = new Date(hoy);
ayer.setDate(hoy.getDate() - 1);
let rows = ref([]);
const nombreArchivo = ref(documentoPDF);
const pagination = ref({
  rowsPerPage: 6,
});
const seguimientoStore = useSeguimientoStore();
const numExpediente = ref("");

async function buscarExpedienteConTipoAsunto() {
  loading.value = true;
  try {
    await seguimientoStore.mostrarSeguimientoExpediente(
      numExpediente.value.expediente,
      numExpediente.value.tipoAsunto,
      numExpediente.value.tipoProcedimiento,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (seguimientoStore.mostrarExpedienteArray.value.length > 0) {
    rows.value = seguimientoStore.mostrarExpedienteArray;
    foundTableContent.value = true;
  } else {
    foundTableContent.value = false;
  }
  loading.value = false;
}

async function buscarExpedientePorNumero(val, update, abort) {
  update(async () => {
    if (val === "" || val.length <= 5) {
      findData.value = true;
      abort();
      return;
    } else {
      buscandoExpedienteEnBD.value = true;
      try {
        await seguimientoStore.buscarExpediente(val);
      } catch (error) {
        seguimientoStore.expedienteArray = null;
        manejoErrores.mostrarError(error);
      }
      buscandoExpedienteEnBD.value = false;
      if (seguimientoStore.expedienteArray.length > 0) {
        findData.value = true;
      } else {
        findData.value = false;
      }
      opcionesExpediente.value = seguimientoStore.expedienteArray;
      if (opcionesExpediente.value?.length === 1) {
        numExpediente.value = opcionesExpediente.value[0];
        funcionInput();
      }
    }
  });
}

async function funcionInput() {
  if (numExpediente.value != null) {
    await buscarExpedienteConTipoAsunto();
    if (foundTableContent.value) {
      ordenarDatosTabla();
    }
  }
}

function setDocumento(tipo) {
  if (tipo === "acuerdo") {
    nombreArchivo.value = acuerdoPDF;
  } else {
    nombreArchivo.value = documentoPDF;
  }
}

async function pushData() {
  try {
    await seguimientoStore.insertarSeguimiento(QrString.value);
    if (seguimientoStore.InsertaSeguimiento === 1) {
      showNotificationSuccess(
        "Se ha recibido el documento correctamente",
        "positive",
        "top",
      );
    } else {
      showNotificationFailure(
        "Error al intentar recibir el documento",
        "negative",
        "top",
      );
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  } finally {
    search.value = null;
  }
}

function ordenarDatosTabla() {
  const listaAreas = seguimientoStore.mostrarExpedienteArray.value.map(
    ({ area }) => area,
  );
  const listaAreasOrdenadas = listaAreas.sort((a, b) => a.localeCompare(b));
  const setAreas = new Set(listaAreasOrdenadas);
  listaAreasSinRepetir.value = Array.from(setAreas);

  const listaTipoDocumento = seguimientoStore.mostrarExpedienteArray.value.map(
    ({ tipoDocumento }) => tipoDocumento,
  );
  const listaTipoDocimentoOrdenados = listaTipoDocumento.sort((a, b) =>
    a.localeCompare(b),
  );
  const setDocumentos = new Set(listaTipoDocimentoOrdenados);
  listaTipoDocumentoSinRepetir.value = Array.from(setDocumentos);

  const listaNombresEmpleados =
    seguimientoStore.mostrarExpedienteArray.value.map(
      ({ userName, puestoDescripcion }) => ({ userName, puestoDescripcion }),
    );
  const listaEmpleadosOrdenados = listaNombresEmpleados.sort((a, b) =>
    a.userName.localeCompare(b.userName),
  );
  listaEmpleadosSinRepetir.value = Array.from(
    new Set(
      listaEmpleadosOrdenados.map((userName) => JSON.stringify(userName)),
    ),
  ).map((jsonString) => JSON.parse(jsonString));

  const listaExpedientes = seguimientoStore.mostrarExpedienteArray.value.map(
    ({ expediente, tipoAsunto }) => ({ expediente, tipoAsunto }),
  );
  const listaExpedientesOrdenados = listaExpedientes.sort((a, b) =>
    a.expediente.localeCompare(b.expediente),
  );
  listaExpedientesSinRepetir.value = Array.from(
    new Set(
      listaExpedientesOrdenados.map((expediente) => JSON.stringify(expediente)),
    ),
  ).map((jsonString) => JSON.parse(jsonString));
}

const buscarPorTeclado = async () => {
  QrString.value = search.value;
  pushData();
};

function setDates(selectedDate) {
  if (selectedDate.filter) {
    filtroFecha.value = true;
    fechaFiltro.value = selectedDate.from;
    fechaFin.value = selectedDate.to;
  } else {
    filtroFecha.value = false;
    fechaFiltro.value = "";
    fechaFin.value = "";
  }
  //Deshabilitando temporalmente filtro fecha
  filtroFecha.value = false;
  fechaFiltro.value = "";
  fechaFin.value = "";
  formatDate();
}

function formatDate() {
  if (filtroFecha.value) {
    var inicio = fechaFiltro.value.split("/");
    var nuevaFechaInicio = inicio[2] + "-" + inicio[1] + "-" + inicio[0];
    fechaFiltro.value = nuevaFechaInicio;
    var fin = fechaFin.value.split("/");
    var nuevaFechaFin = fin[2] + "-" + fin[1] + "-" + fin[0];
    fechaFin.value = nuevaFechaFin;
  } else {
    fechaFiltro.value = "";
    fechaFin.value = "";
  }
}

onMounted(() => {
  $q.lang.table.allRows = "Todos";
  formatDate();
});

const listaEmpleadosSinRepetir = ref([{}]);
const listaExpedientesSinRepetir = ref([{}]);
const listaAreasSinRepetir = ref([{}]);
const listaTipoDocumentoSinRepetir = ref([{}]);

function filterTerm(rows, terms, cols, getCellValue) {
  return rows.filter((row) => {
    const filter1 =
      (!expediente.value || row.expediente === expediente.value.expediente) &&
      (!nombresEmpleados.value ||
        row.userName === nombresEmpleados.value.userName ||
        row.userName === nombresEmpleados.value.userName) &&
      (!tipoArea.value ||
        tipoArea.value.length === 0 ||
        tipoArea.value.includes(row.area)) &&
      (!tipoDocumento.value ||
        tipoDocumento.value.length === 0 ||
        tipoDocumento.value.includes(row.tipoDocumento));

    const fechaInicioValue = fechaFiltro.value;
    const fechaFinValue = fechaFin.value;
    const filtroFechaHabilitado = filtroFecha.value;
    const fechaRegistro = row.FechaHora;

    const filterFechas =
      !filtroFechaHabilitado ||
      !fechaInicioValue ||
      !fechaFinValue ||
      (new Date(fechaInicioValue) <= new Date(fechaRegistro) &&
        new Date(fechaRegistro) <= new Date(fechaFinValue));

    const filter2 = cols.some(
      (col) =>
        (getCellValue(col, row) + "")
          .toUpperCase()
          .indexOf(terms.text.toUpperCase()) !== -1,
    );

    return filter1 && filter2 && filterFechas;
  });
}

const columns = [
  {
    name: "FechaHora",
    label: "Recibido",
    align: "left",
    field: (row) => date.formatDate(row.FechaHora, "DD/MM/YYYY HH:mm:ss"),
  },
  {
    name: "DocumentoId",
    label: "Documento",
    field: "DocumentoId",
    align: "left",
  },
  {
    name: "Empleado",
    label: "En posesión",
    field: "UserName",
    align: "left",
  },
  { name: "Area", label: "Área", field: "Area", align: "left" },
  {
    name: "expediente",
    required: true,
    label: "Expediente",
    align: "center",
    field: "Expediente",
  },
];

const noData = {
  icon: "warning",
  message: "No se encontraron datos",
};

const filter = reactive({
  text: "",
  fecha: "",
});

//FILTRO POR NOMBRE Y USERNAME DE EMPLEADO
const opcionesNombreEmpleado = ref([]);
function filterEmpleados(val, update) {
  update(() => {
    opcionesNombreEmpleado.value = listaEmpleadosSinRepetir.value;
  });

  update(() => {
    const needle = val.toLowerCase();
    opcionesNombreEmpleado.value = listaEmpleadosSinRepetir.value.filter(
      (v) =>
        v.userName.toLowerCase().includes(needle) ||
        v.puestoDescripcion.toLowerCase().includes(needle),
    );
  });
}

//FILTRO POR EXPEDIENTE
const opcionesExpediente = ref([]);
function filterExpediente(val, update) {
  update(() => {
    opcionesExpediente.value = listaExpedientesSinRepetir.value;
  });

  update(() => {
    const needle = val.toLowerCase();
    opcionesExpediente.value = listaExpedientesSinRepetir.value.filter(
      (v) =>
        v.expediente.toLowerCase().includes(needle) ||
        v.tipoAsunto.toLowerCase().includes(needle),
    );
  });
}

//FILTRO POR AREA
const opcionesAreas = ref([]);
function filterArea(val, update) {
  update(() => {
    opcionesAreas.value = listaAreasSinRepetir;
  });

  update(() => {
    const needle = val.toLowerCase();
    opcionesAreas.value = listaAreasSinRepetir.value.filter((v) =>
      v.toLowerCase().includes(needle),
    );
  });
}

//FILTRO POR TIPO DE DOCUMENTO
const opcionesTipoDocumento = ref([]);
function filterTipoDocumento(val, update) {
  update(() => {
    opcionesTipoDocumento.value = listaTipoDocumentoSinRepetir;
  });

  update(() => {
    const needle = val.toLowerCase();
    opcionesTipoDocumento.value = listaTipoDocumentoSinRepetir.value.filter(
      (v) => v.toLowerCase().includes(needle),
    );
  });
}

//BORRAR FILTROS
function borrarFiltros() {
  tipoDocumento.value = null;
  nombresEmpleados.value = "";
  tipoArea.value = null;
  expediente.value = "";
}

function showNotificationSuccess(message, type, position) {
  Notify.create({
    message: message,
    type: type,
    position: position,
  });
}

function showNotificationFailure(message, type, position) {
  Notify.create({
    message: message,
    type: type,
    position: position,
  });
}
</script>
<style>
.expedientes-hoy {
  font-weight: bold;
  font-size: large;
}

.spinner {
  border: 10px solid rgba(0, 0, 0, 0.1);
  border-left-color: #7983ff;
  border-radius: 50%;
  width: 100px;
  height: 100px;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(360deg);
  }
}
</style>
