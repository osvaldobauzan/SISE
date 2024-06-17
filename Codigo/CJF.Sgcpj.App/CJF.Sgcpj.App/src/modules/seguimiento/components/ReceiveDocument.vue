<template>
  <q-card style="min-width: 450px">
    <q-toolbar>
      <q-toolbar-title class="text-weight-bold"
        >Recibir documento</q-toolbar-title
      >
      <q-space></q-space>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <div class="q-gutter-y-md q-ma-md">
      <q-select
        filled
        clearable
        use-input
        input-debounce="0"
        label="Tipo de documento"
        v-model="tipoDocumento"
        :options="opcionesTipoDocumento"
        @filter="filterTipoDocumento"
        @update:model-value="clearFields"
      >
        <template v-slot:no-option>
          <q-item>
            <q-item-section class="text-grey"> Sin resultados </q-item-section>
          </q-item>
        </template>
      </q-select>
      <template
        v-if="
          tipoDocumento === 'Expediente' ||
          tipoDocumento === 'Acuerdo' ||
          tipoDocumento === 'Notificación'
        "
      >
        <q-select
          filled
          clearable
          use-input
          input-debounce="0"
          :loading="buscandoExpedienteEnBD"
          label="Selecciona un expediente"
          v-model="expediente"
          :options="opcionesExpediente"
          :option-label="
            (option) =>
              option.expediente && option.tipoAsunto
                ? `${option.expediente} - ${option.tipoAsunto}`
                : ''
          "
          @update:model-value="funcionInput"
          @filter="buscarExpedientePorNumero"
        >
          <template v-slot:append>
            <q-icon name="mdi-magnify" />
          </template>
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
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.expediente }}</q-item-label>
                <q-item-label caption>{{ scope.opt.tipoAsunto }} </q-item-label>
                <q-item-label
                  class="text-caption"
                  v-if="scope.opt.tipoProcedimiento"
                  >{{ scope.opt.tipoProcedimiento }}</q-item-label
                >
              </q-item-section>
            </q-item>
          </template>
        </q-select>
      </template>
      <template v-if="tipoDocumento === 'Promoción'">
        <q-input
          filled
          clearable
          hint="0000/0000"
          label="Número de promoción"
          v-model="promocion"
        >
        </q-input>
      </template>
      <template v-if="tipoDocumento === 'Oficio'">
        <q-input
          filled
          clearable
          hint="0000/0000"
          label="Número de oficio"
          v-model="oficio"
        >
        </q-input>
      </template>
      <template
        v-if="tipoDocumento === 'Acuerdo' || tipoDocumento === 'Notificación'"
      >
        <q-select
          filled
          clearable
          use-input
          input-debounce="0"
          :loading="buscandoAcuerdoEnBD"
          label="Selecciona un acuerdo"
          v-model="acuerdo"
          :disable="disableAcuerdo"
          :options="opcionesAcuerdos"
          :option-label="
            (option) =>
              option.tipoDocumento && option.fechaHora_F
                ? `${option.tipoDocumento} - ${option.fechaHora_F.split(' ')[0]}`
                : ''
          "
          @filter="filterAcuerdo"
          @update:model-value="fetchDataComboAcuse"
        >
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-grey">
                Sin resultados
              </q-item-section>
            </q-item>
          </template>
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.tipoDocumento }}</q-item-label>
                <q-item-label caption
                  >{{ scope.opt.fechaHora_F.split(" ")[0] }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </template>
        </q-select>
      </template>
      <template v-if="tipoDocumento === 'Notificación'">
        <q-select
          filled
          clearable
          use-input
          input-debounce="0"
          :loading="buscandoAcuseEnBD"
          label="Selecciona un acuse"
          v-model="acuse"
          :disable="disableAcuse"
          :options="opcionesAcuse"
          :option-label="
            (option) =>
              option.nombreParte && option.caracter
                ? `${option.nombreParte} - ${option.caracter}`
                : ''
          "
          @filter="filterAcuse"
        >
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-grey">
                Sin resultados
              </q-item-section>
            </q-item>
          </template>
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.nombreParte }}</q-item-label>
                <q-item-label caption>{{ scope.opt.caracter }} </q-item-label>
              </q-item-section>
            </q-item>
          </template>
        </q-select>
      </template>
      <q-btn
        class="q-px-xl q-mt-xl"
        no-caps
        color="primary"
        label="Recibir"
        @click="crearQr"
        :disable="disableButton"
      ></q-btn>
    </div>
  </q-card>
</template>

<script setup>
import { ref, computed } from "vue";
import { Notify } from "quasar";
import { useSeguimientoStore } from "../stores/seguimiento-store";
import { manejoErrores } from "src/helpers/manejo-errores";

