import { defineStore } from "pinia";
import { apiActuaria } from "boot/axios";

export const useActuariaSintesisStore = defineStore("actuariaSintesisStore", {
  state: () => ({
    sintesis: "",
  }),
  actions: {
    async postSintesis(params) {
      await apiActuaria.post("api/actuaria/sintesis", params);
    },
    async putSintesis(params) {
      await apiActuaria.put("api/actuaria/sintesis", params);
    },
    async getSintesis(params) {
      const response = await apiActuaria.get("api/actuaria/sintesis", params);
      return response?.data;
    },
  },
});
