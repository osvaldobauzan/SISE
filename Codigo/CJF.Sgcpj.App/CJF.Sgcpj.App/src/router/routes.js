// import authRoutes from "../modules/auth/router";

const routes = [
  {
    meta: { requiresAuth: true },
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      {
        path: "",
        component: () => import("../modules/index/pages/IndexPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 2 },
        path: "oficialia",
        component: () => import("../modules/oficialia/pages/OficialiaPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 2 },
        path: "oficialia/:urlVerPromo",
        component: () => import("../modules/oficialia/pages/OficialiaPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 13 },
        path: "tramite",
        component: () => import("../modules/tramite/pages/TramitePage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 13 },
        path: "tramite/:status",
        component: () => import("../modules/tramite/pages/TramitePage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 25 },
        path: "actuaria",
        component: () => import("../modules/actuaria/pages/ActuariaPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 25 },
        path: "actuaria/:status",
        component: () => import("../modules/actuaria/pages/ActuariaPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 44 },
        path: "actuario",
        component: () => import("../modules/actuaria/pages/ActuarioPage.vue"),
      },
      {
        // TODO: pendiente de definir privilegio
        meta: { requiresAuth: true, privilegio: 68 },
        path: "proyectos",
        component: () => import("../modules/proyectos/pages/ProyectosPage.vue"),
      },
      {
        // TODO: pendiente de definir privilegio
        meta: { requiresAuth: true, privilegio: 52 },
        path: "listas",
        component: () => import("../modules/listas/pages/ListasPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 92 },
        path: "sentencias",
        component: () =>
          import("../modules/sentencias/pages/SentenciasPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 52 },
        path: "sentencias/:status",
        component: () =>
          import("../modules/sentencias/pages/SentenciasPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 53 },
        path: "ejecucion",
        component: () => import("../modules/ejecucion/pages/EjecucionPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 53 },
        path: "ejecucion/:status",
        component: () => import("../modules/ejecucion/pages/EjecucionPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 54 },
        path: "seguimiento",
        component: () =>
          import("../modules/seguimiento/pages/SeguimientoPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 55 },
        path: "oficios",
        component: () => import("../modules/libreta/pages/LibretaPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 56 },
        path: "expediente",
        component: () =>
          import("../modules/expediente/pages/ExpedientePage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 56 },
        path: "expedienteCaptura",
        component: () => import("../modules/expediente/pages/CapturaPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 56 },
        path: "expedientePartes",
        component: () => import("../modules/expediente/pages/PartesPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 56 },
        path: "expedienteVincular",
        component: () => import("../modules/expediente/pages/VincularPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 57 },
        path: "calculadoras",
        component: () =>
          import("../modules/calculadora/pages/CalculadoraPage.vue"),
      },
      {
        meta: { requiresAuth: true, privilegio: 58 },
        path: "configuracion",
        component: () =>
          import("../modules/configuracion/pages/ConfiguracionPage.vue"),
      },
      {
        //TODO: pendiente definir privilegio agenda
        meta: { requiresAuth: true, privilegio: 1 },
        path: "agenda",
        component: () =>
          import("../modules/agenda/pages/AgendaPage.vue"),
      },
    ],
  },
  // {
  //   path: "/auth",
  //   ...authRoutes,
  // },
  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/ErrorNotFound.vue"),
  },
  {
    path: "/forbidden",
    component: () => import("pages/ErrorForbidden.vue"),
  },
];

export default routes;
