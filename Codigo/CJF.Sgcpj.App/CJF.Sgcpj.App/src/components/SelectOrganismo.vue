<template>
  <q-card
    v-if="!isLoading && (showError || authStore.organismos.length < 1)"
    class="card-fixed-width"
  >
    <q-card-section>
      <div class="q-gutter-lg text-center">
        <q-icon name="mdi-alert" color="negative" size="xl" />
        <div class="text-body1 text-bold">
          Su usuario no está asociado a un órgano jurisdiccional,<br />
          favor de validar con su titular.
        </div>
        <div class="text-body2">
          Si el problema persiste, por favor comuníquese al #301 extensión 6050.
        </div>
      </div>
    </q-card-section>
    <q-card-actions class="justify-center">
      <q-btn
        class="btn-fixed-width"
        color="blue"
        @click="cerrarSesion()"
        label="Salir"
      />
    </q-card-actions>
  </q-card>
  <q-card
    v-if="authStore.organismos?.length > 1 && !isLoading"
    class="card-fixed-width q-pa-md"
  >
    <q-card-section>
      <q-form ref="form" class="q-gutter-lg">
        <q-img
          src="images/logotipoCJF_horizontal_color.png"
          class="img-fixed-width"
        />
        <div class="text-body2">
          Bienvenido al
          <span class="text-bold text-primary">
            Sistema Integral de Seguimiento de Expediente</span
          >, selecciona el órgano jurisdiccional en el cual quieres trabajar.
        </div>
        <q-select
          ref="selectOrg"
          dense
          filled
          autofocus
          v-cortarLabel
          @input-value="(organismo = null), cambioForm(null)"
          v-model="organismo"
          virtual-scroll-slice-size="100"
          use-input
          input-debounce="0"
          label="Órgano jurisdiccional *"
          @update:model-value="cambioForm"
          :options="organismosOptions"
          @filter="filtrarOrganismo"
          option-label="nombreOficial"
          option-value="catOrganismoId"
          :rules="[
            (val) => Validaciones.validaSelectRequerido(val?.catOrganismoId),
          ]"
        />
      </q-form>
    </q-card-section>
    <q-card-actions>
      <q-btn
        no-caps
        :class="$q.screen.gt.xs ? 'btn-fixed-width' : ''"
        label="Continuar"
        @click="guardaOrganismo()"
        :disable="!formValido && authStore.organismos.length > 0"
        :color="
          formValido || authStore.organismos.length < 1 ? 'blue' : 'grey-6'
        "
      >
      </q-btn>
      <q-btn
        no-caps
        outline
        :class="$q.screen.gt.xs ? 'btn-fixed-width' : ''"
        :color="'blue'"
        @click="cerrarSesion()"
        :label="'Regresar'"
      />
    </q-card-actions>
    <q-inner-loading :showing="isLoading" color="primary" />
  </q-card>
</template>

<script setup>
import { Utils } from "src/helpers/utils";
import { ref, onMounted } from "vue";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { Validaciones } from "src/helpers/validaciones";
import { manejoErrores } from "src/helpers/manejo-errores";

const form = ref(null);
const selectOrg = ref(null);
const organismo = ref(null);
const formValido = ref(false);
const isLoading = ref(true);
const showError = ref(false);
const authStore = useAuthStore();
const organismosOptions = ref(null);

const emit = defineEmits(["cerrar"]);

onMounted(async () => {
  isLoading.value = true;
  try {
    await authStore.getOrganismos();
    organismosOptions.value = authStore.organismos;
    if (organismosOptions.value?.length == 1) {
      organismo.value = organismosOptions.value[0];
      await guardaOrganismo(true);
    }
  } catch (error) {
    showError.value = true;
    manejoErrores.mostrarError(error);
  }
  isLoading.value = false;
});

async function cambioForm(val) {
  formValido.value = await selectOrg.value?.validate(val);
}

async function cerrarSesion() {
  await authStore.logoutUser(true);
}

async function guardaOrganismo(soloTieneUnOrgano = false) {
  isLoading.value = true;
  if ((await form.value?.validate(false)) || soloTieneUnOrgano) {
    try {
      await authStore.crearSesion(organismo.value);
      emit("cerrar");
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
  isLoading.value = false;
}

function filtrarOrganismo(val, update) {
  update(
    async () => {
      organismosOptions.value = Utils.filtrarCombo(
        val,
        authStore.organismos,
        "nombreOficial",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
</script>
