<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title class="text-bold">{{ title }}</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section>
      <div class="row">
        <q-item>
          <q-item-section>
            <q-item-label caption>Acuerdo</q-item-label>
            <q-item-label> {{ item.Contenido }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label caption> Creación del acuerdo</q-item-label>
            <q-item-label>{{
              date.formatDate(item.FechaAlta, "DD/MM/YYYY")
            }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label caption>
              {{ item.CatTipoAsunto }}
            </q-item-label>
            <q-item-label>
              {{ item.AsuntoAlias }}
            </q-item-label>
          </q-item-section>
        </q-item>
      </div>

      <!-- <q-item-label header>Síntesis</q-item-label> -->
      <q-editor v-model="sintesis" />
    </q-card-section>
    <q-card-actions>
      <q-btn label="Crear síntesis" color="primary" style="width: 200px" />
      <q-btn
        outline
        label="Cancelar"
        color="primary"
        style="width: 200px"
        v-close-popup
      />
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { date } from "quasar";

const sintesis = ref("");
//const getBook = (ta) => catTipoAsunto.find((t) => t.shortName === ta).book;

// eslint-disable-next-line no-unused-vars
const props = defineProps({
  item: {
    type: Object,
    required: true,
  },
  title: {
    type: String,
    default: "Agregar de forma manual a lista de acuerdos",
  },
});

onMounted(() => {
  sintesis.value = props.item.Sintesis || "";
});
</script>
