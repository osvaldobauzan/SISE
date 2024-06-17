<template>
  <q-card flat>
    <q-toolbar>
      <q-toolbar-title> Buscar Parte </q-toolbar-title>
    </q-toolbar>
    <q-separator></q-separator>
    <q-stepper v-model="step" ref="stepper" color="primary" animated style="min-width: 900px;">
      <template v-slot:navigation>
        <q-separator spaced></q-separator>
        <q-stepper-navigation>
          <q-btn
            v-if="step < 2"
            @click="$refs.stepper.next()"
            color="primary"
            label="Siguiente"
          />
          <q-btn
            v-if="step === 2"
            @click="
              $refs.stepper.next();
              buscarParte();
            "
            color="primary"
            :label="step === 2 ? 'Buscar' : 'Siguiente'"
          />
          <q-btn
            v-if="step > 1"
            flat
            color="primary"
            @click="$refs.stepper.previous()"
            label="Atrás"
            class="q-ml-sm"
          />
        </q-stepper-navigation>
      </template>
      <q-step
        :name="1"
        title="Selecciona organismo(s)"
        icon="mdi-bank"
        :done="step > 1"
        animated
      >
        <q-list dense>
          <q-item tag="label" v-ripple clickable @click="setActual">
            <q-item-section avatar>
              <q-radio v-model="filterType" val="actual" />
            </q-item-section>
            <q-item-section>
              <q-item-label>{{ user.nombreOficial }}.</q-item-label>
            </q-item-section>
          </q-item>
          <q-item tag="label" v-ripple clickable @click="setTodos">
            <q-item-section avatar>
              <q-radio v-model="filterType" val="todos" />
            </q-item-section>
            <q-item-section>
              <q-item-label>Todos los órganos jurisdiccionales.</q-item-label>
            </q-item-section>
          </q-item>
          <q-item tag="label" v-ripple clickable @click="setFiltro">
            <q-item-section avatar top>
              <q-radio v-model="filterType" val="filtro" />
            </q-item-section>
            <q-item-section>
              <q-item-label
                >Seleccionar por Circuito, Estado, Ciudades o
                Regiones.</q-item-label
              >
            </q-item-section>
          </q-item>
        </q-list>
        <div class="row q-gutter-sm">
          <q-select
            :disable="filterType !== 'filtro'"
            class="col"
            dense
            filled
            label="Circuitos"
            v-model="circuito"
            :options="storeBuscarParte.circuitos"
            @update:model-value="filterByCircuito"
          ></q-select>
          <q-select
            :disable="filterType !== 'filtro'"
            class="col"
            dense
            filled
            label="Estados"
            v-model="estado"
            :options="storeBuscarParte.estados"
            @update:model-value="filterByEstado"
          ></q-select>
          <q-select
            :disable="filterType !== 'filtro'"
            class="col"
            dense
            filled
            label="Ciudades"
            v-model="ciudad"
            :options="storeBuscarParte.ciudades"
            @update:model-value="filterByCiudad"
          ></q-select>
          <!-- <q-select
            :disable="filterType !== 'filtro'"
            class="col"
            dense
            filled
            label="Regiones"
            v-model="region"
            :options="regiones"
            @update:model-value="filterOrganismo('region')"
          ></q-select> -->
        </div>
        <div class="row">
          <div class="col">
            <q-table
              dense
              flat
              bordered
              class="q-mt-md my-sticky-header-table"
              row-key="catOrganismoId"
              selection="multiple"
              v-model:selected="selected"
              :loading="isLoading"
              :filter="filter"
              :title="title"
              :columns="columns"
              :pagination="pagination"
              :rows="rows"
              :visible-columns="visibleColumns"
            >
              <template v-slot:top-right>
                <q-input
                  dense
                  outlined
                  clearable
                  debounce="0"
                  v-model="filter"
                  placeholder="Buscar"
                >
                  <template v-slot:append>
                    <q-icon name="search" />
                  </template>
                </q-input>
              </template>
            </q-table>
          </div>
        </div>
      </q-step>
      <q-step
        :name="2"
        title="Buscar parte"
        icon="mdi-account"
        :done="step > 2"
        animated
      >
        <div class="row fit">
          <div class="col">
            <q-list dense>
              <q-item tag="label" v-ripple clickable>
                <q-item-section avatar>
                  <q-radio v-model="filterStep2" val="5anios" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Últimos 5 años.</q-item-label>
                </q-item-section>
              </q-item>
              <q-item tag="label" v-ripple clickable>
                <q-item-section avatar>
                  <q-radio v-model="filterStep2" val="fecha" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Selecciona las fechas.</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
            <div class="row q-gutter-x-md q-mx-md">
              <q-input
                :disable="filterStep2 !== 'fecha'"
                dense
                filled
                class="col"
                v-model="fechaInicio"
                label="Fecha inicio"
              >
                <template v-slot:append>
                  <q-icon name="mdi-calendar-month" class="cursor-pointer">
                    <q-popup-proxy
                      cover
                      transition-show="scale"
                      transition-hide="scale"
                    >
                      <q-date v-model="fechaInicio" mask="DD/MM/YYYY">
                        <div class="row items-center justify-end">
                          <q-btn
                            v-close-popup
                            label="Cerrar"
                            color="primary"
                            flat
                          />
                        </div>
                      </q-date>
                    </q-popup-proxy>
                  </q-icon>
                </template>
              </q-input>
              <q-input
                :disable="filterStep2 !== 'fecha'"
                dense
                filled
                class="col"
                v-model="fechaFin"
                label="Fecha fin"
              >
                <template v-slot:append>
                  <q-icon name="mdi-calendar-month" class="cursor-pointer">
                    <q-popup-proxy
                      cover
                      transition-show="scale"
                      transition-hide="scale"
                    >
                      <q-date v-model="fechaFin" mask="DD/MM/YYYY">
                        <div class="row items-center justify-end">
                          <q-btn
                            v-close-popup
                            label="Cerrar"
                            color="primary"
                            flat
                          />
                        </div>
                      </q-date>
                    </q-popup-proxy>
                  </q-icon>
                </template>
              </q-input>
            </div>
            <q-tabs
              v-model="tabTipoPersona"
              narrow-indicator
              align="left"
              class="text-primary q-mt-md"
            >
              <q-tab name="1" label="Física" />
              <q-tab name="2" label="Jurídica" />
              <q-tab name="3" label="Autoridad" />
            </q-tabs>
            <q-tab-panels v-model="tabTipoPersona">
              <q-tab-panel name="1">
                <div class="row q-gutter-x-sm">
                  <div class="col">
                    <q-input dense filled v-model="nombre" label="Nombre" />
                  </div>
                  <div class="col">
                    <q-input dense filled v-model="paterno" label="Paterno" />
                  </div>
                  <div class="col">
                    <q-input dense filled v-model="materno" label="Materno" />
                  </div>
                </div>
              </q-tab-panel>

              <q-tab-panel name="2">
                <q-input dense filled v-model="search" label="Denominación" />
              </q-tab-panel>

              <q-tab-panel name="3">
                <q-input dense filled v-model="search" label="Denominación" />
              </q-tab-panel>
            </q-tab-panels>
          </div>
          <q-separator vertical spaced></q-separator>
          <div class="col">
            <q-scroll-area style="height: 400px">
              <q-list dense bordered separator>
                <q-item-label header class="text-bold"
                  >Órganos seleccionados</q-item-label
                >
                <q-separator></q-separator>
                <q-item v-for="org in selected" :key="org">
                  <q-item-section>
                    <q-item-label>{{ org.nombreOficial }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-scroll-area>
          </div>
        </div>
      </q-step>
      <q-step :name="3" title="Resultado" icon="mdi-account-search" animated>
        <q-inner-loading :showing="isLoadingResultado"></q-inner-loading>
        <q-banner
          rounded
          class="bg-orange-1"
          v-if="storeBuscarParte.buscarPartes.length <= 0 && !isLoadingResultado"
        >
          <template v-slot:avatar>
            <q-icon name="mdi-file-remove-outline" color="primary" />
          </template>
          No se encontraron registros
        </q-banner>
        <q-list bordered separator v-else>
          <q-expansion-item
            active-class="bg-grey-4"
            group="tipoasunto"
            icon="mdi-layers"
            class="bg-grey-2"
            v-for="(tipoasunto, index) in storeBuscarParte.getTipoAsuntoList"
            :default-opened="index === 0"
            :key="tipoasunto.label"
          >
            <template v-slot:header>
              <q-item>
                <q-item-section avatar>
                  <q-avatar color="primary" text-color="white" size="md">
                    {{ tipoasunto.count }}
                  </q-avatar>
                </q-item-section>

                <q-item-section class="text-h6">
                  {{ tipoasunto.label }}
                </q-item-section>
              </q-item>
            </template>
            <q-separator></q-separator>
            <q-table
              flat
              dense
              row-key="asuntoNeunId"
              class="my-sticky-header-table"
              :rows="storeBuscarParte.groupedResult[tipoasunto.label]"
              :columns="columnsResultado"
              :pagination="{ rowsPerPage: 0 }"
              :loading="isLoadingResultado"
              style="min-height: 200px"
            >
              <template v-slot:body="props">
                <q-tr :props="props">
                  <q-td>
                    <q-item
                      dense
                      v-ripple
                      clickable
                      class="q-pa-none"
                      @click="
                        selectedItem = props.row;
                        maximizedToggle = false;
                        expedientes.push(props.row);
                        showExpediente = true;
                      "
                    >
                      <q-item-section>
                        <q-item-label
                          class="text-bold text-secondary"
                          style="text-decoration: underline"
                        >
                          {{ props.row.asuntoAlias }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    {{ props.row.nombre }}
                  </q-td>
                  <q-td>
                    {{ props.row.aPaterno }}
                  </q-td>
                  <q-td>
                    {{ props.row.aMaterno }}
                  </q-td>
                  <q-td>
                    {{ props.row.catCaracterPersonaAsuntoDescripcion }}
                  </q-td>
                </q-tr>
              </template>
            </q-table>
          </q-expansion-item>
        </q-list>
      </q-step>
    </q-stepper>
  </q-card>
  <q-dialog v-model="showExpediente" :maximized="maximizedToggle">
    <ModalWindowComponent
      :maximizedToggle="maximizedToggle"
      @toggle-maximized="maximizedToggle = !maximizedToggle"
    >
      <ExpedientePage
        :asuntoNeunId="selectedItem.asuntoNeunId"
        :asuntoAlias="selectedItem.asuntoAlias"
        :tipoAsunto="selectedItem.tipoAsuntoDescripcion"
      />
    </ModalWindowComponent>
  </q-dialog>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { date } from "quasar";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { useBuscarParteStore } from "../stores/buscar-parte-store";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";

const selectedItem = ref(null);
const maximizedToggle = ref(false);
const showExpediente = ref(false);
const expedientes = ref([]);
const storeBuscarParte = useBuscarParteStore();
const filterType = ref("actual");
const storeAuth = useAuthStore();
const { user } = storeAuth;
const step = ref(1);
const search = ref("");
const tabTipoPersona = ref("1");
const fechaInicio = ref("");
const fechaFin = ref("");
const nombre = ref("");
const paterno = ref("");
const materno = ref("");
const circuito = ref("");
const estado = ref("");
const ciudad = ref("");
const region = ref("");
const selected = ref([]);
const filter = ref("");
const title = ref("Actual");
const isLoading = ref(false);
const rows = ref(storeBuscarParte.organismos);
const visibleColumns = ref(["nombreOficial"]);
const filterStep2 = ref("5anios");
const year = new Date().getFullYear();
const isLoadingResultado = ref(false);

const pagination = {
  sortBy: "nombreOficial",
  descending: false,
  rowsPerPage: 0,
};

async function buscarParte() {
  const params = {
    CatOrganismoId:
      "[" + selected.value.map((row) => row.catOrganismoId).toString() + "]",
    CatTipoPersonaId: tabTipoPersona.value,
    FechaInicial:
      filterStep2.value === "5anios"
        ? ""
        : date.formatDate(fechaInicio.value, "YYYY-MM-DD"),
    FechaFinal:
      filterStep2.value === "5anios"
        ? ""
        : date.formatDate(fechaFin.value, "YYYY-MM-DD"),
    Nombre: tabTipoPersona.value === "1" ? nombre.value : search.value,
    APaterno: paterno.value,
    AMaterno: materno.value,
    Anio: year,
  };
  isLoadingResultado.value = true;
  await storeBuscarParte.BuscarPartes({ params: params });
  isLoadingResultado.value = false;
}

function unselectFilters(type) {
  circuito.value = circuito.value.value === type.value ? type : "";
  estado.value = estado.value.value === type.value ? type : "";
  ciudad.value = ciudad.value.value === type.value ? type : "";
  region.value = region.value.value === type.value ? type : "";
}

function filterByCircuito(value) {
  title.value = value.label;
  filter.value = "";
  unselectFilters(value);
  rows.value = storeBuscarParte.organismos.filter(
    (row) => row.catCircuitoId === value.value,
  );
  selected.value = [];
}

function filterByEstado(value) {
  title.value = value.label;
  filter.value = "";
  unselectFilters(value);
  rows.value = storeBuscarParte.organismos.filter(
    (row) => row.catEstadoId === value.value,
  );
  selected.value = [];
}

function filterByCiudad(value) {
  title.value = value.label;
  filter.value = "";
  unselectFilters(value);
  rows.value = storeBuscarParte.organismos.filter(
    (row) => row.catCiudadId === value.value,
  );
  selected.value = [];
}

async function setActual() {
  title.value = user.nombreOficial;
  filter.value = user.nombreOficial;
  rows.value = storeBuscarParte.organismos;
  selected.value = storeBuscarParte.organismos.filter(
    (row) => row.catOrganismoId === user.catOrganismoId,
  );
}

function setTodos() {
  title.value = "Todos los órganos jurisdiccionales";
  filter.value = "";
  rows.value = storeBuscarParte.organismos;
  selected.value = storeBuscarParte.organismos;
}

function setFiltro() {
  title.value = "Selecciona por Circuito, Estado oCiudad";
  filter.value = "";
  rows.value = storeBuscarParte.organismos;
  selected.value = [];
}

onMounted(async () => {
  isLoading.value = true;
  await storeBuscarParte.getOrganismos();
  setActual();
  isLoading.value = false;
});

const columnsResultado = [
  {
    name: "asuntoAlias",
    align: "left",
    label: "Expediente",
    field: "asuntoAlias",
    sortable: true,
  },
  {
    name: "Nombre",
    align: "left",
    label: "Nombre",
    field: "nombre",
    sortable: true,
  },
  {
    name: "APaterno",
    align: "left",
    label: "Paterno",
    field: "aPaterno",
    sortable: true,
  },
  {
    name: "AMaterno",
    align: "left",
    label: "Materno",
    field: "aMaterno",
    sortable: true,
  },
  {
    name: "catCaracterPersonaAsuntoDescripcion",
    align: "left",
    label: "Caracter",
    field: "catCaracterPersonaAsuntoDescripcion",
    sortable: true,
  },
];

const columns = [
  {
    name: "catOrganismoId",
    align: "left",
    label: "Id",
    field: "catOrganismoId",
    sortable: true,
  },
  {
    name: "nombreOficial",
    align: "left",
    label: "Órgano jurisdiccional",
    field: "nombreOficial",
    sortable: true,
  },
  {
    name: "catCircuitoId",
    align: "left",
    label: "Circuito",
    field: "catCircuitoId",
    sortable: true,
  },
  {
    name: "catCiudadId",
    align: "left",
    label: "Ciudad",
    field: "catCiudadId",
    sortable: true,
  },
  {
    name: "catEstadoId",
    align: "left",
    label: "Estado",
    field: "catEstadoId",
    sortable: true,
  },
];
</script>

<style lang="sass" scoped>
.my-sticky-header-table
  /* height or max-height is important */
  max-height: 270px

  .q-table__top,
  .q-table__bottom,

  thead tr th
    position: sticky
    z-index: 1
  thead tr:first-child th
    top: 0

  /* this is when the loading indicator appears */
  &.q-table--loading thead tr:last-child th
    /* height of all previous header rows */
    top: 48px

  /* prevent scrolling behind sticky top row on focus */
  tbody
    /* height of all previous header rows */
    scroll-margin-top: 48px
</style>
