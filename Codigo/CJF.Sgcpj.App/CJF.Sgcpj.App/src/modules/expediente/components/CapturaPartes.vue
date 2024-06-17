<template>
  <template v-if="showPanelPartes">
    <div class="conteiner">
      <q-card flat bordered>
        <q-toolbar style="display: grid">
          <q-toolbar-title class="space-title">
            <span class="title">Partes</span>
            <span class="sub-title"
              >Selecciona las partes a las cuáles asignarás información</span
            >
          </q-toolbar-title>
          <q-space></q-space>
          <div style="width: 75vw; padding-top: 0rem">
            <q-list separator>
              <q-item tag="label" v-ripple>
                <q-item-section side top>
                  <q-checkbox v-model="selectAllChecked" @click="selectAll" />
                </q-item-section>

                <q-item-section>
                  <q-item-label>Seleccionar todos</q-item-label>
                </q-item-section>
              </q-item>

              <q-item
                tag="label"
                v-ripple
                v-for="(item, index) in partesList"
                :key="index"
              >
                <q-item-section side top>
                  <q-checkbox
                    v-model="selectionPartes"
                    :val="item.Parte"
                    @update:model-value="updateSelection"
                  />
                </q-item-section>

                <q-item-section>
                  <q-item-label>{{ item.Parte }}</q-item-label>
                  <q-item-label caption>{{ item.tipoPersona }}</q-item-label>
                </q-item-section>

                <q-item-section>
                  <q-item-label>{{ item.status }}</q-item-label>
                  <q-item-label caption>{{ item.administracion }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </div>
        </q-toolbar>
        <div class="btn-contenido">
          <q-btn
            color="primary"
            label="Continuar"
            @click="showPanelPartes = false"
            :disabled="hasSelection"
          />
        </div>
      </q-card>
    </div>
  </template>
  <template v-else>
    <div class="contenido-btn-volver">
      <q-btn flat round color="primary" icon="mdi-menu">
        <q-menu>
          <q-list style="min-width: 100px">
            <q-item
              clickable
              v-close-popup
              v-for="(item, index) in listaDetalle"
              :key="index"
              @click="selectionMenu = item.value"
            >
              <q-item-section>{{ item.title }}</q-item-section>
            </q-item>
          </q-list>
        </q-menu>
      </q-btn>
      <span>Selecciona una sección</span>
    </div>
    <div class="row contenido-formulario">
      <!-- <div class="col-3">
        <div style="padding-top: 0rem">
          <q-list separator>
            <q-item
              tag="label"
              v-ripple
              v-for="(item, index) in listaDetalle"
              :key="index"
              :class="selectionMenu === item.value ? 'menu-item-select' : ''"
              @click="selectionMenu = item.value"
            >
              <q-item-section>
                <q-item-label>{{ item.title }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </div>
      </div> -->
      <div class="col contenido-secciones">
        <FormDatosGenerales v-if="selectionMenu === 1" />
        <template v-else>
          <h1>Pantalla en construccion</h1>
        </template>
      </div>
      <div class="col-3 contenido-partes">
        <div class="row contenido-partes-header">
          <div class="col-2">
            <p class="title-contenido-partes">Partes</p>
          </div>
          <div class="col btn-add">
            <q-btn
              flat
              icon="mdi-arrow-left"
              label="Seleccionar partes"
              @click="showPanelPartes = true"
            />
          </div>
        </div>
        <q-list>
          <q-item
            tag="label"
            v-ripple
            v-for="(item, index) in selectionPartes"
            :key="index"
          >
            <q-item-section>
              <q-item-label style="color: #24135f">{{ item }}</q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </div>
    </div>
  </template>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { lista, partesExpediente } from "../data/capturaPartes.js";
import FormDatosGenerales from "./FormDatosGenerales.vue";

const listaDetalle = lista;
const partesList = partesExpediente;
const selectionPartes = ref([]);
const selectionMenu = ref(1);
const showPanelPartes = ref(true);
const hasSelection = ref(true);
const selectAllChecked = ref(false);

const selectAll = () => {
  selectionPartes.value = selectAllChecked.value
    ? (selectionPartes.value = partesList.map((option) => option.Parte))
    : [];
  updateSelection();
};

const updateSelection = () => {
  hasSelection.value = !(selectionPartes.value.length > 0);
};

onMounted(() => {
  showPanelPartes.value = true;
});
</script>

<style scoped>
.contenido-header {
  background-color: #24135f;
  color: snow;
  padding: 0.1rem 2rem 1.2rem;
}
.header-subtitle {
  display: grid;
}
h2 {
  font-weight: bold;
}
.center-row {
  align-items: center;
}
/* Cards */
.conteiner {
  margin: 2rem;
}
.space-title {
  display: grid;
  color: black;
  padding-top: 1rem;
  padding-left: 1rem;
  padding-bottom: 1rem;
}
.title {
  font-size: 2rem;
  font-weight: bold;
}
.sub-title {
  font-size: 1rem;
}
.btn-contenido {
  padding-left: 1.5rem;
  padding-bottom: 1.5rem;
  margin-top: 1rem;
}
.contenido-btn-volver {
  background-color: #d9d9d9;
  display: flex;
  align-items: center;
  padding-left: 1.5rem;
  gap: 0.5rem;
  font-weight: bold;
  font-size: 0.9rem;
}
.contenido-formulario {
  margin-top: 1rem;
  padding-left: 1rem;
  padding-right: 1rem;
}
.contenido-partes {
  padding: 1rem;
  background-color: #f0f0f0;
  height: 60vh;
}
.title-contenido-partes {
  color: black;
  font-size: 1rem;
  margin-bottom: 0;
}
.contenido-partes-header {
  align-items: center;
}
.btn-add {
  text-align: end;
}
.menu-item-select {
  background-color: #f0f0f0;
}
.contenido-secciones {
  padding: 0rem 1rem;
  background-color: snow;
}
</style>
