import { defineStore } from "pinia";

export const useMenuStore = defineStore("menu", {
  state: () => ({
    menu: [
      {
        icon: "mdi-view-dashboard-outline",
        icon_selected: "mdi-view-dashboard",
        text: "Gráficas de productividad",
        link: "/",
        isActive: true,
        isVisible: true,
      },
      {
        icon: "mdi-inbox-outline",
        icon_selected: "mdi-inbox",
        text: "Oficialía de partes",
        link: "oficialia",
        isActive: false,
        privilegio: 2,
        isVisible: true,
      },
      {
        icon: "mdi-file-cog-outline",
        icon_selected: "mdi-file-cog",
        text: "Tablero de trámite",
        link: "tramite",
        isActive: false,
        privilegio: 13,
        isVisible: true,
      },
      {
        icon: "mdi-email-outline",
        icon_selected: "mdi-email",
        text: "Actuaría",
        link: "actuaria",
        isActive: false,
        privilegio: 25,
        isVisible: true,
      },
      {
        icon: "mdi-email-arrow-right-outline",
        icon_selected: "mdi-email-arrow-right",
        text: "Actuario",
        link: "actuario",
        isActive: false,
        privilegio: 44,
        isVisible: true,
      },
      // TODO: pendiente de definir privilegio
      {
        icon: "mdi-book-alert-outline",
        icon_selected: "mdi-book-alert",
        text: "Proyectos",
        link: "proyectos",
        isActive: false,
        privilegio: 68,
        isVisible: true,
      },
      // TODO: pendiente de definir privilegio
      {
        icon: "mdi-list-box-outline",
        icon_selected: "mdi-list-box",
        text: "Listas",
        link: "listas",
        isActive: false,
        privilegio: 52,
        isVisible: false,
      },
      {
        icon: "mdi-book-check-outline",
        icon_selected: "mdi-book-check",
        text: "Sentencias",
        link: "sentencias",
        isActive: true,
        privilegio: 92,
        isVisible: true,
      },
      {
        icon: "mdi-gavel",
        icon_selected: "mdi-gavel",
        text: "Ejecución",
        link: "ejecucion",
        isActive: false,
        privilegio: 53,
        isVisible: false,
      },
      {
        icon: "mdi-calendar-month-outline",
        icon_selected: "mdi-calendar-month",
        text: "Agenda",
        link: "agenda",
        isActive: false,
        privilegio: 1,//TODO: pendiente definir privilegio
        isVisible: true,
      },
      {
        icon: "mdi-apps",
        icon_selected: "mdi-apps-box",
        text: "Más módulos",
        esSubmenu: false,
        isVisible: false,
        subMenu: [
          {
            icon: "mdi-arrow-decision-outline",
            icon_selected: "mdi-arrow-decision",
            text: "Seguimiento",
            link: "seguimiento",
            isActive: false,
            privilegio: 54,
            isVisible: true,
          },
          {
            icon: "mdi-notebook-outline",
            icon_selected: "mdi-notebook",
            text: "Libreta de oficios",
            link: "oficios",
            isActive: false,
            privilegio: 55,
            isVisible: true,
          },
          {
            icon: "mdi-folder-outline",
            icon_selected: "mdi-folder",
            text: "Expediente electrónico",
            link: "expediente",
            isActive: false,
            privilegio: 56,
            isVisible: true,
          },
          {
            icon: "mdi-calculator",
            icon_selected: "mdi-calculator",
            text: "Calculadoras",
            link: "calculadoras",
            isActive: false,
            privilegio: 57,
            isVisible: true,
          },
        ],
      },
    ],
  }),
  getters: {
    selectedItem: (state) => state.menu.find((item) => item.isActive),
  },
  actions: {
    setActive(selectedItem) {
      if (typeof selectedItem.link != "string" || !!!selectedItem.link) return;
      if (selectedItem.link.length > 1) {
        selectedItem.link = selectedItem.link.split("/")[1];
      }
      this.menu.forEach((item) => {
        if (item.esSubmenu) {
          item.subMenu.forEach((sub) => {
            if (sub.link == selectedItem.link) {
              sub.isActive = true;
            } else {
              sub.isActive = false;
            }
          });
        } else {
          if (item.link == selectedItem.link) {
            item.isActive = true;
          } else {
            item.isActive = false;
          }
        }
      });
    },
  },
});
