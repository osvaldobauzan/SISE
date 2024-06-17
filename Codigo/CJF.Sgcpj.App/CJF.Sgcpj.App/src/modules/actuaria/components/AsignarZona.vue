<template>
  <q-card style="min-width: 40vw">
    <q-toolbar>
      <q-toolbar-title class="text-bold">
        Asignar Actuario(Zona)
      </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" @click="cancelar" />
    </q-toolbar>
    <q-card-section class="q-gutter-md">
      <q-item class="q-pl-none">
        <q-item-section>
          <q-item-label caption> {{ parte.caracter }} </q-item-label>
          <q-item-label class="text-bold"> {{ parte.parte }} </q-item-label>
        </q-item-section>
      </q-item>
      <q-item-label class="q-my-md">
        Selecciona a la persona que se hará cargo de realizar la notificación a
        la parte.
      </q-item-label>

      <q-form ref="form" class="q-gutter-lg q-my-md q-pl-md">
        <q-select
          ref="selectZona"
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
        <div>
          <q-option-group
            v-model="parteComputed.tipoId"
            :options="options"
            color="primary"
            inline
            @update:model-value="cambioTipo"
          >
            <template v-slot:label-2>
              Electrónica
              <q-item-label class="text-secondary" caption align="center">
                Usuario PSL: {{ props.parte?.usuarioRegistro }}
              </q-item-label>
            </template>
            <template v-slot:label-5> Oficio libre </template>
          </q-option-group>
        </div>
      </q-form>
      <q-inner-loading :showing="cargando" color="primary" />
    </q-card-section>
    <q-card-actions class="q-ml-sm q-mr-sm">
      <q-btn
        class="col"
        no-caps
        :disable="!formValido"
        :color="formValido ? 'blue' : 'grey-6'"
        label="Asignar"
        @click="async () => await asignar()"
      ></q-btn>
      <q-btn
        class="col"
        :color="'blue'"
        outline
        no-caps
        label="Cancelar"
        @click="cancelar"
      ></q-btn>
    </q-card-actions>
    <q-inner-loading :showing="cargandoSave" color="primary" />
  </q-card>
  <DialogConfirmacion
    v-model="showCancelar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="Se perderán los cambios."
    :subTitulo="`Si continúa se perderán los cambios que ha realizado. ¿Desea continuar?`"
    @aceptar="emit('cerrar', false)"
  ></DialogConfirmacion>
  <div>
    <q-dialog v-model="showOficioForm">
      <OficioLibre
        :model-value="oficioLibre"
        :edicion="esEditar"
        :cambio-oficio-libre="cambioOficioLibre"
        @cerrarEditar="
          (val) => {
            //console.log(val);

            if (val.value) {
              oficioLibre.text = val.text;
              showOficioForm = false;
            } else {
              showOficioForm = false;
            }
          }
        "
        @update:modelValue="
          (val) => {
            oficioLibre = val;
            showOficioForm = false;
            cambioOficioLibre = true;
          }
        "
      >
      </OficioLibre>
    </q-dialog>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { noty } from "src/helpers/notify";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import { useActuariaDetalleNotificacionesStore } from "../stores/actuaria-detalle-notificaciones-store";
import { Validaciones } from "src/helpers/validaciones";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import OficioLibre from "src/components/OficioLibre.vue";
// const aplicaCOE = [1, 5, 11];
// eslint-disable-next-line no-unused-vars
const actuariaDetalleNotificacionesStore =
  useActuariaDetalleNotificacionesStore();
const catalogosStore = useCatalogosStore();
const selectZona = ref(null);
const formValido = ref(false);
const cargando = ref(false);
const cargandoSave = ref(false);
const showOficioForm = ref(false);
const oficioLibre = ref({
  text: "",
});
const emit = defineEmits({
  cerrar: (value) => value,
  refrescarNotificaciones: () => true,
});
const parteComputed = computed({
  get() {
    return props.parte;
  },
  set() {},
});

