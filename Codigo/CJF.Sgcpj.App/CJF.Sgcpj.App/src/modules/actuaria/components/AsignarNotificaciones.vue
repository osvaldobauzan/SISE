<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title class="text-bold">
        Asignar notificaciones
      </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" @click="cancelar" />
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section class="q-gutter-sm">
      <q-item-label>
        Indica el actuario al que se asignarán las notificaciones
      </q-item-label>
      <q-form ref="form" class="q-pa-sm row">
        <q-select
          ref="selectZona"
          class="col-xs-12 col-sm-6 col-md-4"
          dense
          filled
          autofocus
          v-cortarLabel
          @input-value="(actuarioSelected = null), cambioForm(null)"
          v-model="actuarioSelected"
          virtual-scroll-slice-size="100"
          use-input
          input-debounce="0"
          label="Selecciona una zona *"
          @update:model-value="cambioForm"
          :options="optionsActuario"
          @filter="filtrarZona"
          option-value="areaId"
          :rules="[(val) => Validaciones.validaSelectRequerido(val?.areaId)]"
        >
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.nombreEmpleado }}</q-item-label>
                <q-item-label caption>{{ scope.opt.nombre }}</q-item-label>
              </q-item-section>
            </q-item>
          </template>
          <template v-slot:selected-item="scope">
            <q-item-section v-bind="scope.itemProps">
              <q-item-label
                >{{ scope.opt.nombreEmpleado }}
                <span class="text-caption text-grey-6">
                  - {{ scope.opt.nombre }}</span
                >
              </q-item-label>
            </q-item-section>
          </template>
        </q-select>
      </q-form>
      <q-inner-loading :showing="cargando" color="primary" />
      <q-table
        flat
        wrap-cells
        dense
        bordered
        style="height: 60vh"
        :filter="filter"
        :rows="rows"
        :columns="columns"
        row-key="autoridadJudicialId"
        :pagination="initialPagination"
      >
        <template v-slot:top-right>
          <q-input
            outlined
            rounded
            dense
            debounce="300"
            v-model="filter"
            placeholder="Buscar"
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
        </template>
        <template v-slot:body="props">
          <q-tr>
            <q-td>
              <q-item-section>
                <q-item-label>
                  {{ props.row.parte || "" }}
                </q-item-label>
                <q-item-label class="text-secondary" caption>
                  {{ props.row.caracter }}
                </q-item-label>
              </q-item-section>
            </q-td>
            <q-td>
              <q-item class="justify-center">
                <q-radio
                  name="noty"
                  v-model="props.row.tipoId"
                  :val="6"
                  @update:model-value="cambioFormulario = true"
                />
              </q-item>
            </q-td>
            <q-td>
              <q-item class="justify-center">
                <q-radio
                  name="noty"
                  v-model="props.row.tipoId"
                  :val="1"
                  @update:model-value="cambioFormulario = true"
                />
              </q-item>
            </q-td>
            <q-td>
              <q-item class="justify-center">
                <q-radio
                  name="noty"
                  v-model="props.row.tipoId"
                  :val="3"
                  @update:model-value="cambioFormulario = true"
                />
              </q-item>
            </q-td>
            <q-td>
              <q-item class="justify-center">
                <q-radio
                  name="noty"
                  v-model="props.row.tipoId"
                  :val="12"
                  @update:model-value="cambioFormulario = true"
                />
              </q-item>
            </q-td>
            <q-td>
              <q-item class="justify-center">
                <q-radio
                  name="noty"
                  :model-value="props.row.tipoId"
                  :val="5"
                  disabled
                />
              </q-item>
            </q-td>
            <q-td>
              <q-item class="justify-center">
                <q-radio
                  name="noty"
                  :model-value="props.row.tipoId"
                  :val="11"
                  disabled
                />
              </q-item>
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </q-card-section>
    <q-card-actions class="q-pr-md" align="left">
      <q-btn
        class="col-xs-5 col-sm-3 col-md-2"
        no-caps
        :disable="!formValido"
        :color="formValido ? 'blue' : 'grey-6'"
        label="Asignar"
        @click="formValido ? guardar() : null"
      ></q-btn>
      <q-btn
        class="col-xs-5 col-sm-3 col-md-2"
        color="blue"
        outline
        no-caps
        label="Cancelar"
        @click="cancelar"
      ></q-btn>
    </q-card-actions>
    <q-inner-loading :showing="cargandoSave" color="primary" />
  </q-card>
  <q-dialog v-model="showExhorto">
    <CapturaExhorto></CapturaExhorto>
  </q-dialog>
  <DialogConfirmacion
    v-model="showCancelar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="Se perderán los cambios."
    :subTitulo="`Si continúa se perderán los cambios que ha realizado. ¿Desea continuar?`"
    @aceptar="emit('cerrar', false)"
  ></DialogConfirmacion>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { noty } from "src/helpers/notify";