const tipoDocumento = ref("");
const tipoDocumentoActual = ref("");
const expediente = ref("");
const promocion = ref("");
const oficio = ref("");
const acuerdo = ref("");
const acuse = ref("");
const disableAcuerdo = ref(true);
const disableAcuse = ref(true);
const QrString = ref("");
const documentoARecibir = ref("");
const seguimientoStore = useSeguimientoStore();
const buscandoExpedienteEnBD = ref(false);
const buscandoAcuerdoEnBD = ref(false);
const buscandoAcuseEnBD = ref(false);
const expedienteABuscar = ref("");
const findData = ref(true);

const disableButton = computed(() => {
  if (tipoDocumento.value === "Acuerdo") {
    return !(expediente.value && acuerdo.value);
  } else if (tipoDocumento.value === "Notificación") {
    return !(expediente.value && acuerdo.value && acuse.value);
  } else if (tipoDocumento.value === "Promoción") {
    return !(promocion.value && promocion.value.match(/^\d{1,4}\/\d{4}$/));
  } else if (tipoDocumento.value === "Oficio") {
    return !(oficio.value && oficio.value.match(/^\d{1,4}\/\d{4}$/));
  } else if (tipoDocumento.value === "Expediente") {
    return !expediente.value;
  } else {
    return true;
  }
});

function clearFields() {
  expediente.value = "";
  promocion.value = "";
  oficio.value = "";
  acuerdo.value = "";
  acuse.value = "";
  disableAcuerdo.value = true;
  disableAcuse.value = true;
  opcionesExpediente.value = [];
  opcionesAcuerdos.value = [];
  opcionesAcuse.value = [];
  Object.keys(myNewQr).forEach((key1) => {
    Object.keys(myNewQr[key1]).forEach((key2) => {
      myNewQr[key1][key2] = "";
    });
  });
}

function crearQr() {
  myNewQr.E.N = expediente.value.AsuntoNeun;
  switch (tipoDocumento.value) {
    case "Expediente":
      tipoDocumentoActual.value = "E";
      myNewQr.E.N = expediente.value.asuntoNeun;
      myNewQr.TipoDocumento.NP = expediente.value.expediente;
      documentoARecibir.value =
        "el expediente " +
        expediente.value.Expediente +
        " - " +
        expediente.value.TipoAsunto;
      break;

    case "Acuerdo":
      tipoDocumentoActual.value = "A";
      myNewQr.E.N = expediente.value.asuntoNeun;
      myNewQr.TipoDocumento.NP = acuerdo.value.documentoId;
      documentoARecibir.value = "el acuerdo - " + acuerdo.value.Fecha;
      break;

    case "Promoción":
      tipoDocumentoActual.value = "P";
      myNewQr.TipoDocumento.NP = promocion.value;
      documentoARecibir.value = "la promoción - " + promocion.value;
      break;

    case "Oficio":
      tipoDocumentoActual.value = "O";
      myNewQr.TipoDocumento.NP = oficio.value;
      documentoARecibir.value = "el oficio - " + oficio.value;
      break;

    case "Notificación":
      tipoDocumentoActual.value = "N";
      myNewQr.E.N = expediente.value.asuntoNeun;
      myNewQr.TipoDocumento.NP = acuerdo.value.documentoId;
      documentoARecibir.value =
        "la notificación - " +
        acuse.value.NombreParte +
        " - " +
        acuse.value.Caracter;
      break;

    default:
      break;
  }
  pushData();
}

