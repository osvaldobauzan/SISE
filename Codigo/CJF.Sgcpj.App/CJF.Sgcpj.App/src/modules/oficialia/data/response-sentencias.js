import { ref } from "vue";
export class ResponseSentencias {
  constructor() {
    this.datos.value = [];
  }
  datos = ref([]);
  totalRegistros = 0;
}