const props = defineProps({
  parte: Object,
  acuerdo: Object,
});
const options = [
  {
    label: "Lista",
    value: 6,
  },
  {
    label: "Personal",
    value: 1,
  },
  {
    label: "Electrónica",
    value: 3,
    disable: props.parte?.usuarioRegistro == null ? true : false,
  },
  {
    label: "Edictos",
    value: 12,
  },
  {
    label: "Oficio",
    value: 5,
  },
  {
    label: "Oficio libre",
    value: 11,
  },
];
const actuarioSelected = ref(null);
const showCancelar = ref(false);
const cambioFormulario = ref(false);
const optionsActuario = ref([]);
onMounted(async () => {
  cargando.value = true;
  try {
    await catalogosStore.getZonas();
    optionsActuario.value = catalogosStore.zonas;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (props.parte.asignadoActuario)
    actuarioSelected.value = optionsActuario.value.find(
      (x) =>
        x.nombreEmpleado == props.parte.asignadoActuario &&
        x.nombre == props.parte.asignadoZona,
    );
  cargando.value = false;
});
async function cambioForm(val) {
  formValido.value = await selectZona.value?.validate(val);
  cambioFormulario.value = true;
}
async function cambioTipo(val) {
  formValido.value = val && actuarioSelected ? true : false;
  if (val == 11) {
    showOficioForm.value = true;
    //emit("cerrar", false);
  }
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
async function asignar() {
  if (!formValido.value) return;
  cargandoSave.value = true;
  const params = {
    asuntoAlias: props.acuerdo.expediente.asuntoAlias,
    catTipoAsuntoId: props.acuerdo.expediente.catTipoAsuntoId,
    catTipoAsunto: props.acuerdo.expediente.catTipoAsunto,
    asuntoNeunId: props.acuerdo.expediente.asuntoNeunId,
    asuntoId: props.acuerdo.sintesisOrden, //TODO: quitar cuando se reaice refactor en BE
    sintesisOrden: props.acuerdo.sintesisOrden,
    actuarioId: actuarioSelected.value.empleadoId,
    parte: props.parte.parteId,
    promovente: props.parte.parteId == 0 ? props.parte.promoventeId : 0,
    tipoNotificacionID: parteComputed.value.tipoId,
    notElecId: props.parte.notElecId,
    generarOficio: [
      {
        nombreParte: props.parte.parte,
        textoOficioLibre: oficioLibre.value.text,
        tipoNotificacionID: parteComputed.value.tipoId,
      },
    ],
  };
  //console.log(params);
  if (props.parte.asignadoActuario) {
    await editarAsignacion(params);
  } else {
    await crearAsignacion(params);
  }
  cargandoSave.value = false;
}
async function crearAsignacion(params) {
  try {
    await actuariaDetalleNotificacionesStore.postAgregarActuario(params);
    noty.correcto(
      `Notificación asignada al actuario ${actuarioSelected.value.nombreEmpleado}`,
    );
    emit("refrescarNotificaciones");
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}
async function editarAsignacion(params) {
  try {
    await actuariaDetalleNotificacionesStore.putAgregarActuario(params);
    noty.correcto(
      `Notificación asignada al actuario ${actuarioSelected.value.nombreEmpleado}`,
    );
    emit("refrescarNotificaciones");
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}
function cancelar() {
  if (cambioFormulario.value) {
    showCancelar.value = true;
  } else {
    emit("cerrar", false);
  }
}
</script>
<style scoped>
.disabled .q-checkbox {
  cursor: pointer !important;
}
:deep(.disabled .q-checkbox .q-checkbox__inner) {
  cursor: pointer !important;
  color: #000;
}
:deep(.disabled .q-checkbox .q-checkbox__label) {
  cursor: pointer !important;
}
:deep(.q-checkbox__bg) {
  cursor: pointer !important;
}
:deep(.q-checkbox__svg) {
  cursor: pointer !important;
}
</style>
