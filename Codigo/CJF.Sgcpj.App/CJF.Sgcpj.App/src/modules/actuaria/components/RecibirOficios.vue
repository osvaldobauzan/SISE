<template>
  <q-card class="bg-blue-grey-1">
    <q-toolbar class="bg-white">
      <q-toolbar-title class="text-bold"> Recibir oficios </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" @click="cerrarOficios" />
    </q-toolbar>
    <q-toolbar class="q-mt-sm">
      <q-space></q-space>
      <q-input
        bg-color="white"
        dense
        outlined
        autofocus
        v-model="search"
        style="width: 500px"
        placeholder="Número de oficio"
        :rules="[(val) => (val ? Validaciones.validaFolioAnio(val) : null)]"
        @keyup.enter="buscarPorTeclado"
      >
        <template v-slot:hint>
          <q-item-label :lines="1">
            <q-icon size="1.2em" color="light-blue" name="info" />
            Ingresa el número del oficio con formato NN/AAAA o escanea el código
            QR
          </q-item-label>
        </template>
        <template v-slot:append>
          <q-icon flat round name="mdi-qrcode-scan"></q-icon>
        </template>
        <template v-slot:after>
          <q-btn
            flat
            round
            icon="mdi-camera"
            @click="showQrScanner = true"
          ></q-btn>
        </template>
      </q-input>
    </q-toolbar>
    <q-card-section>
      <q-table
        dense
        class="my-sticky-header-table"
        :rows="rows"
        :columns="columns"
        v-model:pagination="pagination"
        row-key="index"
        loading-label="Cargando..."
        no-data-label="Sin registros"
        no-results-label="No se encontraron registros"
        rows-per-page-label="Registros por página:"
        :loading="loading"
      >
        <template v-slot:loading>
          <q-inner-loading showing color="primary" />
          <h3 color="primary">Cargando acuerdos...</h3>
        </template>
        <template #no-data>
          <TablaSinDatos
            titulo="Sin registros"
            subTitulo="Inserta registros con código QR o desde el cuadro de texto de Número de oficio."
            icono="mdi-file-search"
          ></TablaSinDatos>
        </template>

        <template v-slot:body="props">
          <q-tr :props="props">
            <q-td style="width: 150px">
              <q-item clickable class="q-pl-none">
                <q-item-section>
                  <q-item-label class="text-bold">
                    {{ props.row.expediente }}
                  </q-item-label>
                  <q-item-label caption class="text-secondary">
                    {{ props.row.tipoAsuntoDescripcion }}
                  </q-item-label>
                  <q-item-label
                    v-if="
                      props.row.nombreTipoCuaderno &&
                      props.row.tipoAsuntoDescripcion?.trim().toLowerCase() !=
                        props.row.nombreTipoCuaderno?.trim().toLowerCase()
                    "
                  >
                    <q-badge
                      :class="`bg-${getBook(
                        props.row.tipoAsuntoDescripcion,
                        props.row.nombreTipoCuaderno,
                      )} text-black`"
                      :label="props.row.nombreTipoCuaderno"
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
                    @click="verOficio(props.row)"
                  >
                    <q-tooltip>Ver oficio</q-tooltip>
                  </q-btn>
                </q-item-section>
                <q-item-section>
                  <q-item-label> {{ props.row.folio }}</q-item-label>
                  <q-item-label caption class="text-secondary">
                    {{ props.row.tipoNotificacion }}</q-item-label
                  >
                </q-item-section>
              </q-item>
            </q-td>

            <q-td>
              <q-item-label>
                {{ usuario.user.cargoDescripcion }}
              </q-item-label>
            </q-td>

            <q-td>
              <q-item-label>
                {{ usuario.user.completo }}
              </q-item-label>
            </q-td>

            <q-td>
              <q-item-label>
                {{ fechaHoy }}
              </q-item-label>
            </q-td>

            <q-td>
              <q-btn
                round
                flat
                icon="mdi-close"
                text-color="negative"
                @click="delOficioTable(props.row)"
              ></q-btn>
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </q-card-section>
    <q-card-actions class="q-mr-sm">
      <q-space></q-space>
      <q-btn
        :disable="rows.length < 1"
        :color="rows.length > 0 ? 'blue' : 'grey-6'"
        label="Recibir"
        style="width: 120px"
        @click="btnGuardar"
        v-close-popup
      />
      <q-btn
        outline=""
        color="blue"
        label="Cancelar"
        style="width: 120px"
        @click="cerrarOficios"
      />
    </q-card-actions>
  </q-card>
  <q-dialog v-model="showQrScanner">
    <QrScanner
      @addOficio="
        async (val) => {
          val.forEach(async (o) => {
            search = o;
            await buscarPorTeclado();
          });
        }
      "
    ></QrScanner>
  </q-dialog>
  <q-dialog v-model="showDialogPdf" full-height full-width>
    <ViewPdfComponent
      :nombreArchivo="archivoOficio"
      :titulo="nombreArchivo"
      :tipoArchivo="tipoArchivo"
    />
  </q-dialog>
  <DialogConfirmacion
    v-model="showCancelar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="¿Deseas cancelar la recepción de oficios?"
    :subTitulo="`No se guardará ninguna información que se haya agregado`"
    @aceptar="emit('cerrar')"
  ></DialogConfirmacion>

  <DialogConfirmacion
    v-model="showRecibirOficio"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="Oficio Existente "
    :subTitulo="`Este oficio ya ha sido recibido por ${nombrePropietarioOficio} , ¿Desea recibirlo aun?`"
    @aceptar="oficioPush()"
  ></DialogConfirmacion>
</template>

<script setup>
import { ref, onBeforeUnmount } from "vue";
import { noty } from "src/helpers/notify";
//import { tabla } from "../data/actuariaPartes.js";
import { catTipoAsunto } from "src/data/catalogos";
import { useActuariaOficiosStore } from "src/modules/actuaria/stores/actuaria-oficios-store";
import ViewPdfComponent from "src/components/ViewPdfComponent.vue";
import QrScanner from "src/components/QrScanner.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
//import AcuseOficio from "../data/docs/OficioEjemplo.pdf";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Validaciones } from "src/helpers/validaciones";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { Utils } from "src/helpers/utils";
import { useActuariaStore } from "../stores/actuaria-store";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";

const usuario = useAuthStore();
var fecha = new Date();
const opciones = { year: "numeric", month: "long", day: "numeric" };
var fechaHoy = fecha.toLocaleDateString("es-ES", opciones);
// const nombreArchivo = ref("AcuseOficio");
//const index = ref(0);
const archivoOficio = ref(null);
const nombreArchivo = ref("");
const tipoArchivo = ref("pdf");
const tituloDialogFolio = ref("Ver Oficio");
const actuariaStore = useActuariaStore();
const loading = ref(false);
const showDialogPdf = ref(false);
const actuariaOficiosStore = useActuariaOficiosStore();
const search = ref("");
const showQrScanner = ref(false);
const showRecibirOficio = ref(false);
const oficio = ref(null);
const nombrePropietarioOficio = ref("");

const emit = defineEmits({
  cerrar: () => true,
});
const showCancelar = ref(false);

async function buscarPorTeclado() {
  if (
    !search.value ||
    typeof Validaciones.validaFolioAnio(search.value) === "string"
  )
    return;
  try {
    const expediente = await actuariaOficiosStore.buscarOficio(search.value);
    if (expediente && expediente.length > 0) {
      oficio.value = expediente[0];

      if (
        !actuariaOficiosStore.oficios.some(
          (o) =>
            o.asuntoNeunId == oficio.value.asuntoNeunId &&
            o.folio == oficio.value.folio,
        )
      ) {
        oficioPush();
      }
      search.value = "";
    } else {
      noty.error("No se encontró oficio");
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

function oficioPush() {
  actuariaOficiosStore.oficios.push(oficio.value);
  showRecibirOficio.value = false;
}

const getBook = (ta, cu) =>
  catTipoAsunto.find((t) => t.name === ta && t.book == cu)?.shortName;

const rows = actuariaOficiosStore.oficios;
const pagination = ref({
  rowsPerPage: 0,
});

function delOficioTable(row) {
  const index = actuariaOficiosStore.oficios.indexOf(row);
  actuariaOficiosStore.oficios.splice(index, 1);
}

async function btnGuardar() {
  if (actuariaOficiosStore.oficios.length > 0) {
    try {
      await actuariaOficiosStore.recibirOficio(actuariaOficiosStore.oficios);
      noty.correcto("Oficios recibidos correctamente");
      actuariaOficiosStore.oficios = [];
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
}

const columns = [
  {
    name: "expediente",
    label: "Expediente",
    field: "Expediente",
    align: "left",
    sortable: true,
  },
  {
    name: "oficio",
    label: "Oficio",
    field: "Oficio",
    align: "left",
    sortable: true,
  },
  {
    name: "cargo",
    label: "Cargo",
    field: "Area",
    align: "left",
    sortable: true,
  },
  {
    name: "empleado",
    label: "Empleado",
    field: "Empleado",
    align: "left",
    sortable: true,
  },
  {
    name: "fecha",
    label: "Fecha",
    field: "Fecha",
    align: "left",
    sortable: true,
  },
  {
    name: "acciones",
    label: "Eliminar",
    align: "left",
  },
];

function cerrarOficios() {
  if (actuariaOficiosStore.oficios.length > 0) {
    showCancelar.value = true;
  } else {
    emit("cerrar");
  }
}
onBeforeUnmount(() => {
  actuariaOficiosStore.oficios = [];
});

async function verOficio(row_noti) {
  loading.value = true;
  tituloDialogFolio.value = `Ver Oficio (${row_noti.folio})`;
  archivoOficio.value = null;
  tipoArchivo.value = null;
  nombreArchivo.value = null;
  try {
    const parametrosArchivo = {
      Id: row_noti.uGuid,
    };
    const infoArchivo =
      await actuariaStore.obtenerArchivoB64(parametrosArchivo);
    if (infoArchivo.base64) {
      if (infoArchivo.nombreArchivo.includes(".pdf")) {
        archivoOficio.value = Utils.base64ToUrlObj(infoArchivo.base64);
        tipoArchivo.value = "pdf";
      } else {
        archivoOficio.value = Utils.base64ToBlobWord(infoArchivo.base64);
        tipoArchivo.value = "word";
      }
      nombreArchivo.value = infoArchivo.nombreArchivo;
    } else {
      noty.error("No se encontró el archivo");
    }
    showDialogPdf.value = true;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  loading.value = false;
}
</script>
