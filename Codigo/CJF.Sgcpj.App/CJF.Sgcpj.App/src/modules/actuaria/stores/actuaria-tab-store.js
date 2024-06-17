import { defineStore } from "pinia";

const DEFAULT_TAB_ACUERDO = "actuaria";

export const useActuariaTabStore = defineStore("ActuariaTabStore", {
  state: () => ({
    tabsAcuerdos: [],
    tabActive: DEFAULT_TAB_ACUERDO,
  }),
  actions: {
    addTabExpediente(expediente) {
      expediente.id = expediente.expediente.asuntoNeunId;
      if (
        this.tabsAcuerdos.filter((tab) => tab.id === expediente.asuntoNeunId)
          .length > 0
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
        cuadernoId: expediente.tipoCuaderno,
      };
      this.tabsAcuerdos.push(exp);
      this.tabActive = expediente.id;
    },
    delTabExpediente(index) {
      const copyTabs = [...this.tabsAcuerdos];
      this.tabsAcuerdos.splice(index, 1);
      if (copyTabs[index].id == this.tabActive) {
        if (this.tabsAcuerdos.length == 0) {
          this.tabActive = DEFAULT_TAB_ACUERDO;
        } else {
          if (this.tabsAcuerdos.length - 1 >= index) {
            this.tabActive = this.tabsAcuerdos[index].id;
          } else {
            this.tabActive = this.tabsAcuerdos[this.tabsAcuerdos.length - 1].id;
          }
        }
      }
    },
    setTabActive(tab) {
      this.tabActive = tab;
    },
  },
});
