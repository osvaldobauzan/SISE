<template>
  <q-page class="q-pa-sm">
    <q-toolbar>
      <q-toolbar-title class="text-bold text-h4 text-primary"
        >Sentencias</q-toolbar-title
      >

      <q-space></q-space>
      <q-btn
        dense
        unelevated
        no-caps
        icon="mdi-upload"
        color="primary"
        label="Subir Engrose sin Proyecto"
        class="q-px-lg q-mr-sm"
        @click="showSubirEngrose = true;">
      </q-btn>
      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        v-permitido="71"
        label="Autorizar"
        icon="mdi-file-sign"
        @click="showTableroAutorizar = true"
        class="q-px-lg q-mr-sm"/>

      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        v-permitido="69"
        label="Preautorizar"
        icon="mdi-file-sign"
        @click="registrosPreautorizarSeleccionados = rows.filter(c => c.selected === true); mostrarDialogoPreautorizar = true;"
        class="q-px-lg"/>

      </q-toolbar>

      <q-toolbar>
      <SelectDateComponent
        ref="selectDateComponent"
        title="Filtrar por fecha"
        @update:selectedDate="setSelectedDate"
      >
      </SelectDateComponent>
      <q-space></q-space>
      <SelectStatusComponent
        :filter="filter.status"
        :listStatus="coloresList"
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
          @request="onRequest"
          :rows="rows"
          :columns="columns"
          :filter="filter"
          :filter-method="filterTerm"
          :loading="loading"
          row-key="asuntoNeunId"
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
          <template v-slot:header-cell-selector="props">
            <q-th :props="props">
              <q-checkbox
                v-model="selectAll"
                @click="updateCheckbox"
              ></q-checkbox>
              <!--TODO: Añadir validación con sin engrose y autorizado-->
            </q-th>
          </template>
          <template v-slot:body="props">
            <q-tr :props="props" :class="getColor(props.row.estadoSentenciaId)">
              <q-td>
                <q-checkbox
                  v-if="props.row.estadoSentenciaId !== estatusProyecto.SinEngrose && props.row.estadoSentenciaId !== estatusProyecto.Autorizado"
                  v-model="props.row.selected"
                />
              </q-td>
              <q-td style="width: 200px">
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
                  <q-item-section avatar>
                    <q-avatar
                      icon="mdi-book-open-variant-outline"
                      :text-color="`${getBookColor(
                        props.row.expediente.catTipoAsunto,
                        props.row.expediente.nombreCorto,
                      )} `"
                      :color="`${getBookColor(
                        props.row.expediente.catTipoAsunto,
                        props.row.expediente.cuaderno,
                      )} `"
                    ></q-avatar>
                  </q-item-section>
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
                    <q-item-label>
                      <q-badge
                        :class="`bg-${getBookColor(
                          props.row.expediente.cuaderno,
                        )} text-black`"
                        :label="props.row.expediente.cuaderno"
                        v-if="
                          props.row.expediente.cuaderno &&
                          props.row.expediente.cuaderno.toLowerCase() !=
                            props.row.expediente.catTipoAsunto.toLowerCase()
                        "
                      >
                      </q-badge>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{
                      date.formatDate(
                        props.row.fechaAprobacionProyecto,
                        "DD/MM/YYYY",
                      )
                    }}
                    </q-item-label>
                    <q-item-label class="text-secondary">
                      {{
                        date.formatDate(
                          props.row.fechaAprobacionProyecto, "HH:mm"
                        )
                      }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <!--TODO: Cambiar esta fecha por la fecha del auto-->
                    <q-item-label>{{
                      date.formatDate(props.row.fechaAuto, "DD/MM/YYYY")
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td style="min-width: 250px">
                {{ props.row.temaDelAsunto?.substring(0, 99) }}
                <template v-if="props.row.temaDelAsunto?.length > 100">
                  ...
                  <a href="javascript:void(0)" class="verMas"> Ver más </a>
                </template>
              </q-td>
              <q-td>
                <div>
                  <q-btn
                    flat
                    stack
                    no-caps
                    icon="mdi-upload"
                    color="negative"
                    label="Sin engrose"
                    @click="
                      selectedItem = props.row;
                      showSubirEngrose = true;
                    "
                    v-if="
                      props.row.estadoSentenciaId === estatusProyecto.SinEngrose
                    "
                  >
                  </q-btn>
                  <q-btn
                    v-else
                    flat
                    stack
                    no-caps
                    color="secondary"
                    icon="mdi-paperclip"
                    @click="
                      //nombreArchivo = Sentencia;
                      selectedItem = props.row;
                      title = 'Engrose';
                      showDialogPdf = true;
                      mostrarBotonPreautorizarSentencia = props.row.estadoSentenciaId !== estatusProyecto.SinEngrose &&
                                                     props.row.estadoSentenciaId !== estatusProyecto.Autorizado;
                      registrosPreautorizarSeleccionados = [props.row];                                                     
                    "
                  >
                    {{
                      date.formatDate(
                        props.row.fechaAprobacionProyecto,
                        "DD/MM/YYYY",
                      )
                    }}
                    <!--//TODO: Modificar la fecha por la llave correcta-->
                    <q-item-label caption class="text-secondary">
                      {{ props.row.Secretario }}
                    </q-item-label>
                  </q-btn>
                </div>
              </q-td>
              <!--
              <q-td>
                <q-btn
                  flat
                  round
                  color="secondary"
                  icon="mdi-eye"
                >
                  <q-tooltip>Ver acuse</q-tooltip>
                </q-btn>
              </q-td>
              -->
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label> {{ props.row.usuarioCapturo }}</q-item-label>
                    <q-item-label caption>{{
                      date.formatDate(props.row.fechaCapturo, "DD/MM/YYYY")
                    }}</q-item-label>
                     <q-item-label caption>{{
                      date.formatDate(props.row.fechaCapturo, "HH:mm")
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.usuarioPreautorizo }}</q-item-label
                    >
                    <q-item-label caption>{{
                      date.formatDate(props.row.fechaPreautorizo, "DD/MM/YYYY")
                    }}</q-item-label>
                    <q-item-label caption>{{
                      date.formatDate(props.row.fechaPreautorizo, "HH:mm")
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.usuarioAutorizo }}</q-item-label
                    >
                    <q-item-label caption>{{
                      date.formatDate(props.row.fechaAutorizo, "DD/MM/YYYY")
                    }}</q-item-label>
                    <q-item-label caption>{{
                      date.formatDate(props.row.fechaAutorizo, "HH:mm")
                    }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-btn
                  flat
                  stack
                  no-caps
                  color="negative"
                  icon="mdi-alert"
                  label="No disponible"
                >
                  <q-tooltip>Ver estado de la versión</q-tooltip>
                </q-btn>
              </q-td>
              <q-td>
                <q-btn flat round color="blue" icon="mdi-dots-vertical">
                  <q-menu auto-close>
                    <q-list style="min-width: 100px">
                      <q-item
                        clickable
                        v-ripple
                        @click="
                          showDetalle = true;
                          selectedItem = props.row;
                        "
                      >
                        <q-item-section side
                          ><q-icon name="mdi-file-edit-outline"></q-icon
                        ></q-item-section>
                        <q-item-section>Editar la sentencia</q-item-section>
                      </q-item>
                      <q-item
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
                            >Eliminar la sentencia</q-item-label
                          >
                        </q-item-section>
                      </q-item>
                    </q-list>
                  </q-menu>
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
          :cuadernoDesc="selectedItem.cuaderno"
        />
      </ModalWindowComponent>
    </q-dialog>
    <q-dialog v-model="showDialogPdf" full-height full-width>
      <VerSentencia :model-value="selectedItem" :sentenciasPreautorizarSeleccionados="registrosPreautorizarSeleccionados">
        <template v-slot:default>
          <div class="q-gutter-lg q-mr-xl">
            <div class="row q-gutter-x-md items-center">
              <q-btn
                v-permitido="69"
                no-caps
                @click="mostrarDialogoPreautorizar = true;"
                unelevated
                icon="mdi-check"
                color="info"
                label="Preautorizar"
                v-if="selectedItem.estadoSentenciaId == 2"
              ></q-btn>
              <q-btn
                v-permitido="19"
                @click="firmadorInicio(2)"
                unelevated
                icon="mdi-check"
                color="positive"
                label="Autorizar"
                v-if="selectedItem.estadoSentenciaId == 3"
              ></q-btn>
              <q-btn
                v-permitido="20"
                @click="showCancelarAcuerdo = true"
                unelevated
                icon="mdi-close"
                color="negative"
                label="Cancelar"
                v-if="selectedItem.estadoSentenciaId == 3 || selectedItem.estadoSentenciaId == 4 || selectedItem.estadoSentenciaId == 5"
              ></q-btn>
            </div>
          </div>
        </template>
        <template v-slot:loading>
          <q-inner-loading :showing="cargandoEstadoAcuerdo" />
        </template>
      </VerSentencia>
      <ViewPdfComponent :nombreArchivo="nombreArchivo" :titulo="title"
        :mostrarPreautorizarSentencia="mostrarBotonPreautorizarSentencia"
        :sentenciasPreautorizarSeleccionados="registrosPreautorizarSeleccionados"
        @preautorizacion-Exitosa="actualizaTablero"
        @buttonAction="buttonAction">

      </ViewPdfComponent>
    </q-dialog>
    <q-dialog v-model="showSubirProyecto">
      <SubirProyecto :item="selectedItem"></SubirProyecto>
    </q-dialog>
    <q-dialog v-model="showVerProyecto" full-height full-width>
      <VerProyecto :item="selectedItem" :title="title"></VerProyecto>
    </q-dialog>
    <q-dialog v-model="showTableroAutorizar" full-height full-width>
      <AutorizarProyectos :registros="registros" />
    </q-dialog>
    <q-dialog v-model="showSubirEngrose" full-height>
      <SubirEngrose
        :item="selectedItem"
        @refrescar-tabla="
          setRows();
          showSubirEngrose = false;
          "
      ></SubirEngrose>
    </q-dialog>
    <q-dialog v-model="mostrarDialogoPreautorizar">
      <DialogoPreautorizaSentencia @buttonClicked="buttonAction" :registrosSeleccionados="registrosPreautorizarSeleccionados" @solicitud-Exitosa="actualizaTablero" />
    </q-dialog>
  </q-page>
</template>

<script setup>
import { date } from "quasar";
import { catTipoAsunto } from "src/data/catalogos";
import { manejoErrores } from "src/helpers/manejo-errores.js";
import { ref, reactive, onMounted } from "vue";
import {
  estatusProyecto,
  useSentenciasStore,
} from "../store/sentencias-store.js";
import TablaSinDatos from "components/TablaSinDatos.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import SubirProyecto from "../components/SubirProyecto.vue";
import VerProyecto from "../components/VerProyecto.vue";
import SubirEngrose from "../components/SubirEngrose.vue";
import AutorizarProyectos from "../components/AutorizarProyectos.vue";
import VerSentencia from "src/modules/sentencias/components/VerSentencia.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";
import InputSearchTable from "src/components/InputSearchTable.vue";
import DialogoPreautorizaSentencia from "../components/DialogoPreautorizaSentencia.vue";
import { Firmador } from "src/helpers/firmadorInicio";
import { useFirmadorStore } from "src/stores/firmador-store";


const showSubirEngrose = ref(false);
const showVerProyecto = ref(false);
const showSubirProyecto = ref(false);
const SentenciasStore = useSentenciasStore();
const showDialogPdf = ref(false);
const selectedDate = ref({});
const selectedItem = ref({});
const maximizedToggle = ref(false);
const expedientes = ref([]);
let refrescar = ref(false);
const showExpediente = ref(false);
const firmadorStore = useFirmadorStore();

const showTableroAutorizar = ref(false);
const title = ref("");
const rows = ref([]);
const selectAll = ref(false);

const textoBuscar = ref("");
const selectDateComponent = ref(null);

let loading = ref(false);

const filter = reactive({
  text: "",
  status: 0,
});
const registros = ref([]);

const mostrarBotonPreautorizarSentencia = ref(false);
const mostrarDialogoPreautorizar = ref(false);
const registrosPreautorizarSeleccionados = ref([]);

onMounted(async () => {
  registros.value = SentenciasStore.sentencias.filter(
    (row) => row.Estado === "Preautorizado",
  );

  const fechaHoy = date.formatDate(Date.now(), "DD/MM/YYYY");
  selectedDate.value = { from:fechaHoy, to:fechaHoy };
  await obtenerSentenciasStorage();
});

function removeFiltrosLocalStorage() {
  localStorage.removeItem("filtrosTramite");
}

async function obtenerSentenciasStorage() {
  const acuerdoFirmar = false;//TODO:Pendiente de pruebas de firmador en ambiente DEV

  if (acuerdoFirmar) {
    const estado = localStorage.getItem("cambioEstadoAcuerdo");
    const acuerdos = JSON.parse(acuerdoFirmar);
    const filtros = JSON.parse(localStorage.getItem("filtrosTramite"));
    if (filtros) {
      textoBuscar.value = filtros.text;
      filter.status = filtros.status;
      Object.assign(valoresFiltros, filtros.valoresFiltros);
      selectDateComponent.value.setFecha(filtros.selectedDate);
      selectedDate.value = filtros.selectedDate;
      Object.assign(pagination.value, filtros.pagination);
      if (filtros.status == 0) await setRows();
    }

    try {
      const documentosTransanccion =
        await firmadorStore.obtenerStatusTransaccion();
      if (documentosTransanccion) {
        const acuerdosFirmados = acuerdos.filter((elementoDeIDs) => {
          return documentosTransanccion.some((elementoTransaccion) => {
            return (
              elementoTransaccion.id === elementoDeIDs.guidDocumento &&
              parseInt(elementoTransaccion.estatus) === 2
            );
          });
        });
        const acuerdosNoFirmados = acuerdos.filter((elementoDeIDs) => {
          return documentosTransanccion.some((elementoTransaccion) => {
            return (
              elementoTransaccion.id === elementoDeIDs.guidDocumento &&
              parseInt(elementoTransaccion.estatus) === 3
            );
          });
        });

        if (acuerdosNoFirmados && acuerdosNoFirmados.length > 1) {
          noty.error(
            `No se completó exitosamente el firmado de ${
              acuerdosNoFirmados.length
            } acuerdos. No es posible su ${estadosError[estado - 1]}`,
          );
        }
        if (acuerdosNoFirmados && acuerdosNoFirmados.length === 1) {
          const acuerdo = acuerdosNoFirmados[0];
          noty.error(
            `No se completó exitosamente el firmado del acuerdo con fecha ${
              acuerdo.fechaAuto_F
            } del expediente ${acuerdo.expediente.asuntoAlias} ${
              acuerdo.expediente.catTipoAsunto
            } ${acuerdo.expediente.tipoProcedimiento || ""}. No es posible su ${
              estadosError[estado - 1]
            }`,
          );
        }
        if (acuerdosFirmados.length == 1) {
          await cambiarEstadoAcuerdo(estado, acuerdosFirmados[0]);
        } else {
          const parametros = acuerdosFirmados.map((acuerdo) => {
            const params = {
              asuntoNeunId: acuerdo.expediente.asuntoNeunId,
              asuntoDocumentoId: acuerdo.asuntoDocumentoId,
              tipoUpdate: estado,
              nombreDocumento: acuerdo.nombreArchivo?.includes(".")
                ? acuerdo.nombreArchivo?.split(".")[0]
                : acuerdo.nombreArchivo,
              tipoAsunto: acuerdo.expediente.catTipoAsunto,
              tipoProcedimiento: acuerdo.expediente.tipoProcedimiento || "",
              numeroExpediente: acuerdo.expediente.asuntoAlias,
              mesa: acuerdo.mesa,
              secretarioId: acuerdo.secretarioId,
              enviarAlerta: estado == 3,
              numeroPromocion: acuerdo.numeroRegistro,
              id: acuerdo.guidDocumento,
            };
            return params;
          });

          const resultadoCambioEstadoAcuerdos =
            await tramiteStore.cambiarEstadoAcuerdoMasivo(parametros);
          if (
            resultadoCambioEstadoAcuerdos.filter((a) => a.status == "fulfilled")
              .length > 0
          ) {
            await setRows();
            noty.correcto(
              `Se han ${estados[estado - 1]} ${
                resultadoCambioEstadoAcuerdos.filter(
                  (a) => a.status == "fulfilled",
                ).length
              } acuerdos exitosamente`,
            );
          }
        }
        localStorage.removeItem("acuerdoFirmar");
        localStorage.removeItem("cambioEstadoAcuerdo");
        localStorage.removeItem("cveTransaccion");
      }
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
  removeFiltrosLocalStorage();
}

function updateCheckbox() {
  rows.value.forEach((row) => {
    row.selected = row.estadoSentenciaId !== estatusProyecto.SinEngrose && row.estadoSentenciaId !== estatusProyecto.Autorizado && selectAll.value;
  });
}

async function onRequest() {
  await setRows();
}

function setSelectedDate(value) {
  selectedDate.value = value;
  setRows();
}

async function setRows() {
  loading.value = true;

  try {
    await SentenciasStore.obtenerSentencias({
      date: selectedDate.value,
      state: filter.status,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rows.value = SentenciasStore.data.datos;
  setColoresList();
  loading.value = false;
}

async function buscaEnBD() {
  pagination.value.page = 1;
  if (textoBuscar.value?.trim() != "") {
    refrescar.value = true;
    filter.text = textoBuscar.value;
  } else if (refrescar.value) {
    refrescar.value = false;
    filter.text = textoBuscar.value;
  }
}

const getBookColor = (ta) =>
  catTipoAsunto.find((t) => t.book === ta)?.shortName ?? "ad";

const getColor = (e) =>
  coloresList.value.find((i) => i.id == e)?.color ?? "bg-gray-2";

const pagination = ref({
  rowsPerPage: 0,
});


function filterTerm(rows, terms) {

  function compareWithTerms(colProperty) {
    return colProperty.some( colValue => {
      return String(colValue).toUpperCase().includes(terms.text.toUpperCase());
    });
  };

  function propertyCheck(row) {
    return compareWithTerms([row['expediente'].asuntoNeunId,
        row['expediente'].asuntoAlias,
        row['expediente'].catTipoAsunto,
        row['expediente'].catOrganismo,
        row['expediente'].cuaderno,
        row['temaDelAsunto'],
        row['usuarioCapturo'],
        row['usuarioPreautorizo'],
        row['usuarioAutorizo'],
        row['estadoSentencia'],
        ]);
  }

  function updateStatus(filteredRows) {
    for (const estatus in estatusProyecto) {
      if(estatusProyecto[estatus] == estatusProyecto.Todos)
      {
        coloresList.value[estatusProyecto.Todos].number = filteredRows.length;
        continue;
      }
      else {
        coloresList.value[estatusProyecto[estatus]].number = filteredRows.filter(
          (row) => row.estadoSentenciaId === estatusProyecto[estatus]
        ).length;
      }
    }
  }

  if (terms.status === 0) {
    let filteredRows = rows.filter((row) => {
      return propertyCheck(row);
    });
    updateStatus(filteredRows);
    return filteredRows;
  } else {
    let filteredRows = rows.filter(
      (row) =>
        terms.status === row.estadoSentenciaId && propertyCheck(row)
    );
    return filteredRows;
  }
}

function setFilterStatus(value) {
  filter.status = value;
  pagination.value.page = 1;
}

const coloresList = ref([]);

function setColoresList() {
  coloresList.value = [
    {
      id: 0,
      color: "bg-grey-4",
      status: estatusProyecto.Todos,
      label: "Ver todas",
      number: SentenciasStore.data.datos.length || 0,
      icon: "mdi-filter-off",
    },
    {
      id: 1,
      color: "bg-red-2",
      status: estatusProyecto.SinEngrose,
      label: "Sin engrose",
      number: SentenciasStore.data.metaDatos[estatusProyecto.SinEngrose] || 0,
    },
    {
      id: 2,
      color: "bg-purple-2",
      status: estatusProyecto.Capturado,
      label: "Capturado",
      number: SentenciasStore.data.metaDatos[estatusProyecto.Capturado] || 0,
    },
    {
      id: 3,
      color: "bg-orange-2",
      status: estatusProyecto.Cancelado,
      label: "Cancelado",
      number: SentenciasStore.data.metaDatos[estatusProyecto.Cancelado] || 0,
    },
    {
      id: 4,
      color: "bg-blue-2",
      status: estatusProyecto.Preautorizado,
      label: "Preautorizado",
      number:
        SentenciasStore.data.metaDatos[estatusProyecto.Preautorizado] || 0,
    },
    {
      id: 5,
      color: "bg-green-3",
      status: estatusProyecto.Autorizado,
      label: "Autorizado",
      number: SentenciasStore.data.metaDatos[estatusProyecto.Autorizado] || 0,
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
    name: "expediente",
    align: "center",
    label: "Expediente",
    field: "AsuntoAlias",
    sortable: true,
  },
  {
    name: "Fecha de aprobación del proyecto de sentencia",
    align: "center",
    label: "Fecha de aprobación del proyecto",
    field: "Estado",
    sortable: true,
  },
  {
    name: "Fecha del auto",
    align: "center",
    label: "Fecha de sentencia",
    field: "FechaAuto",
    sortable: true,
  },
  {
    name: "Sintesis",
    align: "center",
    label: "Tema del asunto",
    field: "Sintesis",
    sortable: true,
  },
  {
    name: "Engrose",
    align: "center",
    label: "Sentencia (Engrose)",
    field: "Engrose",
    sortable: true,
  },
  /*
  {
    name: "Acuse",
    align: "left",
    label: "Acuse de expediente",
    field: "AcuseExpediente",
    sortable: true,
  },
  */
  {
    name: "Capturo",
    align: "center",
    label: "Capturó",
    field: "Capturo",
    sortable: true,
  },
  {
    name: "Preautorizo",
    align: "center",
    label: "Preautorizó",
    field: "SecretarioPreautorizo",
    sortable: true,
  },
  {
    name: "Autorizo",
    align: "center",
    label: "Autorizó",
    field: "TitularAutorizo",
    sortable: true,
  },
  {
    name: "FechaPublicacion",
    align: "center",
    label: "Versión Pública",
    field: "FechaPublicacion",
    sortable: true,
  },
  {
    name: "acciones",
    align: "center",
    label: "",
    sortable: false,
  },
];

async function actualizaTablero() {
  showDialogPdf.value = false;
  mostrarDialogoPreautorizar.value = false;
  await setRows();
}

const buttonAction = () => {
  preautorizarMasivo(true);
};

async function preautorizarMasivo(autorizar) {
  const estadoCat = autorizar ? 2 : 1; //1 para preautorizar, 2 autorizar
  try {
    if (registrosPreautorizarSeleccionados.value.length < 1) {
      noty.error(`No se seleccionó ningún acuerdo para firmar`);
      return;
    }
    localStorage.setItem("cambioEstadoAcuerdo", estadoCat);
    localStorage.setItem("acuerdoFirmar", JSON.stringify(registrosPreautorizarSeleccionados.value));

    let documentos = [{}];

    documentos = registrosPreautorizarSeleccionados?.value?.map((x) => ({
      nombre: x.nombreArchivoSentencia,
      id: x.guidDocumento,
      tipoArchivo: "acuerdo",
      modulo: 2,
    }));

    const documentosAFirmar = {
      documentos: documentos,
      firmarOficios: false,
      accion: estadoCat,
    };
    await Firmador.obtenerURLGraficoSentencias(documentosAFirmar);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

</script>

<style lang="sass">
.my-sticky-header-table
  thead tr th
    position: sticky
    z-index: 1
  thead tr:first-child th
    top: 0
    background-color: #fff
  tbody
    .q-tr
      .q-td
        .q-item
          padding-left:0
          padding-right:0
        .q-item--clickable
          padding-left: 8px
          padding-right: 8px
          border-radius: 3px

.verMas
  background: lightgray
  padding: 2px 4px
  border-radius: 4px
  font-size: 80%
  color: #007bff
  font-weight: 600
</style>
