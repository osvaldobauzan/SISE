<template>
  <q-card style="min-width: 70vw">
    <q-toolbar>
      <q-toolbar-title class="text-bold"> {{ title }}</q-toolbar-title>
      <q-btn
        flat
        round
        dense
        icon="mdi-close"
        @click="emit('cerrar', cambioForm)"
      />
    </q-toolbar>
    <q-separator></q-separator>
    <q-splitter v-model="splitterModel" :limits="[0, 50]">
      <template v-slot:before>
        <VerPromociones
          v-if="detalle.nombreArchivo !== null || detalle.numeroAnexos > 0"
          style="width: 100%"
          promocionDocumento
          :promocion="promocion"
          :es-edicion="esEdicion"
          :esDetalle="false"
        />
      </template>
      <template v-slot:after>
        <q-scroll-area style="width: 100%; height: 595px">
          <q-card-section>
            <q-form @submit="submitForm" ref="formPromocion" no-focus-error>
              <!-- todo: quita col cuando se reduzca el tamaño de la pantalla-->
              <step-promocion
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
                @show-oficio="
                  (val) => {
                    showOficioLibre = true;
                    oficioLibre = val;

                    // console.log(props);
                  }
                "
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
                @event:cancelarReemplazo="parametros.archivoAVincular = null"
                @params:cambio="
                  (val) => {
                    cambianParametros(val);
                  }
                "
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
          ></GenerateQr>
        </q-scroll-area>
        <q-separator></q-separator>
        <q-card-actions align="left">
          <q-btn
            style="min-width: 164px"
            no-caps
            :color="!validoPaso1 ? 'grey-6' : 'primary'"
            @click="validoPaso1 ? submitForm() : null"
            :disable="!validoPaso1"
            :label="!esEdicion ? 'Crear oficios' : 'Guardar'"
            class="q-ml-sm q-mr-sm"
          />
          <q-btn
            @click="emit('cerrar', cambioForm)"
            outline
            no-caps
            label="Cancelar"
            :color="'secondary'"
            :style="!esEdicion ? 'min-width: 210px' : 'min-width: 164px'"
          />
        </q-card-actions>
      </template>
    </q-splitter>
  </q-card>
</template>

<script setup>
import { date, Loading, QSpinner } from "quasar";
import { ref, onMounted, watch, onBeforeUnmount } from "vue";
// import promocionPDF from "assets/PromocionPrueba.pdf";
//import { Validaciones } from '../../../helpers/validaciones';
import { useLibretaStore } from "../stores/libreta-store";
//import { usePromoventesStore } from "../stores/promoventes-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { FormPromocion } from "../data/form-promocion";
import { loader } from "src/helpers/loader";
import { Promocion } from "../data/promocion";
import StepPromocion from "./StepPromocion.vue";
// import StepAnexos from "./StepAnexos.vue";
// import StepPromovente from "./StepPromovente.vue";
// import StepVincularPromocion from "./StepVincularPromocion.vue";
// import VerPromociones from "../components/VerPromociones.vue";
// import InfoPromocion from "./InfoPromocion.vue";
// import { GuardarPersonasAsuntos } from "../data/guardar-personas-asuntos";
// import { GuardarAutoridadJudicial } from "../data/guardar-autoridad-judicial";
// import { GuardarPromoventes } from "../data/guardar-promoventes";
// import { InsertarPromocion } from "../data/insertar-promocion";
import GenerateQr from "src/components/GenerateQr.vue";
import { DetallePromocion } from "../data/detalle-promocion";
import { noty } from "src/helpers/notify";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { Utils } from "src/helpers/utils";

