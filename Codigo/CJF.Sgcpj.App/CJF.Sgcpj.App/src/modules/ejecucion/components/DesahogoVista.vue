<template>
  <q-card>
    <q-splitter
      v-model="splitterModel"
      :limits="[50, 100]"
      style="width: 100%; height: 100%"
    >
      <template v-slot:before>
        <q-pdfviewer
          :type="$q.platform.is.mobile ? 'pdfjs' : 'html5'"
          :src="nombreArchivo"
        />
      </template>

      <template v-slot:after>
        <q-toolbar>
          <q-toolbar-title> Desahogo de vista</q-toolbar-title>
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
                  item?.Expediente.AsuntoAlias
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label caption>Tipo de asunto</q-item-label>
                <q-item-label class="text-bold">{{
                  item?.Expediente.CatTipoAsunto
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label caption>Cuaderno</q-item-label>
                <q-item-label class="text-bold">{{
                  item?.CuadernoNombre
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
        </q-card-section>
        <q-toolbar>
          <q-item-label class="text-bold">
            {{ rows.length }} Partes
          </q-item-label>
        </q-toolbar>
        <q-card-section>
          <q-list separator bordered>
            <q-item class="fit" v-for="(row, index) in rows" :key="index">
              <q-item-section side>
                <q-item-label class="text-bold">
                  {{ index + 1 }}
                  <q-btn
                    flat
                    round
                    class="q-ml-md"
                    icon="mdi-paperclip"
                    @click="nombreArchivo = `./docs/Version${index + 1}.pdf`"
                  >
                  </q-btn>
                </q-item-label>
              </q-item-section>
              <q-item-section>
                <q-item-label class="text-bold">
                  {{ row.Parte }}
                </q-item-label>
                <q-item-label caption>
                  {{ row.Caracter }}
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-chip dense square :label="row.Fecha"></q-chip>
              </q-item-section>
            </q-item>
          </q-list>
        </q-card-section>
      </template>
    </q-splitter>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
// import { date } from "quasar";

const rows = ref([]);
const splitterModel = ref(50);
const nombreArchivo = ref("./docs/Version1.pdf");

const props = defineProps({
  item: {
    type: Object,
    required: true,
  },
});

onMounted(() => {
  rows.value = props.item.DesahogoVista;
});
</script>

<style>
td {
  border-bottom: 1pt solid black;
}
</style>
