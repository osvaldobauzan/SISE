<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title> Lista de expedientes </q-toolbar-title>
    </q-toolbar>
    <q-list
      v-for="ta in listaSinRepetidos"
      :key="ta.TipoAsuntoId"
      bordered
      separator
    >
      <q-item-label header>{{ ta.TipoAsunto }}</q-item-label>
      <div class="row">
        <q-item v-for="item in getLista(ta.TipoAsuntoId)" :key="item.id">
          <div class="col-3">
            <q-item
              @click="item.link ? $router.push(item.link) : null"
              class="q-mb-lg"
            >
              <q-item-section>
                <q-item-label class="text-h6 text-bold">
                  {{ item.Expediente }}</q-item-label
                >
                <q-item-label caption>{{ item.Cuaderno }}</q-item-label>
                <q-item-label caption>
                  <q-chip dense square outline icon="mdi-calendar-blank">
                    {{ date.formatDate(item.FechaAuto, "DD/MM/YYYY") }}</q-chip
                  >
                </q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div class="col">
            <q-item>
              <q-item-section>
                <q-item-label>
                  <q-chip dense square outline icon="mdi-account"
                    >Quejoso: {{ item.Quejoso }}</q-chip
                  >
                  <q-chip dense square outline
                    >Autoridad: {{ item.Autoridad }}</q-chip
                  >
                </q-item-label>
                <q-item-label>
                  {{ item.Sintesis }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </div>
        </q-item>
      </div>
    </q-list>
    <q-expansion-item
      icon="mdi-format-list-checkbox"
      :label="colaps.TipoAsunto"
      class="shadow-1 overflow-hidden q-mb-md"
      style="border-radius: 10px"
      header-class="bg-indigo-2"
      v-for="colaps in arregloConListas"
      :key="colaps.TipoAsuntoId"
    >
      <q-card>
        <q-table
          flat
          dense
          :rows="colaps.lista"
          :columns="columns"
          row-key="index"
          virtual-scroll
        >
          <template v-slot:body="props">
            <q-tr :props="props">
              <q-td
                v-for="col in props.cols"
                :key="col.name"
                :props="props"
                style="cursor: pointer"
              >
                <div v-if="col.name === 'expediente'">
                  <q-item>
                    <q-item-section>
                      <q-item-label
                        class="text-bold"
                        @click="navigateToExpediente(props.row)"
                        >{{ props.row.Expediente }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </div>
                <div
                  v-if="col.name === 'Sintesis' || col.name === 'Autoridad'"
                  :style="{
                    maxWidth: '14rem',
                    minWidth: '10rem',
                    color: '#000000',
                    textWrap: 'balance',
                    maxHeight: 'auto',
                  }"
                >
                  {{ col.value }}
                </div>
                <div
                  v-if="
                    col.name !== 'expediente' &&
                    col.name !== 'Sintesis' &&
                    col.name !== 'Autoridad'
                  "
                >
                  {{ col.value }}
                </div>
              </q-td>
            </q-tr>
          </template>
        </q-table>
      </q-card>
    </q-expansion-item>
  </q-card>
</template>

<script setup>
import { resp } from "../data/listaSentencia";
import { date } from "quasar";
import { useRouter } from "vue-router";

const router = new useRouter();

const nuevaLista = resp.map(({ TipoAsuntoId, TipoAsunto }) => ({
  TipoAsuntoId,
  TipoAsunto,
}));
const listaSinRepetidos = [
  ...new Set(nuevaLista.map((item) => JSON.stringify(item))),
].map((item) => JSON.parse(item));

const arregloConListas = listaSinRepetidos.map((asunto) => {
  return {
    ...asunto,
    lista: resp.filter(
      ({ TipoAsuntoId }) => TipoAsuntoId === asunto.TipoAsuntoId,
    ),
  };
});

const getLista = (id) => {
  return resp.filter((item) => item.TipoAsuntoId === id);
};
const navigateToExpediente = (row) => {
  localStorage.setItem("expedienteSelect", JSON.stringify(row));
  router.push({ path: `/expediente` });
};

const recortarTexto = (texto, longitudMaxima) => {
  return texto.length > longitudMaxima
    ? `${texto.slice(0, longitudMaxima)} ...`
    : `${texto}.`;
};

const columns = [
  {
    name: "expediente",
    required: true,
    label: "Expediente",
    align: "left",
    field: "Expediente",
    // field: (row) => `${row.Expediente.AsuntoAlias} (${row.Expediente.CatTipoAsunto})`,
    sortable: true,
  },
  {
    name: "Tipo",
    label: "Tipo",
    align: "left",
    field: "Cuaderno",
    sortable: true,
  },
  {
    name: "Quejoso",
    align: "left",
    label: "Nombre del Quejoso o Recurrente",
    field: "Quejoso",
  },
  {
    name: "Autoridad",
    align: "left",
    label: "Autoridad(es)",
    field: "Autoridad",
  },
  {
    name: "Sintesis",
    align: "left",
    label: "Síntesis",
    // field: "Sintesis",
    field: (row) => recortarTexto(row.Sintesis, 200),
  },
  {
    name: "FechaAuto",
    label: "Fecha de Acuerdo",
    align: "left",
    field: (row) => date.formatDate(row.FechaAuto, "DD/MM/YYYY"),
  },
];
</script>
