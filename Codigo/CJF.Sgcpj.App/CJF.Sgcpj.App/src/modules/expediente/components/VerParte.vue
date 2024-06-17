<template>
  <q-list separator class="q-mb-md">
    <q-item
      dense
      v-ripple
      clickable
      v-for="parte in partesExpediente"
      :key="parte.catCaracterPersonaAsuntoId"
    >
      <q-item-section
        avatar
        class="q-pl-md"
        @click="emit('verdetalle:event', parte)"
      >
        <q-icon color="grey-7" :name="parte.icon" />
      </q-item-section>
      <q-item-section @click="emit('verdetalle:event', parte)">
        <q-item-label caption>{{
          parte.descripcionCaracterPersona
        }}</q-item-label>
        <q-item-label v-if="parte.tipo == 2 || parte.tipo == 3">{{
          parte.denominacionDeAutoridad || parte.nombre
        }}</q-item-label>
        <q-item-label v-else>{{
          `${parte?.nombre} ${parte?.aPaterno} ${parte?.aMaterno}`
        }}</q-item-label>
      </q-item-section>
      <q-item-section side>
        <div class="row">
          <q-btn
            flat
            round
            size="sm"
            color="blue"
            icon="mdi-file-edit-outline"
            @click="emit('update:parte', parte)"
          >
            <q-tooltip> Editar Parte </q-tooltip>
          </q-btn>
          <q-btn
            flat
            round
            size="sm"
            icon="mdi-delete-outline"
            color="negative"
            @click="emit('delete:parte', parte)"
          >
            <q-tooltip> Eliminar Parte </q-tooltip>
          </q-btn>
        </div>
      </q-item-section>
    </q-item>
    <!-- <q-item v-if="partesRestantes > 0">
      <q-item-section avatar class="q-pl-md">
        <q-icon color="grey-7" :name="'persona'" />
      </q-item-section>
      <q-item-section>
        <q-item-label caption>{{ "Otras partes" }}</q-item-label>
        <q-item-label>{{ "+ " + partesRestantes }}</q-item-label>
      </q-item-section>
    </q-item> -->
  </q-list>
</template>

<script setup>
// import { computed } from "vue";
defineProps({
  partesExpediente: {
    type: Object,
    default: () => ({}),
  },
});

// const solo2Partes = computed(() => {
//   let array = [];
//   let fisica = false;
//   if (props.partesExpediente.find((p) => p.tipo == 1) && !fisica) {
//     array.push(props.partesExpediente.find((p) => p.tipo == 1));
//     fisica = true;
//   }
//   if (props.partesExpediente.find((p) => p.tipo == 2) && !fisica) {
//     array.push(props.partesExpediente.find((p) => p.tipo == 2));
//     fisica = true;
//   }
//   if (props.partesExpediente.find((p) => p.tipo == 3)) {
//     array.push(props.partesExpediente.find((p) => p.tipo == 3));
//   }
//   if (array.length < 2 && props.partesExpediente.length > 1) {
//     props.partesExpediente.forEach((e) => {
//       if (!array.some((p) => p.personaId == e.personaId) && array.length < 2) {
//         array.push(e);
//         return array;
//       }
//     });
//   }
//   return array;
// });

// const partesRestantes = computed(() => {
//   return props.partesExpediente.length - 2;
// });

const emit = defineEmits({
  "update:parte": (value) => value,
  "delete:parte": (value) => value,
  "verdetalle:event": (value) => value,
});
</script>
