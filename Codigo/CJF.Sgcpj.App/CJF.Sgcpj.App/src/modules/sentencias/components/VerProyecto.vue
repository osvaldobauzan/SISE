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

        <!-- <div id="word-container" class="fit"></div> -->

        <q-pdfviewer
          :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
          :src="nombreArchivo"
        />
      </template>

      <template v-slot:after>
        <q-toolbar>
          <q-toolbar-title> Proyecto {{ title }}</q-toolbar-title>
          <q-btn flat round dense icon="mdi-close" v-close-popup />
        </q-toolbar>
        <q-separator></q-separator>
        <q-toolbar>
          <q-item-label class="text-bold"> Datos del Expediente</q-item-label>
        </q-toolbar>
        <q-card-section>
          <div class="row wrap">
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
        </q-card-section>
        <q-toolbar>
          <q-item-label class="text-bold"> Histórico </q-item-label>
          <q-space></q-space>
          <q-btn
            dense
            outline
            no-caps
            class="q-px-sm"
            color="secondary"
            icon="mdi-plus"
            label="Agregar versión"
            @click="showSubirProyecto = true"
            v-if="
              item.Estado === 'Con ajustes' || item.Estado === 'No aprobado'
            "
          ></q-btn>
        </q-toolbar>
        <q-toolbar
          class="q-gutter-md flex flex-center"
          v-if="item.Estado === 'Para revisión'"
        >
          <q-btn
            no-caps
            unelevated
            color="positive"
            icon="mdi-check"
            label="Aprobar"
            @click="changeEstado(item, 'Aprobado')"
            v-close-popup
          ></q-btn>
          <q-btn-group outline>
            <q-btn
              no-caps
              color="warning"
              label="Ajustes de:"
              text-color="secondary"
            />
            <q-btn
              no-caps
              color="warning"
              text-color="secondary"
              label="Forma"
              icon="mdi-pencil-box-outline"
              @click="changeEstado(item, 'Con ajustes', 'Forma')"
              v-close-popup
            />
            <q-btn
              no-caps
              color="warning"
              text-color="secondary"
              label="Fondo"
              icon="mdi-pencil-box"
              @click="changeEstado(item, 'Con ajustes', 'Fondo')"
              v-close-popup
            />
          </q-btn-group>
          <q-btn
            no-caps
            unelevated
            color="negative"
            icon="mdi-close"
            label="No aprobar"
            @click="changeEstado(item, 'No aprobado')"
            v-close-popup
          ></q-btn>
        </q-toolbar>
        <q-card-section>
          <q-list separator bordered>
            <q-expansion-item
              :default-opened="rows.length.toString() === row.archivo"
              group="somegroup"
              header-class="bg-grey-2"
              expand-separator
              v-for="row in rows"
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
                        nombreArchivo = `./docs/Version${row.archivo}.pdf`
                      "
                    ></q-btn>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label class="text-bold">
                      {{ row.version }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ row.fecha }}
                    </q-item-label>
                  </q-item-section>
                  <q-item-section side>
                    <q-item-label>
                      <q-chip square :color="getColor(row.estado)">
                        {{ row.estado }}
                        {{ row.subestado ? ` (${row.subestado})` : "" }}
                      </q-chip>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </template>

              <q-list>
                <q-item>
                  <q-item-section side>
                    <q-icon name="mdi-account-school"></q-icon>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label caption>Titular</q-item-label>
                    <q-item-label>{{ row.titular }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section side>
                    <q-icon name="mdi-account-tie"></q-icon>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label caption>Secretario</q-item-label>
                    <q-item-label>{{ row.secretario }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section side>
                    <q-icon name="mdi-book-education"></q-icon>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label caption>Sentido</q-item-label>
                    <q-item-label>{{ row.sentido }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section side>
                    <q-icon name="mdi-book"></q-icon>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label caption>Tipo sentencia</q-item-label>
                    <q-item-label>{{ row.tipoSentencia }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section side>
                    <q-icon name="mdi-comment-text"></q-icon>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label caption
                      >Comentario del secretario</q-item-label
                    >
                    <q-item-label>{{ row.comentarioSecretario }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item v-if="row.correccionFecha">
                  <q-item-section side>
                    <q-btn
                      flat
                      round
                      dense
                      icon="mdi-paperclip"
                      color="negative"
                      @click="
                        nombreArchivo = `./docs/CorreccionVersion${row.archivo}.pdf`
                      "
                    ></q-btn>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label caption>Comentario del titular</q-item-label>
                    <q-item-label>{{ row.comentarioTitular }}</q-item-label>
                  </q-item-section>
                  <q-item-section side>
                    <q-chip dense square outline>{{
                      date.formatDate(row.correccionFecha, "DD/MM/YYYY")
                    }}</q-chip>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-expansion-item>
          </q-list>
        </q-card-section>
      </template>
    </q-splitter>
  </q-card>
  <q-dialog v-model="showSubirProyecto">
    <SubirProyecto :item="item"></SubirProyecto>
  </q-dialog>
  <q-dialog v-model="showSubirCorrecciones">
    <CorreccionesTitular
      :item="item"
      :version="selectedVersion"
      v-on:close-dialog="cerrarDialog"
    >
    </CorreccionesTitular>
  </q-dialog>
</template>

<script setup>
import { ref, onMounted } from "vue";
import SubirProyecto from "./SubirProyecto.vue";
import CorreccionesTitular from "./CorreccionesTitular.vue";
import { date } from "quasar";
// import Version1 from "../data/docs/Version1.pdf";
// import * as docx from "docx-preview";

const rows = ref([]);
const splitterModel = ref(50);
const showSubirProyecto = ref(false);
const showSubirCorrecciones = ref(false);
const selectedVersion = ref(null);
const nombreArchivo = ref("./docs/Version1.pdf");

function cerrarDialog() {
  this.$emit("closeDialog");
}
// function PreviewWordDoc(docUrl = "docs/EjemploProyecto.docx") {
//   //URL of the Word Document.
//   var url = docUrl;

//   //Send a XmlHttpRequest to the URL.
//   var request = new XMLHttpRequest();
//   request.open("GET", url, true);
//   request.responseType = "blob";
//   request.onload = function () {
//     //Set the ContentType to docx.
//     var contentType =
//       "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

//     //Convert BLOB to File object.
//     var doc = new File([request.response], contentType);

//     //If Document not NULL, render it.
//     if (doc != null) {
//       //Set the Document options.
//       var docxOptions = Object.assign(docx.defaultOptions, {
//         useMathMLPolyfill: true,
//       });
//       //Reference the Container DIV.
//       var container = document.querySelector("#word-container");

//       //Render the Word Document.
//       docx.renderAsync(doc, container, null, docxOptions);
//     }
//   };
//   request.send();
// }

const getColor = (e) => coloresList.find((i) => i.status.startsWith(e)).color;

const coloresList = [
  {
    color: "grey-4",
    status: "Sentencias",
    label: "Ver todas",
    number: 9,
    icon: "mdi-filter-off",
  },
  {
    color: "red-2",
    status: "Sin proyecto",
    label: "Sin proyecto",
    number: 1,
  },
  {
    color: "purple-2",
    status: "Para revisión",
    label: "Para revisión",
    number: 1,
  },
  {
    color: "orange-2",
    status: "No aprobado",
    label: "No aprobado",
    number: 1,
  },
  {
    color: "yellow-2",
    status: "Con ajustes",
    label: "Con ajustes",
    number: 2,
  },
  { color: "green-2", status: "Aprobado", label: "Aprobado", number: 1 },
  {
    color: "blue-2",
    status: "Preautorizado",
    label: "Preautorizado",
    number: 1,
    isHidden: true,
  },
  {
    color: "green-3",
    status: "Autorizado",
    label: "Autorizado",
    number: 2,
    isHidden: true,
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
});

onMounted(() => {
  rows.value = props.item.Historico;
  nombreArchivo.value = `./docs/Version${rows.value.length}.pdf`;
});

function changeEstado(item, estado, subestado = "") {
  item.Estado = estado;
  item.Subestado = subestado;
  item.Historico[0].estado = estado;
  item.Historico[0].subestado = subestado;
  selectedVersion.value = item.Historico[0].version;
  showSubirCorrecciones.value = true;
}
</script>
<style>
td {
  border-bottom: 1pt solid black;
}
</style>
