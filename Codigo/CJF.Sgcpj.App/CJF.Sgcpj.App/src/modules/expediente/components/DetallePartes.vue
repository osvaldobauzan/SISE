<template>
  <div class="detalleParte">
    <q-card flat style="min-height: 60vh; border-radius: unset !important">
      <q-toolbar>
        <q-toolbar-title>
          <q-item>
            <q-item-section>
              <q-item-label class="text-bold text-secondary"
                ><q-icon :name="'persona'" color="secondary" />
                Parte
              </q-item-label>
            </q-item-section>
          </q-item>
        </q-toolbar-title>
        <q-btn flat round dense icon="mdi-close" v-close-popup />
      </q-toolbar>
      <q-separator />
      <q-scroll-area style="width: 100%; height: 60vh">
        <q-list class="q-pt-sm">
          <div class="row wrap q-mb-md">
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Expediente</q-item-label>
                <q-item-label>{{ expediente.asuntoAlias }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Asunto</q-item-label>
                <q-item-label>{{ expediente.catTipoAsunto }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Tipo Persona</q-item-label>
                <q-item-label>{{
                  selectedParte.descripcionTipoPersona
                }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div class="row wrap q-mb-md">
            <q-item class="col-4">
              <q-item-section>
                <q-item-label
                  class="text-grey-6"
                  v-if="selectedParte.tipo === 1"
                  >Nombre</q-item-label
                >
                <q-item-label class="text-grey-6" v-else
                  >Denominación</q-item-label
                >
                <q-item-label :lines="2"> {{ nombreCompleto }}</q-item-label>
                <q-tooltip>{{ nombreCompleto }} </q-tooltip>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Carácter de la persona</q-item-label
                >
                <q-item-label>{{
                  capitalizeWords(selectedParte.descripcionCaracterPersona)
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 2">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Tipo de persona jurídica</q-item-label
                >
                <q-item-label>{{ personaJuridica }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item
              class="col-4"
              v-if="parte.catCaracterPersonaAsuntoId === 13"
            >
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Carácter con el que promueve en su nombre</q-item-label
                >
                <q-item-label>{{ genericoPromueve }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div class="row wrap q-mb-md">
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">¿Es recurrente?</q-item-label>
                <q-item-label v-if="parte.recurrente > 0">Sí</q-item-label>
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 2">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >¿Es sujeto de derecho agrario?</q-item-label
                >
                <q-item-label v-if="parte.sujetoDerechoAgrario > 0"
                  >Sí</q-item-label
                >
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 2">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Oposición para la publicación de datos
                  personales</q-item-label
                >
                <q-item-label v-if="parte.aceptaOponePublicarDatos > 0"
                  >Sí</q-item-label
                >
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item
              class="col-4"
              v-if="
                parte.aceptaOponePublicarDatos > 0 && selectedParte.tipo === 2
              "
            >
              <q-item-section>
                <q-item-label class="text-grey-6">Fecha</q-item-label>
                <q-item-label>{{
                  parte.fechaAceptaOponePublicarDatos
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 3">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Clasificación como autoridad genérica</q-item-label
                >
                <q-item-label>{{ autoridadGenerica }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 3">
              <q-item-section>
                <q-item-label class="text-grey-6">¿Es foránea?</q-item-label>
                <q-item-label v-if="parte.foraneo > 0">Sí</q-item-label>
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 1">
              <q-item-section>
                <q-item-label class="text-grey-6">Sexo</q-item-label>
                <q-item-label>{{ sexoId }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 1">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >¿El sujeto es mayor de edad?</q-item-label
                >
                <q-item-label v-if="parte.mayorEdad === 1">Sí</q-item-label>
                <q-item-label v-else-if="parte.mayorEdad === 0"
                  >No</q-item-label
                >
                <q-item-label v-else>Se desconoce</q-item-label>
              </q-item-section>
            </q-item>
          </div>

          <div class="q-mt-lg"></div>
          <div class="row wrap q-mb-md">
            <q-item
              class="col-4"
              v-if="parte.mayorEdad === 0 && selectedParte.tipo === 1"
            >
              <q-item-section>
                <q-item-label class="text-grey-6">Edad del menor</q-item-label>
                <q-item-label>{{ edadMenor }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item
              class="col-4"
              v-if="
                selectedParte.tipo === 1 &&
                (selectedParte.catCaracterPersonaAsuntoId === 13 ||
                  selectedParte.catCaracterPersonaAsuntoId === 17) &&
                parseInt(tipoAsuntoId) === 1
              "
            >
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >¿Es víctima u ofendido del delito?</q-item-label
                >
                <q-item-label v-if="parte.victimaOfendidoDelito > 0"
                  >Sí</q-item-label
                >
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 1">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >¿Es sujeto de derecho agrario?</q-item-label
                >
                <q-item-label v-if="parte.sujetoDerechoAgrario > 0"
                  >Sí</q-item-label
                >
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 1">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Oposición para la publicación de datos
                  personales</q-item-label
                >
                <q-item-label v-if="parte.aceptaOponePublicarDatos > 0"
                  >Sí</q-item-label
                >
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
          </div>

          <div class="row wrap q-mb-md">
            <q-item
              class="col-4"
              v-if="
                parte.aceptaOponePublicarDatos > 0 && selectedParte.tipo === 1
              "
            >
              <q-item-section>
                <q-item-label class="text-grey-6">Fecha</q-item-label>
                <q-item-label>{{
                  parte.fechaAceptaOponePublicarDatos
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4" v-if="selectedParte.tipo === 1">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >¿Se considera a la persona como parte integrante de un grupo
                  de población vulnerable?</q-item-label
                >
                <q-item-label v-if="parte.esParteGrupoVulnerable > 0"
                  >Sí</q-item-label
                >
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item
              class="col-4"
              v-if="
                parte.esParteGrupoVulnerable > 0 && selectedParte.tipo === 1
              "
            >
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Grupo vulnerable</q-item-label
                >
                <q-item-label>{{ grupoVulnerable }}</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div class="row wrap q-mb-md">
            <q-item class="col-4" v-if="selectedParte.tipo === 1">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >¿Habla alguna lengua?</q-item-label
                >
                <q-item-label v-if="parte.hablaLengua > 0">Sí</q-item-label>
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
            <q-item
              class="col-4"
              v-if="parte.hablaLengua > 0 && selectedParte.tipo === 1"
            >
              <q-item-section>
                <q-item-label class="text-grey-6">Lengua</q-item-label>
                <q-item-label>{{ lenguaHabla }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-8" v-if="selectedParte.tipo === 1">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >¿Cuenta con traductor?</q-item-label
                >
                <q-item-label v-if="parte.traductor > 0">Sí</q-item-label>
                <q-item-label v-else>No</q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div class="row wrap q-mb-md"></div>
        </q-list>
      </q-scroll-area>
      <q-inner-loading :showing="cargandoDetalle"></q-inner-loading>
    </q-card>
  </div>
</template>

<script setup>
import { onMounted, ref, computed } from "vue";
import { useExpedienteElectronicoStore } from "../stores/expediente-electronico-store";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Edad } from "src/data/catalogo-edad";

const expedienteElectronicoStore = useExpedienteElectronicoStore();
const catalogoStore = useCatalogosStore();
const parte = ref({});
const caracterPromueveOptions = ref([]);
const edadData = ref(Edad);
const grupoVulnerableData = ref([]);
const lenguaData = ref([]);
const autoridadGenericaData = ref([]);
const cargandoDetalle = ref(false);

const sexoId = computed(() => {
  switch (parte.value.sexo) {
    case 0:
      return "Se desconoce";
    case 1:
      return "Hombre";
    case 2:
      return "Mujer";
    case 3:
      return "Persona intersexual";
    case 4:
      return "Persona transexual";
    default:
      return "";
  }
});

const tipoAsuntoId = computed(() => props.expediente.catTipoAsuntoId);

const personaJuridica = computed(() => {
  const elemento = catalogoStore.tipoPersonaJuridica.find(
    (objeto) =>
      objeto.catTipoPerJuridicaId === parte.value.catTipoPersonaJuridicaId,
  );
  return elemento ? elemento.descripcion : null;
});

const autoridadGenerica = computed(() => {
  const elemento = autoridadGenericaData.value.find(
    (objeto) =>
      objeto.clasificaAutoridadGenericaId ===
      parte.value.clasificaAutoridadGenericaId,
  );
  return elemento ? elemento.descripcion : null;
});
const edadMenor = computed(() => {
  const elemento = edadData.value.find(
    (objeto) => objeto.id === parte.value.edadMenor,
  );
  return elemento ? elemento.descripcion : null;
});

const genericoPromueve = computed(() => {
  const elemento = caracterPromueveOptions.value.find(
    (objeto) => objeto.id === parte.value.caracterPromueveNombre,
  );
  return elemento ? elemento.descripcion : null;
});

const grupoVulnerable = computed(() => {
  const elemento = grupoVulnerableData.value.find(
    (objeto) => objeto.id === parte.value.grupoVulnerable,
  );
  return elemento ? elemento.descripcion : null;
});

const lenguaHabla = computed(() => {
  const elemento = lenguaData.value.find(
    (objeto) => objeto.id === parte.value.lengua,
  );
  return elemento ? elemento.descripcion : null;
});

const props = defineProps({
  expediente: {
    type: Object,
    default: new Object(),
  },
  selectedParte: {
    type: Object,
    default: new Object(),
  },
});

const nombreCompleto = computed(() => {
  if (props.selectedParte.tipo == 1)
    return (
      parte.value.nombre +
      " " +
      parte.value.aPaterno +
      " " +
      parte.value.aMaterno
    );
  else return parte.value.denominacionDeAutoridad;
});

onMounted(async () => {
  cargandoDetalle.value = true;
  try {
    caracterPromueveOptions.value =
      await catalogoStore.obtenerCatalogoGenerico(521);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    grupoVulnerableData.value =
      await catalogoStore.obtenerCatalogoGenerico(832);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    lenguaData.value = await catalogoStore.obtenerCatalogoGenerico(2156);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogoStore.obtenerClasificacionAutoridadGenerica();
    autoridadGenericaData.value = catalogoStore.clasificacionAutoridadGenerica;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    const respuesta = await expedienteElectronicoStore.obtenerParte(
      props.selectedParte.personaId,
    );
    Object.assign(parte.value, respuesta);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogoStore.obtenerTipoPersonaJuridica(tipoAsuntoId.value);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoDetalle.value = false;
});

function capitalizeWords(inputString) {
  if (inputString) {
    const words = inputString.split(" ");

    const capitalizedWords = words.map((word) => {
      if (word.length === 0) {
        return word;
      }
      const firstLetter = word.charAt(0).toUpperCase();
      const restOfWord = word.slice(1).toLowerCase();
      return firstLetter + restOfWord;
    });

    const resultString = capitalizedWords.join(" ");

    return resultString;
  }
}
</script>

<style scoped>
.pad-left {
  padding-left: 1em;
}

.maxWidth {
  word-wrap: break-word;
}
.q-card {
  border-radius: 0px !important;
}
.line {
  margin-left: 10px;
  width: 90%;
  border-bottom: 1px solid #9e9e9e;
}
.detalleParte {
  background: white;
  width: 50% !important;
  height: 70% !important;
}
</style>
