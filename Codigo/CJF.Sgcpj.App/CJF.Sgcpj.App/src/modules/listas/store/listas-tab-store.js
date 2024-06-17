import { defineStore } from "pinia";

const DEFAULT_TAB_LISTAS = "listas";

export const useListasTabStore = defineStore("ListasTabStore", {
  state: () => ({
    tabsListas: [],
    tabActive: DEFAULT_TAB_LISTAS,
  }),
  actions: {
    addTabExpediente(expediente) {
      expediente.id = expediente.AsuntoNeunId;
      if (
        this.tabsLista.filter((tab) => tab.id === expediente.AsuntoNeunId)
          .length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }

      this.tabsLista.push(expediente);
      this.tabActive = expediente.id;
    },

    delTabExpediente(index) {
      this.tabsLista.splice(index, 1);
      this.tabActive = DEFAULT_TAB_LISTAS;
    },
  },
});
