<template>
  <q-toolbar class="q-gutter-xs q-my-md">
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
      <q-tab no-caps name="libreta" class="q-pl-sm">
        <q-item-label class="text-bold text-h4">
          Libreta de oficios
        </q-item-label>
      </q-tab>
      <q-tab
        no-caps
        :name="tabLibreta.id"
        :label="tabLibreta.asuntoAlias"
        v-for="(tabLibreta, index) in libretaTabStore.tabsLibreta"
        :key="tabLibreta.id"
      >
        <q-btn
          flat
          round
          dense
          size="sm"
          color="negative"
          icon="mdi-close"
          class="q-ml-sm"
          @click="libretaTabStore.delTabExpediente(index)"
        ></q-btn>
      </q-tab>
    </q-tabs>
    <q-space></q-space>
    <q-btn
      label="Crear oficio"
      icon="mdi-plus"
      color="primary"
      class="q-mt-md q-ml-sm"
      @click="
        selectedItem = null;
        selectedOrigen = 'LIBRETA';
        showAgregarPromo = true;
        titlePromocion = 'Crear oficio';
        esEdicion = false;
      "
    >
    </q-btn>
  </q-toolbar>
  <q-tab-panels v-model="tabActive" keep-alive class="bg-blue-grey-1">
    <q-tab-panel name="libreta" class="q-pa-none">
      <q-toolbar class="q-gutter-xs">
        <SelectDateComponent
          title="Fecha de Promoción"
          @update:selectedDate="setSelectedDate"
        ></SelectDateComponent>
        <q-space></q-space>
        <SelectStatusComponent
          :filter="filter.status"
          :listStatus="coloresList"
          @update:filterStatus="setFilterStatus"
        >
        </SelectStatusComponent>
        <q-space></q-space>
        <q-input
          dense
          rounded
          outlined
          class="q-pt-md q-pr-xs"
          bg-color="white"
          v-model="textoBuscar"
          placeholder="Buscar"
          @keyup.enter="buscaEnBD()"
        >
          <template v-slot:append>
            <q-icon
              class="cursor-pointer"
              name="mdi-magnify"
              @click="buscaEnBD()"
            />
          </template>
        </q-input>
      </q-toolbar>
      <q-table
        dense
        wrap-cells
        :class="
          rows.length > 7
            ? 'q-ma-md my-sticky-header-table table-pag-height'
            : 'q-ma-md my-sticky-header-table'
        "
        :rows="rows"
        :columns="columns"
        :filter="filter"
        row-key="index"
        v-model:pagination="pagination"
        :rows-per-page-options="rowsPerPageOptions"
        @request="onRequest"
        rows-per-page-label="Registros por
    página:"
        binary-state-sort
        :loading="loading"
      >
        <template v-slot:loading>
          <q-inner-loading showing color="primary" />
        </template>
        <template #no-data>
          <TablaSinDatos
            :titulo="
              textoBuscar || filter.status !== 0
                ? 'Sin resultados'
                : 'Sin oficios'
            "
            :subTitulo="
              textoBuscar || filter.status !== 0
                ? 'Intenta seleccionar otros criterios para realizar tu filtrado.'
                : 'No hay documentos dentro de las fechas.'
            "
            :icono="
              textoBuscar || filter.status !== 0 ? 'mdi-filter' : 'mdi-file'
            "
          ></TablaSinDatos>
        </template>
        <template v-slot:body="props">
          <q-tr :props="props" :class="getColor(props.row.estado)">
            <q-td style="width: 150px">
              <q-item
                clickable
                class="q-pl-none"
                @click="
                  libretaTabStore.addTabExpediente(props.row);
                  tabActive = props.row.expediente.asuntoNeunId;
                "
              >
                <q-item-section>
                  <q-item-label
                    class="text-bold text-secondary"
                    style="text-decoration: underline"
                  >
                    {{ props.row.noExpediente }}
                  </q-item-label>
                  <q-item-label caption>
                    {{ props.row.tipoAsuntoDescripcion }}
                  </q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td class="text-center">
              {{ props.row.folio }}/{{ props.row.anio }}
            </q-td>

            <q-td>
              <q-item-section>
                <q-item-label>
                  {{ props.row.anexoParteDescripcion || "" }}
                </q-item-label>
                <!-- <q-item-label class="text-secondary" caption>
                  {{ props.row.clasePromoventeDescripcion }}
                </q-item-label> -->
              </q-item-section>
            </q-td>
            <q-td class="text-center">
              <q-btn
                flat
                stack
                :round="props.row.numeroRegistro === 0"
                color="secondary"
                icon="mdi-paperclip"
                @click="visualizaExpediente(props.row)"
                :label="
                  props.row.numeroRegistro === 0 ? '' : props.row.numeroRegistro
                "
              >
                <q-tooltip>Ver oficio</q-tooltip>
              </q-btn>
            </q-td>
            <q-td>
              <q-item-section>
                <q-item-label>
                  {{ props.row.empleadoElimina || "" }}
                </q-item-label>
                <q-item-label class="text-secondary" caption>
                  {{ props.row.fechaElimina }}
                </q-item-label>
              </q-item-section>
            </q-td>
            <q-td>
              <div class="row">
                <div class="col">
                  <q-btn
                    v-if="props.row.anexoStatus !== 0"
                    flat
                    round
                    color="secondary"
                    icon="mdi-file-cancel-outline"
                    @click="
                      selectedItem = props.row;
                      showDialogEliminar = true;
                      titlePromocion = 'Cancelar oficio';
                    "
                  >
                    <q-tooltip> Cancelar oficio </q-tooltip>
                  </q-btn>
                </div>
              </div>
            </q-td>
          </q-tr>
          <q-tr colspan="13" v-show="props.expand" :props="props">
            <q-td colspan="100%">
              <q-table
                flat
                hide-header
                hide-bottom
                :rows="getTableRows(props.row.detalle)"
                :cols="getTableCols(props.row.detalle)"
              ></q-table>
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </q-tab-panel>
    <q-tab-panel
      v-for="tabLibreta in libretaTabStore.tabsLibreta"
      :name="tabLibreta.id"
      :key="tabLibreta.id"
      class="q-pa-none"
    >
      <ExpedientePage :expediente="tabLibreta"></ExpedientePage>
    </q-tab-panel>
  </q-tab-panels>
  <q-dialog v-model="showDetalle" style="min-height: 65vh" full-width>
    <DetallePromocion :model-value="selectedItem"></DetallePromocion>
  </q-dialog>
  <q-dialog persistent v-model="showAgregarPromo" :expediente="selectedItem">
    <AddPromocion
      @cerrar="
        (val) => {
          showAlertCancelarCaptura = val;
          showAgregarPromo = val;
        }
      "
      :title="titlePromocion"
      :origen="selectedOrigen"
      :promocion="selectedItem"
      @add:anexo="
        showAddAnexo = true;
        editarAnexo = false;
      "
      @update:anexo="
        (val) => {
          updateAnexo = val;
          editarAnexo = true;
          showAddAnexo = true;
        }
      "
      :addAnexo="anexo"
      :updateAnexo="updateAnexo"
      :es-editar="esEdicion"
      @refrescar-tabla="setRows"
    ></AddPromocion>
  </q-dialog>
  <q-dialog v-model="showUploadPromociones" persistent>
    <UploadPromociones
      :expediente="selectedItem"
      :multiple="selectedItem === null"
      @refrescar-tabla="setRows"
      @cancelar="showAlertaCancelarCargaMasiva = true"
      @cerrar="showUploadPromociones = false"
    ></UploadPromociones>
  </q-dialog>
  <q-dialog v-model="showDialogPdf" full-height full-width>
    <VerPromociones promocionDocumento :promocion="promocionSeleccionada" />
  </q-dialog>
  <DialogConfirmacion
    v-model="showDialogEliminar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="¿Deseas eliminar la promoción?"
    :subTitulo="`Al dar aceptar se cancelara el oficio ${selectedItem?.folio}/${selectedItem?.anio} del expediente ${selectedItem?.noExpediente} ${selectedItem?.tipoAsuntoDescripcion}.`"
    @aceptar="cancelaOficio"
  ></DialogConfirmacion>
  <DialogConfirmacion
    v-model="showAlertCancelarCaptura"
    titulo="Se perderán los cambios"
    :subTitulo="`Si continua se perderán los cambios que ha realizado, ¿Desea continuar?`"
    @aceptar="showAgregarPromo = false"
  ></DialogConfirmacion>
  <DialogAnexo
    v-if="showAddAnexo"
    v-model="showAddAnexo"
    @add:anexoValue="setAnexo"
    @update:anexoValue="setUpdateAnexo"
    :anexoValue="updateAnexo"
    :esEditar="editarAnexo"
    :es-edicion="esEdicion"
    :promocion="selectedItem"
  ></DialogAnexo>
  <GenerateQr
    @print="generateQrCode = false"
    v-if="generateQrCode"
    v-model="jsonParaQr"
    :descripcion="descripcionQr"
    :es-html="true"
    :auto-print="true"
  ></GenerateQr>
  <DialogConfirmacion
    v-model="showAlertaCancelarCargaMasiva"
    titulo="¿Deseas cancelar la vinculación múltiple?"
    :subTitulo="`Ningún archivo será vinculado`"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    @aceptar="showUploadPromociones = false"
  ></DialogConfirmacion>
