<template>
  <q-page padding>
    <div v-if="!faltaOrganismo" v-permitido="1">
      <div class="row q-ma-md">
        <SelectRangeDateComponent
          title="Fecha"
          @update:selectedDate="setDate"
        ></SelectRangeDateComponent>
        <q-item class="col invisible" v-if="$q.screen.gt.xs">
          <q-item-section side>
            <q-icon name="mdi-arrow-bottom-right" color="negative"></q-icon>
          </q-item-section>
          <q-item-section side>
            <q-item-label class="text-h6 text-black">300</q-item-label>
          </q-item-section>
          <q-item-section>
            <q-item-label class="text-bold">Ingresos</q-item-label>
            <q-item-label caption>Expedientes</q-item-label>
          </q-item-section>
        </q-item>
        <q-item class="col invisible" v-if="$q.screen.gt.xs" hidden>
          <q-item-section side>
            <q-icon name="mdi-arrow-top-right" color="positive"></q-icon>
          </q-item-section>
          <q-item-section side>
            <q-item-label class="text-h6 text-black">50</q-item-label>
          </q-item-section>
          <q-item-section>
            <q-item-label class="text-bold">Egresos</q-item-label>
            <q-item-label caption>Expedientes</q-item-label>
          </q-item-section>
        </q-item>
        <div class="col" v-if="$q.screen.gt.sm"></div>
        <div class="col" v-if="$q.screen.gt.sm"></div>
      </div>

      <div class="q-mr-md" v-if="$q.screen.gt.xs">
        <CardStatusComponent
          :listCards="cards"
          @select-status="(n) => (isSelected = n)"
        />
      </div>
      <q-tabs v-model="isSelected" v-if="!$q.screen.gt.xs" class="q-ma-lg">
        <q-tab :name="card.key" v-for="card in cards" :key="card.key">
          <q-card
            class="col cursor-pointer"
            :class="
              isSelected === card.key
                ? 'bg-grey-8 shadow-8 text-white'
                : 'text-grey-8'
            "
            :key="card.key"
          >
            <q-list clickable v-ripple @click="SelectStatus(card.key)">
              <q-item>
                <q-item-section side v-if="card.icon">
                  <q-icon
                    flat
                    size="xm"
                    :name="card.icon"
                    :class="
                      selectedStatus === card.key ? 'text-white' : 'text-grey-8'
                    "
                  />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    <span class="text-h6">{{ card.name }}</span>
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label>
                    <div class="row fit items-end">
                      <div class="col-auto text-h4 text-bold">
                        {{ card.progress }}
                      </div>
                      <div class="col q-ml-sm q-mb-xs">de {{ card.total }}</div>
                    </div>
                  </q-item-label>
                  <q-item-label>
                    <div class="row q-mt-sm">
                      <q-item-label class="text-caption">{{
                        card.caption
                      }}</q-item-label>
                      <q-space></q-space>
                      <q-item-label class="text-caption"
                        >{{ getProgress(card) }}%</q-item-label
                      >
                    </div>
                  </q-item-label>
                  <q-item-label>
                    <div class="row q-mt-sm">
                      <q-linear-progress
                        rounded
                        size="md"
                        :value="card.progress / card.total"
                        color="green-14"
                        :class="selectedStatus === card.key ? 'bg-white' : ''"
                      >
                      </q-linear-progress>
                    </div>
                  </q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-card>
        </q-tab>
      </q-tabs>
      <q-tab-panels v-model="isSelected" class="bg-transparent">
        <q-tab-panel name="oficialia">
          <OficialiaCard :rowsOficialia="rowsOficialia" :selectedDate="selectedDate"></OficialiaCard>
          <q-inner-loading :showing="cargandoOficialia" />
        </q-tab-panel>
        <q-tab-panel name="tramite">
          <TramiteCard :rowsTramite="rowsTramite"></TramiteCard>
          <q-inner-loading :showing="cargandoTramite" />
        </q-tab-panel>
        <q-tab-panel name="actuaria">
          <ActuariaCard :selectedDate="selectedDate"></ActuariaCard>
        </q-tab-panel>
        <q-tab-panel name="sentencias">
          <SentenciasCard></SentenciasCard>
        </q-tab-panel>
        <q-tab-panel name="ejecucion">
          <EjecucionCard></EjecucionCard>
        </q-tab-panel>
      </q-tab-panels>
    </div>

    <q-dialog v-model="faltaOrganismo" persistent>
      <SelectOrganismo
        @cerrar="
          async () => {
            faltaOrganismo = false;
            if (authStore.user.privilegios?.length > 0) await cargaTableros();
            }
        "
      ></SelectOrganismo>
    </q-dialog>
    <DialogConfirmacion
      :model-value="!faltaOrganismo && authStore.user.privilegios?.length == 0"
      :label-btn-ok="'Salir'"
      @aceptar="cerrarSesion()"
      sub-titulo="Solicítalos al administrador del sistema."
      titulo="No cuentas con permisos en el sistema."
      icon="mdi-information"
      color="blue-3"
      :showCancelar="false"
    ></DialogConfirmacion>
  </q-page>
</template>

