import { defineStore } from "pinia";

const DEFAULT_TAB_OFICIALIA = "oficialia";

export const useOficialiaTabStore = defineStore("oficialiaTabStore", {
  state: () => ({
    tabsOficialia: [],
    tabActive: DEFAULT_TAB_OFICIALIA,
  }),
  actions: {
    addTabExpediente(expediente) {
      expediente.id = expediente.expediente.asuntoNeunId;
      if (
        this.tabsOficialia.filter(
          (tab) => tab.id === expediente.expediente.asuntoNeunId,
        ).length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }

      const exp = {
        ...expediente.expediente,
        id: expediente.id,
        nombreOrigen:
          expediente.nombreOrigen || expediente.origenPromocionDescripcion,
        tipoAsunto: expediente.expediente.catTipoAsunto,
        cuaderno:
          expediente.expediente.nombreCorto || expediente.cuadernoNombre,
        cuadernoId: expediente.cuadernoId,
      };

      this.tabsOficialia.push(exp);
      this.tabActive = expediente.id;
    },

    delTabExpediente(index) {
      const copyTabs = [...this.tabsOficialia];
      this.tabsOficialia.splice(index, 1);
      if (copyTabs[index].id == this.tabActive) {
        if (this.tabsOficialia.length == 0) {
          this.tabActive = DEFAULT_TAB_OFICIALIA;
        } else {
          if (this.tabsOficialia.length - 1 >= index) {
            this.tabActive = this.tabsOficialia[index].id;
          } else {
            this.tabActive =
              this.tabsOficialia[this.tabsOficialia.length - 1].id;
          }
        }
      }
    },
    setTabActive(tab) {
      this.tabActive = tab;
    },
  },
});
