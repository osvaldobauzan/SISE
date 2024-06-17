import { defineStore } from "pinia";

const DEFAULT_TAB_EJECUCION = "ejecucion";

export const useEjecucionTabStore = defineStore("EjecucionTabStore", {
  state: () => ({
    tabsEjecucion: [],
    tabActive: DEFAULT_TAB_EJECUCION,
  }),
  actions: {
    addTabExpediente(expediente) {
      expediente.id = expediente.AsuntoNeunId;
      if (
        this.tabsEjecucion.filter((tab) => tab.id === expediente.AsuntoNeunId)
          .length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }

      this.tabsEjecucion.push(expediente);
      this.tabActive = expediente.id;
    },

    delTabExpediente(index) {
      this.tabsEjecucion.splice(index, 1);
      this.tabActive = DEFAULT_TAB_EJECUCION;
    },
  },
});
