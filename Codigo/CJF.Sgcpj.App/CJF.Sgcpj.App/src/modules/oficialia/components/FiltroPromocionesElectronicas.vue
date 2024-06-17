<template>
  <div class="q-gutter-sm q-mt-xs text-center">
    <q-btn
      flat
      dense
      no-caps
      label="Pendientes"
      class="bg-blue-5 text-white q-px-md"
    >
    </q-btn>

    <q-btn
      flat
      dense
      no-caps
      size="md"
      label="Promociones Electrónicas"
      class="bg-blue-2 q-px-md"
      @click="filterRows('JUICIO EN LÍNEA', 'Promoción Electrónica')"
    >
      <q-badge
        class="q-ml-sm text-bold"
        :color="
          parseInt(PanelElectronicas['Promociones Electrónicas']) > 0
            ? 'negative'
            : 'primary'
        "
        >{{ PanelElectronicas["Promociones Electrónicas"] }}</q-badge
      >
    </q-btn>
    <q-btn
      flat
      dense
      no-caps
      size="md"
      label="Demandas Electrónicas"
      class="bg-blue-2 q-px-md"
      @click="filterRows('JUICIO EN LÍNEA', 'Demanda Electrónica')"
    >
      <q-badge
        class="q-ml-sm text-bold"
        :color="
          parseInt(PanelElectronicas['Demandas Electrónicas']) > 0
            ? 'negative'
            : 'primary'
        "
        >{{ PanelElectronicas["Demandas Electrónicas"] }}</q-badge
      >
    </q-btn>
    <q-btn
      flat
      dense
      no-caps
      size="md"
      label="Comunicaciones Electrónicas"
      class="bg-blue-2 q-px-md"
      @click="filterRows('OCC', 'Comunicación Oficial')"
    >
      <q-badge
        class="q-ml-sm text-bold"
        :color="
          parseInt(PanelElectronicas['Comunicaciones Electrónicas']) > 0
            ? 'negative'
            : 'primary'
        "
        >{{ PanelElectronicas["Comunicaciones Electrónicas"] }}</q-badge
      >
    </q-btn>
    <q-btn
      flat
      dense
      no-caps
      size="md"
      label="Promociones Interconexión IOJ"
      class="bg-blue-2 q-px-md"
      @click="filterRows('INTERCONEXIÓN OJ', '')"
    >
      <q-badge
        class="q-ml-sm text-bold"
        :color="
          parseInt(PanelElectronicas['Promociones Interconexión IOJ']) > 0
            ? 'negative'
            : 'primary'
        "
        >{{ PanelElectronicas["Promociones Interconexión IOJ"] }}</q-badge
      >
    </q-btn>
    <q-btn
      flat
      dense
      no-caps
      size="md"
      label="Promociones Interconexión"
      class="bg-blue-2 q-px-md"
      @click="filterRows('INTERCONEXIÓN', '')"
    >
      <q-badge
        class="q-ml-sm text-bold"
        :color="
          parseInt(PanelElectronicas['Promociones Interconexión']) > 0
            ? 'negative'
            : 'primary'
        "
        >{{ PanelElectronicas["Promociones Interconexión"] }}</q-badge
      >
    </q-btn>
    <q-btn
      flat
      dense
      no-caps
      size="md"
      label="Recursos o Demandas OCC"
      class="bg-blue-2 q-px-md"
      @click="filterRows('OCC', 'Demanda Electrónica')"
    >
      <q-badge
        class="q-ml-sm text-bold"
        :color="
          parseInt(PanelElectronicas['Recursos o Demandas OCC']) > 0
            ? 'negative'
            : 'primary'
        "
        >{{ PanelElectronicas["Recursos o Demandas OCC"] }}</q-badge
      >
    </q-btn>
    <q-btn
      flat
      dense
      no-caps
      size="md"
      label="Promociones OCC"
      class="bg-blue-2 q-px-md"
      @click="filterRows('OCC', 'Promoción Electrónica')"
    >
      <q-badge
        class="q-ml-sm text-bold"
        :color="
          parseInt(PanelElectronicas['Promociones OCC']) > 0
            ? 'negative'
            : 'primary'
        "
        >{{ PanelElectronicas["Promociones OCC"] }}</q-badge
      >
    </q-btn>
  </div>
</template>

<script setup>
</script>
