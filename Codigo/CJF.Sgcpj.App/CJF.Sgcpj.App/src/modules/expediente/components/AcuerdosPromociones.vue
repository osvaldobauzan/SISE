<template>
  <q-card>
    <q-tabs
      v-model="cuadernoId"
      no-caps
      stretch
      :key="tab"
      switch-indicator
      class="bg-grey-4 row"
      active-class="bg-white"
      align="left"
    >
      <q-tab
        v-for="tab in tabs"
        :key="tab.cuadernoId"
        :name="tab.cuadernoId"
        :label="tab.cuaderno"
      />
    </q-tabs>
    <q-toolbar>
      <q-toolbar-title>Promociones, acuerdos y notificaciones</q-toolbar-title>
    </q-toolbar>
    <q-table
      flat
      bordered
      dense
      wrap-cells
      virtual-scroll
      width="auto"
      :columns="columns"
      :rows="dataAcuerdosPromociones"
      row-key="index"
      loading-label="Cargando..."
      no-data-label="No se encontraron registros"
      no-results-label="No se
    encontraron registros en este momento"
      rows-per-page-label="Registros por
    página:"
      :loading="loading"
      :pagination="initialPagination"
    >
      <template v-slot:loading>
        <q-inner-loading showing color="primary" />
      </template>
      <template v-slot:header="props">
        <q-tr :props="props">
          <q-th
            v-for="col in props.cols"
            :key="col"
            :class="`bg-${getColor(col.group)}`"
            :align="col.align"
          >
            {{ col.label }}
          </q-th>
        </q-tr>
      </template>

      <template v-slot:body="props">
        <q-tr :props="props" :class="`${rowClass(props.rowIndex)}`">
          <q-td class="text-center">
            <q-btn
              flat
              stack
              color="secondary"
              icon="mdi-paperclip"
              @click="verPromocion(props.row)"
              v-if="props.row.fechaPresentacion"
              :label="props.row.numeroRegistro === 0 ? '' : props.row.numeroRegistro"
            >
              <q-tooltip>Ver promoción</q-tooltip>
            </q-btn>
            <q-item-section>
              <q-item-label>
                {{
                  date.formatDate(
                    props.row.fechaPresentacion,
                    "DD/MM/YYYY HH:mm",
                  )
                }}
              </q-item-label>
            </q-item-section>
          </q-td>
          <q-td>
            <q-item-label class="text-center">
              {{ props.row.origenCorto }}
            </q-item-label>

            <q-tooltip>{{ props.row.origenPromo }}</q-tooltip>
          </q-td>
          <q-td>
            <q-item-label class="text-center">
              {{ props.row.folio }}
            </q-item-label>
          </q-td>
          <q-td>
            <q-item-label>
              {{ props.row.tipoContenidoDescripcion }}
            </q-item-label>
          </q-td>
          <q-td>
            <q-item-section>
              <q-item-label>
                {{ props.row.promovente }}
              </q-item-label>
              <q-item-label class="text-secondary" caption>
                {{ props.row.caracterPromovente }}
              </q-item-label>
            </q-item-section>
          </q-td>
          <q-td class="text-center">
            <q-btn
              flat
              stack
              color="secondary"
              icon="mdi-paperclip"
              :label="props.row.fechaAcuerdo_F"
              @click="verAcuerdo(props.row)"
              v-if="
                props.row.asuntoDocumentoNombre !== null &&
                props.row.asuntoDocumentoId
              "
            >
              <q-tooltip>Ver acuerdo</q-tooltip>
            </q-btn>
            <q-item-label>
              {{ props.row.plantillaDocumento }}
            </q-item-label>
          </q-td>
          <q-td>
            <q-item-label class="text-center">
              {{ props.row.mesa }}
            </q-item-label>
          </q-td>
          <q-td>
            <q-item-label class="text-center">
              {{ props.row.userNameSecretario }}
            </q-item-label>
          </q-td>
          <q-td>
            <q-item-label class="text-center">
              {{ props.row.porLista_F }}
            </q-item-label>
          </q-td>
          <q-td>
            <q-item-label class="text-center">
              {{ props.row.porOficio }}
            </q-item-label>
          </q-td>
        </q-tr>
      </template>
    </q-table>
  </q-card>

  <q-dialog v-model="showAcuerdoPdf" full-height full-width>
    <VerAcuerdo :model-value="acuerdoSeleccionado"></VerAcuerdo>
  </q-dialog>
  <q-dialog v-model="showPromocionPdf" full-height full-width>
    <VerPromociones promocionDocumento :promocion="promocionSeleccionada" />
  </q-dialog>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { date } from "quasar";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