</template>

<script setup>
import { date } from "quasar";
import { ref, reactive, onMounted } from "vue";
//import { catTipoAsunto } from "src/data/catalogos";
import { useLibretaStore } from "../stores/libreta-store";
import { loader } from "../../../helpers/loader";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { useLibretaTabStore } from "../stores/libreta-tab-store";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import AddPromocion from "../components/AddPromocion.vue";
import UploadPromociones from "../components/UploadPromociones.vue";
import DetallePromocion from "../components/DetallePromocion.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import VerPromociones from "../components/VerPromociones.vue";
import DialogAnexo from "../components/DialogAnexo.vue";
import GenerateQr from "src/components/GenerateQr.vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import { noty } from "src/helpers/notify";

// const oficioLibre = ref({});
// const showOficioLibre = ref(false);
const libretaTabStore = useLibretaTabStore();
const libretaStore = useLibretaStore();
const catalogosStore = useCatalogosStore();
const usuariosStore = useUsuariosStore();
const showDialogEliminar = ref(false);
const showAddAnexo = ref(false);
const showAlertCancelarCaptura = ref(false);
const showAlertaCancelarCargaMasiva = ref(false);
const titlePromocion = ref("Agregar Promoción");
const esEdicion = ref(false);
const showDialogPdf = ref(false);
const showAgregarPromo = ref(false);
const showUploadPromociones = ref(false);
const showDetalle = ref(false);
const selectedItem = ref({});
const selectedOrigen = ref("Libreta");
const anexo = ref(null);
const updateAnexo = ref(null);
const editarAnexo = ref(false);
const generateQrCode = ref(false);
const jsonParaQr = ref("");
const descripcionQr = ref("");
const tabActive = ref("libreta");

