<template>
  <q-page padding>
    <div class="row flex flex-center">
      <div class="col flex-center" style="max-width: 400px">
        <q-card>
          <div class="flex flex-center">
            <q-img
              src="images/sise3-logo_horizontal.png"
              class="q-ma-lg"
              style="max-width: 300px"
            ></q-img>
          </div>
          <q-separator></q-separator>
          <q-card-section>
            <div class="text-h6">Iniciar sesión</div>
          </q-card-section>
          <q-card-section>
            <form @submit.prevent="onSubmit" class="q-gutter-md">
              <q-input
                autofocus
                v-model="credentials.username"
                label="Usuario"
                type="text"
                lazy-rules
              >
                <template v-slot:prepend>
                  <q-icon name="mdi-account" />
                </template>
              </q-input>
              <q-input
                autocomplete
                v-model="credentials.password"
                label="Contraseña"
                type="password"
                lazy-rules
              >
                <template v-slot:prepend>
                  <q-icon name="mdi-lock" />
                </template>
              </q-input>
              <q-card-actions>
                <q-space></q-space>
                <q-btn
                  unelevated
                  label="Ingresar"
                  color="primary"
                  @click="authStore.logIn"
                  :loading="isLoading"
                />
              </q-card-actions>
            </form>
          </q-card-section>
        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script setup>
import { ref, reactive } from "vue";
import { useQuasar } from "quasar";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import router from "../router";

const $q = useQuasar();
const authStore = useAuthStore();

const isLoading = ref(false);

const credentials = reactive({
  username: "",
  password: "",
});

async function onSubmit() {
  if (!credentials.username || !credentials.password) {
    $q.notify({
      color: "negative",
      icon: "warning",
      message: "Por favor ingresa tu correo electrónico y contraseña",
    });
    return;
  }
  isLoading.value = true;

  const result = await authStore.loginUser(credentials);
  if (result.status === 200) {
    router.push("/");
  }
  let mensaje = "";
  switch (result.code) {
    case "auth/wrong-password":
      mensaje = "La contraseña es incorrecta";
      break;
    case "auth/user-not-found":
      mensaje = "El usuario no existe";
      break;
    default:
      mensaje = `Ocurrió un error al iniciar sesión: ${result.message}`;
      break;
  }
  if (result.code) {
    $q.notify({
      color: "negative",
      icon: "warning",
      message: mensaje,
    });
  }
  isLoading.value = false;
}
</script>
