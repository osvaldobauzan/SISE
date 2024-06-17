<template>
  <q-card>
    <q-tabs
      v-model="tab"
      no-caps
      stretch
      switch-indicator
      class="q-mt-lg bg-grey-4"
      active-class="bg-white"
      align="left"
    >
      <q-tab name="principal" label="Principal" />
      <q-tab name="incidental" label="Incidental" />
    </q-tabs>
    <q-toolbar>
      <q-toolbar-title>Promociones, Acuerdos y Notificaciones</q-toolbar-title>
    </q-toolbar>
    <q-tab-panels v-model="tab" animated>
      <q-tab-panel name="principal">
        <q-table
          flat
          bordered
          hide-header
          dense
          wrap-cells
          virtual-scroll
          width="auto"
          :rows="dataAcuerdosPromociones"
          row-key="acuerdo"
          loading-label="Cargando..."
          no-data-label="No se encontraron registros"
          no-results-label="No se
    encontraron registros en este momento"
          rows-per-page-label="Registros por
    página:"
          :pagination="initialPagination"
        >
          <template v-slot:body="props">
            <q-tr :props="props" :class="`bg-${getColor(props.row.estado)}`">
              <q-td>
                <q-item>
                  <q-item-section side>
                    <q-btn
                      flat
                      round
                      :icon="getIcon(props.row.tipo)"
                      color="secondary"
                    >
                      <q-tooltip>Ver {{ props.row.tipo }}</q-tooltip>
                    </q-btn>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.nombre }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ props.row.tipo }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ date.formatDate(props.row.fecha, "DD/MM/YYYY") }}
                    </q-item-label>
                    <q-item-label caption> Fecha elaboración </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.numeroPromociones }}
                    </q-item-label>
                    <q-item-label caption> Promociones </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.estado }}
                    </q-item-label>
                    <q-item-label caption> Estado </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td></q-td>
              <q-td>
                <q-btn flat round size="sm" color="secondary" icon="mdi-eye" />
              </q-td>
              <q-td>
                <q-btn
                  flat
                  round
                  color="primary"
                  :icon="props.expand ? 'mdi-chevron-up' : 'mdi-chevron-down'"
                  @click="props.expand = !props.expand"
                />
              </q-td>
            </q-tr>
            <q-tr
              v-show="props.expand"
              :props="props"
              v-for="(item, indexx) in getDetalle(props.row.acuerdo)"
              :key="indexx"
            >
              <q-td>
                <q-item>
                  <q-item-section side>
                    <q-btn
                      flat
                      round
                      :icon="getIcon(item.tipo)"
                      color="secondary"
                    >
                      <q-tooltip>Ver {{ item.tipo }}</q-tooltip>
                    </q-btn>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>
                      {{ item.nombre }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ item.tipo }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label v-if="item.tipo === 'Promoción'">
                      {{ date.formatDate(item.fecha, "DD/MM/YYYY") }}
                    </q-item-label>
                    <q-item-label v-else>
                      {{ item.notificacion }}
                    </q-item-label>
                    <q-item-label caption v-if="item.tipo === 'Promoción'">
                      Fecha presentación
                    </q-item-label>
                    <q-item-label caption v-else> Notificados </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item v-if="item.tipo === 'Promoción'">
                  <q-item-section>
                    <q-item-label>
                      {{ item.origen }}
                    </q-item-label>
                    <q-item-label caption> Origen </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item v-if="item.tipo === 'Promoción'">
                  <q-item-section>
                    <q-item-label>
                      {{ item.contenido }}
                    </q-item-label>
                    <q-item-label caption> Contenido </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item v-if="item.tipo === 'Promoción'">
                  <q-item-section>
                    <q-item-label>
                      {{ item.promovente }}
                    </q-item-label>
                    <q-item-label caption> Promovente </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-btn flat round size="sm" color="secondary" icon="mdi-eye" />
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </q-tab-panel>
      <q-tab-panel name="incidental">
        <q-table
          flat
          bordered
          hide-header
          dense
          wrap-cells
          virtual-scroll
          width="auto"
          :rows="dataAcuerdosPromociones"
          row-key="acuerdo"
          loading-label="Cargando..."
          no-data-label="No se encontraron registros"
          no-results-label="No se
    encontraron registros en este momento"
          rows-per-page-label="Registros por
    página:"
        >
          <template v-slot:body="props">
            <q-tr :props="props" :class="`bg-${getColor(props.row.estado)}`">
              <q-td>
                <q-item>
                  <q-item-section side>
                    <q-btn
                      flat
                      round
                      :icon="getIcon(props.row.tipo)"
                      color="secondary"
                    >
                      <q-tooltip>Ver {{ props.row.tipo }}</q-tooltip>
                    </q-btn>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.nombre }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ props.row.tipo }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ date.formatDate(props.row.fecha, "DD/MM/YYYY") }}
                    </q-item-label>
                    <q-item-label caption> Fecha elaboración </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.numeroPromociones }}
                    </q-item-label>
                    <q-item-label caption> Promociones </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.estado }}
                    </q-item-label>
                    <q-item-label caption> Estado </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td></q-td>
              <q-td>
                <q-btn flat round size="sm" color="secondary" icon="mdi-eye" />
              </q-td>
              <q-td>
                <q-btn
                  flat
                  round
                  color="primary"
                  :icon="props.expand ? 'mdi-chevron-up' : 'mdi-chevron-down'"
                  @click="props.expand = !props.expand"
                />
              </q-td>
            </q-tr>
            <q-tr
              v-show="props.expand"
              :props="props"
              v-for="(item, indexx) in getDetalle(props.row.acuerdo)"
              :key="indexx"
            >
              <q-td>
                <q-item>
                  <q-item-section side>
                    <q-btn
                      flat
                      round
                      :icon="getIcon(item.tipo)"
                      color="secondary"
                    >
                      <q-tooltip>Ver {{ item.tipo }}</q-tooltip>
                    </q-btn>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>
                      {{ item.nombre }}
                    </q-item-label>
                    <q-item-label caption>
                      {{ item.tipo }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item>
                  <q-item-section>
                    <q-item-label v-if="item.tipo === 'Promoción'">
                      {{ date.formatDate(item.fecha, "DD/MM/YYYY") }}
                    </q-item-label>
                    <q-item-label v-else>
                      {{ item.notificacion }}
                    </q-item-label>
                    <q-item-label caption v-if="item.tipo === 'Promoción'">
                      Fecha presentación
                    </q-item-label>
                    <q-item-label caption v-else> Notificados </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item v-if="item.tipo === 'Promoción'">
                  <q-item-section>
                    <q-item-label>
                      {{ item.origen }}
                    </q-item-label>
                    <q-item-label caption> Origen </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item v-if="item.tipo === 'Promoción'">
                  <q-item-section>
                    <q-item-label>
                      {{ item.contenido }}
                    </q-item-label>
                    <q-item-label caption> Contenido </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item v-if="item.tipo === 'Promoción'">
                  <q-item-section>
                    <q-item-label>
                      {{ item.promovente }}
                    </q-item-label>
                    <q-item-label caption> Promovente </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-btn flat round size="sm" color="secondary" icon="mdi-eye" />
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>

  <q-dialog v-model="showDialogPdf" full-height full-width>
    <ViewPdfComponent :nombreArchivo="documentoPDF" />
  </q-dialog>
</template>

<script setup>
import { ref } from "vue";
import { date } from "quasar";
import {
  dataAcuerdosPromociones,
  detalleAcuerdosPromociones,
} from "../data/expedientePage";
import ViewPdfComponent from "src/components/ViewPdfComponent.vue";
import documentoPDF from "src/assets/PromocionPrueba.pdf";

const tab = ref("principal");
const showDialogPdf = ref(false);

const tipoIcon = [
  { icon: "description", tipo: "Acuerdo" },
  { icon: "mdi-paperclip", tipo: "Promoción" },
  { icon: "assignment", tipo: "Notificación" },
];

const coloresList = [
  { color: "green-1", status: "Notificado" },
  { color: "red-1", status: "Pendiente" },
];

const initialPagination = {
  sortBy: "desc",
  descending: false,
  rowsPerPage: 0,
};

const getIcon = (tipo) => tipoIcon.find((i) => i.tipo === tipo).icon;
const getColor = (e) => coloresList.find((i) => i.status === e).color;
const getDetalle = (id) =>
  detalleAcuerdosPromociones.filter((i) => i.acuerdo === id);
</script>
