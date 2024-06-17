<template>
  <q-card style="min-width: 70vw; min-height: 70%" class="q-pa-md">
    <q-toolbar class="">
      <q-item-section>
        <div class="row items-center">
          <q-toolbar-title class="q-pb-md text-bold"></q-toolbar-title>
          <q-btn flat round dense icon="mdi-close" v-close-popup />
        </div>
      </q-item-section>
    </q-toolbar>
    <q-inner-loading :showing="isLoading" color="primary" />
    <q-toolbar class="q-pb-sm q-pl-none">
      <div class="row full-width">
        <q-option-group
          class="col-xs-12 col-sm-6 col-md-4 col-lg-2"
          name="calendars"
          v-model="selectedOption"
          :options="options"
          type="checkbox"
          color="primary"
        />
        <div class="q-pa-sm col-xs-12 col-md-4 col-sm-6 col-lg-3">
          <SelectDateComponent
            ref="selectDateComponent"
            title="Fecha"
            @update:selectedDate="setSelectedDate"
          >
          <template v-slot>
            <q-item
            clickable
            v-ripple
            v-close-popup
            @click="limpiarFecha"
          >
            <q-item-section avatar>
              <q-icon
                color="grey-8"
                name="mdi-delete-outline"
              />
            </q-item-section>
            <q-item-section>Limpiar</q-item-section>
          </q-item>
          </template>
        </SelectDateComponent>
        </div>
        <q-select
          clearable
          v-cortarLabel
          @input-value="expedienteEncontrado = null"
          class="q-pa-md col-xs-12 col-md-4 col-sm-6 col-lg-3"
          label="Buscar un expediente existente "
          v-model="expedienteEncontrado"
          @filter="buscarExpedientePorNumero"
          :options="opcionesExpediente"
          option-value="asuntoNeunId"
          use-input
          input-debounce="0"
          hint="Formato Número/AAAA"
          dense
          filled
        >
          <template v-slot:append>
            <q-btn flat round icon="mdi-magnify" />
          </template>
          <template v-slot:no-option>
            <q-item>
              <q-item-section class="text-red row">
                <span>
                  <q-icon name="info" /> No se encontraron resultados
                </span>
              </q-item-section>
            </q-item>
          </template>
          <template v-slot:option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.asuntoAlias }}</q-item-label>
                <q-item-label caption>
                  {{ scope.opt.tipoAsunto }}
                </q-item-label>
                <q-item-label
                  class="text-caption"
                  v-if="scope.opt.tipoProcedimiento !== ''"
                >
                  {{ scope.opt.tipoProcedimiento }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </template>
        </q-select>
        <q-select
          v-cortarLabel
          @input-value="audiencia = null"
          dense
          filled
          class="q-pa-md col-xs-12 col-md-4 col-sm-6 col-lg-3"
          use-input
          input-debounce="0"
          v-model="audiencia"
          label="Persona *"
          option-label="descripcion"
          option-value="id"
          @filter="filtrarAudiencia"
          @update:model-value="cambioForm"
          :options="audienciaOptions"
          :loading="cargandoAudiencia"
          :rules="[
            (val) => Validaciones.validaSelectRequerido(val?.descripcion),
          ]"
        >
          <template v-slot:append>
            <q-btn flat round icon="mdi-magnify" />
          </template>
        </q-select>
        <div class="col-xs-12 col-sm-6 col-lg-1 col-md-8">
          <q-btn
            dense
            no-caps
            color="primary"
            label="Buscar"
            @click="buscarData"
            class="q-mt-lg q-ml-sm q-px-lg row"
            style="
              min-width: 100px;
              max-height: 3em;
              min-height: 2.5em;
              max-width: 130px;
            "
            rounded
          >
          </q-btn>
        </div>
      </div>
    </q-toolbar>
    <q-toolbar class="q-pb-sm q-gutter-xs">
      <q-btn
        dense
        no-caps
        color="primary"
        label="Agendar"
        icon="mdi-plus"
        @click="(showAgendar = true)"
        class="q-px-lg"
        style="min-width: 200px"
      >
      </q-btn>
      <q-btn
        dense
        no-caps
        outline
        color="primary"
        label="Crear recordatorio"
        icon="mdi-bell-circle-outline"
        @click="(currentDay = null), (showAgendar = true)"
        class="q-ml-sm q-px-lg"
        style="min-width: 200px"
      >
      </q-btn>
      <q-space></q-space>
      <InputSearchTable v-model="textoBuscar" />
    </q-toolbar>
    <q-list bordered class="rounded-borders">
      <q-expansion-item v-if="selectedOption.includes('1')" expand-separator class="q-pb-md">
        <template v-slot:header>
          <q-item-section>
            <q-item-label class="text-bold text-subtitle1"
              >Audiencias</q-item-label
            >
          </q-item-section>
        </template>
        <q-card>
          <q-table
            flat
            style="max-height: 50vh"
            :filter="textoBuscar"
            ref="tableRef"
            :rows="rows"
            :columns="columns"
            row-key="index"
          >
            <template v-slot:body="props">
              <q-tr>
                <q-td style="max-width: 15em">
                  <q-item class="q-pl-none">
                    <q-item-section>
                      <q-item-label class="text-bold">
                        {{ props.row.expediente }}
                      </q-item-label>
                      <q-item-label caption>
                        {{ props.row.tipoAsunto }}
                      </q-item-label>
                      <q-item-label caption>
                        {{ props.row.tipoProcedimiento }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </q-td>
                <q-td>
                    <q-item-label>{{ props.row.parte }}</q-item-label>
                </q-td>
                <q-td>
                  <q-item-label>{{
                    props.row.fechaAudiencia.split("-").reverse().join("/")
                  }}</q-item-label>
                </q-td>
                <q-td>
                  <q-item-label>{{ props.row.horaAudiencia }}</q-item-label>
                </q-td>
                <q-td >
                  <q-item-label>{{ props.row.audiencia }}</q-item-label>
                </q-td>
                <q-td>
                  <q-item-label>{{ props.row.resultado }}</q-item-label>
                </q-td>
                <q-td>
                  
                  <q-item-label>{{ props.row.empleado }}</q-item-label>
                </q-td>
                <q-td>
                  <q-item-label>{{ props.row.secretario }}</q-item-label>

                </q-td>
              </q-tr>
            </template>
          </q-table>
          <q-inner-loading :showing="cargandoEventos"> </q-inner-loading>
        </q-card>
      </q-expansion-item>
      <q-separator></q-separator>
      <q-expansion-item v-if="selectedOption.includes('2')" expand-separator>
        <template v-slot:header>
          <q-item-section>
            <q-item class="q-pl-none">
              <q-item-label class="text-bold text-subtitle1"
                >Recordatorios</q-item-label
              >
              <div class="q-pl-md">
                <q-option-group
                  v-model="groupRecordatorio"
                  :options="optionsRecordatorio"
                  color="primary"
                  inline
                  dense
                />
              </div>
            </q-item>
          </q-item-section>
        </template>
        <q-card>
          <q-card-section> </q-card-section
          ><q-item-label class="text-bold text-subtitle1"
            >Recordatorios</q-item-label
          >
        </q-card>
      </q-expansion-item>
      <q-separator></q-separator>
    </q-list>

    <!-- <q-card-actions align="left">
      <q-btn
        no-caps
        label="Guardar"
        :color="selected.length > 0 ? 'secondary' : 'grey-6'"
        style="min-width: 164px"
        type="submit"
        :disable="!formValido"
        @click="guardar"
      />
      <q-btn
        no-caps
        @click="cancelar"
        outline
        label="Cancelar"
        :color="'secondary'"
        style="min-width: 164px"
      />
    </q-card-actions> -->
  </q-card>
  <q-dialog v-model="showAgendar" persistent>
      <AgendarAudiencia
        @cerrar="showAgendar = false"
      ></AgendarAudiencia>
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
import { Validaciones } from "src/helpers/validaciones";
// import { date } from "quasar";
import { onMounted, ref } from "vue";
import { manejoErrores } from "src/helpers/manejo-errores";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import InputSearchTable from "src/components/InputSearchTable.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import { useSentenciasStore } from "src/modules/sentencias/store/sentencias-store";
import { Utils } from "src/helpers/utils";
import { useAgendaStore } from "src/modules/agenda/stores/agenda-store";

import { AgendaDto } from "../data/agendadto";
import { noty } from "src/helpers/notify";
import AgendarAudiencia from "./AgendarAudiencia.vue";

const agendaStore = useAgendaStore();

const sentenciasStore = useSentenciasStore();
const showCancelar = ref(false);
const showAgendar = ref(false);
const groupRecordatorio = ref("1");
const selectedDate = ref({});
const selectDateComponent = ref(null);
const expedienteEncontrado = ref(null);
const opcionesExpediente = ref([]);

const optionsRecordatorio = [
  {
    label: "Todo",
    value: "1",
  },
  {
    label: "Mis recordatorios",
    value: "2",
  },
];
const selectedOption = ref(["1", "2"]);

const options = [
  {
    label: "Audiencias",
    value: "1",
  },
  {
    label: "Recordatorios",
    value: "2",
  },
];

// const props = defineProps({
//   // v-model
//   items: {
//     default: [],
//   },
// });
const emit = defineEmits({
  cerrar: (value) => value,
});
const textoBuscar = ref("");
const tableRef = ref(null);
const isLoading = ref(false);
const cargandoEventos = ref(false);

const columns = [
  {
    name: "expediente",
    required: true,
    label: "Expediente",
    align: "left",
    field: (row) => `${row.expediente} ${row.tipoAsunto}`,
    sortable: false,
  },
  {
    name: "parte",
    align: "left",
    label: "Parte",
    field: (row) => row.parte,
    sortable: false,
  },
  {
    name: "fecha",
    align: "left",
    label: "Fecha de Audiencia",
    field: (row) => `${row.fechaAudiencia.split("-").reverse().join("/")}`,
    sortable: false,
  },
  {
    name: "horaAudiencia",
    align: "left",
    label: "Hora de audiencia",
    field: (row) => `${row.horaAudiencia}`,
    sortable: false,
  },
  {
    name: "audiencia ",
    align: "left",
    label: "Audiencia ",
    field: (row) => row.audiencia,
    sortable: false,
  },

  {
    name: "resultado",
    align: "left",
    label: "Resultado",
    field: (row) => row.resultado,
    sortable: false,
  },
  {
    name: "empleado",
    align: "left",
    label: "Agendado por",
    field: (row) => row.empleado,
    sortable: false,
  },
  {
    name: "secretario",
    align: "left",
    label: "Secretario",
    field: (row) => row.secretario,
    sortable: false,
  },
];

const rows = ref(new Array(new AgendaDto()).splice(1,1));

onMounted(async () => {
  limpiarFecha();
});
function limpiarFecha(){
  selectedDate.value = null;
  selectDateComponent.value.setFecha(null);
}

async function setSelectedDate(value) {
  selectedDate.value = value;
  // pagination.value.page = 1;
  // await setRows();
}
async function buscarExpedientePorNumero(val, update, abort) {
  update(
    async () => {
      if (
        val === "" ||
        val.length <= 5 ||
        typeof Validaciones.validaNoExpediente(val) === "string"
      ) {
        abort();
        return;
      } else {
        try {
          await sentenciasStore.buscarExpediente(val, null, 2);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        opcionesExpediente.value = sentenciasStore.expediente;
        if (opcionesExpediente.value?.length === 1) {
          buscarExpediente.value = opcionesExpediente.value[0];
        }
      }
    },
    // "ref" is the Vue reference to the QSelect
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 100),
  );
}
async function buscarData() {
  if (!selectedOption.value || selectedOption.value.length == 0) {
    noty.error("Seleccióna por lo menos una opción Audiencias y/o Recoradorios");
    return;
  }
  if(selectedOption.value.includes("1")){
    await buscarAgenda();
  }
}
async function buscarAgenda() {
  try {
    cargandoEventos.value = true;
    rows.value = await agendaStore.obtenerAgendaAudienciaPorFecha(
      selectedDate.value?.from,
      selectedDate.value?.to,
      expedienteEncontrado.value?.asuntoAlias,
      null
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoEventos.value = false;
}
</script>

<style scoped></style>
