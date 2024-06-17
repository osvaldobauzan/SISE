import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
const authStore = useAuthStore();
/**
 * example for use v-permitido="1" or v-permitido="[1,2]"
 */
export default {
  mounted(el, binding) {
    if (binding.value) {
      let mapPermisos = [];
      if (Array.isArray(binding.value)) {
        mapPermisos = [...binding.value];
      } else {
        mapPermisos.push(binding.value);
      }
      if (
        !authStore.user.privilegios?.some((x) =>
          mapPermisos.some((y) => y === x),
        )
      )
        el.parentNode.removeChild(el);
    }
  },
};
