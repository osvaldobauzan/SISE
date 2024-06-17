<template>
  <q-card bordered flat class="text-center">
    <q-toolbar>
      <q-space></q-space>
      <q-btn flat round dense icon="mdi-close" v-close-popup></q-btn>
    </q-toolbar>
    <div class="q-mt-md">
      <q-avatar size="90px">
        <q-img src="images/user-icon.png"> </q-img>
      </q-avatar>
    </div>
    <q-card-section>
      <q-item-label>{{ user.completo }}</q-item-label>
      <q-item-label class="text-caption text-grey">{{
        user.cargoDescripcion
      }}</q-item-label>
      <q-item-label class="text-subtitle2">{{ user.displayName }}</q-item-label>
      <q-item-label>{{ user.nombreOficial }}</q-item-label>
    </q-card-section>
    <q-card-actions>
      <q-btn
        v-close-popup
        flat
        label="Configuración"
        icon="mdi-cog-outline"
        size="sm"
        color="primary"
        class="text-caption"
        to="/configuracion"
      />
      <q-space></q-space>
      <q-btn
        outline
        label="Cerrar sesión"
        icon="mdi-logout"
        size="sm"
        color="primary"
        push
        v-close-popup
        @click="logout"
      />
    </q-card-actions>
    <q-separator></q-separator>
    <q-card-section>
      <q-card flat bordered class="bg-grey-1">
        <q-item-label header class="text-bold" v-if="organismos.length > 0"
          >Cambiar organismo</q-item-label
        >
        <q-separator></q-separator>
        <q-list align="left" separator>
          <q-item
            clickable
            v-ripple
            v-for="org in organismos"
            :key="org.catOrganismoId"
            @click="guardaOrganismo(org)"
            :active="org.catOrganismoId === user.catOrganismoId"
            active-class="bg-blue-grey-2"
            :disable="org.catOrganismoId === user.catOrganismoId"
          >
            <q-item-section>
              <q-item-label>{{ org.nombreOficial }}</q-item-label>
            </q-item-section>
            <q-item-section avatar>
              <q-icon
                :name="
                  org.catOrganismoId === user.catOrganismoId
                    ? 'mdi-check'
                    : 'mdi-arrow-right'
                "
                color="primary"
              >
                <q-tooltip>
                  {{
                    org.catOrganismoId === user.catOrganismoId
                      ? "Actual"
                      : "Cambiar"
                  }}
                </q-tooltip>
              </q-icon>
            </q-item-section>
          </q-item>
        </q-list>
      </q-card>
    </q-card-section>
  </q-card>
</template>

<script setup>
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { useQuasar } from "quasar";
import { manejoErrores } from "src/helpers/manejo-errores";

const $q = useQuasar();
const storeAuth = useAuthStore();
const { user, organismos } = storeAuth;
const emit = defineEmits(["cerrar"]);

function logout() {
  storeAuth.logoutUser();
}

async function guardaOrganismo(organismo) {
  $q.loading.show({
    message: "Cambiando de órgano jurisdiccional...",
  });

  try {
    emit("cerrar");
    await storeAuth.crearSesion(organismo);
    await storeAuth.cerrarSesion();
    location.reload();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  $q.loading.hide();
}
</script>
