<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title> Agregar Proyecto a la lista </q-toolbar-title>
      <q-space></q-space>
      <q-btn flat round icon="mdi-close" v-close-popup></q-btn>
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section>
      <div class="row">
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption> Listado </q-item-label>
              <q-item-label class="text-bold">08/03/2024</q-item-label>
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption> Sesión </q-item-label>
              <q-item-label class="text-bold">14/03/2024</q-item-label>
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-item>
            <q-item-section>
              <q-item-label caption> Ponencia 1 </q-item-label>
              <q-item-label class="text-bold"
                >José Luis Vargas Valdez</q-item-label
              >
            </q-item-section>
          </q-item>
        </div>
      </div>
    </q-card-section>
    <q-card-section>
      <q-table
        flat
        dense
        bordered
        :columns="columns"
        :rows="rows"
        row-key="expediente"
        selection="multiple"
        v-model:selected="selected"
      >
        <template v-slot:body="props">
          <q-tr :props="props">
            <q-td>
              <q-checkbox
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
              <q-item class="q-pl-none">
                <q-item-section>
                  <q-item-label>{{ props.row.AsuntoAlias }}</q-item-label>
                  <q-item-label caption>{{
                    props.row.catTipoAsunto
                  }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td>
              <q-btn
                flat
                dense
                stack
                icon="mdi-paperclip"
                :label="date.formatDate(props.row.FechaAudiencia, 'DD/MM/YYYY')"
              >
              </q-btn>
            </q-td>
            <q-td>
              <q-item class="q-pl-none">
                <q-item-section>
                  <q-item-label>{{ props.row.Secretario }}</q-item-label>
                  <q-item-label caption>Mesa {{ props.row.Mesa }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td>
              <q-btn
                flat
                dense
                stack
                icon="mdi-paperclip"
                :label="
                  date.formatDate(props.row.ArchivoProyecto, 'DD/MM/YYYY')
                "
              >
              </q-btn>
            </q-td>
            <q-td>
              <q-item>
                <q-item-section>
                  <q-item-label>{{ props.row.Sentido }}</q-item-label>
                  <q-item-label caption>{{
                    props.row.TipoSentencia
                  }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-td>
            <q-td></q-td>
          </q-tr>
        </template>
      </q-table>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions>
      <q-space></q-space>
      <q-btn dense no-caps unelevated color="primary" class="q-px-lg">
        Agregar
      </q-btn>
      <q-btn dense no-caps outline color="primary" class="q-px-lg">
        Cancelar
      </q-btn>
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { date } from "quasar";
import { lista } from "../data/listaComponent";

const selected = ref([]);

const rows = ref([]);
rows.value = lista;

const columns = [
  {
    name: "expediente",
    label: "Expediente",
    align: "left",
    field: "expediente",
  },
  {
    name: "audiencia",
    label: "Audiencia / Turno",
    align: "left",
    field: "audiencia",
  },
  {
    name: "secretario",
    label: "Secretario",
    align: "left",
    field: "secretario",
  },
  {
    name: "proyecto",
    label: "Proyecto",
    align: "left",
    field: "proyecto",
  },
  {
    name: "sentido",
    label: "Sentido",
    align: "left",
    field: "sentido",
  },
  {
    name: "listado",
    label: "Listado",
    align: "left",
    field: "listado",
  },
];
</script>
