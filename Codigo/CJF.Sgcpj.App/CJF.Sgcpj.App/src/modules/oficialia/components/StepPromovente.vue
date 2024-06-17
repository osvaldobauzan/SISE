<template>
  <q-item-label class="text-bold q-my-md text-subtitle1"
    >Información del promovente</q-item-label
  >
  <div v-if="!expedienteNuevo">
    <div class="row q-gutter-xs">
      <div class="col-3">
        <q-input
          class="q-ml-none q-mb-md q-mr-lg"
          rounded
          outlined
          dense
          :clearable="true"
          bg-color="white"
          v-model="parametros.filterText"
          placeholder="Buscar promovente"
        >
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </q-input>
      </div>
      <div class="col-3 q-pr-md">
        <q-select
          dense
          filled
          v-model="parametros.filtroTipoPromovente"
          option-label="label"
          option-value="status"
          option-disable="inactive"
          :options="tiposPartes"
          label="Parte / Promovente / Autoridad Judicial"
          @update:model-value="aplicarFiltroPromoventes"
        >
          <template v-slot:option="scope">
            <q-item dense v-bind="scope.itemProps" :class="scope.opt.color">
              <q-item-section>
                <q-item-label
                  >{{ scope.opt.label }}
                  <q-tooltip
                    v-if="!tipoAsuntoSinParte && scope.opt.label == 'Parte'"
                  >
                    El tipo de asunto no admite partes</q-tooltip
                  ></q-item-label
                >
              </q-item-section>
            </q-item>
          </template>
        </q-select>
      </div>
      <div class="col"></div>
      <div class="col-3 text-right">
        <q-btn
          @click="diagAgregarParte = true"
          icon="mdi-account-plus"
          no-caps
          flat
          :disable="noAgregarNuevoPromovente"
          color="primary"
          class="q-ml-lg"
          :label="'Agregar nuevo promovente'"
        >
          <q-tooltip v-if="noAgregarNuevoPromovente">
            Ya tienes un elemento de la tabla seleccionado
          </q-tooltip>
        </q-btn>
      </div>
    </div>
    <div>
      <q-table
        flat
        class="my-sticky-header-table minWidth"
        style="min-height: fit-content; max-height: 380px"
        wrap-cells
        dense
        :rows-dense="true"
        bordered
        ref="tableRef"
        tabindex="0"
        :rows="rows"
        :columns="columns"
        row-key="index"
        selection="single"
        :rows-per-page-options="[0]"
        :hide-pagination="true"
        virtual-scroll
        :virtual-scroll-item-size="50"
        v-model:selected="parametros.selected"
        @update:selected="setSelecter"
        :loading="cargandoDatosTabla"
        :filter="parametros.filterText"
      >
        <template v-slot:loading>
          <q-inner-loading showing color="primary" />
        </template>
        <template #no-data>
          <div
            class="column items-center justify-center"
            style="min-height: fit-content; min-width: 100%"
          >
            <q-icon
              size="5em"
              :name="
                parametros.filterText == null || parametros.filterText != ''
                  ? 'mdi-account-off'
                  : 'mdi-account-remove'
              "
              color="grey-6 q-mb-sm q-mt-none"
            />
            <div class="text-h5 text-secondary text-bold q-mb-sm">
              {{
                parametros.filterText == null || parametros.filterText != ""
                  ? "Sin resultados"
                  : "Sin promoventes"
              }}
            </div>
            <div class="text-subtitle2 q-mb-md">
              {{
                parametros.filterText == null || parametros.filterText != ""
                  ? "No se encontraron coincidencias, "
                  : "Aún no hay promoventes registrados, "
              }}
              <q-btn
                @click="diagAgregarParte = true"
                no-caps
                flat
                :disable="noAgregarNuevoPromovente"
                color="primary"
                class="text-bold q-ma-none q-pa-none"
                :label="'agrega uno nuevo aquí'"
              ></q-btn>
            </div>
          </div>
        </template>
        <template v-slot:body="props">
          <q-tr :props="props" :class="getColor(props.row.tipo)">
            <q-td>
              <q-checkbox
                dense
                :model-value="props.selected"
                @update:model-value="
                  (val, evt) => {
                    Object.getOwnPropertyDescriptor(props, 'selected').set(
                      val,
                      evt,
                    );
                  }
                "
            /></q-td>
            <q-td>
              {{ props.row.tipo }}
            </q-td>
            <q-td>
              {{ props.row.nombre }}
            </q-td>
            <q-td>
              {{ props.row.descripcion }}
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </div>
  </div>
  <div v-else>
    <div class="text-body2 q-my-lg">
      Selecciona el tipo de figura que promovió la promoción.
    </div>
    <div class="q-pb-md">
      <div class="row q-gutter-sm">
        <q-radio
          class="col-4"
          v-model="parametros.tipoPromovente"
          val="promovente"
          label="Promovente"
          @update:model-value="cambiaronParametros"
        />
        <q-radio
          v-if="tipoAsuntoSinParte"
          class="col-4"
          v-model="parametros.tipoPromovente"
          val="parte"
          label="Parte"
          @update:model-value="cambiaronParametros"
        />
        <q-radio
          class="col"
          v-model="parametros.tipoPromovente"
          val="autoridad"
          label="Autoridad Judicial"
          @update:model-value="cambiaronParametros"
        />
      </div>
    </div>
    <div
      v-if="
        parametros.tipoPromovente === 'promovente' ||
        parametros.tipoPromovente === 'parte'
      "
    >
      <!--Promovente-->
      <template v-if="parametros.tipoPromovente === 'promovente'">
        <div class="row q-gutter-lg">
          <q-select
            v-cortarLabel
            @input-value="parametros.tipoPromoventeCat = null"
            dense
            filled
            use-input
            option-label="descripcion"
            option-value="id"
            label="Tipo *"
            class="col"
            :options="tipoPromoventeOptions"
            v-model="parametros.tipoPromoventeCat"
            @filter="filtrarTipoPromovente"
            lazy-rules
            :rules="reglasPromovente"
            @update:model-value="cambiaronParametros"
          >
          </q-select>
          <div class="col"></div>
        </div>
        <div v-if="tipoPromoveteSinDenominacion" class="row q-gutter-lg">
          <q-input
            dense
            filled
            v-model="parametros.promoventeNombre"
            label="Nombre *"
            class="col-4"
            @update:model-value="cambiaronParametros"
            :rules="reglasPromoventeNom"
          />
          <q-input
            dense
            filled
            v-model="parametros.promoventeApellidoPaterno"
            label="Apellido paterno *"
            class="col-4"
            @update:model-value="cambiaronParametros"
            :rules="reglasPromoventeAP"
          />
          <q-input
            dense
            filled
            v-model="parametros.promoventeApellidoMaterno"
            label="Apellido materno"
            class="col"
            @update:model-value="cambiaronParametros"
            :rules="reglasApellidoMaterno"
          />
        </div>
        <div v-else class="col-8 widthDenominacion">
          <q-input
            dense
            filled
            v-model="parametros.promoventeNombre"
            label="Denominación *"
            :rules="reglasParteDen"
            @update:model-value="cambiaronParametros"
          />
        </div>

        <q-item-label class="text-bold q-my-md text-subtitle1"
          >Agrega la parte asociada</q-item-label
        >
      </template>
      <!--Parte Nueva-->
      <div
        v-if="parametros.tipoParte == 'parteNueva' && tipoAsuntoSinParte"
        class="q-gutter-lg row"
      >
        <q-select
          v-cortarLabel
          @input-value="parametros.parteCatTipoPersona = null"
          dense
          filled
          use-input
          label-slot
          v-model="parametros.parteCatTipoPersona"
          option-label="descripcion"
          option-value="catTipoPersonaId"
          :options="tipoPersonaOptions"
          class="col-4"
          @filter="filtrarTipoPersona"
          :rules="reglasParteTP"
          @update:model-value="cambiaronParametros"
        >
          <template v-slot:label>
            <div v-if="parametros.tipoPromovente === 'promovente'">
              Tipo de persona
            </div>
            <div v-else>Tipo de persona *</div>
          </template>
        </q-select>
        <q-select
          v-cortarLabel
          @input-value="parametros.parteCatTipoPersonaCaracter = null"
          dense
          filled
          use-input
          label-slot
          v-model="parametros.parteCatTipoPersonaCaracter"
          option-label="caracterPersona"
          option-value="caracterPersonaId"
          :options="tipoPersonaCaracterOptions"
          :rules="reglasPartePC"
          class="col-4"
          @filter="filtrarTipoCaracter"
          @update:model-value="cambiaronParametros"
        >
          <template v-slot:label>
            <div v-if="parametros.tipoPromovente === 'promovente'">
              Carácter
            </div>
            <div v-else>Carácter *</div>
          </template>
        </q-select>
      </div>
      <div
        class="q-gutter-lg"
        v-if="
          parametros.parteCatTipoPersona?.catTipoPersonaId == 2 ||
          parametros.parteCatTipoPersona?.catTipoPersonaId == 3
        "
      >
        <div class="widthDenominacion q-pt-lg">
          <q-input
            dense
            filled
            v-model="parametros.denominacionDeAutoridad"
            label="Denominación *"
            :rules="reglasParteDen"
            @update:model-value="cambiaronParametros"
          />
        </div>
      </div>
      <div class="q-gutter-lg row" v-else>
        <div class="col-4">
          <q-input
            dense
            filled
            label-slot
            v-model="parametros.parteNombre"
            :rules="reglasParteNom"
            @update:model-value="cambiaronParametros"
          >
            <template v-slot:label>
              <div v-if="parametros.tipoPromovente === 'promovente'">
                Nombre
              </div>
              <div v-else>Nombre *</div>
            </template></q-input
          >
        </div>
        <div class="col-4">
          <q-input
            dense
            filled
            label-slot
            v-model="parametros.parteApellidoPaterno"
            :rules="reglasParteAP"
            @update:model-value="cambiaronParametros"
          >
            <template v-slot:label>
              <div v-if="parametros.tipoPromovente === 'promovente'">
                Apellido paterno
              </div>
              <div v-else>Apellido paterno *</div>
            </template></q-input
          >
        </div>
        <div class="col">
          <q-input
            dense
            filled
            v-model="parametros.parteApellidoMaterno"
            label="Apellido materno"
            @update:model-value="cambiaronParametros"
            :rules="reglasApellidoMaterno"
          />
        </div>
      </div>
    </div>
  </div>
  <div
    v-if="parametros.tipoPromovente == 'autoridad'"
    class="column content-stretchs"
  >
    <!--Autoridad-->
    <div class="q-gutter-lg q-mb-md row items-start">
      <q-select
        v-cortarLabel
        @input-value="parametros.promoventeAutoridad = null"
        dense
        filled
        use-input
        class="col"
        v-model="parametros.promoventeAutoridad"
        option-label="nombreCompleto"
        option-value="catOganismoId"
        label="Busca una autoridad judicial *"
        lazy-rules
        :options="autoridadJudicialOptions"
        :rules="reglasAutoridadJudicial"
        @update:model-value="cambiaronParametros"
        @filter="buscarAutoridadPorTexto"
      >
        <template v-slot:option="scope">
          <q-item v-bind="scope.itemProps">
            <q-item-section>
              <q-item-label>{{ scope.opt.nombreCompleto }}</q-item-label>
              <q-item-label caption
                >{{ scope.opt.cargoDescripcion }} -
                {{ scope.opt.nombreOficial }}</q-item-label
              >
            </q-item-section>
          </q-item>
        </template>
      </q-select>
      <div class="col"></div>
    </div>
  </div>
  <div>
    <DialogConfirmacion
      v-model="showAlertCancelarCaptura"
      titulo="¿Deseas cancelar?"
      :subTitulo="
        `Si continúas los datos nuevos de tu ` + tab + ' no se guardarán.'
      "
      @aceptar="
        diagAgregarParte = false;
        limpiarCamposNuevoPromovente();
      "
    >
    </DialogConfirmacion>
    <q-dialog v-model="diagAgregarParte" persistent>
      <q-card style="min-width: 45%">
        <q-bar
          style="background-color: white; font-weight: bold; font-size: large"
          class="q-mt-sm"
        >
          <a>Agregar {{ tab }}</a>
        </q-bar>
        <q-card-section class="q-pt-none">
          <br />
          Selecciona el tipo de figura que promovió la promoción.
          <br />
        </q-card-section>
        <q-tabs
          v-model="tab"
          keep-alive
          dense
          class="text-grey"
          active-color="primary"
          indicator-color="primary"
          align="justify"
          narrow-indicator
        >
          <div class="row full-width">
            <div class="col">
              <q-tab name="Promovente" label="Promovente" />
            </div>
            <div v-if="tipoAsuntoSinParte" class="col">
              <q-tab name="Parte" label="Parte" />
            </div>
            <div class="col">
              <q-tab name="Autoridad" label="Autoridad Judicial" />
            </div>
          </div>
        </q-tabs>
        <q-separator />
        <p></p>
        <q-tab-panels v-model="tab" animated>
          <!--Formulario Promovente-->
          <q-tab-panel name="Promovente" class="q-pa-md">
            <q-item-label>
              Agrega un promovente nuevo.
              <br />
              <br />
            </q-item-label>
            <div class="row q-gutter-md">
              <q-select
                v-cortarLabel
                @input-value="parametros.tipoPromoventeCat = null"
                dense
                filled
                use-input
                option-label="descripcion"
                option-value="id"
                label="Tipo *"
                class="col-8 q-pr-md"
                :options="tipoPromoventeOptions"
                v-model="parametros.tipoPromoventeCat"
                @filter="filtrarTipoPromovente"
                lazy-rules
                :rules="reglasPromovente"
                @update:model-value="cambiaronParametros"
              >
              </q-select>
              <div class="col"></div>
            </div>
            <div v-if="tipoPromoveteSinDenominacion" class="row q-gutter-md">
              <q-input
                dense
                filled
                v-model="parametros.promoventeNombre"
                label="Nombre *"
                class="col"
                @update:model-value="cambiaronParametros"
                :rules="reglasPromoventeNom"
              />
              <q-input
                dense
                filled
                v-model="parametros.promoventeApellidoPaterno"
                label="Apellido paterno *"
                class="col"
                @update:model-value="cambiaronParametros"
                :rules="reglasPromoventeAP"
              />
              <q-input
                dense
                filled
                v-model="parametros.promoventeApellidoMaterno"
                label="Apellido materno"
                class="col"
                @update:model-value="cambiaronParametros"
                :rules="reglasApellidoMaterno"
              />
            </div>
            <div v-else class="row">
              <q-input
                dense
                class="col-8"
                filled
                v-model="parametros.promoventeNombre"
                label="Denominación *"
                :rules="reglasParteDen"
                @update:model-value="cambiaronParametros"
              />
              <div class="col"></div>
            </div>
            <q-checkbox v-model="asociarParte">Asociar Parte</q-checkbox>
            <div v-if="asociarParte == true">
              <q-select
                v-cortarLabel
                @input-value="parteExistenteSelected = null"
                dense
                filled
                use-input
                label-slot
                v-model="parteExistenteSelected"
                option-label="denominacionDeAutoridad"
                option-value="nombre"
                :options="parteExistenteOptions"
                class="col-4"
                @filter="filtrarParteExistente"
                @update:model-value="cambiaronParametros"
              >
                <template v-slot:label> Parte a asociar </template>
              </q-select>
            </div>
          </q-tab-panel>

          <!--Formulario Parte-->
          <q-tab-panel name="Parte" class="q-pa-md">
            <q-item-label>
              Agrega una parte nueva.
              <br />
              <br />
            </q-item-label>
            <div class="q-gutter-md row">
              <q-select
                v-cortarLabel
                @input-value="parametros.parteCatTipoPersona = null"
                dense
                filled
                use-input
                label-slot
                v-model="parametros.parteCatTipoPersona"
                option-label="descripcion"
                option-value="catTipoPersonaId"
                :options="tipoPersonaOptions"
                class="col"
                @filter="filtrarTipoPersona"
                :rules="reglasParteTP"
                @update:model-value="cambiaronParametros"
              >
                <template v-slot:label>
                  <div>Tipo de persona *</div>
                </template>
              </q-select>
              <q-select
                v-cortarLabel
                @input-value="parametros.parteCatTipoPersonaCaracter = null"
                dense
                filled
                use-input
                label-slot
                v-model="parametros.parteCatTipoPersonaCaracter"
                option-label="caracterPersona"
                option-value="caracterPersonaId"
                :options="tipoPersonaCaracterOptions"
                :rules="reglasPartePC"
                class="col"
                @filter="filtrarTipoCaracter"
                @update:model-value="cambiaronParametros"
              >
                <template v-slot:label>
                  <div>Carácter *</div>
                </template>
              </q-select>
              <div class="col"></div>
            </div>

            <div
              class="row q-gutter-lg"
              v-if="
                parametros.parteCatTipoPersona?.catTipoPersonaId == 2 ||
                parametros.parteCatTipoPersona?.catTipoPersonaId == 3
              "
            >
              <div class="col-8 q-pr-lg">
                <q-input
                  dense
                  filled
                  v-model="parametros.denominacionDeAutoridad"
                  label="Denominación *"
                  :rules="reglasParteDen"
                  @update:model-value="cambiaronParametros"
                />
              </div>
              <div class="col"></div>
            </div>
            <div class="row q-gutter-md" v-else>
              <div class="col">
                <q-input
                  dense
                  filled
                  label-slot
                  v-model="parametros.parteNombre"
                  :rules="reglasParteNom"
                  @update:model-value="cambiaronParametros"
                >
                  <template v-slot:label>
                    <div>Nombre *</div>
                  </template>
                </q-input>
              </div>
              <div class="col">
                <q-input
                  dense
                  filled
                  label-slot
                  v-model="parametros.parteApellidoPaterno"
                  :rules="reglasParteAP"
                  @update:model-value="cambiaronParametros"
                >
                  <template v-slot:label>
                    <div>Apellido paterno *</div>
                  </template>
                </q-input>
              </div>
              <div class="col">
                <q-input
                  dense
                  filled
                  v-model="parametros.parteApellidoMaterno"
                  label="Apellido materno"
                  @update:model-value="cambiaronParametros"
                  :rules="reglasApellidoMaterno"
                />
              </div>
            </div>
          </q-tab-panel>

          <!--Formulario Autoridad-->
          <q-tab-panel name="Autoridad" class="q-pa-md">
            <q-item-label>
              Escribe el nombre de la autoridad y selecciona de la lista.
              <br />
              <br />
            </q-item-label>
            <q-select
              v-cortarLabel
              @input-value="parametros.promoventeAutoridad = null"
              dense
              filled
              use-input
              class="col"
              v-model="parametros.promoventeAutoridad"
              option-label="nombreCompleto"
              option-value="catOganismoId"
              label="Busca una autoridad judicial *"
              lazy-rules
              :options="autoridadJudicialOptions"
              :rules="reglasAutoridadJudicial"
              @update:model-value="cambiaronParametros"
              @filter="buscarAutoridadPorTexto"
            >
              <template v-slot:option="scope">
                <q-item v-bind="scope.itemProps">
                  <q-item-section>
                    <q-item-label>{{ scope.opt.nombreCompleto }}</q-item-label>
                    <q-item-label caption>
                      {{ scope.opt.cargoDescripcion }} -
                      {{ scope.opt.nombreOficial }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </template>
            </q-select>
          </q-tab-panel>
        </q-tab-panels>
        <q-separator />
        <div class="q-pa-md q-gutter-sm">
          <q-btn
            style="min-width: 164px"
            no-caps
            color="blue"
            label="Agregar"
            @click="agregarNuevo(tab)"
          />
          <q-btn
            style="min-width: 164px"
            no-caps
            outline
            color="blue"
            label="Cancelar"
            @click="showAlertCancelarCaptura = true"
          />
        </div>
        <q-inner-loading :showing="agregandoNuevoPromovente">
          <template v-slot>
            <q-spinner size="40px" />
            <div>Guardando datos</div>
          </template>
        </q-inner-loading>
      </q-card>
    </q-dialog>
  </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount, watch, computed } from "vue";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { Utils } from "../../../helpers/utils";
