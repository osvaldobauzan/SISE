<template>
  <q-card style="min-width: 40vw; height: 87vh">
    <q-toolbar>
      <q-toolbar-title class="text-bold">
        {{ esEditar ? "Editar Parte" : "Agregar Partes" }}
      </q-toolbar-title>
      <q-btn
        flat
        round
        dense
        icon="mdi-close"
        @click="cambioForm ? (showCancelar = true) : emit('cerrar')"
      />
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section class="row q-mb-md">
      <q-scroll-area
        ref="scrollArea"
        :style="
          'width: 100%;' +
          ($q.screen.gt.md
            ? 'height: 67vh'
            : $q.screen.gt.xs
              ? 'height: 64vh'
              : 'height: 47vh')
        "
      >
        <div class="row q-pl-sm">
          <div class="row q-pa-sm col-xs-12 col-sm-6 col-md-4">
            <q-item class="q-pl-none">
              <q-item-section>
                <q-icon :name="'mdi-folder'" size="1.2rem" color="secondary" />
              </q-item-section>
            </q-item>
            <q-item class="q-pl-none">
              <q-item-section>
                <q-item-label class="text-grey-6">Expediente</q-item-label>
                <q-item-label class="text-secondary">{{
                  expediente.asuntoAlias || ""
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <q-item class="q-pa-sm col-xs-12 col-sm-6 col-md-4">
            <q-item-section>
              <q-item-label class="text-grey-6">Asunto</q-item-label>
              <q-item-label>{{ expediente.tipoAsunto || "" }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item
            v-if="expediente.tipoProcedimiento"
            class="q-pa-sm col-xs-12 col-sm-6 col-md-4"
          >
            <q-item-section>
              <q-item-label class="text-grey-6"
                >Tipo Procedimiento</q-item-label
              >
              <q-item-label>{{
                expediente.tipoProcedimiento || ""
              }}</q-item-label>
            </q-item-section>
          </q-item>
        </div>

        <div class="row col-12">
          <q-item class="col-12">
            <q-item-section>
              <q-item-label>Tipo de persona</q-item-label>
            </q-item-section>
          </q-item>
          <q-radio
            class="q-pa-sm col-xs-6 col-sm-6 col-md-4"
            v-model="tipoPersona"
            :val="1"
            label="Física"
            @update:model-value="
              (validaFormulario = true), cambiaronParametros()
            "
          />
          <q-radio
            class="q-pa-sm col-xs-6 col-sm-6 col-md-4"
            v-model="tipoPersona"
            :val="2"
            label="Jurídica"
            @update:model-value="
              (validaFormulario = true), cambiaronParametros()
            "
          />
          <q-radio
            class="q-pa-sm col-xs-6 col-sm-6 col-md-4"
            v-model="tipoPersona"
            :val="3"
            label="Autoridad"
            @update:model-value="
              (validaFormulario = true), cambiaronParametros()
            "
          />
        </div>
        <q-form
          @submit="submitForm"
          class="row full-width"
          ref="formParte"
          no-focus-error
        >
          <div class="row full-width q-pl-sm">
            <component
              v-if="!cargando"
              :is="componentForm"
              v-model="parte"
              :esEditar="esEditar"
              @update:modelValue="cambiaronParametros"
            ></component>
          </div>
        </q-form>
        <q-inner-loading :showing="cargando"> </q-inner-loading>
      </q-scroll-area>
    </q-card-section>
    <q-separator></q-separator>
    <q-card-actions align="left">
      <q-btn
        no-caps
        style="min-width: 164px"
        :color="!formValido ? 'grey-6' : 'blue'"
        @click="formValido ? submitForm() : null"
        :disable="!formValido"
        :label="'Guardar'"
        class="q-ml-sm q-mr-sm"
      />
      <q-btn
        no-caps
        @click="cambioForm ? (showCancelar = true) : emit('cerrar')"
        outline
        label="Cancelar"
        :color="'blue'"
        :style="'min-width: 164px'"
      />
      <q-btn
        v-if="!esEditar"
        flat
        no-caps
        style="min-width: 164px"
        :color="!formValido ? 'grey-6' : 'blue'"
        @click="formValido ? submitForm(true) : null"
        :disable="!formValido"
        :label="'Guardar y capturar otra'"
        class="q-ml-sm q-mr-sm"
      />
    </q-card-actions>
    <q-inner-loading :showing="cargandoGuardado"> </q-inner-loading>
  </q-card>
  <DialogConfirmacion
    v-model="showCancelar"
    label-btn-cancel="Cancelar"
    label-btn-ok="Aceptar"
    titulo="Se perderán los cambios."
    :subTitulo="`Si continúa se perderán los cambios que ha realizado. ¿Desea continuar?`"
    @aceptar="emit('cerrar')"
  ></DialogConfirmacion>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";
import PartePersonaFisicaForm from "./PartePersonaFisicaForm.vue";
import PartePersonaJuridicaForm from "./PartePersonaJuridicaForm.vue";
import ParteAutoridadForm from "./ParteAutoridadForm.vue";
import { FormParte, PersonaAsunto } from "../data/form-parte";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store";
import { noty } from "src/helpers/notify";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";

const expedienteElectronicoStore = useExpedienteElectronicoStore();
const props = defineProps({
  expediente: {
    default: {},
  },
  itemParte: {
    default: {},
  },
  esEditar: {
    default: false,
  },
});
const emit = defineEmits({
  cerrar: (value) => value,
  "refrescar:partes": (value) => value,
});

const catalogoStore = useCatalogosStore();
const parte = ref(new PersonaAsunto());
const scrollArea = ref(null);

const cambioForm = ref(false);
const cargando = ref(false);
const cargandoGuardado = ref(false);
const showCancelar = ref(false);
const tipoPersona = ref(1);
const formValido = ref(false);
const formParte = ref(null);
const validaFormulario = ref(false);
const componentForm = computed(() => {
  switch (tipoPersona.value) {
    case 1:
      return PartePersonaFisicaForm;
    case 2:
      return PartePersonaJuridicaForm;
    case 3:
      return ParteAutoridadForm;
    default:
      return PartePersonaFisicaForm;
  }
});

const tipoAsuntoId = computed(() => props.expediente.catTipoAsuntoId);

async function cambiaronParametros() {
  formValido.value = await formParte.value?.validate(false);
  if (validaFormulario.value) {
    reiniciarFormulario(false);
  }
  //re valida formularios despues de 500 ms
  setTimeout(() => {
    formParte.value?.validate(false).then((v) => {
      formValido.value = v;
      if (validaFormulario.value) {
        reiniciarFormulario(false);
        validaFormulario.value = false;
      }
    });
  }, 500);
  cambioForm.value = true;
}
onMounted(async () => {
  cargando.value = true;
  try {
    await catalogoStore.obtenerTipoPersonaCaracter(tipoAsuntoId.value);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogoStore.obtenerTipoPersonaJuridica(tipoAsuntoId.value);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogoStore.obtenerClasificacionAutoridadGenerica();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  if (props.esEditar) {
    try {
      const respuesta = await expedienteElectronicoStore.obtenerParte(
        props.itemParte.personaId,
      );

      Object.assign(parte.value, respuesta);
      tipoPersona.value = parte.value.catTipoPersonaId;
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
  cargando.value = false;
});
async function submitForm(agregarOtro = false) {
  if (!(await formParte.value?.validate(false))) {
    formValido.value = await formParte.value?.validate(false);
    return;
  }
  const parametros = new FormParte();
  parametros.personaId = props.esEditar ? props.itemParte.personaId : 0;
  parametros.asuntoNeunId = props.expediente.asuntoNeunId;
  const copiaParte = { ...parte.value };
  copiaParte.edad = null;
  copiaParte.sexoJson = null;
  copiaParte.grupoVulnerableJson = null;
  copiaParte.lenguaJson = null;
  copiaParte.caracterPromueve = null;
  copiaParte.caracterPersona = null;
  copiaParte.catTipoPersonaJuridica = null;
  copiaParte.clasificaAutoridadGenerica = null;
  parametros.personaAsunto = {
    ...copiaParte,
    nombre: parte.value.nombre || parte.value.denominacionDeAutoridad,
    denominacionDeAutoridad:
      parte.value.denominacionDeAutoridad || parte.value.nombre,
    catCaracterPersonaAsuntoId: parte.value.caracterPersona?.caracterPersonaId,
    sexo: parte.value.sexoJson?.kIdSexo,
    catTipoPersonaJuridicaId:
      parte.value.catTipoPersonaJuridica?.catTipoPerJuridicaId,
    clasificaAutoridadGenericaId:
      parte.value.clasificaAutoridadGenerica?.clasificaAutoridadGenericaId,
    caracterPromueveNombre: parte.value.caracterPromueve?.id,
    grupoVulnerable: parte.value.grupoVulnerableJson?.id,
    edadMenor: parte.value.edad?.id,
    lengua: parte.value.lenguaJson?.id,
    catTipoPersonaId: tipoPersona.value,
  };
  if (props.esEditar) {
    await editarParte(parametros);
  } else {
    await agregarParte(parametros, agregarOtro);
  }
}
async function agregarParte(parametros, agregarOtro = false) {
  cargandoGuardado.value = true;
  try {
    await expedienteElectronicoStore.agregarParte(parametros);
    noty.correcto(
      `Se ha agregado la parte correctamente al expediente ${props.expediente.asuntoAlias} ${props.expediente.tipoAsunto}`,
    );
    emit("refrescar:partes");
    if (agregarOtro) {
      await reiniciarFormulario();
    } else {
      emit("cerrar");
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoGuardado.value = false;
}
async function editarParte(parametros) {
  cargandoGuardado.value = true;
  try {
    await expedienteElectronicoStore.editarParte(parametros);
    noty.correcto(
      `Se ha modificado la parte correctamente del expediente ${props.expediente.asuntoAlias} ${props.expediente.tipoAsunto}`,
    );
    emit("refrescar:partes");
    emit("cerrar");
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoGuardado.value = false;
}
async function reiniciarFormulario(limpiarTipoPersona = true) {
  Object.assign(parte.value, new PersonaAsunto());
  formParte.value?.reset();
  formParte.value?.resetValidation();
  formParte.value?.focus();
  scrollArea.value.setScrollPosition("vertical", 0, 1);
  if (limpiarTipoPersona) {
    tipoPersona.value = 1;
    cambioForm.value = false;
  }
  parte.value.edad = null;
  parte.value.sexoJson = null;
  parte.value.grupoVulnerableJson = null;
  parte.value.lenguaJson = null;
  parte.value.caracterPromueve = null;
  parte.value.caracterPersona = null;
  parte.value.catTipoPersonaJuridica = null;
  parte.value.clasificaAutoridadGenerica = null;
}
</script>
