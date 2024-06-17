<template>
  <q-page>
    <q-toolbar class="q-gutter-mb q-mb-md">
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
        <q-tab no-caps name="oficialia" class="q-pl-sm">
          <q-item-label class="text-bold text-h4">
            Libreta de oficios
          </q-item-label>
        </q-tab>
        <q-tab
          no-caps
          :name="tabOficialia.id"
          :label="tabOficialia.expediente.asuntoAlias"
          v-for="(tabOficialia, index) in oficialiaTabStore.tabsOficialia"
          :key="tabOficialia.id"
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
              oficialiaTabStore.delTabExpediente(index);
              tabActive = 'oficialia';
            "
          ></q-btn>
        </q-tab>
      </q-tabs>
      <q-space></q-space>
      <q-btn
        no-caps
        unelevated
        label="Agregar promoción"
        icon="mdi-plus"
        color="primary"
        class="q-mt-md q-ml-sm"
        @click="
          selectedItem = null;
          selectedOrigen = 'OFICIALÍA';
          showAgregarPromo = true;
          titlePromocion = 'Agregar promoción';
          esEdicion = false;
        "
        v-if="tabActive === 'oficialia'"
      >
      </q-btn>
      <q-btn
        flat
        dense
        class="q-mt-md q-ml-sm btnWidth"
        icon="mdi-upload"
        color="primary"
        style="border: 2px dashed; border-radius: 7px; width: 60px"
        @click="
          showUploadPromociones = true;
          selectedItem = null;
        "
      >
        <q-tooltip self="top end">Vincular varias promociones</q-tooltip>
      </q-btn>
    </q-toolbar>
    <q-tab-panels v-model="tabActive" keep-alive class="bg-blue-grey-1">
      <q-tab-panel name="oficialia" class="q-pa-none">
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
                  : 'Sin promociones'
              "
              :subTitulo="
                textoBuscar || filter.status !== 0
                  ? 'Intenta seleccionar otros criterios para realizar tu filtrado.'
                  : 'No hay documentos por asignar.'
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
                    oficialiaTabStore.addTabExpediente(props.row);
                    tabActive = props.row.expediente.asuntoNeunId;
                  "
                >
                  <q-item-section>
                    <q-item-label
                      class="text-bold text-secondary"
                      style="text-decoration: underline"
                    >
                      {{ props.row.expediente.asuntoAlias }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ props.row.expediente.catTipoAsunto }}
                    </q-item-label>
                    <q-item-label
                      v-if="
                        props.row.expediente.catTipoAsunto !==
                        props.row.expediente.nombreCorto
                      "
                    >
                      <q-badge
                        :class="`bg-${getBook(
                          props.row.expediente.catTipoAsunto,
                        )} text-black`"
                        :label="props.row.expediente.nombreCorto"
                        v-if="props.row.expediente.nombreCorto"
                      >
                      </q-badge>
                    </q-item-label>
                    <q-item-label>
                      <q-chip
                        outline
                        square
                        dense
                        color="negative"
                        icon="mdi-file-alert"
                        :label="props.row.cuadernoNombre"
                        v-if="
                          props.row.cuadernoNombre &&
                          props.row.cuadernoNombre === 'Sin capturar'
                        "
                      >
                      </q-chip>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td class="text-center">
                <q-btn
                  v-if="props.row.origenPromocion === 0"
                  flat
                  stack
                  :round="props.row.numeroRegistro === 0"
                  color="secondary"
                  icon="mdi-upload"
                  @click="
                    showUploadPromociones = true;
                    selectedItem = props.row;
                  "
                  :label="
                    props.row.NumeroRegistro === 0
                      ? ''
                      : props.row.numeroRegistro
                  "
                >
                  <q-tooltip>Vincular promoción</q-tooltip>
                </q-btn>
                <q-btn
                  v-else-if="!props.row.conArchivo"
                  flat
                  stack
                  no-caps
                  :round="props.row.numeroRegistro === 0"
                  color="negative"
                  icon="mdi-upload"
                  @click="
                    showUploadPromociones = true;
                    selectedItem = props.row;
                    titlePromocion = 'Vincular promoción';
                  "
                >
                  <q-item-label>Sin Archivo</q-item-label>
                  <q-item-label class="text-secondary">{{
                    props.row.numeroRegistro === 0
                      ? ""
                      : props.row.numeroRegistro
                  }}</q-item-label>

                  <q-tooltip>Vincular promoción</q-tooltip>
                </q-btn>
                <q-btn
                  v-else-if="props.row.conArchivo"
                  flat
                  stack
                  :round="props.row.numeroRegistro === 0"
                  color="secondary"
                  icon="mdi-paperclip"
                  @click="visualizaExpediente(props.row)"
                  :label="
                    props.row.numeroRegistro === 0
                      ? ''
                      : props.row.numeroRegistro
                  "
                >
                  <q-tooltip>Ver promoción</q-tooltip>
                </q-btn>
              </q-td>
              <q-td>
                {{ props.row.origenPromocionDescripcion }}
                <q-item-label
                  v-if="props.row.estado === 1"
                  class="text-secondary"
                >
                  {{ props.row.nombreOrigen }}
                </q-item-label>
              </q-td>
              <q-td class="text-center">
                <span class="q-mx-sm">
                  {{
                    date.formatDate(props.row.fechaPresentacion, "DD/MM/YYYY")
                  }}</span
                >
                <br />
                <q-item-label class="text-secondary">
                  {{ date.formatDate(props.row.fechaPresentacion, "HH:mm:ss") }}
                </q-item-label>
              </q-td>
              <q-td>
                <q-item class="text-left">
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.secretarioUserName || "" }}
                    </q-item-label>
                    <q-item-label
                      class="text-secondary"
                      caption
                      v-if="props.row.secretarioUserName"
                    >
                      {{ props.row.mesa }}
                    </q-item-label>
                  </q-item-section>
                  <q-tooltip v-if="props.row.secretarioUserName">
                    {{ props.row.secretarioDescripcion }}
                  </q-tooltip>
                </q-item>
              </q-td>
              <q-td>{{ props.row.tipoContenidoDescripcion }}</q-td>
              <q-td>
                <q-item-section>
                  <q-item-label>
                    {{ props.row.parteDescripcion || "" }}
                  </q-item-label>
                  <q-item-label class="text-secondary" caption>
                    {{ props.row.clasePromoventeDescripcion }}
                  </q-item-label>
                </q-item-section>
              </q-td>
              <q-td>
                <q-item-section>
                  <q-item-label>
                    {{ props.row.usuarioCaptura || "" }}
                  </q-item-label>
                  <q-item-label class="text-secondary" caption>
                    {{ date.formatDate(props.row.fechaCaptura, "DD/MM/YYYY") }}
                  </q-item-label>
                </q-item-section>
              </q-td>
              <q-td>
                <div class="row">
                  <div class="col">
                    <q-btn
                      v-if="props.row.estadoAcuerdo !== 4"
                      flat
                      round
                      color="secondary"
                      icon="mdi-file-edit-outline"
                      @click="
                        showAgregarPromo = true;
                        selectedItem = props.row;
                        titlePromocion = 'Editar promoción';
                      "
                    >
                      <q-tooltip> Editar promoción </q-tooltip>
                    </q-btn>
                    <q-btn
                      flat
                      round
                      color="secondary"
                      icon="mdi-dots-vertical"
                    >
                      <q-menu auto-close>
                        <q-list style="min-width: 100px">
                          <q-item
                            clickable
                            v-ripple
                            v-if="
                              !(
                                props.row.OrigenPromocion === 0 ||
                                props.row.OrigenPromocion === 4 ||
                                props.row.OrigenPromocion === 7
                              )
                            "
                            @click="
                              showDetalle = true;
                              selectedItem = props.row;
                            "
                          >
                            <q-item-section side
                              ><q-icon name="mdi-eye"></q-icon
                            ></q-item-section>
                            <q-item-section>Ver detalle</q-item-section>
                          </q-item>
                          <q-item
                            v-if="props.row.estado === 4"
                            clickable
                            v-ripple
                            @click="
                              selectedItem = props.row;
                              imprimirQR();
                            "
                          >
                            <q-item-section side
                              ><q-icon name="mdi-qrcode"></q-icon
                            ></q-item-section>
                            <q-item-section>Reimprimir QR</q-item-section>
                          </q-item>
                          <q-item
                            v-if="
                              !props.row.catAutorizacionDocumentosId &&
                              props.row.estado !== 1
                            "
                            clickable
                            @click="
                              selectedItem = props.row;
                              showDialogEliminar = true;
                            "
                          >
                            <q-item-section side
                              ><q-icon
                                name="mdi-delete-outline"
                                color="negative"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section>
                              <q-item-label class="text-negative"
                                >Eliminar</q-item-label
                              >
                            </q-item-section>
                          </q-item>
                        </q-list>
                      </q-menu>
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
        v-for="tabOficialia in oficialiaTabStore.tabsOficialia"
        :name="tabOficialia.id"
        :key="tabOficialia.id"
        class="q-pa-none"
      >
        <ExpedientePage :expediente="tabOficialia"></ExpedientePage>
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
      >
      </AddPromocion>
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
      :subTitulo="`Al dar aceptar se eliminará la promoción número ${selectedItem?.numeroRegistro} del expediente ${selectedItem?.expediente?.asuntoAlias} ${selectedItem?.expediente?.catTipoAsunto}.`"
      @aceptar="eliminarPromocion"
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
      :titulo="`Deseas cancelar la vinculación ${
        selectedItem === null ? 'múltiple' : ''
      }`"
      :subTitulo="
        selectedItem === null
          ? `Ningún archivo será vinculado`
          : `El archivo no será vinculado`
      "
      label-btn-cancel="Cancelar"
      label-btn-ok="Aceptar"
      @aceptar="showUploadPromociones = false"
    >
    </DialogConfirmacion>
  </q-page>
</template>

<script setup>
import { date } from "quasar";
import { ref, reactive, onMounted } from "vue";
import { catTipoAsunto } from "src/data/catalogos";
// import { useOficialiaStore } from "../stores/oficialia-store";
import { loader } from "../../../helpers/loader";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
// import { useOficialiaTabStore } from "../stores/oficialia-tab-store";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
// import AddPromocion from "../components/AddPromocion.vue";
// import UploadPromociones from "../components/UploadPromociones.vue";
// import DetallePromocion from "../components/DetallePromocion.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
// import VerPromociones from "../components/VerPromociones.vue";
// import DialogAnexo from "../components/DialogAnexo.vue";
import GenerateQr from "src/components/GenerateQr.vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import { noty } from "src/helpers/notify";

// const oficialiaTabStore = useOficialiaTabStore();
// const oficialiaStore = useOficialiaStore();
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
const selectedOrigen = ref("Oficialía");
const anexo = ref(null);
const updateAnexo = ref(null);
const editarAnexo = ref(false);
const generateQrCode = ref(false);
const jsonParaQr = ref("");
const descripcionQr = ref("");
const tabActive = ref("oficialia");

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
function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      label: "Ver todas",
      number: oficialiaStore.data.metaDatos.totalPromociones || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-blue-3",
      status: 1,
      label: "Electrónicas",
      number: oficialiaStore.data.metaDatos.totalSinCaptura || 0,
    },
    {
      color: "bg-yellow-3",
      status: 2,
      label: "Físicas",
      number: oficialiaStore.data.metaDatos.totalCapturadas || 0,
    },
    {
      color: "bg-green-3",
      status: 4,
      label: "Asignadas",
      number: `${oficialiaStore.data.metaDatos.enviadasAMesa}  (${
        Math.round(
          (oficialiaStore.data.metaDatos.enviadasAMesa * 100) /
            oficialiaStore.data.metaDatos.totalPromociones,
        ) || 0
      }%)`,
    },
  ];
}
let promocionSeleccionada = {};
let loading = ref(false);
let refrescar = ref(false);
let rows = ref([]);
let textoBuscar = ref("");
let rowsPerPageOptions = ref([5, 7, 10, 15, 20, 25, 50, 0]);

const getBook = (ta) =>
  catTipoAsunto.find((t) => t.name === ta)?.shortName || "cm";

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
    name: "Promoción",
    align: "center",
    label: "Promoción",
    field: "NumeroRegistro",
    sortable: true,
    style: "width: 126px; ",
  },
  {
    name: "Origen",
    align: "left",
    label: "Origen",
    field: "OrigenPromocionDescripcion",
    sortable: true,
  },
  {
    name: "Fecha",
    align: "left",
    label: "Presentado",
    field: "FechaPresentacion",
    sortable: true,
  },
  {
    name: "Secretario",
    align: "left",
    label: "Asignado",
    field: "SecretarioUserName",
    sortable: true,
  },
  {
    name: "Contenido",
    align: "left",
    label: "Contenido",
    field: "TipoContenidoDescripcion",
    sortable: true,
  },
  {
    name: "Promovente",
    align: "left",
    label: "Promovente",
    field: "ParteDescripcion",
    sortable: true,
  },
  {
    name: "Capturo",
    align: "left",
    label: "Capturó",
    field: "SecretarioUserName",
    sortable: true,
  },
  {
    name: "acciones",
    align: "center",
    label: "",
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
  // loader.show();
  //obtiene las promiciones del api
  try {
    await oficialiaStore.obtenerPromociones({
      ...selectedDate.value,
      status: filter.status,
      text: textoBuscar.value,
      ...pagination.value,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  //setea data en rows
  rows.value = oficialiaStore.data.datos;
  rows.value.forEach((row, index) => {
    row.index = index;
  });
  pagination.value.rowsNumber = oficialiaStore.data.totalRegistros;
  rowsPerPageOptions.value =
    selectedDate.value.from === selectedDate.value.to
      ? [5, 7, 10, 15, 20, 25, 50, 0]
      : [5, 7, 10, 15, 20, 25, 50];
  setColoresList();
  loading.value = false;
  // loader.hide();
}

const pagination = ref({
  sortBy: "Promoción",
  descending: false,
  page: 1,
  rowsPerPage: 0,
  rowsNumber: 50,
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
async function eliminarPromocion() {
  let correcto = true;
  const parametros = {
    asuntoNeunId: selectedItem.value.expediente.asuntoNeunId,
    yearPromocion: selectedItem.value.yearPromocion,
    numeroOrden: selectedItem.value.numeroOrden,
    numeroPromocion: selectedItem.value.numeroRegistro,
    expediente: selectedItem.value.expediente.asuntoAlias,
    catIdOrganismo: selectedItem.value.expediente.catOrganismoId,
  };
  try {
    await oficialiaStore.eliminarPromocion(parametros);
    noty.correcto(
      `Se ha eliminado la promoción ${parametros.numeroPromocion} del expediente ${parametros.expediente}`,
    );
    correcto = true;
  } catch (error) {
    manejoErrores.mostrarError(error);
    correcto = false;
  }

  if (correcto) {
    await setRows();
  }
}
async function imprimirQR() {
  const parametros = {
    asuntoNeunId: selectedItem.value.expediente.asuntoNeunId,
    origen: selectedItem.value.origen,
    numeroOrden: selectedItem.value.numeroOrden,
    yearPromocion: selectedItem.value.yearPromocion,
    kIdElectronica: selectedItem.value.kIdElectronica,
    catOrganismoId: selectedItem.value.expediente.catOrganismoId,
    esPromocionE: selectedItem.value.esPromocionE,
    estado: selectedItem.value.estado,
  };
  try {
    await oficialiaStore.detallePromocion(parametros);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  const detalle = oficialiaStore.promocion;
  const jsonQr = {
    Expediente: {
      AsuntoNeunId: detalle.asuntoNeunId,
      AsuntoAlias: detalle.expediente,
      CatTipoAsuntoId: detalle.catTipoAsuntoId,
      CatTipoAsunto: detalle.catTipoAsunto,
      TipoProcedimientoId: detalle.tipoProcedimientoId,
      TipoProcedimiento: detalle.tipoProcedimiento,
      CatOrganismoId: detalle.catOrganismoId,
      CatOrganismo: detalle.catOrganismo,
    },
    Promocion: {
      CuadernoId: detalle.cuadernoId,
      Cuaderno: detalle.cuaderno,
      FechaPresentacion: date.formatDate(
        detalle.fechaPresentacion,
        "DD/MM/YYYY",
      ),
      NumeroRegistro: detalle.numeroRegistro,
      YearPromocion: selectedItem.value.yearPromocion,
    },
  };
  descripcionQr.value = `${detalle.catOrganismo}
    <br/>
    ${detalle.expediente}
    <br/>
    ${detalle.asuntoNeunId}
    <br/>
    ${date.formatDate(detalle.fechaPresentacion, "DD/MM/YYYY")}
    <br/>
    ${detalle.horaPresentacion}
    <br/>
    ${detalle.secretarioNombre}`;
  jsonParaQr.value = JSON.stringify(jsonQr);
  generateQrCode.value = true;
}

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