import { FormPromocion } from "../data/form-promocion";
import { Validaciones } from "../../../helpers/validaciones";
import { manejoErrores } from "../../../helpers/manejo-errores";
import { GuardarPromoventes } from "../data/guardar-promoventes";
import { GuardarPersonasAsuntos } from "../data/guardar-personas-asuntos";
import { GuardarAutoridadJudicial } from "../data/guardar-autoridad-judicial";
import { usePromoventesStore } from "../stores/promoventes-store";
import { noty } from "src/helpers/notify";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";

const parametros = ref(new FormPromocion());
const catalogosStore = useCatalogosStore();
const usuariosStore = useUsuariosStore();
const parteExistenteSelected = ref();
const diagAgregarParte = ref(false);
const tab = ref("Promovente");
const promoventesStore = usePromoventesStore();
const asociarParte = ref(false);
const cargandoDatosTabla = ref(false);
let rows = ref([]);
const noAgregarNuevoPromovente = ref(false);
const agregandoNuevoPromovente = ref(false);
const showAlertCancelarCaptura = ref(false);
const copyPartePromoventeAutoridadOptions = ref([]);
let tipoPromoventeOptions = ref([]);
let tipoPersonaOptions = ref([]);
let parteExistenteOptions = ref([]);
let promoventeExistenteOptions = ref([]);
let tipoPersonaCaracterOptions = ref([]);
let autoridadJudicialOptions = ref([]);
//let busquedaAutoridaJudicialOpcions = ref([]);

