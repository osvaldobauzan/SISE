<template>
  <q-layout view="lHh LpR lFf" class="bg-blue-grey-1">
    <q-header
      v-if="
        authStore.user?.catOrganismoId && authStore.user.privilegios?.length > 0
      "
      elevated
      class="bg-white text-grey-9 q-py-xs"
    >
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          @click="leftDrawerOpen = !leftDrawerOpen"
          aria-label="Menu"
          icon="mdi-menu"
        />

        <img
          src="images/sise3-logo_horizontal.png"
          alt=""
          class="q-ml-xs"
          style="height: 50px"
          v-if="$q.screen.gt.xs"
        />
        <q-space />
        <!-- TODO: falta implmentar por eso se oculta -->
        <div class="YL__toolbar-input-container row no-wrap">
          <q-select
            dense
            outlined
            clearable
            use-input
            v-cortarLabel
            input-debounce="0"
            class="col"
            label="Buscar un expediente *"
            option-value="asuntoNeunId"
            v-model="expedienteBuscado"
            :options="opcionesExpediente"
            :placeholder="
              !expedienteBuscado
                ? 'Ingresa el número o escanea el QR del expediente'
                : ''
            "
            @filter="buscarExpedientePorNumero"
            @update:model-value="expedienteEncontrado"
          >
            <template v-slot:append>
              <q-icon name="mdi-qrcode-scan" />
            </template>
            <template v-slot:option="scope">
              <q-item v-bind="scope.itemProps">
                <q-item-section>
                  <q-item-label>{{ scope.opt.asuntoAlias }}</q-item-label>
                  <q-item-label caption>{{
                    scope.opt.tipoAsunto
                  }}</q-item-label>
                  <q-item-label
                    class="text-caption"
                    v-if="scope.opt.tipoProcedimiento !== ''"
                    >{{ scope.opt.tipoProcedimiento }}</q-item-label
                  >
                </q-item-section>
              </q-item>
            </template>
          </q-select>
        </div>
        <q-btn
          no-caps
          color="grey-6"
          icon="mdi-account-search"
          :flat="!$q.screen.gt.sm"
          :outline="$q.screen.gt.sm"
          :round="!$q.screen.gt.sm"
          :label="$q.screen.gt.sm ? 'Buscar parte' : ''"
          class="q-px-md q-ml-md"
          @click="showBuscarParte = true"
        >
          <q-tooltip>Buscar parte</q-tooltip>
        </q-btn>
        <q-space></q-space>
        <div class="q-gutter-sm row items-center no-wrap">
          <q-btn
            v-permitido="58"
            round
            dense
            flat
            color="grey-8"
            icon="mdi-cog"
            to="configuracion"
            class="hidden"
          >
            <q-tooltip>Configuración</q-tooltip>
          </q-btn>
          <q-btn
            round
            dense
            flat
            color="grey-8"
            icon="mdi-bell"
            @click="rightDrawerOpen = !rightDrawerOpen"
          >
            <q-icon
              v-if="
                alertas.some(
                  (x) =>
                    x.EsAlertaSesion &&
                    (x.TipoProceso == 'Inicio' || x.TipoProceso == 'Avance'),
                )
              "
              class="loading"
              name="mdi-loading"
              color="light-blue"
              size="3rem"
              style="position: absolute"
            />
            <q-badge color="red" text-color="white" rounded floating>
              {{ alertas.length }}
            </q-badge>
            <q-tooltip>Alertas</q-tooltip>
          </q-btn>
          <q-item clickable v-ripple>
            <q-menu auto-close>
              <ContextProfile></ContextProfile>
            </q-menu>
            <q-item-section avatar top>
              <q-avatar size="md">
                <q-img src="images/user-icon.png"> </q-img>
              </q-avatar>
            </q-item-section>
            <q-item-section>
              <q-item-label>{{
                getName(authStore.user?.completo)
              }}</q-item-label>
              <q-item-label caption>{{
                authStore.user?.cargoDescripcion
              }}</q-item-label>
            </q-item-section>
          </q-item>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      mini
      show-if-above
      bordered
      v-model="leftDrawerOpen"
      v-if="
        authStore.user?.catOrganismoId && authStore.user.privilegios?.length > 0
      "
      class="text-grey bg-primary"
    >
      <div class="text-center q-ma-md">
        <img
          src="images/logotipoCJF_horizontal_blanco_mini.png"
          alt=""
          class="q-mt-sm"
          width="30"
        />
      </div>
      <q-list padding>
        <template v-for="link in menuStore.menu" :key="link.text">
          <q-item
            v-if="link.esSubmenu"
            clickable
            :active="link.subMenu.some((x) => x.isActive)"
            active-class="text-white border_selected"
            v-permitido="link.subMenu.map((x) => x.privilegio)"
            :class="link.isVisible ? '' : 'hidden'"
          >
            <q-menu
              anchor="top right"
              self="top left"
              class="bg-primary text-grey"
              :offset="[12, 0]"
            >
              <q-list>
                <q-item
                  clickable
                  v-for="submenu in link.subMenu"
                  :key="submenu.text"
                  :active="submenu.isActive"
                  active-class="text-white border_selected"
                  @click="setActive(submenu)"
                  manual-focus
                  v-permitido="submenu.privilegio"
                >
                  <q-tooltip
                    v-if="$q.screen.gt.sm"
                    anchor="center right"
                    self="center left"
                    class="bg-primary"
                    transition-show="jump-right"
                    transition-hide="jump-left"
                    >{{ submenu.text }}</q-tooltip
                  >
                  <q-item-section avatar>
                    <q-icon
                      :name="
                        submenu.isActive ? submenu.icon_selected : submenu.icon
                      "
                    />
                  </q-item-section>
                  <q-item-section>
                    <q-item-label>{{ submenu.text }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-menu>
            <q-tooltip
              v-if="$q.screen.gt.sm"
              anchor="center right"
              self="center left"
              class="bg-primary"
              transition-show="jump-right"
              transition-hide="jump-left"
              >{{ link.text }}</q-tooltip
            >
            <q-item-section avatar>
              <q-icon :name="link.isActive ? link.icon_selected : link.icon" />
            </q-item-section>
            <q-item-section>
              <q-item-label>{{ link.text }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item
            v-else
            clickable
            :active="link.isActive"
            active-class="text-white border_selected"
            @click="setActive(link)"
            v-permitido="link.privilegio"
            :class="link.isVisible ? '' : 'hidden'"
          >
            <q-tooltip
              v-if="$q.screen.gt.sm"
              anchor="center right"
              self="center left"
              class="bg-primary"
              transition-show="jump-right"
              transition-hide="jump-left"
              >{{ link.text }}</q-tooltip
            >
            <q-item-section avatar>
              <q-icon :name="link.isActive ? link.icon_selected : link.icon" />
            </q-item-section>
            <q-item-section>
              <q-item-label>{{ link.text }}</q-item-label>
            </q-item-section>
          </q-item>
        </template>
        <q-separator spaced></q-separator>
        <q-separator spaced></q-separator>
        <q-separator spaced></q-separator>
        <q-item v-ripple clickable @click="authStore.logoutUser">
          <q-item-section avatar>
            <q-icon name="mdi-logout" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Cerrar sesión</q-item-label>
          </q-item-section>
          <q-tooltip
            v-if="$q.screen.gt.sm"
            anchor="center right"
            self="center left"
            class="bg-primary"
            transition-show="jump-right"
            transition-hide="jump-left"
            >Cerrar sesión</q-tooltip
          >
        </q-item>
      </q-list>
    </q-drawer>
    <q-drawer
      overlay
      side="right"
      v-model="rightDrawerOpen"
      v-if="
        authStore.user?.catOrganismoId && authStore.user.privilegios?.length > 0
      "
    >
      <q-toolbar class="bg-primary text-white">
        <q-toolbar-title>Alertas</q-toolbar-title>
        <q-btn
          flat
          round
          dense
          icon="mdi-close"
          @click="rightDrawerOpen = !rightDrawerOpen"
        />
      </q-toolbar>
      <q-list class="q-pa-md q-gutter-md">
        <q-card v-for="(item, index) in alertas" :key="index">
          <q-item
            v-if="item.EsAlertaSesion"
            @mouseover="item.showDelete = true"
            @mouseleave="item.showDelete = false"
          >
            <q-item-section>
              <div class="row q-mb-sm">
                <q-item-label class="q-mb-lg" caption>
                  {{ item.Origen }}</q-item-label
                >
                <q-space></q-space>
                <q-btn
                  v-if="item.showDelete"
                  @click="
                    alertas.splice(index, 1);
                    alertasSesion.splice(
                      alertasSesion.findIndex((a) => a.Id == item.Id),
                      1,
                    );
                  "
                  dense
                  outline
                  round
                  color="red-4"
                  icon="mdi-close"
                  size="6px"
                ></q-btn>
                <q-item-label caption v-else>{{
                  date.formatDate(item.FechaHora, "DD/MM/YYYY HH:mm")
                }}</q-item-label>
              </div>
              <q-item-label class="text-primary text-bold">
                <q-icon
                  v-if="item.TipoProceso == 'Fin'"
                  name="mdi-check-circle-outline"
                  color="green-6"
                  size="6px"
                />
                <q-icon
                  v-else-if="item.TipoProceso == 'Error'"
                  name="mdi-alert-outline"
                  color="negative"
                  size="6px"
                />
                <q-icon
                  v-else
                  class="loading"
                  name="mdi-loading"
                  color="secondary"
                  size="6px"
                />
                {{ item.Titulo }}</q-item-label
              >
              <q-item-label class="q-mb-lg" caption>
                {{ item.Contenido }}</q-item-label
              >
            </q-item-section>
          </q-item>
          <q-item
            v-else
            @mouseover="item.showDelete = true"
            @mouseleave="item.showDelete = false"
          >
            <q-item-section>
              <div class="row q-mb-sm">
                <q-item-label class="q-pb-sm" caption>
                  {{ item.emisor }}</q-item-label
                >
                <q-space></q-space>
                <div v-if="item.showDelete">
                  <q-btn
                    @click="eliminarAlerta(item.id)"
                    dense
                    outline
                    round
                    color="red-4"
                    icon="mdi-close"
                    size="xs"
                  ></q-btn>
                </div>
                <q-item-label caption v-else>{{
                  date.formatDate(item.horaDeLaAlerta, "DD/MM/YYYY HH:mm")
                }}</q-item-label>
              </div>
              <q-item-label class="text-primary text-bold">
                {{ item.mensaje }}</q-item-label
              >
              <q-item-label caption> {{ item.parte }}</q-item-label>
              <div class="row q-mt-sm">
                <q-item-label caption class="text-bold">{{
                  item.receptor
                }}</q-item-label>
                <q-space></q-space>
                <q-item-label caption> {{ item.estado }}</q-item-label>
              </div>
            </q-item-section>
          </q-item>
        </q-card>
      </q-list>
      <q-inner-loading :showing="cargandoAlertas" />
    </q-drawer>
    <q-page-container>
      <router-view />
    </q-page-container>
    <q-footer class="text-grey-8 bg-white">
      <q-banner dense align="right">
        <q-item-label>
          {{ authStore.user?.nombreOficial }}
        </q-item-label>
      </q-banner>
    </q-footer>
  </q-layout>
  <q-dialog v-model="showBuscarParte" :maximized="maximizedToggle">
    <ModalWindowComponent
      :maximizedToggle="maximizedToggle"
      @toggle-maximized="maximizedToggle = !maximizedToggle"
    >
      <BuscarParteComponent></BuscarParteComponent>
    </ModalWindowComponent>
  </q-dialog>
  <q-dialog v-model="showExpediente" :maximized="maximizedToggle">
    <ModalWindowComponent
      :maximizedToggle="maximizedToggle"
      @toggle-maximized="maximizedToggle = !maximizedToggle"
    >
      <ExpedientePage
        :asuntoNeunId="buscarExpediente.asuntoNeunId"
        :asuntoAlias="buscarExpediente.asuntoAlias"
        :tipoAsunto="buscarExpediente.tipoAsunto"
      />
    </ModalWindowComponent>
  </q-dialog>
</template>

<script setup>
import { ref, onMounted, watch, onBeforeUnmount } from "vue";
import { useRouter } from "vue-router";
import { useMenuStore } from "src/stores/menu-store";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { useAlertasStore } from "src/stores/alertas-store";
import { date } from "quasar";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import { useExpedienteElectronicoStore } from "src/modules/expediente/stores/expediente-electronico-store";
import { Validaciones } from "src/helpers/validaciones";

import _ from "lodash";
import ContextProfile from "components/menuContextProfile.vue";
import BuscarParteComponent from "src/modules/buscarParte/components/BuscarParteComponent.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";

const expedienteElectronicoStore = useExpedienteElectronicoStore();
const maximizedToggle = ref(false);

const connection = new HubConnectionBuilder()
  .withUrl(`${process.env.API_ALERTAS}api`, {
    withCredentials: false,
    headers: {
      "Access-Control-Allow-Origin": "*",
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  })
  .build();

const authStore = useAuthStore();
const alertasStore = useAlertasStore();
const alertas = ref([]);
const alertasSesion = ref([]);
const cargandoAlertas = ref(false);
let registration = null;
const showBuscarParte = ref(false);
const opcionesExpediente = ref([]);
const showExpediente = ref(false);
const buscarExpediente = ref(null);

function isJsonString(str) {
  try {
    JSON.parse(str);
  } catch (e) {
    return false;
  }
  return true;
}

async function buscarExpedientePorNumero(val, update, abort) {
  update(
    async () => {
      if (val.length > 10 && isJsonString(val)) {
        val = JSON.parse(val).E.N;
        val = { asuntoNeunId: val };
        expedienteEncontrado(val);
        return;
      }

      if (
        val === "" ||
        val.length <= 5 ||
        typeof Validaciones.validaNoExpediente(val) === "string"
      ) {
        abort();
        return;
      } else {
        try {
          await expedienteElectronicoStore.buscarExpediente(val, null);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }
        opcionesExpediente.value = _.sortBy(
          expedienteElectronicoStore.expedientes,
          ["tipoAsunto"],
        );
        if (opcionesExpediente.value?.length === 1) {
          buscarExpediente.value = opcionesExpediente.value[0];
        }
      }
    },
    // "ref" is the Vue reference to the QSelect
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 700),
  );
}

function expedienteEncontrado(expediente) {
  buscarExpediente.value = expediente;
  expedienteBuscado.value = "";
  opcionesExpediente.value = [];
  expedienteElectronicoStore.resetExpedientes();
  showExpediente.value = true;
}

const getName = (name) => {
  return name.split(" ")[0] + " " + name.split(" ")[1];
};

const stopWatch = watch(
  () => authStore.user?.catOrganismoId,
  // eslint-disable-next-line no-unused-vars
  async (_newValue) => {
    if (_newValue && _newValue > 0) {
      cargandoAlertas.value = true;
      try {
        await alertasStore.obtenerAlertasPorUsuario();
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
      connection.start().then(async () => {
        await alertasStore.postConexion(connection.connectionId);
        connection.on("newMessage", async (message) => {
          if (message.EsAlertaSesion) {
            if (alertasSesion.value?.some((x) => x.Id == message.Id)) {
              const index = alertasSesion.value?.findIndex(
                (x) => x.Id == message.Id,
              );
              alertasSesion.value.splice(index, 1);
            }
            alertasSesion.value?.push({
              ...message,
              horaDeLaAlerta: message.FechaHora,
            });
          } else {
            window.Notification.requestPermission().then(async (perm) => {
              if (perm === "granted") {
                await registration.showNotification(message.Estado || "", {
                  body: message.Mensaje,
                  data: message,
                  icon: "icons/favicon-32x32.png",
                });
              }
            });
          }
          try {
            await alertasStore.obtenerAlertasPorUsuario();
          } catch (error) {
            manejoErrores.mostrarError(error);
          }
        });
      });
      cargandoAlertas.value = false;
    }
  },
  {
    immediate: true,
  },
);

const stopWatchAlertas = watch(
  () => alertasStore.alertas,
  // eslint-disable-next-line no-unused-vars
  async (_newValue) => {
    alertas.value =
      _newValue?.concat(alertasSesion.value).sort((a, b) => {
        return new Date(b.horaDeLaAlerta) - new Date(a.horaDeLaAlerta);
      }) || [];
  },
  {
    immediate: true,
  },
);
//const signalr = useSignalR();
onBeforeUnmount(async () => {
  stopWatch();
  stopWatchAlertas();
  if (connection.state === HubConnectionState.Connected)
    await connection.stop();
});

const menuStore = useMenuStore();
const leftDrawerOpen = ref(false);
const rightDrawerOpen = ref(false);
const router = new useRouter();
const expedienteBuscado = ref("");

function setActive(selectedLink) {
  router.replace(
    selectedLink.link.length > 1 ? `/${selectedLink.link}` : selectedLink.link,
  );
}
onMounted(async () => {
  await Notification.requestPermission();
  registration = await navigator.serviceWorker.register("sw.js");
});
async function eliminarAlerta(id) {
  cargandoAlertas.value = true;
  try {
    await alertasStore.eliminarAlerta(id);
    await alertasStore.obtenerAlertasPorUsuario();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoAlertas.value = false;
}
</script>
<style lang="css">
.loading {
  animation: lds-ring 1.2s cubic-bezier(1, 1, 1, 1) infinite;
}

@keyframes lds-ring {
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(360deg);
  }
}
</style>
<style lang="sass" scoped>
.border_selected
  border-left: 3px solid #fff
.YL
  &__toolbar-input-container
    min-width: 100px
    width: 40%
  &__toolbar-input-btn
    border-radius: 0
    border-style: solid
    border-width: 1px 1px 1px 0
    border-color: rgba(0,0,0,.24)
    max-width: 60px
    width: 100%
  &__drawer-footer-link
    color: inherit
    text-decoration: none
    font-weight: 500
    font-size: .75rem
  &:hover
    color: #000
</style>
