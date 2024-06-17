<template>
  <div dense class="col doc-note doc-note--tip-gray">
    <div class="q-my-md">
      <span class="text-weight-bold">Detalle de la promoción</span>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Número de expediente</div>
      <div class="col-6">{{ value?.numeroExpediente }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Tipo de asunto</div>
      <div class="col-6">{{ value?.tipoAsunto?.tipoAsunto }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Cuaderno</div>
      <div class="col-6">{{ value?.cuaderno?.cuaderno }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Fecha de presentación</div>
      <div class="col-6">{{ value?.fechaPresentacion }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Origen</div>
      <div class="col-6">{{ value?.origenDescripcion }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Número de promoción</div>
      <div class="col-6">{{ value?.registro }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Contenido</div>
      <div class="col-6">{{ value?.contenido?.descripcion }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Copias</div>
      <div class="col-6">{{ value?.copias }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Fojas</div>
      <div class="col-6">{{ value?.fojas }}</div>
    </div>
    <div class="row text-body2 q-my-md">
      <div class="col-6 primary">Secretario asignado</div>
      <div class="col-6">{{ value?.secretario?.completo }}</div>
    </div>
    <div class="q-my-md">
      <span class="text-weight-bold">Promovente</span>
    </div>
    <div
      class="row text-body2 q-my-md"
      v-if="value?.tipoPromovente === 'promovente'"
    >
      <div class="col-6 primary">
        {{ value?.tipoPromoventeCat?.descripcion }}
      </div>
      <div class="col-6">{{ value?.getNombrePromoventeCompleto() }}</div>
    </div>
    <div class="q-my-md" v-if="value?.tipoPromovente === 'promovente'">
      <span class="text-weight-bold">Parte asociada</span>
    </div>
    <div
      class="row text-body2 q-my-md"
      v-if="value?.tipoPromovente === 'promovente'"
    >
      <div class="col-6 primary">
        {{ value?.parteCatTipoPersonaCaracter?.caracterPersona }}
      </div>
      <div class="col-6">{{ value?.getNombreParteNombreCompleto() }}</div>
    </div>
    <template
      v-if="
        value?.tipoPromovente === 'parte' &&
        value?.tipoParte !== 'parteExistente'
      "
    >
      <div class="row text-body2 q-my-md">
        <div class="col-6 primary">
          {{ value?.parteCatTipoPersonaCaracter?.caracterPersona }}
        </div>
        <div class="col-6">{{ value?.getNombreParteNombreCompleto() }}</div>
      </div>
    </template>
    <template v-else>
      <div class="row text-body2 q-my-md">
        <div class="col-6 primary">
          {{ value?.parteCatTipoPersonaCaracter?.descripcionCaracterPersona }}
        </div>
        <div class="col-6">
          {{ value?.parteCatTipoPersonaCaracter?.nombre }}
          {{ value?.parteCatTipoPersonaCaracter?.aPaterno }}
          {{ value?.parteCatTipoPersonaCaracter?.aMaterno }}
        </div>
      </div>
    </template>
    <div
      class="row text-body2 q-my-md"
      v-if="value?.tipoPromovente === 'autoridad'"
    >
      <div class="col-6 primary">Autoridad judicial</div>
      <div class="col-6">{{ value?.promoventeAutoridad?.nombreCompleto }}</div>
    </div>
  </div>
</template>
<script setup>
import { computed } from "vue";
import { FormPromocion } from "../data/form-promocion";
// eslint-disable-next-line no-unused-vars
const props = defineProps({
  detallePromocion: {
    type: FormPromocion,
    default: new FormPromocion(),
  },
});

const value = computed({
  get() {
    return props.detallePromocion;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});
</script>
<style scoped lang="css">
.primary {
  color: #24135f;
}

.doc-note {
  font-size: 16px;
  border-radius: 4px;
  margin: 16px 0;
  padding: 16px;
  border-width: 1px;
  border-style: solid;
}

.doc-note--tip-gray {
  background-color: #eceff1;
  border-color: #eceff1;
}
</style>
