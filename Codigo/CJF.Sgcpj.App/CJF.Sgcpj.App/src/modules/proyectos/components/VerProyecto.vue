<template>
  <q-card>
    <q-splitter
      v-model="splitterModel"
      :limits="[50, 100]"
      style="width: 100%; height: 100%"
    >
      <template v-slot:before>
        <!-- <input
          type="button"
          id="btnPreview"
          value="Cargar documento"
          onclick="PreviewWordDoc()"
        /> -->

        <q-tabs
          v-model="tab"
          inline-label
          outside-arrows
          mobile-arrows
          color="primary"
        >
          <template v-for="(fileId, index) in fileIds" :key="index">
            <q-tab :name="'Version-' + index" :label="fileId.name" >
              <q-btn
                flat
                no-caps
                icon="mdi-file-word"
                @click="descargarArchivo(fileId.id)"
                color="primary"
                label="Descargar Word"
                v-if="fileId.name.includes('Archivo')"
                class="q-ml-md"
              >
              </q-btn>
            </q-tab>
            <!-- Sin Datos -->
          </template>
        </q-tabs>
        <q-tab-panels v-model="tab" animated style="height: 95%" keep-alive>
          <q-tab-panel
            v-for="(fileId, index) in fileIds"
            :key="index"
            :name="'Version-' + index"
            class="q-pa-none"
          >
            <VerArchivoProyecto :id="fileId.id"></VerArchivoProyecto>
          </q-tab-panel>
        </q-tab-panels>
      </template>
      <template v-slot:after>
        <q-toolbar>
          <q-toolbar-title>
            Versiones del Proyecto
            <!-- {{ title }} -->
          </q-toolbar-title>

          <q-btn
            flat
            round
            dense
            icon="mdi-close"
            v-close-popup
            @click="refrescarTabla"
          />
        </q-toolbar>
        <q-separator></q-separator>
        <q-toolbar>
          <q-item-label class="text-bold"> Datos del Expediente</q-item-label>
        </q-toolbar>
        <q-card-section class="q-py-none">
          <div class="row wrap">
            <q-item class="col-12 col-md-4">
              <q-item-section>
                <q-item-label caption>Expediente</q-item-label>
                <q-item-label class="text-bold">{{
                  item?.expediente.asuntoAlias
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-12 col-md-4">
              <q-item-section>
                <q-item-label caption>Tipo de asunto</q-item-label>
                <q-item-label class="text-bold">{{
                  item?.expediente.catTipoAsunto
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-12 col-md-4">
              <q-item-section>
                <q-item-label caption>Cuaderno</q-item-label>
                <q-item-label class="text-bold">{{
                  item?.expediente.cuaderno
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
        </q-card-section>
        <q-inner-loading :showing="cargandoVersiones"></q-inner-loading>
        <section v-if="!cargandoVersiones">
          <q-toolbar>
            <q-space></q-space>
            <q-btn
              dense
              outline
              no-caps
              class="q-px-sm"
              color="secondary"
              icon="mdi-plus"
              label="Agregar versión"
              v-permitido="69"
              @click="showSubirProyecto = true"
              v-if="
                estadoProyecto == estatusProyecto.ConAjustesDeFondo ||
                estadoProyecto == estatusProyecto.ConAjustesDeForma ||
                estadoProyecto == estatusProyecto.NoAprobado
              "
            >
            </q-btn>
          </q-toolbar>
          <q-card-section>
            <q-list separator bordered>
              <q-expansion-item
                :default-opened="index === 0"
                v-for="(row, index) in rows"
                header-class="bg-grey-2"
                group="somegroup"
                expand-separator
                :key="row.version"
              >
                <template v-slot:header>
                  <q-item class="fit">
                    <q-item-section side>
                      <q-btn
                        flat
                        round
                        icon="mdi-paperclip"
                        @click="
                          appendFile(
                            row.archivoId,
                            row.numeroVersion,
                            'Archivo',
                          )
                        "
                      >
                      </q-btn>
                    </q-item-section>
                    <q-item-section>
                      <q-item-label class="text-bold">
                        Versión {{ row.numeroVersion }}
                      </q-item-label>
                      <q-item-label caption>
                        {{ date.formatDate(row.fechaAlta, "DD/MM/YYYY") }}
                      </q-item-label>
                    </q-item-section>
                    <q-item-section side>
                      <q-item-label>
                        <q-chip square :color="getColor(row.estadoId)">
                          {{ row.estadoDescripcion }}
                          {{ row.subestado ? ` (${row.subestado})` : "" }}
                        </q-chip>
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </template>

                <q-list>
                  <q-item
                    class="q-pa-none"
                    v-if="
                      estadoProyecto == estatusProyecto.ParaRevision &&
                      index == 0
                    "
                  >
                    <q-toolbar class="q-gutter-none q-pa-none" v-permitido="71">
                      <div
                        class="row flex-center q-col-gutter-sm q-pa-sm"
                        style="width: 100%"
                      >
                        <div class="col-sm-12 col-md-auto" v-if="!cancelacion">
                          <q-btn
                            no-caps
                            unelevated
                            color="positive"
                            icon="mdi-check"
                            label="Aprobar"
                            @click="
                              changeEstado(item, estatusProyecto.Aprobado)
                            "
                          >
                          </q-btn>
                        </div>
                        <div class="col-sm-12 col-md-auto">
                          <q-btn-group>
                            <q-btn
                              no-caps
                              color="warning"
                              label="Ajustes de:"
                              text-color="secondary"
                            />
                            <q-separator vertical />
                            <q-btn
                              no-caps
                              color="warning"
                              text-color="secondary"
                              label="Forma"
                              icon="mdi-pencil-box-outline"
                              @click="
                                changeEstado(
                                  item,
                                  estatusProyecto.ConAjustesDeForma,
                                )
                              "
                            />
                            <q-separator vertical />
                            <q-btn
                              no-caps
                              color="warning"
                              text-color="secondary"
                              label="Fondo"
                              icon="mdi-pencil-box"
                              @click="
                                changeEstado(
                                  item,
                                  estatusProyecto.ConAjustesDeFondo,
                                )
                              "
                            />
                          </q-btn-group>
                        </div>
                        <div class="col-sm-12 col-md-auto">
                          <q-btn
                            no-caps
                            unelevated
                            color="negative"
                            icon="mdi-close"
                            label="No aprobar"
                            @click="
                              changeEstado(item, estatusProyecto.NoAprobado)
                            "
                          ></q-btn>
                        </div>
                      </div>
                    </q-toolbar>
                  </q-item>
                  <q-separator />
                  <div class="row q-mt-md">
                    <div class="col-sm-6">
                      <q-item>
                        <q-item-section side>
                          <q-icon name="mdi-account-school"></q-icon>
                        </q-item-section>
                        <q-item-section>
                          <q-item-label caption>Titular</q-item-label>
                          <q-item-label>{{ row.nombreTitular }}</q-item-label>
                        </q-item-section>
                      </q-item>
                    </div>
                    <div class="col-sm-6">
                      <q-item>
                        <q-item-section side>
                          <q-icon name="mdi-account-tie"></q-icon>
                        </q-item-section>
                        <q-item-section>
                          <q-item-label caption>Secretario</q-item-label>
                          <q-item-label>{{
                            row.nombreSecretario
                          }}</q-item-label>
                        </q-item-section>
                      </q-item>
                    </div>
                    <div class="col-sm-6">
                      <q-item>
                        <q-item-section side>
                          <q-icon name="mdi-book"></q-icon>
                        </q-item-section>
                        <q-item-section>
                          <q-item-label caption>Tipo sentencia</q-item-label>
                          <q-item-label>{{
                            row.tipoSentenciaDescripcion
                          }}</q-item-label>
                        </q-item-section>
                      </q-item>
                    </div>
                    <div class="col-12">
                      <q-item
                        v-if="row.comentarioTitular || row.archivoComentarioId"
                      >
                        <q-item-section side v-if="row.archivoComentarioId">
                          <q-btn
                            flat
                            round
                            dense
                            icon="mdi-paperclip"
                            color="negative"
                            @click="
                              appendFile(
                                row.archivoComentarioId,
                                row.numeroVersion,
                                'Comentario',
                              )
                            "
                          ></q-btn>
                        </q-item-section>
                        <q-item-section v-if="row.comentarioTitular">
                          <q-item-label caption>
                            Comentario del titular
                            <q-chip
                              outline
                              color="teal"
                              text-color="white"
                              size="sm"
                            >
                              {{ date.formatDate(row.fechaAlta, "DD/MM/YYYY") }}
                            </q-chip>
                          </q-item-label>
                          <p v-html="row.comentarioTitular"></p>
                        </q-item-section>
                      </q-item>
                    </div>

                    <div class="col-12">

                      <q-list bordered>
                        <q-expansion-item
                          :default-opened="index === 0"
                          v-for="(parte, index) in row.partes"
                          icon="mdi-account"
                          expand-separator
                          :label="parte.map((part) => part.name).join(', ')"
                          :key="parte.version"
                          header-class="bg-grey-1 text-grey-8"
                          >

                          <q-table
                            :rows="parte[0].value"
                            :columns="partColumns"
                            :hide-bottom="false"
                            row-key="idParte"
                          >

                            <template v-slot:bottom></template>
                          </q-table>

                        </q-expansion-item>
                      </q-list>
                    </div>
                  </div>
                </q-list>
              </q-expansion-item>
            </q-list>
          </q-card-section>
        </section>
      </template>
    </q-splitter>
  </q-card>
  <q-dialog v-model="showSubirProyecto">
    <SubirProyecto
      :item="item"
      :version="lastVersionData"
      @cerrar="showSubirProyecto = false"
      @cancelar="showAlertaCancelarSubidaProyecto = true"
      @refrescar-tabla="refrescarTabla"
    ></SubirProyecto>
  </q-dialog>
  <q-dialog v-model="showSubirCorrecciones" persistent>
    <CorreccionesTitular
      :item="item"
      :proyecto-id="selectedVersion.proyectoId"
      @refrescar="
        refrescarTabla();
        showSubirCorrecciones = false;
      "
    >
    </CorreccionesTitular>
  </q-dialog>
  <DialogConfirmacion
    v-model="showAlertaCancelarSubidaProyecto"
    :titulo="`¿Deseas descartar la versión?`"
    :subTitulo="`Se perderá la información capturada.`"
    @aceptar="showSubirProyecto = false"
  >
  </DialogConfirmacion>
</template>

<script setup>
import { ref, onMounted } from "vue";
import SubirProyecto from "./SubirProyecto.vue";
import CorreccionesTitular from "./CorreccionesTitular.vue";
import {
  useProyectosStore,
  estatusProyecto,
} from "../store/proyectos-store.js";
import { date } from "quasar";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import VerArchivoProyecto from "./VerArchivoProyecto.vue";

const rows = ref([]);
const fileIds = ref([]);
const splitterModel = ref(50);
const showSubirProyecto = ref(false);
const showSubirCorrecciones = ref(false);
const showAlertaCancelarSubidaProyecto = ref(false);
const cargandoVersiones = ref(false);
const selectedVersion = ref(null);
const ProyectosStore = useProyectosStore();
const tab = ref("Version-0");
const lastVersionData = ref(null);
const estadoProyecto = ref(estatusProyecto.SinProyecto);
const partColumns = ref([
  {
    name: 'motivo',
    required: true,
    label: 'Motivo',
    align: 'left',
    field: row => row.motivo,
    format: val => `${val}`,
    sortable: true
  },
  {
    name: 'sentido',
    required: true,
    label: 'Sentido',
    align: 'left',
    field: row => row.sentido,
    format: val => `${val}`,
    sortable: true
  },
  {
    name: 'descripcion',
    required: true,
    label: 'Descripción',
    align: 'left',
    field: row => row.descripcion,
    format: val => `${val}`,
    sortable: true
  }
]);

const emit = defineEmits({
  refrescarTabla: (value) => value !== null,
  cerrar: (value) => value !== null,
});

const getColor = (e) =>
  coloresList.find((i) => i.status == e)?.color ?? "grey-4";

const coloresList = [
  {
    color: "red-2",
    status: estatusProyecto.SinProyecto,
    label: "Sin proyecto",
  },
  {
    color: "purple-2",
    status: estatusProyecto.ParaRevision,
    label: "Para revisión",
  },
  {
    color: "orange-2",
    status: estatusProyecto.NoAprobado,
    label: "No aprobado",
  },
  {
    color: "yellow-2",
    status: estatusProyecto.ConAjustesDeFondo,
    label: "Con ajustes de fondo",
  },
  {
    color: "yellow-2",
    status: estatusProyecto.ConAjustesDeForma,
    label: "Con ajustes de forma",
  },
  {
    color: "green-3",
    status: estatusProyecto.Aprobado,
    label: "Aprobado",
  },
];

const props = defineProps({
  item: {
    type: Object,
    required: true,
  },
  title: {
    type: String,
    default: "",
  },
  cancelacion: {
    type: Boolean,
    default: false,
  },
});

onMounted(async () => {
  await getHistorial();
});

async function descargarArchivo(id) {
  await ProyectosStore.descargarDocumentos(id);
}

async function appendFile(fileId, version, type) {
  if (!fileIds.value.find((fileElmnt) => fileElmnt.id == fileId)) {
    fileIds.value.push({
      id: fileId,
      name: `${type} - Versión ${version}`,
    });
  }
  let findIndex = fileIds.value.findIndex(
    (fileElmnt) => fileElmnt.id == fileId,
  );
  tab.value = `Version-${findIndex}`;
}

async function getHistorial() {
  cargandoVersiones.value = true;
  try {
    await ProyectosStore.obtenerHistorial(props.item.expediente.asuntoNeunId);
    rows.value = ProyectosStore.proyectoSeleccionado.archivos;
    await appendFile(
      ProyectosStore.proyectoSeleccionado.archivos[0].archivoId,
      ProyectosStore.proyectoSeleccionado.archivos[0].numeroVersion,
      "Archivo",
    );
    lastVersionData.value = ProyectosStore.proyectoSeleccionado.archivos[0];
    estadoProyecto.value =
      ProyectosStore.proyectoSeleccionado.archivos[0].estadoId;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoVersiones.value = false;
}

async function refrescarTabla() {
  await getHistorial();
  emit("refrescarTabla");
}

function changeEstado(item, estado) {
  item.estado = estado;
  selectedVersion.value = rows.value[0];
  showSubirCorrecciones.value = true;
}
</script>
<style>
td {
  border-bottom: 1pt solid black;
}
.q-table__bottom {
  min-height: 0px !important;
}
</style>
