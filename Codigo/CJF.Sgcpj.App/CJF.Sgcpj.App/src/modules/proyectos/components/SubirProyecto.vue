<template>
  <q-card style="width: 80vw; max-width: 80vw;">
    <q-toolbar>
      <q-toolbar-title> Subir proyecto</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-stepper
      v-model="step"
      ref="stepper"
      color="primary"
      keep-alive
      done-color="positive"
      active-color="primary"
      inactive-color="grey"
      animated
    >
    <q-step
        class="scroll"
        style="max-height: 65vh;"
        :name="1"
        title="Datos del expediente y proyecto"
        icon="settings"
        :done="step > 1"
      >

        <q-banner class="light-blue-3 doc-note doc-note--tip" v-if="!item">
          <template v-slot:avatar>
            <q-icon name="mdi-information" color="secondary" size="sm" />
          </template>
          <q-item-label>
            Usa esta opción para llevar el flujo de un proyecto del que aún no se ha
            celebrado la audiencia.
          </q-item-label>
        </q-banner>

        <q-form ref="formValido">
          <q-toolbar>
            <q-toolbar-title class="text-subtitle1">
              <span class="text-bold"> Datos del expediente </span>
              <template v-if="!item">
                <q-spinner-orbit color="primary" v-if="estatusExpediente == 'loading'" />
                <q-avatar size="sm" :color="status[estatusExpediente].color" text-color="white" class="q-ml-lg" v-else
                  :icon="status[estatusExpediente].icon" />
                <template v-if="erroresExpediente.length == 0">
                  <q-chip size="sm">
                    {{ status[estatusExpediente].legend }}
                  </q-chip>
                </template>
                <template v-else>
                  <q-chip color="red" size="sm" text-color="white" v-for="error in erroresExpediente" :key="error">
                    <span>
                      {{ error }}
                    </span>
                  </q-chip>
                </template>
              </template>
            </q-toolbar-title>
          </q-toolbar>
          <q-item v-if="!item">
            <div class="row full-width">
              <q-select clearable dense filled class="col" label="Buscar un expediente existente *"
                v-model="expedienteEncontrado" @filter="buscarExpedientePorNumero" :options="opcionesExpediente"
                option-value="asuntoNeunId" use-input input-debounce="0" hint="Formato Número/AAAA"
                @update:model-value="cambioExpediente" :loading="loadingExpedientes"
                :rules="[(val) => Validaciones.validaSelectRequerido(val)]">
                <template v-slot:append>
                  <q-btn flat round icon="mdi-magnify" />
                </template>
                <template v-slot:no-option>
                  <q-item>
                    <q-item-section class="text-red row">
                      <span>
                        <q-icon name="info" /> No se encontraron resultados</span>
                    </q-item-section>
                  </q-item>
                </template>
                <template v-slot:option="scope">
                  <q-item v-bind="scope.itemProps">
                    <q-item-section>
                      <q-item-label>{{ scope.opt.asuntoAlias }}</q-item-label>
                      <q-item-label caption>
                        {{ scope.opt.tipoAsunto }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </template>
              </q-select>
              <q-select dense filled class="col q-ml-md" use-input input-debounce="0" v-model="cuaderno"
                label="Cuaderno *" option-label="cuaderno" option-value="cuadernoId" @filter="filtrarCuaderno"
                @update:model-value="setExpediente" :options="cuadernoOptions" :rules="[
                    (val) => Validaciones.validaSelectRequerido(val?.cuadernoId),
                  ]">
              </q-select>
            </div>
          </q-item>
          <q-item v-else class="q-col-gutter-md justify-center">

            <div class="col-3">
              <q-card>
                <q-card-section>
                  <div class="text-grey">
                    Expediente
                  </div>
                  <q-separator/>
                  <div class="text-h6">
                    {{ item?.expediente.asuntoAlias }}
                  </div>
                </q-card-section>
              </q-card>
            </div>
            <div class="col-3">
              <q-card>
                <q-card-section>
                  <div class="text-grey">
                    Tipo de asunto
                  </div>
                  <q-separator/>
                  <div class="text-h6">
                    {{ item?.expediente.catTipoAsunto }}
                  </div>
                </q-card-section>
              </q-card>
            </div>
            <div class="col-3">
              <q-card>
                <q-card-section>
                  <div class="text-grey">
                    Cuaderno
                  </div>
                  <q-separator/>
                  <div class="text-h6">
                    {{ item?.expediente.cuaderno }}
                  </div>
                </q-card-section>
              </q-card>
            </div>

          </q-item>
          <q-toolbar>
            <q-toolbar-title class="text-subtitle1">
              <span class="text-bold">
                Datos del proyecto
              </span>
            </q-toolbar-title>
          </q-toolbar>
          <q-item>
            <q-item-section>
              <div class="row q-gutter-md">
                <q-select dense filled class="col" label="Titular" :options="optionsTitular" v-model="titularSelected"
                  option-value="empleadoId" option-label="nombreEmpleado" :loading="loadingCatalogs"
                  @update:model-value="cambioForm" :rules="[
                      (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
                    ]">
                </q-select>
                <q-select dense filled class="col" label="Secretario" :options="optionsSecretario"
                  v-model="secretarioSelected" readonly option-value="empleadoId" option-label="nombreEmpleado"
                  :loading="loadingCatalogs" @update:model-value="cambioForm" :rules="[
                      (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
                    ]">
                </q-select>
                <q-select dense filled class="col" label="Tipo de sentencia o resolución"
                  :options="optionsTipoSentencia" v-model="tipoSentenciaSelected" @update:model-value="cambioForm"
                  :loading="loadingCatalogs" :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
                  option-value="id" option-label="descripcion">
                </q-select>
            </div>
          </q-item-section>
        </q-item>

        <q-item>
          <q-item-section class="fileUp">
            <q-item-label>
              <span class="text-subtitle1 text-bold">
                Sube el proyecto generado para este expediente
              </span>
            </q-item-label>
            <q-item-label
              :style="file !== null ? '' : 'border: 3px dashed #ccc'"
            >
              <q-file
                v-model="file"
                borderless
                class="full-width full-height"
                accept=".doc, .docx"
                max-file-size="30000000"
                @update:model-value="cambioForm"
                :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
              >
                <template v-if="!file" v-slot:prepend>
                  <div class="row label-file">
                    <div class="col">
                      <q-item-label>
                        <q-icon name="mdi-upload" />
                        Arrastra y suelta o
                        <q-btn no-caps flat padding="0px" color="light-blue">
                          busca un archivo
                        </q-btn>
                      </q-item-label>
                    </div>
                  </div>
                </template>
                <template v-slot:file>
                  <q-chip class="full-width full-height q-my-xs" square>
                    <q-avatar>
                      <q-icon :name="'insert_drive_file'" color="primary" />
                    </q-avatar>
                    <span class="ellipsis relative-position text-bold">
                      {{ file.name }}
                    </span>
                    <span
                      class="q-ml-xs text-grey q-pl-sm text-caption"
                      style="width: 15%"
                    >
                      {{
                        file.size / 1024 < 1024
                          ? (file.size / 1024).toFixed(1) + "KB"
                          : (file.size / 1024 / 1024).toFixed(1) + "MB"
                      }}
                    </span>
                    <q-tooltip>
                      {{ file.name }}
                    </q-tooltip>
                  </q-chip>
                </template>
                <template v-if="file" v-slot:after>
                  <q-item
                    dense-toggle
                    class="q-field-after"
                    clickable
                    @click="updateFile(null)"
                  >
                    <q-item-section align="left">
                      <q-icon
                        size="1.2em"
                        :name="'mdi-close'"
                        color="primary"
                      />
                    </q-item-section>
                  </q-item>
                </template>
              </q-file>
            </q-item-label>
            <q-item-label class="q-mt-xs">
              <q-icon name="mdi-information" color="info" class="q-mr-sm" />
              <span class="text-grey-8">
                Sólo puedes subir archivos menores a 30 Mb en formato .doc o
                .docx
              </span>
            </q-item-label>
          </q-item-section>
        </q-item>
      <q-separator></q-separator>
      <q-card-actions class="q-gutter-xl q-px-lg">
        <!--TODO: Recuperar funcionalidad del botón
        <q-btn
          class="col"
          :disable="
            !(estatusExpediente == 'valid') ||
            subiendoProyecto ||
            !estatusFormulario
          "
          :color="estatusFormulario ? 'blue' : 'grey-6'"
          unelevated
          no-caps
          label="Subir"
          type="submit"
        >
        </q-btn>
        -->
        <div class="col"></div>
        <q-btn
          class="col"
          no-caps
          label="Continuar"
          :disable="
            !(estatusExpediente == 'valid') ||
            subiendoProyecto ||
            !estatusFormulario
          "
          :color="estatusFormulario ? 'primary' : 'primary'"
          @click="siguiente()">
        </q-btn>
        <q-btn
          class="col"
          outline
          no-caps
          label="Cancelar"
          @click="emit('cancelar')"
          color="primary"
          text-color="primary"
        >
        </q-btn>
      </q-card-actions>

    </q-form>
    </q-step>
    <q-step
        :name="2"
        title="Motivo del asunto y sentido"
        icon="create_new_folder"
        :done="step > 2"
        class="scroll"
        style="max-height: 65vh;"
      >
      <RelacionarPartes
        :catTipoAsuntoId="catTipoAsuntoId"
        :item="item"
        :asuntoNeunId="asuntoNeunId"
        @regresar="step = step - 1"
        @cancelar="emit('cancelar')"
        @update:motivosPartes="guardar">
      </RelacionarPartes>
      </q-step>
    </q-stepper>
    <q-inner-loading :showing="subiendoProyecto">
      <q-spinner-grid size="2em" />
      <span class="q-mt-md">Cifrando proyecto...</span>
    </q-inner-loading>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useProyectosStore } from "../store/proyectos-store";
