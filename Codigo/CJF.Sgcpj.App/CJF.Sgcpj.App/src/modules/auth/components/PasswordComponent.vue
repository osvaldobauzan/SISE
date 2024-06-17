<template>
  <div class="center">
    <q-badge :color="mayuscula ? 'positive' : 'negative'">
      Mayúscula
      <q-tooltip>Al menos una mayúscula</q-tooltip> </q-badge
    >&nbsp;
    <q-badge :color="minuscula ? 'positive' : 'negative'">
      Minúscula
      <q-tooltip>Al menos una minúscula</q-tooltip> </q-badge
    >&nbsp;
    <q-badge :color="numero ? 'positive' : 'negative'">
      Número
      <q-tooltip>Al menos un número</q-tooltip> </q-badge
    >&nbsp;
    <q-badge :color="simbolo ? 'positive' : 'negative'">
      Símbolo
      <q-tooltip>Al menos un símbolo</q-tooltip> </q-badge
    >&nbsp;
    <q-badge :color="caracteres ? 'positive' : 'negative'">
      > 6
      <q-tooltip>Al menos 6 caracteres</q-tooltip> </q-badge
    >&nbsp;
    {{ password }}
  </div>
</template>
<style scooped>
.center {
  text-align: center;
}
</style>
<script>
import { ref } from "vue";

export default {
  props: {
    password: {
      type: String,
      default: "xxx",
    },
  },
  setup() {
    let mayuscula = ref(false);
    let minuscula = ref(false);
    let numero = ref(false);
    let simbolo = ref(false);
    let caracteres = ref(false);

    function onPasswordChange() {
      const regxs = {
        lower: /[a-z]/,
        upper: /[A-Z]/,
        number: /[0-9]/,
        symbol: /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/,
      };
      minuscula.value = !!regxs.lower.test(props.password.trim());
      mayuscula.value = !!regxs.upper.test(props.password.trim());
      numero.value = !!regxs.number.test(props.password.trim());
      simbolo.value = !!regxs.symbol.test(props.password.trim());
      caracteres.value = !!(props.password.trim().length > 5);
    }

    return {
      mayuscula,
      minuscula,
      numero,
      simbolo,
      caracteres,
      onPasswordChange,
    };
  },
};
</script>