const filter = reactive({
  text: "",
  status: 0,
});
const selectedDate = reactive({
  from: date.formatDate(Date.now(), "DD/MM/YYYY"),
  to: date.formatDate(Date.now(), "DD/MM/YYYY"),
});
function setAnexo(a) {
  anexo.value = a;
}
function setUpdateAnexo(a) {
  updateAnexo.value = a;
}

const coloresList = ref([]);
const getColor = (e) => coloresList.value.find((i) => i.status === e)?.color;
/*PARA BORRAR
  function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      label: "Ver todas",
      number: libretaStore.data.metaDatos.totalPromociones || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-blue-3",
      status: 1,
      label: "Electrónicas",
      number: libretaStore.data.metaDatos.totalSinCaptura || 0,
    },
    {
      color: "bg-yellow-3",
      status: 2,
      label: "Físicas",
      number: libretaStore.data.metaDatos.totalCapturadas || 0,
    },
    {
      color: "bg-green-3",
      status: 4,
      label: "Asignadas",
      number: `${libretaStore.data.metaDatos.enviadasAMesa}  (${
        Math.round(
          (libretaStore.data.metaDatos.enviadasAMesa * 100) /
            libretaStore.data.metaDatos.totalPromociones
        ) || 0
      }%)`,
    },
  ];
}*/
let promocionSeleccionada = {};
let loading = ref(false);
let refrescar = ref(false);
let rows = ref([]);
let textoBuscar = ref("");
let rowsPerPageOptions = ref([5, 7, 10, 15, 20, 25, 50, 0]);