const authStore = useAuthStore();
const usuariosStore = useUsuariosStore();
const generateQrCode = ref(false);
const jsonParaQr = ref("");
const descripcionQr = ref("");
const detalle = ref(new DetallePromocion());
const formPromocion = ref(null);
const esEditarAnexo = ref(false);
// const formVincular = ref(null);
const oficialiaStore = useLibretaStore();
//const promoventesStore = usePromoventesStore();
const validoPaso1 = ref(false);
// const nombreArchivo = ref(promocionPDF);
const splitterModel = ref(0);
const tieneAnexos = ref(false);
let parametros = ref(new FormPromocion());
const cambioFormPromocion = ref(false);
const cambioFormPromovente = ref(false);
const expedienteNuevo = ref(false);
const cambioExpedienteaPromocion = ref(false);
const cambioForm = ref(false);
const originalAsuntoNeunId = ref(null);

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
    type: Promocion,
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
  // v-model event with validation
  cerrar: (value) => value !== null,
  "add:anexo": () => true,
  "update:anexo": (value) => value !== null,
  "refrescar-tabla": () => true,
});
const stopWatch = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.addAnexo?.value,
  async (_newValue) => {
    // do something
    if (!esEditarAnexo.value && _newValue && _newValue.caracter) {
      if (!parametros.value.anexos) {
        parametros.value.anexos = [];
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
const stopWatchAnexo = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.updateAnexo?.value,
  async (_newValue) => {
    // do something
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
  esEdicion.value = false;
  originalAsuntoNeunId.value = props.promocion?.expediente?.asuntoNeunId;
  //tab.value = "asociarExpediente";
  if (props.promocion) {
    esEdicion.value = true;
    const parametrosRequest = {
      asuntoNeunId: props.promocion.expediente.asuntoNeunId,
      origen: props.promocion.origen,
      numeroOrden: props.promocion.numeroOrden,
      yearPromocion: props.promocion.yearPromocion,
      kIdElectronica: props.promocion.kIdElectronica,
      catOrganismoId: props.promocion.expediente.catOrganismoId,
      esPromocionE: props.promocion.esPromocionE,
      estado: props.promocion.estado,
    };
    try {
      await oficialiaStore.detallePromocion(parametrosRequest);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    detalle.value = oficialiaStore.promocion;
    try {
      await usuariosStore.obtenerParteExistente(parametros.value.asuntoNeunId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    try {
      await usuariosStore.obtenerPromoventeExistente(
        parametros.value.asuntoNeunId,
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
    parametros.value.numeroOCC = detalle.value.occ;

    parametros.value.asuntoNeunId = props.promocion.expediente.asuntoNeunId;
    parametros.value.numeroOrden = props.promocion.numeroOrden;

    // const expeditenTipoAsuntoId = parseInt(detalle.value.catTipoAsuntoId);
    //myForm.value.resetValidation();

    parametros.value.fechaPresentacion = date.formatDate(
      new Date(detalle.value.fechaPresentacion),
      "DD/MM/YYYY",
    );
    // parametros.value.horaPresentacion = date.formatDate(Date.now(), "HH:mm:ss");
    parametros.value.horaPresentacion = detalle.value.horaPresentacion;
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

  if (detalle.value.nombreArchivo !== null || detalle.value.numeroAnexos > 0) {
    splitterModel.value = 50;
  } else {
    splitterModel.value = 0;
  }
  formPromocion.value?.focus();
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
            (t) => t.empleadoId == detalle.value.idPromovente,
          );
      }
      break;
    default:
      break;
  }
}
async function cambianParametros(val) {
  if (val) {
    parametros.value = { ...val.value };
  }
  validoPaso1.value = await formPromocion.value?.validate(false);
  cambioForm.value = true;
}
async function reseteaForm(limpiarTodo = false) {
  const paramsCopy = { ...parametros.value };
  parametros.value = new FormPromocion();
  // if (!esEdicion.value) {
  //   try {
  //     await oficialiaStore.calculaRegistro();
  //   } catch (error) {
  //     manejoErrores.mostrarError(error);
  //   }
  //   parametros.value.registro = oficialiaStore.noRegistro;

  // } else {
  //   parametros.value.registro = paramsCopy.registro;
  //   parametros.value.secretario = paramsCopy.secretario;
  // }
  if (
    paramsCopy.tipoAsunto?.catTipoAsuntoId ==
      paramsCopy.expedienteEncontrado?.catTipoAsuntoId &&
    esEdicion.value
  ) {
    parametros.value.contenido = paramsCopy.contenido;
  }
  formPromocion.value?.reset();
  formPromocion.value?.resetValidation();
  parametros.value.fechaPresentacion = date.formatDate(
    Date.now(),
    "DD/MM/YYYY",
  );
  parametros.value.horaPresentacion = date.formatDate(Date.now(), "HH:mm:ss");
  if (!expedienteNuevo.value && !limpiarTodo) {
    parametros.value.expedienteEncontrado = paramsCopy.expedienteEncontrado;
  }
  validoPaso1.value = false;
  cambioForm.value = false;
  if (!expedienteNuevo.value) formPromocion.value?.focus();
}

async function submitForm() {
  const messageLoad =
    "Generando oficios " +
    " para el expediente " +
    parametros.value.numeroExpediente +
    ".";
  Loading.show({
    spinnerColor: "primary",
    spinner: QSpinner,
    message: messageLoad,
  });
  let correcto = ref(false);

  correcto.value = await generarOficio();
  if (!correcto.value) {
    loader.hide();
    await reseteaForm(true);
    emit("cerrar", false);
    return;
  }

  let subioArchivos = false;
  let subioAnexos = false;
  let responseAnexos = [];

  const year =
    parametros.value.fechaPresentacion?.split("/")[2] ||
    date.formatDate(Date.now(), "DD/MM/YYYY").split("/")[2];
  if (
    parametros.value.archivoAVincular !== null ||
    (esEdicion.value &&
      detalle.value.nombreArchivo !== null &&
      parametros.value.fojas != detalle.value.fojas)
  ) {
    let data = new FormData();
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
      data.append("asuntoNeunId", parametros.value.asuntoNeunId);
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
  } else if (detalle.value.nombreArchivo === null) {
    noty.precaucion(
      `Falta vincular el archivo de la promoción ${parametros.value.registro} para ser asignada a una mesa`,
    );
    subioArchivos = true;
  } else {
    subioArchivos = true;
  }

  // ir a guardar anexos
  if (
    parametros.value.anexos &&
    parametros.value.anexos.filter((x) => !x.guardadoEnBD).length > 0
  ) {
    let dataForm = new FormData();
    dataForm.append("noRegistro", parametros.value.registro);
    dataForm.append("asuntoNeunId", parametros.value.asuntoNeunId);
    dataForm.append("numeroOrden", parametros.value.numeroOrden);
    dataForm.append("origen", parametros.value.origen);
    dataForm.append("yearPromocion", year);
    dataForm.append("fojas", parametros.value.fojas);

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
  if (parametros.value.anexosAEliminar.length > 0) {
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
  loader.hide();
  if (correcto.value) {
    emit("refrescar-tabla");
    if (parametros.value.archivoAVincular !== null) {
      const paramsCopy = { ...parametros.value };
      const jsonQr = {
        Expediente: {
          AsuntoNeunId: paramsCopy.asuntoNeunId,
          AsuntoAlias: paramsCopy.numeroExpediente,
          CatTipoAsuntoId: paramsCopy.tipoAsunto.catTipoAsuntoId,
          CatTipoAsunto: paramsCopy.tipoAsunto.tipoAsunto,
          TipoProcedimientoId: paramsCopy.tipoProcedimiento?.id,
          TipoProcedimiento: paramsCopy.tipoProcedimiento?.descripcion,
          CatOrganismoId: authStore.user?.catOrganismoId,
          CatOrganismo: authStore.user?.nombreOficial,
        },
        Promocion: {
          CuadernoId: paramsCopy.cuaderno.cuadernoId,
          Cuaderno: paramsCopy.cuaderno.cuaderno,
          FechaPresentacion: paramsCopy.fechaPresentacion,
          NumeroRegistro: paramsCopy.registro,
          YearPromocion: paramsCopy.fechaPresentacion.split("/")[2],
        },
      };
      descripcionQr.value = `${jsonQr.Expediente.CatOrganismo}
    <br/>
    ${paramsCopy.numeroExpediente}
    <br/>
    ${paramsCopy.asuntoNeunId}
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
    if (props.title.includes("Editar")) {
      emit("cerrar", false);
    }
  }
}
onBeforeUnmount(() => {
  stopWatch();
  stopWatchAnexo();
});
async function generarOficio() {
  noty.correcto(
    `Se han generados los oficios  la promoción del expediente ${parametros.value.numeroExpediente}
            `,
  );
  return true;
}
// async function guardarPasoPromocion() {
//   const correcto = ref(false);
//   parametros.value.secretarioId = parametros.value.secretario.empleadoId;
//   if (parametros.value.anexos) {
//     parametros.value.numeroAnexos = parametros.value?.anexos.length;
//   } else {
//     parametros.value.numeroAnexos = 0;
//   }
//   if (expedienteNuevo.value || esEdicion.value) {
//     parametros.value.asuntoNeunId = 0;

//     if (cambioFormPromocion.value || esEdicion.value) {
//       if (esEdicion.value) {
//         parametros.value.asuntoNeunId = props.promocion.expediente.asuntoNeunId;
//         parametros.value.numeroOrden = props.promocion.numeroOrden;
//         parametros.value.yearPromocion = detalle.value.yearPromocion;
//         parametros.value.AsuntoNeunIdNuevo = null;
//         if (
//           cambioExpedienteaPromocion.value &&
//           esEdicion.value &&
//           parametros.value.expedienteEncontrado === null
//         ) {
//           try {
//             const parametrosInsertar = {
//               catTipoAsuntoId: parametros.value.tipoAsunto.catTipoAsuntoId,
//               numeroOCC: parametros.value.numeroOCC,
//               noExpediente: parametros.value.numeroExpediente,
//               tipoProcedimiento: parametros.value.tipoProcedimiento?.id,
//             };
//             const newPromocionExpediente =
//               await oficialiaStore.expedienteInsertarPromocion(
//                 parametrosInsertar
//               );
//             parametros.value.AsuntoNeunIdNuevo =
//               newPromocionExpediente.asuntoNeunId;
//           } catch (error) {
//             manejoErrores.mostrarError(error);
//           }
//         } else if (
//           cambioExpedienteaPromocion.value &&
//           esEdicion.value &&
//           parametros.value.expedienteEncontrado !== null
//         ) {
//           parametros.value.AsuntoNeunIdNuevo =
//             parametros.value.expedienteEncontrado.asuntoNeunId;
//           parametros.value.asuntoNeunId = originalAsuntoNeunId.value;
//         }
//         try {
//           await oficialiaStore.editarPromocion(parametros.value);
//           correcto.value = true;
//           noty.correcto(
//             `Se ha modificado la promoción ${
//               parametros.value.registro
//             } del expediente ${parametros.value.numeroExpediente} ${
//               parametros.value.archivoAVincular !== null
//                 ? `y asignada a la ${parametros.value.secretario.area}`
//                 : ""
//             }`
//           );
//         } catch (error) {
//           correcto.value = false;
//           manejoErrores.mostrarError(error);
//         }
//       } else {
//         try {
//           await oficialiaStore.crearPromocion(parametros.value);
//           correcto.value = true;
//           noty.correcto(
//             `Se ha capturado la promoción ${
//               parametros.value.registro
//             } en el expediente ${parametros.value.numeroExpediente} ${
//               parametros.value.archivoAVincular !== null
//                 ? `y asignada a la ${parametros.value.secretario.area}`
//                 : ""
//             }`
//           );
//         } catch (error) {
//           correcto.value = false;
//           manejoErrores.mostrarError(error);
//         }
//       }
//     } else {
//       if (esEdicion.value) {
//         correcto.value = true;
//       }
//     }
//     if (correcto.value) {
//       if (!esEdicion.value) {
//         parametros.value.asuntoNeunId = oficialiaStore.asuntoNeunId;
//         parametros.value.numeroOrden = oficialiaStore.numeroOrden;
//       } else {
//         parametros.value.asuntoNeunId = props.promocion.expediente.asuntoNeunId;
//         parametros.value.numeroOrden = props.promocion.numeroOrden;
//       }
//       cambioFormPromocion.value = false;
//       cambioForm.value = false;
//     } else {
//       return false;
//     }
//   } else {
//     const tiposPromovente = ["default", "parte", "promovente", "autoridad"];
//     const params = new InsertarPromocion();
//     params.asuntoNeunId = parametros.value.asuntoNeunId;
//     params.tipoCuaderno = parametros.value.cuaderno.cuadernoId;
//     params.fechaPresentacion = parametros.value.fechaPresentacion;
//     params.horaPresentacion = parametros.value.horaPresentacion;
//     params.clasePromocion = 0;
//     params.clasePromovente = tiposPromovente.findIndex(
//       (t) => t === parametros.value.tipoPromovente
//     );
//     if (parametros.value.promoventeExistente?.personaId) {
//       params.tipoPromovente = parametros.value.promoventeExistente.personaId;
//     } else if (
//       parametros.value.promoventeAutoridadExistente?.personaId &&
//       !parametros.value.tipoPromoventeCat
//     ) {
//       params.tipoPromovente =
//         parametros.value.promoventeAutoridadExistente.personaId;
//     } else {
//       params.tipoPromovente = 0;
//     }
//     params.tipoContenido = parametros.value.contenido.id;
//     params.numeroCopias = parametros.value.copias;
//     params.numeroAnexo = parametros.value.anexos.length;
//     params.secretario = parametros.value.secretario.empleadoId;
//     params.origenPromocion = parametros.value.origen;
//     params.numeroRegistro = parametros.value.registro;
//     try {
//       await oficialiaStore.asociarPromocion(params);
//       correcto.value = true;
//     } catch (error) {
//       manejoErrores.mostrarError(error);
//       correcto.value = false;
//     }
//     if (!correcto.value) return false;
//     noty.correcto(
//       `Se ha vinculado la promoción ${
//         parametros.value.registro
//       } al expediente ${parametros.value.expedienteEncontrado.asuntoAlias} ${
//         parametros.value.archivoAVincular !== null
//           ? `y asignada a la ${parametros.value.secretario.area}`
//           : ""
//       }`
//     );
//     parametros.value.numeroOrden = oficialiaStore.numeroOrden;
//   }

//   // }
//   //   break;
//   // case 2: // guarda promoventes
//   //   {
//   if (cambioFormPromovente.value) {
//     correcto.value = false;
//     const parteExistente = parametros.value.tipoParte === "parteExistente";
//     const esPromoventeExistente = parametros.value.esPromoventeExistente;
//     const personaAsunto = new GuardarPersonasAsuntos();

//     if (
//       parametros.value.tipoPromovente == "parte" ||
//       parametros.value.tipoPromovente == "promovente"
//     ) {
//       personaAsunto.asuntoNeunId = parametros.value.asuntoNeunId;
//       personaAsunto.nombre = parteExistente
//         ? parametros.value.promoventeAutoridadExistente.nombre
//         : parametros.value.parteNombre;
//       personaAsunto.aPaterno = parteExistente
//         ? parametros.value.promoventeAutoridadExistente.aPaterno
//         : parametros.value.parteApellidoPaterno;
//       personaAsunto.aMaterno = parteExistente
//         ? parametros.value.promoventeAutoridadExistente.aMaterno
//         : parametros.value.parteApellidoMaterno;
//       personaAsunto.catTipoPersonaId = parteExistente
//         ? parametros.value.promoventeAutoridadExistente.tipo
//         : parametros.value.parteCatTipoPersona?.catTipoPersonaId;
//       personaAsunto.catCaracterPersonaAsuntoId = parteExistente
//         ? parametros.value.promoventeAutoridadExistente
//             .catCaracterPersonaAsuntoId
//         : parametros.value.parteCatTipoPersonaCaracter?.caracterPersonaId;
//       personaAsunto.numeroOrden = parametros.value.numeroOrden;
//     }

//     const promovente = new GuardarPromoventes();

//     //configurar con respecto al tipo de parte
//     if (parametros.value.tipoPromovente == "promovente") {
//       promovente.asuntoNeunId = parametros.value.asuntoNeunId;
//       promovente.aPaterno = esPromoventeExistente
//         ? parametros.value.promoventeExistente.aPaterno
//         : parametros.value.promoventeApellidoPaterno;
//       promovente.aMaterno = esPromoventeExistente
//         ? parametros.value.promoventeExistente.aMaterno
//         : parametros.value.promoventeApellidoMaterno;
//       promovente.nombre = esPromoventeExistente
//         ? parametros.value.promoventeExistente.nombre
//         : parametros.value.promoventeNombre;
//       promovente.tipo = esPromoventeExistente
//         ? parametros.value.promoventeExistente.tipo
//         : parametros.value.tipoPromoventeCat?.id;
//       promovente.numeroOrden = parametros.value.numeroOrden;
//     }

//     const autoridad = new GuardarAutoridadJudicial();
//     autoridad.asuntoNeunId = parametros.value.asuntoNeunId;
//     autoridad.empleadoId = parametros.value.promoventeAutoridad?.empleadoId;
//     autoridad.autoridadJudicialId =
//       parametros.value.promoventeAutoridad?.catOrganismoId;
//     autoridad.numeroOrden = parametros.value.numeroOrden;
//     switch (parametros.value.tipoPromovente) {
//       case "promovente":
//         {
//           try {
//             await promoventesStore.crearPersonasAsunto(personaAsunto);
//             correcto.value = true;
//             noty.correcto("¡Parte guardada exitosamente!");
//           } catch (error) {
//             correcto.value = false;
//             manejoErrores.mostrarError(error);
//           }
//           promovente.personaId = promoventesStore.personaId;
//           if (!correcto.value) {
//             loader.hide();
//             return false;
//           }
//           try {
//             await promoventesStore.crearPromovente(promovente);
//             correcto.value = true;
//             noty.correcto("¡Promovente guardado exitosamente!");
//           } catch (error) {
//             correcto.value = false;
//             manejoErrores.mostrarError(error);
//           }
//         }
//         break;
//       case "parte":
//         {
//           try {
//             await promoventesStore.crearPersonasAsunto(personaAsunto);
//             correcto.value = true;
//             noty.correcto("¡Parte guardada exitosamente!");
//           } catch (error) {
//             correcto.value = false;
//             manejoErrores.mostrarError(error);
//           }
//         }
//         break;
//       case "autoridad":
//         {
//           try {
//             await promoventesStore.crearAutoridadJudicial(autoridad);
//             correcto.value = true;
//             noty.correcto("¡Autoridad guardada exitosamente!");
//           } catch (error) {
//             correcto.value = false;
//             manejoErrores.mostrarError(error);
//           }
//         }
//         break;
//       default:
//         break;
//     }
//   }
//   if (correcto.value) {
//     cambioFormPromovente.value = false;
//   }
//   return correcto.value;
// }
</script>
<style scoped>
.min-step {
  min-height: 50vh;
}
</style>