import CapturaExhorto from "./CapturaExhorto.vue";
import { Validaciones } from "src/helpers/validaciones";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import { useActuariaDetalleNotificacionesStore } from "../stores/actuaria-detalle-notificaciones-store";
const actuariaDetalleNotificacionesStore =
  useActuariaDetalleNotificacionesStore();

const catalogosStore = useCatalogosStore();
const showExhorto = ref(false);
const filter = ref("");
const actuarioSelected = ref(null);
const selectZona = ref(null);
const formValido = ref(false);
const cargando = ref(false);
const cargandoSave = ref(false);
const showCancelar = ref(false);
const cambioFormulario = ref(false);
const optionsActuario = ref([]);
// const aplicaCOE = [1, 5, 11];
const rows = ref([]);

const columns = [
  {
    name: "oficios",
    label: "",
    align: "left",
    field: (row) => `${row.parte} ${row.caracter}`,
    sortable: true,
  },
  {
    name: "Lista",
    label: "Lista",
    align: "center",
    sortable: false,
  },
  {
    name: "Personal",
    label: "Personal",
    align: "center",
    sortable: false,
  },
  {
    name: "Electrónica",
    label: "Electrónica",
    align: "center",
    sortable: false,
  },
  {
    name: "Edictos",
    label: "Edictos",
    align: "center",
    sortable: false,
  },
  {
    name: "Oficio",
    label: "Oficio",
    align: "center",
    sortable: false,
  },
  {
    name: "Oficio_libre",
    label: "Oficio libre",
    align: "center",
    sortable: false,
  },
];

const props = defineProps({
  partes: {
    type: Array,
  },
  acuerdo: {
    type: Object,
  },
});
const emit = defineEmits({
  cerrar: (value) => value,
});

onMounted(async () => {
  cargando.value = true;
  rows.value = props.partes;
  try {
    await catalogosStore.getZonas();
    optionsActuario.value = catalogosStore.zonas;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargando.value = false;
});
async function cambioForm(val) {
  formValido.value = await selectZona.value?.validate(val);
  cambioFormulario.value = true;
}
function filtrarZona(val, update) {
  update(
    async () => {
      optionsActuario.value = Utils.filtrarCombo(
        val,
        catalogosStore.zonas,
        "nombreEmpleado",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

const initialPagination = {
  rowsPerPage: 0,
};
function cancelar() {
  if (cambioFormulario.value) {
    showCancelar.value = true;
  } else {
    emit("cerrar", false);
  }
}
async function guardar() {
  if (!formValido.value) return;
  const parametros = {
    asuntoNeunId: props.acuerdo.expediente.asuntoNeunId,
    sintesisOrden: props.acuerdo.sintesisOrden,
    actuarioId: actuarioSelected.value.empleadoId,
    partesNotificaciones: rows.value.map((p) => ({
      parteId: p.parteId,
      NotElecId: p.notElecId,
      tipoNotificacionID: p.tipoId,
    })),
  };
  try {
    await actuariaDetalleNotificacionesStore.postAgregarActuarioMasivo(
      parametros,
    );
    noty.correcto(
      `Se han asignado ${rows.value.length} notificaciónes al actuario ${actuarioSelected.value.nombreEmpleado}`,
    );
    emit("cerrar", false);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}
</script>