import { Validaciones } from "src/helpers/validaciones";
import { Utils } from "src/helpers/utils";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { noty } from "src/helpers/notify";
import RelacionarPartes from "./RelacionarPartes.vue";

const catalogosStore = useCatalogosStore();
const step = ref(1);
const proyectosStore = useProyectosStore();
const authStore = useAuthStore();
const tipoSentenciaSelected = ref(null);
const expedienteEncontrado = ref(null);
const opcionesExpediente = ref([]);
const cuaderno = ref(null);
const formValido = ref(false);
const cuadernoOptions = ref([]);
const optionsTitular = ref([]);
const optionsSecretario = ref([]);
const optionsTipoSentencia = ref([]);
const buscarExpediente = ref(null);
const titularSelected = ref(null);
const secretarioSelected = ref(null);
const loadingCatalogs = ref(false);
const estatusExpediente = ref("pending");
const erroresExpediente = ref([]);
const loadingExpedientes = ref(false);
const subiendoProyecto = ref(false);
const estatusFormulario = ref(false);
const catTipoAsuntoId = ref(null);
const asuntoNeunId = ref(null);
const status = ref({
  valid: {
    color: "green",
    icon: "done",
    legend: "Expediente valido",
  },
  invalid: {
    color: "red-6",
    icon: "close",
    legend: "Expediente invalido",
  },
  loading: {
    color: "blue",
    icon: "remove",
    legend: "Validando",
  },
  pending: {
    color: "grey",
    icon: "remove",
    legend: "Selecciona un expediente",
  },
});
const emit = defineEmits({
  refrescarTabla: (value) => value !== null,
  cancelar: (value) => value !== null,
  cerrar: (value) => value !== null,
});
const file = ref(null);
const result = ref(null);
const props = defineProps({
  item: {
    type: Object,
    required: false,
    default: null,
  },
  version: {
    type: Object,
    required: true,
    default: null,
  },
});