//Insertar datos del documento recibido
async function pushData() {
  QrString.value = JSON.stringify(myNewQr).replace(
    /"TipoDocumento"/g,
    `"${tipoDocumentoActual.value}"`,
  );
  if (tipoDocumentoActual.value === "O") {
    QrString.value = oficio.value;
  }
  if (tipoDocumentoActual.value === "P") {
    QrString.value = promocion.value;
  }
  try {
    await seguimientoStore.insertarSeguimiento(QrString.value);
    if (seguimientoStore.InsertaSeguimiento.value.length > 2) {
      expediente.value = "";
      promocion.value = "";
      oficio.value = "";
      acuerdo.value = "";
      acuse.value = "";
      disableAcuerdo.value = true;
      disableAcuse.value = true;

      showNotification(
        "Haz recibido " + documentoARecibir.value,
        "positive",
        "top",
      );
    } else {
      showNotification(
        "Error, no se pudo recibir " + documentoARecibir.value,
        "negative",
        "top",
      );
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

//Llenar combo Expediente
async function buscarExpedientePorNumero(val, update, abort) {
  update(async () => {
    if (val === "" || val.length <= 5) {
      findData.value = true;
      abort();
      return;
    } else {
      buscandoExpedienteEnBD.value = true;
      try {
        await seguimientoStore.buscarExpedientes(val, tipoDocumento.value);
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
        expedienteABuscar.value = opcionesExpediente.value[0];
      }
    }
  });
}

//Llenar combo Acuerdo (Asunto)
async function fetchDataComboAsunto() {
  acuerdo.value = null;
  acuse.value = null;
  disableAcuse.value = true;
  if (expediente.value && tipoDocumento.value != "Expediente") {
    buscandoAcuerdoEnBD.value = true;
    try {
      await seguimientoStore.buscarAcuerdos(
        expediente.value.expediente,
        expediente.value.tipoAsunto,
        expediente.value.tipoProcedimiento,
        tipoDocumento.value,
      );
      if (seguimientoStore.AcuerdoArray.length > 0) {
        acuerdosOptions.value = seguimientoStore.AcuerdoArray;
      } else {
        acuerdosOptions.value = null;
        showNotification(
          "No se encontraron acuerdos para el expediente " +
            expediente.value.expediente +
            " " +
            expediente.value.tipoAsunto,
          "negative",
          "top",
        );
      }
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    buscandoAcuerdoEnBD.value = false;
  }
  if (expediente.value && acuerdosOptions.value) {
    disableAcuerdo.value = false;
  } else {
    disableAcuerdo.value = true;
    acuerdo.value = null;
  }
  disableAcuse.value = true;
  acuse.value = null;
}

//Llenar combo Acuse (Partes) Notificación
async function fetchDataComboAcuse() {
  acuse.value = null;
  if (expediente.value && acuerdo.value) {
    buscandoAcuseEnBD.value = true;
    try {
      await seguimientoStore.buscarAcuses(
        expediente.value.expediente,
        expediente.value.tipoAsunto,
        expediente.value.tipoProcedimiento,
        acuerdo.value.tipoDocumento,
        acuerdo.value.fechaHora_F,
      );
      if (seguimientoStore.AcuseArray.length > 0) {
        acuseOptions.value = seguimientoStore.AcuseArray;
      } else {
        acuseOptions.value = null;
        showNotification(
          "No se encontraron acuses con el acuerdo " +
            acuerdo.value.TipoDocumento +
            " " +
            acuerdo.value.Fecha,
          "negative",
          "top",
        );
      }
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    buscandoAcuseEnBD.value = false;
  }
  if (acuerdo.value) {
    disableAcuse.value = false;
  } else {
    disableAcuse.value = true;
    acuse.value = null;
  }
}

const optionsTipoDocumentos = [
  "Acuerdo",
  "Expediente",
  "Oficio",
  "Promoción",
  "Notificación",
];

function funcionInput() {
  if (expediente.value != null) {
    fetchDataComboAsunto();
  } else {
    acuse.value = null;
    acuerdo.value = null;
    disableAcuerdo.value = true;
    disableAcuse.value = true;
  }
}

const opcionesExpediente = ref([]);
const acuerdosOptions = ref([{}]);
const acuseOptions = ref([{}]);

//FILTRO POR TIPO DE DOCUMENTO
const opcionesTipoDocumento = ref([]);
function filterTipoDocumento(val, update) {
  update(() => {
    opcionesTipoDocumento.value = optionsTipoDocumentos;
  });

  update(() => {
    const needle = val.toLowerCase();
    opcionesTipoDocumento.value = optionsTipoDocumentos.filter((v) =>
      v.toLowerCase().includes(needle),
    );
  });
}

//FILTRO POR ACUERDO
const opcionesAcuerdos = ref([{}]);
function filterAcuerdo(val, update) {
  update(() => {
    opcionesAcuerdos.value = acuerdosOptions.value;
  });

  update(() => {
    const needle = val.toLowerCase();
    opcionesAcuerdos.value = acuerdosOptions.value.filter(
      (v) =>
        v.tipoDocumento.toLowerCase().includes(needle) ||
        v.fechaHora_F.split(" ")[0].toLowerCase().includes(needle),
    );
  });
}

//FILTRO POR ACUSE
const opcionesAcuse = ref([]);
function filterAcuse(val, update) {
  update(() => {
    opcionesAcuse.value = acuseOptions;
  });

  update(() => {
    const needle = val.toLowerCase();
    opcionesAcuse.value = acuseOptions.value.filter(
      (v) =>
        v.nombreParte.toLowerCase().includes(needle) ||
        v.caracter.toLowerCase().includes(needle),
    );
  });
}

function showNotification(message, type, position) {
  Notify.create({
    message: message,
    type: type,
    position: position,
  });
}

//Crea un Json para poder ser recibido y menejado por el backend con la nomenclatura establecida,
const myNewQr = {
  E: {
    //Expediente
    N: "", //AsuntoNeunId
  },
  TipoDocumento: {
    //Eg. Acuerdo = A, Promoción = P, Notificación = N, Oficio = O.
    NP: "", //NumeroPromocion (DocumentoId)
  },
};
</script>
