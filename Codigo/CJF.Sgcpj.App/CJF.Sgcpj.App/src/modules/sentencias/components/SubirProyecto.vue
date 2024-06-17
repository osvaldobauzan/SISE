<template>
  <q-card style="min-width: 700px">
    <q-toolbar>
      <q-toolbar-title> Subir proyecto </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-banner class="light-blue-3 doc-note doc-note--tip" v-if="!item">
      <template v-slot:avatar>
        <q-icon name="mdi-information" color="secondary" size="sm" />
      </template>
      <q-item-label>
        Usa esta opción para llevar el flujo de un proyecto del que aún no se ha
        celebrado la audiencia.
      </q-item-label>
    </q-banner>
    <q-separator></q-separator>
    <q-card-section>
      <q-toolbar>
        <q-toolbar-title class="text-subtitle1 text-bold">
          Datos del expediente
        </q-toolbar-title>
      </q-toolbar>
      <q-item v-if="!item">
        <div class="row full-width">
          <q-select
            clearable
            class="col"
            label="Buscar un expediente existente *"
            v-model="expedienteEncontrado"
            @filter="buscarExpedientePorNumero"
            :options="opcionesExpediente"
            option-value="asuntoNeunId"
            use-input
            input-debounce="0"
            hint="Formato Número/AAAA"
            @update:model-value="cambioExpediente"
            dense
            filled
            :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
          >
            <template v-slot:append>
              <q-btn flat round icon="mdi-magnify" />
            </template>
            <template v-slot:no-option>
              <q-item>
                <q-item-section class="text-red row">
                  <span>
                    <q-icon name="info" /> No se encontraron resultados</span
                  >
                </q-item-section>
              </q-item>
            </template>
            <template v-slot:option="scope">
              <q-item v-bind="scope.itemProps">
                <q-item-section>
                  <q-item-label>{{ scope.opt.asuntoAlias }}</q-item-label>
                  <q-item-label caption>{{
                    scope.opt.tipoAsunto
                  }}</q-item-label>
                </q-item-section>
              </q-item>
            </template>
          </q-select>
          <q-select
            dense
            filled
            class="col q-ml-md"
            use-input
            input-debounce="0"
            v-model="cuaderno"
            label="Cuaderno *"
            option-label="cuaderno"
            option-value="cuadernoId"
            @filter="filtrarCuaderno"
            @update:model-value="cambioForm"
            :options="cuadernoOptions"
            :rules="[
              (val) => Validaciones.validaSelectRequerido(val?.cuadernoId),
            ]"
          ></q-select>
        </div>
      </q-item>
      <q-item v-else>
        <div class="row full-width">
          <q-item class="col-4">
            <q-item-section>
              <q-item-label caption>Expediente</q-item-label>
              <q-item-label class="text-bold">{{
                item?.AsuntoAlias
              }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item class="col-4">
            <q-item-section>
              <q-item-label caption>Tipo de asunto</q-item-label>
              <q-item-label class="text-bold">{{
                item?.CatTipoAsunto
              }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item class="col-4">
            <q-item-section>
              <q-item-label caption>Cuaderno</q-item-label>
              <q-item-label class="text-bold">{{
                item?.Cuaderno
              }}</q-item-label>
            </q-item-section>
          </q-item>
        </div>
      </q-item>
      <q-toolbar>
        <q-toolbar-title class="text-subtitle1 text-bold">
          Datos del proyecto
        </q-toolbar-title>
      </q-toolbar>
      <q-item>
        <q-item-section>
          <div class="row q-gutter-md">
            <q-select
              dense
              filled
              class="col"
              label="Titular"
              :options="optionsTitular"
              v-model="titularSelected"
            ></q-select>
            <q-select
              dense
              filled
              class="col"
              label="Secretario"
              :options="optionsSecretario"
              v-model="secretarioSelected"
            ></q-select>
          </div>
        </q-item-section>
      </q-item>

      <q-item>
        <q-item-section>
          <div class="row q-gutter-md">
            <div class="col">
              <q-select
                dense
                filled
                label="Tipo de sentencia o resolución"
                :options="optionsTipoSentencia"
                v-model="tipoSentenciaSelected"
              ></q-select>
            </div>
            <div class="col">
              <q-select
                dense
                filled
                label="Selecciona el sentido"
                :options="optionsTipoSentido"
                v-model="tipoSentidoSelected"
              ></q-select>
            </div>
          </div>
        </q-item-section>
      </q-item>

      <q-item>
        <q-item-section>
          <q-item-label>
            Sube el archivo word del proyecto generado para este expediente
          </q-item-label>
          <q-item-label>
            <q-uploader
              flat
              auto-upload
              url="http://localhost:4444/upload"
              class="full-width"
              color="transparent"
              style="
                border-radius: 7;
                border-style: dashed;
                border-width: 2px;
                border-color: #cfcfcf;
              "
            >
              <template v-slot:header="scope">
                <q-item>
                  <q-item-section side>
                    <q-icon name="mdi-upload" color="grey-8" />
                  </q-item-section>
                  <q-item-section>
                    <q-item-label class="text-grey-8">
                      Arrastra y suelta el archivo aquí o
                      <q-btn
                        type="a"
                        flat
                        dense
                        no-caps
                        accept=".pdf, application/pdf"
                        class="q-pa-none"
                        color="info"
                        @click="scope.pickFiles"
                        label="busca un archivo"
                      >
                        <q-uploader-add-trigger
                      /></q-btn>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </template>
            </q-uploader>
          </q-item-label>
          <q-item-label class="q-mt-xs">
            <q-icon name="mdi-information" color="info" class="q-mr-sm" />
            <span class="text-grey-8"
              >Sólo puedes subir archivos menores a 20 Mb en formato PDF</span
            >
          </q-item-label>
        </q-item-section>
      </q-item>
      <q-item>
        <q-item-section>
          <q-item-label>
            Escribe brevemente el tema del asunto (síntesis)
          </q-item-label>
          <q-editor v-model="sintesis" style="height: 100px"></q-editor>
        </q-item-section>
      </q-item>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions class="q-gutter-xl q-px-lg">
      <q-btn
        class="col"
        unelevated
        no-caps
        color="primary"
        label="Subir"
        v-close-popup
        @click="subirProyecto('Para revisión')"
      ></q-btn>
      <q-btn class="col" outline no-caps label="Cancelar" v-close-popup></q-btn>
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { noty } from "src/helpers/notify";
import { useSentenciasStore } from "../store/sentencias-store";
import { Validaciones } from "src/helpers/validaciones";
import { Utils } from "src/helpers/utils";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useCatalogosStore } from "src/stores/catalogos-store";

const catalogosStore = useCatalogosStore();
const sentenciasStore = useSentenciasStore();
const tipoSentenciaSelected = ref(null);
const tipoSentidoSelected = ref(null);
const sintesis = ref("");
const expedienteEncontrado = ref(null);
const opcionesExpediente = ref([]);
const cuaderno = ref(null);
const form = ref(null);
const formValido = ref(false);
const cuadernoOptions = ref([]);
const buscarExpediente = ref(null);
const titularSelected = ref(null);
const secretarioSelected = ref(null);

const props = defineProps({
  item: {
    type: Object,
    required: false,
    default: null,
  },
});
async function buscarExpedientePorNumero(val, update, abort) {
  update(
    async () => {
      if (
        val === "" ||
        val.length <= 5 ||
        typeof Validaciones.validaNoExpediente(val) === "string"
      ) {
        abort();
        return;
      } else {
        try {
          await sentenciasStore.buscarExpediente(val, null, 2);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        opcionesExpediente.value = sentenciasStore.expediente;
        if (opcionesExpediente.value?.length === 1) {
          buscarExpediente.value = opcionesExpediente.value[0];
        }
      }
    },
    // "ref" is the Vue reference to the QSelect
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 100),
  );
}
async function cambioExpediente(val) {
  cuaderno.value = null;
  if (val) {
    await obtenCatalogosDependientes(
      val.asuntoNeunId,
      val.asuntoAlias,
      val.catTipoAsuntoId,
    );
  }
  cambioForm();
}
async function obtenCatalogosDependientes(
  asuntoNeunId,
  numeroExpediente,
  catTipoAsuntoId,
) {
  try {
    await catalogosStore.obtenerCuadernos(catTipoAsuntoId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  cuadernoOptions.value = catalogosStore.cuadernos;
}
/**
 * filtra cuaderno en combo
 * @param {*} val valor a buscar
 */
function filtrarCuaderno(val, update) {
  update(
    async () => {
      cuadernoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.cuadernos,
        "cuaderno",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
async function cambioForm() {
  formValido.value = await form.value?.validate(false);
}
function subirProyecto(estado) {
  const expediente = props.item ? props.item : opcionesExpediente.value[0];

  const data = {
    titular: titularSelected.value.value,
    secretario: secretarioSelected.value.value,
    tipoSentencia: tipoSentenciaSelected.value.value,
    sentido: tipoSentidoSelected.value.value,
    estado: estado,
    fecha: new Date(),
    archivo: "",
    comentarioSecretario: sintesis.value,
    correccionSintesis: "",
    correccionArchivo: "",
    correccionFecha: "",
  };
  sentenciasStore.addVersion(expediente, data);
  noty.correcto("Proyecto subido correctamente.");
}
const optionsTipoSentencia = [
  { label: "Sentencia definitiva", value: "Sentencia definitiva" },
  { label: "Resolución", value: "Resolución" },
  { label: "Aclaración de sentencia", value: "Aclaración de sentencia" },
  { label: "Interlocutoria", value: "Interlocutoria" },
];

const optionsTipoSentido = [
  { label: "Ampara", value: "Ampara" },
  { label: "No ampara", value: "No ampara" },
  { label: "Sobresee", value: "Sobresee" },
  { label: "Otro", value: "Otro" },
];
const optionsTitular = [
  { label: "David Cienfuegos Meza", value: "David Cienfuegos Meza" },
  { label: "Erick Zamorano González", value: "Erick Zamorano González" },
  { label: "Daniel Fernández Gómez", value: "Daniel Fernández Gómez" },
];
const optionsSecretario = [
  { label: "Adonaí Martínez Arriola", value: "Adonaí Martínez Arriola" },
  { label: "Amira Deyanira Rosales", value: "Amira Deyanira Rosales" },
  { label: "Alan Yassir Martínez", value: "Alan Yassir Martínez" },
];
</script>
