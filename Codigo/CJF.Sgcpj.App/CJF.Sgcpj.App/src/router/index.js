import { route } from "quasar/wrappers";
import {
  createRouter,
  createMemoryHistory,
  createWebHistory,
  createWebHashHistory,
} from "vue-router";
import routes from "./routes";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { msal } from "src/boot/axios";
import { useMenuStore } from "src/stores/menu-store";

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Router instance.
 */

export default route(function (/* { store, ssrContext } */) {
  const authStore = useAuthStore();
  const menuStore = useMenuStore();
  const createHistory = process.env.SERVER
    ? createMemoryHistory
    : process.env.VUE_ROUTER_MODE === "history"
      ? createWebHistory
      : createWebHashHistory;

  const Router = createRouter({
    scrollBehavior: () => ({ left: 0, top: 0 }),
    routes,

    // Leave this as is and make changes in quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    history: createHistory(process.env.VUE_ROUTER_BASE),
  });
  Router.beforeEach(async (to, from, next) => {
    if (to.path.includes("code")) {
      next("/");
    } else if (to.path.includes("error")) {
      next("/");
      await authStore.logoutUser();
    }
    const requiresAuth = to.matched.some((record) => record.meta?.requiresAuth);
    let IsAuthenticated = !!msal.getAllAccounts()?.length > 0;
    if (requiresAuth && !IsAuthenticated) {
      await authStore.logIn();
      IsAuthenticated = !!msal.getAllAccounts()?.length > 0;

      if (IsAuthenticated) {
        if (
          authStore.user.privilegios?.some((p) => p == to.meta?.privilegio) ||
          !to.meta?.privilegio
        ) {
          next();
        } else {
          next("/forbidden");
        }
      } else {
        next(false);
      }
    } else {
      await authStore.refreshToken();
      if (!authStore.user?.catOrganismoId && to.path !== "/") {
        next("/");
      }
      if (
        authStore.user.privilegios?.some((p) => p == to.meta?.privilegio) ||
        !to.meta?.privilegio
      ) {
        next();
      } else {
        next("/forbidden");
      }
    }
  });
  Router.afterEach((to) => {
    menuStore.setActive({ link: to.path });
  });
  return Router;
});