const tiposPartes = [
  { label: "Todas las partes", status: 0, color: "bg-grey-2" },
  { label: "Parte", status: 1, inactive: false, color: "bg-blue-1" },
  { label: "Promovente", status: 2, color: "bg-red-1" },
  { label: "Autoridad Judicial", status: 3, color: "bg-yellow-1" },
];

const props = defineProps({
  modelValue: {
    default: "",
  },
  detallePromocion: {
    default: {},
  },
  expedienteNuevo: {
    default: false,
  },
  promocion: {
    type: Object,
  },
});

function setSelecter(val) {
  rows.value = [];
  if (val.length > 0) {
    noAgregarNuevoPromovente.value = false;
    rows.value = val;
    setNuevoPromoventeAGuardar(val);
    parametros.value.isSaved = true;
  } else {
    limpiarCamposNuevoPromovente();
    noAgregarNuevoPromovente.value = false;
    rows.value = copyPartePromoventeAutoridadOptions.value;
    let tipoActual = parametros.value.filtroTipoPromovente;
    aplicarFiltroPromoventes(tipoActual);
    parametros.value.isSaved = false;
  }
  cambiaronParametros();
}

function setNuevoPromoventeAGuardar(val) {
  val.forEach((item) => {
    parametros.value.clasePromovente = item.clasePromovente;
    parametros.value.tipoDePromovente = item.tipoDePromovente;
  });
}

