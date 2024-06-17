<template>
  <q-card
    :style="
      'min-height:85vh;' +
      (esEdicion && detalle?.conArchivo
        ? 'min-width: 85vw'
        : 'min-width: 70vw  ')
    "
  >
    <q-separator></q-separator>
    <q-splitter
      before-class="spliterBefore"
      v-model="splitterModel"
      :limits="[0, 50]"
    >
      <template v-slot:before>
        <VerPromociones
          ref="child"
          v-if="detalle?.conArchivo == 1 || detalle.numeroAnexos > 0"
          style="width: 100%; height: 100vh"
          promocionDocumento
          :promocion="
            promocion?.kIdElectronica || promocion?.esPromocionE
              ? promocion
              : detalle
          "
          :es-edicion="esEdicion"
          :esDetalle="false"
        />
      </template>

      <template v-slot:after>
        <q-toolbar>
          <q-toolbar-title class="text-bold q-ml-md">
            {{ title }}</q-toolbar-title
          >
          <q-btn
            flat
            round
            dense
            icon="mdi-close"
            @click="emit('cerrar', cambioForm)"
          />
        </q-toolbar>
        <q-separator></q-separator>
        <q-scroll-area ref="scrollAreaRef" style="width: 100%; height: 74vh">
          <q-card-section>
            <q-form @submit="submitForm" ref="formPromocion" no-focus-error>
              <step-promocion
                ref="stePromocionRef"
                :model-value="parametros"
                @params:cambio="
                  (val) => {
                    cambianParametros(val);
                    cambioFormPromocion = true;
                  }
                "
                @expediente-nuevo="
                  (val) => {
                    expedienteNuevo = val;
                    reseteaForm();
                  }
                "
                @cambio-expediente="
                  (val) => {
                    cambioExpedienteaPromocion = val;
                  }
                "
                :promocion="props.promocion"
                :es-edicion="props.esEdicion"
              ></step-promocion>
              <step-promovente
                @params:cambio="
                  (val) => {
                    cambianParametros(val);
                    cambioFormPromovente = true;
                  }
                "
                :detallePromocion="parametros"
                :expediente-nuevo="expedienteNuevo"
                :promocion="props.promocion"
              ></step-promovente>

              <step-vincular-promocion
                :detalle-promocion="parametros"
                :es-editar="esEdicion"
                :promocion="props.promocion"
                @event:coincide-false="
                  (val) => {
                    fileRequerido = val;
                    cambianParametros();
                  }
                "
                @event:cancelarReemplazo="
                  parametros.archivoAVincular = null;
                  cambianParametros();
                "
                @params:cambio="
                  (val) => {
                    cambianParametros(val);
                  }
                "
                @event:ver-boleta-occ="verBoletaOCC()"
              ></step-vincular-promocion>
              <step-anexos
                @add:anexo="
                  esEditarAnexo = false;
                  emit('add:anexo');
                "
                @update:anexo="
                  (val) => {
                    esEditarAnexo = true;
                    emit('update:anexo', val);
                  }
                "
                @params:cambio="
                  (val) => {
                    cambianParametros(val);
                  }
                "
                :detallePromocion="parametros"
                @tiene-anexos="
                  (val) => {
                    tieneAnexos = val;
                  }
                "
                :es-edicion="props.esEdicion"
              ></step-anexos>
            </q-form>
            <q-inner-loading :showing="cargando"> </q-inner-loading>
          </q-card-section>
          <GenerateQr
            @print="
              generateQrCode = false;
              esEdicion ? emit('cerrar', false) : null;
            "
            v-if="generateQrCode"
            v-model="jsonParaQr"
            :descripcion="descripcionQr"
            :es-html="true"
            :auto-print="true"
          >
          </GenerateQr>
        </q-scroll-area>
        <q-separator></q-separator>
        <q-card-actions align="left">
          <q-btn
            no-caps
            style="min-width: 164px"
            :color="!validoPaso1 ? 'grey-6' : 'blue'"
            @click="validoPaso1 ? submitForm() : null"
            :disable="!validoPaso1"
            :label="!esEdicion ? 'Guardar y capturar otra' : 'Guardar'"
            class="q-ml-sm q-mr-sm"
          />
          <q-btn
            no-caps
            @click="emit('cerrar', cambioForm)"
            outline
            label="Cancelar"
            :color="'secondary'"
            :style="!esEdicion ? 'min-width: 210px' : 'min-width: 164px'"
          />
        </q-card-actions>
      </template>
    </q-splitter>
    <q-inner-loading :showing="cargandoGuardado">
      <template v-slot>
        <q-spinner size="40px" />
        <div v-html="mensajeLoader"></div>
      </template>
    </q-inner-loading>
  </q-card>
</template>

