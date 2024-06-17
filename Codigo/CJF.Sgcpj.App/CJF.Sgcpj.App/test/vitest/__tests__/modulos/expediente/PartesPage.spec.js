import { shallowMount } from "@vue/test-utils";
import PartesPage from "../../../../../src/modules/expediente/pages/PartesPage.vue";
import { expect, test, describe } from "vitest";
import { createRouter, createWebHistory } from "vue-router";

// Set up router
let routes = [{ path: "/", component: { template: "Home page content" } }];
let router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});
describe("PartesPage.vue", () => {
  test("PartesPage renderiza el componente", () => {
    const wrapper = shallowMount(PartesPage);
    expect(wrapper.exists()).toBeTruthy();
  });
  test("computed filteredParts ", () => {
    const wrapper = shallowMount(PartesPage);
    expect(wrapper.vm.filteredParts).toBeTruthy();
    wrapper.vm.searchText = "hola";
    wrapper.vm.partes = [];
    expect(wrapper.vm.filteredParts).toBeTruthy();
  });
  test("funcion agrupaOrdenaDatos ", () => {
    const wrapper = shallowMount(PartesPage);
    expect(
      wrapper.vm.agrupaOrdenaDatos([{ padre: 0, personaId: 0 }]),
    ).toBeTruthy();
  });
  test("funcion back /", async () => {
    const wrapper = shallowMount(PartesPage, {
      global: {
        plugins: [router],
      },
    });
    await router.isReady();
    wrapper.vm.back();
    expect(router.currentRoute.value.path).toEqual("/");
  });
  test("funcion back /oficialia", async () => {
    routes = [{ path: "/", component: { template: "Home page content" } }];
    router = createRouter({
      history: createWebHistory(process.env.BASE_URL),
      routes,
    });
    const wrapper = shallowMount(PartesPage, {
      global: {
        plugins: [router],
      },
    });
    router.push("/oficialia");
    await router.isReady();
    wrapper.vm.back();
    expect(router.currentRoute.value.path).toEqual("/oficialia");
  });
  test("funcion back /tramite", async () => {
    routes = [{ path: "/", component: { template: "Home page content" } }];
    router = createRouter({
      history: createWebHistory(process.env.BASE_URL),
      routes,
    });
    const wrapper = shallowMount(PartesPage, {
      global: {
        plugins: [router],
      },
    });
    router.push("/tramite");
    await router.isReady();
    router.push("/tramite");
    wrapper.vm.back();
    expect(router.currentRoute.value.path).toEqual("/tramite");
  });
  test("funcion back /actuaria", async () => {
    routes = [{ path: "/", component: { template: "Home page content" } }];
    router = createRouter({
      history: createWebHistory(process.env.BASE_URL),
      routes,
    });
    const wrapper = shallowMount(PartesPage, {
      global: {
        plugins: [router],
      },
    });
    router.push("/actuaria");
    await router.isReady();
    wrapper.vm.back();
    expect(router.currentRoute.value.path).toEqual("/actuaria");
  });
  // test("funcion formatoFecha", () => {
  //   const wrapper = shallowMount(PartesPage);
  //   const fecha = new Date(2024, 1, 20);
  //   const dia = fecha.getDate().toString().padStart(2, "0");
  //   const mes = (fecha.getMonth() + 1).toString().padStart(2, "0");
  //   const año = fecha.getFullYear().toString();
  //   const resultado = `${dia}/${mes}/${año}`;
  //   expect(wrapper.vm.formatoFecha(fecha)).toEqual(resultado);
  // });
});