const usuariosAutoridadJudicialAfterFindExpediente = computed(() => {
  return usuariosStore.autoridadJudicialAfterFindExpediente;
});
const tipoAsuntoSinParte = computed(() => {
  if (
    props.detallePromocion &&
    (props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 18 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 19 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 28 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 44 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 45 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 55 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 56 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 69 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 72 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 77 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 78 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 82 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 83 ||
      props.detallePromocion.tipoAsunto?.catTipoAsuntoId === 128)
  ) {
    tiposPartes.forEach((item) => {
      if (item.hasOwnProperty("inactive")) {
        item.inactive = true;
      }
    });
    return false;
  } else {
    tiposPartes.forEach((item) => {
      if (item.hasOwnProperty("inactive")) {
        item.inactive = false;
      }
    });
    return true;
  }
});
const tipoPromoveteSinDenominacion = computed(() => {
  if (
    props.detallePromocion &&
    (props.detallePromocion.tipoPromoventeCat?.id === 466 ||
      props.detallePromocion.tipoPromoventeCat?.id === 470 ||
      props.detallePromocion.tipoPromoventeCat?.id === 2350)
  )
    return false;
  else return true;
});

const autoridadNueva = computed(() => {
  if (
    usuariosAutoridadJudicialAfterFindExpediente.value.includes(
      parametros.value.promoventeAutoridad,
    )
  ) {
    return false;
  } else {
    return true;
  }
});

