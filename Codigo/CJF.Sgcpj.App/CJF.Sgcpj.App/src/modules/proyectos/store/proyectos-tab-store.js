import { defineStore } from "pinia";

const DEFAULT_TAB_PROYECTO = "proyectos";

export const useProyectosTabStore = defineStore("ProyectoTabStore", {
  state: () => ({
    tabsProyectos: [],
    tabActive: DEFAULT_TAB_PROYECTO,
  }),
  actions: {
    addTabExpediente(row) {
      let expediente = row.expediente;
      expediente.id = expediente.asuntoNeunId;
      if (
        this.tabsProyectos.filter((tab) => tab.id === expediente.asuntoNeunId)
          .length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }

      this.tabsProyectos.push(expediente);
      this.tabActive = expediente.id;
    },

    delTabExpediente(index) {
      const copyTabs = [...this.tabsProyectos];
      this.tabsProyectos.splice(index, 1);
      if (copyTabs[index].id == this.tabActive) {
        if (this.tabsProyectos.length == 0) {
          this.tabActive = DEFAULT_TAB_PROYECTO;
        } else {
          if (this.tabsProyectos.length - 1 >= index) {
            this.tabActive = this.tabsProyectos[index].id;
          } else {
            this.tabActive =
              this.tabsProyectos[this.tabsProyectos.length - 1].id;
          }
        }
      }
    },

    setTabActive(tab) {
      this.tabActive = tab;
    },
  },
});