<script setup>
import { date } from "quasar";
import { ref, onMounted, watch, onBeforeUnmount, computed } from "vue";
import { useOficialiaStore } from "../stores/oficialia-store";
import { usePromoventesStore } from "../stores/promoventes-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { FormPromocion } from "../data/form-promocion";
import StepPromocion from "./StepPromocion.vue";
import StepAnexos from "./StepAnexos.vue";
import StepPromovente from "./StepPromovente.vue";
import StepVincularPromocion from "./StepVincularPromocion.vue";
import VerPromociones from "../components/VerPromociones.vue";
import { GuardarPersonasAsuntos } from "../data/guardar-personas-asuntos";
import { GuardarAutoridadJudicial } from "../data/guardar-autoridad-judicial";
import { GuardarPromoventes } from "../data/guardar-promoventes";
import { InsertarPromocion } from "../data/insertar-promocion";
import GenerateQr from "src/components/GenerateQr.vue";
import { DetallePromocion } from "../data/detalle-promocion";
import { noty } from "src/helpers/notify";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { Utils } from "src/helpers/utils";
import { useCatalogosStore } from "../../../stores/catalogos-store";

const catalogosStore = useCatalogosStore();
const cargando = ref(false);
const cargandoGuardado = ref(false);
const mensajeLoader = ref("");
const authStore = useAuthStore();
const usuariosStore = useUsuariosStore();
const generateQrCode = ref(false);
const jsonParaQr = ref("");
const descripcionQr = ref("");
const detalle = ref(new DetallePromocion());
const formPromocion = ref(null);
const esEditarAnexo = ref(false);
const oficialiaStore = useOficialiaStore();
const promoventesStore = usePromoventesStore();
const validoPaso1 = ref(false);
const splitterModel = ref(0);
const tieneAnexos = ref(false);
let parametros = ref(new FormPromocion());
const cambioFormPromocion = ref(false);
const cambioFormPromovente = ref(false);
const expedienteNuevo = ref(false);
const cambioExpedienteaPromocion = ref(false);
const cambioForm = ref(false);
const originalAsuntoNeunId = ref(null);
const cambioExpedienteInsertar = ref(false);
const originalNumeroOCC = ref(null);
const scrollAreaRef = ref(null);
const stePromocionRef = ref(null);
const fileRequerido = ref(false);
const origenesCrearPromo = [5, 6, 15, 22, 29];
const origenesAsociarPromo = [14];
const child = ref(null);
const props = defineProps({
  title: {
    type: String,
    default: "Capturar Promoción",
  },
  origen: {
    type: String,
    default: "OFICIALÍA",
  },
  promocion: {
    type: Object,
  },
  addAnexo: {
    default: null,
  },
  updateAnexo: {
    default: null,
  },
  esEditar: {
    type: Boolean,
    default: false,
  },
});

const esEdicion = ref(false);

// eslint-disable-next-line no-unused-vars
const emit = defineEmits({
  cerrar: (value) => value !== null,
  "add:anexo": () => true,
  "update:anexo": (value) => value !== null,
  "refrescar-tabla": () => true,
});
const stopWatch = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.addAnexo?.value,
  async (_newValue) => {
    if (!esEditarAnexo.value && _newValue) {
      if (!parametros.value.anexos) {
        parametros.value.anexos = [];
      }
      if (_newValue.caracter === null) {
        _newValue.caracter = { id: 0, descripcion: "", elementos: 0 };
      }
      if (_newValue.descripcion === null) {
        _newValue.descripcion = { id: 0, descripcion: "", elementos: 0 };
      }
      parametros.value.anexos.push({
        ..._newValue,
        id: parametros.value.anexos.length + 1,
      });
      cambianParametros();
    }
  },
  {
    immediate: true,
  },
);
const newAutoridad = computed(() => {
  return usuariosStore.autoridadNuevaStore;
});
const stopWatchAnexo = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.updateAnexo?.value,
  async (_newValue) => {
    if (_newValue && _newValue.caracter) {
      const index = parametros.value.anexos.findIndex(
        (x) => x.id === _newValue.id,
      );
      parametros.value.anexos[index] = _newValue;
    }
    validoPaso1.value = await formPromocion.value?.validate(false);
  },
  {
    immediate: true,
  },
);