const columns = [
  {
    name: "tipo",
    label: "Tipo",
    align: "left",
    field: "tipo",
    sortable: true,
  },
  {
    name: "nombre",
    label: "Nombre",
    align: "left",
    field: "nombre",
    sortable: true,
  },
  {
    name: "descripcion",
    label: "Descripción",
    align: "left",
    field: "descripcion",
    sortable: true,
  },
];

const coloresList = ref([]);
const getColor = (e) => coloresList.value.find((i) => i.tipo === e)?.color;
function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      tipo: 0,
      label: "Ver todos",
      icon: "mdi-filter-off",
    },
    {
      color: "bg-blue-1",
      status: 1,
      tipo: "Parte",
      label: "Parte",
    },
    {
      color: "bg-red-1",
      status: 2,
      tipo: "Promovente",
      label: "Promovente",
    },
    {
      color: "bg-yellow-1",
      status: 3,
      tipo: "Autoridad",
      label: "Autoridad",
    },
  ];
}

const emit = defineEmits({
  "update:modelValue": (value) => value !== null,
  "params:cambio": (val) => val !== null,
});
function cambiaronParametros() {
  emit("params:cambio", parametros);
  if (parametros.value.selected == []) {
    cambiaAutoridad(autoridadNueva.value);
  }
  // setTimeout(() => {}, 300);
}

