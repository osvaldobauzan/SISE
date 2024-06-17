<template>
  <q-card class="bg-blue-grey-1">
    <q-toolbar class="bg-white">
      <q-toolbar-title class="text-bold"> Lista de acuerdos </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-separator />
    <q-toolbar class="q-gutter-xs q-my-sm">
      <div class="q-gutter-sm">
        <q-btn
          v-permitido="30"
          flat
          no-caps
          color="secondary"
          icon="mdi-calendar"
          label="Generar lista"
          @click="showPublicarLista = true"
        />
      </div>
      <q-space />
      <q-input
        v-permitido="29"
        dense
        rounded
        outlined
        widht="200px"
        bg-color="white"
        placeholder="Buscar"
        :model-value="searchText"
        @change="updateValue"
      >
        <template v-slot:append>
          <q-icon name="mdi-magnify" />
        </template>
      </q-input>
    </q-toolbar>
    <q-card-section>
      <div class="row q-gutter-sm">
        <div class="col-2">
          <q-card>
            <q-toolbar>
              <q-item-label class="text-bold">Publicaciones</q-item-label>
            </q-toolbar>
            <q-separator />
            <q-scroll-area style="height: 650px; max-width: 300px">
              <q-list separator>
                <q-item
                  :clickable="!item.isDisabled"
                  v-for="item in fechaPublicacionLista"
                  :key="item"
                  @click="
                    generateRecreateListBySelectedDate(item.fechaPublicacion)
                  "
                >
                  <q-tooltip
                    v-if="item.isDisabled"
                    class="bg-red text-body2"
                    anchor="center right"
                    self="center left"
                    :offset="[10, 10]"
                  >
                    <strong>Fecha Inhabil</strong>
                  </q-tooltip>
                  <q-item-section side>
                    <q-icon name="mdi-calendar"></q-icon>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>{{ item.fechaPublicacion }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-scroll-area>
          </q-card>
        </div>
        <div class="col">
          <q-card>
            <q-toolbar>
              <q-toolbar-title>
                Fecha de publicación
                <span class="text-bold">
                  {{ fechaPublicacion }}
                </span>
              </q-toolbar-title>
              <q-btn
                v-print="'#printMe'"
                v-permitido="28"
                flat
                no-caps
                color="secondary"
                icon="mdi-printer"
                label="Imprimir"
              />
            </q-toolbar>
            <q-separator />
            <q-scroll-area style="height: 650px; max-width: 100%">
              <q-list id="printMe">
                <q-toolbar class="print-only">
                  <q-toolbar-title class="text-center">
                    <p style="margin: 0 auto; padding: 5px; text-wrap: wrap">
                      {{ auth.user.nombreOficial }}
                    </p>
                    <p v-if="fechaPublicacion">
                      Lista de acuerdos publicada el día <br />
                      <span class="text-bold">
                        {{
                          customDates
                            .replaceDateFormat(
                              fechaPublicacion,
                              "DD-MM-YYYY",
                              "DD-MMMM-YYYY",
                            )
                            .replaceAll("-", " de ")
                        }}
                      </span>
                    </p>
                  </q-toolbar-title>
                  <q-separator />
                </q-toolbar>
                <div v-for="({ data }, idx) in recreateList" :key="idx">
                  <q-expansion-item
                    v-for="{ tipoAsunto, acuerdos } in data"
                    :key="tipoAsunto"
                    :label="tipoAsunto"
                    default-opened
                    header-class="text-h5"
                  >
                    <div
                      v-for="(acuerdo, index) in acuerdos"
                      :key="acuerdo.AsuntoNeunId"
                      class="row flex"
                    >
                      <AcuerdoHolder :ta="acuerdo" :index="index" />
                      <div class="col-12 text-center">
                        <p class="print-only">
                          {{
                            acuerdos.length === ++index
                              ? acuerdos[0].actuario
                              : ""
                          }}
                        </p>
                      </div>
                    </div>
                    <q-separator />
                  </q-expansion-item>
                  <q-separator />
                </div>
              </q-list>
            </q-scroll-area>
          </q-card>
        </div>
      </div>
    </q-card-section>
  </q-card>
  <q-dialog :full-width="true" v-model="showPublicarLista">
    <PublicarLista />
  </q-dialog>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useActuariaListaStore } from "../stores/actuaria-lista_acuerdos-store";
import PublicarLista from "./PublicarLista.vue";
import { useCatalogosStore } from "src/stores/catalogos-store";
import AcuerdoHolder from "./AcuerdoHolder.vue";
import { customDates } from "src/helpers/dates";
import { date } from "quasar";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
const actuariaListaStore = useActuariaListaStore();
const catalogoStore = useCatalogosStore();
const auth = useAuthStore();
const showPublicarLista = ref(false);
const fechaPublicacion = ref();
const rows = ref([]);
const searchText = ref("");
const tiposAsuntoLista = ref([]);
const fechaPublicacionLista = ref([]);

