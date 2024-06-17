<template>
  <q-card class="bg-blue-grey-1">
    <q-toolbar class="bg-white">
      <q-toolbar-title class="text-bold"
        >Detalle notificaciones</q-toolbar-title
      >
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-toolbar class="q-py-sm flex flex-center">
      <q-card flat bordered>
        <div class="q-gutter-md row">
          <q-item>
            <q-item-section>
              <q-item-label caption>Expediente</q-item-label>
              <q-item-label class="text-h6 text-bold"
                >{{ acuerdo.expediente.asuntoAlias }}
              </q-item-label>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label caption>Tipo de asunto</q-item-label>
              <q-item-label class="text-h6 text-bold">
                {{ acuerdo.expediente.catTipoAsunto }}</q-item-label
              >
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label caption>Cuaderno</q-item-label>
              <q-chip
                :class="`bg-${getBookColor(
                  acuerdo.expediente.catTipoAsunto,
                  acuerdoMetadatos.datosAsunto?.tipoCuaderno,
                )}`"
                class="text-black"
                :label="acuerdoMetadatos.datosAsunto?.tipoCuaderno"
              >
              </q-chip>
            </q-item-section>
          </q-item>
        </div>
      </q-card>
    </q-toolbar>
    <q-toolbar class="q-gutter-xs q-pl-md">
      <q-card flat bordered>
        <q-item class="">
          <q-item-section side>
            <q-btn
              flat
              round
              padding="xs"
              color="grey-9"
              icon="mdi-paperclip"
              @click="showDetalleAcuerdo = true"
              v-permitido="51"
            />
          </q-item-section>
          <q-item-section>
            <q-item-label caption>Acuerdo</q-item-label>
            <q-item-label class="text-bold">{{
              acuerdo.contenido || "Admisión"
            }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-card>
      <q-card flat bordered>
        <q-item class="">          
          <q-item-section>
            <q-item-label caption>Fecha del acuerdo</q-item-label>
            <q-item-label class="text-bold"
              >{{ acuerdo.fechaAuto_F }} </q-item-label
            >
          </q-item-section>
        </q-item>
      </q-card>
      <q-card flat bordered>
        <q-item class="">
          <q-item-section side>
            <q-icon name="mdi-calendar-alert" color="grey-8"></q-icon>
          </q-item-section>
          <q-item-section>
            <q-item-label caption>Transcurrido</q-item-label>
            <q-item-label class="text-bold"
              >{{ acuerdo.transcurrido }} días</q-item-label
            >
          </q-item-section>
        </q-item>
      </q-card>
      <q-space></q-space>
      <SelectStatusComponent
        :filter="filter.status"
        :listStatus="coloresList"
        @update:filterStatus="updateFilterStatus"
      >
      </SelectStatusComponent>
      <q-space></q-space>
      <InputSearchTable v-model="textoBuscar" @onSearch="buscaEnBD()" />
    </q-toolbar>
    <FiltrosColumnas class="q-mb-sm" @cambio-filtro="cambioFiltro" />
    <q-toolbar v-if="selected.length > 0" class="q-pr-md q-pl-md q-pb-xs">
      <q-card flat bordered style="width: 100%">
        <q-card-section class="q-pa-xs">
          <div class="row">
            <div class="col"></div>
            <div class="col-4 text-right">
              <q-btn
                v-permitido="39"
                flat
                no-caps
                color="primary"
                icon="mdi-account-details"
                label="Asignación múltiple"
                @click="showAsignarNotificaciones = true"
              ></q-btn>
              <q-btn
                v-permitido="40"
                flat
                no-caps
                color="primary"
                icon="mdi-upload-multiple"
                label="Vinculación múltiple"
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
    <q-card-section class="q-pa-none">
      <q-table
        dense
        bordered
        wrap-cells
        class="q-mx-md my-sticky-header-table"
        :rows="rows"
        :columns="columns"
        :filter="filter"
        :filter-method="filterTerm"
        row-key="index"
        v-model:pagination="pagination"
        :rows-per-page-options="rowsPerPageOptions"
        @request="onRequest"
        :loading="loading"
        loading-label="Cargando..."
        no-data-label="No se encontraron registros"
        no-results-label="No se encontraron registros"
        rows-per-page-label="Registros por página:"
        selection="multiple"
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
            <q-td auto-width>
              <q-checkbox dense v-model="props.selected" />
            </q-td>
            <q-td>
              <q-item
                v-permitido="42"
                clickable
                v-ripple
                class="q-pl-none"
                @click="
                  parteSelected = props.row;
                  showDetalleParte = true;
                "
              >
                <q-item-section>
                  <q-item-label>{{ props.row.parte }}</q-item-label>
                  <q-item-label caption class="text-secondary">{{
                    props.row.caracter
                  }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td>
              <q-item class="q-pl-none">
                <q-item-section>
                  <q-item-label>{{ props.row.estado }}</q-item-label>
                  <q-item-label v-if="props.row.estadoId != 1">
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
            <q-td>
              <q-item class="q-pl-none" v-if="props.row.asignoPersona">
                <q-item-section>
                  <q-item-label>{{ props.row.asignoPersona }}</q-item-label>
                  <q-item-label class="text-secondary" caption>{{
                    date.formatDate(props.row.asignoFecha, "DD/MM/YYYY")
                  }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td>
              <q-item
                v-permitido="37"
                clickable
                v-ripple
                class="q-pl-none"
                @click="
                  parteSelected = props.row;
                  showAsignarZona = true;
                "
                v-if="props.row.asignadoActuario"
              >
                <q-item-section>
                  <q-item-label>{{ props.row.asignadoActuario }}</q-item-label>
                  <q-item-label class="text-secondary" caption>{{
                    props.row.asignadoZona
                  }}</q-item-label>
                </q-item-section>
              </q-item>
              <div v-else>
                <q-btn
                  v-permitido="36"
                  flat
                  dense
                  no-caps
                  icon="mdi-upload"
                  label="Sin asignar"
                  color="negative"
                  @click="
                    parteSelected = props.row;
                    showAsignarZona = true;
                  "
                ></q-btn>
              </div>
            </q-td>
            <q-td>
              <div v-if="props.row.estadoId < 3">
                <q-btn
                  v-permitido="38"
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
              </div>
              <div v-if="props.row.archivoAcuse !== null">
                <q-btn
                  v-for="(nombreAcuse, index) in props.row.archivosAcuses"
                  :key="index"
                  v-permitido="41"
                  flat
                  round
                  color="secondary"
                  icon="mdi-paperclip"
                  @click="verAcuse(nombreAcuse)"
                >
                  <q-tooltip>Ver acuses notificaciones</q-tooltip>
                </q-btn>
              </div>
            </q-td>
            <q-td class="text-center">
              <q-btn
                v-if="
                  aplicaCOE.includes(props.row.tipoId) &&
                  props.row.tieneCOE &&
                  props.row.asuntoNEUNCOE === 0
                  /*TODO: agregar esta condicion && props.row.tieneCOE*/
                "
                flat
                round
                color="secondary"
                icon="mdi-file-edit"
                @click="
                  parteSelected = props.row;
                  notElecId = props.row.notElecId;
                  showCOE = true;
                "
              >
                <q-tooltip>Capturar C.O.E.</q-tooltip>
              </q-btn>

              <q-btn
                v-if="
                  aplicaCOE.includes(props.row.tipoId) &&
                  props.row.tieneCOE &&
                  props.row.asuntoNEUNCOE !== 0
                "
                v-permitido="35"
                flat
                round
                color="secondary"
                icon="mdi-eye"
                @click="
                  parteSelected = props.row;
                  notElecId = props.row.notElecId;
                  showCOE = true;
                "
              >
                <q-tooltip>Ver C.O.E.</q-tooltip>
              </q-btn>
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </q-card-section>
  </q-card>
  <q-dialog v-model="showSintesisManual">
    <SintesisManual :item="props.acuerdo"></SintesisManual>
  </q-dialog>
  <q-dialog v-model="showDetalleAcuerdo" full-height full-width>
    <DetalleAcuerdo
      :item="acuerdo"
      :metaDatos="acuerdoMetadatos"
    ></DetalleAcuerdo>
  </q-dialog>
  <q-dialog v-model="showAsignarZona" persistent>
    <AsignarZona
      :parte="parteSelected"
      :acuerdo="acuerdo"
      @refrescar-notificaciones="
        async () => {
          showAsignarZona = false;
          await setRows();
        }
      "
      @cerrar="
        async () => {
          showAsignarZona = false;
          await setRows();
        }
      "
    ></AsignarZona>
  </q-dialog>
  <q-dialog
    v-model="showAsignarNotificaciones"
    full-height
    full-width
    persistent
  >
    <AsignarNotificaciones
      :partes="selected"
      :acuerdo="acuerdo"
      @cerrar="(showAsignarNotificaciones = false), setRows()"
    ></AsignarNotificaciones>
  </q-dialog>
  <q-dialog v-model="showSubirAcuse">
    <SubirAcuse
      :partes="selected"
      :acuerdo="acuerdo"
      @cerrar="
        () => {
          showSubirAcuse = false;
          setRows();
        }
      "
    ></SubirAcuse>
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
  <q-dialog v-model="showCOE" persistent>
    <CapturaCOE
      :parte="parteSelected"
      :acuerdo="acuerdo"
      :notElecId="notElecId"
      @cerrar="(showCOE = false), setRows()"
    ></CapturaCOE>
  </q-dialog>
</template>

<script setup>
import { ref, reactive, onMounted } from "vue";
import { date } from "quasar";
import { Utils } from "src/helpers/utils";
import { catTipoAsunto } from "src/data/catalogos";
// import { Firmador } from "src/helpers/firmadorInicio";
import DetalleAcuerdo from "../components/DetalleAcuerdo.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import AsignarZona from "./AsignarZona.vue";
import SubirAcuse from "./SubirAcuse.vue";
import ViewPdfComponent from "components/ViewPdfComponent.vue";
import AsignarNotificaciones from "./AsignarNotificaciones.vue";
import SintesisManual from "./SintesisManual.vue";
import DetalleParte from "./DetalleParte.vue";
import { useActuariaDetalleNotificacionesStore } from "../stores/actuaria-detalle-notificaciones-store";
import FiltrosColumnas from "../components/FiltrosColumnasNotificaciones.vue";
import { FiltrosColumnasNotificacionesDatos } from "../data/filtrosColumnasNotificaciones";
import { manejoErrores } from "src/helpers/manejo-errores";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import InputSearchTable from "src/components/InputSearchTable.vue";
import CapturaCOE from "./CapturaCOE.vue";
import { useActuariaStore } from "../stores/actuaria-store";

const showCOE = ref(false);
const aplicaCOE = [1, 5, 11];
const primeraCargaNotificaciones = ref(false);
const showDetalleParte = ref(false);
const showSintesisManual = ref(false);
const showAsignarNotificaciones = ref(false);
const nombreArchivo = ref(null);
const tipoArchivo = ref("pdf");
const tituloDialogFolio = ref("Ver Acuse");
const showDetalleAcuerdo = ref(false);
const showAsignarZona = ref(false);
const showSubirAcuse = ref(false);
const showDialogPdf = ref(false);
const selected = ref([]);
const parteSelected = ref(null);
const notElecId = ref(null);
const coloresList = ref([]);
const loading = ref(false);
const acuerdoMetadatos = ref([]);
const actuariaNotificacionesStore = useActuariaDetalleNotificacionesStore();
const actuariaStore = useActuariaStore();
const valoresFiltros = reactive(new FiltrosColumnasNotificacionesDatos());

const getBookColor = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name?.toLowerCase() === ta?.toLowerCase() &&
      t.book?.toLowerCase() === nc?.toLowerCase(),
  )?.shortName || "empty";

let refrescar = ref(false);
let rows = ref([]);
let textoBuscar = ref("");
let rowsPerPageOptions = ref([5, 7, 10, 15, 20, 25, 50, 0]);

onMounted(async () => {
  primeraCargaNotificaciones.value = true;
  await setRows();
  primeraCargaNotificaciones.value = false;
});
function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      label: "Ver todo",
      number: actuariaNotificacionesStore.notificaciones.totalRegistros || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-red-2",
      status: 1,
      label: "Pendientes",
      number:
        actuariaNotificacionesStore.notificaciones.metaDatos.pendiente || 0,
    },
    {
      color: "bg-yellow-2",
      status: 2,
      label: "En proceso",
      number:
        actuariaNotificacionesStore.notificaciones.metaDatos.enProceso || 0,
    },
    {
      color: "bg-green-2",
      status: 3,
      label: "Notificados",
      number:
        actuariaNotificacionesStore.notificaciones.metaDatos.notificados || 0,
    },
  ];
}
async function setRows() {
  loading.value = true;
  try {
    await actuariaNotificacionesStore.getNotificaciones({
      status: filter.status,
      text: textoBuscar.value,
      ...pagination.value,
      asuntoNeunId: props.acuerdo.expediente.asuntoNeunId,
      asuntoDocumentoID: props.acuerdo.asuntoDocumentoId,
      primeraCargaNotificaciones: primeraCargaNotificaciones.value,
      valoresFiltros,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rows.value =
    actuariaNotificacionesStore.notificaciones.datos?.map((x, i) => {
      x.index = i;
      x.tieneCOE = Boolean(x.tieneCOE);
      x.archivosAcuses =
        x.archivoAcuse !== null ? x.archivoAcuse.split("|") : "";
      return x;
    }) || [];
  acuerdoMetadatos.value =
    actuariaNotificacionesStore.notificaciones.metaDatos || [];

  pagination.value.rowsNumber =
    actuariaNotificacionesStore.notificaciones.totalRegistros;
  rowsPerPageOptions.value = [
    actuariaNotificacionesStore.notificaciones.totalRegistros,
  ];
  setColoresList();
  mapeaSeleccion();
  loading.value = false;
}

async function cambioFiltro(seleccionado) {
  Object.assign(valoresFiltros, seleccionado);
  await setRows();
}

async function onRequest(props) {
  pagination.value = props.pagination;
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

async function verOficio(row_noti) {
  if (row_noti.folio === 0) {
    return;
  }

  loading.value = true;
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
  loading.value = false;
}

async function verAcuse(nombreAcuse) {
  // const nombreAcuse = row_noti.archivoAcuse;
  const archivoInStore = actuariaStore.acuses.find(
    (acuse) => acuse.nombreArchivo === nombreAcuse,
  );
  if (archivoInStore) {
    nombreArchivo.value = Utils.base64ToUrlObj(archivoInStore.base64);
    showDialogPdf.value = true;
  } else {
    await consultarAcuses(nombreAcuse);
    const rutaArchivo = actuariaStore.acuses.find(
      (acuse) => acuse.nombreArchivo === nombreAcuse,
    );
    nombreArchivo.value = Utils.base64ToUrlObj(rutaArchivo.base64);
    showDialogPdf.value = true;
  }
}

async function consultarAcuses(nombreAcuse) {
  loading.value = true;
  try {
    // Tipo 3 -> Acuses
    const params = {
      asuntoNeunId: props.acuerdo.expediente.asuntoNeunId,
      anioPromocion: 0,
      numeroOrden: 0,
      tipoModulo: 3,
      origen: 0,
      asuntoDocumentoId: props.acuerdo.asuntoDocumentoId,
      sintesisOrden: props.acuerdo.sintesisOrden,
      nombre: nombreAcuse,
    };
    await actuariaStore.obtenerAcusesNas(params);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  loading.value = false;
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

const props = defineProps({
  acuerdo: {
    type: Object,
  },
});

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
    name: "Parte",
    align: "left",
    label: "Parte",
    field: "Parte",
    sortable: true,
  },
  {
    name: "Estado",
    align: "left",
    label: "Estado",
    field: "Estado",
    sortable: true,
  },
  {
    name: "Tipo",
    label: "Tipo",
    align: "left",
    field: "TipoNotificacion",
    sortable: true,
  },
  {
    name: "ActuarioAsigno",
    align: "left",
    label: "Asignó",
    field: "Actuario",
    sortable: true,
  },
  {
    name: "Asignado",
    align: "left",
    label: "Asignado",
    field: "Actuario",
  },
  {
    name: "ArchivoAcuse",
    align: "left",
    label: "Acuse",
    field: "acuse",
    sortable: true,
  },
  {
    name: "coe",
    align: "center",
    label: "C.O.E.",
    field: "",
    sortable: false,
  },
];
function mapeaSeleccion() {
  selected.value.forEach((p) => {
    const parte = rows.value.find((x) => x.parteId == p.parteId);
    if (parte) {
      p.tipoId = parte.tipoId;
      p.coe = parte.coe;
    }
  });
}

/**
 * firmarOficio
 * @param {notificación} rowParte 
 * Nota: Esta función es para el envio del firmado del oficio falta complementar datos para el envio
 */

// async function firmarOficio(rowParte){
//     try {
//       const estadoCat = 1; //1 para preautorizar, 2 autorizar
//       const params = [];
//       const acuerdo = props.acuerdo;

//       params.push(rowParte);

//       localStorage.setItem("cambioEstadoAcuerdo", estadoCat);
//       localStorage.setItem("acuerdoFirmar", JSON.stringify(params));

//       let documentos = [{}];

//       documentos.push({
//         nombre: quitarExtension(acuerdo.nombreArchivo),
//         id: acuerdo.uGuidDocumento,
//         tipoArchivo: "acuerdo",
//         modulo: 2
//       });

//       const documentosAFirmar = {
//         documentos: documentos,
//         firmarOficios: true,
//         accion: estadoCat,
//       };
//       console.log(documentosAFirmar);
//       await Firmador.obtenerURLGrafico(documentosAFirmar);

//     } catch (error) {
//       manejoErrores.mostrarError(error);
//     }
//   }

/**
 * Funcion que se usa para quitar la exteción el nombre del acuerdo y poder enviarlo al proceso de firma.
 */
  // function quitarExtension(nombreArchivo) {
  //   let partes = nombreArchivo.split('.');
  //   if (partes.length === 1 || (partes[0] === "" && partes.length === 2)) {
  //       return nombreArchivo; // Retorna el nombre original
  //   }
  //   partes.pop();
  //   return partes.join('.');
  // }
</script>
<style lang="sass" scoped>
.my-sticky-header-table
  thead tr th
    position: sticky
    z-index: 1
  thead tr:first-child th
    top: 0
    background-color: #fff
.tipoFolio
  text-decoration: underline
  color: var(--q-secondary)
  font-weight: bold
</style>