function cambiaAutoridad(val) {
  usuariosStore.autoridadNuevaStore = val;
}
const stopWatchDetalle = watch(
  // eslint-disable-next-line no-unused-vars
  () => props.detallePromocion,
  async (_newValue) => {
    parametros.value = _newValue;
  },
  {
    immediate: true,
  },
);

/*const filter = reactive({
  text: "",
});*/

function aplicarFiltroPromoventes(val) {
  const needle = val.status;
  if (val.status != 0) {
    rows.value = copyPartePromoventeAutoridadOptions.value.filter(
      (v) => v.status === needle,
    );
  } else {
    rows.value = copyPartePromoventeAutoridadOptions.value;
  }
}

function limpiarCamposNuevoPromovente() {
  parametros.value.parteNombre = "";
  parametros.value.parteApellidoMaterno = "";
  parametros.value.parteApellidoPaterno = "";
  parametros.value.parteAutoridadDenominacion = "";
  parametros.value.parteCatTipoPersona = [];
  parametros.value.parteCatTipoPersonaCaracter = [];
  parametros.value.promoventeNombre = "";
  parametros.value.promoventeApellidoMaterno = "";
  parametros.value.promoventeApellidoPaterno = "";
  parametros.value.promoventeAutoridad = [];
  parametros.value.tipoPromoventeCat = [];
}

let timer = null;
watch(cargandoDatosTabla, (newValue) => {
  if (newValue) {
    timer = setTimeout(() => {
      cargandoDatosTabla.value = false;
    }, 1500);
  } else {
    clearTimeout(timer);
  }
});

