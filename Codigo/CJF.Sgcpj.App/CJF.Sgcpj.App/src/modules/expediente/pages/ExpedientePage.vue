<template>
  <q-toolbar>
    <q-toolbar-title class="text-h5 text-primary text-bold"
      >Expediente Electrónico</q-toolbar-title
    >
  </q-toolbar>
  <q-toolbar class="q-gutter-md">
    <q-item>
      <q-item-section side>
        <q-icon size="md" :color="`${getBookColor(tipoAsunto, cuadernoDesc)}`">
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
              :fill="getBookColorHex(tipoAsunto, cuadernoDesc)"
            />
          </svg>
        </q-icon>
      </q-item-section>
      <q-item-section>
        <q-item-label caption>Expediente</q-item-label>
        <q-item-label class="text-h5 text-bold text-primary">
          {{ asuntoAlias || "" }}</q-item-label
        >
      </q-item-section>
    </q-item>
    <q-item>
      <q-item-section>
        <q-item-label caption>Tipo de Asunto</q-item-label>
        <q-item-label class="text-h6 text-bold">
          {{ tipoAsunto || "" }}</q-item-label
        >
      </q-item-section>
    </q-item>
    <q-item>
      <q-item-section v-if="cuadernoDesc">
        <q-item-label caption>Cuaderno</q-item-label>
        <q-item-label class="text-h6 text-bold">
          {{ cuadernoDesc || "" }}</q-item-label
        >
      </q-item-section>
    </q-item>
    <q-item>
      <q-item-section>
        <q-item-label caption>NEUN</q-item-label>
        <q-item-label class="text-h6 text-bold">
          {{ asuntoNeunId || "" }}</q-item-label
        >
      </q-item-section>
    </q-item>
    <q-item v-if="nombreOrigen">
      <q-item-section>
        <q-item-label caption>Origen</q-item-label>
        <q-item-label class="text-bold">{{ nombreOrigen }}</q-item-label>
      </q-item-section>
    </q-item>

    <q-space></q-space>
    <q-btn
      class="hidden"
      v-permitido="66"
      dense
      flat
      no-caps
      color="primary"
      icon="mdi-folder-eye"
      label="Ver expediente electrónico"
      @click="openExpedienteElectronico(expediente)"
    />
    <q-btn
      class="hidden"
      dense
      flat
      no-caps
      color="primary"
      icon="mdi-folder-arrow-left-right"
      label="Solicitar vinculación"
      @click="showVincularExpediente = true"
    ></q-btn>
    <q-btn
      class="hidden"
      dense
      flat
      no-caps
      color="primary"
      icon="mdi-share"
      label="Vincular"
      @click="showCompartirExpediente = true"
    ></q-btn>
    <q-btn
      class="q-px-lg hidden"
      unelevated
      label="Captura"
      icon="mdi-text-box-edit-outline"
      color="secondary"
      @click="$router.push('expedienteCaptura')"
    >
    </q-btn>
  </q-toolbar>
  <div class="row q-ma-md">
    <div class="col">
      <DatosGenerales :asuntoNeunId="asuntoNeunId"></DatosGenerales>
    </div>
  </div>
  <div class="row q-ma-md hidden">
    <div class="col q-mr-md">
      <q-card>
        <q-toolbar>
          <q-toolbar-title>Audiencia constitucional</q-toolbar-title>
        </q-toolbar>
        <div class="row">
          <div class="col">
            <q-item>
              <q-item-section side>
                <q-icon name="mdi-calendar-blank" />
              </q-item-section>
              <q-item-section>
                <q-item-label caption>Agendada</q-item-label>
                <q-item-label
                  >{{ audiencia?.fecha }} {{ audiencia?.hora }}</q-item-label
                >
              </q-item-section>
            </q-item>
          </div>
          <div class="col">
            <q-item>
              <q-item-section side>
                <q-icon name="mdi-check" />
              </q-item-section>
              <q-item-section>
                <q-item-label caption>Celebrada</q-item-label>
                <q-item-label>{{
                  audiencia?.resultado
                    ? `${audiencia?.resultado} - ${audiencia?.tipoAudiencia}`
                    : ""
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
        </div>
        <q-inner-loading :showing="cargandoAudiencia"> </q-inner-loading>
      </q-card>
    </div>
    <div class="col">
      <q-card>
        <q-toolbar>
          <q-toolbar-title>
            <q-btn flat round icon="mdi-gavel" color="secondary"></q-btn>
            Sentencia</q-toolbar-title
          >
        </q-toolbar>
        <div class="row">
          <div class="col">
            <q-item>
              <q-item-section side>
                <q-icon name="mdi-calendar-blank" color="grey-8" />
              </q-item-section>
              <q-item-section>
                <q-item-label caption>Engrose</q-item-label>
                <q-item-label>{{
                  estadoSentencia?.fechaSentencia
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div class="col">
            <q-item>
              <q-item-section side>
                <q-badge color="positive" rounded> </q-badge>
              </q-item-section>
              <q-item-section>
                <q-item-label caption>Estado</q-item-label>
                <q-item-label>{{ estadoSentencia?.estado }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div class="col">
            <q-item>
              <q-item-section side>
                <q-badge color="red" rounded> </q-badge>
              </q-item-section>
              <q-item-section>
                <q-item-label caption>Ejecución</q-item-label>
                <q-item-label>{{ estadoSentencia?.ejecucion }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
        </div>
        <q-inner-loading :showing="cargandoSentencia"> </q-inner-loading>
      </q-card>
    </div>
  </div>
  <div class="row q-mx-md">
    <q-card class="col" style="height: 300px">
      <q-toolbar>
        <q-toolbar-title>Ficha técnica</q-toolbar-title>
      </q-toolbar>
      <VerFichaTecnica :asuntoNeunId="asuntoNeunId"></VerFichaTecnica>
    </q-card>
    <q-card class="col q-ml-md" v-if="tipoAsuntoSinParte" style="height: 300px">
      <q-toolbar>
        <q-toolbar-title>Partes</q-toolbar-title>
        <q-btn
          v-permitido="63"
          flat
          no-caps
          label="Agregar parte"
          color="secondary"
          icon="mdi-account-plus"
          @click="(esEditarParte = false), (showAgregarParte = true)"
        >
        </q-btn>
      </q-toolbar>
      <q-separator></q-separator>
      <q-scroll-area style="height: 245px">
        <VerParte
          :partesExpediente="partesExpediente"
          @update:parte="editarParte"
          @delete:parte="
            (p) => {
              parte = p;
              showEliminarParte = true;
            }
          "
          @verdetalle:event="
            (val) => {
              parte = val;
              showDetalle = true;
            }
          "
        ></VerParte>
      </q-scroll-area>
      <!-- <q-card-actions align="center" class="hidden">
        <q-btn
          v-if="partesExpediente.length > 2"
          flat
          no-caps
          color="secondary"
          style="text-align: center"
          label="Ver más partes"
          @click="
            expedienteElectronicoStore.setExpedienteSeleccionado(expediente),
              $router.push('/expedientePartes')
          "
        >
        </q-btn>
        <q-btn
          v-else-if="partesExpediente.length === 0"
          flat
          disable
          no-caps
          color="secondary"
          style="text-align: center"
          label="No existen partes registradas."
        >
        </q-btn>
      </q-card-actions> -->
      <q-inner-loading :showing="cargandoPartes"> </q-inner-loading>
    </q-card>
  </div>
  <div class="row q-ma-md">
    <div class="col">
      <VerAcuerdosPromociones
        :asuntoNeunId="asuntoNeunId"
      ></VerAcuerdosPromociones>
    </div>
  </div>
  <q-dialog v-model="showDetalle" full-height full-width>
    <DetallePartes
      :selectedParte="parte"
      :expediente="expediente"
    ></DetallePartes>
  </q-dialog>
  <q-dialog v-model="showDialogPdf">
    <ViewPdfComponent :nombreArchivo="documentoPDF" />
  </q-dialog>
  <q-dialog v-model="showAgregarParte" persistent>
    <AgregarPartes
      :expediente="expediente"
      :itemParte="parte"
      :esEditar="esEditarParte"
      @cerrar="showAgregarParte = false"
      @refrescar:partes="getPartes()"
    ></AgregarPartes>
  </q-dialog>
  <q-dialog v-model="showCompartirExpediente">
    <CompartirExpediente></CompartirExpediente>
  </q-dialog>
  <q-dialog v-model="showVincularExpediente">
    <VincularExpediente></VincularExpediente>
  </q-dialog>
  <DialogConfirmacion
    v-model="showEliminarParte"
    label-btn-cancel="Cancelar"
    label-btn-ok="Eliminar"
    titulo="¿Deseas eliminar la parte?"
    :subTitulo="`Se eliminará la parte ${
      parte?.tipo == 1
        ? `${parte?.nombre} ${parte?.aPaterno} ${parte?.aMaterno}`
        : parte?.denominacionDeAutoridad
    } del expediente ${expediente?.asuntoAlias} ${expediente?.tipoAsunto}.`"
    @aceptar="eliminaParte"
  >
  </DialogConfirmacion>
</template>

<script setup>
import { noty } from "src/helpers/notify";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { ref, onMounted, computed } from "vue";
import { catTipoAsunto } from "src/data/catalogos";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store.js";
import ViewPdfComponent from "src/components/ViewPdfComponent.vue";
import documentoPDF from "src/assets/PromocionPrueba.pdf";
import VerFichaTecnica from "../components/VerFichaTecnica.vue";
import VerParte from "src/modules/expediente/components/VerParte.vue";
import VerAcuerdosPromociones from "../components/AcuerdosPromociones.vue";
import AgregarPartes from "../components/AgregarPartes.vue";
import CompartirExpediente from "../components/CompartirExpediente.vue";
import VincularExpediente from "../components/VincularExpediente.vue";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import DetallePartes from "../components/DetallePartes.vue";
import DatosGenerales from "../components/DatosGenerales.vue";

const expedienteElectronicoStore = useExpedienteElectronicoStore();
const usuariosStore = useUsuariosStore();
const showAgregarParte = ref(false);
const showEliminarParte = ref(false);
const esEditarParte = ref(false);
const parte = ref(null);
const showDialogPdf = ref(false);
const showCompartirExpediente = ref(false);
const showVincularExpediente = ref(false);
const cargandoPartes = ref(false);
const cargandoAudiencia = ref(false);
const cargandoSentencia = ref(false);
const estadoSentencia = ref({});
const audiencia = ref({});
const showDetalle = ref(false);
const expediente = ref({});

const props = defineProps({
  asuntoNeunId: {
    type: Number,
    default: 0,
  },
  asuntoAlias: {
    type: String,
    default: "",
  },
  tipoAsunto: {
    type: String,
    default: "",
  },
  cuadernoDesc: {
    type: String,
    default: "",
  },
});

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

const tipoAsuntoSinParte = computed(() => {
  const array = [18, 19, 28, 44, 45, 55, 56, 69, 72, 78, 82, 83, 128];

  return !array.includes(parseInt(props?.tipoAsunto));
});

const partesExpediente = ref([]);

async function getPartes() {
  cargandoPartes.value = true;
  try {
    const asuntoNeunId = props.asuntoNeunId;
    const partes = await usuariosStore.obtenerParteExistente(asuntoNeunId);
    expedienteElectronicoStore.setPartes(partes, asuntoNeunId);
    partesExpediente.value = partes;

    partesExpediente.value?.forEach((e) => {
      e.icon = "persona";
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoPartes.value = false;
}

function openExpedienteElectronico(asuntoNeunId) {
  expedienteElectronicoStore.getUrlExpedienteElectronico(asuntoNeunId);
}

onMounted(async () => {
  // await Promise.all([getPartes(), getAudiencia(), getEstadoSentencia()]);
  await Promise.all([getPartes()]);
  expediente.value = {
    asuntoNeunId: props.asuntoNeunId,
    asuntoAlias: props.asuntoAlias,
    tipoAsunto: props.tipoAsunto,
    cuadernoDesc: props.cuadernoDesc,
  };
});

// async function getAudiencia() {
//   cargandoAudiencia.value = true;
//   try {
//     audiencia.value = await expedienteElectronicoStore.obtenerAudiencia(
//       props.asuntoNeunId,
//       props.cuadernoId,
//     );
//   } catch (error) {
//     manejoErrores.mostrarError(error);
//   }
//   cargandoAudiencia.value = false;
// }
// async function getEstadoSentencia() {
//   cargandoSentencia.value = true;
//   try {
//     estadoSentencia.value =
//       await expedienteElectronicoStore.obtenerEstadoSentencia(
//         props.expediente.asuntoNeunId,
//       );
//   } catch (error) {
//     manejoErrores.mostrarError(error);
//   }
//   cargandoSentencia.value = false;
// }

function editarParte(parteEditar) {
  esEditarParte.value = true;
  parte.value = parteEditar;
  showAgregarParte.value = true;
}
/**
 * elimina parte
 */
async function eliminaParte() {
  try {
    const parametros = {
      personaId: parte.value.personaId,
      usuarioElimina: 0,
    };
    await expedienteElectronicoStore.eliminarParte(parametros);
    noty.correcto(
      `Se ha eliminado la parte ${
        parte.value?.tipo == 1
          ? `${parte.value?.nombre} ${parte.value?.aPaterno} ${parte.value?.aMaterno}`
          : parte.value?.denominacionDeAutoridad
      } del expediente ${props.expediente?.asuntoAlias} ${
        props.expediente?.tipoAsunto
      }.`,
    );
    await getPartes();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}
</script>