const recreateList = ref([]);

const generateRecreateListBySelectedDate = (fecha) => {
  recreateList.value = fechaPublicacionLista.value.filter(
    (publicacion) => publicacion.fechaPublicacion == fecha,
  );
  recreateList.value = recreateList.value.map((publicacion) => {
    return {
      ...publicacion,
      data: publicacion.data.filter(({ acuerdos }) => acuerdos.length > 0),
    };
  });
  fechaPublicacion.value = customDates.replaceDateFormat(
    fecha,
    "DD-MM-YYYY",
    "DD-MM-YYYY",
  );
};

const props = defineProps({
  dateRange: {
    from: {
      type: String,
      require: true,
    },
    to: {
      type: String,
      require: true,
    },
  },
});

onMounted(async () => {
  const vstart = props.dateRange.from.split("/");
  const vend = props.dateRange.to.split("/");
  const from = new Date(vstart[1] + "/" + vstart[0] + "/" + vstart[2]);
  const to = new Date(vend[1] + "/" + vend[0] + "/" + vend[2]);

  await actuariaListaStore.getListaAcuerdos(
    date.formatDate(from, "YYYY-MM-DD"),
    date.formatDate(to, "YYYY-MM-DD"),
  );
  rows.value = actuariaListaStore.listaAcuerdos;
  if (rows.value) {
    tiposAsuntoLista.value = [
      ...new Set(rows.value.map((item) => item.tipoAsunto)),
    ]; // [ 'A', 'B']
    fechaPublicacion.value = props.dateRange.from;
  }
  fechaPublicacionLista.value = await getDaysArray(
    props.dateRange.from,
    props.dateRange.to,
  );
  fechaPublicacionLista.value.reverse();
});

const getDaysArray = async (start, end) => {
  const arr = [];
  const vstart = start.split("/");
  const vend = end.split("/");

  const amStartDate = new Date(vstart[1] + "/" + vstart[0] + "/" + vstart[2]);
  const amEndDate = new Date(vend[1] + "/" + vend[0] + "/" + vend[2]);

  await catalogoStore
    .getDiasInhabiles(
      date.formatDate(amStartDate, "YYYY-MM-DD"),
      date.formatDate(amEndDate, "YYYY-MM-DD"),
    )
    .then((diasInhabiles) => {
      for (
        const dt = amStartDate;
        dt <= amEndDate;
        dt.setDate(dt.getDate() + 1)
      ) {
        if (
          !dt.toDateString().includes("Sat") &&
          !dt.toDateString().includes("Sun")
        ) {
          arr.push({
            fechaPublicacion: date.formatDate(dt, "DD-MM-YYYY"),
            isDisabled: diasInhabiles
              ? diasInhabiles.includes(date.formatDate(dt, "YYYY-MM-DD"))
              : false,
            data:
              tiposAsuntoLista.value.map((tipoAsunto) => {
                return {
                  acuerdos: getListaAcuerdosByDay(
                    tipoAsunto,
                    date.formatDate(dt, "YYYY-MM-DD"),
                  ),
                  tipoAsunto,
                };
              }) ?? null,
          });
        }
      }
    });
  return arr;
};

function getListaAcuerdosByDay(tipoAsunto, fecha) {
  return rows.value.filter(
    (acuerdo) =>
      date.formatDate(acuerdo.fechaAuto, "YYYY-MM-DD") == fecha &&
      acuerdo.tipoAsunto === tipoAsunto,
  );
}

function updateValue(event) {
  searchText.value = event;
  if (event.length >= 2) {
    const filterList = recreateList.value.filter((element) =>
      element.fechaPublicacion.includes(fechaPublicacion.value),
    );
    recreateList.value = filterList.map((element) => {
      return {
        data: element.data
          .map((section) => {
            return {
              acuerdos: section.acuerdos.filter(
                (h) =>
                  h.expediente.includes(event) ||
                  h.quejoso.toLowerCase().includes(event),
              ),
              tipoAsunto: section.tipoAsunto,
            };
          })
          .filter((el) => el.acuerdos.length > 0),
        fechaPublicacion: element.fechaPublicacion,
        isDisabled: element.isDisabled,
      };
    });
  } else {
    generateRecreateListBySelectedDate(fechaPublicacion.value);
  }
}
</script>

<style scoped>
.print-only {
  display: none;
}

@media print {
  .no-print {
    display: none;
  }

  .print-only {
    text-align: center;
    display: block;
  }
}

table tr:nth-child(even) td {
  border-bottom: thin solid lightgray;
}

table tr:nth-child(odd) td {
  background-color: #f2f2f2;
  border-bottom: thin solid lightgray;
}
</style>