onMounted(async () => {
  loadingCatalogs.value = true;
  try {
    await catalogosStore.obtenerTitulares();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  optionsTitular.value = catalogosStore.titulares;

  try {
    await catalogosStore.obtenerSecretarios();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  optionsSecretario.value = catalogosStore.secretarios;
  if (
    optionsSecretario.value.find(
      (x) => x.empleadoId === authStore.user.empleadoId,
    )
  )
    secretarioSelected.value = optionsSecretario.value.find(
      (x) => x.empleadoId === authStore.user.empleadoId,
    );
  else secretarioSelected.value = optionsSecretario.value[0];

  if (props.item) {
    estatusExpediente.value = "valid";
    try {
      await catalogosStore.obtenerTiposSentencia(
        props.item?.expediente.catTipoAsuntoId,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsTipoSentencia.value = catalogosStore.tipoSentencia;

    try {
      await catalogosStore.obtenerSentidosSentencias(
        props.item?.expediente.catTipoAsuntoId,
      );
    } catch (error) {
      manejoErrores.mostrarError(error);
    }

  }

  if (props.version) {
    titularSelected.value = optionsTitular.value.find(
      (x) => x.empleadoId === props.version.titularId,
    );
    secretarioSelected.value = optionsSecretario.value.find(
      (x) => x.empleadoId === props.version.secretarioId,
    );
    tipoSentenciaSelected.value = optionsTipoSentencia.value.find(
      (x) => x.id === props.version.tipoSentenciaId,
    );
  }

  loadingCatalogs.value = false;
});

async function updateFile(newFile) {
  file.value = await Utils.fileToBlob(newFile);
}

function siguiente() {
  step.value = step.value + 1;
  if(props.item?.expediente)
  {
    catTipoAsuntoId.value = props.item.expediente.catTipoAsuntoId;
    asuntoNeunId.value = props.item.expediente.asuntoNeunId;
  }
  else{
    catTipoAsuntoId.value = expedienteEncontrado.value.catTipoAsuntoId;
    asuntoNeunId.value = expedienteEncontrado.value.asuntoNeunId;
  }
}
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
        loadingExpedientes.value = true;
        try {
          await proyectosStore.buscarExpediente(val, null, 2);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        opcionesExpediente.value = proyectosStore.expediente;
        if (opcionesExpediente.value?.length === 1) {
          buscarExpediente.value = opcionesExpediente.value[0];
        }
        loadingExpedientes.value = false;
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
  estatusExpediente.value = "pending";
  erroresExpediente.value = [];
  if (val) {
    await obtenCatalogosDependientes(
      val.asuntoNeunId,
      val.asuntoAlias,
      val.catTipoAsuntoId,
    );
    try {
      await catalogosStore.obtenerTiposSentencia(val.catTipoAsuntoId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    try {
      await catalogosStore.obtenerSentidosSentencias( val.catTipoAsuntoId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsTipoSentencia.value = catalogosStore.tipoSentencia;

  }

  await cambioForm();
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
  if (cuadernoOptions.value.length == 1) {
    cuaderno.value = cuadernoOptions.value[0];
    setExpediente(cuaderno.value);
  }
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

async function setExpediente(notebook) {
  try {
    if (!notebook) return;

    estatusExpediente.value = "loading";

    let response = await proyectosStore.validarExpediente({
      asuntoAlias: expedienteEncontrado.value.asuntoAlias,
      cuadernoId: notebook.cuadernoId,
      tipoAsuntoId: expedienteEncontrado.value.catTipoAsuntoId,
      asuntoNeunId: expedienteEncontrado.value.asuntoNeunId,
    });

    estatusExpediente.value = response.puedeIngestar ? "valid" : "invalid";
    erroresExpediente.value = response.motivos;
    await cambioForm();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}
async function cambioForm() {
  estatusFormulario.value = await formValido.value?.validate(false);
  await formValido.value?.resetValidation();
}

async function guardar(value) {
  let correcto = false;
  subiendoProyecto.value = true;
  let data = new FormData();
  if (props.item?.expediente) {
    data.append("catOrganismoId", props.item.expediente.catOrganismoId);
    data.append("asuntoNeunId", props.item.expediente.asuntoNeunId);
  } else {
    data.append("catOrganismoId", expedienteEncontrado.value.catOrganismoId);
    data.append("asuntoNeunId", expedienteEncontrado.value.asuntoNeunId);
  }

  data.append("titularId", titularSelected.value.empleadoId);
  data.append("secretarioId", secretarioSelected.value.empleadoId);
  data.append("tipoSentenciaId", tipoSentenciaSelected.value.id);
  data.append("motivosPartes", JSON.stringify(value));
  data.append(file.value.name, Utils.blobToFile(file.value), file.value.name);

  try {
    let expLabel = "";
    if (props.item) expLabel = props.item?.expediente.asuntoAlias;
    else expLabel = expedienteEncontrado.value.asuntoAlias;

    result.value = await proyectosStore.addVersion(data);
    correcto = true;
    noty.correcto(`Se ha vinculado el proyecto al expediente ${expLabel}`);
    emit("refrescarTabla");
    emit("cerrar");
  } catch (error) {
    correcto = false;
    manejoErrores.mostrarError(error);
  }

  if (correcto) {
    file.value = null;
    formValido.value.reset();
    formValido.value.resetValidation();
  }
  emit("refrescarTabla");
  subiendoProyecto.value = false;
}
</script>

<style>
.q-file {
  [role="alert"] {
    text-align: center;
    margin-bottom: 10px;
  }
}
.fileUp {
  .q-field__bottom {
    margin-bottom: 1rem;
  }
}
.text-subtitle1 {
  overflow: auto;
  text-overflow: initial;
  white-space: normal;
}
</style>