onMounted(async () => {
  cargando.value = true;
  mensajeLoader.value = "";
  esEdicion.value = false;
  originalAsuntoNeunId.value = props.promocion?.expediente?.asuntoNeunId;
  if (props.promocion) {
    esEdicion.value = true;
    const parametrosRequest = {
      asuntoNeunId: props.promocion.expediente?.asuntoNeunId,
      origen: props.promocion.origen,
      numeroOrden: props.promocion.numeroOrden,
      yearPromocion: props.promocion.yearPromocion,
      kIdElectronica: props.promocion.kIdElectronica,
      catOrganismoId: props.promocion.expediente?.catOrganismoId,
      horaPromocionElectronica:
        date.formatDate(props.promocion?.fechaPresentacion, "HH:mm") || "",
      esPromocionE: props.promocion.esPromocionE,
      estado: props.promocion.estado,
      tipo:
        props.promocion.origenPromocionDescripcion ||
        props.modelValue.nombreOrigen,
      subTipo: props.promocion.nombreOrigen,
    };
    try {
      await oficialiaStore.detallePromocion(parametrosRequest);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    detalle.value = oficialiaStore.promocion;
    originalNumeroOCC.value = detalle.value.occ;
    try {
      await usuariosStore.obtenerParteExistente(
        props.promocion.expediente?.asuntoNeunId,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    try {
      await usuariosStore.obtenerPromoventeExistente(
        props.promocion.expediente?.asuntoNeunId,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    try {
      await usuariosStore.obtenAutoridadXExpediente(
        props.promocion.expediente?.asuntoNeunId,
        detalle.value.expediente,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    await setPromovente();
    parametros.value.numeroExpediente = detalle.value.expediente;
    parametros.value.registro = detalle.value.numeroRegistro;
    parametros.value.origen = detalle.value.origenPromocionId;
    parametros.value.origenDescripcion = detalle.value.origenPromocion;
    parametros.value.copias = detalle.value.numeroCopias;
    parametros.value.fojas = detalle.value.fojas;
    parametros.value.numeroOCC =
      detalle.value.occ == 0 ? "" : detalle.value.occ;
    parametros.value.asuntoNeunId = props.promocion.expediente?.asuntoNeunId;
    parametros.value.numeroOrden = props.promocion.numeroOrden;
    parametros.value.fechaPresentacion = date.formatDate(
      new Date(detalle.value.fechaPresentacion),
      "DD/MM/YYYY",
    );
    const tipoAsunto = catalogosStore.asuntos.find(
      (s) => s.catTipoAsuntoId == detalle.value.catTipoAsuntoId,
    );
    parametros.value.tipoAsunto = tipoAsunto;
    //Calcula numero de registro si no lo trae la promo
    if (!parametros.value.registro) {
      try {
        await oficialiaStore.calculaRegistro();
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
      parametros.value.registro = "" + oficialiaStore.noRegistro;
    }
    parametros.value.horaPresentacion = detalle.value.horaPresentacion
      ? `${detalle.value.horaPresentacion.split(":")[0]}:${
          detalle.value.horaPresentacion.split(":")[1]
        }`
      : detalle.value.horaPresentacion;
    parametros.value.anexos = detalle.value.anexos?.map((a, i) => {
      let arc = {};
      arc.descripcion = {
        id: a.DescripcionAnexo,
        descripcion: a.Descripcion,
      };
      arc.caracter = { id: a.CaracterAnexo, descripcion: a.Caracter };
      arc.tipoAnexo = { id: a.ClaseAnexo, descripcion: a.TipoAnexo };
      arc.guardadoEnBD = true;
      arc.nombreArchivo = a.NombreArchivo;
      arc.consecutivo = a.Consecutivo;
      arc.id = i + 1;
      return arc;
    });

    parametros.value.tipoParte = "parteExistente";
    parametros.value.esPromoventeExistente = true;
  } else {
    parametros.value.anexos = [];
  }
  props.origen === "OFICIALÍA"
    ? (splitterModel.value = 0)
    : (splitterModel.value = 50);

  if (detalle.value.conArchivo == 1 || detalle.value.numeroAnexos > 0) {
    splitterModel.value = 50;
  } else {
    splitterModel.value = 0;
  }
  formPromocion.value?.focus();
  cargando.value = false;
});

async function setPromovente() {
  switch (detalle.value.clasePromoventeDescripcion) {
    case "Promovente":
      {
        parametros.value.tipoPromovente = "promovente";

        parametros.value.promoventeExistente =
          usuariosStore.promoventeExistente?.find(
            (t) => t.personaId == detalle.value.idPromovente,
          );

        parametros.value.promoventeAutoridadExistente =
          usuariosStore.parteExistente?.find(
            (t) => t.personaId == detalle.value.parteAsociadaId,
          );
      }
      break;
    case "Partes":
      {
        parametros.value.tipoPromovente = "parte";
        parametros.value.promoventeAutoridadExistente =
          usuariosStore.parteExistente?.find(
            (t) => t.personaId == detalle.value.idPromovente,
          );
      }
      break;
    case "Autoridad Judicial":
      {
        parametros.value.tipoPromovente = "autoridad";
        try {
          await usuariosStore.obtenerAutoridadJudicial(
            detalle.value.promoventeNombre +
              " " +
              detalle.value.promoventeApellidoPaterno +
              " " +
              detalle.value.promoventeApellidoMaterno,
          );
        } catch (error) {
          manejoErrores.mostrarError(error);
        }

        parametros.value.promoventeAutoridad =
          usuariosStore.autoridadJudicial?.find(
            (t) =>
              t.catOrganismoId == detalle.value.autoridadOrganismoId &&
              t.empleadoId == detalle.value.idPromovente,
          );
      }
      break;
    default:
      break;
  }
}
async function cambianParametros(val) {
  formPromocion.value?.resetValidation();
  if (val) {
    parametros.value = { ...val.value };
  }
  const fileValido =
    !esEdicion.value ||
    (esEdicion.value && detalle.value.nombreArchivo == null) ||
    (esEdicion.value &&
      detalle.value.nombreArchivo != null &&
      fileRequerido.value &&
      parametros.value.archivoAVincular != null) ||
    (esEdicion.value &&
      detalle.value.nombreArchivo != null &&
      !fileRequerido.value &&
      parametros.value.archivoAVincular == null);
  const formValido = await formPromocion.value?.validate(false);
  validoPaso1.value = formValido && fileValido;
  setTimeout(() => {
    formPromocion.value?.validate(false).then((formularioValido) => {
      validoPaso1.value = formularioValido && fileValido;
    });
  }, 300);
  cambioForm.value = true;
}
async function reseteaForm(limpiarTodo = false) {
  const paramsCopy = { ...parametros.value };
  parametros.value = new FormPromocion();
  if (!esEdicion.value) {
    try {
      await oficialiaStore.calculaRegistro();
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    parametros.value.registro = oficialiaStore.noRegistro;
  } else {
    parametros.value.registro = paramsCopy.registro;
    parametros.value.secretario = paramsCopy.secretario;
  }
  if (
    paramsCopy.tipoAsunto?.catTipoAsuntoId ==
      paramsCopy.expedienteEncontrado?.catTipoAsuntoId &&
    esEdicion.value
  ) {
    parametros.value.contenido = paramsCopy.contenido;
  }
  formPromocion.value?.reset();
  formPromocion.value?.resetValidation();
  if (props.promocion?.estado === 1) {
    parametros.value.fechaPresentacion = paramsCopy.fechaPresentacion;
    parametros.value.horaPresentacion = paramsCopy.horaPresentacion;
  } else {
    parametros.value.fechaPresentacion = date.formatDate(
      Date.now(),
      "DD/MM/YYYY",
    );
    parametros.value.horaPresentacion = date.formatDate(Date.now(), "HH:mm");
  }
  if (!expedienteNuevo.value && !limpiarTodo) {
    parametros.value.expedienteEncontrado = paramsCopy.expedienteEncontrado;
  }
  validoPaso1.value = false;
  cambioForm.value = false;
  if (!expedienteNuevo.value) formPromocion.value?.focus();
  if (
    origenesAsociarPromo.some((o) => o == props.promocion?.origen) ||
    origenesCrearPromo.some((o) => o == props.promocion?.origen)
  ) {
    parametros.value.origen = detalle.value.origenPromocionId;
    parametros.value.origenDescripcion = detalle.value.origenPromocion;
  }
}

async function submitForm() {
  if (/^\d+$/.test(parametros.value.numeroOCC)) {
    const year = new Date().getFullYear();
    parametros.value.numeroOCC += "/" + year;
  }
  if (!(await formPromocion.value?.validate(false))) return;
  const messageLoad =
    "Vinculando promoción número <b>" +
    parametros.value.registro +
    "</b> al expediente <b>" +
    parametros.value.numeroExpediente +
    "</b>.";
  mensajeLoader.value = messageLoad;
  cargandoGuardado.value = true;
  let correcto = ref(false);
  //Se eliminan los anexos en bd
  if (parametros.value.anexosAEliminar?.length > 0) {
    try {
      await Promise.all(
        parametros.value.anexosAEliminar.map((x) =>
          oficialiaStore.eliminarAnexo(x),
        ),
      );
      correcto.value = true;
      noty.correcto(`¡Anexo eliminado exitosamente!`);
    } catch (error) {
      correcto.value = false;
      manejoErrores.mostrarError(error);
    }
  }
  correcto.value = await guardarPasoPromocion();
  if (!correcto.value) {
    cargandoGuardado.value = false;
    return;
  }

  let subioArchivos = false;
  let subioAnexos = false;
  let responseAnexos = [];

  const year =
    parametros.value.fechaPresentacion?.split("/")[2] ||
    date.formatDate(Date.now(), "DD/MM/YYYY").split("/")[2];
  let data = new FormData();
  if (parametros.value.archivoAVincular !== null) {
    let soloFojas = false;
    data.append("noRegistro", parametros.value.registro);
    data.append("numeroOrden", parametros.value.numeroOrden);
    data.append("origen", parametros.value.origen);
    data.append("fojas", parametros.value.fojas);
    data.append("yearPromocion", year);
    if (parametros.value.archivoAVincular !== null) {
      data.append(
        parametros.value.archivoAVincular.name,
        Utils.blobToFile(
          parametros.value.archivoAVincular.blob,
          parametros.value.archivoAVincular.name,
        ),
        parametros.value.archivoAVincular.name,
      );
    } else {
      soloFojas = true;
    }
    if (
      cambioExpedienteaPromocion.value &&
      esEdicion.value &&
      parametros.value.expedienteEncontrado === null
    ) {
      data.append("asuntoNeunId", parametros.value.AsuntoNeunIdNuevo);
    } else if (
      cambioExpedienteaPromocion.value &&
      esEdicion.value &&
      parametros.value.expedienteEncontrado !== null
    ) {
      data.append(
        "asuntoNeunId",
        parametros.value.expedienteEncontrado.asuntoNeunId,
      );
    } else {
      data.append(
        "asuntoNeunId",
        parametros.value.asuntoNeunId ?? originalAsuntoNeunId.value,
      );
    }
    try {
      await oficialiaStore.subirArchivo(data);
      subioArchivos = true;
      if (!soloFojas) {
        noty.correcto("¡Archivo guardado exitosamente!");
      }
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  } else if (
    detalle.value.nombreArchivo === null &&
    ((detalle.value && detalle.value?.conArchivo != 1) || !detalle.value)
  ) {
    noty.precaucion(
      `Falta vincular el archivo de la promoción ${parametros.value.registro} para ser asignada a una mesa`,
    );
    subioArchivos = true;
  } else {
    subioArchivos = true;
  }

  // guarda anexos
  if (
    parametros.value.anexos &&
    parametros.value.anexos.filter((x) => !x.guardadoEnBD).length > 0
  ) {
    let dataForm = new FormData();
    let asuntoNeunIdAnexo =
      parametros.value.asuntoNeunId ?? parametros.value.AsuntoNeunIdNuevo;
    dataForm.append("noRegistro", parametros.value.registro);
    dataForm.append(
      "asuntoNeunId",
      asuntoNeunIdAnexo ?? originalAsuntoNeunId.value,
    );
    dataForm.append("numeroOrden", parametros.value.numeroOrden);
    dataForm.append("origen", parametros.value.origen);
    dataForm.append("yearPromocion", year);
    dataForm.append("fojas", 0);

    const newData = parametros.value.anexos
      .filter((x) => !x.guardadoEnBD)
      .map((item) => {
        const newItem = { ...item }; // Crear una copia del objeto original
        delete newItem.file; // Eliminar el atributo "file" del nuevo objeto
        return newItem;
      });

    dataForm.append("archivos", JSON.stringify(newData));
    parametros.value.anexos
      .filter((x) => !x.guardadoEnBD)
      .forEach((element) => {
        if (element.file) {
          dataForm.append(
            element.file.name,
            Utils.blobToFile(element.file.blob, element.file.name),
            element.file.name,
          );
        } else {
          dataForm.append(element.name, element.name);
        }
      });

    try {
      responseAnexos = await oficialiaStore.subirAnexos(dataForm);
      noty.correcto("¡Anexo guardado exitosamente!");
    } catch (error) {
      manejoErrores.mostrarError(error);
    }

    subioAnexos = !responseAnexos.some((a) => !a.guardadoEnBD);
    correcto.value = subioArchivos && subioAnexos;
  } else {
    correcto.value = subioArchivos;
  }
  cargandoGuardado.value = false;
  if (correcto.value) {
    const paramsCopy = { ...parametros.value };
    emit("refrescar-tabla");
    if (parametros.value.archivoAVincular !== null) {
      const jsonQr = {"E": {"N": data.get("asuntoNeunId")},"P": {"NP": `${paramsCopy.registro}/${paramsCopy.fechaPresentacion.split("/")[2]}`,"O": paramsCopy.numeroOrden},
      };
      descripcionQr.value = `${authStore.user?.nombreOficial}
    <br/>
    ${paramsCopy.numeroExpediente}
    <br/>
    ${data.get("asuntoNeunId")}
    <br/>
    ${paramsCopy.fechaPresentacion}
    <br/>
    ${paramsCopy.horaPresentacion}
    <br/>
    ${paramsCopy.secretario.completo}`;
      jsonParaQr.value = JSON.stringify(jsonQr);
      generateQrCode.value = true;
    }
    await reseteaForm(true);
    usuariosStore.parteExistente = [];
    usuariosStore.promoventeExistente = [];
    usuariosStore.autoridadJudicial = [];
    if (props.title.includes("Editar")) {
      emit("cerrar", false);
    }
  }
  scrollAreaRef.value.setScrollPosition("vertical", 0);
  stePromocionRef.value.setFocusExpediente();
}

onBeforeUnmount(() => {
  stopWatch();
  stopWatchAnexo();
});
async function guardarPasoPromocion() {
  const correcto = ref(false);
  if (origenesAsociarPromo.some((o) => o == props.promocion?.origen)) {
    parametros.value.expedienteEncontrado = props.promocion?.expediente;
    if (!detalle.value.numeroRegistro) {
      esEdicion.value = false;
    }
    if (!esEdicion.value && cambioExpedienteaPromocion.value) {
      expedienteNuevo.value = parametros.value.expedienteEncontrado == null;
      cambioExpedienteaPromocion.value = false;
    }
  }
  if (origenesCrearPromo.some((o) => o == props.promocion?.origen)) {
    if (!detalle.value.numeroRegistro) {
      esEdicion.value = false;
      expedienteNuevo.value = parametros.value.expedienteEncontrado == null;
      if (!cambioExpedienteaPromocion.value)
        expedienteNuevo.value = parametros.value.expedienteEncontrado != null;
    }
  }

  parametros.value.secretarioId = parametros.value.secretario?.empleadoId;
  if (parametros.value.anexos) {
    parametros.value.numeroAnexos = parametros.value?.anexos.length;
  } else {
    parametros.value.numeroAnexos = 0;
  }
  if (parametros.value.contenido == null) {
    let auxCont = {
      id: 2615,
      descripcion: "",
    };
    parametros.value.contenido = auxCont;
  }
  if (expedienteNuevo.value || esEdicion.value) {
    parametros.value.asuntoNeunId = 0;
    if (cambioFormPromocion.value || esEdicion.value) {
      if (esEdicion.value) {
        parametros.value.asuntoNeunId =
          props.promocion?.expediente?.asuntoNeunId;
        parametros.value.numeroOrden = props.promocion?.numeroOrden;
        parametros.value.yearPromocion = detalle.value.yearPromocion;
        parametros.value.AsuntoNeunIdNuevo = null;
        if (
          (cambioExpedienteaPromocion.value ||
            parametros.value.numeroOCC !== originalNumeroOCC.value) &&
          esEdicion.value &&
          parametros.value.expedienteEncontrado === null
        ) {
          try {
            const parametrosInsertar = {
              catTipoAsuntoId: parametros.value.tipoAsunto.catTipoAsuntoId,
              numeroOCC: parametros.value.numeroOCC,
              noExpediente: parametros.value.numeroExpediente,
              tipoProcedimiento: parametros.value.tipoProcedimiento?.id,
              // piAsuntoNeunId: cambioExpedienteaPromocion.value
              //   ? null
              //   : parametros.value.asuntoNeunId,
              piAsuntoNeunId: (esEdicion.value = true
                ? parametros.value.asuntoNeunId
                : null),
              //en teoria en la linea anterior no debería de entrar nunca a caso nulo, se deja la
              //condición y el código anterior en caso de existir un escenario no contemplado
              //en las pruebas para su posterior validación.
              //comentado por: oiguerrero

              //esActualizacion: cambioExpedienteaPromocion.value ? 0 : 1,
              esActualizacion: (esEdicion.value = true ? 1 : 0),
              //Condición anterior para diferenciar entre actualización y nueva promoción
              //esActualizacion: esEdicion.value = true ? 1 : 0,
            };
            const newPromocionExpediente =
              await oficialiaStore.expedienteInsertarPromocion(
                parametrosInsertar,
              );
            cambioExpedienteInsertar.value = true;
            parametros.value.AsuntoNeunIdNuevo =
              newPromocionExpediente.asuntoNeunId;
          } catch (error) {
            manejoErrores.mostrarError(error);
          }
        } else if (
          cambioExpedienteaPromocion.value &&
          esEdicion.value &&
          parametros.value.expedienteEncontrado !== null
        ) {
          parametros.value.AsuntoNeunIdNuevo =
            parametros.value.expedienteEncontrado?.asuntoNeunId;
          parametros.value.asuntoNeunId = originalAsuntoNeunId.value;
        }
        try {
          await oficialiaStore.editarPromocion(parametros.value);
          correcto.value = true;
          noty.correcto(
            `Se ha modificado la promoción ${
              parametros.value.registro
            } del expediente ${parametros.value.numeroExpediente} ${
              parametros.value.archivoAVincular !== null
                ? `y asignada a la ${parametros.value.secretario.area}`
                : ""
            }`,
          );
        } catch (error) {
          correcto.value = false;
          manejoErrores.mostrarError(error);
        }
      } else {
        try {
          await oficialiaStore.crearPromocion({
            ...parametros.value,
            origen: origenesCrearPromo.some((o) => o == props.promocion?.origen)
              ? props.promocion?.origen
              : parametros.value.origen,
            folio: detalle.value.folio || 0,
          });
          correcto.value = true;
          noty.correcto(
            `Se ha capturado la promoción ${
              parametros.value.registro
            } en el expediente ${parametros.value.numeroExpediente} ${
              parametros.value.archivoAVincular !== null ||
              detalle.value?.conArchivo == 1
                ? `y asignada a la ${parametros.value.secretario.area}`
                : ""
            }`,
          );
        } catch (error) {
          correcto.value = false;
          manejoErrores.mostrarError(error);
        }
      }
    } else {
      if (esEdicion.value) {
        correcto.value = true;
      }
    }
    if (correcto.value) {
      if (!esEdicion.value) {
        parametros.value.asuntoNeunId = oficialiaStore.asuntoNeunId;
        parametros.value.numeroOrden = oficialiaStore.numeroOrden;
      } else {
        parametros.value.asuntoNeunId = parametros.value.AsuntoNeunIdNuevo;
        parametros.value.numeroOrden = props.promocion?.numeroOrden;
      }
      cambioFormPromocion.value = false;
      cambioForm.value = false;
    } else {
      return false;
    }
  } else {
    correcto.value = await asociarPromocion();
    if (!correcto.value) return false;
    noty.correcto(
      `Se ha vinculado la promoción ${
        parametros.value.registro
      } al expediente ${
        props.promocion?.esPromocionE &&
        !origenesCrearPromo.some((o) => o == props.promocion?.origen)
          ? props.promocion.expediente?.asuntoAlias
          : parametros.value.expedienteEncontrado?.asuntoAlias
      } ${
        parametros.value.archivoAVincular !== null ||
        detalle.value?.conArchivo == 1
          ? `y asignada a la ${parametros.value.secretario?.area}`
          : ""
      }`,
    );
    parametros.value.numeroOrden = oficialiaStore.numeroOrden;
  }
  // guarda promoventes
  if (!esEdicion.value) {
    correcto.value = await guardarPromovente();
    cambioFormPromovente.value = correcto.value
      ? false
      : cambioFormPromovente.value;
  }
  return correcto.value;
}
async function guardarPromovente() {
  if (cambioFormPromovente.value || cambioExpedienteInsertar.value) {
    let correcto = false;
    const parteExistente = parametros.value.tipoParte === "parteExistente";
    const esPromoventeExistente = parametros.value.esPromoventeExistente;
    const personaAsunto = new GuardarPersonasAsuntos();

    if (
      parametros.value.tipoPromovente == "parte" ||
      parametros.value.tipoPromovente == "promovente"
    ) {
      personaAsunto.asuntoNeunId =
        parametros.value.asuntoNeunId ?? originalAsuntoNeunId.value;
      personaAsunto.nombre = parteExistente
        ? parametros.value.promoventeAutoridadExistente?.nombre
        : parametros.value.parteNombre;

      if (personaAsunto.nombre == undefined) {
        personaAsunto.nombre = "";
      }

      personaAsunto.aPaterno = parteExistente
        ? parametros.value.promoventeAutoridadExistente?.aPaterno
        : parametros.value.parteApellidoPaterno;

      if (personaAsunto.aPaterno == undefined) {
        personaAsunto.aPaterno = "";
      }

      personaAsunto.aMaterno = parteExistente
        ? parametros.value.promoventeAutoridadExistente?.aMaterno
        : parametros.value.parteApellidoMaterno;

      if (personaAsunto.aMaterno == undefined) {
        personaAsunto.aMaterno = "";
      }

      personaAsunto.denominacionDeAutoridad = parteExistente
        ? parametros.value.promoventeAutoridadExistente?.denominacionDeAutoridad
        : parametros.value.denominacionDeAutoridad;

      if (personaAsunto.denominacionDeAutoridad == undefined) {
        personaAsunto.denominacionDeAutoridad = "";
      }

      personaAsunto.catTipoPersonaId = parteExistente
        ? parametros.value.promoventeAutoridadExistente?.tipo
        : parametros.value.parteCatTipoPersona?.catTipoPersonaId;

      if (personaAsunto.catTipoPersonaId == undefined) {
        personaAsunto.catTipoPersonaId = 1;
      }

      personaAsunto.catCaracterPersonaAsuntoId = parteExistente
        ? parametros.value.promoventeAutoridadExistente
            ?.catCaracterPersonaAsuntoId
        : parametros.value.parteCatTipoPersonaCaracter?.caracterPersonaId;

      if (personaAsunto.catCaracterPersonaAsuntoId == undefined) {
        personaAsunto.catCaracterPersonaAsuntoId = 13;
      }

      personaAsunto.numeroOrden = parametros.value.numeroOrden;
    }

    const promovente = new GuardarPromoventes();

    // configurar con respecto al tipo de parte
    if (parametros.value.tipoPromovente == "promovente") {
      promovente.asuntoNeunId =
        parametros.value.asuntoNeunId ?? originalAsuntoNeunId.value;
      if (esPromoventeExistente) {
        if (parametros.value.promoventeExistente == undefined) {
          promovente.aPaterno = "";
          promovente.aMaterno = "";
          promovente.nombre = "";
          promovente.tipo = undefined;
        } else {
          promovente.aPaterno = parametros.value.promoventeExistente.aPaterno;
          promovente.aMaterno = parametros.value.promoventeExistente.aMaterno;
          promovente.nombre = parametros.value.promoventeExistente.nombre;
          promovente.tipo = parametros.value.promoventeExistente.tipo;
        }
      } else {
        promovente.aPaterno = parametros.value.promoventeApellidoPaterno;
        promovente.aMaterno = parametros.value.promoventeApellidoMaterno;
        promovente.nombre = parametros.value.promoventeNombre;
        promovente.tipo = parametros.value.tipoPromoventeCat?.id;
      }
      promovente.numeroOrden = parametros.value.numeroOrden;
    }
    const autoridad = new GuardarAutoridadJudicial();
    autoridad.asuntoNeunId =
      parametros.value.asuntoNeunId ?? originalAsuntoNeunId.value;
    autoridad.empleadoId = parametros.value.promoventeAutoridad?.empleadoId;
    autoridad.catIdOrganismo =
      parametros.value.promoventeAutoridad?.catOrganismoId;
    autoridad.numeroOrden = parametros.value.numeroOrden;
    if (!parametros.value.clasePromovente) {
      switch (parametros.value.tipoPromovente) {
        case "parte":
          parametros.value.clasePromovente = 1;
          break;
        case "promovente":
          parametros.value.clasePromovente = 2;
          break;
        case "autoridad":
          parametros.value.clasePromovente = 3;
          break;
        default:
          parametros.value.clasePromovente = 0;
          break;
      }
    }
    if (!parametros.value.isSaved) {
      switch (parametros.value.clasePromovente) {
        case 2:
          {
            if (
              personaAsunto.catTipoPersonaId &&
              personaAsunto.catCaracterPersonaAsuntoId
            ) {
              try {
                await promoventesStore.crearPersonasAsunto(personaAsunto);
                correcto = true;
                noty.correcto("¡Parte guardada exitosamente!");
              } catch (error) {
                correcto = false;
                manejoErrores.mostrarError(error);
              }
              promovente.personaId = promoventesStore.personaId;
            } else {
              correcto = true;
            }
            if (!correcto) {
              cargandoGuardado.value = false;
              return false;
            }
            try {
              await promoventesStore.crearPromovente(promovente);
              correcto = true;
              noty.correcto("¡Promovente guardado exitosamente!");
            } catch (error) {
              correcto = false;
              manejoErrores.mostrarError(error);
            }
          }
          break;
        case 1:
          {
            try {
              if (personaAsunto.catTipoPersonaId != 1) {
                personaAsunto.nombre = personaAsunto.denominacionDeAutoridad;
              }
              await promoventesStore.crearPersonasAsunto(personaAsunto);
              correcto = true;
              noty.correcto("¡Parte guardada exitosamente!");
            } catch (error) {
              correcto = false;
              manejoErrores.mostrarError(error);
            }
          }
          break;
        case 3:
          {
            if (newAutoridad.value) {
              try {
                await promoventesStore.crearAutoridadJudicial(autoridad);
                correcto = true;
                noty.correcto("¡Autoridad guardada exitosamente!");
              } catch (error) {
                correcto = false;
                manejoErrores.mostrarError(error);
              }
            } else {
              correcto = true;
            }
          }
          break;
        default:
          break;
      }
    } else {
      correcto = true;
    }
    parametros.value.isSaved = false;
    return correcto;
  }
  return true;
}

async function verBoletaOCC() {
  await child.value.mostrarPdf(detalle.value?.boletaOCC);
}

async function asociarPromocion() {
  let correcto = false;
  //const tiposPromovente = ["default", "parte", "promovente", "autoridad"];
  const params = new InsertarPromocion();
  params.asuntoNeunId = parametros.value.asuntoNeunId;
  params.tipoCuaderno = parametros.value.cuaderno?.cuadernoId;
  params.fechaPresentacion = parametros.value.fechaPresentacion;
  params.horaPresentacion = parametros.value.horaPresentacion;
  params.clasePromocion = 0;
  params.conExpedienteElectronico =
    props.promocion?.origen == 22 && props.promocion?.expediente?.asuntoNeunId;
  /*params.clasePromovente = tiposPromovente.findIndex(
    (t) => t === parametros.value.tipoPromovente,
  );*/
  if (parametros.value.clasePromovente >= 0) {
    params.clasePromovente = parametros.value.clasePromovente;
  }
  if (parametros.value.tipoDePromovente) {
    params.tipoPromovente = parametros.value.tipoDePromovente;
  } /*else if (
    parametros.value.promoventeAutoridadExistente?.personaId &&
    !parametros.value.tipoPromoventeCat?.id
  ) {
    params.tipoPromovente =
      parametros.value.promoventeAutoridadExistente.personaId;
  } else if (parametros.value.tipoPromoventeCat?.id) {
    params.tipoPromovente = parametros.value.tipoPromoventeCat?.id;
  } else if (parametros.value.promoventeAutoridad?.empleadoId) {
    params.tipoPromovente = parametros.value.promoventeAutoridad?.empleadoId;
  } else {
    params.tipoPromovente = 0;
  }*/
  params.tipoContenido = parametros.value.contenido?.id;

  if (parametros.value.copias == null) {
    params.numeroCopias = 0;
  } else {
    params.numeroCopias = parametros.value.copias;
  }
  params.numeroAnexo = parametros.value.anexos?.length;
  params.secretario = parametros.value.secretario?.empleadoId;
  params.origenPromocion = parametros.value.origen;
  params.numeroRegistro = parametros.value.registro;
  // parametros para alerta
  params.numeroExpediente =
    props.promocion?.esPromocionE &&
    !origenesCrearPromo.some((o) => o == props.promocion?.origen)
      ? props.promocion.expediente?.asuntoAlias
      : parametros.value.expedienteEncontrado?.asuntoAlias;
  params.tipoAsunto =
    props.promocion?.esPromocionE &&
    !origenesCrearPromo.some((o) => o == props.promocion?.origen)
      ? parametros.value.tipoAsunto.catTipoAsuntoId
      : parametros.value.expedienteEncontrado?.tipoAsunto;
  params.tipoProcedimiento =
    props.promocion?.esPromocionE &&
    !origenesCrearPromo.some((o) => o == props.promocion?.origen)
      ? parametros.value?.tipoProcedimiento?.id
      : parametros.value.expedienteEncontrado?.tipoProcedimiento;
  params.archivoAVincular = parametros.value.archivoAVincular !== null;
  params.mesa = parametros.value.secretario?.area;
  if (parametros.value.fojas == null) {
    params.fojas = 0;
  } else {
    params.fojas = parametros.value.fojas;
  }
  params.folio = detalle.value.folio || 0;
  params.origen = props.promocion?.esPromocionE ? props.promocion?.origen : 0;
  try {
    await oficialiaStore.asociarPromocion(params);
    correcto = true;
  } catch (error) {
    manejoErrores.mostrarError(error);
    correcto = false;
  }
  return correcto;
}
//--
</script>
<style scoped>
.min-step {
  min-height: 50vh;
}
.spliterBefore {
  height: inherit;
}
</style>
