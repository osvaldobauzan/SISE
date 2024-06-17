<template>
  <q-card style="min-width: 60vw" class="q-pa-md">
    <q-toolbar class="q-gutter-xs q-pb-lg">
      <q-item-section>
        <div class="row items-center">
          <q-toolbar-title class="q-pb-md text-bold">{{
            privilegio.titulo
          }}</q-toolbar-title>
          <q-btn flat round dense icon="mdi-close" @click="cancelar" />
        </div>

        <q-separator></q-separator>
        <q-item-label class="q-py-md">
          {{ privilegio.subTitulo }}
        </q-item-label>
      </q-item-section>
    </q-toolbar>
    <q-toolbar class="q-gutter-xs q-pl-md">
      <q-checkbox v-model="firmarOficios" v-if="privilegio.estado == 2"
        >¿Firmar oficios?</q-checkbox
      >
      <q-space></q-space>
      <q-input
        dense
        rounded
        outlined
        bg-color="white"
        v-model="textoBuscar"
        placeholder="Buscar"
      >
        <template v-slot:append>
          <q-icon
            v-if="textoBuscar?.trim()"
            class="cursor-pointer"
            name="mdi-close"
            @click="textoBuscar = ''"
          />
          <q-icon name="mdi-magnify" />
        </template>
      </q-input>
    </q-toolbar>

    <q-inner-loading :showing="isLoading" color="primary" />
    <q-table
      v-if="!isLoading"
      flat
      style="max-height: 50vh"
      :filter="textoBuscar"
      ref="tableRef"
      :rows="rows"
      :columns="columns"
      row-key="index"
      selection="multiple"
      v-model:selected="selected"
      @update:selected="emit('seleccionados', selected)"
    >
      <template v-slot:header-cell-oficiosFirmados="props">
        <q-th v-if="privilegio.estado != 2">{{ props.col.label }} </q-th>
      </template>
      <template v-slot:header-selection="scope">
        <q-checkbox v-model="scope.selected" />
      </template>

      <template v-slot:body="props">
        <q-tr :class="getColor(props.row.estado)">
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
          <q-td style="max-width: 15em">
            <q-item>
              <q-item-section>
                <q-item-label class="text-bold">
                  {{ props.row.expediente.asuntoAlias }}
                </q-item-label>
                <q-item-label caption>
                  {{ props.row.expediente.catTipoAsunto }}
                </q-item-label>
                <q-item-label caption>
                  {{ props.row.expediente.tipoProcedimiento }}
                </q-item-label>
                <q-item-label
                  v-if="
                    props.row.nombreTipoCuaderno &&
                    props.row.nombreTipoCuaderno.trim().toLowerCase() !=
                      props.row.expediente.catTipoAsunto?.trim().toLowerCase()
                  "
                >
                  <q-badge
                    :class="`bg-${props.row.nombreCorto} text-${getTextColor(
                      props.row.nombreCorto,
                    )}`"
                    :label="props.row.nombreTipoCuaderno"
                  >
                  </q-badge>
                </q-item-label>
              </q-item-section>
            </q-item>
          </q-td>
          <q-td
            style="border-left: rgb(190, 190, 190) solid 1px"
            class="text-center"
          >
            <q-btn
              flat
              stack
              color="blue"
              icon="mdi-paperclip"
              :label="props.row.fechaAuto_F"
              @click="
                selectedItem = props.row;
                showAcuerdo = true;
              "
            >
              <q-tooltip>Acuerdo</q-tooltip>
            </q-btn>
          </q-td>
          <q-td class="text-center"
            >{{ props.row.userNameOficial || "" }}<br />
            <q-item-label class="text-secondary">{{
              date.formatDate(props.row.fechaAuto, "DD/MM/YYYY")
            }}</q-item-label>
            <q-item-label class="text-secondary">{{
              date.formatDate(props.row.fechaAuto, "HH:mm")
            }}</q-item-label>
          </q-td>
          <q-td>
            <div class="text-center">
              {{ props.row.empleadoPreAutoriza }}<br />
              <q-item-label class="text-secondary">{{
                date.formatDate(props.row.fechaPreAutoriza, "DD/MM/YYYY")
              }}</q-item-label>
              <q-item-label class="text-secondary">{{
                date.formatDate(props.row.fechaPreAutoriza, "HH:mm")
              }}</q-item-label>
            </div>
          </q-td>
          <q-td v-if="privilegio.estado !== 2">
            <div class="text-center inline text-white">
              <q-item-label>
                <q-badge
                  v-if="!!props.row.oficiosFirmados"
                  class="bg-green-5 q-pa-sm"
                >
                  SI
                </q-badge>
                <q-badge v-else class="bg-red-5 q-pa-sm">
                  Los oficios también serán firmados
                </q-badge>
              </q-item-label>
            </div>
          </q-td>
        </q-tr>
      </template>
    </q-table>
    <q-card-actions align="left">
      <q-btn
        no-caps
        label="Continuar"
        :color="selected.length > 0 ? 'secondary' : 'grey-6'"
        style="min-width: 164px"
        type="submit"
        :disable="!!selected.length < 1"
        @click="getDetalles"
      />
      <q-btn
        no-caps
        @click="cancelar"
        outline
        label="Cancelar"
        :color="'secondary'"
        style="min-width: 164px"
      />
    </q-card-actions>
  </q-card>
  <q-dialog v-model="showAcuerdo" full-height full-width>
    <VerAcuerdo :model-value="selectedItem"></VerAcuerdo>
  </q-dialog>
  <DialogConfirmacion
    v-model="showCancelar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="Se perderán los cambios."
    :subTitulo="`Si continúa se perderán los cambios que ha realizado. ¿Desea continuar?`"
    @aceptar="emit('cerrar', true)"
  ></DialogConfirmacion>
</template>

<script setup>
import { date } from "quasar";
import { onMounted, ref, reactive } from "vue";
import { useTramiteStore } from "../store/tramite-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import VerAcuerdo from "src/modules/tramite/components/VerAcuerdo.vue";
import { Firmador } from "src/helpers/firmadorInicio";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";

const showAcuerdo = ref(false);
const firmarOficios = ref(false);
const showCancelar = ref(false);

const selectedItem = ref(null);
const tramiteStore = useTramiteStore();
const props = defineProps({
  // v-model
  modelValue: {
    default: "",
  },
  selectedDate: {
    default: "",
  },
  estado: {
    default: 0,
  },
});
const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  seleccionados: (value) => value !== null,
  cerrar: (value) => value,
});
const textoBuscar = ref("");
const selected = ref([]);
const tableRef = ref(null);
const isLoading = ref(false);
const authStore = useAuthStore();
const getColor = (e) => coloresList.find((i) => i.status === +e)?.color;

const privilegio = reactive({
  privilegio: 15,
  estado: 3,
  titulo: "Autorizar acuerdos",
  subTitulo:
    "Selecciona de los acuerdos ya preautorizados aquellos que deseas autorizar.",
});

const estadoPrivilegio = ref([
  {
    privilegio: 14,
    estado: 2,
    estadoCat: 1,
    titulo: "Preautorizar acuerdos",
    subTitulo: "Selecciona de los acuerdos que deseas preautorizar.",
  },
  {
    privilegio: 15,
    estado: 3,
    estadoCat: 2,
    titulo: "Autorizar acuerdos",
    subTitulo:
      "Selecciona de los acuerdos ya preautorizados aquellos que deseas autorizar.",
  },
]);
const coloresList = [
  {
    color: "bg-purple-2",
    status: 2,
    label: "Con Acuerdo",
  },
  {
    color: "bg-blue-2",
    status: 3,
    label: "Preautorizados",
  },
];

const columns = [
  {
    name: "expediente",
    required: true,
    label: "Expediente",
    align: "left",
    field: (row) =>
      `${row.expediente.asuntoAlias} ${row.expediente.catTipoAsunto} ${
        row.nombreTipoCuaderno || ""
      } ${row.expediente.tipoProcedimiento || ""} `,
    sortable: true,
  },
  {
    name: "acuerdo",
    align: "center",
    label: "Acuerdo",
    field: (row) => row.fechaAuto_F,
  },
  {
    name: "Capturo",
    align: "center",
    label: "Capturó",
    field: (row) =>
      `${row.userNameOficial} ${date.formatDate(
        row.fechaAuto,
        "DD/MM/YYYY",
      )} ${date.formatDate(row.fechaAuto, "HH:mm")}`,
    sortable: true,
  },
  {
    name: "Preautorizo",
    align: "center",
    label: "Preautorizó",
    field: (row) =>
      `${row.empleadoPreAutoriza} ${date.formatDate(
        row.fechaPreAutoriza,
        "DD/MM/YYYY",
      )} ${date.formatDate(row.fechaPreAutoriza, "HH:mm")}`,
    sortable: true,
  },
  {
    name: "oficiosFirmados",
    align: "center",
    label: "Oficios Firmados",
    field: (row) => row.oficiosFirmados,
  },
];

const rows = ref([]);
const pagination = ref({
  sortBy: "promocion",
  descending: true,
  page: 1,
  rowsPerPage: 0,
});
onMounted(async () => {
  await setRows();
});

async function setRows() {
  isLoading.value = true;
  try {
    Object.assign(
      privilegio,
      estadoPrivilegio.value.find((obj) =>
        authStore.user.privilegios.includes(obj.privilegio),
      ),
    );
    if (
      authStore.user.privilegios.includes(14) &&
      authStore.user.privilegios.includes(15)
    ) {
      if (props.estado == 2) {
        Object.assign(
          privilegio,
          estadoPrivilegio.value.find((obj) => obj.estado == 2),
        );
      }
      if (props.estado == 3) {
        Object.assign(
          privilegio,
          estadoPrivilegio.value.find((obj) => obj.estado == 3),
        );
      }
    }

    await tramiteStore.obtenerTramites({
      ...props.selectedDate,
      status: privilegio.estado,
      text: textoBuscar.value,
      ...pagination.value,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  rows.value =
    tramiteStore.dataTramites.datos?.map((x, i) => {
      x.index = i;
      return x;
    }) || [];
  isLoading.value = false;
}

async function getDetalles() {
  let result = [];
  try {
    const params = [];
    selected.value.forEach((a) =>
      params.push({
        anioPromocion: a.yearPromocion,
        numeroOrden: a.numeroOrden,
        tipoModulo: 2,
        asuntoNeunId: a.expediente.asuntoNeunId,
        asuntoDocumentoId: a.asuntoDocumentoId,
      }),
    );
    localStorage.setItem("acuerdoFirmar", JSON.stringify(selected.value));
    localStorage.setItem("cambioEstadoAcuerdo", privilegio.estadoCat);
    result = await tramiteStore.obtnerDetallesAcuerdos(params);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  result = result
    .filter((x) => x.status === 200)
    .map((x) => {
      return {
        nombre: x.data.anexos[0]?.nombre,
        id: x.data.anexos[0]?.guidDocumento,
        tipoArchivo: "acuerdo",
        modulo: 2,
      };
    });
  const documentosAFirmar = {
    documentos: result,
    firmarOficios: firmarOficios.value,
    accion: privilegio.estadoCat,
  };
  await Firmador.obtenerURLGrafico(documentosAFirmar);
}
const getTextColor = (ta) =>
  ["jom", "cm", "cc", "pca"].find((t) => t === ta) ? "black" : "white";

function cancelar() {
  if (!!selected.value.length > 0) {
    showCancelar.value = true;
  } else {
    emit("cerrar", true);
  }
}
</script>

<style scoped></style>
