import { defineStore } from "pinia";

const DEFAULT_TAB_SENTENCIA = "sentencias";

export const useSentenciaTabStore = defineStore("SentenciaTabStore", {
  state: () => ({
    tabsSentencia: [],
    tabActive: DEFAULT_TAB_SENTENCIA,
  }),
  actions: {
    addTabExpediente(row) {
      let expediente = row.expediente;
      expediente.id = expediente.asuntoNeunId;
      if (
        this.tabsSentencia.filter((tab) => tab.id === expediente.asuntoNeunId)
          .length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }

      this.tabsSentencia.push(expediente);
      this.tabActive = expediente.id;
    },

    delTabExpediente(index) {
      const copyTabs = [...this.tabsSentencia];
      this.tabsSentencia.splice(index, 1);

      if (copyTabs[index].id == this.tabActive) {
        if (this.tabsSentencia.length == 0) {
          this.tabActive = DEFAULT_TAB_SENTENCIA;
        } else {
          if (this.tabsSentencia.length - 1 >= index) {
            this.tabActive = this.tabsSentencia[index].id;
          } else {
            this.tabActive =
              this.tabsSentencia[this.tabsSentencia.length - 1].id;
          }
        }
      }
    },

    setTabActive(tab) {
      this.tabActive = tab;
    },
  },
});