import VerPromociones from "src/modules/oficialia/components/VerPromociones.vue";
import { manejoErrores } from "src/helpers/manejo-errores";

const cuadernoId = ref(0);
const tabs = ref([
  {
    cuadernoId: 0,
    cuaderno: "Todos",
  },
]);

const showAcuerdoPdf = ref(false);
const showPromocionPdf = ref(false);

const expedienteElectronicoStore = useExpedienteElectronicoStore();
const loading = ref(false);
const acuerdoSeleccionado = ref({});
const promocionSeleccionada = ref({});

const props = defineProps({
  asuntoNeunId: {
    type: Number,
    required: true,
  },
});

const dataAcuerdosPromociones = computed(() => {
  return expedienteElectronicoStore.filtrarPorCuadernoId(
    props.asuntoNeunId,
    cuadernoId.value,
  );
});

const columns = [
  {
    name: "Presentación",
    label: "Presentación",
    align: "center",
    field: "fechaPresentacion",
    group: "promocion",
  },

  {
    name: "Origen",
    align: "center",
    label: "Origen",
    field: "origenCorto",
    group: "promocion",
  },
  {
    name: "Folio",
    align: "center",
    label: "Folio",
    field: "folio",
    group: "promocion",
  },
  {
    name: "Contenido",
    align: "left",
    label: "Contenido",
    field: "tipoContenidoDescripcion",
    group: "promocion",
  },
  {
    name: "Promovente",
    align: "left",
    label: "Promovente",
    field: "promovente",
    group: "promocion",
  },
  // {
  //   name: "Archivo",
  //   align: "center",
  //   label: "Archivo",
  //   field: "asuntoDocumentoNombre",
  //   group: "acuerdo",
  // },
  {
    name: "Acuerdo",
    align: "center",
    label: "Acuerdo",
    field: "fechaAcuerdo_F",
    group: "acuerdo",
  },
  {
    name: "Mesa",
    align: "Center",
    label: "Mesa",
    field: "mesa",
    group: "acuerdo",
  },
  {
    name: "Secretario",
    align: "Center",
    label: "Secretario",
    field: "userNameSecretario",
    group: "acuerdo",
  },
  {
    name: "Por Lista",
    align: "Center",
    label: "Por Lista",
    field: "porLista_F",
    group: "notificaciones",
  },
  {
    name: "Por Oficio",
    align: "Center",
    label: "Por Oficio",
    field: "porOficio",
    group: "notificaciones",
  },
];
const coloresList = [
  { color: "green-2", group: "promocion" },
  { color: "blue-2", group: "acuerdo" },
  { color: "pink-2", group: "notificaciones" },
];

const getColor = (e) => coloresList.find((i) => i.group === e).color;

const initialPagination = {
  sortBy: "desc",
  descending: false,
  rowsPerPage: 0,
};

onMounted(async () => {
  loading.value = true;
  try {
    let acuerdosPromociones =
      await expedienteElectronicoStore.obtenerExpedienteElectronico(
        props.asuntoNeunId,
      );
    const acuerdos = [];
    acuerdosPromociones.forEach((grupo) => {
      const acuerdo = {
        cuadernoId: grupo.cuadernoId,
        cuaderno: grupo.cuaderno,
      };
      const existeCuadernoId = acuerdos.some(
        (objeto) => objeto.cuadernoId === grupo.cuadernoId,
      );
      if (!existeCuadernoId) {
        acuerdos.push(acuerdo);
      }
    });

    tabs.value = [...tabs.value, ...acuerdos];
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  loading.value = false;
});

function rowClass(index) {
  return index % 2 === 0 ? "fila-par" : "fila-impar";
}
async function verAcuerdo(acuerdo) {
  acuerdoSeleccionado.value = {
    expediente: {
      asuntoNeunId: acuerdo.asuntoNeunId,
    },
    asuntoDocumentoId: acuerdo.asuntoDocumentoId,
  };

  showAcuerdoPdf.value = true;
}
async function verPromocion(promocion) {
  const promocionJson = {
    asuntoNeunId: promocion.asuntoNeunId,
    yearPromocion: promocion.anioPromocion,
    numeroOrden: promocion.numeroOrden,
    origen: promocion.origenPromocion,
  };
  promocionSeleccionada.value = promocionJson;
  showPromocionPdf.value = true;
}
</script>
<style>
.fila-par {
  background-color: #f2f2f2;
}

.fila-impar {
  background-color: #fff;
}
</style>
