<template>
  <q-card>
    <q-splitter
      v-model="splitterModel"
      :limits="[50, 100]"
      style="width: 100%; height: 100%"
    >
      <template v-slot:before>
        <VerAcuerdo :model-value="item" :esDialogo="false"> </VerAcuerdo>
      </template>

      <template v-slot:after>
        <q-toolbar>
          <q-toolbar-title class="text-bold"
            >Información del acuerdo</q-toolbar-title
          >
          <q-btn flat round dense icon="mdi-close" v-close-popup />
        </q-toolbar>
        <q-separator></q-separator>
        <q-card-section>
          <q-item-label header class="text-bold q-pb-none"
            >Datos del expediente</q-item-label
          >
          <div class="row">
            <q-item>
              <q-item-section>
                <q-item-label caption>Expediente</q-item-label>
                <q-item-label class="text-bold">
                  {{ item.expediente.asuntoAlias }}</q-item-label
                >
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-item-label caption> Tipo de asunto </q-item-label>
                <q-item-label class="text-bold">
                  {{ item.expediente.catTipoAsunto }}
                </q-item-label>
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-item-label caption> Cuaderno </q-item-label>
                <q-item-label class="text-bold">
                  {{ metaDatos.datosAsunto?.tipoCuaderno }}
                </q-item-label>
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-item-label caption> Fecha del acuerdo </q-item-label>
                <q-item-label class="text-bold">
                  {{ date.formatDate(item.fechaAlta, "DD/MM/YYYY") }}
                  <!-- Verificar -->
                </q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <q-item-label header class="text-bold q-pb-none"
            >Promociones</q-item-label
          >
          <q-item-label class="q-pl-md"
            >Promociones que se le da respuesta en este cuaderno</q-item-label
          >
          <q-list dense>
            <q-item v-for="(item, index) in promociones" :key="index">
              <q-item-section side>
                <q-icon name="mdi-file"></q-icon>
              </q-item-section>
              <q-item-section class="text-bold">{{
                item.promocion
              }}</q-item-section>
            </q-item>
          </q-list>
          <q-item-label header class="q-pb-none text-bold"
            >Contenido del acuerdo</q-item-label
          >
          <q-item-label class="q-pl-md text-bold">
            {{ datosAcuerdo[0]?.contenidoAcuerdo }}
          </q-item-label>
          <q-item-label header class="text-bold q-pb-none"
            >Ingreso a actuaría</q-item-label
          >
          <q-item-label class="q-pl-md">
            Fecha en la que actuaría recibió el acuerdo.
          </q-item-label>
          <q-item class="q-py-none">
            <q-item-section side>
              <q-icon name="mdi-calendar"></q-icon>
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-bold">{{
                date.formatDate(item.fechaAutoriza, "DD/MM/YYYY")
              }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item-label header class="text-bold q-pb-none"
            >Días transcurridos</q-item-label
          >
          <q-item-label class="q-pl-md">
            Tiempo que ha pasado sin haber notificado a todas sus partes.
          </q-item-label>
          <q-item class="q-py-none">
            <q-item-section side>
              <q-icon name="mdi-calendar-alert"></q-icon>
            </q-item-section>
            <q-item-section>
              <q-item-label class="text-bold"
                >{{ item.transcurrido }} días</q-item-label
              >
            </q-item-section>
          </q-item>
          <q-item-label header class="text-bold q-pb-none"
            >Síntesis</q-item-label
          >
          <q-item-label class="q-px-md text-justify">
            {{ item.sintesis }}
          </q-item-label>
        </q-card-section>
      </template>
    </q-splitter>
  </q-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { date } from "quasar";
import { useActuariaDetalleNotificacionesStore } from "../stores/actuaria-detalle-notificaciones-store.js";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
import { manejoErrores } from "src/helpers/manejo-errores";

const sintesis = ref("");
const actuariaDetalleNotificacionesStore =
  useActuariaDetalleNotificacionesStore();
const promociones = ref([]);
const datosAcuerdo = ref([]);

const splitterModel = ref(50);
// eslint-disable-next-line no-unused-vars
const props = defineProps({
  item: {
    type: Object,
    required: true,
  },
  metaDatos: {
    type: Object,
    required: true,
  },
});

onMounted(async () => {
  getDetalle();
  sintesis.value = props.item.sintesis || "";
});

async function getDetalle() {
  try {
    const params = {
      asuntoNeunId: props.item.expediente.asuntoNeunId,
      sintesisOrden: props.item.sintesisOrden,
      asuntoDocumentoId: props.item.asuntoDocumentoId,
    };
    await actuariaDetalleNotificacionesStore.getDetalleAcuerdo(params);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  promociones.value =
    actuariaDetalleNotificacionesStore.detalleAcuerdo.promociones;
  datosAcuerdo.value = actuariaDetalleNotificacionesStore.detalleAcuerdo.datos;
}
</script>
