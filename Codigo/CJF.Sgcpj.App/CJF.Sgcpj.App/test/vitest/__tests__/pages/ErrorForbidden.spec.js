import { shallowMount } from "@vue/test-utils";
import ErrorForbidden from "src/pages/ErrorForbidden.vue";
import { expect, it, describe, beforeEach } from "vitest";
import { Quasar, QBtn } from "quasar";
import { createRouter, createWebHistory } from "vue-router";

// Set up router
const routes = [{ path: "/", component: { template: "Home page content" } }];
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

// Define Quasar Plugin for shallowMount
const quasarPlugin = {
  install: (app) => {
    app.config.globalProperties.$q = {
      // Mocking quasar globals
      platform: {},
    };
    app.use(Quasar, { components: { QBtn } }); // Specify components used
  },
};
describe("ErrorForbidden.vue", () => {
  let wrapper;

  beforeEach(() => {
    wrapper = shallowMount(ErrorForbidden);
  });

  it('renders a div with class "fullscreen"', () => {
    const fullscreenDiv = wrapper.find(".fullscreen");
    expect(fullscreenDiv.exists()).toBe(true);
  });

  it("renders error code 403 in large font", () => {
    const errorCode = wrapper.find('div[style*="font-size: 30vh;"]');
    expect(errorCode.text()).toContain("403");
  });

  it("renders the error message", () => {
    const errorMessage = wrapper.find(".text-h2");
    expect(errorMessage.text()).toContain(
      "No tiene permiso para acceder al directorio en este servidor",
    );
  });

  it("the route forbidden exist", async () => {
    router.push("/forbidden");
    await router.isReady();
    expect(router.currentRoute.value.path).toBe("/forbidden");
  });
  it('has a button with label "Ir al inicio"', () => {
    const wrapper = shallowMount(ErrorForbidden, {
      global: {
        plugins: [router, quasarPlugin],
      },
    });
    const button = wrapper.findComponent(QBtn);
    expect(button.exists()).toBe(true);
    expect(button.props().label).toBe("Ir al inicio");
  });
});