<script setup>
import { date } from "quasar";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { useRouter } from "vue-router";
import { ref, onMounted, computed } from "vue";

import SelectOrganismo from "src/components/SelectOrganismo.vue";
import CardStatusComponent from "src/components/CardStatusComponent.vue";
import OficialiaCard from "src/modules/index/components/oficialia/oficialiaCard.vue";
import TramiteCard from "src/modules/index/components/tramite/tramiteCard.vue";
import ActuariaCard from "src/modules/index/components/actuaria/actuariaCard.vue";
import EjecucionCard from "src/modules/index/components/ejecucion/ejecucionCard.vue";
import SentenciasCard from "src/modules/index/components/sentencias/sentenciasCard.vue";
import { useIndexStore } from "../stores/index-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import SelectRangeDateComponent from "src/components/SelectRangeDateComponent.vue";

const router = new useRouter();
const authStore = useAuthStore();
const faltaOrganismo = ref(!!!authStore.user?.catOrganismoId);
const isSelected = ref("tramite");
const indexStore = useIndexStore();
const cargandoOficialia = ref(false);
const cargandoTramite = ref(false);
let rowsOficialia = ref([]);
let rowsTramite = ref([]);

const selectedDate = ref({
  from: date.formatDate(Date.now(), "DD/MM/YYYY"),
  to: date.formatDate(Date.now(), "DD/MM/YYYY"),
});

async function setDate(fecha) {
  selectedDate.value = fecha;
  if (!faltaOrganismo.value && authStore.user.privilegios?.length > 0) {
    await indexStore.obtenerKpis(selectedDate.value.from, selectedDate.value.to);
    await indexStore.obtenerIndicadoresZonas(selectedDate.value.from, selectedDate.value.to, authStore.user.catOrganismoId);
    await cargaTableros();
  }
}
const cards = computed(() => {
  return [
    {
      key: "oficialia",
      name: "Oficialía",
      icon: "mdi-inbox",
      progress: totalAsignadosMesa(),
      total: totalOficialia(),
      caption: "Promociones Turnadas",
    },
    {
      key: "tramite",
      name: "Trámite",
      icon: "mdi-file-cog",
      progress: totalAutorizados(),
      total: totalTramite(),
      caption: "Acuerdos Autorizados",
    },
    {
      key: "actuaria",
      name: "Actuaría",
      icon: "mdi-email",
      progress: indexStore.totalNotificaciones.notificadas,
      total: indexStore.totalNotificaciones.total,
      caption: "Notificaciones realizadas",
    },
    {
      key: "sentencias",
      name: "Sentencias",
      icon: "mdi-notebook",
      progress: 10,
      total: 30,
      caption: "Autorizadas",
      invisible: true,
    },
    {
      key: "ejecucion",
      name: "Ejecución",
      icon: "mdi-gavel",
      progress: 1,
      total: 9,
      caption: "Sentencias cumplidas",
      invisible: true,
    },
  ];
});

const getProgress = (card) =>
  parseFloat((card.progress / card.total) * 100).toFixed(0);

function totalAutorizados() {
  let autorizados = 0;
  rowsTramite.value.forEach((e) => {
    if (parseInt(e.estado) === 4) {
      autorizados++;
    }
  });
  return autorizados;
}

function totalAsignadosMesa() {
  let asignados = 0;
  rowsOficialia.value.forEach((e) => {
    if (parseInt(e.estado) === 4) {
      asignados++;
    }
  });
  return asignados;
}

function totalTramite() {
  return rowsTramite.value.length;
}

function totalOficialia() {
  return rowsOficialia.value.length;
}

onMounted(async () => {
  if (!faltaOrganismo.value && authStore.user.privilegios?.length > 0) {
    await cargaTableros();
    await indexStore.obtenerKpis(selectedDate.value.from, selectedDate.value.to);
    await indexStore.obtenerIndicadoresZonas(selectedDate.value.from, selectedDate.value.to, authStore.user.catOrganismoId);
  }
});

async function getOficialiaRows() {
  cargandoOficialia.value = true;
  if (selectedDate.value != undefined){
    try {
    await indexStore.obtenerPromociones({
      ...selectedDate.value,
      text: "",
      status: 0,
      sortBy: "Promoción",
      descending: false,
      page: 1,
      rowsPerPage: 0,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  rowsOficialia.value = indexStore.oficialiaData.datos || [];
  cargandoOficialia.value = false;
  }
}

async function getTramiteRows() {
  cargandoTramite.value = true;
  if (selectedDate.value != undefined){
    try {
    await indexStore.obtenerTramites({
      ...selectedDate.value,
      text: "",
      status: 0,
      sortBy: "Promoción",
      descending: true,
      page: 1,
      rowsPerPage: 0,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  rowsTramite.value = indexStore.tramitesData.datos || [];
  cargandoTramite.value = false;
  }
}

async function cargaTableros() {
  const pInicio = localStorage.getItem("pInicio");
  localStorage.removeItem("pInicio");
  if (!(pInicio == "null" || pInicio == null)) {
    router.push("/" + pInicio);
  }
  await Promise.all([getOficialiaRows(), getTramiteRows()]);
}
async function cerrarSesion() {
  await authStore.logoutUser(true);
}
</script>