// const getBook = (ta) =>
//   catTipoAsunto.find((t) => t.name === ta)?.shortName || "cm";

function getTableCols() {
  return [
    {
      name: "Propiedad",
      style: "width: 50px",
    },
    { name: "Valor" },
  ];
}

function getTableRows(obj) {
  const prop = Object.keys(obj || []).map((i) => ({
    name: i,
  }));

  return prop.map((i) => ({
    Propiedad: i.name,
    Valor: obj[i.name],
  }));
}

onMounted(async () => {
  try {
    await catalogosStore.obtenerAsuntos();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogosStore.obtenerTipos();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  try {
    await usuariosStore.obtenerSecretarios();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
});

/**
 * Va a al store para obtenerExpediente y mostrarlo
 * @param {*} id asuntoNeunId
 */
async function visualizaExpediente(promocion) {
  loader.show();
  promocionSeleccionada = promocion;
  showDialogPdf.value = true;
  loader.hide();
}

const columns = [
  {
    name: "Expediente",
    label: "Expediente",
    align: "left",
    sortable: true,
    field: (row) => row.Expediente.AsuntoAlias,
    style: "width: 10px",
  },
  {
    name: "Número",
    align: "center",
    label: "Número",
    field: "NumeroRegistro",
    sortable: true,
    style: "width: 126px; ",
  },
  {
    name: "Destinatario",
    align: "left",
    label: "Destinatario",
    field: "ParteDescripcion",
    sortable: true,
  },
  {
    name: "Oficio",
    align: "center",
    label: "Oficio",
    field: "NumeroRegistro",
    sortable: true,
    style: "width: 126px; ",
  },
  {
    name: "Cancelo",
    align: "left",
    label: "Canceló",
    field: "SecretarioUserName",
    sortable: true,
  },
  {
    name: "acciones",
    align: "center",
    label: "Cancelar",
    sortable: false,
  },
];

function setFilterStatus(value) {
  filter.status = value;
  pagination.value.page = 1;
}

function setSelectedDate(value) {
  selectedDate.value = value;
  pagination.value.page = 1;
  setRows();
}
/**
 * va a store para obtener los registros
 */
async function setRows() {
  if (!selectedDate.value) {
    // setHoy();
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
  //loader.show();
  //obtiene las promiciones del api
  try {
    var datePartsFrom = selectedDate.value.from.split("/");
    var dateFrom = new Date(
      +datePartsFrom[2],
      datePartsFrom[1] - 1,
      +datePartsFrom[0],
    );
    var datePartsTo = selectedDate.value.to.split("/");
    var dateTo = new Date(+datePartsTo[2], datePartsTo[1] - 1, +datePartsTo[0]);

    await libretaStore.obtenerLibreta({
      from: date.formatDate(dateFrom, "YYYY-MM-DD"),
      to: date.formatDate(dateTo, "YYYY-MM-DD"),
      status: filter.status,
      text: textoBuscar.value,
      ...pagination.value,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  //setea data en rows
  rows.value = libretaStore.data.data;
  rows.value.forEach((row, index) => {
    row.index = index;
  });
  pagination.value.rowsNumber =
    libretaStore.data.data.length > 0
      ? libretaStore.data.data[0].totalRegistros
      : 0;
  rowsPerPageOptions.value =
    selectedDate.value.from === selectedDate.value.to
      ? [5, 7, 10, 15, 20, 25, 50, 0]
      : [5, 7, 10, 15, 20, 25, 50];
  //PARA BORRAR - setColoresList();
  loading.value = false;
  // loader.hide();
}

const pagination = ref({
  sortBy: "Promoción",
  descending: false,
  page: 0,
  rowsPerPage: 0,
  rowsNumber: 10,
});
/**
 * Busca en el server (Api)
 */
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

async function onRequest(props) {
  pagination.value = props.pagination;
  // if (
  //   selectedDate.value.from === selectedDate.value.to &&
  //   pagination.value.rowsPerPage === 0
  // ) {
  //   return;
  // }
  await setRows();
}
/**
 * Elimina promoción
 */
async function cancelaOficio() {
  let correcto = true;
  const parametros = {
    folio: selectedItem.value.folio,
    anio: selectedItem.value.anio,
  };
  await libretaStore.cancelaOficio(parametros);
  noty.correcto(
    `Se ha eliminado el oficio ${parametros.folio}/${parametros.anio}`,
  );
  correcto = true;

  if (correcto) {
    await setRows();
  }
}
// async function imprimirQR() {
//   const parametros = {
//     asuntoNeunId: selectedItem.value.expediente.asuntoNeunId,
//     origen: selectedItem.value.origen,
//     numeroOrden: selectedItem.value.numeroOrden,
//     yearPromocion: selectedItem.value.yearPromocion,
//     kIdElectronica: selectedItem.value.kIdElectronica,
//     catOrganismoId: selectedItem.value.expediente.catOrganismoId,
//   };
//   try {
//     await libretaStore.detallePromocion(parametros);
//   } catch (error) {
//     manejoErrores.mostrarError(error);
//   }
//   const detalle = libretaStore.promocion;
//   const jsonQr = {
//     Expediente: {
//       AsuntoNeunId: detalle.asuntoNeunId,
//       AsuntoAlias: detalle.expediente,
//       CatTipoAsuntoId: detalle.catTipoAsuntoId,
//       CatTipoAsunto: detalle.catTipoAsunto,
//       TipoProcedimientoId: detalle.tipoProcedimientoId,
//       TipoProcedimiento: detalle.tipoProcedimiento,
//       CatOrganismoId: detalle.catOrganismoId,
//       CatOrganismo: detalle.catOrganismo,
//     },
//     Promocion: {
//       CuadernoId: detalle.cuadernoId,
//       Cuaderno: detalle.cuaderno,
//       FechaPresentacion: date.formatDate(
//         detalle.fechaPresentacion,
//         "DD/MM/YYYY"
//       ),
//       NumeroRegistro: detalle.numeroRegistro,
//       YearPromocion: selectedItem.value.yearPromocion,
//     },
//   };
//   descripcionQr.value = `${detalle.catOrganismo}
//     <br/>
//     ${detalle.expediente}
//     <br/>
//     ${detalle.asuntoNeunId}
//     <br/>
//     ${date.formatDate(detalle.fechaPresentacion, "DD/MM/YYYY")}
//     <br/>
//     ${detalle.horaPresentacion}
//     <br/>
//     ${detalle.secretarioNombre}`;
//   jsonParaQr.value = JSON.stringify(jsonQr);
//   generateQrCode.value = true;
// }

// function configurarParametrosEdicion(row) {
//   showAgregarPromo.value = true;
//   selectedItem.value = row;
//   titlePromocion.value = "Editar promoción";
//   esEdicion.value = true;
// }
</script>

<style lang="css" scoped>
.my-sticky-header-table thead tr th {
  position: sticky;
  z-index: 1;
}

.q-table thead tr th:has(i) {
  min-width: 99px;
}

.my-sticky-header-table thead tr:first-child th {
  top: 0;
  background-color: #fff;
}

.q-dialog__inner--fullwidth > div {
  width: 70% !important;
}

.q-splitter--vertical > .q-splitter__panel {
  height: unset;
}

.q-gutter-x-xs > *,
.q-gutter-xs > * {
  margin-left: 8px;
}

.q-table tbody td {
  font-size: 14px;
}

.q-table--dense .q-table th {
  padding: 16px 8px;
}
</style>
