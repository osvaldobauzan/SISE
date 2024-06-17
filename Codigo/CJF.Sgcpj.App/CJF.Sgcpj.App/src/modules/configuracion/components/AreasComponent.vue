<template>
  <div>
    <q-toolbar>
      <q-space></q-space>
      <q-btn
        unelevate
        icon="add"
        label="Agregar"
        @click="showAgregarArea = true"
        color="primary"
      />
    </q-toolbar>
    <q-list bordered>
      <div class="row">
        <div class="col">
          <q-item>
            <q-item-section>
              <q-tree
                selected-color="bg-grey text-primary"
                v-model:selected="selected"
                default-expand-all
                :nodes="areas"
                node-key="label"
              />
            </q-item-section>
          </q-item>
        </div>
        <div class="col">
          <q-card flat>
            <q-toolbar>
              <q-toolbar-title>{{ selected }}</q-toolbar-title>
            </q-toolbar>
            <q-card-section>
              <q-item>
                <q-item-section>
                  <q-item-label></q-item-label>
                </q-item-section>
              </q-item>
            </q-card-section>
          </q-card>
        </div>
      </div>
    </q-list>
  </div>
  <q-dialog v-model="showAgregarArea">
    <AgregarZona />
  </q-dialog>
</template>

<script setup>
import { ref } from "vue";
import AgregarZona from "./AgregarZona.vue";

const selected = ref("Titular");
const showAgregarArea = ref(false);
const areas = [
  {
    label: "Titular",
    avatar: "images/user-icon.png",
    value: "Al Rashid Martínez",
    children: [
      {
        label: "Oficialía de partes",
        icon: "mdi-inbox-outline",
      },
      {
        label: "Trámite",
        icon: "mdi-file-cog-outline",
        children: [
          { label: "Mesa I" },
          { label: "Mesa II" },
          { label: "Mesa III" },
          { label: "Mesa IV" },
        ],
      },
      {
        label: "Actuaría",
        icon: "mdi-email-outline",
        children: [
          { label: "Coordinador" },
          { label: "Actuario 1" },
          { label: "Actuario 2" },
          { label: "Actuario 3" },
          { label: "Actuario 4" },
        ],
      },
      {
        label: "Sentencias",
        icon: "mdi-book-outline",
        children: [
          {
            label: "Secretario proyectista 1",
          },
          { label: "Secretario proyectista 2" },
        ],
      },
    ],
  },
];
</script>

<style scope>
.show {
  opacity: 1;
}

.mouse_over {
  opacity: 0;
}

.mouse_over:hover {
  opacity: 1;
}
</style>
