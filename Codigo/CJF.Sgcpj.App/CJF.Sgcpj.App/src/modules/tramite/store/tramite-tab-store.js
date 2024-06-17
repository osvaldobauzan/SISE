import { defineStore } from "pinia";

const DEFAULT_TAB_TRAMITE = "tramite";

export const useTramiteTabStore = defineStore("TramiteTabStore", {
  state: () => ({
    tabsTramite: [],
    tabActive: DEFAULT_TAB_TRAMITE,
  }),
  actions: {
    addTabExpediente(expediente) {
      expediente.id = expediente.expediente.asuntoNeunId;
      if (
        this.tabsTramite.filter(
          (tab) => tab.id === expediente.expediente.asuntoNeunId,
        ).length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }
      const exp = {
        ...expediente.expediente,
        id: expediente.id,
        nombreOrigen: expediente.nombreOrigen,
        tipoAsunto: expediente.expediente.catTipoAsunto,
        cuaderno: expediente.expediente.nombreCorto,
        cuadernoId: expediente.tipoCuadernoId,
      };
      this.tabsTramite.push(exp);
      this.tabActive = expediente.id;
    },

    delTabExpediente(index) {
      const copyTabs = [...this.tabsTramite];
      this.tabsTramite.splice(index, 1);
      if (copyTabs[index].id == this.tabActive) {
        if (this.tabsTramite.length == 0) {
          this.tabActive = DEFAULT_TAB_TRAMITE;
        } else {
          if (this.tabsTramite.length - 1 >= index) {
            this.tabActive = this.tabsTramite[index].id;
          } else {
            this.tabActive = this.tabsTramite[this.tabsTramite.length - 1].id;
          }
        }
      }
    },
    setTabActive(tab) {
      this.tabActive = tab;
    },
  },
});