onMounted(async () => {
  parametros.value = props.detallePromocion;
  usuariosStore.$subscribe(() => {
    cargandoDatosTabla.value = true;
    if (tipoAsuntoSinParte.value) {
      parteExistenteOptions.value = usuariosStore.parteExistente;
    } else {
      parteExistenteOptions.value = [];
    }
    promoventeExistenteOptions.value = usuariosStore.promoventeExistente;
    autoridadJudicialOptions.value = usuariosStore.autoridadJudicial;
    clearTimeout(timer);
    timer = setTimeout(() => {
      setRows();
      setColoresList();
      cargandoDatosTabla.value = false;
    }, 1500);
  });
  try {
    await catalogosStore.obtenerPromoventes(1);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoPromoventeOptions.value = catalogosStore.tiposPromovente;
  tipoPersonaCaracterOptions.value = catalogosStore.tipoPersonaCaracter;
  try {
    await catalogosStore.obtenerTipoPersona(
      parametros.value.tipoAsunto?.catTipoAsuntoId,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoPersonaOptions.value = catalogosStore.tipoPersona;
});

onBeforeUnmount(() => {
  stopWatchDetalle();
});

const reglasPromovente = true; //ref([
//   (val) => Validaciones.validaSelectRequerido(val?.id),
// ]);

const reglasParteTP = true; //computed(() => {
//   if (parametros.value.tipoPromovente === "parte")
//     return [(val) => Validaciones.validaInputRequerido(val)];
//   else return [];
// });

const reglasPartePC = true; //computed(() => {
//   if (parametros.value.tipoPromovente === "parte")
//     return [(val) => Validaciones.validaInputRequerido(val)];
//   else return [];
// });

const reglasPromoventeNom = true; //ref([
//   (val) => Validaciones.validaInputRequerido(val),
//   (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
// ]);

const reglasPromoventeAP = true; //ref([
//   (val) => Validaciones.validaInputRequerido(val),
//   (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
// ]);

const reglasParteNom = computed(() => {
  if (parametros.value.tipoPromovente === "parte")
    return [
      //(val) => Validaciones.validaInputRequerido(val),
      (val) =>
        Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
    ];
  else
    return [
      (val) =>
        Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
    ];
});

const reglasParteAP = computed(() => {
  if (parametros.value.tipoPromovente === "parte")
    return [
      //(val) => Validaciones.validaInputRequerido(val),
      (val) =>
        Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
    ];
  else
    return [
      (val) =>
        Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
    ];
});
const reglasApellidoMaterno = ref([
  (val) => Validaciones.validaAlfanumericoConAlgunosCaracteresEspeciales(val),
]);

const reglasParteDen = true; //computed(() => {
//   return [(val) => Validaciones.validaInputRequerido(val)];
// });

const reglasAutoridadJudicial = true; //ref([
//   (val) =>
//     parametros.value?.tipoPromovente == "autoridad"
//       ? Validaciones.validaSelectRequerido(val?.empleadoId)
//       : true,
// ]);

async function agregarNuevo(tipo) {
  agregandoNuevoPromovente.value = true;
  if (tipo === "Promovente") {
    const promovente = new GuardarPromoventes();
    promovente.asuntoNeunId = parametros.value.asuntoNeunId;
    promovente.tipo = parametros.value.tipoPromoventeCat.id;
    promovente.nombre = parametros.value.promoventeNombre;
    promovente.aPaterno = parametros.value.promoventeApellidoPaterno;
    promovente.aMaterno = parametros.value.promoventeApellidoMaterno;
    promovente.numeroOrden = parametros.value.numeroOrden;
    if (asociarParte.value == true && parteExistenteSelected.value != null) {
      promovente.personaId = parteExistenteSelected.value.personaId;
    }
    try {
      await promoventesStore.crearPromovente(promovente);
      parametros.value.isSaved = true;
      noty.correcto("¡Promovente guardado exitosamente!");
      usuariosStore.promoventeExistente = [];
      try {
        await usuariosStore.obtenerPromoventeExistente(
          parametros.value.asuntoNeunId,
        );
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
      parametros.value.parteExistenteGeneral =
        parametros.value.promoventeNombre +
        " " +
        parametros.value.promoventeApellidoPaterno +
        " " +
        parametros.value.promoventeApellidoMaterno;
      parametros.value.tipoPromoventeCat.id = 0;
      parametros.value.promoventeNombre = "";
      parametros.value.promoventeApellidoPaterno = "";
      parametros.value.promoventeApellidoMaterno = "";
    } catch (error) {
      manejoErrores.mostrarError(error);
      parametros.value.isSaved = false;
    }
  }
  if (tipo === "Parte") {
    const parte = new GuardarPersonasAsuntos();
    parte.asuntoNeunId = parametros.value.asuntoNeunId;
    parte.usuarioCaptura = 0;
    parte.nombre = parametros.value.parteNombre;
    parte.aPaterno = parametros.value.parteApellidoPaterno;
    parte.aMaterno = parametros.value.parteApellidoMaterno;
    parte.catTipoPersonaId =
      parametros.value.parteCatTipoPersona.catTipoPersonaId;
    parte.catCaracterPersonaAsuntoId =
      parametros.value.parteCatTipoPersonaCaracter.caracterPersonaId;
    parte.denominacionDeAutoridad = parametros.value.denominacionDeAutoridad;
    parte.numeroOrden = parametros.value.numeroOrden;
    if (parte.catTipoPersonaId != 1) {
      parte.nombre = parte.denominacionDeAutoridad;
    }
    try {
      await promoventesStore.crearPersonasAsunto(parte);
      parametros.value.isSaved = true;
      noty.correcto("¡Parte guardada exitosamente!");
      parametros.value.parteExistenteGeneral =
        parametros.value.parteNombre +
        " " +
        parametros.value.parteApellidoPaterno +
        " " +
        parametros.value.parteApellidoMaterno;
      parametros.value.parteNombre = "";
      parametros.value.parteApellidoPaterno = "";
      parametros.value.parteApellidoMaterno = "";
      parametros.value.denominacionDeAutoridad = [];
      parametros.value.parteCatTipoPersona = [];
      parametros.value.parteCatTipoPersonaCaracter = [];
    } catch (error) {
      manejoErrores.mostrarError(error);
      parametros.value.isSaved = false;
    }
  }
  if (tipo === "Autoridad") {
    let autoridad = new GuardarAutoridadJudicial();
    autoridad.asuntoNeunId = parametros.value.asuntoNeunId;
    autoridad.empleadoId = parametros.value.promoventeAutoridad.empleadoId;
    autoridad.numeroOrden = parametros.value.numeroOrden;
    autoridad.catIdOrganismo =
      parametros.value.promoventeAutoridad.catOrganismoId;
    try {
      await promoventesStore.crearAutoridadJudicial(autoridad);
      parametros.value.isSaved = true;
      noty.correcto("¡Autoridad guardada exitosamente!");
      parametros.value.parteExistenteGeneral =
        parametros.value.promoventeAutoridad.nombreCompleto;
      parametros.value.promoventeAutoridad = [];
    } catch (error) {
      manejoErrores.mostrarError(error);
      parametros.value.isSaved = false;
    }
  }
  diagAgregarParte.value = false;
  limpiarCamposNuevoPromovente();
  setRows();
  agregandoNuevoPromovente.value = false;
}

function setRows() {
  rows.value = [];
  parametros.value.partePromoventeAutoridadOptions = [];
  parteExistenteOptions.value.forEach((parte) => {
    parametros.value.partePromoventeAutoridadOptions.push({
      nombre: parte.denominacionDeAutoridad,
      descripcion: parte.descripcionCaracterPersona,
      tipo: "Parte",
      status: 1,
      clasePromovente: 1,
      tipoDePromovente: parte.personaId,
    });
  });

  promoventeExistenteOptions.value.forEach((promovente) => {
    parametros.value.partePromoventeAutoridadOptions.push({
      nombre:
        promovente.nombre +
        " " +
        promovente.aPaterno +
        " " +
        promovente.aMaterno,
      descripcion: promovente.promoventeTipo,
      tipo: "Promovente",
      status: 2,
      clasePromovente: 2,
      tipoDePromovente: promovente.promoventeId,
    });
  });

  if (autoridadJudicialOptions.value[0]?.nombreCompleto != "") {
    autoridadJudicialOptions.value.forEach((autoridad) => {
      parametros.value.partePromoventeAutoridadOptions.push({
        nombre: autoridad.nombreCompleto,
        descripcion: autoridad.cargoDescripcion + " " + autoridad.nombreOficial,
        tipo: "Autoridad",
        status: 3,
        clasePromovente: 3,
        tipoDePromovente: autoridad.empleadoId,
      });
    });
  }
  copyPartePromoventeAutoridadOptions.value =
    parametros.value.partePromoventeAutoridadOptions;
  rows.value = parametros.value.partePromoventeAutoridadOptions || [];
  rows.value.forEach((row, index) => {
    row.index = index;
  });
}

/**
 * filtra tipo TipoPromovente
 * @param {*} val valor a buscar
 */
function filtrarTipoPromovente(val, update) {
  update(
    async () => {
      tipoPromoventeOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tiposPromovente,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

/**
 * filtra TipoPersona en combo
 * @param {*} val valor a buscar
 */
function filtrarTipoPersona(val, update) {
  update(
    async () => {
      tipoPersonaOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tipoPersona,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

/**
 * filtra TipoCaracter en combo
 * @param {*} val valor a buscar
 */
function filtrarTipoCaracter(val, update) {
  update(
    async () => {
      tipoPersonaCaracterOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.tipoPersonaCaracter,
        "caracterPersona",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

async function buscarAutoridadPorTexto(val, update, abort) {
  update(
    async () => {
      if (val === "" || val.length < 4) {
        abort();
        return;
      } else {
        try {
          await usuariosStore.obtenerAutoridadJudicial(val);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        autoridadJudicialOptions.value = usuariosStore.autoridadJudicial;
        //busquedaAutoridaJudicialOpcions.value = usuariosStore.autoridadJudicial;
      }
    },
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 700),
  );
}

/**
 * filtra TipoCaracter en combo
 * @param {*} val valor a buscar
 */
function filtrarParteExistente(val, update) {
  update(
    async () => {
      parteExistenteOptions.value = Utils.filtrarCombo(
        val,
        usuariosStore.parteExistente,
        "personaTipo",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>
<script>
export default {
  inheritAttrs: false,
};
</script>
