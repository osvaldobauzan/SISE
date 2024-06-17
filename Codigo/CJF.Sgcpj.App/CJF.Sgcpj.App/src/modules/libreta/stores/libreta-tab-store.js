import { defineStore } from "pinia";

const DEFAULT_TAB_LIBRETA = "libreta";

export const useLibretaTabStore = defineStore("libretaTabStore", {
  state: () => ({
    tabsLibreta: [],
    tabActive: DEFAULT_TAB_LIBRETA,
  }),
  actions: {
    addTabExpediente(expediente) {
      expediente.id = expediente.asuntoNeunId;
      if (
        this.tabsLibreta.filter((tab) => tab.id === expediente.asuntoNeunId)
          .length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }

      this.tabsLibreta.push(expediente);
      this.tabActive = expediente.id;
    },

    delTabExpediente(index) {
      this.tabsLibreta.splice(index, 1);
      this.tabActive = DEFAULT_TAB_LIBRETA;
    },
  },
});
